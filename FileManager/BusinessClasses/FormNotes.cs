using System;
using System.Windows.Forms;

namespace FileManager.SalesDepotClasses
{
    public partial class FormNotes : Form
    {
        string note = "";
        bool isBold = false;

        public FormNotes()
        {
            InitializeComponent();
        }

        public string Note
        {
            get
            {
                return note;
            }
            set
            {
                note = value;
                if (string.IsNullOrEmpty(note))
                    rbNone.Checked = true;
                else if (note.Equals(rbNew.Text))
                    rbNew.Checked = true;
                else if (note.Equals(rbUpdated.Text))
                    rbUpdated.Checked = true;
                else if (note.Equals(rbSell.Text))
                    rbSell.Checked = true;
                else if (note.Equals(rbAttention.Text))
                    rbAttention.Checked = true;
                else 
                {
                    rbCustomNote.Checked = true;
                    edCustomNote.Text = note;
                }
            }
        }

        public bool IsBold
        {
            get
            {
                return isBold;
            }
            set
            {
                rbRegular.Checked = !value;
                rbBold.Checked = value;
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (rbNew.Checked)
                note = rbNew.Text;
            else if (rbUpdated.Checked)
                note = rbUpdated.Text;
            else if (rbSell.Checked)
                note = rbSell.Text;
            else if (rbAttention.Checked)
                note = rbAttention.Text;
            else if (rbCustomNote.Checked)
                note = "-"+edCustomNote.Text;
            else
                note = "";

            isBold = rbBold.Checked;

            this.Close();
        }

        private void rbNew_CheckedChanged(object sender, EventArgs e)
        {
            edCustomNote.Enabled = rbCustomNote.Checked;
        }
    }
}
