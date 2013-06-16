using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SalesDepot.Services;
using SalesDepot.Services.QBuilderService;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class QBuilder
	{
		private static QBuilder _instance;
		private QBuilder()
		{
			Logos = new List<PageLogo>();
			Pages = new List<QPageRecord>();
		}
		public static QBuilder Instance
		{
			get
			{
				if (_instance == null)
					_instance = new QBuilder();
				return _instance;
			}
		}

		#region Connection Processing
		public Connection Connection { get; private set; }
		public bool Connected { get; set; }
		public event EventHandler<ConnectionChangedArgs> ConnectionChanged;

		public bool Login(string host, string user, string password, bool savePassword)
		{
			if (Connected)
			{
				Connected = false;
				Connection = null;
			}

			Connection = new Connection(host, user, password, savePassword);
			var message = String.Empty;
			var thread = new Thread(delegate()
			{
				Connected = Connection.Client.Login(out message);
			});
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
			if (Connected)
			{
				if (ConnectionChanged != null)
					ConnectionChanged(this, new ConnectionChangedArgs(Connection));
			}
			else
				Connection = null;
			return Connected;
		}

		public void Logout()
		{
			Connected = false;
			Connection = null;
			Logos.Clear();
			SelectedPageId = null;
			Pages.Clear();
			if (ConnectionChanged != null)
				ConnectionChanged(this, new ConnectionChangedArgs(Connection));
		}
		#endregion

		#region Link Cart Processing
		public event EventHandler<EventArgs> LinkCartChanged;
		public IEnumerable<QPageLinkRecord> GetLinkCart()
		{
			var message = String.Empty;
			var result = new List<QPageLinkRecord>();
			if (Connected)
			{
				var thread = new Thread(delegate()
											{
												result.AddRange(Connection.Client.GetLinkCart(out message));
											});
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
			}
			return result;
		}

		public bool AddLinkToCart(string linkId)
		{
			var message = String.Empty;
			var result = true;
			var thread = new Thread(delegate()
										{
											result = Connection.Client.IsLinkAvailableOnSite(linkId, out message);
											if (result)
												Connection.Client.AddLinkToCart(linkId, out message);
											result &= String.IsNullOrEmpty(message);
										});
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
			if (result)
			{
				if (LinkCartChanged != null)
					LinkCartChanged(this, new EventArgs());
			}
			return result;
		}

		public bool AddFolderToCart(string folderId)
		{
			var message = String.Empty;
			var result = true;
			var thread = new Thread(delegate()
			{
				result = Connection.Client.IsFolderAvailableOnSite(folderId, out message);
				if (result)
					Connection.Client.AddFolderToCart(folderId, out message);
				result &= String.IsNullOrEmpty(message);
			});
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
			if (result)
			{
				if (LinkCartChanged != null)
					LinkCartChanged(this, new EventArgs());
			}
			return result;
		}

		public bool DeleteLinkFromCart(string linkId)
		{
			var message = String.Empty;
			var result = true;
			var thread = new Thread(delegate()
			{
				Connection.Client.DeleteLinkFromCart(linkId, out message);
				result &= String.IsNullOrEmpty(message);
			});
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
			return result;
		}

		public bool DeleteAllFromCart()
		{
			var message = String.Empty;
			var result = true;
			var thread = new Thread(delegate()
			{
				Connection.Client.DeleteAllFromCart(out message);
				result &= String.IsNullOrEmpty(message);
			});
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
			return result;
		}
		#endregion

		#region Page List Processing
		public List<QPageRecord> Pages { get; private set; }
		public string SelectedPageId { get; private set; }
		public event EventHandler<EventArgs> PageListChanged;

		public QPageRecord SelectedPage
		{
			get { return Pages.FirstOrDefault(p => p.id.Equals(SelectedPageId)); }
		}

		public void GetPageList()
		{
			Pages.Clear();
			var message = String.Empty;
			if (Connected)
			{
				var thread = new Thread(delegate()
				{
					Pages.AddRange(Connection.Client.GetPagesByUser(out message));
				});
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
			}
		}

		public void AddPage(string title)
		{
			var message = String.Empty;
			var result = true;
			var pageId = String.Empty;
			var thread = new Thread(delegate()
			{
				pageId = Connection.Client.AddPage(title, out message);
				result &= String.IsNullOrEmpty(message);
			});
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
			if (result)
			{
				SelectedPageId = pageId;
				if (PageListChanged != null)
					PageListChanged(this, new EventArgs());
			}
		}

		public string AddPageLite(string linkId, string title, int expiresInDays, bool restricted, bool showLinkToMainSite, string logo)
		{
			var message = String.Empty;
			var result = false;
			var url = String.Empty;
			var thread = new Thread(delegate()
										{
											result = Connection.Client.IsLinkAvailableOnSite(linkId, out message);
											if (result)
												url = Connection.Client.EmailWebLink(linkId, title, expiresInDays, restricted, showLinkToMainSite, logo, out message);
											result &= String.IsNullOrEmpty(message);
										});
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
			if (result)
			{
				SelectedPageId = null;
				if (PageListChanged != null)
					PageListChanged(this, new EventArgs());
			}
			return url;
		}

		public void ClonePage(string clonedPageId, string title)
		{
			var message = String.Empty;
			var result = true;
			var pageId = String.Empty;
			var thread = new Thread(delegate()
			{
				pageId = Connection.Client.ClonePage(clonedPageId, title, out message);
				result &= String.IsNullOrEmpty(message);
			});
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
			if (result)
			{
				SelectedPageId = pageId;
				if (PageListChanged != null)
					PageListChanged(this, new EventArgs());
			}
		}

		public void DeletePages(string[] pagesIds)
		{
			var message = String.Empty;
			var result = true;
			var thread = new Thread(delegate()
			{
				Connection.Client.DeletePages(pagesIds, out message);
				result &= String.IsNullOrEmpty(message);
			});
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
			if (result)
			{
				SelectedPageId = null;
				if (PageListChanged != null)
					PageListChanged(this, new EventArgs());
			}
		}
		#endregion

		#region Page Content Processing
		public List<PageLogo> Logos { get; private set; }
		public event EventHandler<EventArgs> PageChanged;

		public void LoadLogos()
		{
			Logos.Clear();
			var message = String.Empty;
			if (Connected)
				Logos.AddRange(Connection.Client.GetPageLogos(out message).Select(logo => new PageLogo(logo)));
		}

		public void LoadPage(string pageId)
		{
			SelectedPageId = pageId;
			if (!(SelectedPage != null && SelectedPage.FullyLoaded))
			{
				var message = String.Empty;
				var result = true;
				QPageRecord page = null;
				var thread = new Thread(delegate()
				{
					page = Connection.Client.GetPageContent(pageId, out message);
					LoadLogos();
					result &= String.IsNullOrEmpty(message);
				});
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				if (result && page != null)
				{
					var pageIndex = Pages.IndexOf(SelectedPage);
					Pages.Remove(SelectedPage);
					Pages.Insert(pageIndex, page);
					page.FullyLoaded = true;
				}
			}
			if (PageChanged != null)
				PageChanged(this, new EventArgs());
		}

		public void SavePage()
		{
			var page = SelectedPage;
			var message = String.Empty;
			var thread = new Thread(delegate()
			{
				Connection.Client.SavePageContent(page.id, page.title, page.subtitle, page.header, page.footer, page.expirationDate, page.isRestricted, page.showLinkMainSite, page.showTicker, page.logo, out message);
			});
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
		}

		public bool AddLinksToPage(string[] linkIds)
		{
			var message = String.Empty;
			var result = true;
			var thread = new Thread(delegate()
			{
				Connection.Client.AddLinksToPage(linkIds, SelectedPageId, out message);
				result &= String.IsNullOrEmpty(message);
			});
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
			if (result)
			{
				SelectedPage.FullyLoaded = false;
				LoadPage(SelectedPageId);
				if (LinkCartChanged != null)
					LinkCartChanged(this, new EventArgs());
			}
			return result;
		}

		public bool DeleteLinkFromPage(string linkId)
		{
			var message = String.Empty;
			var result = true;
			var thread = new Thread(delegate()
			{
				Connection.Client.DeleteLinkFromPage(linkId, out message);
				result &= String.IsNullOrEmpty(message);
			});
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
			return result;
		}
		#endregion
	}

	public class Connection
	{
		public SiteClient Client { get; private set; }
		public bool SavePassword { get; private set; }

		public Connection(string host, string user, string password, bool savePassord)
		{
			SavePassword = savePassord;
			Client = new SiteClient(host, user, password);
		}
	}

	public class ConnectionChangedArgs : EventArgs
	{
		public Connection Connection { get; private set; }

		public ConnectionChangedArgs(Connection connection)
			: base()
		{
			Connection = connection;
		}
	}

	public class PageLogo
	{
		public string EncodedImage { get; private set; }
		public Image Image { get; private set; }

		public PageLogo(string logo)
		{
			EncodedImage = logo;
			Image = String.IsNullOrEmpty(logo) ? null : new Bitmap(new MemoryStream(Convert.FromBase64String(logo)));
		}
	}
}
