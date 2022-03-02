﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.42000 版自动生成。
// 
#pragma warning disable 1591

namespace NPIPickListAC.JSMESWebReference {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="MesApiSoap", Namespace="http://tempuri.org.bill/")]
    public partial class MesApi : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetMESStockInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetMESPNInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetMaterialTransferInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpdateStockINStatusOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpdateReturnLineStatusOperationCompleted;
        
        private System.Threading.SendOrPostCallback CheckPackOutLogicOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetWOOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetSAPMIX_COLLECTIONOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetSAPTransfer_WHOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetSAPAllocaDNOperationCompleted;
        
        private System.Threading.SendOrPostCallback Transfer_RDdataOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetMesdataOperationCompleted;
        
        private System.Threading.SendOrPostCallback SapDataToFoxOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;

        /// <remarks/>
        public MesApi(string strUrl)
        {
            this.Url = strUrl;
            //this.Url = global::EDIWarehouseInOUT.Properties.Settings.Default.EDIWarehouseInOUT_WebReferenceWCF_MesApi;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetMESStockInfoCompletedEventHandler GetMESStockInfoCompleted;
        
        /// <remarks/>
        public event GetMESPNInfoCompletedEventHandler GetMESPNInfoCompleted;
        
        /// <remarks/>
        public event GetMaterialTransferInfoCompletedEventHandler GetMaterialTransferInfoCompleted;
        
        /// <remarks/>
        public event UpdateStockINStatusCompletedEventHandler UpdateStockINStatusCompleted;
        
        /// <remarks/>
        public event UpdateReturnLineStatusCompletedEventHandler UpdateReturnLineStatusCompleted;
        
        /// <remarks/>
        public event CheckPackOutLogicCompletedEventHandler CheckPackOutLogicCompleted;
        
        /// <remarks/>
        public event GetWOCompletedEventHandler GetWOCompleted;
        
        /// <remarks/>
        public event GetSAPMIX_COLLECTIONCompletedEventHandler GetSAPMIX_COLLECTIONCompleted;
        
        /// <remarks/>
        public event GetSAPTransfer_WHCompletedEventHandler GetSAPTransfer_WHCompleted;
        
        /// <remarks/>
        public event GetSAPAllocaDNCompletedEventHandler GetSAPAllocaDNCompleted;
        
        /// <remarks/>
        public event Transfer_RDdataCompletedEventHandler Transfer_RDdataCompleted;
        
        /// <remarks/>
        public event GetMesdataCompletedEventHandler GetMesdataCompleted;
        
        /// <remarks/>
        public event SapDataToFoxCompletedEventHandler SapDataToFoxCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org.bill/GetMESStockInfo", RequestNamespace="http://tempuri.org.bill/", ResponseNamespace="http://tempuri.org.bill/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetMESStockInfo(string Box) {
            object[] results = this.Invoke("GetMESStockInfo", new object[] {
                        Box});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetMESStockInfoAsync(string Box) {
            this.GetMESStockInfoAsync(Box, null);
        }
        
        /// <remarks/>
        public void GetMESStockInfoAsync(string Box, object userState) {
            if ((this.GetMESStockInfoOperationCompleted == null)) {
                this.GetMESStockInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetMESStockInfoOperationCompleted);
            }
            this.InvokeAsync("GetMESStockInfo", new object[] {
                        Box}, this.GetMESStockInfoOperationCompleted, userState);
        }
        
        private void OnGetMESStockInfoOperationCompleted(object arg) {
            if ((this.GetMESStockInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetMESStockInfoCompleted(this, new GetMESStockInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org.bill/GetMESPNInfo", RequestNamespace="http://tempuri.org.bill/", ResponseNamespace="http://tempuri.org.bill/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetMESPNInfo(string PN) {
            object[] results = this.Invoke("GetMESPNInfo", new object[] {
                        PN});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetMESPNInfoAsync(string PN) {
            this.GetMESPNInfoAsync(PN, null);
        }
        
        /// <remarks/>
        public void GetMESPNInfoAsync(string PN, object userState) {
            if ((this.GetMESPNInfoOperationCompleted == null)) {
                this.GetMESPNInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetMESPNInfoOperationCompleted);
            }
            this.InvokeAsync("GetMESPNInfo", new object[] {
                        PN}, this.GetMESPNInfoOperationCompleted, userState);
        }
        
        private void OnGetMESPNInfoOperationCompleted(object arg) {
            if ((this.GetMESPNInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetMESPNInfoCompleted(this, new GetMESPNInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org.bill/GetMaterialTransferInfo", RequestNamespace="http://tempuri.org.bill/", ResponseNamespace="http://tempuri.org.bill/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetMaterialTransferInfo(string BoxID) {
            object[] results = this.Invoke("GetMaterialTransferInfo", new object[] {
                        BoxID});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetMaterialTransferInfoAsync(string BoxID) {
            this.GetMaterialTransferInfoAsync(BoxID, null);
        }
        
        /// <remarks/>
        public void GetMaterialTransferInfoAsync(string BoxID, object userState) {
            if ((this.GetMaterialTransferInfoOperationCompleted == null)) {
                this.GetMaterialTransferInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetMaterialTransferInfoOperationCompleted);
            }
            this.InvokeAsync("GetMaterialTransferInfo", new object[] {
                        BoxID}, this.GetMaterialTransferInfoOperationCompleted, userState);
        }
        
        private void OnGetMaterialTransferInfoOperationCompleted(object arg) {
            if ((this.GetMaterialTransferInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetMaterialTransferInfoCompleted(this, new GetMaterialTransferInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org.bill/UpdateStockINStatus", RequestNamespace="http://tempuri.org.bill/", ResponseNamespace="http://tempuri.org.bill/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string UpdateStockINStatus(string Box) {
            object[] results = this.Invoke("UpdateStockINStatus", new object[] {
                        Box});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void UpdateStockINStatusAsync(string Box) {
            this.UpdateStockINStatusAsync(Box, null);
        }
        
        /// <remarks/>
        public void UpdateStockINStatusAsync(string Box, object userState) {
            if ((this.UpdateStockINStatusOperationCompleted == null)) {
                this.UpdateStockINStatusOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateStockINStatusOperationCompleted);
            }
            this.InvokeAsync("UpdateStockINStatus", new object[] {
                        Box}, this.UpdateStockINStatusOperationCompleted, userState);
        }
        
        private void OnUpdateStockINStatusOperationCompleted(object arg) {
            if ((this.UpdateStockINStatusCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateStockINStatusCompleted(this, new UpdateStockINStatusCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org.bill/UpdateReturnLineStatus", RequestNamespace="http://tempuri.org.bill/", ResponseNamespace="http://tempuri.org.bill/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string UpdateReturnLineStatus(string Box) {
            object[] results = this.Invoke("UpdateReturnLineStatus", new object[] {
                        Box});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void UpdateReturnLineStatusAsync(string Box) {
            this.UpdateReturnLineStatusAsync(Box, null);
        }
        
        /// <remarks/>
        public void UpdateReturnLineStatusAsync(string Box, object userState) {
            if ((this.UpdateReturnLineStatusOperationCompleted == null)) {
                this.UpdateReturnLineStatusOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateReturnLineStatusOperationCompleted);
            }
            this.InvokeAsync("UpdateReturnLineStatus", new object[] {
                        Box}, this.UpdateReturnLineStatusOperationCompleted, userState);
        }
        
        private void OnUpdateReturnLineStatusOperationCompleted(object arg) {
            if ((this.UpdateReturnLineStatusCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateReturnLineStatusCompleted(this, new UpdateReturnLineStatusCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org.bill/CheckPackOutLogic", RequestNamespace="http://tempuri.org.bill/", ResponseNamespace="http://tempuri.org.bill/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string CheckPackOutLogic(string json) {
            object[] results = this.Invoke("CheckPackOutLogic", new object[] {
                        json});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void CheckPackOutLogicAsync(string json) {
            this.CheckPackOutLogicAsync(json, null);
        }
        
        /// <remarks/>
        public void CheckPackOutLogicAsync(string json, object userState) {
            if ((this.CheckPackOutLogicOperationCompleted == null)) {
                this.CheckPackOutLogicOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCheckPackOutLogicOperationCompleted);
            }
            this.InvokeAsync("CheckPackOutLogic", new object[] {
                        json}, this.CheckPackOutLogicOperationCompleted, userState);
        }
        
        private void OnCheckPackOutLogicOperationCompleted(object arg) {
            if ((this.CheckPackOutLogicCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CheckPackOutLogicCompleted(this, new CheckPackOutLogicCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org.bill/GetWO", RequestNamespace="http://tempuri.org.bill/", ResponseNamespace="http://tempuri.org.bill/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetWO(string json) {
            object[] results = this.Invoke("GetWO", new object[] {
                        json});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetWOAsync(string json) {
            this.GetWOAsync(json, null);
        }
        
        /// <remarks/>
        public void GetWOAsync(string json, object userState) {
            if ((this.GetWOOperationCompleted == null)) {
                this.GetWOOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetWOOperationCompleted);
            }
            this.InvokeAsync("GetWO", new object[] {
                        json}, this.GetWOOperationCompleted, userState);
        }
        
        private void OnGetWOOperationCompleted(object arg) {
            if ((this.GetWOCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetWOCompleted(this, new GetWOCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org.bill/GetSAPMIX_COLLECTION", RequestNamespace="http://tempuri.org.bill/", ResponseNamespace="http://tempuri.org.bill/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetSAPMIX_COLLECTION(string json) {
            object[] results = this.Invoke("GetSAPMIX_COLLECTION", new object[] {
                        json});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetSAPMIX_COLLECTIONAsync(string json) {
            this.GetSAPMIX_COLLECTIONAsync(json, null);
        }
        
        /// <remarks/>
        public void GetSAPMIX_COLLECTIONAsync(string json, object userState) {
            if ((this.GetSAPMIX_COLLECTIONOperationCompleted == null)) {
                this.GetSAPMIX_COLLECTIONOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetSAPMIX_COLLECTIONOperationCompleted);
            }
            this.InvokeAsync("GetSAPMIX_COLLECTION", new object[] {
                        json}, this.GetSAPMIX_COLLECTIONOperationCompleted, userState);
        }
        
        private void OnGetSAPMIX_COLLECTIONOperationCompleted(object arg) {
            if ((this.GetSAPMIX_COLLECTIONCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetSAPMIX_COLLECTIONCompleted(this, new GetSAPMIX_COLLECTIONCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org.bill/GetSAPTransfer_WH", RequestNamespace="http://tempuri.org.bill/", ResponseNamespace="http://tempuri.org.bill/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetSAPTransfer_WH(string json) {
            object[] results = this.Invoke("GetSAPTransfer_WH", new object[] {
                        json});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetSAPTransfer_WHAsync(string json) {
            this.GetSAPTransfer_WHAsync(json, null);
        }
        
        /// <remarks/>
        public void GetSAPTransfer_WHAsync(string json, object userState) {
            if ((this.GetSAPTransfer_WHOperationCompleted == null)) {
                this.GetSAPTransfer_WHOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetSAPTransfer_WHOperationCompleted);
            }
            this.InvokeAsync("GetSAPTransfer_WH", new object[] {
                        json}, this.GetSAPTransfer_WHOperationCompleted, userState);
        }
        
        private void OnGetSAPTransfer_WHOperationCompleted(object arg) {
            if ((this.GetSAPTransfer_WHCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetSAPTransfer_WHCompleted(this, new GetSAPTransfer_WHCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org.bill/GetSAPAllocaDN", RequestNamespace="http://tempuri.org.bill/", ResponseNamespace="http://tempuri.org.bill/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetSAPAllocaDN(string json) {
            object[] results = this.Invoke("GetSAPAllocaDN", new object[] {
                        json});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetSAPAllocaDNAsync(string json) {
            this.GetSAPAllocaDNAsync(json, null);
        }
        
        /// <remarks/>
        public void GetSAPAllocaDNAsync(string json, object userState) {
            if ((this.GetSAPAllocaDNOperationCompleted == null)) {
                this.GetSAPAllocaDNOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetSAPAllocaDNOperationCompleted);
            }
            this.InvokeAsync("GetSAPAllocaDN", new object[] {
                        json}, this.GetSAPAllocaDNOperationCompleted, userState);
        }
        
        private void OnGetSAPAllocaDNOperationCompleted(object arg) {
            if ((this.GetSAPAllocaDNCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetSAPAllocaDNCompleted(this, new GetSAPAllocaDNCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org.bill/Transfer_RDdata", RequestNamespace="http://tempuri.org.bill/", ResponseNamespace="http://tempuri.org.bill/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void Transfer_RDdata(string Sn) {
            this.Invoke("Transfer_RDdata", new object[] {
                        Sn});
        }
        
        /// <remarks/>
        public void Transfer_RDdataAsync(string Sn) {
            this.Transfer_RDdataAsync(Sn, null);
        }
        
        /// <remarks/>
        public void Transfer_RDdataAsync(string Sn, object userState) {
            if ((this.Transfer_RDdataOperationCompleted == null)) {
                this.Transfer_RDdataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTransfer_RDdataOperationCompleted);
            }
            this.InvokeAsync("Transfer_RDdata", new object[] {
                        Sn}, this.Transfer_RDdataOperationCompleted, userState);
        }
        
        private void OnTransfer_RDdataOperationCompleted(object arg) {
            if ((this.Transfer_RDdataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Transfer_RDdataCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org.bill/GetMesdata", RequestNamespace="http://tempuri.org.bill/", ResponseNamespace="http://tempuri.org.bill/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void GetMesdata(string Json) {
            this.Invoke("GetMesdata", new object[] {
                        Json});
        }
        
        /// <remarks/>
        public void GetMesdataAsync(string Json) {
            this.GetMesdataAsync(Json, null);
        }
        
        /// <remarks/>
        public void GetMesdataAsync(string Json, object userState) {
            if ((this.GetMesdataOperationCompleted == null)) {
                this.GetMesdataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetMesdataOperationCompleted);
            }
            this.InvokeAsync("GetMesdata", new object[] {
                        Json}, this.GetMesdataOperationCompleted, userState);
        }
        
        private void OnGetMesdataOperationCompleted(object arg) {
            if ((this.GetMesdataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetMesdataCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org.bill/SapDataToFox", RequestNamespace="http://tempuri.org.bill/", ResponseNamespace="http://tempuri.org.bill/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void SapDataToFox(string Json) {
            this.Invoke("SapDataToFox", new object[] {
                        Json});
        }
        
        /// <remarks/>
        public void SapDataToFoxAsync(string Json) {
            this.SapDataToFoxAsync(Json, null);
        }
        
        /// <remarks/>
        public void SapDataToFoxAsync(string Json, object userState) {
            if ((this.SapDataToFoxOperationCompleted == null)) {
                this.SapDataToFoxOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSapDataToFoxOperationCompleted);
            }
            this.InvokeAsync("SapDataToFox", new object[] {
                        Json}, this.SapDataToFoxOperationCompleted, userState);
        }
        
        private void OnSapDataToFoxOperationCompleted(object arg) {
            if ((this.SapDataToFoxCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SapDataToFoxCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void GetMESStockInfoCompletedEventHandler(object sender, GetMESStockInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetMESStockInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetMESStockInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void GetMESPNInfoCompletedEventHandler(object sender, GetMESPNInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetMESPNInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetMESPNInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void GetMaterialTransferInfoCompletedEventHandler(object sender, GetMaterialTransferInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetMaterialTransferInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetMaterialTransferInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void UpdateStockINStatusCompletedEventHandler(object sender, UpdateStockINStatusCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpdateStockINStatusCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UpdateStockINStatusCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void UpdateReturnLineStatusCompletedEventHandler(object sender, UpdateReturnLineStatusCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpdateReturnLineStatusCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UpdateReturnLineStatusCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void CheckPackOutLogicCompletedEventHandler(object sender, CheckPackOutLogicCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CheckPackOutLogicCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CheckPackOutLogicCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void GetWOCompletedEventHandler(object sender, GetWOCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetWOCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetWOCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void GetSAPMIX_COLLECTIONCompletedEventHandler(object sender, GetSAPMIX_COLLECTIONCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetSAPMIX_COLLECTIONCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetSAPMIX_COLLECTIONCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void GetSAPTransfer_WHCompletedEventHandler(object sender, GetSAPTransfer_WHCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetSAPTransfer_WHCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetSAPTransfer_WHCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void GetSAPAllocaDNCompletedEventHandler(object sender, GetSAPAllocaDNCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetSAPAllocaDNCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetSAPAllocaDNCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void Transfer_RDdataCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void GetMesdataCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void SapDataToFoxCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591