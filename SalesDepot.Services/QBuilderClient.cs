﻿using System;
using System.Collections.Generic;
using SalesDepot.Services.QBuilderService;

namespace SalesDepot.Services
{
	public partial class SiteClient
	{
		private QbuilderControllerService GetQBuilderClient()
		{
			try
			{
				var client = new QbuilderControllerService();
				client.Url = string.Format("{0}/qbuilder/quote?ws=1", _website);
				return client;
			}
			catch
			{
				return null;
			}
		}

		public bool Login(out string message)
		{
			message = string.Empty;
			var client = GetQBuilderClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					return !String.IsNullOrEmpty(sessionKey);
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
			return false;
		}

		public IEnumerable<string> GetPageLogos(out string message)
		{
			message = string.Empty;
			var pageLogos = new List<string>();
			var client = GetQBuilderClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						pageLogos.AddRange(client.getPageLogos(sessionKey) ?? new string[] { });
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
			return pageLogos.ToArray();
		}

		public QPageModel[] GetAllPages(out string message)
		{
			message = string.Empty;
			var pages = new List<QPageModel>();
			var client = GetQBuilderClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						pages.AddRange(client.getAllPages(sessionKey) ?? new QPageModel[] { });
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
			return pages.ToArray();
		}

		public QPageModel[] GetPagesByUser(out string message)
		{
			message = string.Empty;
			var pages = new List<QPageModel>();
			var client = GetQBuilderClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						pages.AddRange(client.getPagesByUser(sessionKey, _login) ?? new QPageModel[] { });
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
			return pages.ToArray();
		}

		public QPageLinkModel[] GetLinkCart(out string message)
		{
			message = string.Empty;
			var links = new List<QPageLinkModel>();
			var client = GetQBuilderClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						links.AddRange(client.getLinkCart(sessionKey, _login) ?? new QPageLinkModel[] { });
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
			return links.ToArray();
		}

		public bool IsLinkAvailableOnSite(string linkId, out string message)
		{
			message = string.Empty;
			var client = GetQBuilderClient();
			var result = false;
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						result = client.isLinkAvailable(sessionKey, linkId);
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
			return result;
		}

		public void AddLinkToCart(string linkId, out string message)
		{
			message = string.Empty;
			var client = GetQBuilderClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.addLinkToCart(sessionKey, _login, linkId, null);
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

		public bool IsFolderAvailableOnSite(string folderId, out string message)
		{
			message = string.Empty;
			var client = GetQBuilderClient();
			var result = false;
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						result = client.isFolderAvailable(sessionKey, folderId);
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
			return result;
		}

		public void AddFolderToCart(string folderId, out string message)
		{
			message = string.Empty;
			var client = GetQBuilderClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.addLinkToCart(sessionKey, _login, null, folderId);
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

		public void AddLinksToPage(string[] linkIds, string pageId, int firstLinkIndex, out string message)
		{
			message = string.Empty;
			var client = GetQBuilderClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.addLinksToPage(sessionKey, linkIds, pageId, firstLinkIndex);
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

		public void SetPageLinkOrder(string linkId, string pageId, int linkIndex, out string message)
		{
			message = string.Empty;
			var client = GetQBuilderClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.setPageLinkOrder(sessionKey, pageId, linkId, linkIndex);
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

		public QPageModel GetPageContent(string pageId, out string message)
		{
			message = string.Empty;
			var client = GetQBuilderClient();
			QPageModel result = null;
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						result = client.getPageContent(sessionKey, pageId);
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
			return result;
		}

		public void SavePageContent(string pageId, string title, string description, string header, string footer, string expirationDate, bool restricted, bool disableBanners, bool disableWidgets, bool showLinsAsUrl, bool recordActivity, string pinCode, string activityEmailCopy, string logo, out string message)
		{
			message = String.Empty;
			var client = GetQBuilderClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.savePageContent(sessionKey, pageId, title, description, header, footer, expirationDate, restricted, disableBanners, disableWidgets, showLinsAsUrl, recordActivity, pinCode, activityEmailCopy, logo);
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

		public string AddPage(string title, out string message)
		{
			message = string.Empty;
			var client = GetQBuilderClient();
			var pageId = String.Empty;
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						pageId = client.addPage(sessionKey, _login, title, DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
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
			return pageId;
		}

		public string ClonePage(string clonedPageId, string title, out string message)
		{
			message = string.Empty;
			var client = GetQBuilderClient();
			var pageId = String.Empty;
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						pageId = client.clonePage(sessionKey, _login, clonedPageId, title, DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
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
			return pageId;
		}

		public string EmailWebLink(string linkId, string title, int expiresInDays, bool restricted, string logo, bool disableBanners, bool disableWidgets, bool showLinksAsUrl, bool recordActivity, string pinCode, string activityEmailCopy, out string message)
		{
			message = String.Empty;
			var result = String.Empty;
			var client = GetQBuilderClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						result = client.emailLink(sessionKey, _login, linkId, title, DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"), expiresInDays, restricted, logo, disableBanners, disableWidgets, showLinksAsUrl, recordActivity, pinCode, activityEmailCopy);
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
			return result;
		}

		public void DeleteLinkFromCart(string linkInCartId, out string message)
		{
			message = string.Empty;
			var client = GetQBuilderClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.deleteLinkFromCart(sessionKey, linkInCartId);
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

		public void DeleteAllFromCart(out string message)
		{
			message = string.Empty;
			var client = GetQBuilderClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.deleteAllFromCart(sessionKey, _login);
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

		public void DeleteLinkFromPage(string linkInPageId, out string message)
		{
			message = string.Empty;
			var client = GetQBuilderClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.deleteLinkFromPage(sessionKey, linkInPageId);
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

		public void DeletePages(string[] pageIds, out string message)
		{
			message = string.Empty;
			var client = GetQBuilderClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.deletePages(sessionKey, pageIds);
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

		public void SetPageOrder(string pageId, int pageIndex, out string message)
		{
			message = string.Empty;
			var client = GetQBuilderClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.setPageOrder(sessionKey, _login, pageId, pageIndex);
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
