﻿namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Views
{
	partial class BaseWallbin
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
			this.pnEmpty = new System.Windows.Forms.Panel();
			this.pnContainer = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// pnEmpty
			// 
			this.pnEmpty.Location = new System.Drawing.Point(3, 441);
			this.pnEmpty.Name = "pnEmpty";
			this.pnEmpty.Size = new System.Drawing.Size(766, 45);
			this.pnEmpty.TabIndex = 0;
			// 
			// pnContainer
			// 
			this.pnContainer.Location = new System.Drawing.Point(3, 3);
			this.pnContainer.Name = "pnContainer";
			this.pnContainer.Size = new System.Drawing.Size(766, 376);
			this.pnContainer.TabIndex = 1;
			// 
			// BaseWallbin
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnContainer);
			this.Controls.Add(this.pnEmpty);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "BaseWallbin";
			this.Size = new System.Drawing.Size(772, 489);
			this.ResumeLayout(false);

		}

		#endregion

		protected System.Windows.Forms.Panel pnEmpty;
		protected System.Windows.Forms.Panel pnContainer;
	}
}