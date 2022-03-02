using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfICTGeTech.Model;

namespace WcfICTGeTech.Service.OperationAdapter
{
    public class Sync_LocationAdapter : AbstractDataAdapter<Sync_LocationInModel, Sync_LocationOutModel>
    {
        private IEnumerable<Db_WMS_LOCATION> dbModels { get; set; }


        public Sync_LocationAdapter(string data)
            : base(data)
        {
        }

        //protected override string callAction { get { return "Sync_Location"; } set { } }

        protected override object CheckModel()
        {
            if (inModel.items == null || inModel.items.Count() < 1)
                throw new Exception("没有 items");
            var tmpList = inModel.items.Select(x => x.WAREHOUSE_NO).Distinct();
            var wareHouse = getWarehouseId(tmpList);

            if(tmpList.Where(x=>wareHouse.FirstOrDefault(y=>y.WAREHOUSE_NO == x)==null).Count()>0)
                throw new Exception("没有 对应WareHouse Id");

            // 转换 Model
            dbModels = inModel.items.Select(x => new Db_WMS_LOCATION
            {
                LOCATION_NO = x.LOCATION_NO,
                LOCATION_NAME = "AT-" + x.LOCATION_NAME,
                UPDATE_USERID = 10086,
                LOCATION_TYPE = x.LOCATION_TYPE,
                ENABLED = x.ENABLED,
                REGION = "ALL",
                WAREHOUSE_ID = wareHouse.FirstOrDefault(y=>y.WAREHOUSE_NO == x.WAREHOUSE_NO).WAREHOUSE_ID
            });

            return null;
        }

        protected override Sync_LocationOutModel DoAction(object bMsg)
        {
            string sql = @"
            MERGE INTO PPSUSER.WMS_LOCATION A
                USING (SELECT '' FROM DUAL) C
                ON (A.LOCATION_NO = :LOCATION_NO)
            WHEN MATCHED THEN
                  UPDATE
                     SET A.LOCATION_NAME = :LOCATION_NAME, A.UPDATE_USERID= :UPDATE_USERID  ,A.UPDATE_TIME = SYSDATE,
                     A.ENABLED = :ENABLED,A.LOCATION_TYPE = :LOCATION_TYPE,A.WAREHOUSE_ID=:WAREHOUSE_ID
            WHEN NOT MATCHED THEN
              INSERT
               (LOCATION_ID,LOCATION_NO,LOCATION_NAME,UPDATE_USERID,UPDATE_TIME,ENABLED,LOCATION_TYPE,WAREHOUSE_ID,REGION)
              VALUES
              (null,:LOCATION_NO,:LOCATION_NAME,:UPDATE_USERID,SYSDATE,:ENABLED,:LOCATION_TYPE,:WAREHOUSE_ID,:REGION)";

            Dictionary<string, object> trans = new Dictionary<string, object>();
            trans.Add(sql, dbModels.ToList());
            ClientUtils.DoExtremeSpeedTransaction(trans);

            //写入系统
            return new Sync_LocationOutModel
            {
                Result = true,
            };
        }


        private IEnumerable<Db_WMS_WAREHOUSE> getWarehouseId(IEnumerable<string> warehouseNo)
        {
            var list = ClientUtils.Query<Db_WMS_WAREHOUSE>
                 ("SELECT A.WAREHOUSE_ID, A.WAREHOUSE_NO   FROM PPSUSER.WMS_WAREHOUSE A  WHERE A.WAREHOUSE_NO IN :warehouseNo", new { warehouseNo });
            return list;
        }
    }
}
