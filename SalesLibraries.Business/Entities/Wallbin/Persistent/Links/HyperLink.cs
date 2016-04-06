using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public abstract class HyperLink : LibraryObjectLink
	{
		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public override string FullPath
		{
			get { return RelativePath; }
		}

		[NotMapped, JsonIgnore]
		public override string WebPath
		{
			get { return RelativePath; }
		}

		[NotMapped, JsonIgnore]
		public override string Hint
		{
			get
			{
				var lines = new List<string>();
				if (!String.IsNullOrEmpty(((LibraryObjectLinkSettings)Settings).HoverNote))
					lines.Add(((LibraryObjectLinkSettings)Settings).HoverNote);
				lines.Add(String.Format("Url: {0}", Url));
				lines.Add(base.Hint);
				return String.Join(Environment.NewLine, lines);
			}
		}

		[NotMapped, JsonIgnore]
		public string Url
		{
			get { return RelativePath; }
		}
		#endregion
	}
}
