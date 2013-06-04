using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesDepot.Services.QBuilderService
{
	public partial class QPageRecord
	{
		public bool FullyLoaded { get; set; }

		public string FullName
		{
			get { return (firstName + " " + lastName).Trim(); }
		}

		public string Type
		{
			get { return isEmail ? "Email" : "quickSITE"; }
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

		public string Details
		{
			get
			{
				var result = new StringBuilder();
				if (CreateDate.HasValue)
					result.AppendLine(String.Format("{0} - Created: {1}", Type, CreateDate.Value.ToString("MM/dd/yy hh:mm tt")));
				return result.ToString();
			}
		}

		public void RemoveLink(QPageLinkRecord link)
		{
			if (link != null)
			{
				var linkList = new List<QPageLinkRecord>();
				linkList.AddRange(links);
				linkList.Remove(link);
				links = linkList.ToArray();
			}
		}
	}
}