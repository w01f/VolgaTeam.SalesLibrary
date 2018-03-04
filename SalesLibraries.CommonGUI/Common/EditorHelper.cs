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

		public static void OnEditorEnter(object sender, EventArgs e)
		{
			_enter = true;
			ResetEnterFlag();
		}

		public static void OnEditorMouseUp(object sender, MouseEventArgs e)
		{
			if (!_needSelect) return;
			if (sender is BaseEdit baseEdit) baseEdit.SelectAll();
		}

		public static void OnEditorMouseDown(object sender, MouseEventArgs e)
		{
			_needSelect = _enter;
		}

		private static void ResetEnterFlag()
		{
			_enter = false;
		}

		public static void OnFontEditClick(object sender, EventArgs e)
		{
			if (!(sender is ButtonEdit fontEdit)) return;
			using (var dlgFont = new FontDialog())
			{
				dlgFont.Font = fontEdit.Tag as Font;
				if (dlgFont.ShowDialog() != DialogResult.OK) return;
				fontEdit.Tag = dlgFont.Font;
				fontEdit.EditValue = Utils.FontToString(dlgFont.Font);
			}
		}

		public static void OnFontEditButtonClick(object sender, ButtonPressedEventArgs e)
		{
			OnFontEditClick(sender, null);
		}
	}
}
