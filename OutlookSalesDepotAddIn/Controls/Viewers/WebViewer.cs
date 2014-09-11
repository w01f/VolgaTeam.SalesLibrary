﻿using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using OutlookSalesDepotAddIn.BusinessClasses;

namespace OutlookSalesDepotAddIn.Controls.Viewers
{
	[ToolboxItem(false)]
	public partial class WebViewer : UserControl, IFileViewer
	{
		#region Properties
		public LibraryLink File { get; private set; }

		public string DisplayName
		{
			get { return File.DisplayName; }
		}

		public string CriteriaOverlap
		{
			get { return File.CriteriaOverlap; }
		}

		public Image Widget
		{
			get { return File.Widget; }
		}
		#endregion

		public WebViewer(LibraryLink file)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			File = file;
			webBrowser.Navigate(File.LocalPath);
		}

		#region IFileViewer Methods
		public void Attach()
		{
			LinkManager.Instance.AttachFile(File);
		}

		public void ReleaseResources()
		{
			webBrowser.Navigate("about:blank");
		}
		#endregion
	}
}