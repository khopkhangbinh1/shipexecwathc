using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Model
{
    public class PackoutNotifyInModel
    {
        public string TROLLEYID { get; set; }
        public string OPTYPE { get; set; }
        public int PACKQTY { get; set; }
    }

    public class PackoutNotifyOutModel : ICTReturnModel
    {
    }
}
