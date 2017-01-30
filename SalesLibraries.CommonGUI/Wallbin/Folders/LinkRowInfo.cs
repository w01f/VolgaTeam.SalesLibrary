using System;
using System.Drawing;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Objects.Graphics;
using SalesLibraries.CommonGUI.Wallbin.Views;
using HorizontalAlignment = SalesLibraries.Business.Entities.Wallbin.Common.Enums.HorizontalAlignment;

namespace SalesLibraries.CommonGUI.Wallbin.Folders
{
	public class LinkRowInfo : IDisposable
	{
		private const int WidthMargin = 6;
		private const int DefaultImageWidth = 26;
		private const int DefaultImageHeight = Widget.DefaultHeight;

		private LinkRow _parent;

		public Image Image { get; private set; }
		public Rectangle ImageBorder { get; private set; }
		public string Text { get; private set; }
		public Rectangle TextBorder { get; private set; }
		public int RowWidth { get; private set; }
		public int RowHeight { get; private set; }
		public Color ForeColor { get; private set; }
		public Font Font { get; private set; }
		public bool WordWrap { get; private set; }
		public Color BackColor
		{
			get
			{
				if (_parent.Selected)
					return _parent.FolderBox.SelectedRowBackColor; 
				return Link.Folder.Settings.BackgroundWindowColor;
			}
		}

		public bool IsResponsible => WordWrap || (Link != null && Link.Banner.Enable && Link.Banner.ImageAlignement != HorizontalAlignment.Left);

		protected BaseLibraryLink Link => _parent?.Source;

		protected IWallbinViewFormat FormatState => _parent.FolderBox.FormatState;

		public LinkRowInfo(LinkRow parent)
		{
			_parent = parent;
			ImageBorder = new Rectangle();
			Text = String.Empty;
			TextBorder = new Rectangle();
		}

