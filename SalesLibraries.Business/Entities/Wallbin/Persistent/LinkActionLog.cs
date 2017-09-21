using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent
{
	public class LinkActionLog : BaseEntity<LibraryContext>, ICollectionItem
	{
		#region Persistent Properties
		[Required]
		public LinkActionType ActionType { get; set; }
		[Required]
		public DateTime Date { get; set; }
		public int Order { get; set; }
		public string SettingsEncoded { get; set; }
		public virtual Library Library { get; set; }
		#endregion

		#region Nonpersistent Properties
		private ActionSettings _settings;
		[NotMapped, JsonIgnore]
		public ActionSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<ActionSettings>(null, SettingsEncoded)); }
			set { _settings = value; }
		}

		[NotMapped, JsonIgnore]
		public IChangable Parent
		{
			get { return Library; }
			set { Library = value as Library; }
		}

		[NotMapped, JsonIgnore]
		public int CollectionOrder
		{
			get { return Order; }
			set { Order = value; }
		}
		#endregion

		public LinkActionLog()
		{
			Date = DateTime.Now;
		}

		public override void BeforeSave()
		{
			SettingsEncoded = Settings.Serialize();
		}

		public override void AfterSave()
		{
			Settings = null;
		}

		public override void ResetParent()
		{
			Library = null;
		}
		
		public class ActionSettings : SettingsContainer
		{
			public Guid ExtId { get; set; }
			public string Name { get; set; }
			public string Path { get; set; }
		}
	}
}
