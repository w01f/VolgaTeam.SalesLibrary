using System;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Views
{
	class ListPageContent : PageContent
	{
		private Panel _topAnchor;

		private const int LeftPadding = 20;
		private const int RightPadding = 20;
		private const int TopPadding = 20;
		public ListPageContent(IPageView pageContainer) : base(pageContainer) { }

		#region Public Methods
		public override void DisposeContent()
		{
			DisposeTopAnchor();
			base.DisposeContent();
		}
		#endregion

		#region Protected Methods
		protected override void LoadContentParts()
		{
			LoadTopAnchor();
			LoadFolders();
		}

		protected override void UpdateContentParts()
		{
			UpdateFolders();
		}

		protected override void UpdateContentPartsSizes()
		{
			UpdateFoldersSize();
		}

		private void LoadTopAnchor()
		{
			_topAnchor = new Panel();
			_topAnchor.Dock = DockStyle.Top;
			_topAnchor.Height = 0;
			Controls.Add(_topAnchor);
		}

		private void DisposeTopAnchor()
		{
			if (_topAnchor == null) return;
			Controls.Remove(_topAnchor);
			_topAnchor.Dispose();
			_topAnchor = null;
		}
		#endregion

		#region Folders Processing
		protected override void OnFolderSizeChanged(object sender, EventArgs e)
		{
			WinAPIHelper.SendMessage(Handle, 11, IntPtr.Zero, IntPtr.Zero);
			UpdateFoldersSize();
			WinAPIHelper.SendMessage(Handle, 11, new IntPtr(1), IntPtr.Zero);
			Refresh();
		}

		protected override void UpdateFoldersSize()
		{
			var width = InnerWidth - (LeftPadding + RightPadding);
			var top = _topAnchor.Bottom;
			foreach (var folderBox in _folderBoxes
				.OrderBy(fb => fb.DataSource.RowOrder))
			{
				folderBox.UpdateHeaderSize();
				folderBox.Width = width;
				folderBox.Top = top + TopPadding;
				folderBox.Left = LeftPadding;
				top += (folderBox.Height + TopPadding);
			}
		}
		#endregion
	}
}
