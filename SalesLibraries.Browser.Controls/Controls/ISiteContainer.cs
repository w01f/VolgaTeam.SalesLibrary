using SalesLibraries.Browser.Controls.BusinessClasses.Objects;

namespace SalesLibraries.Browser.Controls.Controls
{
	public interface ISiteContainer
	{
		SiteSettings SiteSettings { get; }
		string CurrentUrl { get; }

		void CopyUrl();
		void EmailUrl();

		void UpdateExtensionsState();

		void UpdateYouTubeState();

		void UpdateNavigationButtons();
		void NavigateBack();
		void NavigateForward();
		void RefreshPage();
	}
}
