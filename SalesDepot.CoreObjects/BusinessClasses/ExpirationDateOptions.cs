using System;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class ExpirationDateOptions
	{
		public ExpirationDateOptions()
		{
			EnableExpirationDate = false;
			ExpirationDate = DateTime.MinValue;
			LabelLinkWhenExpired = true;
			SendEmailWhenSync = false;
		}

		public bool EnableExpirationDate { get; set; }
		public DateTime ExpirationDate { get; set; }
		public bool LabelLinkWhenExpired { get; set; }
		public bool SendEmailWhenSync { get; set; }

		public bool IsExpired
		{
			get
			{
				if (EnableExpirationDate && ExpirationDate != DateTime.MinValue)
					return ((long)ExpirationDate.Subtract(DateTime.Now).TotalMilliseconds) < 0;
				return false;
			}
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<EnableExpirationDate>" + EnableExpirationDate + @"</EnableExpirationDate>");
			result.AppendLine(@"<ExpirationDate>" + ExpirationDate + @"</ExpirationDate>");
			result.AppendLine(@"<LabelLinkWhenExpired>" + LabelLinkWhenExpired + @"</LabelLinkWhenExpired>");
			result.AppendLine(@"<SendEmailWhenSync>" + SendEmailWhenSync + @"</SendEmailWhenSync>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;
			DateTime tempDate = DateTime.Now;

			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "EnableExpirationDate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableExpirationDate = tempBool;
						break;
					case "ExpirationDate":
						if (DateTime.TryParse(childNode.InnerText, out tempDate))
							ExpirationDate = tempDate;
						break;
					case "LabelLinkWhenExpired":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							LabelLinkWhenExpired = tempBool;
						break;
					case "SendEmailWhenSync":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							SendEmailWhenSync = tempBool;
						break;
				}
			}
		}
	}
}