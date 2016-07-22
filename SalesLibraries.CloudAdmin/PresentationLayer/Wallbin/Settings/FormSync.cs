﻿using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.Business.Entities.Wallbin.Persistent;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Settings
{
	public partial class FormSync : MetroForm
	{
		public Library Library { get; set; }

		public FormSync()
		{
			InitializeComponent();
		}

		private void Form_Load(object sender, EventArgs e)
		{
			ckMinimizeOnSync.Checked = Library.SyncSettings.MinimizeOnSync;
			ckCloseAfterSync.Checked = Library.SyncSettings.CloseAfterSync;
			ckShowSyncStatus.Checked = Library.SyncSettings.ShowProgress;
		}

		private void FormSync_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			Library.SyncSettings.MinimizeOnSync = ckMinimizeOnSync.Checked;
			Library.SyncSettings.CloseAfterSync = ckCloseAfterSync.Checked;
			Library.SyncSettings.ShowProgress = ckShowSyncStatus.Checked;
		}
	}
}