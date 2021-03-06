﻿using System;
using System.Collections.Generic;
using SalesLibraries.ServiceConnector.QBuilderService;

namespace SalesLibraries.ServiceConnector.Services.Soap
{
	public partial class SoapServiceConnection
	{
		private QBuilderControllerService GetQBuilderClient()
		{
			try
			{
				var client = new QBuilderControllerService();
				client.Url = string.Format("{0}/QBuilder/quote?ws=1", Website);
				return client;
			}
			catch
			{
				return null;
			}
		}

		public QPageModel[] GetAllPages(DateTime startDate, DateTime endDate, bool filterByViewDate, out string message)
		{
			message = string.Empty;
			var pages = new List<QPageModel>();
			var client = GetQBuilderClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
					{
						while (startDate < endDate)
						{
							var nextDate = startDate.AddDays(30);
							if (nextDate > endDate)
								nextDate = endDate;
							pages.AddRange(client.getAllPages(sessionKey, startDate.ToString("MM/dd/yyyy hh:mm tt"), nextDate.ToString("MM/dd/yyyy hh:mm tt"), filterByViewDate) ?? new QPageModel[] { });
							startDate = nextDate;
						}
					}
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

		public void DeletePages(string[] pageIds, out string message)
		{
			message = string.Empty;
			var client = GetQBuilderClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
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
	}
}
