namespace FileManager.PresentationClasses.Tags
{
	partial class TagsCleaner
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
			this.laHeader = new System.Windows.Forms.Label();
			this.pnMain = new System.Windows.Forms.Panel();
			this.pnData = new System.Windows.Forms.Panel();
			this.pbWarning = new System.Windows.Forms.PictureBox();
			this.laWarning = new System.Windows.Forms.Label();
			this.laWarningDescription = new System.Windows.Forms.Label();
			this.buttonXCategories = new DevComponents.DotNetBar.ButtonX();
			this.buttonXKeywords = new DevComponents.DotNetBar.ButtonX();
			this.buttonXFileCards = new DevComponents.DotNetBar.ButtonX();
			this.buttonXFileAttachments = new DevComponents.DotNetBar.ButtonX();
			this.buttonXWebAttachments = new DevComponents.DotNetBar.ButtonX();
			this.pnMain.SuspendLayout();
			this.pnData.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbWarning)).BeginInit();
			this.SuspendLayout();
			// 
			// laHeader
			// 
			this.laHeader.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.laHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.laHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laHeader.Location = new System.Drawing.Point(0, 0);
			this.laHeader.Name = "laHeader";
			this.laHeader.Size = new System.Drawing.Size(350, 24);
			this.laHeader.TabIndex = 0;
			this.laHeader.Text = "Blow Up Tags";
			this.laHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnMain
			// 
			this.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnMain.Controls.Add(this.pnData);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 24);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(350, 657);
			this.pnMain.TabIndex = 1;
			// 
			// pnData
			// 
			this.pnData.Controls.Add(this.buttonXWebAttachments);
			this.pnData.Controls.Add(this.buttonXFileAttachments);
			this.pnData.Controls.Add(this.buttonXFileCards);
			this.pnData.Controls.Add(this.buttonXKeywords);
			this.pnData.Controls.Add(this.buttonXCategories);
			this.pnData.Controls.Add(this.laWarningDescription);
			this.pnData.Controls.Add(this.laWarning);
			this.pnData.Controls.Add(this.pbWarning);
			this.pnData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnData.Location = new System.Drawing.Point(0, 0);
			this.pnData.Name = "pnData";
			this.pnData.Size = new System.Drawing.Size(346, 653);
			this.pnData.TabIndex = 1;
			// 
			// pbWarning
			// 
			this.pbWarning.Image = global::FileManager.Properties.Resources.TagsCleanerWarning;
			this.pbWarning.Location = new System.Drawing.Point(3, 3);
			this.pbWarning.Name = "pbWarning";
			this.pbWarning.Size = new System.Drawing.Size(79, 78);
			this.pbWarning.TabIndex = 0;
			this.pbWarning.TabStop = false;
			// 
			// laWarning
			// 
			this.laWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laWarning.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laWarning.ForeColor = System.Drawing.Color.Red;
			this.laWarning.Location = new System.Drawing.Point(88, 3);
			this.laWarning.Name = "laWarning";
			this.laWarning.Size = new System.Drawing.Size(255, 78);
			this.laWarning.TabIndex = 1;
			this.laWarning.Text = "Uhm....\r\nAre You Sure?";
			this.laWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laWarningDescription
			// 
			this.laWarningDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laWarningDescription.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laWarningDescription.Location = new System.Drawing.Point(16, 84);
			this.laWarningDescription.Name = "laWarningDescription";
			this.laWarningDescription.Size = new System.Drawing.Size(311, 78);
			this.laWarningDescription.TabIndex = 2;
			this.laWarningDescription.Text = "You have been granted the power and unique gift to BLOW up all tags on this page." +
    " You BETTER BE CAREFUL!";
			this.laWarningDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonXCategories
			// 
			this.buttonXCategories.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCategories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCategories.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCategories.Image = global::FileManager.Properties.Resources.TagsCategoriesWidget;
			this.buttonXCategories.Location = new System.Drawing.Point(19, 165);
			this.buttonXCategories.Name = "buttonXCategories";
			this.buttonXCategories.Size = new System.Drawing.Size(308, 47);
			this.buttonXCategories.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCategories.TabIndex = 8;
			this.buttonXCategories.Text = "Blow up ALL Category Tags for this Page!";
			this.buttonXCategories.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXCategories.TextColor = System.Drawing.Color.Black;
			this.buttonXCategories.Click += new System.EventHandler(this.buttonXCategories_Click);
			// 
			// buttonXKeywords
			// 
			this.buttonXKeywords.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXKeywords.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXKeywords.Image = global::FileManager.Properties.Resources.TagsKeywordsWidget;
			this.buttonXKeywords.Location = new System.Drawing.Point(19, 244);
			this.buttonXKeywords.Name = "buttonXKeywords";
			this.buttonXKeywords.Size = new System.Drawing.Size(308, 47);
			this.buttonXKeywords.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXKeywords.TabIndex = 9;
			this.buttonXKeywords.Text = "Blow up ALL Keyword Tags for this Page!";
			this.buttonXKeywords.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXKeywords.TextColor = System.Drawing.Color.Black;
			this.buttonXKeywords.Click += new System.EventHandler(this.buttonXKeywords_Click);
			// 
			// buttonXFileCards
			// 
			this.buttonXFileCards.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXFileCards.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXFileCards.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXFileCards.Image = global::FileManager.Properties.Resources.TagsFileCardsWidget;
			this.buttonXFileCards.Location = new System.Drawing.Point(19, 326);
			this.buttonXFileCards.Name = "buttonXFileCards";
			this.buttonXFileCards.Size = new System.Drawing.Size(308, 47);
			this.buttonXFileCards.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXFileCards.TabIndex = 10;
			this.buttonXFileCards.Text = "Blow up ALL FileCards for this Page!";
			this.buttonXFileCards.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXFileCards.TextColor = System.Drawing.Color.Black;
			this.buttonXFileCards.Click += new System.EventHandler(this.buttonXFileCards_Click);
			// 
			// buttonXFileAttachments
			// 
			this.buttonXFileAttachments.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXFileAttachments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXFileAttachments.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXFileAttachments.Image = global::FileManager.Properties.Resources.TagsFileAttachmentsWidget;
			this.buttonXFileAttachments.Location = new System.Drawing.Point(19, 407);
			this.buttonXFileAttachments.Name = "buttonXFileAttachments";
			this.buttonXFileAttachments.Size = new System.Drawing.Size(308, 47);
			this.buttonXFileAttachments.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXFileAttachments.TabIndex = 11;
			this.buttonXFileAttachments.Text = "Blow up ALL FileCards for this Page!";
			this.buttonXFileAttachments.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXFileAttachments.TextColor = System.Drawing.Color.Black;
			this.buttonXFileAttachments.Click += new System.EventHandler(this.buttonXFileAttachments_Click);
			// 
			// buttonXWebAttachments
			// 
			this.buttonXWebAttachments.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXWebAttachments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXWebAttachments.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXWebAttachments.Image = global::FileManager.Properties.Resources.TagsWebAttachmentsWidget;
			this.buttonXWebAttachments.Location = new System.Drawing.Point(19, 487);
			this.buttonXWebAttachments.Name = "buttonXWebAttachments";
			this.buttonXWebAttachments.Size = new System.Drawing.Size(308, 47);
			this.buttonXWebAttachments.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXWebAttachments.TabIndex = 12;
			this.buttonXWebAttachments.Text = "Blow up ALL FileCards for this Page!";
			this.buttonXWebAttachments.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXWebAttachments.TextColor = System.Drawing.Color.Black;
			this.buttonXWebAttachments.Click += new System.EventHandler(this.buttonXWebAttachments_Click);
			// 
			// TagsCleaner
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.laHeader);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "TagsCleaner";
			this.Size = new System.Drawing.Size(350, 681);
			this.pnMain.ResumeLayout(false);
			this.pnData.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbWarning)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label laHeader;
		private System.Windows.Forms.Panel pnMain;
		private System.Windows.Forms.Panel pnData;
		private System.Windows.Forms.PictureBox pbWarning;
		private System.Windows.Forms.Label laWarning;
		private System.Windows.Forms.Label laWarningDescription;
		private DevComponents.DotNetBar.ButtonX buttonXWebAttachments;
		private DevComponents.DotNetBar.ButtonX buttonXFileAttachments;
		private DevComponents.DotNetBar.ButtonX buttonXFileCards;
		private DevComponents.DotNetBar.ButtonX buttonXKeywords;
		private DevComponents.DotNetBar.ButtonX buttonXCategories;
	}
}
