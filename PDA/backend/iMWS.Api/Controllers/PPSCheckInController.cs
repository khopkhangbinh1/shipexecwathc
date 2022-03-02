using iMWS.Api.Db;
using iMWS.Api.Filters;
using iMWS.Api.Models.PPSPick;
using iMWS.Api.Models.WCF;
using iMWS.Api.Models.Bean;
using iMWS.Api.Models.PPSUSER;
using iMWS.Api.Models.Service;
using WcfHost;
using WcfHost.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace iMWS.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/ppscheckin")]
    // [JwtAuth]
    public class PPSCheckInController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        [HttpPost]
        [Route("CheckInTrolleyNo")]
        public IHttpActionResult CheckInTrolleyNo(string TrolleyNo)
        {
            string errmsg = "";
            try
            {
                PPSDb ppsDb = new PPSDb();
                string sql = @"select  Trolley_no  from ppsuser.t_trolley_line_info where  trolley_no=:TrolleyNo";
                var list = ppsDb.Query<baseinfo>(sql, new { TrolleyNo });
                if (list.Count() > 0)
                {
                    string sql1 = @"select Pallet_No from ppsuser.t_location
                        where pallet_no in 
                        (select pallet_no
                        from ppsuser.t_trolley_sn_status
                        where trolley_no =:TrolleyNo
                    union
                    select pallet_no
                        from ppsuser.t_trolley_mpart_sn
                        where trolley_no=:TrolleyNo'";
                    var list1 = ppsDb.Query<baseinfo>(sql, new { TrolleyNo });
                    if (list1.Count() > 0)
                    {
                        return Ok(Result.Create(true, ""));
                    }
                    else
                    {
                        errmsg = string.Format("Trolley:{0} used by Pallet:{1}", TrolleyNo, list.ToString());
                        return Ok(Result.Create(false, "" + errmsg));

                    }
                }
                else
                {
                    errmsg = "Check Trolley No Error ! pls check.";
                    return Ok(Result.Create(false, "" + errmsg));

                }
            }
            catch (Exception ex)
            {
                errmsg = ex.Message;
            }
            return Ok(Result.Create(true, "" + errmsg));

        }


        [HttpPost]
        [Route("CheckInTrolleyLineNo")]
        public IHttpActionResult CheckInTrolleyLineNo(baseinfo bean)
        {
            string errmsg = "";
            try
            {
                PPSDb ppsDb = new PPSDb();
                bean.Sides_No = bean.Trolley_Line_No.Substring(0, 1);
                bean.Trolley_Line_No = bean.Trolley_no + "-" + bean.Trolley_Line_No;
                string sql = "select * from ppsuser.t_trolley_line_info where trolley_no=:Trolley_no";
                var p = ppsDb.Query<string>(sql, bean).FirstOrDefault();
                if (p != null)
                {
                    string sql1 = "select * from ppsuser.T_TROLLEY_MPART_SN  where trolley_no =:Trolley_no  and trolley_line_no =:Trolley_Line_No";
                    var p1 = ppsDb.Query<string>(sql1, bean).FirstOrDefault();
                    if (p1 != null)
                    {
                        errmsg = "Trolley Line has be Used ! pls check.";
                        return Ok(Result.Create(false, "" + errmsg));
                    }
                    else
                    {
                        string sql2 = @"select A.Trolley_Line_No, a.MaxQty, nvl(b.usedqty, 0) as UsedQty
                                          from (selecT distinct trolley_line_no, maxqty
                                                  from t_trolley_line_info
                                                 where trolley_no =:trolley_no
                                                   and sides_no =:sides_no) A,
                                               (select trolley_no || '-' || sides_no || lpad(level_no, 2, 0) as trolley_line_no,
                                                       count(custom_sn) as usedqty
                                                  from t_trolley_mpart_sn
                                                 where trolley_no =:trolley_no
                                                 group by trolley_no || '-' || sides_no || lpad(level_no, 2, 0)) B
                                         where A.TROLLEY_LINE_NO = B.trolley_line_no(+)
                                         order by A.Trolley_Line_No
                                        ";
                        var list = ppsDb.Query<baseinfo>(sql2, new { trolley_no = bean.Trolley_no, sides_no = bean.Sides_No });
                        return Ok(Result.Create(true, "", list));
                    }
                }
                else
                {
                    errmsg = "Check Trolley Line No Error ! pls check.";
                    return Ok(Result.Create(false, "" + errmsg));
                }
            }
            catch (Exception ex)
            {
                errmsg = ex.Message;
            }
            return Ok(Result.Create(false, "" + errmsg));

        }


        [HttpPost]
        [Route("CheckInCartonNo")]
        public IHttpActionResult CheckInCartonNo(TrolleyBean tbean)
        {
            string errmsg = "";
            try
            {
                PPSDb ppsDb = new PPSDb();
                tbean.trolley_line_no = tbean.trolley_no + "-" + tbean.trolley_line_no;
                tbean.sides_no = tbean.trolley_line_no.Substring(0, 1);
                tbean.level_no = tbean.trolley_line_no.Substring(tbean.trolley_line_no.Length - 2);
                //string sql = "select distinct pallet_no from ppsuser.T_TROLLEY_MPART_SN  where trolley_no =:trolley_no";
                tbean.pallet_no = "SYS";//ppsDb.Query<string>(sql, tbean).FirstOrDefault();
                string sql = "select part_no from ppsuser.t_sn_status a, pptest.oms_partmapping b where a.customer_sn = carton_no and a.part_no = b.part(+)";
                tbean.ictpartno = ppsDb.Query<string>(sql, tbean).FirstOrDefault();
                var m = ppsDb.InsertCarton(tbean);
                errmsg = m.ErrMsg;
                string sql1 = @"select * from ppsuser.t_trolley_sns a where a.trolley_line_no=:Trolley_line_no";
                var list1 = ppsDb.Query<baseinfo>(sql1, new { Trolley_line_no=tbean.trolley_line_no });
                string sql2 = @"select * from ppsuser.t_trolley_mpart_sn a where a.trolley_no=:Trolley_no and a.sides_no=Sides_no";
                var list2 = ppsDb.Query<baseinfo>(sql2, new { Trolley_no=tbean.trolley_no, Sides_no =tbean.sides_no});
                //string sql3 = @"select * from ppsuser.t_trolley_mpart_sn a where a.trolley_no=:Trolley_no ";
                //var list3 = ppsDb.Query<baseinfo>(sql3, new { Trolley_no=tbean.trolley_no });
                int htj = list1.Count();
                int mtj = list2.Count();
                //int ctj = list3.Count();
                if (errmsg.Equals("OK"))
                {
                    string sql4 = @"select a.trolley_line_no,a.pointno,a.custom_sn,a.ictpartno,a.pallet_no from ppsuser.T_TROLLEY_SNS a where a.trolley_no=:trolley_no ";
                    var list = ppsDb.Query<TrolleyBean>(sql4, new { trolley_no = tbean.trolley_no });
                    /*
                    if  (ctj == 196)
                    {
                        OnCommit("sscc");
                        string sql6 = @"select a.seq_no from ppsuser.t_trolley_mpart_sn A  where a.trolley_no=:trolley_no";
                        var list11 = ppsDb.Query<TrolleyBean>(sql6, new { trolley_no = tbean.trolley_no });
                        return Ok(Result.Create(true, "", list11));
                    }*/
                    if (htj == 4)
                    {
                        OnCommit("sscc");
                        string sql5 = @"select a.trolley_line_no,a.pointno,a.custom_sn,a.ictpartno,a.pallet_no from ppsuser.T_TROLLEY_SNS a where a.trolley_no=:trolley_no ";
                        var list11 = ppsDb.Query<TrolleyBean>(sql5, new { trolley_no = tbean.trolley_no });
                        return Ok(Result.Create(true, "", list11));
                    }
                    else if (htj < 4 )
                    {
                        return Ok(Result.Create(true, "", list));

                    }
                }
                else
                {
                    errmsg = "Insert sns error! pls check.";
                    return Ok(Result.Create(false, "" + errmsg));

                }
            }
            catch (Exception ex)
            {
                errmsg = ex.Message;
            }
            return Ok(Result.Create(false, "" + errmsg));
        }

        [HttpPost]
        [Route("OnCommit")]
        public IHttpActionResult OnCommit(string sscc)
        {
            string errmsg = "";
            try
            {
                PPSDb ppsDb = new PPSDb();
                if (sscc != "" && sscc != null)
                {
                    string sql1 = @"select  *  from ppsuser.T_TROLLEY_SNS ";
                    var list = ppsDb.Query<TrolleyBean>(sql1);
                    for (int i = 0; i < list.Count(); i++)
                    {
                        string sql2 = @"select  *  from ppsuser.T_TROLLEY_SNS where rownum='1'";
                        var list1 = ppsDb.Query<TrolleyBean>(sql2).ToArray();
                        string custom_sn = list1[0].custom_sn;
                        string pallet_no = list1[0].pallet_no;
                        string trolley_line_no = list1[0].trolley_line_no;
                        string pointno = list1[0].pointno.ToString();
                        string part = list1[0].ictpartno;
                        string eid = list1[0].emp_id;
                        var m = ppsDb.Commit(custom_sn, pallet_no, trolley_line_no, pointno, part, eid);
                        errmsg = m.ErrMsg;
                    }
                    return Ok(Result.Create(false, ""));
                }
                else
                {
                    errmsg = "T_TROLLEY_SNS为空! pls check.";
                    return Ok(Result.Create(false, "" + errmsg));
                }
            }
            catch (Exception ex)
            {
                errmsg = ex.Message;
            }
            return Ok(Result.Create(false, "" + errmsg));
        }



        [HttpPost]
        [Route("CallAGV")]
        public IHttpActionResult CallAGV(string trolleyno)
        {
            string errmsg = "";
            try
            {
                PPSDb ppsDb = new PPSDb();
                if (!string.IsNullOrEmpty(trolleyno))
                {
                    iMWS.Api.Models.WCF.IEOSGateway ws = HttpChannel.Get<iMWS.Api.Models.WCF.IEOSGateway>("http://10.33.20.185:8101/EOSGateway");
                    string data = "{\"WAREHOUSE_FROM_ID\": \"E3\",";
                    data += "\"TROLLEYID\":\"" + trolleyno + "\",";
                    data += "\"OPTYPE\": \"CHECKINSTOCKIN\"}";
                    string res = ws.PostStockInNotify(data, "" + errmsg);
                    return Ok(Result.Create(true, ""));
                }
                else
                {
                    errmsg = "trolley_no! pls check.";
                    return Ok(Result.Create(false, "" + errmsg));
                }
            }
            catch (Exception ex)
            {
                errmsg = ex.Message;
            }
            return Ok(Result.Create(true, "" + errmsg));
        }


        [HttpPost]
        [Route("SelectLocation")]
        public IHttpActionResult SelectLocation(string Location)
        {
            string errmsg = "";
            try
            {
                PPSDb ppsDb = new PPSDb();
                if (Location != "" && Location != null)
                {
                    string sql1 = @"select pallet_no,part_no,cartonqty from ppsuser.t_location where location_name=:Location ";
                    var list = ppsDb.Query<Location>(sql1, new { Location = Location });
                    return Ok(Result.Create(true, "", list));
                }
                else
                {
                    errmsg = "Location is null! pls check.";
                    return Ok(Result.Create(false, "" + errmsg));
                }
            }
            catch (Exception ex)
            {
                errmsg = ex.Message;
            }
            return Ok(Result.Create(false, "" + errmsg));
        }



    }
}





