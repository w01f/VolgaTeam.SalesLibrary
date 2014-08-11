using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;

namespace SalesDepot.ToolForms.Gallery
{
	public partial class FormAddFavoriteImage : MetroForm
	{
		private readonly List<string> _existedNames = new List<string>();

		public FormAddFavoriteImage(Image targetImage, string defaultName, IEnumerable<string> existedNames)
		{
			InitializeComponent();
			textEditImageName.EditValue = defaultName;
			pictureBoxImage.Image = targetImage;
			_existedNames.AddRange(existedNames);
		}

		public string ImageName
		{
			get
			{
				if (textEditImageName.EditValue != null)
					return textEditImageName.EditValue.ToString();
				return null;
			}
		}

		private void textEditScheduleName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Enter) return;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void FormAddFavoriteImage_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			if (String.IsNullOrEmpty(ImageName))
			{
				AppManager.Instance.ShowWarning("Image Name can't be empty");
				e.Cancel = true;
			}
			else if (_existedNames.Contains(ImageName.ToLower()))
			{
				AppManager.Instance.ShowWarning("Image must have unique name");
				e.Cancel = true;
			}
			else
				e.Cancel = false;
		}
	}
}