using iMWS.Api.Db;
using iMWS.Api.Filters;
using iMWS.Api.Models.PPSPick;
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

namespace iMWS.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/ppspick")]
    // [JwtAuth]
    public class PPSPickController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// 取得PickTaskSearch任务列表
        [HttpGet]
        [Route("GetPickTaskSummaryList")]
        public IHttpActionResult GetPickTaskSummaryList(DateTime? shipDate, string shipmentid)
        {
            string errmsg = "";
            try
            {
                log.Info("get");
                #region old
                //string sql = @"select aa.shipment_id   ShipmentID,
                //           aa.pallet_no PalletNo,
                //           aa.region,
                //           aa.shipment_type ShipmentType,
                //           aa.type          ShipType
                //      from (select CASE
                //                     WHEN shipment_type = 'DS' THEN
                //                      T_REGION_TYPE(A.SHIPMENT_ID, A.REGION)
                //                     ELSE
                //                      A.REGION
                //                   END AS REGION,
                //                   a.shipment_type,
                //                   a.type,
                //                   a.shipment_id,
                //                   a.shipping_time,
                //                   b.pallet_no
                //              from ppsuser.t_shipment_info a
                //              join ppsuser.t_shipment_pallet b
                //                on a.shipment_id = b.shipment_id
                //               and a.status in ('WP', 'IP')
                //               and b.carton_qty >
                //                   decode(b.pick_carton, null, 0, '', 0, b.pick_carton)) aa
                //     where trunc(aa.shipping_time) = trunc(:shipDate)
                //     group by aa.shipment_id,
                //              aa.pallet_no,
                //              aa.region,
                //              aa.shipment_type,
                //              aa.type
                //    ";
                #endregion
                string sql = @"select aa.shipment_id   ShipmentID,
                           aa.pallet_no PalletNo,
                           aa.region,
                           aa.shipment_type ShipmentType,
                           aa.type ShipType,
                           aa.pick_status pickStatus,
                           aa.carrier_name carrierName,
                           aa.priority
                      from(select CASE
                                     WHEN shipment_type = 'DS' THEN
                                      T_REGION_TYPE(A.SHIPMENT_ID, A.REGION)
                                     ELSE
                                      A.REGION
                                   END AS REGION,
                                   a.shipment_type,
                                   a.type,
                                   a.carrier_name,
                                   a.shipment_id,
                                   b.pick_status,
                                   a.shipping_time,
                                   b.pallet_no,
                                   a.priority
                              from ppsuser.t_shipment_info a
                              join ppsuser.t_shipment_pallet b
                                on a.shipment_id = b.shipment_id
                               and a.status in ('WP', 'IP')
                               and b.carton_qty >
                                   decode(b.pick_carton, null, 0, '', 0, b.pick_carton)) aa
                     where trunc(aa.shipping_time) = trunc(:shipDate)";
                if (!string.IsNullOrEmpty(shipmentid))
                    sql += " and shipment_id=:shipmentid ";
                sql += " order by aa.type desc, priority,aa.shipment_id";
                if (!shipDate.HasValue)
                    shipDate = DateTime.Now.Date;
                PPSDb db = new PPSDb();
                if (!string.IsNullOrEmpty(shipmentid))
                {
                    var list = db.Query<PickTaskSummaryViewModel>(sql, new { shipDate, shipmentid });
                    return Ok(Result.Create(true, "", list));
                }
                else
                {
                    var list = db.Query<PickTaskSummaryViewModel>(sql, new { shipDate });
                    return Ok(Result.Create(true, "", list));
                }
            }
            catch (Exception ex)
            {
                errmsg = ex.Message;
            }
            return Ok(Result.Create(false, "" + errmsg));

        }


        /// 取得栈板
        [HttpGet]
        [Route("GetPickTask")]
        public IHttpActionResult GetPickTask2(string palletno, string empno)
        {
            var result = new PickTaskViewModel()
            {
                PalletId = palletno
            };
            return Ok(Result.Create(true, "", result));
        }

        /// 取得栈板资讯
        [HttpGet]
        [Route("GetPickPalletInfo")]
        public IHttpActionResult GetPickPalletInfo(string palletId)
        {
            string errmsg = "";
            try
            {

            }
            catch (Exception ex)
            {
                errmsg = ex.Message;

            }
            string sql = @"
                select a.shipment_id ShipmentId,
                       a.pallet_no,
                       c.carrier_name Carrier,
                       c.priority Priority,
                       a.carton_qty qty,
                       a.pick_carton DealQty,
                       case
                         when c.region = 'EMEIA' and
                              (c.carrier_code like '%DHL%' or
                              c.carrier_name like '%DHL%') and
                              c.service_level = 'WPX' then
                          palletlengthcm || '*' || palletwidthcm || 'WPX'
                         else
                          palletlengthcm || '*' || palletwidthcm
                       end as packcodedesc
                  from ppsuser.t_shipment_pallet a
                 inner join ppsuser.t_shipment_pallet_part b
                    on a.pallet_no = b.pallet_no
                 inner join ppsuser.t_shipment_info c
                    on a.shipment_id = c.shipment_id
                  join pptest.oms_carton_info oci1
                    on a.pack_code = oci1.packcode
                  join pptest.oms_pallet_info opi1
                    on oci1.palletcode = opi1.palletcode
                 where a.pallet_no = :palletId
            ";

            PPSDb db = new PPSDb();
            var list = db.Query<PickPalletInfoViewModel>(sql, new { palletId });
            if (list.Count() < 1)
                return Ok(Result.Create(false, "No Pallet Id"));

            var result = new PickPalletInfoViewModel()
            {
                ShipmentId = "ACD123456789",
                Carrier = "貨代",
                Priority = "優先及",
                Qty = 0,
                DealQty = 0,
                Packcodedesc = "包规"
            };
            result = list.First();
            return Ok(Result.Create(true, "", result));
        }

        /// 取得栈板作业项目

        [HttpGet]
        [Route("GetPickTaskItem")]
        public IHttpActionResult GetPickTaskItem(string palletId, string area)
        {
            PPSDb ppsDb = new PPSDb();

            string sql = @"SELECT C.ICTPN ICTPN, C.QTY QTY, C.CARTON_QTY CTNQTY, C.PICK_carton PICKCTN, C.CARTON_QTY - C.PICK_CARTON RESTCTN
                      FROM PPSUSER.T_SHIPMENT_PALLET_PART C
                     WHERE C.PALLET_NO = :palletId";
            var list = ppsDb.Query<PickTaskItemViewModel>(sql, new { palletId, area });
            return Ok(Result.Create(true, "", list));
        }




        /// 取得栈板库位资料--按栈板显示
        [HttpGet]
        [Route("GetTaskStockLoc1")]
        public IHttpActionResult GetTaskStockLoc1(string palletId, string area, string ICTPN)
        {
            PPSDb ppsDb = new PPSDb();
            string sql = "";
            sql = @"                                                               
                   select distinct(b.ictpn) from ppsuser.t_shipment_pallet a
                    join ppsuser.t_order_info b
                    on  a.shipment_id=b.shipment_id
                    where rownum=1
                    and a.pallet_no=:palletId";
            var list = ppsDb.Query<TaskStockLocViewModel>(sql, new { palletId = palletId }).ToArray();
            string ictpnn = "";
            if (ICTPN == null)
            {
                ictpnn = list[0].ICTPN;
            }
            else
            {
                ictpnn = ICTPN;
            }

            if (ictpnn.Substring(10, 1).Equals("K"))
            {
                sql = @"                                                               
                            select ICTPN,Loc，case when total=PickTotal   then  ''
                              else CTNQty end  as  CTNQty ，total,
                            case when total=PickTotal   then  Loccc
                              else LineNo end  as  LineNo  
                                from (
                            select xx.ICTPN,xx.Loc，xx.LineNo，xx.CTNQty，xx.total，xx.Loccc,yy.PickTotal  from
                            (
                            select '' ICTPN,
                                   to_char(aa.Location_no) Loc,
                                   to_char(aa.trolley_line_no) LineNo,
                                   listagg(aa.pointno, '*') within group(order by aa.pointno) as CTNQty,
                                   (select count(*)
                                      from ppsuser.t_trolley_sn_status
                                     where trolley_no =
                                           substr(aa.trolley_line_no,
                                                  0,
                                                  instr(aa.trolley_line_no, '-', 1, 3) - 1)) as total， substr(aa.trolley_line_no, 0, instr(aa.trolley_line_no, '-', 1, 3) - 1) Loccc
                              from (select distinct decode(g.pack_pallet_no,
                                                           null,
                                                           c.pallet_no,
                                                           g.pack_pallet_no) pallet_no,
                                                    '' ictpn,
                                                    '' mpn,
                                                    d.Location_no,
                                                    d.sides_no,
                                                    d.level_no,
                                                    d.seq_no,
                                                    d.pointno,
                                                    f.trolley_line_no
                                      from ppsuser.t_pallet_order c
                                     inner join ppsuser.vw_person_log d
                                        on c.delivery_no = d.delivery_no
                                       and c.line_item = d.line_item
                                       and c.ictpn = d.part_no
                                      join ppsuser.t_trolley_line_info f
                                        on d.trolley_no = f.trolley_no
                                       and d.sides_no = f.sides_no
                                       and d.level_no = f.level_no
                                       and d.seq_no = f.seq_no
                                      left join ppsuser.t_sn_ppart g
                                        on d.carton_no = g.carton_no
                                     where c.pallet_no = :palletId
                                       and f.trolley_no not in ('ICT-00-00-000')
                                     order by d.Location_no, f.trolley_line_no, d.pointno) aa
                             where aa.pallet_no = :palletId
                             group by aa.pallet_no, aa.Location_no, aa.trolley_line_no
                            )xx  join 
                            (
                            select  Loccc ,sum(CTNQty2) PickTotal  from 
                            (
                            select count(aa.pointno) as CTNQty2,
                                   substr(aa.trolley_line_no,
                                          0,
                                          instr(aa.trolley_line_no, '-', 1, 3) - 1) Loccc
                              from (select distinct decode(g.pack_pallet_no,
                                                           null,
                                                           c.pallet_no,
                                                           g.pack_pallet_no) pallet_no,
                                                    '' ictpn,
                                                    '' mpn,
                                                    d.Location_no,
                                                    d.sides_no,
                                                    d.level_no,
                                                    d.seq_no,
                                                    d.pointno,
                                                    f.trolley_line_no
                                      from ppsuser.t_pallet_order c
                                     inner join ppsuser.vw_person_log d
                                        on c.delivery_no = d.delivery_no
                                       and c.line_item = d.line_item
                                       and c.ictpn = d.part_no
                                      join ppsuser.t_trolley_line_info f
                                        on d.trolley_no = f.trolley_no
                                       and d.sides_no = f.sides_no
                                       and d.level_no = f.level_no
                                       and d.seq_no = f.seq_no
                                      left join ppsuser.t_sn_ppart g
                                        on d.carton_no = g.carton_no
                                     where c.pallet_no =:palletId
                                       and f.trolley_no not in ('ICT-00-00-000')
                                     order by d.Location_no, f.trolley_line_no, d.pointno) aa
                             where aa.pallet_no = :palletId
                             group by aa.pallet_no, aa.Location_no, aa.trolley_line_no
                            )
                            group by  Loccc
                            ) yy  on xx.Loccc=yy.Loccc
                            )
                            ";
            }
            else
            {
                sql = @"                                                               
                    Select t.part_no ICTPN, t.location_no Loc,'' LineNo, qty CTNQty,TO_CHAR(udt,'YYYY/MM/DD') udt 
                                      from(Select distinct b.part_no,
                                                            b.location_no,
                                                            (b.CARTONQTY - b.QHCARTONQTY) qty,
                                                            min(a.udt) as udt
                                              from ppsuser.t_location b, ppsuser.t_sn_status a
                                             where a.wc = 'W0'
                                               and A.LOCATION_ID(+) = B.LOCATION_ID
                                               AND b.qty > 0
                                               and b.cartonqty > 0
                                               and(b.part_no =: ictpnn or
                                                   b.part_no in
                                                   (select distinct a.part_no
                                                       from ppsuser.t_sn_status a
                                                      where a.customer_sn =: ictpnn
                                                         or a.carton_no =: ictpnn
                                                         or a.pallet_no =:ictpnn)) and
                                                         a.part_no = b.part_no
                                             group by b.part_no,
                                                      b.location_no,b.CARTONQTY ,b.QHCARTONQTY
                                                     ) t
                                     where t.qty > 0
                                     order by t.Udt";
            }
            var list1 = ppsDb.Query<TaskStockLocViewModel>(sql, new { ictpnn = ictpnn });
            return Ok(Result.Create(true, "", list1));
        }

        [HttpGet]
        [Route("GetTaskStockLoc")]
        public IHttpActionResult GetTaskStockLoc(string palletId)
        {
            PPSDb ppsDb = new PPSDb();
            string sql = "SELECT DISTINCT ICTPN from ppsuser.T_PALLET_ORDER where PALLET_NO=:palletId";
            var list = ppsDb.Query<string>(sql, new { palletId = palletId }).ToArray();
            var list1 = new List<TaskStockLocViewModel>();

            foreach (var ictpnn in list)
            {
                //string ictpnn = item.ICTPN;
                if (ictpnn.Substring(10, 1).Equals("K"))
                {
                    sql = @"                                                               
                            select ICTPN,Loc，case when total=PickTotal   then  ''
                              else CTNQty end  as  CTNQty ，total,
                            case when total=PickTotal   then  Loccc
                              else LineNo end  as  LineNo  
                                from (
                            select xx.ICTPN,xx.Loc，xx.LineNo，xx.CTNQty，xx.total，xx.Loccc,yy.PickTotal  from
                            (
                            select '' ICTPN,
                                   to_char(aa.Location_no) Loc,
                                   to_char(aa.trolley_line_no) LineNo,
                                   listagg(aa.pointno, '*') within group(order by aa.pointno) as CTNQty,
                                   (select count(*)
                                      from ppsuser.t_trolley_sn_status
                                     where trolley_no =
                                           substr(aa.trolley_line_no,
                                                  0,
                                                  instr(aa.trolley_line_no, '-', 1, 3) - 1)) as total， substr(aa.trolley_line_no, 0, instr(aa.trolley_line_no, '-', 1, 3) - 1) Loccc
                              from (select distinct decode(g.pack_pallet_no,
                                                           null,
                                                           c.pallet_no,
                                                           g.pack_pallet_no) pallet_no,
                                                    '' ictpn,
                                                    '' mpn,
                                                    d.Location_no,
                                                    d.sides_no,
                                                    d.level_no,
                                                    d.seq_no,
                                                    d.pointno,
                                                    f.trolley_line_no
                                      from ppsuser.t_pallet_order c
                                     inner join ppsuser.vw_person_log d
                                        on c.delivery_no = d.delivery_no
                                       and c.line_item = d.line_item
                                       and c.ictpn = d.part_no
                                      join ppsuser.t_trolley_line_info f
                                        on d.trolley_no = f.trolley_no
                                       and d.sides_no = f.sides_no
                                       and d.level_no = f.level_no
                                       and d.seq_no = f.seq_no
                                      left join ppsuser.t_sn_ppart g
                                        on d.carton_no = g.carton_no
                                     where c.pallet_no = :palletId
                                       and f.trolley_no not in ('ICT-00-00-000')
                                     order by d.Location_no, f.trolley_line_no, d.pointno) aa
                             where aa.pallet_no = :palletId
                             group by aa.pallet_no, aa.Location_no, aa.trolley_line_no
                            )xx  join 
                            (
                            select  Loccc ,sum(CTNQty2) PickTotal  from 
                            (
                            select count(aa.pointno) as CTNQty2,
                                   substr(aa.trolley_line_no,
                                          0,
                                          instr(aa.trolley_line_no, '-', 1, 3) - 1) Loccc
                              from (select distinct decode(g.pack_pallet_no,
                                                           null,
                                                           c.pallet_no,
                                                           g.pack_pallet_no) pallet_no,
                                                    '' ictpn,
                                                    '' mpn,
                                                    d.Location_no,
                                                    d.sides_no,
                                                    d.level_no,
                                                    d.seq_no,
                                                    d.pointno,
                                                    f.trolley_line_no
                                      from ppsuser.t_pallet_order c
                                     inner join ppsuser.vw_person_log d
                                        on c.delivery_no = d.delivery_no
                                       and c.line_item = d.line_item
                                       and c.ictpn = d.part_no
                                      join ppsuser.t_trolley_line_info f
                                        on d.trolley_no = f.trolley_no
                                       and d.sides_no = f.sides_no
                                       and d.level_no = f.level_no
                                       and d.seq_no = f.seq_no
                                      left join ppsuser.t_sn_ppart g
                                        on d.carton_no = g.carton_no
                                     where c.pallet_no =:palletId
                                       and f.trolley_no not in ('ICT-00-00-000')
                                     order by d.Location_no, f.trolley_line_no, d.pointno) aa
                             where aa.pallet_no = :palletId
                             group by aa.pallet_no, aa.Location_no, aa.trolley_line_no
                            )
                            group by  Loccc
                            ) yy  on xx.Loccc=yy.Loccc
                            )
                            ";
                }
                else
                {
                    sql = @"                                                               
                    Select t.part_no ICTPN, t.location_no Loc,'' LineNo, qty CTNQty,TO_CHAR(udt,'YYYY/MM/DD') udt 
                                      from(Select distinct b.part_no,
                                                            b.location_no,
                                                            (b.CARTONQTY - b.QHCARTONQTY) qty,
                                                            min(a.udt) as udt
                                              from ppsuser.t_location b, ppsuser.t_sn_status a
                                             where a.wc = 'W0'
                                               and A.LOCATION_ID(+) = B.LOCATION_ID
                                               AND b.qty > 0
                                               and b.cartonqty > 0
                                               and(b.part_no =: ictpnn or
                                                   b.part_no in
                                                   (select distinct a.part_no
                                                       from ppsuser.t_sn_status a
                                                      where a.customer_sn =: ictpnn
                                                         or a.carton_no =: ictpnn
                                                         or a.pallet_no =:ictpnn)) and
                                                         a.part_no = b.part_no
                                             group by b.part_no,
                                                      b.location_no,b.CARTONQTY ,b.QHCARTONQTY
                                                     ) t
                                     where t.qty > 0
                                     order by t.Udt";
                }
                var lstTemp = ppsDb.Query<TaskStockLocViewModel>(sql, new { ictpnn = ictpnn }).ToList();
                list1.AddRange(lstTemp);

            }
            return Ok(Result.Create(true, "", JsonConvert.SerializeObject(list1)));
        }

        /// 开始/继续任务
        [HttpPost]
        [Route("BindPickMechine")]
        public IHttpActionResult BindPickMechine(PickTaskViewModel vm)
        {
            PPSDb ppsDb = new PPSDb();
            string sql = "select t.shipment_id shipment_id from ppsuser.t_shipment_pallet t where t.pallet_no  =:PalletId";
            var p = ppsDb.Query<string>(sql, vm).FirstOrDefault();
            var m = ppsDb.CheckHold(p, "SHIPMENT");
            string errmsg = m.RetMsg;
            if (!errmsg.Equals("OK"))
            {
                return Ok(Result.Create(false, "" + errmsg));
            }

            m = ppsDb.PPartPreAssinPalletNO(p);
            errmsg = m.RetMsg;
            if (!errmsg.Equals("OK"))
            {
                return Ok(Result.Create(false, "" + errmsg));
            }


            m = ppsDb.CheckPalletStatus(p, vm.PalletId, vm.uuid);
            errmsg = m.errmsg;
            if (errmsg.Equals("OK"))
            {
                return Ok(Result.Create(true, ""));
            }
            else if (errmsg.ToString().Contains("WA"))
            {
                return Ok(Result.Create(true, "" + errmsg));
            }
            return Ok(Result.Create(false, "" + errmsg));
        }

        /// 结束任务
        [HttpPost]
        [Route("PickEnd")]
        public IHttpActionResult PickEnd(string strPalletno)
        {
            PPSDb ppsDb = new PPSDb();

            var m = ppsDb.PickCartonEnd(strPalletno, "");
            string errmsg = m.RetMsg;
            if (!errmsg.Equals("OK"))
            {
                return Ok(Result.Create(false, "" + errmsg));
            }
            return Ok(Result.Create(true, ""));
        }







        /// PPS Pick(箱号序号刷入)
        [HttpPost]
        [Route("PickCTN")]
        public IHttpActionResult PickCTN(PickCTNViewModel vm)
        {
            log.Info("data : " + JsonConvert.SerializeObject(vm));

            bool IsEntirety = false;
            if (vm.CTNNO.Length == 9 && vm.CTNNO.StartsWith("JS"))
            {
                IsEntirety = true;
            }
            PPSDb ppsDb = new PPSDb();
            string strCarton = DelPrefixCartonSN(vm.CTNNO);
            var m1 = ppsDb.CheckSN(vm.Palletid, strCarton, IsEntirety);
            string errmsg1 = m1.ErrMsg;
            strCarton = m1.Outsn;
            if (!errmsg1.Equals("OK"))
            {
                return Ok(Result.Create(false, "" + errmsg1));

            }
            var m2 = ppsDb.CheckHold(strCarton, "SERIALNUMBER");
            string errmsg2 = m2.RetMsg;
            if (!errmsg2.Equals("OK"))
            {
                return Ok(Result.Create(false, "" + errmsg2));
            }

            var g_MarinaUrl = ppsDb.PPSGetbasicparameterBySP("MARINA_URL", null);
            var g_MarinaSite = ppsDb.PPSGetbasicparameterBySP("MARINA_SITE", null);

            var vDBType = ppsDb.PPSGetbasicparameterBySP("DB_TYPE", null);

            var vMarinaPackoutFlag = ppsDb.GetMarinaPackoutFlag(strCarton, "PICK");

            string msgCheckMarina = string.Empty;
            string msgPackout = string.Empty;
            if (!vDBType.outparavalue.Equals("TEST"))
            {
                if (vMarinaPackoutFlag.outmarinaflag.Equals("Y"))
                {
                    bool bl = ppsDb.CheckMarinaServer(strCarton, g_MarinaSite.outparavalue, g_MarinaUrl.outparavalue, out msgCheckMarina);
                    if (!bl)
                    {
                        return Ok(Result.Create(false, "MarinaServer " + msgCheckMarina));
                    }
                }

                if (vMarinaPackoutFlag.outpackoutflag.Equals("Y"))
                {
                    bool bl = ppsDb.CheckPackoutLogic(strCarton, out msgPackout);
                    if (!bl)
                    {
                        return Ok(Result.Create(false, "PackoutLogic " + msgPackout));
                    }
                }
            }


            string sql = "select max(t.pallet_no) pallet_no from PPSUSER.T_PALLET_PICK t where t.pallet_no = :Palletid";
            var p = ppsDb.Query<string>(sql, vm).FirstOrDefault();

            var m3 = ppsDb.InsertPalletPick(IsEntirety, p, vm.Palletid, strCarton, vm.empNo);
            string errmsg3 = m3.ErrMsg;
            if (!errmsg3.Equals("OK") && !errmsg3.StartsWith("FINISH"))
            {
                return Ok(Result.Create(false, "" + errmsg3));
            }
            else
            {
                var m4 = ppsDb.PPSInsertWorkLogBy(strCarton, "PICK", vm.UUID);
                string errmsg4 = m4.errmsg;
                if (!errmsg2.Equals("OK"))
                {
                    return Ok(Result.Create(false, "" + errmsg2));
                }

            }
            return Ok(Result.Create(true, errmsg3 + "成功!"));
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



        /// 获取箱号信息  抓取该箱号的料号  实现跳行
        [HttpGet]
        [Route("GetSnPartNo")]
        public IHttpActionResult GetSnPartNo(string strSN)
        {
            PPSDb ppsDb = new PPSDb();

            string sql = @" select distinct part_no PartNo, carton_no CartonNo
                                  from ppsuser.t_sn_status a
                                 where a.carton_no in
                                       (select carton_no from ppsuser.t_sn_status where customer_sn = :strSN)
                                union
                                select distinct  part_no PartNo , carton_no CartonNo
                                  from ppsuser.t_sn_status b
                                 where b.carton_no = :strSN";
            var list = ppsDb.Query<PPSCartonPartNoModel>(sql, new { strSN });
            return Ok(Result.Create(true, "", list));

        }


        /// 判断是否为最后一箱，且获取箱号
        [HttpGet]
        [Route("GetCartonNo")]
        public IHttpActionResult GetCartonNo(string CartonNo)
        {
            PPSDb ppsDb = new PPSDb();

            string sql = @"  select aa.carton_no CartonNo
                              from (select carton_no, rownum lll
                                      from ppsuser.t_sn_status
                                     where substr(pick_pallet_no, 3) in
                                           (select pallet_no
                                              from ppsuser.t_shipment_pallet a
                                             where a.carton_qty = a.pick_carton
                                               and pallet_no in
                                                   (select distinct (substr(pick_pallet_no, 3))
                                                      from ppsuser.t_sn_status
                                                     where carton_no = :CartonNo))) aa
                             where aa.lll = '1'";
            var list = ppsDb.Query<PPSCartonNoModel>(sql, new { CartonNo });
            if (list.Count() == 1)
            {
                return Ok(Result.Create(true, "", list));
            }
            return Ok(Result.Create(true, "", list));
        }

        [HttpGet]
        [Route("GetPickLog")]
        public IHttpActionResult GetPickLog(string uuid)
        {
            PPSDb ppsDb = new PPSDb();

            string sql = @" 
                           select aa.shipmentid shipmentId,
                           aa.palletno palletNo,
                           aa.starttime startTime,
                           aa.endtime endTime,
                           bb.qty qty,
                           bb.DealQty dealQty,
                        d.EMP_NO empNo
                      from (select shipment_id ShipmentId,
                                   pallet_no PalletNo,
                                   min(cdt) StarTtime,
                                   max(cdt) EndTime
                              from (select a.shipment_id, a.pallet_no, b.carton_no, b.cdt
                                      from ppsuser.t_shipment_pallet a
                                      join (select distinct (substr(a.pick_pallet_no, 3)) pallet_no,
                                                           b.carton_no,
                                                           b.cdt
                                             from ppsuser.t_sn_status a
                                             join ppsuser.t_passstation_log b
                                               on a.carton_no = b.carton_no
                                            and b.mac = :uuid
                                              and b.in_station = 'PICK'
                                              and b.isavailable = '1') b
                                        on a.pallet_no = b.pallet_no)
                             group by shipment_id, pallet_no) aa
                      join (select a.shipment_id ShipmentId,
                                   a.pallet_no   palletno,
                                   a.carton_qty  qty,
                                   a.pick_carton DealQty
                              from ppsuser.t_shipment_pallet a
                             inner join ppsuser.t_shipment_pallet_part b
                                on a.pallet_no = b.pallet_no
                             inner join ppsuser.t_shipment_info c
                                on a.shipment_id = c.shipment_id
                              join pptest.oms_carton_info oci1
                                on a.pack_code = oci1.packcode
                              join pptest.oms_pallet_info opi1
                                on oci1.palletcode = opi1.palletcode
                             where a.pallet_no in
                                   (select distinct (substr(a.pick_pallet_no, 3)) pallet_no
                                      from ppsuser.t_sn_status a
                                      join ppsuser.t_passstation_log b
                                        on a.carton_no = b.carton_no
                                     and b.mac = :uuid
                                       and b.in_station = 'PICK'
                                       and b.isavailable = '1')) bb
                        on aa.shipmentid = bb.ShipmentId
                       and aa.palletno = bb.palletno
                       and aa.starttime>=(sysdate-1)
                        join ( select distinct pallet_no, emp_no from ppsuser.T_PALLET_PICK) d
                                on aa.palletno= d.pallet_no
                         group by aa.shipmentid,
                                       aa.palletno,
                                       aa.starttime,
                                       aa.endtime,
                                       bb.qty,
                                       bb.DealQty,
                                 d.EMP_NO";
            var list = ppsDb.Query<PPSPickLogModel>(sql, new { uuid });
            return Ok(Result.Create(true, "", list));

        }
    }
}





