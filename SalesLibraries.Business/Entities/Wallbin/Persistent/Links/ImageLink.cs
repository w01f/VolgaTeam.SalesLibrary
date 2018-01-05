using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public class ImageLink : PreviewableFileLink, IThumbnailSettingsHolder
	{
		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public override string WebFormat
		{
			get
			{
				if (FileFormatHelper.IsJpegFile(FullPath))
					return WebFormats.Jpeg;
				if (FileFormatHelper.IsPngFile(FullPath))
					return WebFormats.Png;
				if (FileFormatHelper.IsGifFile(FullPath))
					return WebFormats.Gif;
				return base.WebFormat;
			}
		}

		[NotMapped, JsonIgnore]
		public Color ThumbnailBackColor => ParentFolder.Settings.BackgroundWindowColor;

		[NotMapped, JsonIgnore]
		public bool ShowSourceFilesList => false;
		#endregion

		public IList<string> GetThumbnailSourceFiles()
		{
			return new[] { FullPath };
		}
	}
}
