namespace OutlookSalesDepotAddIn.Forms
{
    partial class FormLinkPreview
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
			this.barManager = new DevExpress.XtraBars.BarManager(this.components);
			this.barOperations = new DevExpress.XtraBars.Bar();
			this.barLargeButtonItemAttach = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barLargeButtonItemExit = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.pnPreview = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
			this.SuspendLayout();
			// 
			// barManager
			// 
			this.barManager.AllowCustomization = false;
			this.barManager.AllowItemAnimatedHighlighting = false;
			this.barManager.AllowMoveBarOnToolbar = false;
			this.barManager.AllowQuickCustomization = false;
			this.barManager.AllowShowToolbarsPopup = false;
			this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barOperations});
			this.barManager.DockControls.Add(this.barDockControlTop);
			this.barManager.DockControls.Add(this.barDockControlBottom);
			this.barManager.DockControls.Add(this.barDockControlLeft);
			this.barManager.DockControls.Add(this.barDockControlRight);
			this.barManager.Form = this;
			this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barLargeButtonItemAttach,
            this.barLargeButtonItemExit});
			this.barManager.MaxItemId = 15;
			this.barManager.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
			// 
			// barOperations
			// 
			this.barOperations.BarName = "Tools";
			this.barOperations.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
			this.barOperations.DockCol = 0;
			this.barOperations.DockRow = 0;
			this.barOperations.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.barOperations.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barLargeButtonItemAttach, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barLargeButtonItemExit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
			this.barOperations.OptionsBar.AllowQuickCustomization = false;
			this.barOperations.OptionsBar.DisableClose = true;
			this.barOperations.OptionsBar.DisableCustomization = true;
			this.barOperations.OptionsBar.DrawDragBorder = false;
			this.barOperations.OptionsBar.UseWholeRow = true;
			this.barOperations.Text = "Tools";
			// 
			// barLargeButtonItemAttach
			// 
			this.barLargeButtonItemAttach.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.barLargeButtonItemAttach.Caption = "Attach this File";
			this.barLargeButtonItemAttach.Glyph = global::OutlookSalesDepotAddIn.Properties.Resources.Attach;
			this.barLargeButtonItemAttach.Id = 1;
			this.barLargeButtonItemAttach.Name = "barLargeButtonItemAttach";
			toolTipTitleItem1.Text = "Attach";
			toolTipItem1.LeftIndent = 6;
			toolTipItem1.Text = "Attach this file";
			superToolTip1.Items.Add(toolTipTitleItem1);
			superToolTip1.Items.Add(toolTipItem1);
			this.barLargeButtonItemAttach.SuperTip = superToolTip1;
			this.barLargeButtonItemAttach.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemAttach_ItemClick);
			// 
			// barLargeButtonItemExit
			// 
			this.barLargeButtonItemExit.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.barLargeButtonItemExit.Caption = "Exit";
			this.barLargeButtonItemExit.Glyph = global::OutlookSalesDepotAddIn.Properties.Resources.Exit;
			this.barLargeButtonItemExit.Id = 7;
			this.barLargeButtonItemExit.Name = "barLargeButtonItemExit";
			toolTipTitleItem2.Text = "Exit";
			toolTipItem2.LeftIndent = 6;
			toolTipItem2.Text = "Close QuickView and return to the Sales Library";
			superToolTip2.Items.Add(toolTipTitleItem2);
			superToolTip2.Items.Add(toolTipItem2);
			this.barLargeButtonItemExit.SuperTip = superToolTip2;
			this.barLargeButtonItemExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemExit_ItemClick);
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.ForeColor = System.Drawing.Color.Black;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(934, 106);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.ForeColor = System.Drawing.Color.Black;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 562);
			this.barDockControlBottom.Size = new System.Drawing.Size(934, 0);
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.ForeColor = System.Drawing.Color.Black;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 106);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 456);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.ForeColor = System.Drawing.Color.Black;
			this.barDockControlRight.Location = new System.Drawing.Point(934, 106);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 456);
			// 
			// repositoryItemTextEdit1
			// 
			this.repositoryItemTextEdit1.AutoHeight = false;
			this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
			// 
			// pnPreview
			// 
			this.pnPreview.BackColor = System.Drawing.Color.Transparent;
			this.pnPreview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnPreview.ForeColor = System.Drawing.Color.Black;
			this.pnPreview.Location = new System.Drawing.Point(0, 106);
			this.pnPreview.Name = "pnPreview";
			this.pnPreview.Size = new System.Drawing.Size(934, 456);
			this.pnPreview.TabIndex = 10;
			// 
			// FormLinkPreview
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(934, 562);
			this.Controls.Add(this.pnPreview);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "FormLinkPreview";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "QuickView";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormLinkPreview_FormClosed);
			this.Shown += new System.EventHandler(this.FormQuickView_Shown);
			((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar barOperations;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemAttach;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemExit;
		private System.Windows.Forms.Panel pnPreview;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;

    }
}