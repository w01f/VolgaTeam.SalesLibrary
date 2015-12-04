﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.SalesDepot.Business.LinkViewers;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.EmailBin
{
	public partial class EmailBinControl : UserControl
	{
		private bool _isLoading;

		public EmailBinControl()
		{
			InitializeComponent();
		}

		public void InitControl()
		{
			_isLoading = true;
			LoadLinks();
			LoadEmailBinOptions();
			UpdateEmailBinButtons();
			MainController.Instance.EmailBin.ListChanged += (o, e) =>
			{
				LoadLinks();
				UpdateEmailBinButtons();
			};
			_isLoading = false;
		}

		private void LoadLinks()
		{
			gridControlFiles.DataSource = MainController.Instance.EmailBin.EmailLinks;
			gridViewFiles.RefreshData();
		}

		private void LoadEmailBinOptions()
		{
			buttonXPDF.Checked = MainController.Instance.Settings.EmailBinSettings.EmailBinSendAsPdf;
			buttonXZip.Checked = MainController.Instance.Settings.EmailBinSettings.EmailBinSendAsZip;
		}

		private void UpdateEmailBinButtons()
		{
			buttonXCreateEmail.Enabled = MainController.Instance.EmailBin.EmailLinks.Any();
			buttonXEmptyEmailBin.Enabled = MainController.Instance.EmailBin.EmailLinks.Any();
			buttonXPDF.Enabled = MainController.Instance.EmailBin.EmailLinks.Any();
			buttonXZip.Enabled = MainController.Instance.EmailBin.EmailLinks.Any();
		}

		private void buttonXPDF_CheckedChanged(object sender, EventArgs e)
		{
			if (_isLoading) return;
			MainController.Instance.Settings.EmailBinSettings.EmailBinSendAsPdf = buttonXPDF.Checked;
			MainController.Instance.Settings.SaveSettings();
		}

		private void buttonXZip_CheckedChanged(object sender, EventArgs e)
		{
			if (_isLoading) return;
			MainController.Instance.Settings.EmailBinSettings.EmailBinSendAsZip = buttonXZip.Checked;
			MainController.Instance.Settings.SaveSettings();
		}

		private void repositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure you want to remove from email bin?") == DialogResult.Yes)
			{
				gridViewFiles.CloseEditor();
				var focussedLink = gridViewFiles.GetFocusedRow() as LibraryFileLink;
				MainController.Instance.EmailBin.RemoveLink(focussedLink);
				UpdateEmailBinButtons();
			}
		}

		private void buttonXEmptyEmailBin_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure you want to remove ALL from email bin?") == DialogResult.Yes)
			{
				MainController.Instance.EmailBin.ClearAll();
				UpdateEmailBinButtons();
			}
		}

		private void buttonXCreateEmail_Click(object sender, EventArgs e)
		{
			using (var powerPointProcessor = new PowerPointHidden())
			{
				if (!powerPointProcessor.Connect()) return;

				var emailFiles = new List<string>();
				if (buttonXPDF.Checked)
				{
					var convertResult = true;
					foreach (var item in MainController.Instance.EmailBin.EmailLinks)
					{
						Application.DoEvents();
						switch (item.Type)
						{
							case FileTypes.PowerPoint:
								var pdfFileName = Path.Combine(RemoteResourceManager.Instance.TempFolder.LocalPath, Path.GetFileNameWithoutExtension(item.FullPath) + ".pdf");
								if (powerPointProcessor.ExportPresentationAsPdf(item.FullPath, pdfFileName))
								{
									if (File.Exists(pdfFileName))
										emailFiles.Add(pdfFileName);
								}
								else
								{
									convertResult = false;
									emailFiles.Add(item.FullPath);
								}
								break;
							default:
								emailFiles.Add(item.FullPath);
								break;
						}
					}
					if (!convertResult)
						if (MainController.Instance.PopupMessages.ShowWarningQuestion("Some Power Point files were not converted to PDF.\nDo you want to send them in original format?") != DialogResult.Yes)
							return;
				}
				else
					emailFiles.AddRange(MainController.Instance.EmailBin.EmailLinks.Select(l => l.FullPath));

				if (emailFiles.Count > 0 && MainController.Instance.Settings.EmailBinSettings.EmailBinSendAsZip)
				{
					using (var form = new FormZipFileName())
					{
						if (form.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK)
						{
							string compressedFilesPath = Path.Combine(RemoteResourceManager.Instance.TempFolder.LocalPath, form.FileName + ".zip");
							Utils.CompressFiles(emailFiles, compressedFilesPath);
							emailFiles.Clear();
							emailFiles.Add(compressedFilesPath);
						}
						else
							emailFiles.Clear();
					}
				}
				if (emailFiles.Count > 0)
					LinkManager.EmailFiles(emailFiles.ToArray());
			}
		}
	}
}
