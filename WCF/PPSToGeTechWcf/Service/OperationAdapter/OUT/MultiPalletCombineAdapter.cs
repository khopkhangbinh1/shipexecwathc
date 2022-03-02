using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfICTGeTech.Model;

//namespace WcfICTGeTech.Service.OperationAdapter.OUT
//{
//    class MultiPalletCombineAdapter
//    {
//    }
//}


namespace WcfICTGeTech.Service.OperationAdapter
{
    public class MultiPalletCombineAdapter : AbstractDataAdapter<MultiPalletCombineInModel, MultiPalletCombineOutModel>
    {

        public MultiPalletCombineAdapter(string data)
            : base(data)
        {
        }

        //protected override string callAction { get { return this.GetType().Name.Replace("Adapter",""); } set { } }

        protected override object CheckModel()
        {
            string strGUID = System.Guid.NewGuid().ToString().ToUpper(); 


            return null;
        }

        protected override MultiPalletCombineOutModel DoAction(object bMsg)
        {
            string output = GetGeTechService.MultiPalletCombine(_input);
            return JsonConvert.DeserializeObject<MultiPalletCombineOutModel>(output);
        }
    }
}
