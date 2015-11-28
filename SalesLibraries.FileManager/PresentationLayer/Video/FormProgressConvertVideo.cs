using System.Drawing;
using SalesLibraries.CommonGUI.BackgroundProcesses;

namespace SalesLibraries.FileManager.PresentationLayer.Video
{
	class FormProgressConvertVideo : FormProgressWithAbort
	{
		protected override void InitForm()
		{
			Title = "Converting Video...";
			panelEx.Style.BackColor1.Color = Color.Black;
			panelEx.Style.BackColor2.Color = Color.Black;
			panelExCancel.Style.BackColor1.Color = Color.Black;
			panelExCancel.Style.BackColor2.Color = Color.Black;
		}
	}
}
