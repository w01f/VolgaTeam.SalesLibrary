using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FileManager.ConfigurationClasses;
using FileManager.PresentationClasses.IPad;
using FileManager.PresentationClasses.OvernightsCalendar;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.WallBin.Decorators
{
	public class LibraryDecorator
	{
		private bool _isDisposed;

		public Library Library { get; set; }

		public RegularLibraryControl RegularControl { get; private set; }
		public MultitabLibraryControl TabControl { get; private set; }
		public OvernightsCalendarControl OvernightsCalendar { get; private set; }
		public IPadContentManagerControl IPadContentManager { get; private set; }
		public IPadPermissionsManagerControl IPadPermissionsManager { get; private set; }

		public bool StateChanged { get; set; }
		public bool FirstTimeProcess { get; set; }

		public List<PageDecorator> Pages { get; set; }
		private PageDecorator _activePage;
		public PageDecorator ActivePage
		{
			get { return _activePage ?? (_activePage = Pages.FirstOrDefault(x => x.Page.Name.Equals(SettingsManager.Instance.SelectedPage) || string.IsNullOrEmpty(SettingsManager.Instance.SelectedPage) || !Pages.Select(y => y.Page.Name).Contains(SettingsManager.Instance.SelectedPage))); }
			set
			{
				if (_activePage != null)
				{
					if (_activePage.ActiveBox != null)
						_activePage.ActiveBox.MakeInactive();
					_activePage.ClearSelection();
				}
				if (value != null)
				{
					SettingsManager.Instance.SelectedPage = value.Page.Name;
					SettingsManager.Instance.Save();
				}
				_activePage = value;
			}
		}

		public LibraryDecorator(Library library)
		{
			Library = library;
		}

		public void Init()
		{
			FirstTimeProcess = true;
			Pages = new List<PageDecorator>();
			BuildWallbin();
		}

		public void Dispose()
		{
			_isDisposed = true;
			if (TabControl != null)
			{
				TabControl.Dispose();
				TabControl = null;
			}
			if (RegularControl != null)
			{
				RegularControl.Dispose();
				RegularControl = null;
			}
			if (OvernightsCalendar != null)
			{
				OvernightsCalendar.Parent = null;
				OvernightsCalendar.DisposeCalendar();
				OvernightsCalendar.Dispose();
				OvernightsCalendar = null;
			}
			if (IPadContentManager != null)
			{
				IPadContentManager.Parent = null;
				IPadContentManager.Dispose();
				IPadContentManager = null;
			}
			if (IPadPermissionsManager != null)
			{
				IPadPermissionsManager.Parent = null;
				IPadPermissionsManager.Dispose();
				IPadPermissionsManager = null;
			}
			Application.DoEvents();
			foreach (var pageDecorator in Pages)
			{
				pageDecorator.Dispose();
				Application.DoEvents();
			}
			Pages.Clear();
		}

		public void BuildWallbin()
		{
			ActivePage = null;
			foreach (var pageDecorator in Library.Pages.Select(page => new PageDecorator(page) { Parent = this }))
			{
				Pages.Add(pageDecorator);
				Application.DoEvents();
			}
			RegularControl = new RegularLibraryControl();
			RegularControl.Init(Pages.ToArray());
			TabControl = new MultitabLibraryControl();
			TabControl.Init(Pages.ToArray());

			IPadContentManager = new IPadContentManagerControl(this);
			IPadPermissionsManager = new IPadPermissionsManagerControl(this);

			StateChanged = false;
		}

		public void BuildOvernightsCalendar(bool forceBuild = false)
		{
			OvernightsCalendar = new OvernightsCalendarControl(this);
			if (SettingsManager.Instance.EnableOvernightsCalendarTab)
				Library.OvernightsCalendar.LoadParts();
			if (!Library.OvernightsCalendar.Enabled || !SettingsManager.Instance.EnableOvernightsCalendarTab) return;
			OvernightsCalendar.Build(forceBuild);
			Application.DoEvents();
		}

		public void Save()
		{
			if (!_isDisposed)
			{
				foreach (var page in Pages)
					page.Save();
				Library.Save();
				StateChanged = false;
				IPadContentManager.UpdateVideoFiles();
			}
		}
	}
}