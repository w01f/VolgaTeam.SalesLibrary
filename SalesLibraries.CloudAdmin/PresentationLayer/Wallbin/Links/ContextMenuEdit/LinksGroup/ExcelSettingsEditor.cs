﻿using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit.LinksGroup
{
	class ExcelSettingsEditor : BaseContextMenuEditor
	{
		public BarCheckItem ItemIsArchiveResource { get; private set; }
		public BarCheckItem ItemDoNotGenerateContentText { get; private set; }
		public BarCheckItem ItemForceDownload { get; private set; }
		public BarCheckItem ItemForceOpen { get; private set; }

		private ExcelSettingsLoader ExcelSettingsLoader => (ExcelSettingsLoader)_linksLoader;

		public ExcelSettingsEditor(BarSubItem itemsContainer) : base(itemsContainer)
		{
			InitContextMenu();
		}

		public override void LoadLinks(IEnumerable<BaseLibraryLink> targetLinks)
		{
			var linksList = targetLinks.OfType<ExcelLink>().ToList();
			base.LoadLinks(linksList);
		}

		protected override ILinksLoader CreateLoader(IEnumerable<BaseLibraryLink> targetLinks)
		{
			return new ExcelSettingsLoader(this, targetLinks.OfType<ExcelLink>());
		}

		protected override void PopulateContextMenu()
		{
			_barManager.BeginInit();
			ItemsContainer.LinksPersistInfo.Clear();
			ItemsContainer.LinksPersistInfo.AddRange(new[]
			{
				new LinkPersistInfo(ItemIsArchiveResource),
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

			ItemIsArchiveResource = new BarCheckItem
			{
				Id = maxId,
				Caption = "Archive Resource",
				CheckBoxVisibility = CheckBoxVisibility.AfterText
			};
			ItemIsArchiveResource.CheckedChanged += (o, e) => ExcelSettingsLoader.OnValuesChanged();
			maxId++;

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

			_barManager.Items.Add(ItemIsArchiveResource);
			_barManager.Items.Add(ItemForceDownload);
			_barManager.Items.Add(ItemDoNotGenerateContentText);
			_barManager.Items.Add(ItemForceOpen);
			_barManager.MaxItemId = maxId;

			_barManager.EndInit();
		}
	}
}
