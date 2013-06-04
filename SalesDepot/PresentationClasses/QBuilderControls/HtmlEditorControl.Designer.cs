namespace SalesDepot.PresentationClasses.QBuilderControls
{
	partial class HtmlEditorControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HtmlEditorControl));
			this.richEditControl = new DevExpress.XtraRichEdit.RichEditControl();
			this.barManager = new DevExpress.XtraBars.BarManager(this.components);
			this.fontBar = new DevExpress.XtraRichEdit.UI.FontBar();
			this.toggleFontBoldItem = new DevExpress.XtraRichEdit.UI.ToggleFontBoldItem();
			this.toggleFontItalicItem = new DevExpress.XtraRichEdit.UI.ToggleFontItalicItem();
			this.toggleFontUnderlineItem = new DevExpress.XtraRichEdit.UI.ToggleFontUnderlineItem();
			this.toggleFontSubscriptItem = new DevExpress.XtraRichEdit.UI.ToggleFontSubscriptItem();
			this.toggleFontSuperscriptItem = new DevExpress.XtraRichEdit.UI.ToggleFontSuperscriptItem();
			this.changeFontSizeItem = new DevExpress.XtraRichEdit.UI.ChangeFontSizeItem();
			this.repositoryItemRichEditFontSizeEdit = new DevExpress.XtraRichEdit.Design.RepositoryItemRichEditFontSizeEdit();
			this.changeFontNameItem = new DevExpress.XtraRichEdit.UI.ChangeFontNameItem();
			this.repositoryItemFontEdit = new DevExpress.XtraEditors.Repository.RepositoryItemFontEdit();
			this.changeFontColorItem = new DevExpress.XtraRichEdit.UI.ChangeFontColorItem();
			this.redoItem = new DevExpress.XtraRichEdit.UI.RedoItem();
			this.undoItem = new DevExpress.XtraRichEdit.UI.UndoItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.repositoryItemRichEditStyleEdit = new DevExpress.XtraRichEdit.Design.RepositoryItemRichEditStyleEdit();
			this.richEditBarController = new DevExpress.XtraRichEdit.UI.RichEditBarController();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichEditFontSizeEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichEditStyleEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.richEditBarController)).BeginInit();
			this.SuspendLayout();
			// 
			// richEditControl
			// 
			this.richEditControl.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
			this.richEditControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richEditControl.LayoutUnit = DevExpress.XtraRichEdit.DocumentLayoutUnit.Pixel;
			this.richEditControl.Location = new System.Drawing.Point(0, 26);
			this.richEditControl.MenuManager = this.barManager;
			this.richEditControl.Name = "richEditControl";
			this.richEditControl.Options.VerticalScrollbar.Visibility = DevExpress.XtraRichEdit.RichEditScrollbarVisibility.Hidden;
			this.richEditControl.Size = new System.Drawing.Size(432, 344);
			this.richEditControl.TabIndex = 0;
			// 
			// barManager
			// 
			this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.fontBar});
			this.barManager.DockControls.Add(this.barDockControlTop);
			this.barManager.DockControls.Add(this.barDockControlBottom);
			this.barManager.DockControls.Add(this.barDockControlLeft);
			this.barManager.DockControls.Add(this.barDockControlRight);
			this.barManager.Form = this;
			this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.undoItem,
            this.redoItem,
            this.changeFontNameItem,
            this.changeFontSizeItem,
            this.changeFontColorItem,
            this.toggleFontBoldItem,
            this.toggleFontItalicItem,
            this.toggleFontUnderlineItem,
            this.toggleFontSuperscriptItem,
            this.toggleFontSubscriptItem});
			this.barManager.MaxItemId = 55;
			this.barManager.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemFontEdit,
            this.repositoryItemRichEditFontSizeEdit,
            this.repositoryItemRichEditStyleEdit});
			// 
			// fontBar
			// 
			this.fontBar.DockCol = 0;
			this.fontBar.DockRow = 0;
			this.fontBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.fontBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleFontBoldItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleFontItalicItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleFontUnderlineItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleFontSubscriptItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleFontSuperscriptItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.changeFontSizeItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.changeFontNameItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.changeFontColorItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.redoItem, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.undoItem)});
			this.fontBar.OptionsBar.AllowQuickCustomization = false;
			this.fontBar.OptionsBar.DisableClose = true;
			this.fontBar.OptionsBar.DisableCustomization = true;
			this.fontBar.OptionsBar.DrawDragBorder = false;
			this.fontBar.OptionsBar.RotateWhenVertical = false;
			this.fontBar.OptionsBar.UseWholeRow = true;
			// 
			// toggleFontBoldItem
			// 
			this.toggleFontBoldItem.Glyph = ((System.Drawing.Image)(resources.GetObject("toggleFontBoldItem.Glyph")));
			this.toggleFontBoldItem.Id = 6;
			this.toggleFontBoldItem.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toggleFontBoldItem.LargeGlyph")));
			this.toggleFontBoldItem.Name = "toggleFontBoldItem";
			// 
			// toggleFontItalicItem
			// 
			this.toggleFontItalicItem.Glyph = ((System.Drawing.Image)(resources.GetObject("toggleFontItalicItem.Glyph")));
			this.toggleFontItalicItem.Id = 7;
			this.toggleFontItalicItem.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toggleFontItalicItem.LargeGlyph")));
			this.toggleFontItalicItem.Name = "toggleFontItalicItem";
			// 
			// toggleFontUnderlineItem
			// 
			this.toggleFontUnderlineItem.Glyph = ((System.Drawing.Image)(resources.GetObject("toggleFontUnderlineItem.Glyph")));
			this.toggleFontUnderlineItem.Id = 8;
			this.toggleFontUnderlineItem.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toggleFontUnderlineItem.LargeGlyph")));
			this.toggleFontUnderlineItem.Name = "toggleFontUnderlineItem";
			// 
			// toggleFontSubscriptItem
			// 
			this.toggleFontSubscriptItem.Glyph = ((System.Drawing.Image)(resources.GetObject("toggleFontSubscriptItem.Glyph")));
			this.toggleFontSubscriptItem.Id = 13;
			this.toggleFontSubscriptItem.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toggleFontSubscriptItem.LargeGlyph")));
			this.toggleFontSubscriptItem.Name = "toggleFontSubscriptItem";
			// 
			// toggleFontSuperscriptItem
			// 
			this.toggleFontSuperscriptItem.Glyph = ((System.Drawing.Image)(resources.GetObject("toggleFontSuperscriptItem.Glyph")));
			this.toggleFontSuperscriptItem.Id = 12;
			this.toggleFontSuperscriptItem.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toggleFontSuperscriptItem.LargeGlyph")));
			this.toggleFontSuperscriptItem.Name = "toggleFontSuperscriptItem";
			// 
			// changeFontSizeItem
			// 
			this.changeFontSizeItem.Edit = this.repositoryItemRichEditFontSizeEdit;
			this.changeFontSizeItem.Id = 3;
			this.changeFontSizeItem.Name = "changeFontSizeItem";
			// 
			// repositoryItemRichEditFontSizeEdit
			// 
			this.repositoryItemRichEditFontSizeEdit.AutoHeight = false;
			this.repositoryItemRichEditFontSizeEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemRichEditFontSizeEdit.Control = this.richEditControl;
			this.repositoryItemRichEditFontSizeEdit.Name = "repositoryItemRichEditFontSizeEdit";
			// 
			// changeFontNameItem
			// 
			this.changeFontNameItem.Edit = this.repositoryItemFontEdit;
			this.changeFontNameItem.Id = 2;
			this.changeFontNameItem.Name = "changeFontNameItem";
			this.changeFontNameItem.Width = 99;
			// 
			// repositoryItemFontEdit
			// 
			this.repositoryItemFontEdit.AutoHeight = false;
			this.repositoryItemFontEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemFontEdit.Name = "repositoryItemFontEdit";
			// 
			// changeFontColorItem
			// 
			this.changeFontColorItem.Glyph = ((System.Drawing.Image)(resources.GetObject("changeFontColorItem.Glyph")));
			this.changeFontColorItem.Id = 4;
			this.changeFontColorItem.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("changeFontColorItem.LargeGlyph")));
			this.changeFontColorItem.Name = "changeFontColorItem";
			// 
			// redoItem
			// 
			this.redoItem.Glyph = ((System.Drawing.Image)(resources.GetObject("redoItem.Glyph")));
			this.redoItem.Id = 1;
			this.redoItem.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("redoItem.LargeGlyph")));
			this.redoItem.Name = "redoItem";
			// 
			// undoItem
			// 
			this.undoItem.Glyph = ((System.Drawing.Image)(resources.GetObject("undoItem.Glyph")));
			this.undoItem.Id = 0;
			this.undoItem.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("undoItem.LargeGlyph")));
			this.undoItem.Name = "undoItem";
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(432, 26);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 370);
			this.barDockControlBottom.Size = new System.Drawing.Size(432, 0);
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 344);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(432, 26);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 344);
			// 
			// repositoryItemRichEditStyleEdit
			// 
			this.repositoryItemRichEditStyleEdit.AutoHeight = false;
			this.repositoryItemRichEditStyleEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemRichEditStyleEdit.Control = this.richEditControl;
			this.repositoryItemRichEditStyleEdit.Name = "repositoryItemRichEditStyleEdit";
			// 
			// richEditBarController
			// 
			this.richEditBarController.BarItems.Add(this.undoItem);
			this.richEditBarController.BarItems.Add(this.redoItem);
			this.richEditBarController.BarItems.Add(this.changeFontNameItem);
			this.richEditBarController.BarItems.Add(this.changeFontSizeItem);
			this.richEditBarController.BarItems.Add(this.changeFontColorItem);
			this.richEditBarController.BarItems.Add(this.toggleFontBoldItem);
			this.richEditBarController.BarItems.Add(this.toggleFontItalicItem);
			this.richEditBarController.BarItems.Add(this.toggleFontUnderlineItem);
			this.richEditBarController.BarItems.Add(this.toggleFontSuperscriptItem);
			this.richEditBarController.BarItems.Add(this.toggleFontSubscriptItem);
			this.richEditBarController.RichEditControl = this.richEditControl;
			// 
			// HtmlEditorControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.richEditControl);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.Name = "HtmlEditorControl";
			this.Size = new System.Drawing.Size(432, 370);
			((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichEditFontSizeEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichEditStyleEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.richEditBarController)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraRichEdit.RichEditControl richEditControl;
		private DevExpress.XtraBars.BarManager barManager;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraRichEdit.UI.UndoItem undoItem;
		private DevExpress.XtraRichEdit.UI.RedoItem redoItem;
		private DevExpress.XtraRichEdit.UI.FontBar fontBar;
		private DevExpress.XtraRichEdit.UI.ChangeFontNameItem changeFontNameItem;
		private DevExpress.XtraEditors.Repository.RepositoryItemFontEdit repositoryItemFontEdit;
		private DevExpress.XtraRichEdit.UI.ChangeFontSizeItem changeFontSizeItem;
		private DevExpress.XtraRichEdit.Design.RepositoryItemRichEditFontSizeEdit repositoryItemRichEditFontSizeEdit;
		private DevExpress.XtraRichEdit.UI.ChangeFontColorItem changeFontColorItem;
		private DevExpress.XtraRichEdit.UI.ToggleFontBoldItem toggleFontBoldItem;
		private DevExpress.XtraRichEdit.UI.ToggleFontItalicItem toggleFontItalicItem;
		private DevExpress.XtraRichEdit.UI.ToggleFontUnderlineItem toggleFontUnderlineItem;
		private DevExpress.XtraRichEdit.UI.ToggleFontSuperscriptItem toggleFontSuperscriptItem;
		private DevExpress.XtraRichEdit.UI.ToggleFontSubscriptItem toggleFontSubscriptItem;
		private DevExpress.XtraRichEdit.Design.RepositoryItemRichEditStyleEdit repositoryItemRichEditStyleEdit;
		private DevExpress.XtraRichEdit.UI.RichEditBarController richEditBarController;
	}
}
