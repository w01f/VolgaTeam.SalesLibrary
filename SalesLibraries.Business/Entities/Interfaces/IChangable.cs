using System;

namespace SalesLibraries.Business.Entities.Interfaces
{
	public interface IChangable
	{
		DateTime LastModified { get; set; }
		bool IsModified(IChangable latest);
		void MarkAsModified();
	}
}
