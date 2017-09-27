using System;
using System.Collections.Generic;
using System.Linq;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.LinksTreeSelector
{
	public abstract class TreeGroup
	{
		public abstract string Title { get; }
		public string TitleAndLinksCount => String.Format("{0} ({1})", Title, Links.Count);

		public List<BaseLibraryLink> Links { get; } = new List<BaseLibraryLink>();
	}

	abstract class LinksFormatTreeGroup : TreeGroup
	{
		public abstract string[] TargetLinkFormats { get; }
		public abstract int StateImageIndex { get; }

		public static IList<LinksFormatTreeGroup> GetDefaultGroups()
		{
			return new LinksFormatTreeGroup[]
			{
				new PowerPointTreeGroup(),
				new WordTreeGroup(),
				new ExcelTreeGroup(),
				new PdfTreeGroup(),
				new VideoTreeGroup(),
				new ImageTreeGroup(),
				new UrlTreeGroup(),
				new UndefinedTreeGroup()
			};
		}
	}

	class RootTreeGroup : TreeGroup
	{
		private readonly ILinksGroup _linksGroup;
		public override string Title => String.Format("{0} (All Files)", _linksGroup.LinkGroupName);

		public RootTreeGroup(ILinksGroup linksGroup, FileTypes? defaultLinkType = null)
		{
			_linksGroup = linksGroup;
			Links.AddRange(linksGroup.AllGroupLinks.Where(link => defaultLinkType == null || link.Type == defaultLinkType));
		}
	}

	class PowerPointTreeGroup : LinksFormatTreeGroup
	{
		public override string Title => "PowerPoint Files";

		public override string[] TargetLinkFormats => new[] { WebFormats.PowerPoint };

		public override int StateImageIndex => 7;
	}

	class WordTreeGroup : LinksFormatTreeGroup
	{
		public override string Title => "Document Files";

		public override string[] TargetLinkFormats => new[] { WebFormats.Word };

		public override int StateImageIndex => 3;
	}

	class ExcelTreeGroup : LinksFormatTreeGroup
	{
		public override string Title => "Excel Files";

		public override string[] TargetLinkFormats => new[] { WebFormats.Excel };

		public override int StateImageIndex => 9;
	}

	class PdfTreeGroup : LinksFormatTreeGroup
	{
		public override string Title => "PDF Files";

		public override string[] TargetLinkFormats => new[] { WebFormats.Pdf };

		public override int StateImageIndex => 5;
	}

	class VideoTreeGroup : LinksFormatTreeGroup
	{
		public override string Title => "Video Files";

		public override string[] TargetLinkFormats => new[] { WebFormats.Video };

		public override int StateImageIndex => 4;
	}

	class ImageTreeGroup : LinksFormatTreeGroup
	{
		public override string Title => "Image Files";

		public override string[] TargetLinkFormats => new[]
		{
			WebFormats.Png,
			WebFormats.Jpeg,
			WebFormats.Gif
		};

		public override int StateImageIndex => 6;
	}

	class UrlTreeGroup : LinksFormatTreeGroup
	{
		public override string Title => "Hyperlinks";

		public override string[] TargetLinkFormats => new[]
		{
			WebFormats.Url,
			WebFormats.Html5,
			WebFormats.QuickSite,
			WebFormats.YouTube,
			WebFormats.Vimeo,
			WebFormats.AppLink,
			WebFormats.Lan,
			WebFormats.InternalLibraryFolder,
			WebFormats.InternalLibraryLink,
			WebFormats.InternalLibraryPage,
			WebFormats.InternalWallbin,
		};

		public override int StateImageIndex => 8;
	}

	class UndefinedTreeGroup : LinksFormatTreeGroup
	{
		public override string Title => "Other Files";

		public override string[] TargetLinkFormats => new string[] { };

		public override int StateImageIndex => 2;
	}
}

