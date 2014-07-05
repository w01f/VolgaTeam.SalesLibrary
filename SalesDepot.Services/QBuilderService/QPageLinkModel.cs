using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace SalesDepot.Services.QBuilderService
{
	public partial class QPageLinkModel
	{
		public Image Logo
		{
			get
			{
				return String.IsNullOrEmpty(logo) ? null : new Bitmap(new MemoryStream(Convert.FromBase64String(logo)));
			}
		}

		public string Details
		{
			get
			{
				var result = new StringBuilder();
				if (!String.IsNullOrEmpty(libraryName))
					result.AppendLine(String.Format("Library: {0}", libraryName));
				if (!String.IsNullOrEmpty(fileName))
					result.AppendLine(String.Format("File: {0}", fileName));
				return result.ToString();
			}
		}
	}
}