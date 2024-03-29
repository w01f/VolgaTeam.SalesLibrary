﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent
{
	public class LinkBundle : WallbinCollectionEntity
	{
		#region Persistent Properties
		private string _name;
		[Required]
		public string Name
		{
			get => _name;
			set
			{
				if (_name != value)
					MarkAsModified();
				_name = value;
			}
		}

		private int _order;
		[Required]
		public int Order
		{
			get => _order;
			set
			{
				if (_order != value)
					MarkAsModified();
				_order = value;
			}
		}
		public string SettingsEncoded { get; set; }
		public virtual Library Library { get; set; }
		#endregion

		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public override IChangable Parent
		{
			get => Library;
			set => Library = value as Library;
		}

		private LinkBundleSettings _settings;
		[NotMapped, JsonIgnore]
		public LinkBundleSettings Settings
		{
			get => _settings ?? (_settings = SettingsContainer.CreateInstance<LinkBundleSettings>(this, SettingsEncoded));
			set => _settings = value;
		}

		[NotMapped, JsonIgnore]
		public override int CollectionOrder
		{
			get => Order;
			set => Order = value;
		}
		#endregion

		public override string ToString()
		{
			return Name;
		}

		public override void BeforeSave()
		{
			if (NeedToSave)
			{
				SettingsEncoded = Settings.Serialize();
			}
			base.BeforeSave();
		}

		public override void AfterSave()
		{
			Settings = null;
		}

		public override void Delete(LibraryContext context)
		{
			foreach (var link in GetLibraryLinks())
				link.Delete(context);
			base.Delete(context);
		}

		public override void ResetParent()
		{
			Library = null;
		}

		public override void AfterClone(BaseEntity<LibraryContext> original)
		{
			base.AfterClone(original);
			Settings = null;
		}

		public LinkBundle Clone(string name)
		{
			NeedToSave = true;
			BeforeSave();
			var clone = CreateEntity<LinkBundle>(linkBundleClone =>
			{
				linkBundleClone.Name = name;
				linkBundleClone.Order = Library.LinkBundles.Count;
				linkBundleClone.SettingsEncoded = SettingsEncoded;
				linkBundleClone.Library = Library;
			});
			Library.LinkBundles.Add(clone);
			((List<LinkBundle>)Library.LinkBundles).ChangeItemPosition(clone, Order + 1);
			return clone;
		}

		public IList<LinkBundleLink> GetLibraryLinks()
		{
			return Library.Pages
				.SelectMany(p => p.TopLevelLinks)
				.OfType<LinkBundleLink>()
				.Where(l => ((LinkBundleLinkSettings)l.Settings).BundleId == ExtId)
				.ToList();
		}

		public LibraryLinkItem AddLibraryLink(LibraryObjectLink libraryObjectLink, int targetIndex)
		{
			var defaulUseAsThumbnail = Settings.Items.All(item => !item.UseAsThumbnail);
			var libraryLinkItem = AddBundleItem<LibraryLinkItem>(targetIndex, libraryObjectLink.ExtId);
			libraryLinkItem.UseAsThumbnail = defaulUseAsThumbnail && libraryLinkItem.ThumbnailAvailable;
			return libraryLinkItem;
		}

		public TItem AddBundleItem<TItem>(int targetIndex = -1, params object[] parameters) where TItem : BaseBundleItem
		{
			var bundleItem = BaseBundleItem.Create<TItem>(this, parameters);
			if (targetIndex >= 0)
				Settings.Items.InsertItem(bundleItem, targetIndex);
			else
				Settings.Items.AddItem(bundleItem);
			return bundleItem;
		}

		public class LinkBundleSettings : SettingsContainer
		{
			public bool ApplyDefaultHoverNotes { get; set; }

			[JsonProperty(ItemTypeNameHandling = TypeNameHandling.All)]
			public List<BaseBundleItem> Items { get; private set; }

			public LinkBundleSettings()
			{
				Items = new List<BaseBundleItem>();
			}

			public override void AfterCreate()
			{
				base.AfterCreate();
				Items.ForEach(i =>
				{
					i.Parent = Parent;
					i.AfterCreate();
				});
			}
		}
	}
}
