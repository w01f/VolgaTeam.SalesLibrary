using System;
using SalesLibraries.Common.Objects.Activity;
using SalesLibraries.SalesDepot.PresentationLayer.Settings;

namespace SalesLibraries.SalesDepot.Controllers
{
	class SettingsPage : SettingsContainer, IPageController
	{
		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }

		#region IPageController
		public void InitController()
		{
			InitControl();
			MainController.Instance.MainForm.buttonItemGallery1Help.Click += OnHelpClick;
		}

		public void ShowPage(TabPageEnum pageType)
		{
			IsActive = true;
			if (!MainController.Instance.MainForm.pnContainer.Controls.Contains(this))
				MainController.Instance.MainForm.pnContainer.Controls.Add(this);
			BringToFront();
		}

		public void OnLibraryChanged(object sender, EventArgs e) { }
		#endregion

		private void OnHelpClick(object sender, EventArgs eventArgs)
		{
			MainController.Instance.HelpManager.OpenHelpLink("settings");
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// SettingsPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.Name = "SettingsPage";
			this.Size = new System.Drawing.Size(972, 904);
			this.ResumeLayout(false);

		}
	}
}
