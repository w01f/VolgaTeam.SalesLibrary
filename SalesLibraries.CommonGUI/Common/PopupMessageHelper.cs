using System;
using System.Windows.Forms;

namespace SalesLibraries.CommonGUI.Common
{
	public class PopupMessageHelper
	{
		public string Title { get; private set; }

		public PopupMessageHelper(string title)
		{
			Title = title;
		}

		public void ChangeTitle(string title)
		{
			Title = title;
		}

		public void ShowInfo(string text)
		{
			MessageBox.Show(text, Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public void ShowWarning(string text)
		{
			MessageBox.Show(text, Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public DialogResult ShowQuestion(string text)
		{
			return MessageBox.Show(text, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}

		public DialogResult ShowWarningQuestion(string text)
		{
			return MessageBox.Show(text, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		}
	}
}
