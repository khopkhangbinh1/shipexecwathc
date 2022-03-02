using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace PackListAC
{
    class PackListDal
    {
        public DataSet GetShippingPrintInfoBySQL(string strPartNo)
        {
            //Morris 20210222
            string sql = string.Empty;
            sql = string.Format(@"  select a.pick_pallet_no palletno,
                                           b.carrier_name carriername,
                                           b.shipment_id shipmentid,
                                           (select min(shiptocountry)
                                              from nonedipps.t_fd_order_detail tfod
                                             where tfod.freightorder = b.shipment_id) region,
                                           a.pallet_number palletnum,
                                           d.location_no palletloc
                                    
                                      from nonedipps.t_pallet_pick a
                                      left join nonedipps.t_shipment_pallet b
                                        on a.pallet_no = b.pallet_no
                                      left join nonedipps.t_shipment_info c
                                        on b.shipment_id = c.shipment_id
                                      left join ppsuser.t_dock_location_info d
                                        on a.pallet_no = d.pallet_no
                                     where a.pick_pallet_no = '{0}'", strPartNo);

            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }
        public void updatePrintStatus(string deliveryNo, string lineItem, string strPickPalletNo)
        {
            string sql = string.Empty;
            sql = string.Format(@" update nonedipps.t_sn_status a
                                    set a.isprint = 'Y'
                                   where a.delivery_no = '{0}'
                                   and a.line_item = '{1}'
                                     ", deliveryNo, lineItem, strPickPalletNo);
            ClientUtils.ExecuteSQL(sql);
        }
        public DataSet GetPickPalletNoBySQL(string strPartNo)
        {
            //Morris 20210222
            string sql = string.Empty;
            sql = string.Format(@"  select distinct a.pack_pallet_no as packpalletno,
                                    a.pick_pallet_no as pickpalletno
                                     from nonedipps.t_sn_status a
                                    where a.carton_no = '{0}'
                                       or a.pick_pallet_no = '{1}'
                                   ", strPartNo, strPartNo);

            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }
        public DataSet GETDATALABLEEMEIASQL(string cartonNo)
        {
            //Morris 20210222
            string sql = string.Empty;
            sql = string.Format(@"  SELECT * FROM NONEDIPPS.T_SHIPMENT_INFO WHERE REGION ='EMEIA' AND SHIPMENT_ID IN(select SHIPMENT_ID from NONEDIPPS.T_SN_STATUS WHERE CARTON_NO='{0}')
                                   ", cartonNo);


            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }
        public DataSet GetpickpaletSQL(string pickpallet_no, string shipmentID)
        {
            //Morris 20210222
            string sql = string.Empty;
            sql = string.Format(@"select*from nonedipps.t_shipment_pallet_part a,
                                nonedipps.t_shipment_pallet b where a.pallet_no = b.pallet_no and  a.PICK_STATUS ='FP' and b.PICK_STATUS ='FP' and b.shipment_id ='{0}' and
                                a.pallet_no = (select distinct (substr(pick_pallet_no, 3,16)) pallet_no from nonedipps.t_Sn_status where pick_pallet_no ='{1}' ) "
                        , shipmentID, pickpallet_no);


            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }
    }
}
