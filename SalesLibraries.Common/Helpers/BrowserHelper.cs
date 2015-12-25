using System.Diagnostics;

namespace SalesLibraries.Common.Helpers
{
	class BrowserHelper
	{
		private static bool _chromeDefinded;
		private static bool _firefoxDefinded;
		private static bool _operaDefinded;
		private static bool _chromeInstalled;
		private static bool _firefoxInstalled;
		private static bool _operaInstalled;

		public static bool ChromeInstalled
		{
			get
			{
				if (!_chromeDefinded)
				{
					try
					{
						var process = new Process
						{
							StartInfo =
							{
								FileName = "chrome.exe",
								CreateNoWindow = true,
								WindowStyle = ProcessWindowStyle.Hidden
							}
						};
						process.Start();
						process.Kill();
						_chromeInstalled = true;
					}
					catch
					{
						_chromeInstalled = false;
					}
					_chromeDefinded = true;
				}
				return _chromeInstalled;
			}
		}

		public static bool FirefoxInstalled
		{
			get
			{
				if (!_firefoxDefinded)
				{
					try
					{
						var process = new Process
						{
							StartInfo =
							{
								FileName = "firefox.exe",
								CreateNoWindow = true,
								WindowStyle = ProcessWindowStyle.Hidden
							}
						};
						process.Start();
						process.Kill();
						_firefoxInstalled = true;
					}
					catch
					{
						_firefoxInstalled = false;
					}
					_firefoxDefinded = true;
				}
				return _firefoxInstalled;
			}
		}

		public static bool OperaInstalled
		{
			get
			{
				if (!_operaDefinded)
				{
					try
					{
						var process = new Process
						{
							StartInfo =
							{
								FileName = "opera.exe",
								CreateNoWindow = true,
								WindowStyle = ProcessWindowStyle.Hidden
							}
						};
						process.Start();
						process.Kill();
						_operaInstalled = true;
					}
					catch
					{
						_operaInstalled = false;
					}
					_operaDefinded = true;
				}
				return _operaInstalled;
			}
		}
	}
}
