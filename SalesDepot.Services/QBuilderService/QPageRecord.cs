using System;
using System.Linq;

namespace SalesDepot.Services.QBuilderService
{
	public partial class QPageRecord
	{
		public string FullName
		{
			get { return (firstName + " " + lastName).Trim(); }
		}

		public string Type
		{
			get { return isEmail ? "email" : "quickSITE"; }
		}

		public string[] GroupNameList
		{
			get
			{
				return !string.IsNullOrEmpty(groups) ? groups.Split(',').Select(x => x.Trim()).ToArray() : new string[] { };
			}
		}

		public DateTime? CreateDate
		{
			get
			{
				DateTime temp;
				if (DateTime.TryParse(createDate, out temp))
					return temp;
				return null;
			}
		}

		public DateTime? ExpirationDate
		{
			get
			{
				DateTime temp;
				if (DateTime.TryParse(expirationDate, out temp))
					return temp;
				return null;
			}
		}
	}
}