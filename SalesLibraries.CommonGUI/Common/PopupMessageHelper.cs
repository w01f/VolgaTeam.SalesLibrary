using System.Windows.Forms;

namespace SalesLibraries.CommonGUI.Common
{
	public class PopupMessageHelper
	{
		private readonly string _title;

		public PopupMessageHelper(string title)
		{
			_title = title;
		}

		public void ShowInfo(string text)
		{
			MessageBox.Show(text, _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public void ShowWarning(string text)
		{
			MessageBox.Show(text, _title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public DialogResult ShowQuestion(string text)
		{
			return MessageBox.Show(text, _title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}

		public DialogResult ShowWarningQuestion(string text)
		{
			return MessageBox.Show(text, _title, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		}
	}
}
