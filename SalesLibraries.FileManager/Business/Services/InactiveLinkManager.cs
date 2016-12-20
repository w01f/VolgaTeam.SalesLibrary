using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.FileManager.Business.Models.InactiveLinks;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Libraries;

namespace SalesLibraries.FileManager.Business.Services
{
	class InactiveLinkManager
	{
		public static InactiveLinkManager Instance { get; } = new InactiveLinkManager();

		public List<LibraryFileLink> DeadLinks { get; }
		public List<LibraryObjectLink> ExpiredLinks { get; }

		private InactiveLinkManager()
		{
			DeadLinks = new List<LibraryFileLink>();
			ExpiredLinks = new List<LibraryObjectLink>();
		}

		public void Load(IEnumerable<Library> libraries)
		{
			DeadLinks.Clear();
			ExpiredLinks.Clear();
			foreach (var library in libraries)
				LoadLinksInternal(library);
		}

		public void Load(Library library)
		{
			DeadLinks.Clear();
			ExpiredLinks.Clear();
			LoadLinksInternal(library);
		}

		public void FixLinks()
		{
			var inactiveLinks = new List<InactiveLink>();
			inactiveLinks.AddRange(
				DeadLinks
					.Select(deadLink => new InactiveLink(deadLink)));
			inactiveLinks.AddRange(
				ExpiredLinks
					.Where(expiredLink => !inactiveLinks.Any(inactiveLink => inactiveLink.Link.ExtId.Equals(expiredLink.ExtId)))
					.Select(expiredLink => new InactiveLink(expiredLink)));
			using (var form = new FormDeleteInactiveLinks(inactiveLinks))
			{
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				var deletedLinks = form.InactiveLinks
					.Where(link => link.IsDeleted)
					.Select(link => link.Link)
					.ToList();
				var changedLibraries = form.InactiveLinks
					.Where(link => link.IsDeleted || link.IsChanged)
					.Select(link => link.Link.ParentLibrary)
					.Distinct()
					.ToList();
				MainController.Instance.ProcessManager.Run("Saving Changes...", cancelationToken =>
				{
					foreach (var deletedLink in deletedLinks)
						deletedLink.DeleteLink(true);
					foreach (var library in changedLibraries)
						library.Context.SaveChanges();
				});
			}
		}

		public void NotifyAboutExpiredLinks(InactiveLinksSettings settings)
		{
			if (!ExpiredLinks.Any(link => link.ExpirationSettings.SendEmailOnSync)) return;
			if (OutlookHelper.Instance.Connect())
			{
				OutlookHelper.Instance.CreateMessage(settings.EmailList, string.Join(Environment.NewLine, ExpiredLinks.Where(link => link.ExpirationSettings.SendEmailOnSync).Select(link => link.FullPath)), settings.SendEmail);
				OutlookHelper.Instance.Disconnect();
			}
			else
				MainController.Instance.PopupMessages.ShowWarning("Cannot open Outlook");
		}

		private void LoadLinksInternal(Library library)
		{
			var deadLinks = library.Pages.SelectMany(p => p.TopLevelLinks).GetDeadLinks().ToList();
			DeadLinks.AddRange(deadLinks);
			var expiredLinks = library.Pages.SelectMany(p => p.TopLevelLinks).GetExpiredLinks().ToList();
			ExpiredLinks.AddRange(expiredLinks);
			if (deadLinks.Any() || expiredLinks.Any())
				library.Context.SaveChanges();
		}
	}
}
