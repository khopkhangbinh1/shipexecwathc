using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Model
{

    public class PPSPickCompleteInModel
    {
        public string TaskNo { get; set; }
        public string TROLLEYID { get; set; }
        public string OPTYPE { get; set; }

        public List<TrolleyMovCarton> CARTONS { get; set; }

    }

    public class PPSPickCompleteOutModel : ICTReturnModel
    {
    }
}
