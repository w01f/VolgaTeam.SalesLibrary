﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace SalesLibraries.ServiceConnector.InactiveUsersService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="InactiveUsersControllerBinding", Namespace="urn:InactiveUsersControllerwsdl")]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(UserViewModel))]
    public partial class InactiveUsersControllerService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback getInactiveUsersOperationCompleted;
        
        private System.Threading.SendOrPostCallback resetUserOperationCompleted;
        
        private System.Threading.SendOrPostCallback deleteUserOperationCompleted;
        
        private System.Threading.SendOrPostCallback getSessionKeyOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public InactiveUsersControllerService() {
            this.Url = global::SalesLibraries.ServiceConnector.Properties.Settings.Default.SalesLibraries_ServiceConnector_InactiveUsersService_InactiveUsersControllerService;
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
        public event getInactiveUsersCompletedEventHandler getInactiveUsersCompleted;
        
        /// <remarks/>
        public event resetUserCompletedEventHandler resetUserCompleted;
        
        /// <remarks/>
        public event deleteUserCompletedEventHandler deleteUserCompleted;
        
        /// <remarks/>
        public event getSessionKeyCompletedEventHandler getSessionKeyCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:InactiveUsersControllerwsdl#getInactiveUsers", RequestNamespace="urn:InactiveUsersControllerwsdl", ResponseNamespace="urn:InactiveUsersControllerwsdl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public UserViewModel[] getInactiveUsers(string sessionKey, string dateStart, string dateEnd) {
            object[] results = this.Invoke("getInactiveUsers", new object[] {
                        sessionKey,
                        dateStart,
                        dateEnd});
            return ((UserViewModel[])(results[0]));
        }
        
        /// <remarks/>
        public void getInactiveUsersAsync(string sessionKey, string dateStart, string dateEnd) {
            this.getInactiveUsersAsync(sessionKey, dateStart, dateEnd, null);
        }
        
        /// <remarks/>
        public void getInactiveUsersAsync(string sessionKey, string dateStart, string dateEnd, object userState) {
            if ((this.getInactiveUsersOperationCompleted == null)) {
                this.getInactiveUsersOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetInactiveUsersOperationCompleted);
            }
            this.InvokeAsync("getInactiveUsers", new object[] {
                        sessionKey,
                        dateStart,
                        dateEnd}, this.getInactiveUsersOperationCompleted, userState);
        }
        
        private void OngetInactiveUsersOperationCompleted(object arg) {
            if ((this.getInactiveUsersCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getInactiveUsersCompleted(this, new getInactiveUsersCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:InactiveUsersControllerwsdl#resetUser", RequestNamespace="urn:InactiveUsersControllerwsdl", ResponseNamespace="urn:InactiveUsersControllerwsdl")]
        public void resetUser(string sessionKey, string login, string password) {
            this.Invoke("resetUser", new object[] {
                        sessionKey,
                        login,
                        password});
        }
        
        /// <remarks/>
        public void resetUserAsync(string sessionKey, string login, string password) {
            this.resetUserAsync(sessionKey, login, password, null);
        }
        
        /// <remarks/>
        public void resetUserAsync(string sessionKey, string login, string password, object userState) {
            if ((this.resetUserOperationCompleted == null)) {
                this.resetUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OnresetUserOperationCompleted);
            }
            this.InvokeAsync("resetUser", new object[] {
                        sessionKey,
                        login,
                        password}, this.resetUserOperationCompleted, userState);
        }
        
        private void OnresetUserOperationCompleted(object arg) {
            if ((this.resetUserCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.resetUserCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:InactiveUsersControllerwsdl#deleteUser", RequestNamespace="urn:InactiveUsersControllerwsdl", ResponseNamespace="urn:InactiveUsersControllerwsdl")]
        public void deleteUser(string sessionKey, string login) {
            this.Invoke("deleteUser", new object[] {
                        sessionKey,
                        login});
        }
        
        /// <remarks/>
        public void deleteUserAsync(string sessionKey, string login) {
            this.deleteUserAsync(sessionKey, login, null);
        }
        
        /// <remarks/>
        public void deleteUserAsync(string sessionKey, string login, object userState) {
            if ((this.deleteUserOperationCompleted == null)) {
                this.deleteUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OndeleteUserOperationCompleted);
            }
            this.InvokeAsync("deleteUser", new object[] {
                        sessionKey,
                        login}, this.deleteUserOperationCompleted, userState);
        }
        
        private void OndeleteUserOperationCompleted(object arg) {
            if ((this.deleteUserCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.deleteUserCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:InactiveUsersControllerwsdl#getSessionKey", RequestNamespace="urn:InactiveUsersControllerwsdl", ResponseNamespace="urn:InactiveUsersControllerwsdl")]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3190.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:InactiveUsersControllerwsdl")]
    public partial class UserViewModel {
        
        private int idField;
        
        private string loginField;
        
        private string passwordField;
        
        private string firstNameField;
        
        private string lastNameField;
        
        private string emailField;
        
        private string phoneField;
        
        private int roleField;
        
        private string dateAddField;
        
        private string dateModifyField;
        
        private string dateLastActivityField;
        
        private string[] assignedGroupsField;
        
        private bool allGroupsField;
        
        private string[] assignedLibrariesField;
        
        private bool allLibrariesField;
        
        /// <remarks/>
        public int id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
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
        public string password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
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
        public string phone {
            get {
                return this.phoneField;
            }
            set {
                this.phoneField = value;
            }
        }
        
        /// <remarks/>
        public int role {
            get {
                return this.roleField;
            }
            set {
                this.roleField = value;
            }
        }
        
        /// <remarks/>
        public string dateAdd {
            get {
                return this.dateAddField;
            }
            set {
                this.dateAddField = value;
            }
        }
        
        /// <remarks/>
        public string dateModify {
            get {
                return this.dateModifyField;
            }
            set {
                this.dateModifyField = value;
            }
        }
        
        /// <remarks/>
        public string dateLastActivity {
            get {
                return this.dateLastActivityField;
            }
            set {
                this.dateLastActivityField = value;
            }
        }
        
        /// <remarks/>
        public string[] assignedGroups {
            get {
                return this.assignedGroupsField;
            }
            set {
                this.assignedGroupsField = value;
            }
        }
        
        /// <remarks/>
        public bool allGroups {
            get {
                return this.allGroupsField;
            }
            set {
                this.allGroupsField = value;
            }
        }
        
        /// <remarks/>
        public string[] assignedLibraries {
            get {
                return this.assignedLibrariesField;
            }
            set {
                this.assignedLibrariesField = value;
            }
        }
        
        /// <remarks/>
        public bool allLibraries {
            get {
                return this.allLibrariesField;
            }
            set {
                this.allLibrariesField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    public delegate void getInactiveUsersCompletedEventHandler(object sender, getInactiveUsersCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getInactiveUsersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getInactiveUsersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public UserViewModel[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((UserViewModel[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    public delegate void resetUserCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    public delegate void deleteUserCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    public delegate void getSessionKeyCompletedEventHandler(object sender, getSessionKeyCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
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