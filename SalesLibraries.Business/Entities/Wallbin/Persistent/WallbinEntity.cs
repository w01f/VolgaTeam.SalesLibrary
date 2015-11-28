using System;
using System.ComponentModel.DataAnnotations;
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
		protected WallbinEntity()
		{
			ExtId = Guid.NewGuid();
			LastModified = DateTime.Now;
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
			LastModified = DateTime.Now;
		}
	}
}
