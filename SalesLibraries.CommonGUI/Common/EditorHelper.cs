using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CommonGUI.Common
{
	public static class EditorHelper
	{
		private static bool _enter;
		private static bool _needSelect;

		public static void EditorEnter(object sender, EventArgs e)
		{
			_enter = true;
			ResetEnterFlag();
		}

		public static void EditorMouseUp(object sender, MouseEventArgs e)
		{
			if (_needSelect)
			{
				var baseEdit = sender as BaseEdit;
				if (baseEdit != null) baseEdit.SelectAll();
			}
		}

		public static void EditorMouseDown(object sender, MouseEventArgs e)
		{
			_needSelect = _enter;
		}

		private static void ResetEnterFlag()
		{
			_enter = false;
		}

		public static void FontEdit_Click(object sender, EventArgs e)
		{
			var fontEdit = sender as ButtonEdit;
			if (fontEdit == null) return;
			using (var dlgFont = new FontDialog())
			{
				dlgFont.Font = fontEdit.Tag as Font;
				if (dlgFont.ShowDialog() != DialogResult.OK) return;
				fontEdit.Tag = dlgFont.Font;
				fontEdit.EditValue = Utils.FontToString(dlgFont.Font);
			}
		}

		public static void FontEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			FontEdit_Click(sender, null);
		}
	}
}
