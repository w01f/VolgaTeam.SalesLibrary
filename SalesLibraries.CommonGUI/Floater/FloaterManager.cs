﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CommonGUI.Floater
{
	public class FloaterManager
	{
		private int _floaterPositionX = int.MinValue;
		private int _floaterPositionY = int.MinValue;

		private FloaterManager() { }

		public static FloaterManager Instance { get; } = new FloaterManager();

		public void ShowFloater(Form sender, string caption, Image logo, Action afterShow)
		{
			var isSenderMaximazed = sender.WindowState == FormWindowState.Maximized;
			var x = _floaterPositionX == Int32.MinValue ? sender.Left + sender.Width - 50 : _floaterPositionX;
			var y = _floaterPositionY == Int32.MinValue ? sender.Top + 50 : _floaterPositionY;
			foreach (Form openForm in Application.OpenForms)
				openForm.Opacity = 0;
			using (var form = new FormFloater(x, y, caption, logo))
			{
				form.Shown += (o, e) =>
				{
					afterShow?.Invoke();
				};
				var result = form.ShowDialog();
				_floaterPositionY = form.Top;
				_floaterPositionX = form.Left + form.Width;
				if (result == DialogResult.Yes)
					Utils.ActivateForm(sender.Handle, isSenderMaximazed, false);
				else
				{
					sender.WindowState = FormWindowState.Minimized;
					Utils.ActivateTaskbar();
				}
			}
			foreach (Form openForm in Application.OpenForms)
				openForm.Opacity = 1;
		}
	}
}
