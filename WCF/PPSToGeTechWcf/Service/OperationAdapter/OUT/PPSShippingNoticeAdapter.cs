
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfICTGeTech.Model;

namespace WcfICTGeTech.Service.OperationAdapter
{
    public class PPSShippingNoticeAdapter : AbstractDataAdapter<PPSShippingNoticeInModel, PPSShippingNoticeOutModel>
    {

        public PPSShippingNoticeAdapter(string data)
            : base(data)
        {
        }

        //protected override string callAction { get { return this.GetType().Name.Replace("Adapter",""); } set { } }

        protected override object CheckModel()
        {
            return null;
        }

        protected override PPSShippingNoticeOutModel DoAction(object bMsg)
        {
            string output = GetGeTechService.PPSShippingNotice(_input);
            return JsonConvert.DeserializeObject<PPSShippingNoticeOutModel>(output);
        }
    }
}
