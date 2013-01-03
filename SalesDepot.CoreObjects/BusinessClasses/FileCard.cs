using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class FileCard
	{
		public FileCard(ILibraryFile parent)
		{
			Parent = parent;
			Identifier = Guid.NewGuid();
			Notes = new List<string>();
			Title = "Information about this file...";
		}

		public ILibraryFile Parent { get; private set; }
		public Guid Identifier { get; set; }
		public bool Enable { get; set; }
		public string Title { get; set; }
		public string Advertiser { get; set; }
		public DateTime? DateSold { get; set; }
		public double? BroadcastClosed { get; set; }
		public double? DigitalClosed { get; set; }
		public double? PublishingClosed { get; set; }
		public string SalesName { get; set; }
		public string SalesEmail { get; set; }
		public string SalesPhone { get; set; }
		public string SalesStation { get; set; }
		public List<string> Notes { get; set; }

		public FileCard Clone(ILibraryFile parent)
		{
			var fileCard = new FileCard(parent);
			fileCard.Enable = Enable;
			fileCard.Title = Title;
			fileCard.Advertiser = Advertiser;
			fileCard.DateSold = DateSold;
			fileCard.BroadcastClosed = BroadcastClosed;
			fileCard.DigitalClosed = DigitalClosed;
			fileCard.PublishingClosed = PublishingClosed;
			fileCard.SalesName = SalesName;
			fileCard.SalesEmail = SalesEmail;
			fileCard.SalesPhone = SalesPhone;
			fileCard.SalesStation = SalesStation;
			fileCard.Notes.AddRange(Notes);
			return fileCard;
		}

		public bool Compare(FileCard anotherFileCard)
		{
			return anotherFileCard.Enable == Enable &&
				anotherFileCard.Title == Title &&
				anotherFileCard.Advertiser == Advertiser &&
				anotherFileCard.DateSold == DateSold &&
				anotherFileCard.BroadcastClosed == BroadcastClosed &&
				anotherFileCard.DigitalClosed == DigitalClosed &&
				anotherFileCard.PublishingClosed == PublishingClosed &&
				anotherFileCard.SalesName == SalesName &&
				anotherFileCard.SalesEmail == SalesEmail &&
				anotherFileCard.SalesPhone == SalesPhone &&
				anotherFileCard.SalesStation == SalesStation &&
				anotherFileCard.Notes.All(x => Notes.Contains(x)) && Notes.Count == anotherFileCard.Notes.Count;
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<Identifier>" + Identifier.ToString() + @"</Identifier>");
			result.AppendLine(@"<Enable>" + Enable.ToString() + @"</Enable>");
			result.AppendLine(@"<Title>" + Title.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Title>");
			if (!string.IsNullOrEmpty(Advertiser))
				result.AppendLine(@"<Advertiser>" + Advertiser.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Advertiser>");
			if (DateSold.HasValue)
				result.AppendLine(@"<DateSold>" + DateSold.Value.ToString() + @"</DateSold>");
			if (BroadcastClosed.HasValue)
				result.AppendLine(@"<BroadcastClosed>" + BroadcastClosed.Value.ToString() + @"</BroadcastClosed>");
			if (DigitalClosed.HasValue)
				result.AppendLine(@"<DigitalClosed>" + DigitalClosed.Value.ToString() + @"</DigitalClosed>");
			if (PublishingClosed.HasValue)
				result.AppendLine(@"<PublishingClosed>" + PublishingClosed.Value.ToString() + @"</PublishingClosed>");
			if (!string.IsNullOrEmpty(SalesName))
				result.AppendLine(@"<SalesName>" + SalesName.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</SalesName>");
			if (!string.IsNullOrEmpty(SalesEmail))
				result.AppendLine(@"<SalesEmail>" + SalesEmail.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</SalesEmail>");
			if (!string.IsNullOrEmpty(SalesPhone))
				result.AppendLine(@"<SalesPhone>" + SalesPhone.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</SalesPhone>");
			if (!string.IsNullOrEmpty(SalesStation))
				result.AppendLine(@"<SalesStation>" + SalesStation.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</SalesStation>");
			result.AppendLine(@"<Notes>");
			foreach (string note in Notes)
				result.AppendLine(@"<Note>" + note.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Note>");
			result.AppendLine(@"</Notes>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				double tempDouble;
				switch (childNode.Name)
				{
					case "Identifier":
						Guid tempGuid;
						if (Guid.TryParse(childNode.InnerText, out tempGuid))
							Identifier = tempGuid;
						break;
					case "Enable":
						bool tempBool;
						if (bool.TryParse(childNode.InnerText, out tempBool))
							Enable = tempBool;
						break;
					case "Title":
						Title = childNode.InnerText;
						break;
					case "Advertiser":
						Advertiser = childNode.InnerText;
						break;
					case "DateSold":
						DateTime tempDate;
						if (DateTime.TryParse(childNode.InnerText, out tempDate))
							DateSold = tempDate;
						break;
					case "BroadcastClosed":
						if (double.TryParse(childNode.InnerText, out tempDouble))
							BroadcastClosed = tempDouble;
						break;
					case "DigitalClosed":
						if (double.TryParse(childNode.InnerText, out tempDouble))
							DigitalClosed = tempDouble;
						break;
					case "PublishingClosed":
						if (double.TryParse(childNode.InnerText, out tempDouble))
							PublishingClosed = tempDouble;
						break;
					case "SalesName":
						SalesName = childNode.InnerText;
						break;
					case "SalesEmail":
						SalesEmail = childNode.InnerText;
						break;
					case "SalesPhone":
						SalesPhone = childNode.InnerText;
						break;
					case "SalesStation":
						SalesStation = childNode.InnerText;
						break;
					case "Notes":
						foreach (XmlNode noteNode in childNode.ChildNodes)
							Notes.Add(noteNode.InnerText);
						break;
				}
			}
		}
	}
}