using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using EO.WebBrowser;
using EO.WebBrowser.WinForm;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Business.Gallery;
using SalesLibraries.SalesDepot.Business.Services;
using Vintasoft.Imaging;
using Vintasoft.Imaging.VisualTools;

namespace SalesLibraries.SalesDepot.PresentationLayer.Gallery
{
	[ToolboxItem(false)]
	public abstract partial class GalleryControl : UserControl
	{
		private bool _initialized;
		private WebControl _browser;
		private ImageViewer _imageContainer;
		private int _idCommandDownload;
		private int _idCommandEdit;
		private int _zoomIndex;
		private readonly List<ButtonItem> _browseModes = new List<ButtonItem>();

		protected GalleryControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			InitBrowser();
			InitImageContainer();

			ViewMode.Click += ViewMode_Click;
			ViewMode.CheckedChanged += ViewMode_CheckedChanged;
			EditMode.Click += ViewMode_Click;
			EditMode.CheckedChanged += ViewMode_CheckedChanged;
			SectionsList.EditValueChanged += SectionChanged;
			GroupsList.EditValueChanged += GroupChanged;
			ImageSelect.Click += ImageSelect_Click;
			ImageCrop.Click += ImageCrop_Click;
			ZoomIn.Click += ZoomIn_Click;
			ZoomOut.Click += ZoomOut_Click;
			Copy.Click += Copy_Click;
		}

		#region Controls
		public abstract GalleryManager Manager { get; }
		public abstract RibbonPanel Panel { get; }
		public abstract RibbonBar BrowseBar { get; }
		public abstract RibbonBar ImageBar { get; }
		public abstract RibbonBar ZoomBar { get; }
		public abstract RibbonBar CopyBar { get; }
		public abstract ItemContainer BrowseModeContainer { get; }
		public abstract ButtonItem ViewMode { get; }
		public abstract ButtonItem EditMode { get; }
		public abstract ButtonItem ImageSelect { get; }
		public abstract ButtonItem ImageCrop { get; }
		public abstract ButtonItem ZoomIn { get; }
		public abstract ButtonItem ZoomOut { get; }
		public abstract ButtonItem Copy { get; }
		public abstract ComboBoxEdit SectionsList { get; }
		public abstract ComboBoxEdit GroupsList { get; }
		#endregion

		public void InitControl()
		{
			if (_initialized) return;
			_initialized = true;
			Panel.Visible = false;
			ViewMode_CheckedChanged(null, EventArgs.Empty);
			InitBrowseMode();
			ViewMode_Click(ViewMode, EventArgs.Empty);
			Panel.Visible = true;
			Panel.ResumeLayout(true);
		}

		private void InitBrowseMode()
		{
			_browseModes.Clear();
			BrowseModeContainer.SubItems.Clear();
			foreach (var sourceUrl in Manager.SourceUrls)
			{
				var button = new ButtonItem();
				button.Text = sourceUrl.Name;
				button.Tag = sourceUrl;
				button.Click += BrowseMode_Click;
				button.CheckedChanged += BrowseMode_CheckedChanged;
				_browseModes.Add(button);
				BrowseModeContainer.SubItems.Add(button);
			}
			BrowseModeContainer.Visible = _browseModes.Count > 1;
			if (_browseModes.Count > 2)
				BrowseModeContainer.ItemSpacing = 1;
			if (_browseModes.Any())
				_browseModes.First().Checked = true;
			BrowseBar.RecalcLayout();
			Panel.PerformLayout();
		}

		private void UpdateBrowseMode()
		{
			Navigate(String.Empty);
			LoadImageToEditor(null);
			_browseModes.ForEach(b => b.Enabled = false);
			ShowProgress();
			SectionsList.EditValue = null;
			var selectedMode = _browseModes.FirstOrDefault(b => b.Checked);
			var sections = new List<SnapshotCollection>();
			var backWorker = new BackgroundWorker();
			backWorker.DoWork += (o, e) =>
			{
				sections.AddRange(Manager.GetSnapshots(selectedMode.Tag as GalleryManager.SourceUrl));
			};
			backWorker.RunWorkerCompleted += (o, e) =>
			{
				Application.DoEvents();
				HideProgress();
				_browseModes.ForEach(b => b.Enabled = true);
				SectionsList.Properties.Items.Clear();
				SectionsList.Properties.Items.AddRange(sections);
				SectionsList.Enabled = sections.Any();
				if (Manager.AutoLoad)
					SectionsList.EditValue = sections.FirstOrDefault();
			};
			backWorker.RunWorkerAsync();
		}

