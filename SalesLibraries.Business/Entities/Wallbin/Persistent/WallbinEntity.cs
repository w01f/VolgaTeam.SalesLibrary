using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Business.Entities.Interfaces;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent
{
	public abstract class WallbinEntity : BaseEntity<LibraryContext>, IExtKeyHolder, IChangable
	{
		#region Persistent Properties
		[Required]
		public Guid ExtId { get; set; }

		[Required]
		public DateTime LastModified { get; set; }
		#endregion

		#region Non Persistent Properties
		[NotMapped, JsonIgnore]
		public bool AllowToHandleChanges { get; set; }
		[NotMapped, JsonIgnore]
		protected bool NeedToSave { get; set; }
		#endregion

		protected WallbinEntity()
		{
			ExtId = Guid.NewGuid();
			LastModified = DateTime.Now;
		}

		public override void BeforeSave()
		{
			NeedToSave = false;
		}

		public virtual bool CompareByKey(IExtKeyHolder target)
		{
			if (target == null) return false;
			return target.ExtId == ExtId;
		}

		public virtual bool IsModified(IChangable latest)
		{
			return latest.LastModified > LastModified;
		}

		public virtual void MarkAsModified()
		{
			if (!AllowToHandleChanges) return;
			NeedToSave = true;
			LastModified = DateTime.Now;
		}

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

		public static TWallbinEntitty CreateEntity<TWallbinEntitty>(Action<TWallbinEntitty> configureCallback = null) where TWallbinEntitty : WallbinEntity
		{
			var entity = Activator.CreateInstance<TWallbinEntitty>();
			entity.AllowToHandleChanges = true;
			configureCallback?.Invoke(entity);
			return entity;
		}
	}
}
