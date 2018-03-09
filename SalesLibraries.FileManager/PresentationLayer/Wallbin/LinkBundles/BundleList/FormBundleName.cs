using System;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.BundleList
{
	public partial class FormBundleName : MetroForm
	{
		public string Title
		{
			get { return simpleLabelItemTitle.Text; }
			set { simpleLabelItemTitle.Text = String.Format(simpleLabelItemTitle.Text,value); }
		}

		public string BundleName
		{
			get { return textEditName.EditValue as String; }
			set { textEditName.EditValue = value; }
		}

		public FormBundleName()
		{
			InitializeComponent();
			textEditName.MouseDown += EditorHelper.OnEditorMouseDown;
			textEditName.MouseUp += EditorHelper.OnEditorMouseUp;
			textEditName.Enter += EditorHelper.OnEditorEnter;
			textEditName.Focus();

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}
	}
}