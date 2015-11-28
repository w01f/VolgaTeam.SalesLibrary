using System.Collections.Generic;
using SalesLibraries.Business.Entities.Common;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent
{
	public class InactiveLinksSettings : SettingsContainer
	{
		private bool _enable;
		public bool Enable
		{
			get { return _enable; }
			set
			{
				if (_enable != value)
					OnSettingsChanged();
				_enable = value;
			}
		}

		private bool _showBoldWarning;
		public bool ShowBoldWarning
		{
			get { return _showBoldWarning; }
			set
			{
				if (_showBoldWarning != value)
					OnSettingsChanged();
				_showBoldWarning = value;
			}
		}

		private bool _replaceInactiveLinksWithLineBreak;
		public bool ReplaceInactiveLinksWithLineBreak
		{
			get { return _replaceInactiveLinksWithLineBreak; }
			set
			{
				if (_replaceInactiveLinksWithLineBreak != value)
					OnSettingsChanged();
				_replaceInactiveLinksWithLineBreak = value;
			}
		}

		private bool _showMessageAtStartup;
		public bool ShowMessageAtStartup
		{
			get { return _showMessageAtStartup; }
			set
			{
				if (_showMessageAtStartup != value)
					OnSettingsChanged();
				_showMessageAtStartup = value;
			}
		}

		private bool _sendEmail;
		public bool SendEmail
		{
			get { return _sendEmail; }
			set
			{
				if (_sendEmail != value)
					OnSettingsChanged();
				_sendEmail = value;
			}
		}

		public List<string> EmailList { get; private set; }

		public InactiveLinksSettings()
		{
			EmailList = new List<string>();
		}
	}
}
