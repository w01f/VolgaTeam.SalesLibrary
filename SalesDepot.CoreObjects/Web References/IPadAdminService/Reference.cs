﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.269.
// 
#pragma warning disable 1591

namespace SalesDepot.CoreObjects.IPadAdminService
{
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
    [System.Web.Services.WebServiceBindingAttribute(Name = "AdminControllerBinding", Namespace = "urn:AdminControllerwsdl")]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(UserRecord))]
    public partial class AdminControllerService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback getSessionKeyOperationCompleted;

        private System.Threading.SendOrPostCallback setUserOperationCompleted;

        private System.Threading.SendOrPostCallback deleteUserOperationCompleted;

        private System.Threading.SendOrPostCallback getUsersOperationCompleted;

        private bool useDefaultCredentialsSetExplicitly;

        /// <remarks/>
        public AdminControllerService()
        {
            this.Url = global::SalesDepot.CoreObjects.Properties.Settings.Default.SalesDepot_CoreObjects_IPadAdminService_AdminControllerService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true))
            {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else
            {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        public new string Url
        {
            get
            {
                return base.Url;
            }
            set
            {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true)
                            && (this.useDefaultCredentialsSetExplicitly == false))
                            && (this.IsLocalFileSystemWebService(value) == false)))
                {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }

        public new bool UseDefaultCredentials
        {
            get
            {
                return base.UseDefaultCredentials;
            }
            set
            {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        /// <remarks/>
        public event getSessionKeyCompletedEventHandler getSessionKeyCompleted;

        /// <remarks/>
        public event setUserCompletedEventHandler setUserCompleted;

        /// <remarks/>
        public event deleteUserCompletedEventHandler deleteUserCompleted;

        /// <remarks/>
        public event getUsersCompletedEventHandler getUsersCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:AdminControllerwsdl#getSessionKey", RequestNamespace = "urn:AdminControllerwsdl", ResponseNamespace = "urn:AdminControllerwsdl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public string getSessionKey(string login, string password)
        {
            object[] results = this.Invoke("getSessionKey", new object[] {
                        login,
                        password});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void getSessionKeyAsync(string login, string password)
        {
            this.getSessionKeyAsync(login, password, null);
        }

        /// <remarks/>
        public void getSessionKeyAsync(string login, string password, object userState)
        {
            if ((this.getSessionKeyOperationCompleted == null))
            {
                this.getSessionKeyOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetSessionKeyOperationCompleted);
            }
            this.InvokeAsync("getSessionKey", new object[] {
                        login,
                        password}, this.getSessionKeyOperationCompleted, userState);
        }

        private void OngetSessionKeyOperationCompleted(object arg)
        {
            if ((this.getSessionKeyCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getSessionKeyCompleted(this, new getSessionKeyCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:AdminControllerwsdl#setUser", RequestNamespace = "urn:AdminControllerwsdl", ResponseNamespace = "urn:AdminControllerwsdl")]
        public void setUser(string sessionKey, string login, string password, string firstName, string lastName, string email)
        {
            this.Invoke("setUser", new object[] {
                        sessionKey,
                        login,
                        password,
                        firstName,
                        lastName,
                        email});
        }

        /// <remarks/>
        public void setUserAsync(string sessionKey, string login, string password, string firstName, string lastName, string email)
        {
            this.setUserAsync(sessionKey, login, password, firstName, lastName, email, null);
        }

        /// <remarks/>
        public void setUserAsync(string sessionKey, string login, string password, string firstName, string lastName, string email, object userState)
        {
            if ((this.setUserOperationCompleted == null))
            {
                this.setUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsetUserOperationCompleted);
            }
            this.InvokeAsync("setUser", new object[] {
                        sessionKey,
                        login,
                        password,
                        firstName,
                        lastName,
                        email}, this.setUserOperationCompleted, userState);
        }

        private void OnsetUserOperationCompleted(object arg)
        {
            if ((this.setUserCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.setUserCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:AdminControllerwsdl#deleteUser", RequestNamespace = "urn:AdminControllerwsdl", ResponseNamespace = "urn:AdminControllerwsdl")]
        public void deleteUser(string sessionKey, string login)
        {
            this.Invoke("deleteUser", new object[] {
                        sessionKey,
                        login});
        }

        /// <remarks/>
        public void deleteUserAsync(string sessionKey, string login)
        {
            this.deleteUserAsync(sessionKey, login, null);
        }

        /// <remarks/>
        public void deleteUserAsync(string sessionKey, string login, object userState)
        {
            if ((this.deleteUserOperationCompleted == null))
            {
                this.deleteUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OndeleteUserOperationCompleted);
            }
            this.InvokeAsync("deleteUser", new object[] {
                        sessionKey,
                        login}, this.deleteUserOperationCompleted, userState);
        }

        private void OndeleteUserOperationCompleted(object arg)
        {
            if ((this.deleteUserCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.deleteUserCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:AdminControllerwsdl#getUsers", RequestNamespace = "urn:AdminControllerwsdl", ResponseNamespace = "urn:AdminControllerwsdl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public UserRecord[] getUsers(string sessionKey)
        {
            object[] results = this.Invoke("getUsers", new object[] {
                        sessionKey});
            return ((UserRecord[])(results[0]));
        }

        /// <remarks/>
        public void getUsersAsync(string sessionKey)
        {
            this.getUsersAsync(sessionKey, null);
        }

        /// <remarks/>
        public void getUsersAsync(string sessionKey, object userState)
        {
            if ((this.getUsersOperationCompleted == null))
            {
                this.getUsersOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetUsersOperationCompleted);
            }
            this.InvokeAsync("getUsers", new object[] {
                        sessionKey}, this.getUsersOperationCompleted, userState);
        }

        private void OngetUsersOperationCompleted(object arg)
        {
            if ((this.getUsersCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getUsersCompleted(this, new getUsersCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }

        private bool IsLocalFileSystemWebService(string url)
        {
            if (((url == null)
                        || (url == string.Empty)))
            {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024)
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0)))
            {
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
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:AdminControllerwsdl")]
    public partial class UserRecord
    {

        private string loginField;

        private string passwordField;

        private string firstNameField;

        private string lastNameField;

        private string emailField;

        /// <remarks/>
        public string login
        {
            get
            {
                return this.loginField;
            }
            set
            {
                this.loginField = value;
            }
        }

        /// <remarks/>
        public string password
        {
            get
            {
                return this.passwordField;
            }
            set
            {
                this.passwordField = value;
            }
        }

        /// <remarks/>
        public string firstName
        {
            get
            {
                return this.firstNameField;
            }
            set
            {
                this.firstNameField = value;
            }
        }

        /// <remarks/>
        public string lastName
        {
            get
            {
                return this.lastNameField;
            }
            set
            {
                this.lastNameField = value;
            }
        }

        /// <remarks/>
        public string email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }

        public string FullName
        {
            get
            {
                return (this.firstName + " " + this.lastName).Trim();
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
    public partial class getSessionKeyCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal getSessionKeyCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void setUserCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void deleteUserCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void getUsersCompletedEventHandler(object sender, getUsersCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getUsersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal getUsersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UserRecord[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UserRecord[])(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591