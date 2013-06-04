﻿using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SalesDepot.BusinessClasses;

namespace SalesDepot.PresentationClasses.Viewers
{
	[ToolboxItem(false)]
	public partial class EmptyViewer : UserControl, IFileViewer
	{
		#region Properties
		public LibraryLink File { get; private set; }

		public string DisplayName
		{
			get { return string.Empty; }
		}

		public string CriteriaOverlap
		{
			get { return string.Empty; }
		}

		public Image Widget
		{
			get { return null; }
		}
		#endregion

		public EmptyViewer(LibraryLink file)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			File = file;
		}

		#region IFileViewer Methods
		public void ReleaseResources() {}

		public void Open() {}

		public void Save() {}

		public void Email() {}

		public void Print() {}

		public void EmailLinkToQuickSite() {}

		public void AddLinkToQuickSite() {}
		#endregion
	}
}