﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.296.
// 
#pragma warning disable 1591

namespace SalesDepot.Services.QBuilderService {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="QbuilderControllerBinding", Namespace="urn:QbuilderControllerwsdl")]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(QPageRecord))]
    public partial class QbuilderControllerService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback getSessionKeyOperationCompleted;
        
        private System.Threading.SendOrPostCallback getAllPagesOperationCompleted;
        
        private System.Threading.SendOrPostCallback deletePageFromServiceOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public QbuilderControllerService() {
            this.Url = global::SalesDepot.Services.Properties.Settings.Default.SalesDepot_Services_QBuilderService_QbuilderControllerService;
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
        public event getAllPagesCompletedEventHandler getAllPagesCompleted;
        
        /// <remarks/>
        public event deletePageFromServiceCompletedEventHandler deletePageFromServiceCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:QbuilderControllerwsdl#getSessionKey", RequestNamespace="urn:QbuilderControllerwsdl", ResponseNamespace="urn:QbuilderControllerwsdl")]
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
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:QbuilderControllerwsdl#getAllPages", RequestNamespace="urn:QbuilderControllerwsdl", ResponseNamespace="urn:QbuilderControllerwsdl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public QPageRecord[] getAllPages(string sessionKey) {
            object[] results = this.Invoke("getAllPages", new object[] {
                        sessionKey});
            return ((QPageRecord[])(results[0]));
        }
        
        /// <remarks/>
        public void getAllPagesAsync(string sessionKey) {
            this.getAllPagesAsync(sessionKey, null);
        }
        
        /// <remarks/>
        public void getAllPagesAsync(string sessionKey, object userState) {
            if ((this.getAllPagesOperationCompleted == null)) {
                this.getAllPagesOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetAllPagesOperationCompleted);
            }
            this.InvokeAsync("getAllPages", new object[] {
                        sessionKey}, this.getAllPagesOperationCompleted, userState);
        }
        
        private void OngetAllPagesOperationCompleted(object arg) {
            if ((this.getAllPagesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getAllPagesCompleted(this, new getAllPagesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:QbuilderControllerwsdl#deletePageFromService", RequestNamespace="urn:QbuilderControllerwsdl", ResponseNamespace="urn:QbuilderControllerwsdl")]
        public void deletePageFromService(string sessionKey, string pageId) {
            this.Invoke("deletePageFromService", new object[] {
                        sessionKey,
                        pageId});
        }
        
        /// <remarks/>
        public void deletePageFromServiceAsync(string sessionKey, string pageId) {
            this.deletePageFromServiceAsync(sessionKey, pageId, null);
        }
        
        /// <remarks/>
        public void deletePageFromServiceAsync(string sessionKey, string pageId, object userState) {
            if ((this.deletePageFromServiceOperationCompleted == null)) {
                this.deletePageFromServiceOperationCompleted = new System.Threading.SendOrPostCallback(this.OndeletePageFromServiceOperationCompleted);
            }
            this.InvokeAsync("deletePageFromService", new object[] {
                        sessionKey,
                        pageId}, this.deletePageFromServiceOperationCompleted, userState);
        }
        
        private void OndeletePageFromServiceOperationCompleted(object arg) {
            if ((this.deletePageFromServiceCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.deletePageFromServiceCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:QbuilderControllerwsdl")]
    public partial class QPageRecord {
        
        private string idField;
        
        private string titleField;
        
        private bool isEmailField;
        
        private string urlField;
        
        private string createDateField;
        
        private string expirationDateField;
        
        private string loginField;
        
        private string firstNameField;
        
        private string lastNameField;
        
        private string emailField;
        
        private string groupsField;
        
        /// <remarks/>
        public string id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string title {
            get {
                return this.titleField;
            }
            set {
                this.titleField = value;
            }
        }
        
        /// <remarks/>
        public bool isEmail {
            get {
                return this.isEmailField;
            }
            set {
                this.isEmailField = value;
            }
        }
        
        /// <remarks/>
        public string url {
            get {
                return this.urlField;
            }
            set {
                this.urlField = value;
            }
        }
        
        /// <remarks/>
        public string createDate {
            get {
                return this.createDateField;
            }
            set {
                this.createDateField = value;
            }
        }
        
        /// <remarks/>
        public string expirationDate {
            get {
                return this.expirationDateField;
            }
            set {
                this.expirationDateField = value;
            }
        }
        
        /// <remarks/>
        public string login {
            get {
                return this.loginField;
            }
            set {
                this.loginField = value;
            }
        }
        
        /// <remarks/>
        public string firstName {
            get {
                return this.firstNameField;
            }
            set {
                this.firstNameField = value;
            }
        }
        
        /// <remarks/>
        public string lastName {
            get {
                return this.lastNameField;
            }
            set {
                this.lastNameField = value;
            }
        }
        
        /// <remarks/>
        public string email {
            get {
                return this.emailField;
            }
            set {
                this.emailField = value;
            }
        }
        
        /// <remarks/>
        public string groups {
            get {
                return this.groupsField;
            }
            set {
                this.groupsField = value;
            }
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
    public delegate void getAllPagesCompletedEventHandler(object sender, getAllPagesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getAllPagesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getAllPagesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public QPageRecord[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((QPageRecord[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void deletePageFromServiceCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591