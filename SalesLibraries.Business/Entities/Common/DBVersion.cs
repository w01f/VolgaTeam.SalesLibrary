using System;
using System.ComponentModel.DataAnnotations;

namespace SalesLibraries.Business.Entities.Common
{
	public class DBVersion
	{
		[Key]
		public Int64 Id { get; set; }

		[Required]
		public Int64 Revision { get; set; }
	}
}
