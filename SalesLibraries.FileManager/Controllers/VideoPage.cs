using System;
using SalesLibraries.Common.DataState;
using SalesLibraries.FileManager.PresentationLayer.Video;

namespace SalesLibraries.FileManager.Controllers
{
	class VideoPage : IPageController
	{
		private VideoContentEditor _editor;
		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }

		public VideoPage()
		{
			NeedToUpdate = true;
		}

		public void InitController()
		{
			DataStateObserver.Instance.DataChanged += (o, e) =>
			{
				if (e.ChangeType != DataChangeType.LibrarySelected) return;
				OnLibraryChanged(o, e);
			};

			_editor = new VideoContentEditor();
			MainController.Instance.MainForm.buttonItemVideoConvert.Click += OnVideoConvertClick;
			MainController.Instance.MainForm.buttonItemVideoDelete.Click += OnVideoDeleteClick;
		}

		public void ShowPage(TabPageEnum pageType)
		{
			IsActive = true;

			if (!MainController.Instance.MainForm.pnContainer.Controls.Contains(_editor))
				MainController.Instance.MainForm.pnContainer.Controls.Add(_editor);

			_editor.Visible = true;
			_editor.BringToFront();

			if (NeedToUpdate)
			{
				NeedToUpdate = false;
				OnLibraryChanged(this, EventArgs.Empty);
			}

			_editor.LoadVideoInfo();
		}

		public void ProcessChanges()
		{
			if (!IsActive) return;
			_editor.ProcessChanges();
		}

		public void OnLibraryChanged(object sender, EventArgs e)
		{
			if (!IsActive)
			{
				NeedToUpdate = true;
				return;
			}
			var activeWallbin = MainController.Instance.WallbinViews.ActiveWallbin;
			if (activeWallbin == null) return;
			_editor.LoadLibrary(activeWallbin.DataStorage);
		}

		public void UpdateStatusBar()
		{
			MainController.Instance.UpdateCommonStatusBar();
		}

		private void OnVideoConvertClick(object sender, EventArgs eventArgs)
		{
			_editor.ConvertSelected();
		}

		private void OnVideoDeleteClick(object sender, EventArgs eventArgs)
		{
			_editor.DeleteSelected();
		}
	}
}
