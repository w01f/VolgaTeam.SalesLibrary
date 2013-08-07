using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FileManager.Controllers;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.Tags
{
	[ToolboxItem(false)]
	public partial class TagsCleaner : UserControl, ITagsEditor
	{
		private readonly Dictionary<Guid, SearchTags> _categoriesCopy = new Dictionary<Guid, SearchTags>();
		private readonly Dictionary<Guid, List<string>> _superFiltersCopy = new Dictionary<Guid, List<string>>();
		private readonly Dictionary<Guid, CustomKeywords> _keywordsCopy = new Dictionary<Guid, CustomKeywords>();
		private readonly Dictionary<Guid, FileCard> _fileCardsCopy = new Dictionary<Guid, FileCard>();
		private readonly Dictionary<Guid, AttachmentProperties> _attachmentsCopy = new Dictionary<Guid, AttachmentProperties>();
		private readonly Dictionary<Guid, bool> _securityRestrictedCopy = new Dictionary<Guid, bool>();
		private readonly Dictionary<Guid, bool> _securityNoShareCopy = new Dictionary<Guid, bool>();
		private readonly Dictionary<Guid, string> _securityAssignedUsersCopy = new Dictionary<Guid, string>();

		private bool _categoriesChanged;
		private bool _superFiltersChanged;
		private bool _keywordsChanged;
		private bool _fileCardsChanged;
		private bool _attachmentsChanged;
		private bool _securityChanged;
		private bool _needToApply;
		public bool NeedToApply
		{
			get { return _needToApply; }
			set
			{
				_needToApply = value;
				var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
				if (activePage != null) activePage.Parent.StateChanged = true;
			}
		}
		public TagsCleaner()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		#region ITagsEditor Members
		public event EventHandler<EventArgs> EditorChanged;
		public void UpdateData()
		{
			if (MainController.Instance.ActiveDecorator == null) return;

			_categoriesChanged = false;
			_superFiltersChanged = false;
			_keywordsChanged = false;
			_fileCardsChanged = false;
			_attachmentsChanged = false;
			_securityChanged = false;

			_categoriesCopy.Clear();
			_superFiltersCopy.Clear();
			_keywordsCopy.Clear();
			_fileCardsCopy.Clear();
			_attachmentsCopy.Clear();
			_securityRestrictedCopy.Clear();
			_securityNoShareCopy.Clear();
			_securityAssignedUsersCopy.Clear();
			foreach (var link in MainController.Instance.ActiveDecorator.Library.Pages.SelectMany(p => p.Folders.SelectMany(folder => folder.Files)))
			{
				var categoryCopy = new SearchTags();
				categoryCopy.SearchGroups.AddRange(link.SearchTags.SearchGroups);
				_categoriesCopy.Add(link.Identifier, categoryCopy);

				_superFiltersCopy.Add(link.Identifier, new List<string>(link.SuperFilters.Select(sf => sf.Name)));

				var keywordsCopy = new CustomKeywords();
				keywordsCopy.Tags.AddRange(link.CustomKeywords.Tags);
				_keywordsCopy.Add(link.Identifier, keywordsCopy);

				_fileCardsCopy.Add(link.Identifier, link.FileCard.Clone(link));

				_attachmentsCopy.Add(link.Identifier, link.AttachmentProperties.Clone(link));

				_securityRestrictedCopy.Add(link.Identifier, link.IsRestricted);
				_securityNoShareCopy.Add(link.Identifier, link.NoShare);
				_securityAssignedUsersCopy.Add(link.Identifier, link.AssignedUsers);
			}
		}
		public void ApplyData()
		{
			if (MainController.Instance.ActiveDecorator == null) return;
			foreach (var link in MainController.Instance.ActiveDecorator.Library.Pages.SelectMany(p => p.Folders.SelectMany(folder => folder.Files)))
			{
				if (_categoriesChanged)
				{
					link.SearchTags.SearchGroups.Clear();
					link.SearchTags.SearchGroups.AddRange(_categoriesCopy[link.Identifier].SearchGroups);
				}

				if (_superFiltersChanged)
				{
					link.SuperFilters.Clear();
					link.SuperFilters.AddRange(_superFiltersCopy[link.Identifier].Select(it => new SuperFilter() { Name = it }));
				}

				if (_keywordsChanged)
				{
					link.CustomKeywords.Tags.Clear();
					link.CustomKeywords.Tags.AddRange(_keywordsCopy[link.Identifier].Tags);
				}

				if (_fileCardsChanged)
					link.FileCard = _fileCardsCopy[link.Identifier];
				if (_attachmentsChanged)
					link.AttachmentProperties = _attachmentsCopy[link.Identifier];

				if (_securityChanged)
				{
					link.IsRestricted = _securityRestrictedCopy[link.Identifier];
					link.NoShare = _securityNoShareCopy[link.Identifier];
					link.AssignedUsers = _securityAssignedUsersCopy[link.Identifier];
				}
			}
			UpdateData();
			MainController.Instance.ActiveDecorator.StateChanged = true;
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
			MainController.Instance.WallbinController.UpdateTagCountInfo();
		}
		#endregion

		private void ClearCategories(bool allPages)
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are You Sure ?") != DialogResult.Yes) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you ABSOLUTELY 100% POSITIVE?") != DialogResult.Yes) return;
			foreach (var category in _categoriesCopy.Where(it => allPages || activePage.Page.Folders.SelectMany(f => f.Files.Select(l => l.Identifier)).Contains(it.Key)).Select(it => it.Value))
				category.SearchGroups.Clear();
			_categoriesChanged = true;
			NeedToApply = true;
		}

		private void ClearSuperFilters(bool allPages)
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are You Sure ?") != DialogResult.Yes) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you ABSOLUTELY 100% POSITIVE?") != DialogResult.Yes) return;
			foreach (var superFilters in _superFiltersCopy.Where(it => allPages || activePage.Page.Folders.SelectMany(f => f.Files.Select(l => l.Identifier)).Contains(it.Key)).Select(it => it.Value))
				superFilters.Clear();
			_superFiltersChanged = true;
			NeedToApply = true;
		}

		private void ClearKeywords(bool allPages)
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are You Sure ?") != DialogResult.Yes) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you ABSOLUTELY 100% POSITIVE?") != DialogResult.Yes) return;
			foreach (var keywords in _keywordsCopy.Where(it => allPages || activePage.Page.Folders.SelectMany(f => f.Files.Select(l => l.Identifier)).Contains(it.Key)).Select(it => it.Value))
				keywords.Tags.Clear();
			_keywordsChanged = true;
			NeedToApply = true;
		}

		private void ClearFileCards(bool allPages)
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are You Sure ?") != DialogResult.Yes) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you ABSOLUTELY 100% POSITIVE?") != DialogResult.Yes) return;
			foreach (var fileCard in _fileCardsCopy.Where(it => allPages || activePage.Page.Folders.SelectMany(f => f.Files.Select(l => l.Identifier)).Contains(it.Key)).Select(it => it.Value))
			{
				fileCard.Enable = false;
				fileCard.Title = string.Empty;
				fileCard.Advertiser = null;
				fileCard.DateSold = null;
				fileCard.BroadcastClosed = null;
				fileCard.DigitalClosed = null;
				fileCard.PublishingClosed = null;
				fileCard.SalesName = null;
				fileCard.SalesEmail = null;
				fileCard.SalesPhone = null;
				fileCard.SalesStation = null;
				fileCard.Notes.Clear();
			}
			_fileCardsChanged = true;
			NeedToApply = true;
		}

		private void ClearFileAttachments(bool allPages)
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are You Sure ?") != DialogResult.Yes) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you ABSOLUTELY 100% POSITIVE?") != DialogResult.Yes) return;
			foreach (var attatchment in _attachmentsCopy.Where(it => allPages || activePage.Page.Folders.SelectMany(f => f.Files.Select(l => l.Identifier)).Contains(it.Key)).Select(it => it.Value))
				attatchment.FilesAttachments.Clear();
			_attachmentsChanged = true;
			NeedToApply = true;
		}

		private void ClearWebAttachments(bool allPages)
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are You Sure ?") != DialogResult.Yes) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you ABSOLUTELY 100% POSITIVE?") != DialogResult.Yes) return;
			foreach (var attatchment in _attachmentsCopy.Where(it => allPages || activePage.Page.Folders.SelectMany(f => f.Files.Select(l => l.Identifier)).Contains(it.Key)).Select(it => it.Value))
				attatchment.WebAttachments.Clear();
			_attachmentsChanged = true;
			NeedToApply = true;
		}

		private void ClearSecurity(bool allPages)
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are You Sure ?") != DialogResult.Yes) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you ABSOLUTELY 100% POSITIVE?") != DialogResult.Yes) return;
			foreach (var restrictedPair in _securityRestrictedCopy.Where(it => allPages || activePage.Page.Folders.SelectMany(f => f.Files.Select(l => l.Identifier)).Contains(it.Key)))
				_securityRestrictedCopy[restrictedPair.Key] = false;
			foreach (var noSharePair in _securityNoShareCopy.Where(it => allPages || activePage.Page.Folders.SelectMany(f => f.Files.Select(l => l.Identifier)).Contains(it.Key)))
				_securityNoShareCopy[noSharePair.Key] = false;
			foreach (var assignedUsersPair in _securityAssignedUsersCopy.Where(it => allPages || activePage.Page.Folders.SelectMany(f => f.Files.Select(l => l.Identifier)).Contains(it.Key)))
				_securityAssignedUsersCopy[assignedUsersPair.Key] = null;
			_securityChanged = true;
			NeedToApply = true;
		}

		private void hyperLinkEditCategoriesActivePage_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearCategories(false);
			e.Handled = true;
		}

		private void hyperLinkEditCategoriesAllPages_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearCategories(true);
			e.Handled = true;
		}

		private void hyperLinkEditSuperFiltersActivePage_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearSuperFilters(false);
			e.Handled = true;
		}

		private void hyperLinkEditSuperFiltersAllPages_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearSuperFilters(true);
			e.Handled = true;
		}

		private void hyperLinkEditKeywordsActivePage_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearKeywords(false);
			e.Handled = true;
		}

		private void hyperLinkEditKeywordsAllPages_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearKeywords(true);
			e.Handled = true;
		}

		private void hyperLinkEditFileCardsActivePage_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearFileCards(false);
			e.Handled = true;
		}

		private void hyperLinkEditFileCardsAllPages_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearFileCards(true);
			e.Handled = true;
		}

		private void hyperLinkEditFileAttachmentsActivePage_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearFileAttachments(false);
			e.Handled = true;
		}

		private void hyperLinkEditFileAttachmentsAllPages_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearFileAttachments(true);
			e.Handled = true;
		}

		private void hyperLinkEditWebAttachmentsActivePage_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearWebAttachments(false);
			e.Handled = true;
		}

		private void hyperLinkEditWebAttachmentsAllPages_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearWebAttachments(true);
			e.Handled = true;
		}

		private void hyperLinkEditSecurityActivePage_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearSecurity(false);
			e.Handled = true;
		}

		private void hyperLinkEditSecurityAllPages_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearSecurity(true);
			e.Handled = true;
		}
	}
}
