using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Model
{
    public class Sync_LocationInModel
    {
        public Sync_LocationInItemModel[] items { get; set; }
    }

    public class Sync_LocationOutModel : ICTReturnModel
    {
    }

    public class Sync_LocationInItemModel
    {
        public string LOCATION_NO { get; set; }
        public string LOCATION_NAME { get; set; }
        public string LOCATION_TYPE { get; set; }
        public string WAREHOUSE_NO { get; set; }
        public string UPDATE_USERID { get; set; }
        public DateTime UPDATE_TIME { get; set; }
        public string ENABLED { get; set; }
    }


}
