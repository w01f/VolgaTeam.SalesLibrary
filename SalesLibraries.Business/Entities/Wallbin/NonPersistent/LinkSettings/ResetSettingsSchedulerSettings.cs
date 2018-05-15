using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class ResetSettingsSchedulerSettings : SettingsContainer
	{
		private DateTime? _resetDate;
		public DateTime? ResetDate
		{
			get => _resetDate;
			set
			{
				if (_resetDate != value)
					OnSettingsChanged();
				_resetDate = value;
			}
		}

		public List<LinkSettingsGroupType> SettingsGroups { get; private set; }

		[JsonIgnore]
		public bool Enabled => ResetDate.HasValue && SettingsGroups.Any();

		public ResetSettingsSchedulerSettings()
		{
			SettingsGroups = new List<LinkSettingsGroupType>();
		}
	}
}
