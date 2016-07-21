using System;
using SalesLibraries.CommonGUI.Wallbin.Views;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Views
{
	public class FormatState : IWallbinViewFormat
	{
		#region IWallbinViewFormat
		public int FontSize
		{
			get { return MainController.Instance.Settings.WallbinViewSettings.FontSize; }
			set
			{
				MainController.Instance.Settings.WallbinViewSettings.FontSize = value;
				MainController.Instance.Settings.SaveSettings();
			}
		}

		public int RowSpace
		{
			get { return MainController.Instance.Settings.WallbinViewSettings.RowSpace; }
			set
			{
				MainController.Instance.Settings.WallbinViewSettings.RowSpace = value;
				MainController.Instance.Settings.SaveSettings();
			}
		}

		public bool AllowEdit => false;
		public bool AllowMultiSelect => false;

		public bool ShowCategoryTags => false;

		public bool ShowSuperFilterTags => false;

		public bool ShowKeywordTags => false;

		public bool ShowSecurityTags => false;

		public event EventHandler<EventArgs> StateChanged;
		#endregion

		public bool ClassicView
		{
			get { return MainController.Instance.Settings.WallbinViewSettings.ClassicView; } 
			private set { MainController.Instance.Settings.WallbinViewSettings.ClassicView = value; } 
		}
		public bool ListView
		{
			get { return MainController.Instance.Settings.WallbinViewSettings.ListView; }
			private set { MainController.Instance.Settings.WallbinViewSettings.ListView = value; }
		}
		public bool AccordionView
		{
			get { return MainController.Instance.Settings.WallbinViewSettings.AccordionView; }
			private set { MainController.Instance.Settings.WallbinViewSettings.AccordionView = value; }
		}

		public void Update()
		{
			StateChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
