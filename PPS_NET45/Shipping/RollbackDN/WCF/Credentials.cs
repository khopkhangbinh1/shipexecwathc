﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollbackDN.Wcf
{
    /// <summary>
    /// Credentials Model
    /// </summary>
    public class Credentials
    {
        public ClientAccessCredentials ClientAccessCredentials { get; set; }
        public UserContext UserContext { get; set; }
    }
    //public class ClientAccessCredentials
    //{
    //    public string AccessKey { get => "6c99edae-02e9-4ea6-a9b9-434c766ed763"; }
    //    public string Name { get => "webservice"; }
    //}
    //public class UserContext
    //{
    //    public string CompanyId { get => "0938d710-f64e-4753-85e1-42875acc2190"; }
    //    public string Machine { get => "ICT_KS_MACHINE"; }
    //}
    public class ClientAccessCredentials
    {
        public string AccessKey { get; set; }
        public string Name { get; set; }
    }
    public class UserContext
    {
        public string CompanyId { get; set; }
        public string SiteId { get; set; }
        public string Machine { get; set; }
    }
}
