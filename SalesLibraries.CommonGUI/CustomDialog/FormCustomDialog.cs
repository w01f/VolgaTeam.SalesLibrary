using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CommonGUI.CustomDialog
{
	public partial class FormCustomDialog : Form
	{
		public FormCustomDialog(string message, IEnumerable<CustomDialogButtonInfo> buttons)
		{
			InitializeComponent();

			layoutControlItemButtons.MaxSize = RectangleHelper.ScaleSize(layoutControlItemButtons.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemButtons.MinSize = RectangleHelper.ScaleSize(layoutControlItemButtons.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));

			simpleLabelItemMessage.Text = String.Format("<color=white>{0}</color>", message);
			LoadButtons(buttons);
		}

		private void LoadButtons(IEnumerable<CustomDialogButtonInfo> buttons)
		{
			const int buttonMargin = 20;
			var leftPosition = pnButtons.Width;
			var buttonHeight = pnButtons.Height - buttonMargin;
			var topPosition = (pnButtons.Height - buttonHeight) / 2;

			foreach (var buttonInfo in buttons.Reverse().ToList())
			{
				var button = new ButtonX();
				button.ColorTable = eButtonColor.OrangeWithBackground;
				button.Text = buttonInfo.Title;
				button.DialogResult = buttonInfo.DialogResult;
				button.TextColor = Color.Black;
				button.Height = buttonHeight;
				button.Width = (Int32)(buttonInfo.Width* Utils.GetScaleFactor(CreateGraphics().DpiX).Width);
				button.Style = eDotNetBarStyle.StyleManagerControlled;
				button.Top = topPosition;
				button.Anchor = AnchorStyles.Top | AnchorStyles.Right;

				leftPosition -= button.Width;
				button.Left = leftPosition;
				leftPosition -= buttonMargin;

				pnButtons.Controls.Add(button);
			}
		}
	}
}
