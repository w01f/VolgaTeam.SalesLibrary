using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public class CommonFileLink : LibraryFileLink
	{
		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public override string WebFormat
		{
			get
			{
				if (FileFormatHelper.IsJpegFile(FullPath))
					return WebFormats.Jpeg;
				if (FileFormatHelper.IsAppleDocumentFile(FullPath))
					return WebFormats.AppleDocument;
				if (FileFormatHelper.IsMp3File(FullPath))
					return WebFormats.Mp3;
				if (FileFormatHelper.IsPngFile(FullPath))
					return WebFormats.Png;
				if (FileFormatHelper.IsPngFile(FullPath))
					return WebFormats.Png;
				if (FileFormatHelper.IsUrlFile(FullPath))
					return WebFormats.Url;
				return base.WebFormat;
			}
		}
		#endregion
	}
}
