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
		private readonly Dictionary<Guid, CustomKeywords> _keywordsCopy = new Dictionary<Guid, CustomKeywords>();
		private readonly Dictionary<Guid, FileCard> _fileCardsCopy = new Dictionary<Guid, FileCard>();
		private readonly Dictionary<Guid, AttachmentProperties> _attachmentsCopy = new Dictionary<Guid, AttachmentProperties>();
		private readonly Dictionary<Guid, bool> _securityRestrictedCopy = new Dictionary<Guid, bool>();
		private readonly Dictionary<Guid, string> _securityAssignedUsersCopy = new Dictionary<Guid, string>();

		private bool _categoriesChanged;
		private bool _keywordsChanged;
		private bool _fileCardsChanged;
		private bool _attachmentsChanged;
		private bool _securityChanged;

		public TagsCleaner()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			if (!((CreateGraphics()).DpiX > 96)) return;
			buttonXCategories.Font = new Font(buttonXCategories.Font.FontFamily, buttonXCategories.Font.Size - 2, buttonXCategories.Font.Style);
			buttonXKeywords.Font = new Font(buttonXKeywords.Font.FontFamily, buttonXKeywords.Font.Size - 2, buttonXKeywords.Font.Style);
			buttonXFileCards.Font = new Font(buttonXFileCards.Font.FontFamily, buttonXFileCards.Font.Size - 2, buttonXFileCards.Font.Style);
			buttonXFileAttachments.Font = new Font(buttonXFileAttachments.Font.FontFamily, buttonXFileAttachments.Font.Size - 2, buttonXFileAttachments.Font.Style);
			buttonXWebAttachments.Font = new Font(buttonXWebAttachments.Font.FontFamily, buttonXWebAttachments.Font.Size - 2, buttonXWebAttachments.Font.Style);
			buttonXSecurity.Font = new Font(buttonXSecurity.Font.FontFamily, buttonXSecurity.Font.Size - 2, buttonXSecurity.Font.Style);
			laWarning.Font = new Font(laWarning.Font.FontFamily, laWarning.Font.Size - 3, laWarning.Font.Style);
			laWarningDescription.Font = new Font(laWarningDescription.Font.FontFamily, laWarningDescription.Font.Size - 2, laWarningDescription.Font.Style);
		}

		#region ITagsEditor Members
		public event EventHandler<EventArgs> EditorChanged;
		public void UpdateData()
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;

			_categoriesChanged = false;
			_keywordsChanged = false;
			_fileCardsChanged = false;
			_attachmentsChanged = false;
			_securityChanged = false;

			_categoriesCopy.Clear();
			_keywordsCopy.Clear();
			_fileCardsCopy.Clear();
			_attachmentsCopy.Clear();
			_securityRestrictedCopy.Clear();
			_securityAssignedUsersCopy.Clear();
			foreach (var link in activePage.Page.Folders.SelectMany(folder => folder.Files))
			{
				var categoryCopy = new SearchTags();
				categoryCopy.SearchGroups.AddRange(link.SearchTags.SearchGroups);
				_categoriesCopy.Add(link.Identifier, categoryCopy);

				var keywordsCopy = new CustomKeywords();
				keywordsCopy.Tags.AddRange(link.CustomKeywords.Tags);
				_keywordsCopy.Add(link.Identifier, keywordsCopy);

				_fileCardsCopy.Add(link.Identifier, link.FileCard.Clone(link));

				_attachmentsCopy.Add(link.Identifier, link.AttachmentProperties.Clone(link));

				_securityRestrictedCopy.Add(link.Identifier, link.IsRestricted);
				_securityAssignedUsersCopy.Add(link.Identifier, link.AssignedUsers);
			}
		}
		public void ApplyData()
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			foreach (var link in activePage.Page.Folders.SelectMany(folder => folder.Files))
			{
				if (_categoriesChanged)
				{
					link.SearchTags.SearchGroups.Clear();
					link.SearchTags.SearchGroups.AddRange(_categoriesCopy[link.Identifier].SearchGroups);
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
					link.AssignedUsers  = _securityAssignedUsersCopy[link.Identifier];
				}
			}
			UpdateData();
		}
		#endregion

		private void buttonXCategories_Click(object sender, EventArgs e)
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are You Sure ?") != DialogResult.Yes) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you ABSOLUTELY 100% POSITIVE?") != DialogResult.Yes) return;
			foreach (var category in _categoriesCopy.Values)
				category.SearchGroups.Clear();
			_categoriesChanged = true;
			activePage.Parent.StateChanged = true;
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
		}

		private void buttonXKeywords_Click(object sender, EventArgs e)
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are You Sure ?") != DialogResult.Yes) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you ABSOLUTELY 100% POSITIVE?") != DialogResult.Yes) return;
			foreach (var keywords in _keywordsCopy.Values)
				keywords.Tags.Clear();
			_keywordsChanged = true;
			activePage.Parent.StateChanged = true;
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
		}

		private void buttonXFileCards_Click(object sender, EventArgs e)
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are You Sure ?") != DialogResult.Yes) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you ABSOLUTELY 100% POSITIVE?") != DialogResult.Yes) return;
			foreach (var fileCard in _fileCardsCopy.Values)
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
			activePage.Parent.StateChanged = true;
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
		}

		private void buttonXFileAttachments_Click(object sender, EventArgs e)
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are You Sure ?") != DialogResult.Yes) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you ABSOLUTELY 100% POSITIVE?") != DialogResult.Yes) return;
			foreach (var attatchment in _attachmentsCopy.Values)
				attatchment.FilesAttachments.Clear();
			_attachmentsChanged = true;
			activePage.Parent.StateChanged = true;
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
		}

		private void buttonXWebAttachments_Click(object sender, EventArgs e)
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are You Sure ?") != DialogResult.Yes) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you ABSOLUTELY 100% POSITIVE?") != DialogResult.Yes) return;
			foreach (var attatchment in _attachmentsCopy.Values)
				attatchment.WebAttachments.Clear();
			_attachmentsChanged = true;
			activePage.Parent.StateChanged = true;
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
		}

		private void buttonXSecurity_Click(object sender, EventArgs e)
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are You Sure ?") != DialogResult.Yes) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you ABSOLUTELY 100% POSITIVE?") != DialogResult.Yes) return;
			foreach (var restrictedPair in _securityRestrictedCopy)
				_securityRestrictedCopy[restrictedPair.Key] = false;
			foreach (var assignedUsersPair in _securityAssignedUsersCopy)
				_securityAssignedUsersCopy[assignedUsersPair.Key] = null;
			_securityChanged = true;
			activePage.Parent.StateChanged = true;
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
		}
	}
}
