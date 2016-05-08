using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	static class SettingsEditorFactory
	{
		public static DialogResult Run(BaseLibraryLink link, LinkSettingsType settingsType, bool isEmbedded = false)
		{
			var dilogResult = DialogResult.Cancel;
			link.PerformTransaction(link.ParentLibrary.Context,
				linkCopy =>
				{
					var editForm = ObjectIntendHelper.GetObjectInstances(typeof(ILinkSettingsEditForm), null, linkCopy)
						.OfType<ILinkSettingsEditForm>()
						.FirstOrDefault(form => form.EditableSettings.Any(st => st == settingsType) && form.IsForEmbedded == isEmbedded);
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
						if (isEmbedded)
						{
							current.BeforeSave();
							original.ApplyValues(current);
						}
						else
							original.Save(context, current, false);
					}));
			return dilogResult;
		}
	}
}
