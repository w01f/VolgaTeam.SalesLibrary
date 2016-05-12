using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.Common.Helpers;

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
						editForm.InitForm(settingsType);
						dilogResult = ((Form)editForm).ShowDialog(MainController.Instance.MainForm);
						((Form)editForm).Dispose();
						if (dilogResult == DialogResult.OK)
							linkCopy.MarkAsModified();
						return dilogResult == DialogResult.OK;
					}
					return dilogResult == DialogResult.Cancel;
				},
				copyMethod => MainController.Instance.ProcessManager.Run("Preparing Data...", cancelationToken => copyMethod()),
				(context, original, current) => MainController.Instance.ProcessManager.Run("Saving Changes...",
					cancelationToken =>
					{
						original.Save(context, current, false);
					}));
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
				copyMethod => MainController.Instance.ProcessManager.Run("Preparing Data...", cancelationToken => copyMethod()),
				(context, original, current) => MainController.Instance.ProcessManager.Run("Saving Changes...",
					cancelationToken =>
					{
						current.BeforeSave();
						original.ApplyValues(current);
					}));
			return dilogResult;
		}
	}
}
