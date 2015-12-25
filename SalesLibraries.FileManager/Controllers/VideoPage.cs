using System;
using System.Windows.Forms;
using SalesLibraries.Common.DataState;
using SalesLibraries.FileManager.Business.Synchronization;
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

			_editor.LoadVideoInfo();

			_editor.BringToFront();
		}

		public void ProcessChanges()
		{
			if (!IsActive) return;
			_editor.ProcessChanges();
		}

		public void OnLibraryChanged(object sender, EventArgs e)
		{
			var activeWallbin = MainController.Instance.WallbinViews.ActiveWallbin;
			if (activeWallbin == null) return;
			MainController.Instance.ProcessManager.RunInQueue("Loading Video...",
					() => MainController.Instance.MainForm.Invoke(
						new MethodInvoker(() => _editor.LoadLibrary(activeWallbin.DataStorage))));
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
