using System;
using System.Drawing;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Objects.Graphics;
using SalesLibraries.CommonGUI.Wallbin.Views;

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
		public Color BackColor
		{
			get
			{
				if (!_parent.Selected)
					return Link.Folder.Settings.BackgroundWindowColor;
				return _parent.FolderBox.SelectedRowBackColor;
			}
		}
		protected BaseLibraryLink Link
		{
			get { return _parent.Source; }
		}

		protected IWallbinViewFormat FormatState
		{
			get { return _parent.FolderBox.FormatState; }
		}

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
				if (Link.Banner.ShowText)
				{
					imageLeft = 0;
				}
				else
				{
					switch (Link.Banner.ImageAlignement)
					{
						case Alignment.Left:
							imageLeft = 0;
							break;
						case Alignment.Center:
							imageLeft = (_parent.DataGridView.Width - Link.Banner.DisplayedImage.Width) / 2;
							if (imageLeft < 0)
								imageLeft = 0;
							break;
						case Alignment.Right:
							imageLeft = _parent.DataGridView.Width - Link.Banner.DisplayedImage.Width;
							if (imageLeft < 0)
								imageLeft = 0;
							break;
						default:
							imageLeft = 0;
							break;
					}
				}
				imageWidth = Link.Banner.DisplayedImage.Width > DefaultImageWidth ? Link.Banner.DisplayedImage.Width : DefaultImageWidth;
				imageHeight = Link.Banner.DisplayedImage.Height > DefaultImageHeight ? Link.Banner.DisplayedImage.Height : DefaultImageHeight;
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
				imageWidth = Link is LineBreak || !Link.Folder.ContainLinkWidgets ? 0 : DefaultImageWidth;
				imageHeight = DefaultImageHeight;
			}
			#endregion

			#region Text
			Text = !String.IsNullOrEmpty(Link.DisplayName) ? Link.DisplayName : String.Empty;
			#endregion

			#region Font
			Font fontForSizeCalculation;
			if (Link.DisplayFont != null)
			{
				Font = Link.DisplayFont;
				fontForSizeCalculation = Link.DisplayFont;
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
				fontForSizeCalculation = _parent.FolderBox.BoldItalicUndrerlineRowFont;
			}
			else
			{
				Font = _parent.FolderBox.RegularRowFont;
				fontForSizeCalculation = _parent.FolderBox.BoldRowFont;
			}
			#endregion

			#region Text Size and Coordinates
			SizeF textSize;
			int textLeft;
			int textTop;
			int textWidth;
			int textHeight;
			using (var g = Graphics.FromImage(new Bitmap(1, 1)))
			{
				textSize = g.MeasureString(Text, fontForSizeCalculation, Int32.MaxValue);
			}
			if (Link.Banner.Enable)
			{
				textLeft = imageLeft + imageWidth;
				textWidth = (Int32)textSize.Width;
				textHeight = (Int32)textSize.Height;
			}
			else
			{
				textLeft = imageLeft + imageWidth + WidthMargin;
				textWidth = (int)textSize.Width;
				textHeight = (int)textSize.Height;
			}

			//TODO Hack code. Refactor this!
			textWidth += 10;
			#endregion

			#region Fore Color
			ForeColor = Link.DisplayColor;
			#endregion

			#region Correct Image and text coordinates
			if (textHeight > imageHeight)
			{
				textTop = 0;
				imageTop = (textHeight - imageHeight) / 2;
			}
			else if (textHeight == imageHeight)
			{
				textTop = 0;
				imageTop = 0;
			}
			else
			{
				textTop = (imageHeight - textHeight) / 2;
				imageTop = 0;
			}
			#endregion

			ImageBorder = new Rectangle(imageLeft, imageTop, imageWidth, imageHeight);
			TextBorder = new Rectangle(textLeft, textTop, textWidth, textHeight);
			var newRowWidth = ImageBorder.Width + TextBorder.Width + 10;
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
