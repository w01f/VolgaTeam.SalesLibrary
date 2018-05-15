using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class SecuritySettings : SettingsContainer
	{
		private bool _isForbidden;
		public bool IsForbidden
		{
			get => _isForbidden;
			set
			{
				if (_isForbidden != value)
					OnSettingsChanged();
				_isForbidden = value;
			}
		}

		private bool _isRestricted;
		public bool IsRestricted
		{
			get => _isRestricted;
			set
			{
				if (_isRestricted != value)
					OnSettingsChanged();
				_isRestricted = value;
			}
		}

		private bool _noShare;
		public bool NoShare
		{
			get => _noShare;
			set
			{
				if (_noShare != value)
					OnSettingsChanged();
				_noShare = value;
			}
		}

		private string _assignedUsers;
		public string AssignedUsers
		{
			get => _assignedUsers;
			set
			{
				if (_assignedUsers != value)
					OnSettingsChanged();
				_assignedUsers = value;
			}
		}

		private string _deniedUsers;
		public string DeniedUsers
		{
			get => _deniedUsers;
			set
			{
				if (_deniedUsers != value)
					OnSettingsChanged();
				_deniedUsers = value;
			}
		}

		[JsonIgnore]
		public bool HasSecuritySettings => NoShare || IsForbidden || IsRestricted;
	}
}