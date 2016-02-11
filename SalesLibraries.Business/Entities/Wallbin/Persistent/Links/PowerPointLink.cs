using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public class PowerPointLink : PreviewableLink
	{
		#region Nonpersistent Properties
		private PowerPointLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<PowerPointLinkSettings>(this, SettingsEncoded)); }
			set { _settings = value as PowerPointLinkSettings; }
		}

		[NotMapped, JsonIgnore]
		public override string WebFormat
		{
			get { return WebFormats.PowerPoint; }
		}

		[NotMapped, JsonIgnore]
		public override string Hint
		{
			get
			{
				return String.Format("{0}{2}{1}",
					base.Hint,
					String.Format("Slide Size: {0} W = {1} H = {2}",
						((PowerPointLinkSettings)Settings).Orientation,
						((PowerPointLinkSettings)Settings).Width.ToString("#.##"),
						((PowerPointLinkSettings)Settings).Height.ToString("#.##")),
					Environment.NewLine);
			}
		}
		#endregion

		public PowerPointLink()
		{
			Type = FileTypes.PowerPoint;
		}

		public override void Delete(LibraryContext context)
		{
			var powerPointSettings = (PowerPointLinkSettings)Settings;
			powerPointSettings.ClearQuickViewContent();
			base.Delete(context);
		}

		protected override void AfterCreate()
		{
			var powerPointSettings = (PowerPointLinkSettings)Settings;
			powerPointSettings.UpdateSizeInfo();
			base.AfterCreate();
		}
	}
}