		private void InitImageContainer()
		{
			_imageContainer = new ImageViewer();
			Controls.Add(_imageContainer);
			_imageContainer.Dock = DockStyle.Fill;
			var menuItemCopy = new System.Windows.Forms.MenuItem("Copy to Clipboard");
			menuItemCopy.Click += Copy_Click;
			_imageContainer.ContextMenu = new System.Windows.Forms.ContextMenu(new[] { menuItemCopy });
		}

		private void InitBrowser()
		{
			_idCommandDownload = CommandIds.RegisterUserCommand("DownloadImage");
			_idCommandEdit = CommandIds.RegisterUserCommand("EditImage");
			_browser = new WebControl();
			Controls.Add(_browser);
			_browser.WebView = new WebView();
			_browser.Dock = DockStyle.Fill;
			_browser.WebView.LoadCompleted += WebView_LoadComplete;
			_browser.WebView.BeforeContextMenu += WebView_BeforeContextMenu;
			_browser.WebView.Command += WebView_Command;
		}

		private void Navigate(string url)
		{
			ShowProgress();
			BrowseBar.Enabled = false;
			_browser.WebView.LoadUrl(url.Replace(" ", "%20"));
		}

		private Dictionary<string, Image> DownloadImages(IEnumerable<string> urls)
		{
			var images = new Dictionary<string, Image>();
			ShowProgress();
			UseWaitCursor = true;
			Application.DoEvents();
			try
			{
				Application.DoEvents();
				var thread = new Thread(() =>
				{
					foreach (var url in urls)
					{
						var uri = new Uri(url, true);
						images.Add(Path.GetFileNameWithoutExtension(HttpUtility.UrlDecode(uri.Segments[uri.Segments.Length - 1])), Image.FromStream(new MemoryStream(new WebClient().DownloadData(url))));
					}
				});
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
			}
			catch { }
			UseWaitCursor = false;
			HideProgress();
			return images;
		}

		private void LoadImageToEditor(Image image)
		{
			_zoomIndex = 100;
			ChangeTool(null);
			_imageContainer.Image = null;
			if (image != null)
				_imageContainer.Image = new VintasoftImage(image, true);
			EditMode.Enabled = _imageContainer.Image != null;
		}

		private void ChangeTool(VisualTool tool)
		{
			_imageContainer.VisualTool = _imageContainer.VisualTool != null && tool != null && tool.GetType() == _imageContainer.VisualTool.GetType() ? null : tool;
			ImageCrop.Checked = (_imageContainer.VisualTool as CropSelectionTool) != null;
			ImageSelect.Checked = (_imageContainer.VisualTool as RectangularSelectionToolWithCopyPaste) != null;
		}

