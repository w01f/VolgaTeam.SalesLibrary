using System.Collections.Generic;
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
			get { return _name; }
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
			get { return _order; }
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
			get { return Library; }
			set { Library = value as Library; }
		}

		private LinkBundleSettings _settings;
		[NotMapped, JsonIgnore]
		public LinkBundleSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<LinkBundleSettings>(this, SettingsEncoded)); }
			set { _settings = value; }
		}

		[NotMapped, JsonIgnore]
		public override int CollectionOrder
		{
			get { return Order; }
			set { Order = value; }
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

		public LibraryLinkItem AddLibraryLink(LibraryObjectLink libraryObjectLink)
		{
			var defaulUseAsThumbnail = Settings.Items.All(item => !item.UseAsThumbnail);
			var libraryLinkItem = AddBundleItem<LibraryLinkItem>(libraryObjectLink.ExtId);
			libraryLinkItem.UseAsThumbnail = defaulUseAsThumbnail && libraryLinkItem.ThumbnailAvailable;
			return libraryLinkItem;
		}

		public TItem AddBundleItem<TItem>(params object[] parameters) where TItem : BaseBundleItem
		{
			var bundleItem = BaseBundleItem.Create<TItem>(this, parameters);
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

			protected override void AfterCreate()
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
