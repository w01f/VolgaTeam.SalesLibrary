using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	class DocumentSettingsEditor : BaseContextMenuEditor
	{
		public BarCheckItem ItemIsArchiveResource { get; private set; }
		public BarCheckItem ItemDoNotGeneratePreview { get; private set; }
		public BarCheckItem ItemDoNotGenerateContentText { get; private set; }
		public BarCheckItem ItemForcePreview { get; private set; }

		private DocumentSettingsLoader DocumentSettingsLoader => (DocumentSettingsLoader)_linkLoader;

		public DocumentSettingsEditor(BarSubItem itemsContainer) : base(itemsContainer)
		{
			InitContextMenu();
		}

		protected override ILinkLoader CreateLoader(IList<BaseLibraryLink> targetLinks)
		{
			return new DocumentSettingsLoader(this, targetLinks.OfType<LibraryObjectLink>().ToList());
		}

		protected override void PopulateContextMenu()
		{
			if (!DocumentSettingsLoader.TargetLinks.OfType<DocumentLink>().Any()) return;

			_barManager.BeginInit();
			ItemsContainer.LinksPersistInfo.Clear();
			ItemsContainer.LinksPersistInfo.AddRange(new[]
			{
				new LinkPersistInfo(ItemIsArchiveResource),
				new LinkPersistInfo(ItemDoNotGeneratePreview),
				new LinkPersistInfo(ItemDoNotGenerateContentText),
				new LinkPersistInfo(ItemForcePreview),
			});
			_barManager.EndInit();
		}

		private void InitContextMenu()
		{
			_barManager.BeginInit();

			var maxId = _barManager.MaxItemId++;

			ItemIsArchiveResource = new BarCheckItem
			{
				Id = maxId,
				Caption = "Archive Resource",
				CheckBoxVisibility = CheckBoxVisibility.AfterText
			};
			ItemIsArchiveResource.CheckedChanged += (o, e) => DocumentSettingsLoader.OnValuesChanged();
			maxId++;

			ItemDoNotGeneratePreview = new BarCheckItem
			{
				Id = maxId,
				Caption = "No PNG Preview",
				CheckBoxVisibility = CheckBoxVisibility.AfterText
			};
			ItemDoNotGeneratePreview.CheckedChanged += (o, e) => DocumentSettingsLoader.OnValuesChanged();
			maxId++;

			ItemDoNotGenerateContentText = new BarCheckItem
			{
				Id = maxId,
				Caption = "No Data File",
				CheckBoxVisibility = CheckBoxVisibility.AfterText
			};
			ItemDoNotGenerateContentText.CheckedChanged += (o, e) => DocumentSettingsLoader.OnValuesChanged();
			maxId++;

			ItemForcePreview = new BarCheckItem
			{
				Id = maxId,
				Caption = "Launch Browser Tab",
				CheckBoxVisibility = CheckBoxVisibility.AfterText
			};
			ItemForcePreview.CheckedChanged += (o, e) => DocumentSettingsLoader.OnValuesChanged();
			maxId++;

			_barManager.Items.Add(ItemIsArchiveResource);
			_barManager.Items.Add(ItemDoNotGeneratePreview);
			_barManager.Items.Add(ItemDoNotGenerateContentText);
			_barManager.Items.Add(ItemForcePreview);
			_barManager.MaxItemId = maxId;

			_barManager.EndInit();
		}
	}
}
