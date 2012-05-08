using System;
using System.Windows.Forms;

namespace FileManager.ToolForms
{
    public partial class FormSuccessDisplay : Form
    {
        public FormSuccessDisplay()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}