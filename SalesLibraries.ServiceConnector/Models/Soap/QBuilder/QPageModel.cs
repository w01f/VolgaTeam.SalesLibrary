using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesLibraries.ServiceConnector.QBuilderService
{
	public partial class QPageModel
	{
		public bool FullyLoaded { get; set; }

		public string FullName => (firstName + " " + lastName).Trim();

		public string Type => isEmail ? "Email" : "quickSITE";

		public string SecurityType => isRestricted ? "protected" : "public";

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

		public int? TotalViews => totalViews > 0 ? (int?)totalViews : null;

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

		public void RemoveLink(QPageLinkModel link)
		{
			if (link != null)
			{
				var linkList = new List<QPageLinkModel>();
				linkList.AddRange(links);
				linkList.Remove(link);
				links = linkList.ToArray();
			}
		}
	}
}