using System.Drawing;
using SalesLibraries.CommonGUI.BackgroundProcesses;

namespace SalesLibraries.FileManager.PresentationLayer.Sync
{
	class FormProgressSync : FormProgressWithAbort
	{
		protected override void InitForm()
		{
			Title = "Uploading Library...";
			panelEx.Style.BackColor1.Color = Color.ForestGreen;
			panelEx.Style.BackColor2.Color = Color.ForestGreen;
			panelExCancel.Style.BackColor1.Color = Color.ForestGreen;
			panelExCancel.Style.BackColor2.Color = Color.ForestGreen;
		}
	}
}
