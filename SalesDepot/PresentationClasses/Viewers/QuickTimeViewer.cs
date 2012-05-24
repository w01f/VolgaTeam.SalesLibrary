﻿using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.Viewers
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class QuickTimeViewer : UserControl, IFileViewer
    {
        #region Properties
        public BusinessClasses.LibraryFile File { get; private set; }

        public string DisplayName
        {
            get
            {
                return this.File.DisplayName;
            }
        }

        public string CriteriaOverlap
        {
            get
            {
                return this.File.CriteriaOverlap;
            }
        }

        public Image Widget
        {
            get
            {
                return this.File.Widget;
            }
        }
        #endregion

        public QuickTimeViewer(BusinessClasses.LibraryFile file)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Visible = false;

            this.File = file;

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

        public void InsertIntoPresentation()
        {
            if(this.File.Type == BusinessClasses.FileTypes.MediaPlayerVideo)
                BusinessClasses.LinkManager.Instance.AddVideoIntoPresentation(new FileInfo(this.File.LocalPath));
        }
        #endregion

        #region IFileViewer Methods
        public void ReleaseResources()
        {
        }

        public void Open()
        {
            BusinessClasses.LinkManager.Instance.OpenVideo(new FileInfo(this.File.LocalPath));
        }

        public void Save()
        {
            ToolClasses.ActivityRecorder.Instance.WriteActivity();
            BusinessClasses.LinkManager.Instance.SaveFile("Save copy of the file as", new FileInfo(this.File.LocalPath));
        }

        public void Email()
        {
            ToolClasses.ActivityRecorder.Instance.WriteActivity();
            BusinessClasses.LinkManager.Instance.EmailFile(this.File.LocalPath);
        }

        public void Print()
        {
            ToolClasses.ActivityRecorder.Instance.WriteActivity();
            BusinessClasses.LinkManager.Instance.PrintFile(new FileInfo(this.File.LocalPath));
        }
        #endregion
    }
}
