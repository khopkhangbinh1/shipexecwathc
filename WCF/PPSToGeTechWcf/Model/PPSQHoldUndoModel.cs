using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Model
{
    public class PPSQHoldUndoInModel
    {
        public string OPT { get; set; }
        public List<TrolleyMovCarton> CARTONS { get; set; }

    }

    public class PPSQHoldUndoOutModel : ICTReturnModel
    {
    }
}
