using System;

namespace SalesLibraries.Business.Entities.Interfaces
{
	public interface IExtKeyHolder
	{
		Guid ExtId { get; set; }

		bool CompareByKey(IExtKeyHolder target);
	}
}
