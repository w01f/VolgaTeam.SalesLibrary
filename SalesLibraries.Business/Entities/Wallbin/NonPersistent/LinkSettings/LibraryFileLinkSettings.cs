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
				if (ParentFileLink == null || ParentFileLink.IsDead)
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
	}
}
