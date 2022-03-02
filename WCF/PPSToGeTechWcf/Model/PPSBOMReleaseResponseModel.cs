using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Model
{
    public class PPSBOMReleaseResponseInModel
    {
        public string TaskNo { get; set; }
        public string OPTYPE { get; set; }
        public string AUFNR { get; set; }
        public string AUART { get; set; }
        public List<TrolleyMovCarton> CARTONS { get; set; }
    }

    public class PPSBOMReleaseResponseOutModel : ICTReturnModel
    {
        public string TaskNo { get; set; }
    }

}
