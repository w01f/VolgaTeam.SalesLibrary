using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SalesDepot.Floater
{
	public class FloaterManager
	{
		private static readonly FloaterManager _instance = new FloaterManager();
		private int _floaterPositionX = int.MinValue;
		private int _floaterPositionY = int.MinValue;

		private FloaterManager()
		{
		}

		public static FloaterManager Instance
		{
			get { return _instance; }
		}

		public void ShowFloater(Form sender, string ribbonText, Image logo, Action afterShow)
		{
			var x = _floaterPositionX == Int32.MinValue ? FormMain.Instance.Left + FormMain.Instance.Width - 50 : _floaterPositionX;
			var y = _floaterPositionY == Int32.MinValue ? FormMain.Instance.Top + 50 : _floaterPositionY;
			foreach (var openForm in Application.OpenForms.OfType<Form>())
				openForm.Opacity = 0;
			using (var form = new FormFloater(x, y, logo, ribbonText))
			{
				form.Shown += (o, e) =>
				{
					if (afterShow != null)
						afterShow();
				};
				var result = form.ShowDialog();
				_floaterPositionY = form.Top;
				_floaterPositionX = form.Left + form.Width;
				if (result == DialogResult.Yes)
					AppManager.Instance.ActivateMainForm();
				else
				{
					sender.WindowState = FormWindowState.Minimized;
					AppManager.Instance.ActivateTaskbar();
				}
			}
			foreach (Form openForm in Application.OpenForms)
				openForm.Opacity = 1;
		}

		public void ShowFloater(Form sender, Action afterShow)
		{
			ShowFloater(sender, FormMain.Instance.FloaterText, FormMain.Instance.FloaterLogo, afterShow);
		}
	}
}
