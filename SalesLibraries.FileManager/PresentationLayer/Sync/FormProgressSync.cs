using System.Drawing;
using SalesLibraries.CommonGUI.BackgroundProcesses;

namespace SalesLibraries.FileManager.PresentationLayer.Sync
{
	class FormProgressSync : FormProgressWithAbort
	{
		protected override void InitForm()
		{
			Title = "Syncing Sales Library...";
			panelEx.Style.BackColor1.Color = Color.Blue;
			panelEx.Style.BackColor2.Color = Color.Blue;
			panelExCancel.Style.BackColor1.Color = Color.Blue;
			panelExCancel.Style.BackColor2.Color = Color.Blue;
		}
	}
}
