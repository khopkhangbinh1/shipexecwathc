using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Model
{
    public class SendMailNotifyInModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string MailKey { get; set; }
        public string[] Emails { get; set; }
    }

    public class SendMailNotifyOutModel : ICTReturnModel
    {
    }



}
