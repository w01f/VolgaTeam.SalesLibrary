﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1008
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.1008.
// 
#pragma warning disable 1591

namespace SalesDepot.Services.UtilityService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="UtilityControllerBinding", Namespace="urn:UtilityControllerwsdl")]
    public partial class UtilityControllerService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback getSessionKeyOperationCompleted;
        
        private System.Threading.SendOrPostCallback updateContentOperationCompleted;
        
        private System.Threading.SendOrPostCallback updateHelpOperationCompleted;
        
        private System.Threading.SendOrPostCallback cleanExpiredEmailsOperationCompleted;
        
        private System.Threading.SendOrPostCallback notifyDeadLinksOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public UtilityControllerService() {
            this.Url = global::SalesDepot.Services.Properties.Settings.Default.SalesDepot_Services_UtilityService_UtilityControllerService;
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
        public event getSessionKeyCompletedEventHandler getSessionKeyCompleted;
        
        /// <remarks/>
        public event updateContentCompletedEventHandler updateContentCompleted;
        
        /// <remarks/>
        public event updateHelpCompletedEventHandler updateHelpCompleted;
        
        /// <remarks/>
        public event cleanExpiredEmailsCompletedEventHandler cleanExpiredEmailsCompleted;
        
        /// <remarks/>
        public event notifyDeadLinksCompletedEventHandler notifyDeadLinksCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:UtilityControllerwsdl#getSessionKey", RequestNamespace="urn:UtilityControllerwsdl", ResponseNamespace="urn:UtilityControllerwsdl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public string getSessionKey(string login, string password) {
            object[] results = this.Invoke("getSessionKey", new object[] {
                        login,
                        password});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void getSessionKeyAsync(string login, string password) {
            this.getSessionKeyAsync(login, password, null);
        }
        
        /// <remarks/>
        public void getSessionKeyAsync(string login, string password, object userState) {
            if ((this.getSessionKeyOperationCompleted == null)) {
                this.getSessionKeyOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetSessionKeyOperationCompleted);
            }
            this.InvokeAsync("getSessionKey", new object[] {
                        login,
                        password}, this.getSessionKeyOperationCompleted, userState);
        }
        
        private void OngetSessionKeyOperationCompleted(object arg) {
            if ((this.getSessionKeyCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getSessionKeyCompleted(this, new getSessionKeyCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:UtilityControllerwsdl#updateContent", RequestNamespace="urn:UtilityControllerwsdl", ResponseNamespace="urn:UtilityControllerwsdl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public string updateContent(string sessionKey) {
            object[] results = this.Invoke("updateContent", new object[] {
                        sessionKey});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void updateContentAsync(string sessionKey) {
            this.updateContentAsync(sessionKey, null);
        }
        
        /// <remarks/>
        public void updateContentAsync(string sessionKey, object userState) {
            if ((this.updateContentOperationCompleted == null)) {
                this.updateContentOperationCompleted = new System.Threading.SendOrPostCallback(this.OnupdateContentOperationCompleted);
            }
            this.InvokeAsync("updateContent", new object[] {
                        sessionKey}, this.updateContentOperationCompleted, userState);
        }
        
        private void OnupdateContentOperationCompleted(object arg) {
            if ((this.updateContentCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.updateContentCompleted(this, new updateContentCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:UtilityControllerwsdl#updateHelp", RequestNamespace="urn:UtilityControllerwsdl", ResponseNamespace="urn:UtilityControllerwsdl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public string updateHelp(string sessionKey) {
            object[] results = this.Invoke("updateHelp", new object[] {
                        sessionKey});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void updateHelpAsync(string sessionKey) {
            this.updateHelpAsync(sessionKey, null);
        }
        
        /// <remarks/>
        public void updateHelpAsync(string sessionKey, object userState) {
            if ((this.updateHelpOperationCompleted == null)) {
                this.updateHelpOperationCompleted = new System.Threading.SendOrPostCallback(this.OnupdateHelpOperationCompleted);
            }
            this.InvokeAsync("updateHelp", new object[] {
                        sessionKey}, this.updateHelpOperationCompleted, userState);
        }
        
        private void OnupdateHelpOperationCompleted(object arg) {
            if ((this.updateHelpCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.updateHelpCompleted(this, new updateHelpCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:UtilityControllerwsdl#cleanExpiredEmails", RequestNamespace="urn:UtilityControllerwsdl", ResponseNamespace="urn:UtilityControllerwsdl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public string cleanExpiredEmails(string sessionKey) {
            object[] results = this.Invoke("cleanExpiredEmails", new object[] {
                        sessionKey});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void cleanExpiredEmailsAsync(string sessionKey) {
            this.cleanExpiredEmailsAsync(sessionKey, null);
        }
        
        /// <remarks/>
        public void cleanExpiredEmailsAsync(string sessionKey, object userState) {
            if ((this.cleanExpiredEmailsOperationCompleted == null)) {
                this.cleanExpiredEmailsOperationCompleted = new System.Threading.SendOrPostCallback(this.OncleanExpiredEmailsOperationCompleted);
            }
            this.InvokeAsync("cleanExpiredEmails", new object[] {
                        sessionKey}, this.cleanExpiredEmailsOperationCompleted, userState);
        }
        
        private void OncleanExpiredEmailsOperationCompleted(object arg) {
            if ((this.cleanExpiredEmailsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.cleanExpiredEmailsCompleted(this, new cleanExpiredEmailsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:UtilityControllerwsdl#notifyDeadLinks", RequestNamespace="urn:UtilityControllerwsdl", ResponseNamespace="urn:UtilityControllerwsdl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public string notifyDeadLinks(string sessionKey) {
            object[] results = this.Invoke("notifyDeadLinks", new object[] {
                        sessionKey});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void notifyDeadLinksAsync(string sessionKey) {
            this.notifyDeadLinksAsync(sessionKey, null);
        }
        
        /// <remarks/>
        public void notifyDeadLinksAsync(string sessionKey, object userState) {
            if ((this.notifyDeadLinksOperationCompleted == null)) {
                this.notifyDeadLinksOperationCompleted = new System.Threading.SendOrPostCallback(this.OnnotifyDeadLinksOperationCompleted);
            }
            this.InvokeAsync("notifyDeadLinks", new object[] {
                        sessionKey}, this.notifyDeadLinksOperationCompleted, userState);
        }
        
        private void OnnotifyDeadLinksOperationCompleted(object arg) {
            if ((this.notifyDeadLinksCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.notifyDeadLinksCompleted(this, new notifyDeadLinksCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void getSessionKeyCompletedEventHandler(object sender, getSessionKeyCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getSessionKeyCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getSessionKeyCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void updateContentCompletedEventHandler(object sender, updateContentCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class updateContentCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal updateContentCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void updateHelpCompletedEventHandler(object sender, updateHelpCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class updateHelpCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal updateHelpCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void cleanExpiredEmailsCompletedEventHandler(object sender, cleanExpiredEmailsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class cleanExpiredEmailsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal cleanExpiredEmailsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void notifyDeadLinksCompletedEventHandler(object sender, notifyDeadLinksCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class notifyDeadLinksCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal notifyDeadLinksCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
}

#pragma warning restore 1591