﻿using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	class DocumentSettingsEditor : BaseContextMenuEditor
	{
		public BarCheckItem ItemDoNotGeneratePreview { get; private set; }
		public BarCheckItem ItemDoNotGenerateContentText { get; private set; }
		public BarCheckItem ItemForcePreview { get; private set; }

		private DocumentSettingsLoader DocumentSettingsLoader => (DocumentSettingsLoader)_linkLoader;

		public DocumentSettingsEditor(BarSubItem itemsContainer) : base(itemsContainer)
		{
			InitContextMenu();
		}

		public override void LoadLink(BaseLibraryLink targetLink)
		{
			if (!(targetLink is DocumentLink || targetLink is PowerPointLink)) return;
			base.LoadLink(targetLink);
		}

		protected override ILinkLoader CreateLoader(BaseLibraryLink targetLink)
		{
			return new DocumentSettingsLoader(this, (LibraryObjectLink)targetLink);
		}

		protected override void PopulateContextMenu()
		{
			_barManager.BeginInit();
			_itemsContainer.LinksPersistInfo.Clear();
			_itemsContainer.LinksPersistInfo.AddRange(new[]
			{
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

			_barManager.Items.Add(ItemDoNotGeneratePreview);
			_barManager.Items.Add(ItemDoNotGenerateContentText);
			_barManager.Items.Add(ItemForcePreview);
			_barManager.MaxItemId = maxId;

			_barManager.EndInit();
		}
	}
}
