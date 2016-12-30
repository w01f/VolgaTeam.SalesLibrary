﻿using System;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	public interface ILinkSettingsEditControl
	{
		LinkSettingsType[] SupportedSettingsTypes { get; }
		int Order { get; }
		bool AvailableForEmbedded { get; }
		SettingsEditorHeaderInfo HeaderInfo { get; }
		event EventHandler<EventArgs> ForceCloseRequested;
		void LoadData();
		void SaveData();
	}
}
