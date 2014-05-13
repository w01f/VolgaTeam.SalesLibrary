using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using SalesDepot.CoreObjects.InteropClasses;
using SalesDepot.Services.TickerService;
using SalesDepot.SiteManager.BusinessClasses;
using SalesDepot.SiteManager.ToolForms;

namespace SalesDepot.SiteManager.PresentationClasses.Ticker
{
	[ToolboxItem(false)]
	public partial class TickerManagerControl : UserControl
	{
		private readonly List<TickerLink> _records = new List<TickerLink>();

		public TickerManagerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		private void UpdateOrder()
		{
			var i = 0;
			var j = 1;
			foreach (var tickerLink in _records.OrderBy(x => x.order))
			{
				tickerLink.order = i;
				tickerLink.UserOrder = j;
				i += 2;
				j++;
			}
			gridViewTicker.RefreshData();
		}

		private void SetData(ref string updateMessage)
		{
			var message = string.Empty;
			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = "Updating data...";
				form.TopMost = true;
				var thread = new Thread(() => WebSiteManager.Instance.SelectedSite.SetTickerLinks(_records.ToArray(), out message));
				form.Show();
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
				if (string.IsNullOrEmpty(message))
					RefreshData(false);
				form.Close();
				Enabled = true;
				FormMain.Instance.ribbonControl.Enabled = true;
			}
			updateMessage = message;
		}

		public void RefreshData(bool showMessages)
		{
			gridControlTicker.DataSource = null;
			_records.Clear();
			var message = string.Empty;
			if (showMessages)
			{
				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Loading data...";
					form.TopMost = true;
					var thread = new Thread(() => _records.AddRange(WebSiteManager.Instance.SelectedSite.GetTickerLinks(out message)));
					form.Show();
					thread.Start();
					while (thread.IsAlive)
					{
						Thread.Sleep(100);
						Application.DoEvents();
					}
					form.Close();
					Enabled = true;
					FormMain.Instance.ribbonControl.Enabled = true;
				}
				if (!string.IsNullOrEmpty(message))
					AppManager.Instance.ShowWarning(message);
			}
			else
			{
				var thread = new Thread(() => _records.AddRange(WebSiteManager.Instance.SelectedSite.GetTickerLinks(out message)));
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			UpdateOrder();
			gridControlTicker.DataSource = _records;
			gridViewTicker.RefreshData();
		}

		public void AddObject()
		{
			var message = string.Empty;
			using (var formEdit = new FormEditTickerLink(true, new TickerLink()))
			{
				if (formEdit.ShowDialog() == DialogResult.OK)
				{
					var link = formEdit.Link;
					_records.Add(link);
					SetData(ref message);
				}
			}
			if (!string.IsNullOrEmpty(message))
				AppManager.Instance.ShowWarning(message);
		}

		public void EditObject()
		{
			var selectedLink = gridViewTicker.GetFocusedRow() as TickerLink;
			if (selectedLink == null) return;
			var message = string.Empty;
			using (var formEdit = new FormEditTickerLink(false, selectedLink))
			{
				if (formEdit.ShowDialog() == DialogResult.OK)
					SetData(ref message);
			}
			if (!string.IsNullOrEmpty(message))
				AppManager.Instance.ShowWarning(message);
		}

		public void DeleteObject()
		{
			var selectedLink = gridViewTicker.GetFocusedRow() as TickerLink;
			if (selectedLink == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Do you realy want to delete link?") == DialogResult.Yes)
			{
				var message = string.Empty;
				_records.Remove(selectedLink);
				SetData(ref message);
				if (!string.IsNullOrEmpty(message))
					AppManager.Instance.ShowWarning(message);
			}
		}

		public void ExportUsers()
		{
			using (var dialog = new SaveFileDialog())
			{
				dialog.Title = "Export Links";
				dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
				dialog.Filter = "Excel files|*.xlsx";
				dialog.FileName = "TickerLinks.xlsx";
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					var tickerLinks = new string[_records.Count, 8];
					for (var i = 0; i < _records.Count; i++)
					{
						tickerLinks[i, 0] = _records[i].TypeString;
						tickerLinks[i, 1] = _records[i].text;
						tickerLinks[i, 2] = _records[i].type.Equals("url") ? _records[i].GetDataByTag("path") : null;
						tickerLinks[i, 3] = _records[i].type.Equals("file") ? _records[i].GetDataByTag("path") : null;
						tickerLinks[i, 4] = _records[i].type.Equals("video") ? _records[i].GetDataByTag("path") : null;
						tickerLinks[i, 5] = _records[i].type.Equals("link") ? _records[i].GetDataByTag("library") : null;
						tickerLinks[i, 6] = _records[i].type.Equals("link") ? _records[i].GetDataByTag("page") : null;
						tickerLinks[i, 7] = _records[i].type.Equals("link") ? _records[i].GetDataByTag("link") : null;
					}
					ExcelHelper.Instance.ExportTickerLinks(dialog.FileName, tickerLinks);
					try
					{
						Process.Start(dialog.FileName);
					}
					catch { }
				}
			}
		}

		public void ImportUsers()
		{
			var message = string.Empty;
			using (var dialog = new OpenFileDialog())
			{
				dialog.Title = "Import Links";
				dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
				dialog.Filter = "Excel files|*.xlsx";
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					using (var form = new FormProgress())
					{
						FormMain.Instance.ribbonControl.Enabled = false;
						Enabled = false;
						form.laProgress.Text = "Import links...";
						form.TopMost = true;
						var thread = new Thread(() =>
						{
							var tickerLinks = ImportManager.ImportTickers(dialog.FileName, out message);
							if (!string.IsNullOrEmpty(message)) return;
							_records.Clear();
							_records.AddRange(tickerLinks);
						});
						form.Show();
						thread.Start();
						while (thread.IsAlive)
						{
							Thread.Sleep(100);
							Application.DoEvents();
						}
						form.Close();
						Enabled = true;
						FormMain.Instance.ribbonControl.Enabled = true;
					}
					if (string.IsNullOrEmpty(message))
						SetData(ref message);
				}
			}
			if (!string.IsNullOrEmpty(message))
				AppManager.Instance.ShowWarning(message);
		}

		private void repositoryItemButtonEditUsersActions_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (gridViewTicker.FocusedRowHandle != GridControl.InvalidRowHandle)
			{
				if (e.Button.Index == 0)
					EditObject();
				else if (e.Button.Index == 1)
					DeleteObject();
			}
		}

		private void repositoryItemButtonEditTickerOrder_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			var link = gridViewTicker.GetFocusedRow() as TickerLink;
			if (link == null) return;
			var focussedIndex = gridViewTicker.FocusedRowHandle;
			switch (e.Button.Index)
			{
				case 0:
					link.order -= 3;
					focussedIndex--;
					break;
				case 1:
					link.order += 3;
					focussedIndex++;
					break;
			}
			UpdateOrder();
			var message = string.Empty;
			SetData(ref message);
			if (!string.IsNullOrEmpty(message))
				AppManager.Instance.ShowWarning(message);
			if (focussedIndex < 0)
				focussedIndex = 0;
			if (focussedIndex >= gridViewTicker.RowCount)
				focussedIndex = gridViewTicker.RowCount - 1;
			if (focussedIndex != GridControl.InvalidRowHandle)
				gridViewTicker.FocusedRowHandle = focussedIndex;
		}
	}
}