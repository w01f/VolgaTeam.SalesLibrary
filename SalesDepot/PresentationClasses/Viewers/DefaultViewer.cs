using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SalesDepot.BusinessClasses;
using SalesDepot.CoreObjects.BusinessClasses;

namespace SalesDepot.PresentationClasses.Viewers
{
	[ToolboxItem(false)]
	public partial class DefaultViewer : UserControl, IFileViewer
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

		public DefaultViewer(LibraryLink file)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			File = file;

			switch (File.Type)
			{
				case FileTypes.BuggyPresentation:
				case FileTypes.FriendlyPresentation:
				case FileTypes.Presentation:
					laMessage.Text = "Double-Click PowerPoint files to preview";
					break;
				default:
					laMessage.Text = "Double-Click File to preview...";
					break;
			}
		}

		#region IFileViewer Methods
		public void ReleaseResources() {}

		public void Open()
		{
			switch (File.Type)
			{
				case FileTypes.Other:
				case FileTypes.QuickTimeVideo:
					LinkManager.Instance.OpenCopyOfFile(File);
					break;
				case FileTypes.Folder:
					LinkManager.Instance.OpenFolder(File);
					break;
				case FileTypes.Url:
					LinkManager.Instance.StartProcess(File);
					break;
				case FileTypes.Network:
					LinkManager.Instance.StartProcess(File);
					break;
				default:
					LinkManager.Instance.OpenLink(File);
					break;
			}
		}

		public void Save()
		{
			LinkManager.Instance.SaveFile("Save copy of the file as", File);
		}

		public void Email()
		{
			LinkManager.Instance.EmailFile(File);
		}

		public void Print()
		{
			LinkManager.Instance.PrintFile(File);
		}
		#endregion

		private void laMessage_DoubleClick(object sender, EventArgs e)
		{
			LinkManager.Instance.OpenLink(File);
		}
	}
}