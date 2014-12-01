using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using FileManager.ConfigurationClasses;
using FileManager.PresentationClasses.WallBin.LinkProperties;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.ToolForms.WallBin
{
	public enum LinkPropertiesType
	{
		Notes,
		Tags,
		ExpirationDate,
		Security,
		Widget,
		Banner,
		AdvancedSettings,
	}

	public partial class FormLinkProperties : MetroForm
	{
		public FormLinkProperties()
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
			{
				var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.AppearanceDisabled.Font = styleControllerFont;
				styleController.AppearanceDropDown.Font = styleControllerFont;
				styleController.AppearanceDropDownHeader.Font = styleControllerFont;
				styleController.AppearanceFocused.Font = styleControllerFont;
				styleController.AppearanceReadOnly.Font = styleControllerFont;
				xtraTabControl.AppearancePage.HeaderActive.Font = new Font(xtraTabControl.AppearancePage.HeaderActive.Font.FontFamily, xtraTabControl.AppearancePage.HeaderActive.Font.Size - 2, xtraTabControl.AppearancePage.HeaderActive.Font.Style);
				xtraTabControl.AppearancePage.Header.Font = new Font(xtraTabControl.AppearancePage.Header.Font.FontFamily, xtraTabControl.AppearancePage.Header.Font.Size - 2, xtraTabControl.AppearancePage.Header.Font.Style);
				xtraTabControl.AppearancePage.HeaderDisabled.Font = new Font(xtraTabControl.AppearancePage.HeaderDisabled.Font.FontFamily, xtraTabControl.AppearancePage.HeaderDisabled.Font.Size - 2, xtraTabControl.AppearancePage.HeaderDisabled.Font.Style);
				xtraTabControl.AppearancePage.HeaderHotTracked.Font = new Font(xtraTabControl.AppearancePage.HeaderHotTracked.Font.FontFamily, xtraTabControl.AppearancePage.HeaderHotTracked.Font.Size - 2, xtraTabControl.AppearancePage.HeaderHotTracked.Font.Style);
			}
		}

		public static DialogResult ShowProperties(LibraryLink data, LinkPropertiesType propertiesType, bool isEmbedded = false)
		{
			DialogResult dilogResult;
			using (var form = new FormLinkProperties())
			{
				form.Width = 680;
				form.Height = 630;
				form.Text = String.IsNullOrEmpty(data.PropertiesName) && data.Type == FileTypes.LineBreak ?
					"Line Break" :
					data.PropertiesName;
				form.StartPosition = FormStartPosition.CenterScreen;
				var optionPages = new List<ILinkProperties>();
				switch (propertiesType)
				{
					case LinkPropertiesType.Notes:
						if (isEmbedded)
							form.Height = 300;
						if (data.Type == FileTypes.LineBreak)
							optionPages.Add(new LineBreakOptions(data));
						else if (data.Type == FileTypes.BuggyPresentation ||
							 data.Type == FileTypes.FriendlyPresentation ||
							 data.Type == FileTypes.Presentation ||
							 data.Type == FileTypes.PDF ||
							 data.Type == FileTypes.Word ||
							 (data.Type == FileTypes.Other && new[] { "ppt", "doc", "pdf" }.Contains(data.Format)))
						{
							if (isEmbedded)
								optionPages.Add(new SlideEmbeddedLinkOptions(data));
							else
								optionPages.Add(new SlideLinkOptions(data));
						}
						else if (data.Type == FileTypes.Other && new[] { "xls" }.Contains(data.Format))
						{
							if (isEmbedded)
								optionPages.Add(new ExcelEmbeddedLinkOptions(data));
							else
								optionPages.Add(new ExcelLinkOptions(data));
						}
						else if (data.Type == FileTypes.MediaPlayerVideo || data.Type == FileTypes.QuickTimeVideo)
						{
							if (isEmbedded)
								optionPages.Add(new VideoEmbeddedLinkOptions(data));
							else
								optionPages.Add(new VideoLinkOptions(data));
						}
						else if (data.Type == FileTypes.Url)
						{
							optionPages.Add(new WebLinkOptions(data));
						}
						else if (data.Type == FileTypes.Folder)
						{
							optionPages.Add(new LinkBaseOptions(data));
						}
						else if (!isEmbedded)
							optionPages.Add(new LinkBaseOptions(data));
						else
							AppManager.Instance.ShowWarning("Settings are not available for files of this type");
						break;
					case LinkPropertiesType.Tags:
						optionPages.Add(new TagsOptions(data));
						break;
					case LinkPropertiesType.ExpirationDate:
						optionPages.Add(new ExpiredDateOptions(data));
						break;
					case LinkPropertiesType.Security:
						optionPages.Add(new SecurityOptions(data));
						break;
					case LinkPropertiesType.Widget:
						form.Width = 940;
						form.Height = 750;
						optionPages.Add(new WidgetOptions(data));
						break;
					case LinkPropertiesType.Banner:
						form.Width = 940;
						form.Height = 750;
						optionPages.Add(new BannerOptions(data));
						break;
					case LinkPropertiesType.AdvancedSettings:
						if (data is LibraryFolderLink)
							optionPages.Add(new FolderFilesOptions((LibraryFolderLink)data));
						break;
				}
				form.AddOptionPages(optionPages);
				dilogResult = form.ShowDialog(FormMain.Instance);
			}
			if (dilogResult == DialogResult.OK)
				data.LastChanged = DateTime.Now;
			return dilogResult;
		}

		public void AddOptionPages(IEnumerable<ILinkProperties> pages)
		{
			var optionPages = pages.ToArray();
			foreach (var optionPage in optionPages)
			{
				optionPage.OnForseClose += (o, e) => ForceClose();
				//AssignCloseActiveEditorsonOutSideClick((XtraTabPage)optionPage);
			}
			xtraTabControl.ShowTabHeader = optionPages.Length > 1 ? DefaultBoolean.True : DefaultBoolean.False;
			xtraTabControl.TabPages.AddRange(pages.OfType<XtraTabPage>().ToArray());
		}

		private void ForceClose()
		{
			foreach (var optionPage in xtraTabControl.TabPages.OfType<ILinkProperties>())
				optionPage.SaveData();
			DialogResult = DialogResult.OK;
			Close();
		}

		private void xtraTabControl_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			hyperLinkEditRequestNewCategories.Visible = e.Page is TagsOptions &&
				!String.IsNullOrEmpty(SettingsManager.Instance.CategoryRequestRecipients) &&
				!String.IsNullOrEmpty(SettingsManager.Instance.CategoryRequestSubject) &&
				!String.IsNullOrEmpty(SettingsManager.Instance.CategoryRequestBody);
		}

		private void btOK_Click(object sender, EventArgs e)
		{
			ForceClose();
		}

		private void hyperLinkEditRequestNewCategories_OpenLink(object sender, OpenLinkEventArgs e)
		{
			try { Process.Start(String.Format("mailto:{0}?subject={1}&body={2}", SettingsManager.Instance.CategoryRequestRecipients, SettingsManager.Instance.CategoryRequestSubject, SettingsManager.Instance.CategoryRequestBody)); }
			catch { }
			e.Handled = true;
		}
	}
}