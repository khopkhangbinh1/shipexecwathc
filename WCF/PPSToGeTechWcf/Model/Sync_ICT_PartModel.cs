using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Model
{

    public class Sync_ICT_PartInModel
    {
        public List<Sync_ICT_PartInItemModel> items { get; set; }
    }

    public class Sync_ICT_PartOutModel : ICTReturnModel
    {
    }

    public class Sync_ICT_PartInItemModel
    {
        public string ICT_PARTNO { get; set; }
        public string PARTDESC { get; set; }
        public string UPDATE_USERID { get; set; }
        public DateTime UPDATE_TIME { get; set; }
        public string ENABLED { get; set; }
    }

}
