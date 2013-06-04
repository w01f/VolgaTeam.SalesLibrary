﻿using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SalesDepot.BusinessClasses;

namespace SalesDepot.PresentationClasses.Viewers
{
	[ToolboxItem(false)]
	public partial class QuickTimeViewer : UserControl, IFileViewer
	{
		private FileInfo _tempCopy;

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

		public QuickTimeViewer(LibraryLink file)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			File = file;
			if (System.IO.File.Exists(File.LocalPath))
			{
				string tempPath = Path.Combine(AppManager.Instance.TempFolder.FullName, Path.GetFileName(File.LocalPath));
				System.IO.File.Copy(File.LocalPath, tempPath, true);
				_tempCopy = new FileInfo(tempPath);
			}
			//axWindowsMediaPlayer.URL = this.File.FullPath;
		}

		#region VideoViewer Methods
		public void Play()
		{
			//axWindowsMediaPlayer.Ctlcontrols.play();
		}

		public void Pause()
		{
			//axWindowsMediaPlayer.Ctlcontrols.pause();
		}

		public void Stop()
		{
			//axWindowsMediaPlayer.Ctlcontrols.stop();
		}

		public void InsertIntoPresentation() { }
		#endregion

		#region IFileViewer Methods
		public void ReleaseResources() { }

		public void Open()
		{
			LinkManager.Instance.OpenVideo(File);
		}

		public void Save()
		{
			LinkManager.Instance.SaveFile("Save copy of the file as", File);
		}

		public void Email()
		{
			LinkManager.Instance.EmailFile(File.LocalPath);
		}

		public void Print()
		{
			LinkManager.Instance.PrintFile(File);
		}

		public void EmailLinkToQuickSite()
		{
			LinkManager.Instance.EmailLinkToQuickSite(File);
		}

		public void AddLinkToQuickSite()
		{
			LinkManager.Instance.AddLinkToQuickSite(File);
		}
		#endregion
	}
}