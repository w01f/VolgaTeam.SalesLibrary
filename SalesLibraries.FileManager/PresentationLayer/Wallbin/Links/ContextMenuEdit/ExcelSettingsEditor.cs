﻿using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	class ExcelSettingsEditor : BaseContextMenuEditor
	{
		public BarCheckItem ItemDoNotGenerateContentText { get; private set; }
		public BarCheckItem ItemForceDownload { get; private set; }
		public BarCheckItem ItemForceOpen { get; private set; }

		private ExcelSettingsLoader ExcelSettingsLoader => (ExcelSettingsLoader)_linkLoader;

		public ExcelSettingsEditor(BarSubItem itemsContainer) : base(itemsContainer)
		{
			InitContextMenu();
		}

		public override void LoadLink(BaseLibraryLink targetLink)
		{
			if (!(targetLink is ExcelLink)) return;
			base.LoadLink(targetLink);
		}

		protected override ILinkLoader CreateLoader(BaseLibraryLink targetLink)
		{
			return new ExcelSettingsLoader(this, (LibraryObjectLink)targetLink);
		}

		protected override void PopulateContextMenu()
		{
			_barManager.BeginInit();
			_itemsContainer.LinksPersistInfo.Clear();
			_itemsContainer.LinksPersistInfo.AddRange(new[]
			{
				new LinkPersistInfo(ItemForceDownload),
				new LinkPersistInfo(ItemDoNotGenerateContentText),
				new LinkPersistInfo(ItemForceOpen),
			});
			_barManager.EndInit();
		}

		private void InitContextMenu()
		{
			_barManager.BeginInit();

			var maxId = _barManager.MaxItemId++;

			ItemDoNotGenerateContentText = new BarCheckItem
			{
				Id = maxId,
				Caption = "No Data File",
				CheckBoxVisibility = CheckBoxVisibility.AfterText
			};
			ItemDoNotGenerateContentText.CheckedChanged += (o, e) => ExcelSettingsLoader.OnValuesChanged();
			maxId++;

			ItemForceDownload = new BarCheckItem
			{
				Id = maxId,
				Caption = "Instant Download",
				CheckBoxVisibility = CheckBoxVisibility.AfterText
			};
			ItemForceDownload.CheckedChanged += (o, e) => ExcelSettingsLoader.OnValuesChanged();
			maxId++;

			ItemForceOpen = new BarCheckItem
			{
				Id = maxId,
				Caption = "Auto Open",
				CheckBoxVisibility = CheckBoxVisibility.AfterText
			};
			ItemForceOpen.CheckedChanged += (o, e) => ExcelSettingsLoader.OnValuesChanged();
			maxId++;

			_barManager.Items.Add(ItemForceDownload);
			_barManager.Items.Add(ItemDoNotGenerateContentText);
			_barManager.Items.Add(ItemForceOpen);
			_barManager.MaxItemId = maxId;

			_barManager.EndInit();
		}
	}
}
