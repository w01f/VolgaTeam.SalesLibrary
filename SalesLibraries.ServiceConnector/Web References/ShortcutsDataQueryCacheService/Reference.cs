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

namespace SalesLibraries.ServiceConnector.ShortcutsDataQueryCacheService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ShortcutsDataQueryCacheControllerBinding", Namespace="urn:ShortcutsDataQueryCacheControllerwsdl")]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(SoapShortcutModel))]
    public partial class ShortcutsDataQueryCacheControllerService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback getLandingPagesOperationCompleted;
        
        private System.Threading.SendOrPostCallback getProfilesOperationCompleted;
        
        private System.Threading.SendOrPostCallback saveProfileOperationCompleted;
        
        private System.Threading.SendOrPostCallback deleteProfileOperationCompleted;
        
        private System.Threading.SendOrPostCallback resetDataQueryCacheOperationCompleted;
        
        private System.Threading.SendOrPostCallback getSessionKeyOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ShortcutsDataQueryCacheControllerService() {
            this.Url = global::SalesLibraries.ServiceConnector.Properties.Settings.Default.SalesLibraries_ServiceConnector_ShortcutsDataQueryCacheService_ShortcutsDataQueryCacheControllerService;
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
        public event getLandingPagesCompletedEventHandler getLandingPagesCompleted;
        
        /// <remarks/>
        public event getProfilesCompletedEventHandler getProfilesCompleted;
        
        /// <remarks/>
        public event saveProfileCompletedEventHandler saveProfileCompleted;
        
        /// <remarks/>
        public event deleteProfileCompletedEventHandler deleteProfileCompleted;
        
        /// <remarks/>
        public event resetDataQueryCacheCompletedEventHandler resetDataQueryCacheCompleted;
        
        /// <remarks/>
        public event getSessionKeyCompletedEventHandler getSessionKeyCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:ShortcutsDataQueryCacheControllerwsdl#getLandingPages", RequestNamespace="urn:ShortcutsDataQueryCacheControllerwsdl", ResponseNamespace="urn:ShortcutsDataQueryCacheControllerwsdl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public SoapShortcutModel[] getLandingPages(string sessionKey) {
            object[] results = this.Invoke("getLandingPages", new object[] {
                        sessionKey});
            return ((SoapShortcutModel[])(results[0]));
        }
        
        /// <remarks/>
        public void getLandingPagesAsync(string sessionKey) {
            this.getLandingPagesAsync(sessionKey, null);
        }
        
        /// <remarks/>
        public void getLandingPagesAsync(string sessionKey, object userState) {
            if ((this.getLandingPagesOperationCompleted == null)) {
                this.getLandingPagesOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetLandingPagesOperationCompleted);
            }
            this.InvokeAsync("getLandingPages", new object[] {
                        sessionKey}, this.getLandingPagesOperationCompleted, userState);
        }
        
        private void OngetLandingPagesOperationCompleted(object arg) {
            if ((this.getLandingPagesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getLandingPagesCompleted(this, new getLandingPagesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:ShortcutsDataQueryCacheControllerwsdl#getProfiles", RequestNamespace="urn:ShortcutsDataQueryCacheControllerwsdl", ResponseNamespace="urn:ShortcutsDataQueryCacheControllerwsdl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public ShortcutDataQueryCacheServiceProfile[] getProfiles(string sessionKey) {
            object[] results = this.Invoke("getProfiles", new object[] {
                        sessionKey});
            return ((ShortcutDataQueryCacheServiceProfile[])(results[0]));
        }
        
        /// <remarks/>
        public void getProfilesAsync(string sessionKey) {
            this.getProfilesAsync(sessionKey, null);
        }
        
        /// <remarks/>
        public void getProfilesAsync(string sessionKey, object userState) {
            if ((this.getProfilesOperationCompleted == null)) {
                this.getProfilesOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetProfilesOperationCompleted);
            }
            this.InvokeAsync("getProfiles", new object[] {
                        sessionKey}, this.getProfilesOperationCompleted, userState);
        }
        
        private void OngetProfilesOperationCompleted(object arg) {
            if ((this.getProfilesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getProfilesCompleted(this, new getProfilesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:ShortcutsDataQueryCacheControllerwsdl#saveProfile", RequestNamespace="urn:ShortcutsDataQueryCacheControllerwsdl", ResponseNamespace="urn:ShortcutsDataQueryCacheControllerwsdl")]
        public void saveProfile(string sessionKey, ShortcutDataQueryCacheServiceProfile profile) {
            this.Invoke("saveProfile", new object[] {
                        sessionKey,
                        profile});
        }
        
        /// <remarks/>
        public void saveProfileAsync(string sessionKey, ShortcutDataQueryCacheServiceProfile profile) {
            this.saveProfileAsync(sessionKey, profile, null);
        }
        
        /// <remarks/>
        public void saveProfileAsync(string sessionKey, ShortcutDataQueryCacheServiceProfile profile, object userState) {
            if ((this.saveProfileOperationCompleted == null)) {
                this.saveProfileOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsaveProfileOperationCompleted);
            }
            this.InvokeAsync("saveProfile", new object[] {
                        sessionKey,
                        profile}, this.saveProfileOperationCompleted, userState);
        }
        
        private void OnsaveProfileOperationCompleted(object arg) {
            if ((this.saveProfileCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.saveProfileCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:ShortcutsDataQueryCacheControllerwsdl#deleteProfile", RequestNamespace="urn:ShortcutsDataQueryCacheControllerwsdl", ResponseNamespace="urn:ShortcutsDataQueryCacheControllerwsdl")]
        public void deleteProfile(string sessionKey, ShortcutDataQueryCacheServiceProfile profile) {
            this.Invoke("deleteProfile", new object[] {
                        sessionKey,
                        profile});
        }
        
        /// <remarks/>
        public void deleteProfileAsync(string sessionKey, ShortcutDataQueryCacheServiceProfile profile) {
            this.deleteProfileAsync(sessionKey, profile, null);
        }
        
        /// <remarks/>
        public void deleteProfileAsync(string sessionKey, ShortcutDataQueryCacheServiceProfile profile, object userState) {
            if ((this.deleteProfileOperationCompleted == null)) {
                this.deleteProfileOperationCompleted = new System.Threading.SendOrPostCallback(this.OndeleteProfileOperationCompleted);
            }
            this.InvokeAsync("deleteProfile", new object[] {
                        sessionKey,
                        profile}, this.deleteProfileOperationCompleted, userState);
        }
        
        private void OndeleteProfileOperationCompleted(object arg) {
            if ((this.deleteProfileCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.deleteProfileCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:ShortcutsDataQueryCacheControllerwsdl#resetDataQueryCache", RequestNamespace="urn:ShortcutsDataQueryCacheControllerwsdl", ResponseNamespace="urn:ShortcutsDataQueryCacheControllerwsdl")]
        public void resetDataQueryCache(string sessionKey, string landingPageId) {
            this.Invoke("resetDataQueryCache", new object[] {
                        sessionKey,
                        landingPageId});
        }
        
        /// <remarks/>
        public void resetDataQueryCacheAsync(string sessionKey, string landingPageId) {
            this.resetDataQueryCacheAsync(sessionKey, landingPageId, null);
        }
        
        /// <remarks/>
        public void resetDataQueryCacheAsync(string sessionKey, string landingPageId, object userState) {
            if ((this.resetDataQueryCacheOperationCompleted == null)) {
                this.resetDataQueryCacheOperationCompleted = new System.Threading.SendOrPostCallback(this.OnresetDataQueryCacheOperationCompleted);
            }
            this.InvokeAsync("resetDataQueryCache", new object[] {
                        sessionKey,
                        landingPageId}, this.resetDataQueryCacheOperationCompleted, userState);
        }
        
        private void OnresetDataQueryCacheOperationCompleted(object arg) {
            if ((this.resetDataQueryCacheCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.resetDataQueryCacheCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:ShortcutsDataQueryCacheControllerwsdl#getSessionKey", RequestNamespace="urn:ShortcutsDataQueryCacheControllerwsdl", ResponseNamespace="urn:ShortcutsDataQueryCacheControllerwsdl")]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:ShortcutsDataQueryCacheControllerwsdl")]
    public partial class SoapShortcutModel {
        
        private string idField;
        
        private string titleField;
        
        private string descriptionField;
        
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
        public string description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:ShortcutsDataQueryCacheControllerwsdl")]
    public partial class ShortcutDataQueryCacheServiceProfile {
        
        private string[] shortcutIdsField;
        
        private string idField;
        
        private string nameField;
        
        private string typeField;
        
        /// <remarks/>
        public string[] shortcutIds {
            get {
                return this.shortcutIdsField;
            }
            set {
                this.shortcutIdsField = value;
            }
        }
        
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
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public string type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void getLandingPagesCompletedEventHandler(object sender, getLandingPagesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getLandingPagesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getLandingPagesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public SoapShortcutModel[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((SoapShortcutModel[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void getProfilesCompletedEventHandler(object sender, getProfilesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getProfilesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getProfilesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ShortcutDataQueryCacheServiceProfile[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ShortcutDataQueryCacheServiceProfile[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void saveProfileCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void deleteProfileCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void resetDataQueryCacheCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void getSessionKeyCompletedEventHandler(object sender, getSessionKeyCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
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