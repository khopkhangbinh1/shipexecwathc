using iMWS.Api.Db;
using iMWS.Api.Filters;
using iMWS.Api.Models.PPSCheck;
using iMWS.Api.Models.Bean;
using iMWS.Api.Models.PPSUSER;
using iMWS.Api.Models.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ClientUtilsDll;

namespace iMWS.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/ppscheck")]
    public class CheckController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpGet]
        [Route("getWHS")]
        public IHttpActionResult GetWHS()
        {
            PPSDb ppsDb = new PPSDb();

            String strSql = "select warehouse_id id,warehouse_No name  from ppsuser.WMS_WAREHOUSE  where enabled = 'Y' and warehouse_No<> 'SYS' ORDER BY WAREHOUSE_NO";

            var list = ppsDb.Query<CheckCNTVewModel>(strSql);
            if (list.Count() == 0)
            {
                return Ok(Result.Create(false, "No Data!"));
            }
            return Ok(Result.Create(true, "", list));
        }
        public string DelPrefixCartonSN(string insn)
        {
            if (insn.Length == 20 && insn.Substring(0, 2).Equals("00"))
            { insn = insn.Substring(2); }
            else if (insn.StartsWith("3S"))
            { insn = insn.Substring(2); }
            else if (insn.StartsWith("S"))
            { insn = insn.Substring(1); }

            return insn;

        }
        [HttpPost]
        [Route("inventoryCTN")]
        public IHttpActionResult inventoryCTN(CheckCNTModel vm)
        {
            PPSDb ppsDb = new PPSDb();

            bool rdoSN = true;
            string insn = vm.CTN;
            if (insn == null)
            {

                return Ok(Result.Create(false, "输入的 不能为空！"));
            }
            insn = DelPrefixCartonSN(insn);
            insn = ppsDb.ChangeCSNtoCarton(insn);
          //  bool checkHool = false;
            if (vm.checkHold == "Y")
            {
                var m2 = ppsDb.CheckHold(insn, "SERIALNUMBER");
                string errmsg2 = m2.RetMsg;
                if (!errmsg2.Equals("OK"))
                {
                    return Ok(Result.Create(false, "QH:" + errmsg2));
                }
            }
            if (rdoSN)
            {
                var m3 = ppsDb.WMSStockCheckBySP(vm.locationID, insn, vm.IsFirst, vm.empNo, out string tempname);
                String errmsg = m3;

                if (errmsg.Equals("OK-FINISH"))
                {
                    return Ok(Result.Create(true, "" + errmsg));
                }
                else if (m3.Equals("OK"))
                {
                    return Ok(Result.Create(true, "" + errmsg));
                }
                else if (errmsg.ToString().Contains("WA"))
                {
                    return Ok(Result.Create(false, "" + errmsg));
                }
                else
                {
                    return Ok(Result.Create(false, "" + errmsg));
                }
            }
            return Ok(Result.Create(false, ""));
        }

        [HttpPost]
        [Route("inventoryPallet")]
        public IHttpActionResult inventoryPallet(CheckCNTModel vm)
        {
            PPSDb ppsDb = new PPSDb();
            string insn = vm.CTN;
            String insn2 = vm.CTN2;
            if (insn == null)
            {
                return Ok(Result.Create(false, "输入的 不能为空！"));
            }
            insn = DelPrefixCartonSN(insn);
            insn = ppsDb.ChangeCSNtoCarton(insn);
            insn2 = DelPrefixCartonSN(insn2);
            insn2 = ppsDb.ChangeCSNtoCarton(insn2);
            bool checkHool = false;
            if (vm.checkCNTPllet == "Y")
            {
                var m3 = ppsDb.WMSStockCheckBySP2(vm.locationID, insn, insn2, vm.palletCartonQTY, vm.IsFirst, vm.empNo, out string tempname);
                String errmsg = m3;

                if (errmsg.Equals("OK-FINISH"))
                {
                    return Ok(Result.Create(true, "" + errmsg));
                }
                else if (m3.Equals("OK"))
                {
                    return Ok(Result.Create(true, "" + errmsg));
                }
                else if (errmsg.ToString().Contains("WA"))
                {
                    return Ok(Result.Create(false, "" + errmsg));
                }
                else
                {
                    return Ok(Result.Create(false, "" + errmsg));
                }
            }
            return Ok(Result.Create(false, ""));
        }
        [HttpGet]
        [Route("getwh")]
        public IHttpActionResult getwh()
        {
            PPSDb ppsDb = new PPSDb();

            string sql = @" select warehouse_id id, warehouse_name  name
                                   from ppsuser.wms_warehouse 
                                   where enabled = 'Y' and warehouse_no <> 'sys' order by warehouse_no";
            var list = ppsDb.Query<CheckCNTVewModel>(sql);
            if (list.Count() == 0)
            {
                return Ok(Result.Create(false, "No Data!"));
            }
            return Ok(Result.Create(true, "", list));

        }
        [HttpGet]
        [Route("getlocationinventory")]
        public IHttpActionResult getlocationinventory(string id)
        {
            PPSDb ppsDb = new PPSDb();

            string strsql = string.Format("select location_id id, location_no name "
                             + "     from ppsuser.wms_location "
                             + "    where location_no in "
                             + "          (select location_no from ppsuser.t_location where qty > 0) "
                             + "      and enabled = 'Y' "
                             + "      and warehouse_id = '{0}'"
                             + "    order by location_no", id);
            var list = ppsDb.Query<CheckCNTVewModel>(strsql);
            if (list.Count() == 0)
            {
                return Ok(Result.Create(false, "No Data!"));
            }
            return Ok(Result.Create(true, "", list));

        }
        [HttpGet]
        [Route("getLocationDetail")]
        public IHttpActionResult getLocationDetail(String whID, String LocationNo)
        {
            PPSDb ppsDb = new PPSDb();

            String strSql = "SELECT Part_NO ICTPN,location_id locationID,Pallet_NO palletNo,CartonQTY cartonQTY ,qty  Qty, QHCARTONQTY  QHCaton,QHQTY QHQty FROM PPSUSER.T_LOCATION where 1 = 1   ";


            if (whID != null)
            {
                strSql += " and warehouse_id = '{0}'";
            }
            if (LocationNo != "")
            {
                strSql += " and location_no = '{1}'";
            }
            string sql = string.Format(strSql, whID, LocationNo);
            var list = ppsDb.Query<CheckCNTVewModel>(sql);
            if (list.Count() == 0)
            {
                return Ok(Result.Create(false, "No Data!"));
            }
            return Ok(Result.Create(true, "", list));
        }
        [HttpGet]
        [Route("getLocationSnInfo")]
        public IHttpActionResult getLocationSnInfo(String LocationNo)
        {
            PPSDb ppsDb = new PPSDb();


            String strSql = string.Format(@"select distinct a.pallet_no palletNo, a.carton_no CTNSN, a.part_no ICTPN, a.location_no locationNo
                                  from ppsuser.t_sn_status a
                                  join ppsuser.t_location b
                                    on a.location_id = b.location_id
                                 where a.wc = 'W0'
                                   and a.location_no = '{0}'
                                ", LocationNo);
            var list = ppsDb.Query<CheckCNTVewModel>(strSql);
            if (list.Count() == 0)
            {
                return Ok(Result.Create(false, "No Data!"));
            }
            return Ok(Result.Create(true, "", list));
        }
        [HttpGet]
        [Route("getLocationCheckLog")]
        public IHttpActionResult getLocationCheckLog(String LocationNo)
        {
            PPSDb ppsDb = new PPSDb();

            string strsql = string.Format(@"select daycode        datetime,
                                        checktime checktime,
                                       pallet_no      palletno,
                                       cartonqty      ctnqty,
                                       passcartonqty  passcartonqty,
                                       errorcartonqty errorcartonqty,
                                       result         result
                                  from ppsuser.t_location_check
                                 where daycode = to_char(sysdate, 'yyyy-mm-dd')
                                   and location_no = '{0}'
                                 order by checktime desc
                                ", LocationNo);
            var list = ppsDb.Query<CheckCNTVewModel>(strsql);
            if (list.Count() == 0)
            {
                return Ok(Result.Create(false, "No Data!"));
            }
            return Ok(Result.Create(true, "", list));
        }

        [HttpPost]
        [Route("CheckPalletCarton")] 
        public IHttpActionResult CheckPalletCarton(String insn, string insn2)
        {
            PPSDb ppsDb = new PPSDb();
            if (ppsDb.CheckPalletCarton(insn, insn2) == "No Data!")
            {
                return Ok(Result.Create(false, "输入的箱号与栈板号不匹配"));
            }
            else
            {
                return Ok(Result.Create(true, ""));
            }
            return Ok(Result.Create(false, "输入的箱号与栈板号不匹配"));
        }
        [HttpGet]
        [Route("GetSnInfoBySQL")] 
        public IHttpActionResult GetSnInfoBySQL(String insn)
        {
            PPSDb ppsDb = new PPSDb();
            insn = DelPrefixCartonSN(insn);
            insn = ppsDb.ChangeCSNtoCarton(insn);
            string sql = string.Format(@"
                                select distinct pallet_no palletno, carton_no CTNSN, part_no ICTPN, location_no locationNo,location_id id
                                  from ppsuser.t_sn_status a
                                 where wc = 'W0'
                                   and carton_no ='{0}'
                                union
                                select distinct pallet_no, carton_no, part_no, location_no ,location_id
                                  from ppsuser.t_sn_status a
                                 where wc = 'W0'
                                   and pallet_no ='{0}'
                                ", insn);
            var list = ppsDb.Query<CheckCNTVewModel>(sql);
            return Ok(Result.Create(true, "", list));
        }
        [HttpGet]
        [Route("inventoryCTN2")]
        public IHttpActionResult inventoryCTN2(String insn, string insn2)
        {
            PPSDb ppsDb = new PPSDb();
            insn = DelPrefixCartonSN(insn);
            insn = ppsDb.ChangeCSNtoCarton(insn);
            insn2 = DelPrefixCartonSN(insn2);
            insn2 = ppsDb.ChangeCSNtoCarton(insn2);
            string sql = string.Format(@"
                                 select distinct a.carton_no , a.pallet_no
                                 from ppsuser.t_sn_status a
                                where a.pallet_no ='{0}'
                                  and a.carton_no ='{1}'
                                  and a.wc='W0'
                                ", insn2, insn);
            var list = ppsDb.Query<CheckCNTVewModel>(sql);
            if (list.Count() == 0)
            {
                return Ok(Result.Create(false, "No Data!"));
            }
            return Ok(Result.Create(true, "", list));
        }
    }
}