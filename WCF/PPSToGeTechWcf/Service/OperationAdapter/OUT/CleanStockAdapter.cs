
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfICTGeTech.Model;

namespace WcfICTGeTech.Service.OperationAdapter
{
    public class CleanStockAdapter : AbstractDataAdapter<CleanStockInModel, CleanStockOutModel>
    {

        public CleanStockAdapter(string data)
            : base(data)
        {
        }

        //protected override string callAction { get { return this.GetType().Name.Replace("Adapter",""); } set { } }

        protected override object CheckModel()
        {
            return null;
        }

        protected override CleanStockOutModel DoAction(object bMsg)
        {
            string output = GetGeTechService.CleanStock(_input);
            return JsonConvert.DeserializeObject<CleanStockOutModel>(output);
        }
    }
}
