using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.LinkBundles.SingleBundle
{
	[IntendForClass(typeof(InfoItem))]
	//public partial class InfoEditControl : UserControl, ILinkBundleInfoEditControl
	public partial class InfoEditControl : XtraTabPage, ILinkBundleInfoEditControl
	{
		private InfoItem _bundleItem;

		public InfoEditControl(InfoItem bundleItem)
		{
			_bundleItem = bundleItem;
			InitializeComponent();

			Text = InfoItem.ItemName;

			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
			}

			buttonEditTextFont.ButtonClick += EditorHelper.FontEdit_ButtonClick;
			buttonEditTextFont.Click += EditorHelper.FontEdit_Click;
		}

		public void LoadData()
		{
			checkEditHeader.Checked = !String.IsNullOrEmpty(_bundleItem.Header);
			textEditHeader.EditValue = _bundleItem.Header;

			checkEditBody.Checked = !String.IsNullOrEmpty(_bundleItem.Body);
			memoEditBody.EditValue = _bundleItem.Body;

			colorEditTextColor.Color = _bundleItem.ForeColor;
			colorEditBackColor.Color = _bundleItem.BackColor;
			buttonEditTextFont.Tag = _bundleItem.Font;
			buttonEditTextFont.EditValue = Utils.FontToString(_bundleItem.Font);
		}

		public void SaveData()
		{
			_bundleItem.Header = textEditHeader.EditValue as String;
			_bundleItem.Body = memoEditBody.EditValue as String;

			_bundleItem.ForeColor = colorEditTextColor.Color;
			_bundleItem.BackColor = colorEditBackColor.Color;
			_bundleItem.Font = buttonEditTextFont.Tag as Font;
		}

		public void Release()
		{
			_bundleItem = null;
		}

		private void OnHeaderCheckedChanged(object sender, System.EventArgs e)
		{
			textEditHeader.Enabled = checkEditHeader.Checked;
			if (!checkEditHeader.Checked)
				textEditHeader.EditValue = null;
		}

		private void OnBodyCheckedChanged(object sender, System.EventArgs e)
		{
			memoEditBody.Enabled = checkEditBody.Checked;
			labelControlTextColor.Enabled = checkEditBody.Checked;
			colorEditTextColor.Enabled = checkEditBody.Checked;
			labelControlBackColor.Enabled = checkEditBody.Checked;
			colorEditBackColor.Enabled = checkEditBody.Checked;
			labelControlTextFont.Enabled = checkEditBody.Checked;
			buttonEditTextFont.Enabled = checkEditBody.Checked;
			if (!checkEditBody.Checked)
				memoEditBody.EditValue = null;
		}

		private void OnTextColorEditValueChanged(object sender, EventArgs e)
		{
			memoEditBody.ForeColor = colorEditTextColor.Color;
		}

		private void OnBackColorEditValueChanged(object sender, EventArgs e)
		{
			memoEditBody.BackColor = colorEditBackColor.Color;
		}

		private void OnFontEditValueChanged(object sender, EventArgs e)
		{
			memoEditBody.Font = (Font)buttonEditTextFont.Tag;
			memoEditBody.Properties.Appearance.Font = memoEditBody.Font;
			memoEditBody.Properties.AppearanceDisabled.Font = memoEditBody.Font;
			memoEditBody.Properties.AppearanceFocused.Font = memoEditBody.Font;
			memoEditBody.Properties.AppearanceReadOnly.Font = memoEditBody.Font;
		}
	}
}
