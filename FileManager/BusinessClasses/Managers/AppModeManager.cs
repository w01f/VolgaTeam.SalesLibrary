using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FileManager.BusinessClasses
{
	enum AppModeEnum
	{
		Local = 0,
		Cloud = 1
	}

	class AppModeManager
	{
		private static readonly AppModeManager _instance = new AppModeManager();

		public AppModeEnum AppMode { get; private set; }

		public static AppModeManager Instance
		{
			get { return _instance; }
		}

		private AppModeManager()
		{

		}

		public void Load()
		{
			AppMode = AppModeEnum.Local;
			var settingsPath = Path.Combine(Path.GetDirectoryName(typeof(AppModeManager).Assembly.Location), "mode.txt");
			if (!File.Exists(settingsPath)) return;
			AppModeEnum mode;
			if (!Enum.TryParse(File.ReadAllText(settingsPath), out mode)) return;
			AppMode = mode;
		}
	}
}
