using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Model
{
    public class Sync_TrolleyInModel
    {
        public List<Sync_TrolleyInItemModel> items { get; set; }
    }

    public class Sync_TrolleyOutModel : ICTReturnModel
    {
    }

    public class Sync_TrolleyInItemModel
    {
        public string TROLLEY_ID { get; set; }
        public string TROLLEY_TYPE { get; set; }
        public string SIDES_NO { get; set; }
        public int LEVEL_NO { get; set; }
        public int MAXSEQ { get; set; }
        public int PACKQTY { get; set; }
        public string UPDATE_USERID { get; set; }
        public DateTime UPDATE_TIME { get; set; }
        public string ENABLED { get; set; }
    }


}
