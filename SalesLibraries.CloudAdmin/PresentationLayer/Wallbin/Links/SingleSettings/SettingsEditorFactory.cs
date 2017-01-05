﻿using System;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CloudAdmin.Controllers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	static class SettingsEditorFactory
	{
		public static DialogResult Run(BaseLibraryLink link, LinkSettingsType settingsType)
		{
			var dilogResult = DialogResult.Cancel;
			link.PerformTransaction(link.ParentLibrary.Context,
				linkCopy =>
				{
					var editForm = ObjectIntendHelper.GetObjectInstances(typeof(ILinkSettingsEditForm), null, linkCopy)
						.OfType<ILinkSettingsEditForm>()
						.FirstOrDefault(form => form.EditableSettings.Any(st => st == settingsType));
					if (editForm != null)
					{
						editForm.InitForm<ILinkSettingsEditControl>(settingsType);
						dilogResult = ((Form)editForm).ShowDialog(MainController.Instance.MainForm);
						((Form)editForm).Dispose();
						if (dilogResult == DialogResult.OK)
							linkCopy.MarkAsModified();
						return dilogResult == DialogResult.OK;
					}
					return dilogResult == DialogResult.Cancel;
				},
				copyMethod => MainController.Instance.ProcessManager.Run("Preparing Data...", (cancelationToken, formProgess) => copyMethod()),
				(context, original, current) => MainController.Instance.ProcessManager.Run("Saving Changes...",
					(cancelationToken, formProgess) =>
					{
						original.Save(context, current, false);
					}));
			return dilogResult;
		}

		public static DialogResult Run(ILinksGroup linksGroup, LinkSettingsType settingsType, FileTypes? defaultLinkType = null)
		{
			var dilogResult = DialogResult.Cancel;

			var editForm = ObjectIntendHelper.GetObjectInstances(typeof(ILinkSetSettingsEditForm), null, linksGroup, defaultLinkType)
					.OfType<ILinkSettingsEditForm>()
					.FirstOrDefault(form => form.EditableSettings.Any(st => st == settingsType));
			if (editForm != null)
			{
				editForm.InitForm<ILinkSetSettingsEditControl>(settingsType);
				dilogResult = ((Form)editForm).ShowDialog(MainController.Instance.MainForm);
				((Form)editForm).Dispose();
				if (dilogResult == DialogResult.OK)
					foreach (var link in linksGroup.AllLinks.Where(link => !defaultLinkType.HasValue || link.Type == defaultLinkType.Value))
						link.MarkAsModified();
			}

			return dilogResult;
		}

		public static DialogResult RunEmbedded(LibraryFileLink link, LibraryFolderLink parentLink, LinkSettingsType settingsType)
		{
			var dilogResult = DialogResult.Cancel;
			link.PerformTransaction(link.ParentLibrary.Context,
				linkCopy =>
				{
					using (var editForm = new FormEditLinkSettingsEmbedded(linkCopy, parentLink))
					{
						editForm.InitForm(settingsType);
						dilogResult = editForm.ShowDialog();
						if (dilogResult == DialogResult.OK)
							linkCopy.MarkAsModified();
						return dilogResult == DialogResult.OK;
					}
				},
				copyMethod => MainController.Instance.ProcessManager.Run("Preparing Data...", (cancelationToken, formProgess) => copyMethod()),
				(context, original, current) => MainController.Instance.ProcessManager.Run("Saving Changes...",
					(cancelationToken, formProgess) =>
					{
						current.BeforeSave();
						original.ApplyValues(current);
					}));
			return dilogResult;
		}
	}
}
