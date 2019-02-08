using System;
using System.Collections.Generic;
using SalesLibraries.ServiceConnector.AdminService;

namespace SalesLibraries.ServiceConnector.Services.Soap
{
    public partial class SoapServiceConnection
    {
        public IEnumerable<LibraryViewModel> GetLibraries(out string message)
        {
            message = string.Empty;
            var libraries = new List<LibraryViewModel>();
            var client = GetAdminClient();
            if (client != null)
            {
                try
                {
                    var sessionKey = client.getSessionKey(Login, Password);
                    if (!string.IsNullOrEmpty(sessionKey))
                        libraries.AddRange(client.getLibraries(sessionKey) ?? new LibraryViewModel[] { });
                    else
                        message = "Couldn't complete operation.\nLogin or password are not correct.";
                }
                catch (Exception ex)
                {
                    message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
                }
            }
            else
                message = "Couldn't complete operation.\nServer is unavailable.";
            return libraries.ToArray();
        }

        public SoapLibraryPage GetLibraryPage(string pageId, out string message)
        {
            message = string.Empty;
            SoapLibraryPage libraryPage = null;
            var client = GetAdminClient();
            if (client != null)
            {
                try
                {
                    var sessionKey = client.getSessionKey(Login, Password);
                    if (!string.IsNullOrEmpty(sessionKey))
                        libraryPage = client.getLibraryPage(sessionKey, pageId);
                    else
                        message = "Couldn't complete operation.\nLogin or password are not correct.";
                }
                catch (Exception ex)
                {
                    message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
                }
            }
            else
                message = "Couldn't complete operation.\nServer is unavailable.";
            return libraryPage;
        }

        public void SetPage(string id, UserViewModel[] users, GroupViewModel[] groups, out string message)
        {
            message = string.Empty;
            var client = GetAdminClient();
            if (client != null)
            {
                try
                {
                    var sessionKey = client.getSessionKey(Login, Password);
                    if (!string.IsNullOrEmpty(sessionKey))
                        client.setPage(sessionKey, id, users, groups);
                    else
                        message = "Couldn't complete operation.\nLogin or password are not correct.";
                }
                catch (Exception ex)
                {
                    message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
                }
            }
            else
                message = "Couldn't complete operation.\nServer is unavailable.";
        }
    }
}
