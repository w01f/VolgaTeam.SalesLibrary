using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.SingleBundle
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

			buttonEditTextFont.ButtonClick += EditorHelper.OnFontEditButtonClick;
			buttonEditTextFont.Click += EditorHelper.OnFontEditClick;

			layoutControlItemLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTitle.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTitle.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
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
			layoutControlItemHeaderEditor.Enabled = checkEditHeader.Checked;
			if (!checkEditHeader.Checked)
				textEditHeader.EditValue = null;
		}

		private void OnBodyCheckedChanged(object sender, System.EventArgs e)
		{
			layoutControlItemBodyEditor.Enabled = checkEditBody.Checked;
			layoutControlItemTextColor.Enabled = checkEditBody.Checked;
			layoutControlItemBackColor.Enabled = checkEditBody.Checked;
			layoutControlItemTextFont.Enabled = checkEditBody.Checked;
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
