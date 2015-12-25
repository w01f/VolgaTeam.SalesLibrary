using System;
using System.Text;
using System.Xml;

namespace SalesLibraries.SalesDepot.Configuration
{
	class WallbinViewSettings
	{
		public string SelectedLibrary { get; set; }
		public string SelectedPage { get; set; }
		public int FontSize { get; set; }
		public int RowSpace { get; set; }

		public bool HomeView { get; set; }
		public bool ClassicView { get; set; }
		public bool ListView { get; set; }
		public bool AccordionView { get; set; }

		public bool MultitabView { get; set; }
		
		public bool SearchView { get; set; }
		public bool ShowSearchByName { get; set; }
		public bool ShowSearchByDate { get; set; }
		public bool ShowSearchByTags { get; set; }

		public bool CalendarView { get; set; }

		public bool LastViewed { get; set; }

		public WallbinViewSettings()
		{
			SelectedLibrary = String.Empty;
			SelectedPage = String.Empty;
			FontSize = 13;
			RowSpace = 2;

			HomeView = true;
			ClassicView = true;
			ListView = false;
			AccordionView = false;
			
			SearchView = false;
			ShowSearchByName = true;
			ShowSearchByDate = false;
			ShowSearchByTags = false;
			LastViewed = false;

			MultitabView = true;
		}

		public string Serialize()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<SelectedLibrary>" + SelectedLibrary.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedLibrary>");
			xml.AppendLine(@"<SelectedPage>" + SelectedPage.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedPage>");
			xml.AppendLine(@"<FontSize>" + FontSize + @"</FontSize>");
			xml.AppendLine(@"<RowSpace>" + RowSpace + @"</RowSpace>");

			xml.AppendLine(@"<MultitabView>" + MultitabView + @"</MultitabView>");
			xml.AppendLine(@"<HomeView>" + HomeView + @"</HomeView>");
			xml.AppendLine(@"<SearchView>" + SearchView + @"</SearchView>");
			xml.AppendLine(@"<CalendarView>" + CalendarView + @"</CalendarView>");
			if (LastViewed)
			{
				xml.AppendLine(@"<ClassicView>" + ClassicView + @"</ClassicView>");
				xml.AppendLine(@"<ListView>" + ListView + @"</ListView>");
				xml.AppendLine(@"<AccordionView>" + AccordionView + @"</AccordionView>");
				xml.AppendLine(@"<ShowSearchByDate>" + ShowSearchByDate + @"</ShowSearchByDate>");
				xml.AppendLine(@"<ShowSearchByTags>" + ShowSearchByTags + @"</ShowSearchByTags>");
				xml.AppendLine(@"<ShowSearchByName>" + ShowSearchByName + @"</ShowSearchByName>");
			}
			return xml.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				bool tempBool;
				int tempInt;
				switch (childNode.Name)
				{
					case "SelectedLibrary":
						SelectedLibrary = childNode.InnerText;
						break;
					case "SelectedPage":
						SelectedPage = childNode.InnerText;
						break;
					case "FontSize":
						if (Int32.TryParse(childNode.InnerText, out tempInt))
							FontSize = tempInt;
						break;
					case "RowSpace":
						if (Int32.TryParse(childNode.InnerText, out tempInt))
							RowSpace = tempInt;
						break;

					case "HomeView":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							HomeView = (LastViewed && tempBool) || (!LastViewed && HomeView);
						break;
					case "SearchView":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							SearchView = (LastViewed && tempBool) || (!LastViewed && SearchView);
						break;
					case "CalendarView":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							CalendarView = tempBool;
						break;
					case "MultitabView":
										if (bool.TryParse(childNode.InnerText, out tempBool))
					MultitabView = tempBool;
						break;
					case "ClassicView":
						if (LastViewed && bool.TryParse(childNode.InnerText, out tempBool))
							ClassicView = tempBool;
						break;
					case "ListView":
						if (LastViewed && bool.TryParse(childNode.InnerText, out tempBool))
							ListView = tempBool;
						break;
					case "AccordionView":
						if (LastViewed && bool.TryParse(childNode.InnerText, out tempBool))
							AccordionView = tempBool;
						break;
					case "ShowSearchByDate":
						if (LastViewed && bool.TryParse(childNode.InnerText, out tempBool))
							ShowSearchByDate = tempBool;
						break;
					case "ShowSearchByTags":
						if (LastViewed && bool.TryParse(childNode.InnerText, out tempBool))
							ShowSearchByTags = tempBool;
						break;
					case "ShowSearchByName":
						if (LastViewed && bool.TryParse(childNode.InnerText, out tempBool))
							ShowSearchByName = tempBool;
						break;
				}
			}
		}

		public void LoadDefault()
		{
			if (RemoteResourceManager.Instance.DefaultViewFile.ExistsLocal())
			{
				var document = new XmlDocument();
				try
				{
					document.Load(RemoteResourceManager.Instance.DefaultViewFile.LocalPath);
				}
				catch { }

				var node = document.SelectSingleNode(@"/defaultview/SalesLibrary/Classic");
				bool tempBool;
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						ClassicView = tempBool;
				node = document.SelectSingleNode(@"/defaultview/SalesLibrary/List");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						ListView = tempBool;
				node = document.SelectSingleNode(@"/defaultview/SalesLibrary/Accordion");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						AccordionView = tempBool;
				node = document.SelectSingleNode(@"/defaultview/SalesLibrary/solutiontarget");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						ShowSearchByTags = tempBool;
				node = document.SelectSingleNode(@"/defaultview/SalesLibrary/solutiontitle");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						ShowSearchByName = tempBool;
				node = document.SelectSingleNode(@"/defaultview/SalesLibrary/solutiondate");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						ShowSearchByDate = tempBool;
				node = document.SelectSingleNode(@"/defaultview/SalesLibrary/lastviewed");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						LastViewed = tempBool;

				HomeView = ClassicView || ListView || AccordionView;
				SearchView = ShowSearchByTags || ShowSearchByDate || ShowSearchByName;
			}

			if (LastViewed)
				ClassicView = true;
		}

		public void SelectHomePage()
		{
			HomeView = true;
			SearchView = false;
			CalendarView = false;
		}

		public void SelectSearchPage()
		{
			HomeView = false;
			SearchView = true;
			CalendarView = false;
		}

		public void SelectCalendarPage()
		{
			HomeView = false;
			SearchView = false;
			CalendarView = true;
		}
	}
}
