﻿namespace CarrierWCF.Wcf
{
    using CarrierWCF.Core;
    using CarrierWCF.Entity;
    using CarrierWCF.Model;
    using CarrierWCF.Models;
    using DBTools;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OperationWCF;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using ExecutionResult = DBTools.ExecutionResult;
    using Packages = Models.Packages;

    public class ICTToCarrierService : HttpHosting, IICTToCarrierService
    {
        private corebridge core;
        private DBTools.ExecutionResult exeRes;
        private const int LOOP_COUT = 3;
        private string strGUID = System.Guid.NewGuid().ToString().ToUpper();

        public ICTToCarrierService()
        {
            this.core = new corebridge();
        }
        private T callAPI<T>(string Url, object data)
        {
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(30);
            HttpResponseMessage result = HttpClientExtensions.PostAsJsonAsync<object>(client, Url, data).Result;
            HttpStatusCode statusCode = result.StatusCode;
            if (statusCode == HttpStatusCode.OK)
            {
                return HttpContentExtensions.ReadAsAsync<T>(result.Content).Result;
            }
            if (statusCode != HttpStatusCode.BadRequest)
            {
                throw new Exception(result.StatusCode.ToString());
            }
            JObject obj2 = HttpContentExtensions.ReadAsAsync<JObject>(result.Content).Result;
            throw new Exception(result.StatusCode.ToString() + "\t" + obj2["Message"].ToString());
        }


        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }
        /// <summary>
        /// 1 carton = 1 request
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string ShipDevTest(string data)
        {
            exeRes = new DBTools.ExecutionResult { Status = true, Message = "OK" };
            ShipModel model = new ShipModel();
            ShipOutputModel model2 = new ShipOutputModel();
            try
            {
                model = JsonConvert.DeserializeObject<ShipModel>(data);
                //model.ClientAccessCredentials = new ClientAccessCredentials();
                //model.UserContext = new UserContext();
                //model.ClientAccessCredentials = corebridge.GetClientAccess();
                //model.UserContext = corebridge.GetUserContext();
                model2 = this.callAPI<ShipOutputModel>(ServiceUrl + "/Ship", JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(model)).ToObject<object>());
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            finally
            {
                if (exeRes.Message == "OK")
                    exeRes.Message = JsonConvert.SerializeObject(model2);
            }
            return exeRes.Message;
        }

        public string Ship(string data)
        {
            int loop = 0;
            string res = "";
            ShipModel model = new ShipModel();
            try
            {
                model = JsonConvert.DeserializeObject<ShipModel>(data);
                //model.ClientAccessCredentials = new ClientAccessCredentials();
                //model.UserContext = new UserContext();
                //model.ClientAccessCredentials = corebridge.GetClientAccess();
                //model.UserContext = corebridge.GetUserContext();
                do
                {
                    try
                    {
                        loop++;
                        res = DefaultShip(model);
                        if (res == "OK")
                            break; //if no exeception then break call api
                    }
                    catch (Exception ex)
                    {
                        res = ex.Message;
                    }
                    finally
                    {
                        if (loop == 3 && res != "OK")
                            SendMailAlert(model.ShipmentRequest.Packages[0].MiscReference5, res);
                    }
                } while (loop < LOOP_COUT);
            }
            catch (Exception ex)
            {
                res = ex.Message;
            }
            return res;
        }

        //public string DefaultShip(string data)
        public string DefaultShip(ShipModel model)
        {
            exeRes = new DBTools.ExecutionResult { Status = true, Message = "OK" };
            //ShipModel model = new ShipModel();
            ShipOutputModel model2 = new ShipOutputModel();

            try
            {
                bool isDuplicate = false;
                //model = JsonConvert.DeserializeObject<ShipModel>(data);
                this.core.WriteLog(JsonConvert.SerializeObject(model), "SHIP", exeRes.Status, "",
                    "ICT-UPS", model.ShipmentRequest.PackageDefaults.ShipperReference, strGUID, model.ShipmentRequest.Packages[0].MiscReference5);

                model2 = this.callAPI<ShipOutputModel>(ServiceUrl + "/Ship", JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(model)).ToObject<object>());
                if (!model2.ErrorCode.Equals(0))
                {
                    exeRes.Status = false;
                    exeRes.Message = string.Format("NG:{0} : {1}", model2.ErrorCode, model2.ErrorMessage);
                }
                if (!model2.ShipmentResponse.PackageDefaults.ErrorCode.Equals(0) || !this.exeRes.Status)
                {
                    if ((model2.ShipmentResponse.PackageDefaults.ErrorCode == 1001 && model2.ShipmentResponse.PackageDefaults.ErrorMessage.Contains("Duplicate Tracking Number"))
                        || (model2.ShipmentResponse.PackageDefaults.ErrorCode == 115 && model2.ShipmentResponse.PackageDefaults.ErrorMessage.Contains("found in shipment history"))//最近5天
                        )
                    {
                        isDuplicate = true;
                        var objSearch = JsonConvert.DeserializeObject<ExecutionResult>(GetGlbMSNByTrackingNo(model, strGUID));
                        if (objSearch.Status)
                        {
                            var rawObj = new UPSRawDataEntity()
                            {
                                CARTON_NO = model.ShipmentRequest.Packages[0].MiscReference5,
                                TRACKING_NO = model.ShipmentRequest.Packages[0].TrackingNumber,
                                GLOBALMSN = objSearch.Anything.ToString(),
                                DELIVERY_NO = model.ShipmentRequest.PackageDefaults.ShipperReference,
                                ClientAccessCredentials = model.ClientAccessCredentials,
                                UserContext = model.UserContext
                            };
                            var reReq = RePrint(JsonConvert.SerializeObject(rawObj), strGUID);
                            if (reReq != "OK")
                            {
                                exeRes.Status = false;
                                exeRes.Message = reReq;
                            }
                        }
                        else
                        {
                            exeRes.Status = false;
                            exeRes.Message = objSearch.Message;
                        }
                    }
                    else
                    {
                        exeRes.Status = false;
                        exeRes.Message = string.Format("NG:SHIP_PackageDefaults {0} : {1}", model2.ShipmentResponse.PackageDefaults.ErrorCode, model2.ShipmentResponse.PackageDefaults.ErrorMessage);
                    }
                }
                if (exeRes.Status && !isDuplicate)
                {
                    foreach (Package package in model2.ShipmentResponse.Packages)
                    {
                        int errorCode = package.ErrorCode;
                        if (!errorCode.Equals(0))
                        {
                            exeRes.Status = false;
                            exeRes.Message = string.Format("NG:Packages {0} : {1}", package.ErrorCode, package.ErrorMessage);
                        }
                        foreach (var doc in package.Documents)
                        {
                            //如果没有获取到label document data直接回滚掉ups 中数据，并返回报错信息
                            if (!doc.ErrorCode.Equals(0))
                            {
                                Void(JsonConvert.SerializeObject(new VoidRequestModel
                                {
                                    //ClientAccessCredentials = new ClientAccessCredentials(),
                                    //UserContext = new UserContext(),
                                    ClientAccessCredentials = model.ClientAccessCredentials,
                                    UserContext = model.UserContext,
                                    GlobalMsns = new int[] { package.GlobalMsn }
                                }));
                                exeRes.Status = false;
                                exeRes.Message = string.Format("NG:Document {0} : has no label data", doc.ErrorCode);
                            }
                        }
                        if (package.Documents[0].ErrorCode.Equals(0))
                        {
                            var rawObj = new UPSRawDataEntity();
                            rawObj.GLOBALMSN = package.GlobalMsn.ToString();
                            rawObj.CARTON_NO = model.ShipmentRequest.Packages.Where(x => x.TrackingNumber == package.TrackingNumber).FirstOrDefault().MiscReference5;
                            rawObj.TRACKING_NO = package.TrackingNumber;
                            rawObj.DELIVERY_NO = model.ShipmentRequest.PackageDefaults.ShipperReference;
                            rawObj.RAWDATA = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(package.Documents[0].RawData[0]));
                            exeRes = this.core.InsertRawData(rawObj);
                        }
                    }

                    if (exeRes.Status)
                    {
                        exeRes = this.core.InsertResponseData(model, model2);
                    }
                }
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            finally
            {
                //this.core.WriteLog(JsonConvert.SerializeObject(model), "SHIP", exeRes.Status, exeRes.Status ? JsonConvert.SerializeObject(model2) : exeRes.Message,
                //    "ICT-UPS", model.ShipmentRequest.PackageDefaults.ShipperReference, model.ShipmentRequest.Packages[0].MiscReference5);
                //this.core.WriteLog(data, "SHIP", exeRes.Status, exeRes.Status ? JsonConvert.SerializeObject(model2) : exeRes.Message, "ICT-UPS", model.ShipmentRequest.PackageDefaults.ShipperReference);
                this.core.WriteUpdateLog(exeRes.Status, exeRes.Status ? JsonConvert.SerializeObject(model2) : exeRes.Message, strGUID);
            }
            return exeRes.Message;
        }

        public string Void(string data)
        {
            exeRes = new ExecutionResult { Status = true, Message = "OK" };
            VoidResponseModel Res = new VoidResponseModel();
            var objTemp = new VoidRequestModel();
            try
            {
                objTemp = JsonConvert.DeserializeObject<VoidRequestModel>(data);
                //objTemp.ClientAccessCredentials = new ClientAccessCredentials();
                //objTemp.UserContext = new UserContext();
                //objTemp.ClientAccessCredentials = corebridge.GetClientAccess();
                //objTemp.UserContext = corebridge.GetUserContext();
                JObject obj2 = JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(objTemp));
                Res = this.callAPI<VoidResponseModel>(ServiceUrl + "/voidpackages", obj2.ToObject<object>());
                if (!Res.ErrorCode.Equals(0))
                {
                    exeRes.Status = false;
                    exeRes.Message = string.Format("NG:{0} : {1}", Res.ErrorCode, "Cancel SN Fail!");
                }
                if (exeRes.Status)
                {
                    foreach (var item in Res.Packages)
                    {
                        if (!item.ErrorCode.Equals(0))
                        {
                            exeRes.Status = false;
                            exeRes.Message = string.Format("NG:Packages {0} : {1}", item.ErrorCode, " Fail!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            finally
            {
                this.core.WriteLog(JsonConvert.SerializeObject(objTemp), "VOID", exeRes.Status, exeRes.Status ? JsonConvert.SerializeObject(Res) : exeRes.Message, "ICT-UPS", "", strGUID, "");
            }
            return exeRes.Message;
        }

        public static string ServiceUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["UPSUrl"];
                // return ConfigurationManager.AppSettings["https://webservice.uat.apple.shipexec.com/ShippingService.svc/rest"];
            }
        }

        public string GetGlbMSNByTrackingNo(ShipModel model, string strGUID)
        {
            exeRes = new ExecutionResult { Status = true, Message = "OK" };
            SearchRequestModel Mdel = new SearchRequestModel()
            {
                //ClientAccessCredentials = new ClientAccessCredentials(),
                //UserContext = new UserContext(),
                //ClientAccessCredentials = corebridge.GetClientAccess(),
                //UserContext = corebridge.GetUserContext(),
                ClientAccessCredentials = model.ClientAccessCredentials,
                UserContext = model.UserContext,
                SearchCriteria = new SearchCriteria()
                {
                    Skip = "0",
                    Take = "10",
                    WhereClauses = new WhereClauses[1],
                    OrderByClauses = new OrderByClauses[1]
                }
            };
            Mdel.SearchCriteria.WhereClauses[0] = new WhereClauses()
            {
                FieldName = "TrackingNumber",
                FieldValue = model.ShipmentRequest.Packages[0].TrackingNumber,
                Operator = "0"
            };

            Mdel.SearchCriteria.OrderByClauses[0] = new OrderByClauses()
            {
                FieldName = "GlobalMsn",
                Direction = "desc"
            };
            SearchOutputModel Mdel2 = new SearchOutputModel();
            try
            {
                Mdel2 = this.callAPI<SearchOutputModel>(ServiceUrl + "/SearchPackageHistory", JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(Mdel)).ToObject<object>());
                if (!Mdel2.ErrorCode.Equals(0))
                {
                    exeRes.Status = false;
                    exeRes.Message = string.Format("NG:{0} : {1}", Mdel2.ErrorCode, "Search ERROR");
                }
                if (exeRes.Status)
                {
                    if (!Mdel2.Packages[0].ErrorCode.Equals(0))
                    {
                        exeRes.Status = false;
                        exeRes.Message = string.Format("NG:Packages {0} : {1}", Mdel2.Packages[0].ErrorCode, Mdel2.Packages[0].ErrorMessage);
                    }
                    else
                    {
                        exeRes.Anything = Mdel2.Packages[0].GlobalMsn;
                    }
                }
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = string.Format("NG:{0} : {1}", "UPS_SEARCH", ex.Message);
            }
            finally
            {
                this.core.WriteLog(JsonConvert.SerializeObject(Mdel), "UPS_SEARCH", exeRes.Status, exeRes.Status ? JsonConvert.SerializeObject(Mdel2) : exeRes.Message, "ICT-UPS", "", strGUID);
            }
            return JsonConvert.SerializeObject(exeRes);
        }
        public string RePrint(string data, string strGUID)
        {
            exeRes = new ExecutionResult { Status = true, Message = "OK" };
            RePrintResponseModel Mdel2 = new RePrintResponseModel();
            RePrintReqUPSModel Mdel = new RePrintReqUPSModel();
            try
            {
                var rawOjb = JsonConvert.DeserializeObject<UPSRawDataEntity>(data);
                Mdel.PrintConfiguration = new PrintConfiguration()
                { GlobalMsn = rawOjb.GLOBALMSN };
                //Mdel.ClientAccessCredentials = new ClientAccessCredentials();
                //Mdel.UserContext = new UserContext();
                //Mdel.ClientAccessCredentials = corebridge.GetClientAccess();
                //Mdel.UserContext = corebridge.GetUserContext();
                Mdel.ClientAccessCredentials = rawOjb.ClientAccessCredentials;
                Mdel.UserContext = rawOjb.UserContext;
                Mdel2 = this.callAPI<RePrintResponseModel>(ServiceUrl + "/Print", JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(Mdel)).ToObject<object>());
                if (!Mdel2.ErrorCode.Equals(0))
                {
                    exeRes.Status = false;
                    exeRes.Message = string.Format("NG:{0} : {1}", Mdel2.ErrorCode, Mdel2.ErrorMessage);
                }
                if (exeRes.Status)
                {
                    rawOjb.RAWDATA = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(Mdel2.DocumentResponses[0].RawData[0]));
                    this.core.InsertRawData(rawOjb);
                }
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = string.Format("NG:{0} : {1}", "UPS_REPRINT", ex.Message);
            }
            finally
            {
                this.core.WriteLog(JsonConvert.SerializeObject(Mdel), "UPS_REPRINT", exeRes.Status, exeRes.Status ? JsonConvert.SerializeObject(Mdel2) : exeRes.Message, "ICT-UPS", "", strGUID);
            }
            return exeRes.Message;
        }

        public string SendMailAlert(string carton, string msg)
        {
            var res = this.core.SendMail(carton, msg);
            return res.Message;
            //await System.Threading.Tasks.Task.Run(() => this.core.SendMail(carton, msg));
        }
    }
}