		public void Recalc()
		{
			#region Image
			Image = null;
			if (Link.Banner.Enable && Link.Banner.DisplayedImage != null)
				Image = Link.Banner.DisplayedImage;
			else if (FormatState.ShowCategoryTags && Link.Tags.HasCategories)
				Image = Properties.Resources.TagsWidgetCategories;
			else if (FormatState.ShowSuperFilterTags && Link.Tags.HasSuperFilters)
				Image = Properties.Resources.TagsWidgetSuperFIlters;
			else if (FormatState.ShowKeywordTags && Link.Tags.HasKeywords)
				Image = Properties.Resources.TagsWidgetKeywords;
			else if (FormatState.ShowSecurityTags && Link.Security.HasSecuritySettings)
			{
				if (Link.Security.IsForbidden)
					Image = Properties.Resources.TagsWidgetSecurityHidden;
				else if (Link.Security.NoShare)
					Image = Properties.Resources.TagsWidgetSecurityLocal;
				else if (Link.Security.IsRestricted)
				{
					if (!String.IsNullOrEmpty(Link.Security.AssignedUsers))
						Image = Properties.Resources.TagsWidgetSecurityWhiteList;
					else if (!String.IsNullOrEmpty(Link.Security.DeniedUsers))
						Image = Properties.Resources.TagsWidgetSecurityBlackList;
				}
			}

			if (Image == null && !Link.Widget.Disabled)
				Image = Link.Widget.DisplayedImage;
			#endregion

			#region Image Size and Coordinates
			int imageLeft;
			int imageTop;
			int imageWidth;
			int imageHeight;
			if (Link.Banner.Enable && Link.Banner.DisplayedImage != null)
			{
				if (Link.Banner.TextEnabled)
				{
					imageLeft = 0;
				}
				else
				{
					switch (Link.Banner.ImageAlignement)
					{
						case HorizontalAlignment.Left:
							imageLeft = 0;
							break;
						case HorizontalAlignment.Center:
							imageLeft = (_parent.DataGridView.Width - Link.Banner.DisplayedImage.Width) / 2;
							if (imageLeft < 0)
								imageLeft = 0;
							break;
						case HorizontalAlignment.Right:
							imageLeft = _parent.DataGridView.Width - Link.Banner.DisplayedImage.Width;
							if (imageLeft < 0)
								imageLeft = 0;
							break;
						default:
							imageLeft = 0;
							break;
					}
				}
				imageWidth = Link.Banner.DisplayedImage.Width;
				imageHeight = Link.Banner.DisplayedImage.Height;
			}
			else if ((FormatState.ShowCategoryTags ||
					FormatState.ShowKeywordTags ||
					FormatState.ShowSuperFilterTags ||
					FormatState.ShowSecurityTags
					) &&
				(Link.Security.HasSecuritySettings || Link.Tags.HasTags) &&
				Image != null)
			{
				imageLeft = 0;
				imageWidth = DefaultImageWidth;
				imageHeight = DefaultImageHeight;
			}
			else if (!Link.Widget.Disabled && Link.Widget.DisplayedImage != null)
			{
				imageLeft = 0;
				imageWidth = Link.Widget.DisplayedImage.Width > DefaultImageWidth ? Link.Widget.DisplayedImage.Width : DefaultImageWidth;
				imageHeight = Link.Widget.DisplayedImage.Height > DefaultImageHeight ? Link.Widget.DisplayedImage.Height : DefaultImageHeight;
			}
			else
			{
				imageLeft = 0;
				imageWidth = 0;
				imageHeight = DefaultImageHeight;
			}
			#endregion

			#region Text
			Text = !String.IsNullOrEmpty(Link.DisplayName) ? Link.DisplayName : String.Empty;
			WordWrap = Link.Settings.TextWordWrap;
			#endregion

			#region Font
			if (Link.DisplayFont != null)
			{
				Font = Link.DisplayFont;
			}
			else if (Link is LibraryObjectLink)
			{
				var objectLinkSettings = (LibraryObjectLinkSettings)Link.Settings;

				if (objectLinkSettings.DisplayAsBold)
				{
					Font = _parent.FolderBox.BoldRowFont;
				}
				else
				{
					switch (objectLinkSettings.RegularFontStyle)
					{
						case (FontStyle.Bold | FontStyle.Italic | FontStyle.Underline):
							Font = _parent.FolderBox.BoldItalicUndrerlineRowFont;
							break;
						case (FontStyle.Bold | FontStyle.Italic):
							Font = _parent.FolderBox.BoldItalicRowFont;
							break;
						case (FontStyle.Italic | FontStyle.Underline):
							Font = _parent.FolderBox.ItalicUnderlineRowFont;
							break;
						case (FontStyle.Bold | FontStyle.Underline):
							Font = _parent.FolderBox.BoldUnderlineRowFont;
							break;
						case FontStyle.Bold:
							Font = _parent.FolderBox.BoldRowFont;
							break;
						case FontStyle.Italic:
							Font = _parent.FolderBox.ItalicRowFont;
							break;
						case FontStyle.Underline:
							Font = _parent.FolderBox.UnderlineRowFont;
							break;
						default:
							Font = _parent.FolderBox.RegularRowFont;
							break;
					}
				}
			}
			else
				Font = _parent.FolderBox.RegularRowFont;
			#endregion

			#region Text Size and Coordinates
			int textLeft;
			int textTop;
			int textWidth;
			int textHeight;

			var textForCalculation = Text;

			var fontForSizeCalculation = new Font(
				Font.FontFamily,
				(Int32)Math.Ceiling(Font.Size),
				FontStyle.Bold | FontStyle.Italic | FontStyle.Underline,
				GraphicsUnit.Point);

			using (var g = Graphics.FromImage(new Bitmap(1, 1)))
			{
				var textSize = g.MeasureString(
					textForCalculation,
					fontForSizeCalculation,
					new Size(WordWrap ? _parent.DataGridView.Width - imageWidth - WidthMargin : Int32.MaxValue, Int32.MaxValue));
				textLeft = imageLeft + imageWidth + WidthMargin;
				textWidth = (Int32)Math.Ceiling(textSize.Width);
				textHeight = (Int32)Math.Ceiling(textSize.Height);
			}
			#endregion

			#region Fore Color
			ForeColor = Link.DisplayColor;
			#endregion

			#region Correct Image and text coordinates
			if (textHeight > imageHeight)
			{
				textTop = 0;
				if (Link != null && Link.Banner.Enable)
					imageTop = Link.Banner.ImageVerticalAlignement == VerticalAlignment.Top ? 0 : (textHeight - imageHeight) / 2;
				else
					imageTop = WordWrap ? 0 : (textHeight - imageHeight) / 2;
			}
			else if (textHeight == imageHeight)
			{
				textTop = 0;
				imageTop = 0;
			}
			else
			{
				if (Link != null && Link.Banner.Enable)
					textTop = Link.Banner.ImageVerticalAlignement == VerticalAlignment.Top ? 0 : (imageHeight - textHeight) / 2;
				else
					textTop = (imageHeight - textHeight) / 2;
				imageTop = 0;
			}
			#endregion

			ImageBorder = new Rectangle(imageLeft, imageTop, imageWidth, imageHeight);
			TextBorder = new Rectangle(textLeft, textTop, textWidth, textHeight);
			var newRowWidth = ImageBorder.Width + TextBorder.Width;
			var newRowHeight = (textHeight > imageHeight ? textHeight : imageHeight);

			switch (_parent.FolderBox.FormatState.RowSpace)
			{
				case 2:
					newRowHeight += 5;
					break;
				case 3:
					newRowHeight += 10;
					break;
			}

			if (newRowWidth != RowWidth ||
				newRowHeight != RowHeight)
			{
				RowWidth = newRowWidth;
				RowHeight = newRowHeight;
				_parent.OnInfoChanged();
			}

			_parent.Cells[0].Value = Text;
			_parent.Height = RowHeight;
		}

		public void Dispose()
		{
			_parent = null;
		}
	}
}
