using System;
using System.Drawing;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings
{
	public abstract class BaseBundleItem : ICollectionItem
	{
		public Guid Id { get; set; }
		public LinkBundleItemType ItemType { get; set; }

		private int _collectionOrder;
		public int CollectionOrder
		{
			get { return _collectionOrder; }
			set
			{
				if (_collectionOrder != value)
					OnSettingsChanged();
				_collectionOrder = value;
			}
		}

		private Image _image;
		public Image Image
		{
			get { return _image; }
			set
			{
				if (_image != value)
					OnSettingsChanged();
				_image = value;
			}
		}

		private string _title;
		public string Title
		{
			get { return _title; }
			set
			{
				if (_title != value)
				{
					OnSettingsChanged();
					if (ParentBundle != null && ParentBundle.Settings.ApplyDefaultHoverNotes)
						HoverTip = value;
				}
				_title = value;
			}
		}

		private string _hoverTip;
		public string HoverTip
		{
			get { return _hoverTip; }
			set
			{
				if (_hoverTip != value)
					OnSettingsChanged();
				_hoverTip = value;
			}
		}

		private bool _useAsThumbnail;
		public bool UseAsThumbnail
		{
			get { return _useAsThumbnail; }
			set
			{
				if (_useAsThumbnail != value)
					OnSettingsChanged();
				_useAsThumbnail = value;
			}
		}

		public bool Visible { get; set; }

		[JsonIgnore]
		public IChangable Parent { get; set; }
		[JsonIgnore]
		public abstract string Name { get; set; }
		[JsonIgnore]
		public LinkBundle ParentBundle => Parent as LinkBundle;
		[JsonIgnore]
		protected bool AllowToHandleChanges { get; set; }

		protected BaseBundleItem()
		{
			Id = Guid.NewGuid();
			Visible = true;
		}

		protected virtual void AfterConstraction(params object[] parameters)
		{
			Title = Name;
			if (ParentBundle != null && ParentBundle.Settings.ApplyDefaultHoverNotes)
				HoverTip = Title;
		}

		public virtual void AfterCreate() { }

		[OnDeserialized]
		public void AfterDeserialize(StreamingContext context)
		{
			AllowToHandleChanges = true;
		}

		[OnDeserializing]
		public void BeforeDeserialize(StreamingContext context)
		{
			AllowToHandleChanges = false;
		}

		protected void OnSettingsChanged()
		{
			if (AllowToHandleChanges)
				Parent?.MarkAsModified();
		}

		public static TItem Create<TItem>(LinkBundle parent, params object[] parameters) where TItem : BaseBundleItem
		{
			var item = Activator.CreateInstance<TItem>();
			item.Parent = parent;
			item.CollectionOrder = parent.Settings.Items.Count;
			item.AfterConstraction(parameters);
			item.AllowToHandleChanges = true;
			return item;
		}
	}
}
