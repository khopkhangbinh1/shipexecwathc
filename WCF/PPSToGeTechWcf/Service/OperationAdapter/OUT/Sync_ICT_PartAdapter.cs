using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfICTGeTech.Model;

namespace WcfICTGeTech.Service.OperationAdapter
{

    public class Sync_ICT_PartAdapter : AbstractDataAdapter<Sync_ICT_PartInModel, Sync_ICT_PartOutModel>
    {

        public Sync_ICT_PartAdapter(string data)
            : base(data)
        {
        }

        //protected override string callAction { get { return this.GetType().Name.Replace("Adapter",""); } set { } }

        protected override object CheckModel()
        {
            return null;
        }

        protected override Sync_ICT_PartOutModel DoAction(object bMsg)
        {
            string output = GetGeTechService.Sync_ICT_Part(_input);
            return JsonConvert.DeserializeObject<Sync_ICT_PartOutModel>(output);
        }
    }

}
