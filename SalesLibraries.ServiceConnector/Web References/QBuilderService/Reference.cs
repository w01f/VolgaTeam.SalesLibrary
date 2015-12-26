﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.34209.
// 
#pragma warning disable 1591

namespace SalesLibraries.ServiceConnector.QBuilderService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="QBuilderControllerBinding", Namespace="urn:QBuilderControllerwsdl")]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(QPageLinkModel))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(QPageModel))]
    public partial class QBuilderControllerService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback getAllPagesOperationCompleted;
        
        private System.Threading.SendOrPostCallback deletePagesOperationCompleted;
        
        private System.Threading.SendOrPostCallback getSessionKeyOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public QBuilderControllerService() {
            this.Url = global::SalesLibraries.ServiceConnector.Properties.Settings.Default.SalesLibraries_ServiceConnector_QBuilderService_QBuilderControllerService;
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
        public event getAllPagesCompletedEventHandler getAllPagesCompleted;
        
        /// <remarks/>
        public event deletePagesCompletedEventHandler deletePagesCompleted;
        
        /// <remarks/>
        public event getSessionKeyCompletedEventHandler getSessionKeyCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:QBuilderControllerwsdl#getAllPages", RequestNamespace="urn:QBuilderControllerwsdl", ResponseNamespace="urn:QBuilderControllerwsdl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public QPageModel[] getAllPages(string sessionKey) {
            object[] results = this.Invoke("getAllPages", new object[] {
                        sessionKey});
            return ((QPageModel[])(results[0]));
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
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:QBuilderControllerwsdl#deletePages", RequestNamespace="urn:QBuilderControllerwsdl", ResponseNamespace="urn:QBuilderControllerwsdl")]
        public void deletePages(string sessionKey, string[] pageIds) {
            this.Invoke("deletePages", new object[] {
                        sessionKey,
                        pageIds});
        }
        
        /// <remarks/>
        public void deletePagesAsync(string sessionKey, string[] pageIds) {
            this.deletePagesAsync(sessionKey, pageIds, null);
        }
        
        /// <remarks/>
        public void deletePagesAsync(string sessionKey, string[] pageIds, object userState) {
            if ((this.deletePagesOperationCompleted == null)) {
                this.deletePagesOperationCompleted = new System.Threading.SendOrPostCallback(this.OndeletePagesOperationCompleted);
            }
            this.InvokeAsync("deletePages", new object[] {
                        sessionKey,
                        pageIds}, this.deletePagesOperationCompleted, userState);
        }
        
        private void OndeletePagesOperationCompleted(object arg) {
            if ((this.deletePagesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.deletePagesCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:QBuilderControllerwsdl#getSessionKey", RequestNamespace="urn:QBuilderControllerwsdl", ResponseNamespace="urn:QBuilderControllerwsdl")]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:QBuilderControllerwsdl")]
    public partial class QPageModel {
        
        private string idField;
        
        private string titleField;
        
        private bool isEmailField;
        
        private string urlField;
        
        private string createDateField;
        
        private string expirationDateField;
        
        private string subtitleField;
        
        private string headerField;
        
        private string footerField;
        
        private bool isRestrictedField;
        
        private bool disableBannersField;
        
        private bool disableWidgetsField;
        
        private bool showLinksAsUrlField;
        
        private bool recordActivityField;
        
        private string pinCodeField;
        
        private string activityEmailCopyField;
        
        private string logoField;
        
        private QPageLinkModel[] linksField;
        
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
        public string subtitle {
            get {
                return this.subtitleField;
            }
            set {
                this.subtitleField = value;
            }
        }
        
        /// <remarks/>
        public string header {
            get {
                return this.headerField;
            }
            set {
                this.headerField = value;
            }
        }
        
        /// <remarks/>
        public string footer {
            get {
                return this.footerField;
            }
            set {
                this.footerField = value;
            }
        }
        
        /// <remarks/>
        public bool isRestricted {
            get {
                return this.isRestrictedField;
            }
            set {
                this.isRestrictedField = value;
            }
        }
        
        /// <remarks/>
        public bool disableBanners {
            get {
                return this.disableBannersField;
            }
            set {
                this.disableBannersField = value;
            }
        }
        
        /// <remarks/>
        public bool disableWidgets {
            get {
                return this.disableWidgetsField;
            }
            set {
                this.disableWidgetsField = value;
            }
        }
        
        /// <remarks/>
        public bool showLinksAsUrl {
            get {
                return this.showLinksAsUrlField;
            }
            set {
                this.showLinksAsUrlField = value;
            }
        }
        
        /// <remarks/>
        public bool recordActivity {
            get {
                return this.recordActivityField;
            }
            set {
                this.recordActivityField = value;
            }
        }
        
        /// <remarks/>
        public string pinCode {
            get {
                return this.pinCodeField;
            }
            set {
                this.pinCodeField = value;
            }
        }
        
        /// <remarks/>
        public string activityEmailCopy {
            get {
                return this.activityEmailCopyField;
            }
            set {
                this.activityEmailCopyField = value;
            }
        }
        
        /// <remarks/>
        public string logo {
            get {
                return this.logoField;
            }
            set {
                this.logoField = value;
            }
        }
        
        /// <remarks/>
        public QPageLinkModel[] links {
            get {
                return this.linksField;
            }
            set {
                this.linksField = value;
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:QBuilderControllerwsdl")]
    public partial class QPageLinkModel {
        
        private string idField;
        
        private string parentIdField;
        
        private string linkIdField;
        
        private string libraryIdField;
        
        private string nameField;
        
        private string fileNameField;
        
        private string libraryNameField;
        
        private string logoField;
        
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
        public string parentId {
            get {
                return this.parentIdField;
            }
            set {
                this.parentIdField = value;
            }
        }
        
        /// <remarks/>
        public string linkId {
            get {
                return this.linkIdField;
            }
            set {
                this.linkIdField = value;
            }
        }
        
        /// <remarks/>
        public string libraryId {
            get {
                return this.libraryIdField;
            }
            set {
                this.libraryIdField = value;
            }
        }
        
        /// <remarks/>
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public string fileName {
            get {
                return this.fileNameField;
            }
            set {
                this.fileNameField = value;
            }
        }
        
        /// <remarks/>
        public string libraryName {
            get {
                return this.libraryNameField;
            }
            set {
                this.libraryNameField = value;
            }
        }
        
        /// <remarks/>
        public string logo {
            get {
                return this.logoField;
            }
            set {
                this.logoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void getAllPagesCompletedEventHandler(object sender, getAllPagesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getAllPagesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getAllPagesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public QPageModel[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((QPageModel[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void deletePagesCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void getSessionKeyCompletedEventHandler(object sender, getSessionKeyCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
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
}

#pragma warning restore 1591