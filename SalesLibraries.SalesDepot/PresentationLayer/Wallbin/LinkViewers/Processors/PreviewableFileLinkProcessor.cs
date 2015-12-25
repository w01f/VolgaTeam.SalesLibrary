using System;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.SalesDepot.Business.LinkViewers;
using SalesLibraries.SalesDepot.Configuration;
using SalesLibraries.SalesDepot.Controllers;
using SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Processors
{
	abstract class PreviewableFileLinkProcessor:ILinkViewProcessor
	{
		protected readonly LibraryFileLink _fileLink;

		protected PreviewableFileLinkProcessor(LibraryFileLink fileLink)
		{
			_fileLink = fileLink;
		}

		public void Open()
		{
			switch (GetLaunchOptions())
			{
				case LinkLaunchOptionsEnum.Menu:
					using (var formViewOptions = new FormViewOptions())
					{
						formViewOptions.Text = String.Format(formViewOptions.Text, _fileLink.Name);
						if (formViewOptions.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK)
						{
							if (formViewOptions.SelectedOption == ViewOptions.Open)
								LinkManager.OpenCopyOfFile(_fileLink);
							else if (formViewOptions.SelectedOption == ViewOptions.Save)
								LinkManager.SaveLink("Save copy of the file as", _fileLink);
							else if (formViewOptions.SelectedOption == ViewOptions.Print)
								LinkManager.PrintFile(_fileLink);
							else if (formViewOptions.SelectedOption == ViewOptions.Email)
								EmailLink();
						}
					}
					break;
				case LinkLaunchOptionsEnum.Launch:
					LinkManager.OpenCopyOfFile(_fileLink);
					return;
				case LinkLaunchOptionsEnum.Viewer:
					OpenViewer();
					break;
			}
		}

		protected abstract LinkLaunchOptionsEnum GetLaunchOptions();
		protected abstract void OpenViewer();
		protected abstract void EmailLink();
	}
}
