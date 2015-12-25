using System;
using System.Windows.Forms;
using SalesLibraries.Common.DataState;
using SalesLibraries.SalesDepot.PresentationLayer.Settings;
using SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Views;

namespace SalesLibraries.SalesDepot.Controllers
{
	class WallbinPage : WallbinContainer, IPageController
	{
		public bool IsActive { get; set; }

		#region IPageController
		public void InitController()
		{
			DataStateObserver.Instance.DataChanged += (o, e) =>
			{
				if (e.ChangeType != DataChangeType.LibrarySelected) return;
				OnLibraryChanged(o, e);
			};
			InitControl();
			MainController.Instance.MainForm.buttonItemHomeSettings.Click += OnHomeSettings_Click;
			MainController.Instance.MainForm.buttonItemHomeZoomIn.Click += OnHomeZoomInClick;
			MainController.Instance.MainForm.buttonItemHomeZoomOut.Click += OnHomeZoomOutClick;
			MainController.Instance.MainForm.buttonItemHomeHelp.Click += OnHelpClick;
		}

		public void ShowPage(TabPageEnum pageType)
		{
			IsActive = true;

			if (NeedToUpdate)
			{
				NeedToUpdate = false;
				OnLibraryChanged(this, EventArgs.Empty);
			}

			if (!MainController.Instance.MainForm.pnContainer.Controls.Contains(this))
				MainController.Instance.MainForm.pnContainer.Controls.Add(this);
			pnContainer.BringToFront();
			BringToFront();
			MainController.Instance.ActivityManager.AddUserActivity("Wall Bin selected");
		}

		public void OnLibraryChanged(object sender, EventArgs e)
		{
			if (!IsActive)
			{
				NeedToUpdate = true;
				return;
			}
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
			{
				pnEmpty.BringToFront();
				var activeWallbin = MainController.Instance.WallbinViews.ActiveWallbin;
				if (activeWallbin == null) return;
				if (!pnContainer.Controls.Contains((Control)activeWallbin))
					pnContainer.Controls.Add((Control)activeWallbin);

				activeWallbin.ShowView();
				((Control)activeWallbin).BringToFront();
				pnContainer.BringToFront();
			})));

		}
		#endregion

		private void OnHomeZoomInClick(object sender, EventArgs e)
		{
			var needToUpdate = false;
			if (MainController.Instance.WallbinViews.FormatState.FontSize < 19)
			{
				MainController.Instance.WallbinViews.FormatState.FontSize += 3;
				needToUpdate = true;
			}
			else
				MainController.Instance.WallbinViews.FormatState.FontSize = 19;
			if (MainController.Instance.WallbinViews.FormatState.RowSpace < 3)
			{
				MainController.Instance.WallbinViews.FormatState.RowSpace += 1;
				needToUpdate = true;
			}
			else
				MainController.Instance.WallbinViews.FormatState.RowSpace = 3;
			if (needToUpdate)
				MainController.Instance.WallbinViews.FormatState.Update();
		}

		private void OnHomeZoomOutClick(object sender, EventArgs e)
		{
			var needToUpdate = false;
			if (MainController.Instance.WallbinViews.FormatState.FontSize > 10)
			{
				MainController.Instance.WallbinViews.FormatState.FontSize -= 3;
				needToUpdate = true;
			}
			else
				MainController.Instance.WallbinViews.FormatState.FontSize = 10;
			if (MainController.Instance.WallbinViews.FormatState.RowSpace > 1)
			{
				MainController.Instance.WallbinViews.FormatState.RowSpace -= 1;
				needToUpdate = true;
			}
			else
				MainController.Instance.WallbinViews.FormatState.RowSpace = 1;
			if (needToUpdate)
				MainController.Instance.WallbinViews.FormatState.Update();
		}

		private void OnHomeSettings_Click(object sender, EventArgs e)
		{
			using (var form = new FormWallbinSettings())
			{
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				NeedToUpdate = true;
				MainController.Instance.LoadWallbinViews();
			}
		}

		private void OnHelpClick(object sender, EventArgs eventArgs)
		{
			if (MainController.Instance.WallbinViews.FormatState.ClassicView)
			{
				MainController.Instance.HelpManager.OpenHelpLink("classic");
			}
			else if (MainController.Instance.WallbinViews.FormatState.ListView)
			{
				MainController.Instance.HelpManager.OpenHelpLink("list");
			}
			else if (MainController.Instance.WallbinViews.FormatState.AccordionView)
			{
				MainController.Instance.HelpManager.OpenHelpLink("accord");
			}
		}
	}
}
