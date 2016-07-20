using System;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class LibraryFileLinkSettings : LibraryObjectLinkSettings
	{
		public DateTime? FileDate { get; set; }

		[JsonIgnore]
		protected LibraryFileLink ParentFileLink => (LibraryFileLink)Parent;

		public override string Note
		{
			get
			{
				if (ParentFileLink.IsDead &&
					ParentLink.ParentLibrary.InactiveLinksSettings.Enable &&
					(ParentLink.ParentLibrary.InactiveLinksSettings.ShowBoldWarning ||
						ParentLink.ParentLibrary.InactiveLinksSettings.ReplaceInactiveLinksWithLineBreak))
					return String.Empty;
				return base.Note;
			}
			set
			{
				if (_note != value)
					OnSettingsChanged();
				_note = value;
			}
		}

		[JsonIgnore]
		public override bool DisplayAsBold
		{
			get
			{
				if (ParentFileLink.IsDead &&
					ParentLink.ParentLibrary.InactiveLinksSettings.Enable &&
					ParentLink.ParentLibrary.InactiveLinksSettings.ShowBoldWarning)
					return true;
				return base.DisplayAsBold;
			}
		}
	}
}
