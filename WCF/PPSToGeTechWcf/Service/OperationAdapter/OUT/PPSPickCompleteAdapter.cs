using Newtonsoft.Json;
using WcfICTGeTech.Model;

namespace WcfICTGeTech.Service.OperationAdapter
{

    public class PPSPickCompleteAdapter : AbstractDataAdapter<PPSPickCompleteInModel, PPSPickCompleteOutModel>
    {

        public PPSPickCompleteAdapter(string data)
            : base(data)
        {
        }

        //protected override string callAction { get { return this.GetType().Name.Replace("Adapter",""); } set { } }

        protected override object CheckModel()
        {
            return null;
        }

        protected override PPSPickCompleteOutModel DoAction(object bMsg)
        {
            string output = GetGeTechService.PPSPickComplete(_input);
            return JsonConvert.DeserializeObject<PPSPickCompleteOutModel>(output);
        }
    }
}
