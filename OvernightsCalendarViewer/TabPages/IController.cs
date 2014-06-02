namespace OvernightsCalendarViewer.TabPages
{
	interface IController
	{
		bool IsActive { get; set; }
		bool NeedToUpdate { get; set; }
		void InitController();
		void ShowTab();
	}
}
