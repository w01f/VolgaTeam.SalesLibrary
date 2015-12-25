using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Business.LinkViewers;
using SalesLibraries.SalesDepot.Configuration;
using SalesLibraries.SalesDepot.Controllers;
using SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Processors
{
	[IntendForClass(typeof(VideoLink))]
	class VideoLinkProcessor : ILinkViewProcessor
	{
		private readonly VideoLink _videoLink;

		public VideoLinkProcessor(VideoLink videoLink)
		{
			_videoLink = videoLink;
		}

		public void Open()
		{
			switch (MainController.Instance.Settings.LinkLaunchSettings.Video)
			{
				case LinkLaunchOptionsEnum.Menu:
					using (var formVideoOptions = new FormVideoViewOptions())
					{
						formVideoOptions.Text = string.Format(formVideoOptions.Text, _videoLink.Name);
						if (formVideoOptions.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK)
						{
							if (formVideoOptions.SelectedOption == VideoViewOptions.Add)
							{
								LinkManager.AddVideoIntoPresentation(_videoLink);
							}
							else if (formVideoOptions.SelectedOption == VideoViewOptions.Open)
							{
								LinkManager.OpenVideo(_videoLink);
							}
						}
					}
					break;
				case LinkLaunchOptionsEnum.Launch:
					LinkManager.OpenVideo(_videoLink);
					return;
				case LinkLaunchOptionsEnum.Viewer:
					LinkManager.PreviewLink(_videoLink);
					break;
			}
		}
	}
}
