namespace FileManager.SalesDepotClasses
{
    partial class FormNotes
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
            this.gbNotes = new System.Windows.Forms.GroupBox();
            this.edCustomNote = new System.Windows.Forms.TextBox();
            this.rbCustomNote = new System.Windows.Forms.RadioButton();
            this.rbNone = new System.Windows.Forms.RadioButton();
            this.rbAttention = new System.Windows.Forms.RadioButton();
            this.rbSell = new System.Windows.Forms.RadioButton();
            this.rbUpdated = new System.Windows.Forms.RadioButton();
            this.rbNew = new System.Windows.Forms.RadioButton();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbRegular = new System.Windows.Forms.RadioButton();
            this.rbBold = new System.Windows.Forms.RadioButton();
            this.gbNotes.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbNotes
            // 
            this.gbNotes.Controls.Add(this.edCustomNote);
            this.gbNotes.Controls.Add(this.rbCustomNote);
            this.gbNotes.Controls.Add(this.rbNone);
            this.gbNotes.Controls.Add(this.rbAttention);
            this.gbNotes.Controls.Add(this.rbSell);
            this.gbNotes.Controls.Add(this.rbUpdated);
            this.gbNotes.Controls.Add(this.rbNew);
            this.gbNotes.Location = new System.Drawing.Point(5, -1);
            this.gbNotes.Name = "gbNotes";
            this.gbNotes.Size = new System.Drawing.Size(200, 212);
            this.gbNotes.TabIndex = 0;
            this.gbNotes.TabStop = false;
            // 
            // edCustomNote
            // 
            this.edCustomNote.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edCustomNote.Location = new System.Drawing.Point(25, 179);
            this.edCustomNote.Name = "edCustomNote";
            this.edCustomNote.Size = new System.Drawing.Size(166, 26);
            this.edCustomNote.TabIndex = 6;
            // 
            // rbCustomNote
            // 
            this.rbCustomNote.AutoSize = true;
            this.rbCustomNote.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbCustomNote.Location = new System.Drawing.Point(6, 155);
            this.rbCustomNote.Name = "rbCustomNote";
            this.rbCustomNote.Size = new System.Drawing.Size(127, 23);
            this.rbCustomNote.TabIndex = 5;
            this.rbCustomNote.TabStop = true;
            this.rbCustomNote.Text = "Custom Note";
            this.rbCustomNote.UseVisualStyleBackColor = true;
            this.rbCustomNote.CheckedChanged += new System.EventHandler(this.rbNew_CheckedChanged);
            // 
            // rbNone
            // 
            this.rbNone.AutoSize = true;
            this.rbNone.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbNone.Location = new System.Drawing.Point(6, 13);
            this.rbNone.Name = "rbNone";
            this.rbNone.Size = new System.Drawing.Size(68, 23);
            this.rbNone.TabIndex = 4;
            this.rbNone.TabStop = true;
            this.rbNone.Text = "None";
            this.rbNone.UseVisualStyleBackColor = true;
            this.rbNone.CheckedChanged += new System.EventHandler(this.rbNew_CheckedChanged);
            // 
            // rbAttention
            // 
            this.rbAttention.AutoSize = true;
            this.rbAttention.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbAttention.Location = new System.Drawing.Point(6, 126);
            this.rbAttention.Name = "rbAttention";
            this.rbAttention.Size = new System.Drawing.Size(128, 23);
            this.rbAttention.TabIndex = 3;
            this.rbAttention.TabStop = true;
            this.rbAttention.Text = "-ATTENTION!";
            this.rbAttention.UseVisualStyleBackColor = true;
            this.rbAttention.CheckedChanged += new System.EventHandler(this.rbNew_CheckedChanged);
            // 
            // rbSell
            // 
            this.rbSell.AutoSize = true;
            this.rbSell.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbSell.Location = new System.Drawing.Point(6, 97);
            this.rbSell.Name = "rbSell";
            this.rbSell.Size = new System.Drawing.Size(119, 23);
            this.rbSell.TabIndex = 2;
            this.rbSell.TabStop = true;
            this.rbSell.Text = "-SELL THIS!";
            this.rbSell.UseVisualStyleBackColor = true;
            this.rbSell.CheckedChanged += new System.EventHandler(this.rbNew_CheckedChanged);
            // 
            // rbUpdated
            // 
            this.rbUpdated.AutoSize = true;
            this.rbUpdated.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbUpdated.Location = new System.Drawing.Point(6, 68);
            this.rbUpdated.Name = "rbUpdated";
            this.rbUpdated.Size = new System.Drawing.Size(115, 23);
            this.rbUpdated.TabIndex = 1;
            this.rbUpdated.TabStop = true;
            this.rbUpdated.Text = "-UPDATED!";
            this.rbUpdated.UseVisualStyleBackColor = true;
            this.rbUpdated.CheckedChanged += new System.EventHandler(this.rbNew_CheckedChanged);
            // 
            // rbNew
            // 
            this.rbNew.AutoSize = true;
            this.rbNew.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbNew.Location = new System.Drawing.Point(6, 39);
            this.rbNew.Name = "rbNew";
            this.rbNew.Size = new System.Drawing.Size(74, 23);
            this.rbNew.TabIndex = 0;
            this.rbNew.TabStop = true;
            this.rbNew.Text = "-NEW!";
            this.rbNew.UseVisualStyleBackColor = true;
            this.rbNew.CheckedChanged += new System.EventHandler(this.rbNew_CheckedChanged);
            // 
            // btOK
            // 
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btOK.Location = new System.Drawing.Point(211, 5);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 30);
            this.btOK.TabIndex = 1;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btCancel.Location = new System.Drawing.Point(211, 41);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 30);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbBold);
            this.groupBox1.Controls.Add(this.rbRegular);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(5, 217);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 47);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Line Text Format";
            // 
            // rbRegular
            // 
            this.rbRegular.AutoSize = true;
            this.rbRegular.Checked = true;
            this.rbRegular.Location = new System.Drawing.Point(7, 21);
            this.rbRegular.Name = "rbRegular";
            this.rbRegular.Size = new System.Drawing.Size(67, 20);
            this.rbRegular.TabIndex = 0;
            this.rbRegular.TabStop = true;
            this.rbRegular.Text = "Normal";
            this.rbRegular.UseVisualStyleBackColor = true;
            // 
            // rbBold
            // 
            this.rbBold.AutoSize = true;
            this.rbBold.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbBold.Location = new System.Drawing.Point(124, 21);
            this.rbBold.Name = "rbBold";
            this.rbBold.Size = new System.Drawing.Size(62, 20);
            this.rbBold.TabIndex = 1;
            this.rbBold.Text = "BOLD";
            this.rbBold.UseVisualStyleBackColor = true;
            // 
            // DWBNotesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 272);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.gbNotes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DWBNotesForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Link Notes";
            this.gbNotes.ResumeLayout(false);
            this.gbNotes.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbNotes;
        private System.Windows.Forms.RadioButton rbUpdated;
        private System.Windows.Forms.RadioButton rbNew;
        private System.Windows.Forms.RadioButton rbAttention;
        private System.Windows.Forms.RadioButton rbSell;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.RadioButton rbNone;
        private System.Windows.Forms.RadioButton rbCustomNote;
        private System.Windows.Forms.TextBox edCustomNote;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbBold;
        private System.Windows.Forms.RadioButton rbRegular;
    }
}