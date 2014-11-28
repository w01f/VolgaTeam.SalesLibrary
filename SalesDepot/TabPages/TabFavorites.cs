using System;
using SalesDepot.PresentationClasses.Gallery;

namespace SalesDepot.TabPages
{
	public class TabFavorites : FavoriteImagesControl, IController
	{
		#region IController
		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }

		public void InitController()
		{
			FormMain.Instance.buttonItemFavoritesHelp.Click += ButtonItemFavoritesHelpOnClick;
		}

		public void ShowTab()
		{
			IsActive = true;
			BringToFront();
			AppManager.Instance.ActivityManager.AddUserActivity("Favorites selected");
		}
		#endregion

		private void ButtonItemFavoritesHelpOnClick(object sender, EventArgs eventArgs)
		{
			AppManager.Instance.HelpManager.OpenHelpLink("favorites");
		}
	}
}
