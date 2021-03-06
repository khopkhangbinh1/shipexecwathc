using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class ConsumerPkListBilingualCanadaForm
    {
        public ConsumerPkListBilingualCanadaForm(string acDn, string lineItem, string shipmentId, bool print)
        {
            Initialize(acDn, lineItem, shipmentId, print, "");
        }

        public ConsumerPkListBilingualCanadaForm(string acDn, string lineItem, string shipmentId, bool print, string strPath)
        {
            Initialize(acDn, lineItem, shipmentId, print, strPath);
        }

        public ConsumerPkListBilingualCanadaForm(string acDn, string lineItem)
        {
            Initialize2(acDn, lineItem);
        }

        public void Initialize(string acDn, string lineItem, string shipmentId, bool print, string strPath)
        {
            DataSet ds = new DataSet();
            string headerSql = string.Format(@"  select distinct a.shiptoname as ST_NAME,
                                                         a.shiptoaddress as ST_ADDR1,
                                                         a.shiptoAddress2 as ST_ADDR2,
                                                         a.Shiptoaddress3 as ST_ADDR3,
                                                         a.shiptocity as ST_CITY,
                                                         a.shiptozip as ST_POSTAL,
                                                         a.shiptostate as ST_PROVINCE,      
                                                         a.shiptocountry as ST_COUNTRY,
                                                         'Apple Online Store' as SHIP_FROM, --这个是固定值
                                                         a.soldtoname as SO_NAME,
                                                         a.deliveryno as AC_DN,
                                                         a.weborderno as WO_NUM,
                                                         TO_CHAR(TO_DATE(A.ORDERDATE, 'yyyyMMdd'),
                                                                 'dd MON yyyy',
                                                                 'NLS_DATE_LANGUAGE=american') as AC_PO_DATE,
                                                         TO_CHAR(tsi.shipping_time,
                                                                 'dd MON yyyy',
                                                                 'NLS_DATE_LANGUAGE=american') as SHIP_DATE, --T_ORDER_INFO 这个DN 的Udt 
                                                         'Standard Shipping Method Only' as SHIP_VIA, --固定的
                                                         (SELECT SUM(TOI_SUM.QTY)
                                                            FROM PPSUSER.T_ORDER_INFO TOI_SUM
                                                           WHERE TOI_SUM.DELIVERY_NO = TOI.DELIVERY_NO 
                                                           and   TOI_SUM.SHIPMENT_ID = TOI.SHIPMENT_ID ) TOT_QTY,
                                                         (select round(sum(t1.totalWeight), 2)
                                                            from (select TOI.ICTPN,
                                                                         TOI.DELIVERY_NO,
                                                                         vmi.GROSSWEIGHTKG * SUM(TOI.CARTON_QTY) as totalWeight
                                                                    from PPSUSER.T_ORDER_INFO TOI,
                                                                         pptest.vw_mpn_info   vmi
                                                                   where TOI.ICTPN = vmi.ICTPARTNO
                                                                     and TOI.DELIVERY_NO = '{0}'
                                                                     AND TOI.SHIPMENT_ID = '{2}'
                                                                   group by TOI.ICTPN,
                                                                            TOI.DELIVERY_NO,
                                                                            vmi.GROSSWEIGHTKG) t1) TOT_GW,
                                                         SUBSTR(a.euheadtext1, 4) as PKLST_TEXT1,
                                                         SUBSTR(a.euheadtext2, 4) as PKLST_TEXT2,
                                                         SUBSTR(a.euheadtext3, 4) as PKLST_TEXT3,
                                                         SUBSTR(a.euheadtext4, 4) as PKLST_TEXT4,
                                                         SUBSTR(a.euheadtext5, 4) as PKLST_TEXT5,
                                                         SUBSTR(a.euheadtext6, 4) as PKLST_TEXT6,
                                                         SUBSTR(a.euheadtext7, 4) as PKLST_TEXT7,
                                                         SUBSTR(a.euheadtext8, 4) as PKLST_TEXT8,
                                                         tsi.carrier_name as CarrierName
                                           from ppsuser.t_940_unicode   a,
                                                ppsuser.t_order_info    toi,
                                                ppsuser.t_shipment_info tsi
                                          where ppsuser.t_newtrim_function(a.deliveryno) = toi.delivery_no
                                            and ppsuser.t_newtrim_function(a.custdelitem) = toi.line_item
                                            and toi.shipment_id = tsi.shipment_id
                                            and toi.delivery_no ='{0}'
                                            and toi.line_item ='{1}'
                                            and toi.shipment_id ='{2}' ", acDn, lineItem, shipmentId);
            string lineSql = string.Format(@" SELECT   T.DELIVERY_NO as AC_DN,
                                                       T.LINE_ITEM as AC_DN_LINE,
                                                       T.MPN as AC_PN,
                                                       MATE_DESC,
                                                       T.msg1 as GIFT_MSG1,
                                                       T.msg2 as GIFT_MSG2,
                                                       T.msg3 as GIFT_MSG3,
                                                       T.msg4 as GIFT_MSG4,
                                                       T.msg5 as GIFT_MSG5,
                                                       '' as GIFT_MSG6,
                                                       '' as GIFT_MSG7,
                                                       sum(qty) as QTY,
                                                       GIFT_MSG8
                                                  FROM (SELECT TOI.DELIVERY_NO,
                                                               TOI.LINE_ITEM,
                                                               TOI.MPN,
                                                               PPSUSER.T_SHOW_NPI(T9U.DELIVERYNO,T9U.CUSTDELITEM,'Consumer Packing List') as MATE_DESC,
                                                               t9u.msg1,
                                                               t9u.msg2,
                                                               t9u.msg3,
                                                               t9u.msg4,
                                                               t9u.msg5,
                                                               DECODE(t9u.MSG1 || t9u.MSG2 || t9u.MSG3 || t9u.MSG4 ||
                                                                      t9u.MSG5,
                                                                      '',
                                                                      '',
                                                                      'Gift Message/Message Cadeau') AS GIFT_MSG8,
                                                               toi.qty
                                                          FROM PPSUSER.T_ORDER_INFO  TOI,
                                                               PPTEST.VW_MPN_INFO    VMI,
                                                               PPSUSER.T_940_UNICODE T9U
                                                         WHERE TOI.ICTPN = VMI.ICTPARTNO
                                                           AND TOI.LINE_ITEM = T9U.CUSTDELITEM
                                                           AND TOI.DELIVERY_NO = T9U.DELIVERYNO
                                                           AND TOI.SHIPMENT_ID = '{1}'
                                                           AND TOI.DELIVERY_NO = '{0}') T
                                                 GROUP BY T.DELIVERY_NO,
                                                          T.LINE_ITEM,
                                                          T.MPN,
                                                          MATE_DESC,
                                                          GIFT_MSG8,
                                                          T.msg1,
                                                          T.msg2,
                                                          T.msg3,
                                                          T.msg4,
                                                          T.msg5
                                                 order by LINE_ITEM
                                                ", acDn, shipmentId);
            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));

            if (print)
            {
                Util.CreateDataTable(Constant.ConsumerPkListBilingualCanada_URL, ds);
            }
            else
            {
                Util.printPDFCrystalReportV2(Constant.ConsumerPkListBilingualCanada_URL_ByGW, ds, strPath);
            }

        }

        public void Initialize2(string acDn, string lineItem)
        {
            DataSet ds = new DataSet();
            string headerSql = string.Format(@"  select distinct a.shiptoname as ST_NAME,
                                                         a.shiptoaddress as ST_ADDR1,
                                                         a.shiptoAddress2 as ST_ADDR2,
                                                         a.Shiptoaddress3 as ST_ADDR3,
                                                         a.shiptocity as ST_CITY,
                                                         a.shiptozip as ST_POSTAL,
                                                         a.shiptostate as ST_PROVINCE,      
                                                         a.shiptocountry as ST_COUNTRY,
                                                         'Apple Online Store' as SHIP_FROM, --这个是固定值
                                                         a.soldtoname as SO_NAME,
                                                         a.deliveryno as AC_DN,
                                                         a.weborderno as WO_NUM,
                                                         TO_CHAR(TO_DATE(A.ORDERDATE, 'yyyyMMdd'),
                                                                 'dd MON yyyy',
                                                                 'NLS_DATE_LANGUAGE=american') as AC_PO_DATE,
                                                         (SELECT TO_CHAR(max(omm.shipdate),'dd MON yyyy','NLS_DATE_LANGUAGE=american') 
                                                 FROM pptest.oms_940_m omm WHERE omm.ac_dn='{0}') as SHIP_DATE, 
                                                         'Standard Shipping Method Only' as SHIP_VIA, --固定的
                                                         (SELECT SUM(TOI_sum.PCS_QTY)
                                                               FROM PPSUSER.VW_PERSON_DN_INFO TOI_sum
                                                              WHERE toi_sum.DN_NO = '{0}'
                                                              ) TOT_QTY,
                                                       (   select round(sum(t1.totalWeight), 2)
                                                     from (select bb.ICT_PARTNO,
                                                                  bb.DN_NO,
                                                                  vmi.Grossweightkgp  * SUM(bb.PCS_QTY) as totalWeight
                                                             from PPSUSER.VW_PERSON_DN_INFO  bb, pptest.OMS_PARTMAPPING vmi
                                                            where bb.ICT_PARTNO = vmi.PART
                                                              and bb.DN_NO = '{0}'
                                                            group by bb.ICT_PARTNO, bb.DN_NO, vmi.Grossweightkgp) t1) TOT_GW,
                                                         SUBSTR(a.euheadtext1, 4) as PKLST_TEXT1,
                                                         SUBSTR(a.euheadtext2, 4) as PKLST_TEXT2,
                                                         SUBSTR(a.euheadtext3, 4) as PKLST_TEXT3,
                                                         SUBSTR(a.euheadtext4, 4) as PKLST_TEXT4,
                                                         SUBSTR(a.euheadtext5, 4) as PKLST_TEXT5,
                                                         SUBSTR(a.euheadtext6, 4) as PKLST_TEXT6,
                                                         SUBSTR(a.euheadtext7, 4) as PKLST_TEXT7,
                                                         SUBSTR(a.euheadtext8, 4) as PKLST_TEXT8,
                                                         (SELECT y.CarrierName FROM pptest.OMS_940_M x INNER JOIN pptest.oms_carrier y ON x.t_carrier=y.scac AND upper(x.shipmode)=upper(y.shipmode) WHERE x.ac_dn='{0}' AND rownum=1) as  CarrierName
                                           from ppsuser.t_940_unicode   a
                                          WHERE a.deliveryno = '{0}'
                                                and   a.custdelitem = '{1}' ", acDn, lineItem);
            string lineSql = string.Format(@" SELECT   T.DELIVERYNO as AC_DN,
                                                       T.CUSTDELITEM as AC_DN_LINE,
                                                       T.MPN as AC_PN,
                                                       MATE_DESC,
                                                       T.msg1 as GIFT_MSG1,
                                                       T.msg2 as GIFT_MSG2,
                                                       T.msg3 as GIFT_MSG3,
                                                       T.msg4 as GIFT_MSG4,
                                                       T.msg5 as GIFT_MSG5,
                                                       '' as GIFT_MSG6,
                                                       '' as GIFT_MSG7,
                                                       sum(PCS_QTY) as QTY,
                                                       GIFT_MSG8
                                                  FROM (SELECT T9U.DELIVERYNO,
                                                               T9U.CUSTDELITEM,
                                                               T9U.MPN,
                                                               PPSUSER.T_SHOW_NPI_MES(T9U.DELIVERYNO,T9U.CUSTDELITEM,'Consumer Packing List',T9U.MPN) as MATE_DESC,
                                                               t9u.msg1,
                                                               t9u.msg2,
                                                               t9u.msg3,
                                                               t9u.msg4,
                                                               t9u.msg5,
                                                               DECODE(t9u.MSG1 || t9u.MSG2 || t9u.MSG3 || t9u.MSG4 ||
                                                                      t9u.MSG5,
                                                                      '',
                                                                      '',
                                                                      'Gift Message/Message Cadeau') AS GIFT_MSG8,
                                                               bb.PCS_QTY
                                                          FROM PPSUSER.VW_PERSON_DN_INFO  bb,
                                                               PPTEST.VW_MPN_INFO    VMI,
                                                               PPSUSER.T_940_UNICODE T9U
                                                         WHERE bb.ICT_PARTNO = VMI.ICTPARTNO
                                                               AND bb.DN_LINE = T9U.CUSTDELITEM
                                                               AND bb.DN_NO = T9U.DELIVERYNO
                                                               AND bb.DN_NO = '{0}') T
                                                 GROUP BY T.DELIVERYNO,
                                                          T.CUSTDELITEM,
                                                          T.MPN,
                                                          MATE_DESC,
                                                          GIFT_MSG8,
                                                          T.msg1,
                                                          T.msg2,
                                                          T.msg3,
                                                          T.msg4,
                                                          T.msg5
                                                 order by CUSTDELITEM
                                                ", acDn);
            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));

            Util.CreateDataTable(Constant.ConsumerPkListBilingualCanada_URL, ds);
        }

        public ConsumerPkListBilingualCanadaForm(string acDn, string lineItem, string shipmentId, string strCrystalFullPath, bool isCustom, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            Initialize(acDn, lineItem, shipmentId, strCrystalFullPath, isCustom, isPDF, strPDFPath, nCopies, isOnlyGetDS, out dtCrystal, out serverFullLabelName);
        }
        public void Initialize(string acDn, string lineItem, string shipmentId, string strCrystalFullPath, bool isCustom, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            dtCrystal = null;
            serverFullLabelName = string.Empty;
            DataSet ds = new DataSet();
            #region headerSql
            string headerSql = string.Format(@"  select distinct a.shiptoname as ST_NAME,
                                                         a.shiptoaddress as ST_ADDR1,
                                                         a.shiptoAddress2 as ST_ADDR2,
                                                         a.Shiptoaddress3 as ST_ADDR3,
                                                         a.shiptocity as ST_CITY,
                                                         a.shiptozip as ST_POSTAL,
                                                         a.shiptostate as ST_PROVINCE,      
                                                         a.shiptocountry as ST_COUNTRY,
                                                         'Apple Online Store' as SHIP_FROM, --这个是固定值
                                                         a.soldtoname as SO_NAME,
                                                         a.deliveryno as AC_DN,
                                                         a.weborderno as WO_NUM,
                                                         TO_CHAR(TO_DATE(A.ORDERDATE, 'yyyyMMdd'),
                                                                 'dd MON yyyy',
                                                                 'NLS_DATE_LANGUAGE=american') as AC_PO_DATE,
                                                         TO_CHAR(tsi.shipping_time,
                                                                 'dd MON yyyy',
                                                                 'NLS_DATE_LANGUAGE=american') as SHIP_DATE, --T_ORDER_INFO 这个DN 的Udt 
                                                         'Standard Shipping Method Only' as SHIP_VIA, --固定的
                                                         (SELECT SUM(TOI_SUM.QTY)
                                                            FROM PPSUSER.T_ORDER_INFO TOI_SUM
                                                           WHERE TOI_SUM.DELIVERY_NO = TOI.DELIVERY_NO 
                                                           and   TOI_SUM.SHIPMENT_ID = TOI.SHIPMENT_ID ) TOT_QTY,
                                                         (select round(sum(t1.totalWeight), 2)
                                                            from (select TOI.ICTPN,
                                                                         TOI.DELIVERY_NO,
                                                                         vmi.GROSSWEIGHTKG * SUM(TOI.CARTON_QTY) as totalWeight
                                                                    from PPSUSER.T_ORDER_INFO TOI,
                                                                         pptest.vw_mpn_info   vmi
                                                                   where TOI.ICTPN = vmi.ICTPARTNO
                                                                     and TOI.DELIVERY_NO = '{0}'
                                                                     AND TOI.SHIPMENT_ID = '{2}'
                                                                   group by TOI.ICTPN,
                                                                            TOI.DELIVERY_NO,
                                                                            vmi.GROSSWEIGHTKG) t1) TOT_GW,
                                                         SUBSTR(a.euheadtext1, 4) as PKLST_TEXT1,
                                                         SUBSTR(a.euheadtext2, 4) as PKLST_TEXT2,
                                                         SUBSTR(a.euheadtext3, 4) as PKLST_TEXT3,
                                                         SUBSTR(a.euheadtext4, 4) as PKLST_TEXT4,
                                                         SUBSTR(a.euheadtext5, 4) as PKLST_TEXT5,
                                                         SUBSTR(a.euheadtext6, 4) as PKLST_TEXT6,
                                                         SUBSTR(a.euheadtext7, 4) as PKLST_TEXT7,
                                                         SUBSTR(a.euheadtext8, 4) as PKLST_TEXT8,
                                                         tsi.carrier_name as CarrierName
                                           from ppsuser.t_940_unicode   a,
                                                ppsuser.t_order_info    toi,
                                                ppsuser.t_shipment_info tsi
                                          where ppsuser.t_newtrim_function(a.deliveryno) = toi.delivery_no
                                            and ppsuser.t_newtrim_function(a.custdelitem) = toi.line_item
                                            and toi.shipment_id = tsi.shipment_id
                                            and toi.delivery_no ='{0}'
                                            and toi.line_item ='{1}'
                                            and toi.shipment_id ='{2}' ", acDn, lineItem, shipmentId);
            #endregion
            #region lineSql
            string lineSql = string.Format(@" SELECT   T.DELIVERY_NO as AC_DN,
                                                       T.LINE_ITEM as AC_DN_LINE,
                                                       T.MPN as AC_PN,
                                                       MATE_DESC,
                                                       T.msg1 as GIFT_MSG1,
                                                       T.msg2 as GIFT_MSG2,
                                                       T.msg3 as GIFT_MSG3,
                                                       T.msg4 as GIFT_MSG4,
                                                       T.msg5 as GIFT_MSG5,
                                                       '' as GIFT_MSG6,
                                                       '' as GIFT_MSG7,
                                                       sum(qty) as QTY,
                                                       GIFT_MSG8
                                                  FROM (SELECT TOI.DELIVERY_NO,
                                                               TOI.LINE_ITEM,
                                                               TOI.MPN,
                                                               PPSUSER.T_SHOW_NPI(T9U.DELIVERYNO,T9U.CUSTDELITEM,'Consumer Packing List') as MATE_DESC,
                                                               t9u.msg1,
                                                               t9u.msg2,
                                                               t9u.msg3,
                                                               t9u.msg4,
                                                               t9u.msg5,
                                                               DECODE(t9u.MSG1 || t9u.MSG2 || t9u.MSG3 || t9u.MSG4 ||
                                                                      t9u.MSG5,
                                                                      '',
                                                                      '',
                                                                      'Gift Message/Message Cadeau') AS GIFT_MSG8,
                                                               toi.qty
                                                          FROM PPSUSER.T_ORDER_INFO  TOI,
                                                               PPTEST.VW_MPN_INFO    VMI,
                                                               PPSUSER.T_940_UNICODE T9U
                                                         WHERE TOI.ICTPN = VMI.ICTPARTNO
                                                           AND TOI.LINE_ITEM = T9U.CUSTDELITEM
                                                           AND TOI.DELIVERY_NO = T9U.DELIVERYNO
                                                           AND TOI.SHIPMENT_ID = '{1}'
                                                           AND TOI.DELIVERY_NO = '{0}') T
                                                 GROUP BY T.DELIVERY_NO,
                                                          T.LINE_ITEM,
                                                          T.MPN,
                                                          MATE_DESC,
                                                          GIFT_MSG8,
                                                          T.msg1,
                                                          T.msg2,
                                                          T.msg3,
                                                          T.msg4,
                                                          T.msg5
                                                 order by LINE_ITEM
                                                ", acDn, shipmentId);
            #endregion
            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));


            if (string.IsNullOrEmpty(strCrystalFullPath))
            {
                if (!isCustom) //不是关务用默认值
                {
                    strCrystalFullPath = Constant.ConsumerPkListBilingualCanada_URL;
                }
                else//isCustom如果是关务的是用宋体的模板 
                {
                    strCrystalFullPath = Constant.ConsumerPkListBilingualCanada_URL_ByGW;
                }
            }

            dtCrystal = ds;

            if (!isOnlyGetDS)
            {

                if (isPDF)
                {

                    if (string.IsNullOrEmpty(strPDFPath))
                    {
                        strPDFPath = AppDomain.CurrentDomain.BaseDirectory + @"\PDF\CR_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".PDF";
                    }
                    Util.printPDFCrystalReportV3(strCrystalFullPath, ds, strPDFPath, out serverFullLabelName);
                }
                else
                {
                    Util.CreateDataTableADDcount(strCrystalFullPath, ds, nCopies, out serverFullLabelName);
                }
            }
            else
            {
                string reportPath = Util.checkCRReportVersion(strCrystalFullPath, out serverFullLabelName);
            }
        }
    }
}