		private void ViewMode_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonItem;
			if (button == null) return;
			if (button.Checked) return;
			ViewMode.Checked = false;
			EditMode.Checked = false;
			button.Checked = true;
		}

		private void ViewMode_CheckedChanged(object sender, EventArgs e)
		{
			var button = sender as ButtonItem;
			if (button == null) return;
			if (!button.Checked) return;
			BrowseBar.Enabled = false;
			CopyBar.Enabled = false;
			ImageBar.Enabled = false;
			ZoomBar.Enabled = false;
			if (ViewMode.Checked)
			{
				BrowseBar.Enabled = true;
				_browser.BringToFront();
			}
			else if (EditMode.Checked)
			{
				CopyBar.Enabled = true;
				ImageBar.Enabled = true;
				ZoomBar.Enabled = true;
				_imageContainer.BringToFront();
			}
		}

		private void BrowseMode_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonItem;
			if (button == null) return;
			if (button.Checked) return;
			_browseModes.ForEach(b => b.Checked = false);
			button.Checked = true;
		}

		private void BrowseMode_CheckedChanged(object sender, EventArgs e)
		{
			var button = sender as ButtonItem;
			if (button == null) return;
			if (!button.Checked) return;
			UpdateBrowseMode();
		}

		private void WebView_LoadComplete(object sender, LoadCompletedEventArgs loadCompletedEventArgs)
		{
			HideProgress();
			BrowseBar.Enabled = true;
		}

		private void SectionChanged(object sender, EventArgs e)
		{
			GroupsList.EditValue = null;
			GroupsList.Properties.Items.Clear();
			GroupsList.Enabled = false;
			var section = SectionsList.EditValue as SnapshotCollection;
			if (section == null) return;
			GroupsList.Enabled = section.Screenshots.Any();
			GroupsList.Properties.Items.AddRange(section.Screenshots);
			if (Manager.AutoLoad)
				GroupsList.EditValue = section.Screenshots.FirstOrDefault();
		}

		private void GroupChanged(object sender, EventArgs e)
		{
			var snapshot = GroupsList.EditValue as SnapshotItem;
			if (snapshot == null) return;
			Navigate(snapshot.Url);
		}

		private void WebView_BeforeContextMenu(object sender, BeforeContextMenuEventArgs e)
		{
			e.Menu.Items.Clear();
			if (e.MenuInfo.MediaType != ContextMenuMediaType.Image) return;
			e.Menu.Items.Add(new EO.WebBrowser.MenuItem("Copy Image to Clipboard", _idCommandDownload));
			e.Menu.Items.Add(new EO.WebBrowser.MenuItem("Edit this Image first", _idCommandEdit));
		}

		void WebView_Command(object sender, CommandEventArgs e)
		{
			if (e.CommandId == _idCommandEdit)
			{
				var images = DownloadImages(new[] { e.MenuInfo.SourceUrl });
				if (!images.Any()) return;
				LoadImageToEditor(images.First().Value);
				ViewMode_Click(EditMode, EventArgs.Empty);
			}
			else if (e.CommandId == _idCommandDownload)
			{
				var images = DownloadImages(new[] { e.MenuInfo.SourceUrl });
				if (images.Any())
					Utils.PutImageToClipboard(images.First().Value);
			}
		}

		private void ShowProgress()
		{
			circularProgressWebpage.BringToFront();
			circularProgressWebpage.Visible = true;
			circularProgressWebpage.IsRunning = true;
			circularProgressWebpage.BringToFront();
		}

		private void HideProgress()
		{
			circularProgressWebpage.IsRunning = false;
			circularProgressWebpage.Visible = false;
		}

		private void ImageSelect_Click(object sender, EventArgs e)
		{
			ChangeTool(new RectangularSelectionToolWithCopyPaste { Cursor = Cursors.Cross });
		}

		private void ImageCrop_Click(object sender, EventArgs e)
		{
			ChangeTool(new CropSelectionTool { Cursor = Cursors.Cross });
		}

		private void ZoomIn_Click(object sender, EventArgs e)
		{
			_imageContainer.ChangeZoom(_zoomIndex += 10, _imageContainer.ClientRectangle.GetCenter());
		}

		private void ZoomOut_Click(object sender, EventArgs e)
		{
			_imageContainer.ChangeZoom((_zoomIndex > 10 ? (_zoomIndex -= 10) : 10), _imageContainer.ClientRectangle.GetCenter());
		}

		private void Copy_Click(object sender, EventArgs e)
		{
			Utils.PutImageToClipboard(_imageContainer.Image.GetAsBitmap());
		}

		private void GalleryControl_Resize(object sender, EventArgs e)
		{
			var p = ClientRectangle.GetCenter();
			var tmp = circularProgressWebpage.ClientRectangle.GetCenter();
			p.Offset(-tmp.X, -tmp.Y);
			circularProgressWebpage.Location = p;
		}
	}
}