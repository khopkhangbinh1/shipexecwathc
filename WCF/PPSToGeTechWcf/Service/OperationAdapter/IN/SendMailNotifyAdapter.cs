using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfICTGeTech.Model;

namespace WcfICTGeTech.Service.OperationAdapter
{

    public class SendMailNotifyAdapter : AbstractDataAdapter<SendMailNotifyInModel, SendMailNotifyOutModel>
    {

        public SendMailNotifyAdapter(string data)
            : base(data)
        {
        }

        //protected override string callAction { get { return "Sync_Location"; } set { } }

        protected override object CheckModel()
        {
            if (inModel == null || string.IsNullOrEmpty(inModel.Subject) || string.IsNullOrEmpty(inModel.Body)
                || inModel.Emails == null )
                throw new Exception("传入格式错误");

            var emails = inModel.Emails.ToList();
            emails.RemoveAll(x => string.IsNullOrEmpty(x) ||
            !(x.ToLower().Contains("@luxshare.com") || x.ToLower().Contains("@luxshare-ict.com")));

            if (emails.Count<1)
                throw new Exception("不允许外发 , 或邮件人员为空");

            //if (inModel.MailKey != "XXXXX")
            //    throw new Exception("");
            inModel.Emails = emails.ToArray();
            return null;
        }

        protected override SendMailNotifyOutModel DoAction(object bMsg)
        {
            CommonHelper.SendMail(inModel.Subject, inModel.Body, inModel.Emails);
            return new SendMailNotifyOutModel
            {
                Result = true,
            };
        }

    }
}
