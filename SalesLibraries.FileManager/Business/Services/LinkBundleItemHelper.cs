using System.Drawing;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.Business.Services
{
	public static class LinkBundleItemHelper
	{
		public static BaseBundleItem AssignDefaultImage(this BaseBundleItem target)
		{
			switch (target.ItemType)
			{
				case LinkBundleItemType.LibraryLink:
					if (((LibraryLinkItem)target).IsDead)
						break;
					switch (((LibraryLinkItem)target).TargetLink.Type)
					{
						case FileTypes.PowerPoint:
							target.Image = MainController.Instance.Lists.LinkBundleImages.DefaultPowerPointLogo.ExistsLocal()
								? Image.FromFile(MainController.Instance.Lists.LinkBundleImages.DefaultPowerPointLogo.LocalPath)
								: null;
							break;
						case FileTypes.Word:
							target.Image = MainController.Instance.Lists.LinkBundleImages.DefaultWordLogo.ExistsLocal()
								? Image.FromFile(MainController.Instance.Lists.LinkBundleImages.DefaultWordLogo.LocalPath)
								: null;
							break;
						case FileTypes.Excel:
							target.Image = MainController.Instance.Lists.LinkBundleImages.DefaultExcelLogo.ExistsLocal()
								? Image.FromFile(MainController.Instance.Lists.LinkBundleImages.DefaultExcelLogo.LocalPath)
								: null;
							break;
						case FileTypes.Pdf:
							target.Image = MainController.Instance.Lists.LinkBundleImages.DefaultPdfLogo.ExistsLocal()
								? Image.FromFile(MainController.Instance.Lists.LinkBundleImages.DefaultPdfLogo.LocalPath)
								: null;
							break;
						case FileTypes.Video:
							target.Image = MainController.Instance.Lists.LinkBundleImages.DefaultVideoLogo.ExistsLocal()
								? Image.FromFile(MainController.Instance.Lists.LinkBundleImages.DefaultVideoLogo.LocalPath)
								: null;
							break;
						case FileTypes.Url:
						case FileTypes.Html5:
						case FileTypes.YouTube:
						case FileTypes.Vimeo:
						case FileTypes.InternalLink:
						case FileTypes.AppLink:
						case FileTypes.QPageLink:
							target.Image = MainController.Instance.Lists.LinkBundleImages.DefaultUrlLogo.ExistsLocal()
								? Image.FromFile(MainController.Instance.Lists.LinkBundleImages.DefaultUrlLogo.LocalPath)
								: null;
							break;
						case FileTypes.Other:
							target.Image = MainController.Instance.Lists.LinkBundleImages.DefaultImageLogo.ExistsLocal() &&
									(FileFormatHelper.IsPngFile(((LibraryLinkItem)target).TargetLink.FullPath) ||
									FileFormatHelper.IsJpegFile(((LibraryLinkItem)target).TargetLink.FullPath))
								? Image.FromFile(MainController.Instance.Lists.LinkBundleImages.DefaultVideoLogo.LocalPath)
								: null;
							break;
					}
					break;
				case LinkBundleItemType.LaunchScreen:
					target.Image = MainController.Instance.Lists.LinkBundleImages.DefaultInfoLogo.ExistsLocal()
						? Image.FromFile(MainController.Instance.Lists.LinkBundleImages.DefaultInfoLogo.LocalPath)
						: null;
					break;
				case LinkBundleItemType.Info:
					target.Image = MainController.Instance.Lists.LinkBundleImages.DefaultInfoLogo.ExistsLocal()
						? Image.FromFile(MainController.Instance.Lists.LinkBundleImages.DefaultInfoLogo.LocalPath)
						: null;
					break;
				case LinkBundleItemType.Revenue:
					target.Image = MainController.Instance.Lists.LinkBundleImages.DefaultRevenueLogo.ExistsLocal()
						? Image.FromFile(MainController.Instance.Lists.LinkBundleImages.DefaultRevenueLogo.LocalPath)
						: null;
					break;
				case LinkBundleItemType.Strategy:
					target.Image = MainController.Instance.Lists.LinkBundleImages.DefaultStrategyLogo.ExistsLocal()
						? Image.FromFile(MainController.Instance.Lists.LinkBundleImages.DefaultStrategyLogo.LocalPath)
						: null;
					break;
				case LinkBundleItemType.Url:
					target.Image = MainController.Instance.Lists.LinkBundleImages.DefaultUrlLogo.ExistsLocal()
						? Image.FromFile(MainController.Instance.Lists.LinkBundleImages.DefaultUrlLogo.LocalPath)
						: null;
					break;
			}
			return target;
		}
	}
}
