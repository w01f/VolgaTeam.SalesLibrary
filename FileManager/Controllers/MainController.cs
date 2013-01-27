using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using FileManager.BusinessClasses;
using FileManager.ConfigurationClasses;
using FileManager.PresentationClasses.WallBin.Decorators;
using FileManager.ToolForms;
using FileManager.ToolForms.Settings;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.Controllers
{
	public class MainController
	{
		private static MainController _instance;
		private readonly Dictionary<TabPageEnum, IPageController> _controllers = new Dictionary<TabPageEnum, IPageController>();

		private TabPageEnum _activeTab;
		#region Libraries\Pages
		public Dictionary<Guid, LibraryDecorator> Decorators { get; private set; }

		private LibraryDecorator _activeDecorator;
		public LibraryDecorator ActiveDecorator
		{
			get { return _activeDecorator ?? (_activeDecorator = Decorators.Values.FirstOrDefault(x => x.Library.Name.Equals(SettingsManager.Instance.SelectedLibrary) || string.IsNullOrEmpty(SettingsManager.Instance.SelectedLibrary))); }
			set
			{
				if (_activeDecorator != null && _activeDecorator.StateChanged)
					_activeDecorator.Save();
				if (value != null)
				{
					SettingsManager.Instance.SelectedLibrary = value.Library.Name;
					SettingsManager.Instance.SelectedPage = string.Empty;
					SettingsManager.Instance.Save();
					value.ActivePage = null;
				}
				_activeDecorator = value;
			}
		}

		public event EventHandler<EventArgs> LibraryChanged;
		public void RequestUpdateLibrary(Library library)
		{
			FormMain.Instance.pnEmpty.BringToFront();
			FormMain.Instance.ribbonControl.Enabled = false;
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Update Library...";
				formProgress.TopMost = true;
				var thread = new Thread(() => FormMain.Instance.Invoke((MethodInvoker)delegate
				{
					if (ActiveDecorator != null)
						ActiveDecorator.Dispose();
					ActiveDecorator = null;
					if (Decorators.ContainsKey(library.Identifier))
					{
						Decorators[library.Identifier] = new LibraryDecorator(library);
						Decorators[library.Identifier].Init();
						if (SettingsManager.Instance.EnableOvernightsCalendarTab)
							Decorators[library.Identifier].BuildOvernightsCalendar();
					}
					if (LibraryChanged != null)
						LibraryChanged(this, new EventArgs());
				}));
				formProgress.Show();
				Application.DoEvents();
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				formProgress.Close();
			}
			ShowTab(_activeTab);
			FormMain.Instance.pnMain.BringToFront();
			FormMain.Instance.ribbonControl.Enabled = true;
		}
		public void RequestChangeLibrary()
		{
			FormMain.Instance.pnEmpty.BringToFront();
			FormMain.Instance.ribbonControl.Enabled = false;
			using (var formProgress = new FormProgress())
			{
				formProgress.TopMost = true;
				formProgress.laProgress.Text = "Update Library...";
				var thread = new Thread(() => FormMain.Instance.Invoke((MethodInvoker)delegate
				{
					if (LibraryChanged != null)
						LibraryChanged(this, new EventArgs());
					Application.DoEvents();
				}));
				formProgress.Show();
				Application.DoEvents();
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				formProgress.Close();
			}
			FormMain.Instance.pnMain.BringToFront();
			FormMain.Instance.ribbonControl.Enabled = true;
		}
		public event EventHandler<EventArgs> PageChanged;
		public void RequestChangePage()
		{
			if (PageChanged != null)
				PageChanged(this, new EventArgs());

		}
		#endregion

		#region Controllers
		public WallbinController WallbinController { get; private set; }
		public ClipartController ClipartController { get; private set; }
		public CalendarController CalendarController { get; private set; }
		public IPadContentController IPadContentController { get; private set; }
		public IPadUsersController IPadUsersController { get; private set; }
		#endregion

		#region Tool Forms
		private FormPaths _formPath;
		#endregion

		private MainController()
		{
			Decorators = new Dictionary<Guid, LibraryDecorator>();
			_formPath = new FormPaths();
		}

		public static MainController Instance
		{
			get { return _instance ?? (_instance = new MainController()); }
		}

		private void BuildDecorators()
		{
			_activeDecorator = null;
			foreach (var libraryDecorator in Decorators.Values)
				libraryDecorator.Dispose();
			Decorators.Clear();
			foreach (var library in LibraryManager.Instance.LibraryCollection)
			{
				var decorator = new LibraryDecorator(library);
				Decorators.Add(library.Identifier, decorator);
				decorator.Init();
				Application.DoEvents();
			}
		}

		private void BuildOvernightsCalendars()
		{
			foreach (LibraryDecorator libraryDecorator in Decorators.Values)
			{
				libraryDecorator.BuildOvernightsCalendar();
				Application.DoEvents();
			}
		}

		private void InitializePresentationLayer()
		{
			using (var formProgress = new FormProgress())
			{
				formProgress.TopMost = true;
				FormMain.Instance.pnEmpty.BringToFront();
				FormMain.Instance.ribbonControl.Enabled = false;
				FormMain.Instance.pnMain.Controls.Clear();
				var thread = new Thread(delegate()
					{
						FormMain.Instance.Invoke((MethodInvoker)delegate
						{
							formProgress.laProgress.Text = "Loading Libraries...";
							Application.DoEvents();
							FormMain.Instance.pnMain.Controls.Clear();
							Application.DoEvents();
						});

						LibraryManager.Instance.LoadLibraries(new DirectoryInfo(SettingsManager.Instance.BackupPath));
						foreach (Library library in LibraryManager.Instance.LibraryCollection)
							library.ProcessPresentationProperties();

						FormMain.Instance.Invoke((MethodInvoker)delegate
						{
							BuildDecorators();
							Application.DoEvents();
						});

						if (SettingsManager.Instance.EnableOvernightsCalendarTab)
							FormMain.Instance.Invoke((MethodInvoker)delegate
							{
								formProgress.laProgress.Text = "Loading Overnights Calendar...";
								Application.DoEvents();
								BuildOvernightsCalendars();
								Application.DoEvents();
							});

						FormMain.Instance.Invoke((MethodInvoker)delegate
						{
							formProgress.laProgress.Text = "Preparing libraries to display...";
							Application.DoEvents();
							FormMain.Instance.pnMain.Controls.Clear();
							Application.DoEvents();
						});

						FormMain.Instance.Invoke((MethodInvoker)delegate
						{
							Application.DoEvents();
							WallbinController.InitController();
							Application.DoEvents();
							ClipartController.InitController();
							Application.DoEvents();
							CalendarController.InitController();
							Application.DoEvents();
							IPadContentController.InitController();
							Application.DoEvents();
							IPadUsersController.InitController();
							Application.DoEvents();
						});
					});
				formProgress.Show();
				thread.Start();
				Application.DoEvents();
				while (thread.IsAlive)
					Application.DoEvents();
				formProgress.Close();
				FormMain.Instance.ribbonControl.Enabled = true;
				FormMain.Instance.pnMain.BringToFront();
			}
		}

		public void InitializeControllers()
		{
			_controllers.Clear();
			WallbinController = new WallbinController();
			_controllers.Add(TabPageEnum.Home, WallbinController);
			_controllers.Add(TabPageEnum.Tags, WallbinController);
			_controllers.Add(TabPageEnum.Settings, WallbinController);
			_controllers.Add(TabPageEnum.ProgramManager, WallbinController);

			ClipartController = new ClipartController();
			_controllers.Add(TabPageEnum.Clipart, ClipartController);

			CalendarController = new CalendarController();
			_controllers.Add(TabPageEnum.Calendar, CalendarController);

			IPadContentController = new IPadContentController();
			_controllers.Add(TabPageEnum.IPadContent, IPadContentController);

			IPadUsersController = new IPadUsersController();
			_controllers.Add(TabPageEnum.IPadUsers, IPadUsersController);
		}

		public void LoadDataAndGUI()
		{
			ActiveDecorator = null;
			LibraryChanged = null;
			PageChanged = null;
			InitializePresentationLayer();
			ShowTab(TabPageEnum.Home);
		}

		#region Ribbon Commands
		public void ChangePath()
		{
			if (SaveLibraryWarning())
				if (_formPath.ShowDialog() == DialogResult.OK)
					LoadDataAndGUI();

		}
		#endregion

		#region Common Page Functionality
		public void ShowTab(TabPageEnum tabPage)
		{
			if (!_controllers.ContainsKey(tabPage)) return;
			SaveLibraryWarning();
			_activeTab = tabPage;
			_controllers[tabPage].PrepareTab(tabPage);
			FormMain.Instance.pnEmpty.BringToFront();
			_controllers[tabPage].ShowTab();
			FormMain.Instance.pnEmpty.SendToBack();
		}

		public bool SaveLibraryWarning()
		{
			if (ActiveDecorator != null && ActiveDecorator.StateChanged)
			{
				if (AppManager.Instance.ShowQuestion("Before you leave, do you want to save the changes you made?") == DialogResult.Yes)
				{
					ActiveDecorator.Save();
					return true;
				}
				if (MessageBox.Show("You are about to lose your changes.\nThe changes will be LOST FOREVER & EVER & EVER!", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
				{
					RequestUpdateLibrary(ActiveDecorator.Library);
					return true;
				}
				return true;
			}
			return true;
		}
		#endregion
	}

	public enum TabPageEnum
	{
		None,
		Home,
		Tags,
		Settings,
		Clipart,
		Calendar,
		ProgramManager,
		IPadContent,
		IPadUsers
	}

	public interface IPageController
	{
		void InitController();
		void PrepareTab(TabPageEnum tabPage);
		void ShowTab();
	}
}