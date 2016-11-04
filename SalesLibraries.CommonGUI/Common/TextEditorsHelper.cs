using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

namespace SalesLibraries.CommonGUI.Common
{
	public static class TextEditorsHelper
	{
		private static bool _enter;
		private static bool _needToPerformSelectionMode;

		public static void EnableSelectAll(this BaseEdit editor)
		{
			editor.MouseUp += OnMouseUpForSelect;
			editor.MouseDown += OnMouseDown;
			editor.Enter += OnEnterForSelect;
		}

		public static void EnableSelectAll(this RepositoryItem editor)
		{
			editor.MouseUp += OnMouseUpForSelect;
			editor.MouseDown += OnMouseDown;
			editor.Enter += OnEnterForSelect;
		}

		public static void DisableSelectAll(this TextEdit editor)
		{
			editor.Enter += OnEnterForReset;
		}

		private static void OnEnterForSelect(object sender, EventArgs e)
		{
			_enter = true;
		}

		private static void OnEnterForReset(object sender, EventArgs e)
		{
			var editor = sender as TextEdit;
			if (!String.IsNullOrEmpty(editor?.Text))
				editor.Select(editor.Text.Length, 0);
		}

		private static void OnMouseUpForSelect(object sender, MouseEventArgs e)
		{
			if (_needToPerformSelectionMode)
				((BaseEdit)sender).SelectAll();
			ResetEnterFlag();
		}

		private static void OnMouseDown(object sender, MouseEventArgs e)
		{
			_needToPerformSelectionMode = _enter;
		}

		private static void ResetEnterFlag()
		{
			_enter = false;
		}
	}
}
