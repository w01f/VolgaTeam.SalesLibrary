using System;
using System.IO;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo
{
	public class InternalLinkTemplate
	{
		public InternlalLinkTemplateType Type { get; set; }
		public string SourcePath { get; set; }
		public string Name => Path.GetFileNameWithoutExtension(SourcePath);

		public override String ToString()
		{
			return Name;
		}

		public string Serialize()
		{
			return File.Exists(SourcePath) ? 
				Convert.ToBase64String(File.ReadAllBytes(SourcePath)):
				null;
		}
	}
}
