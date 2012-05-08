using System;
using System.Windows.Forms;

namespace SalesDepot.ToolForms
{

    public enum ViewOptions
    {
        Open = 0,
        Save,
        Email,
        Print
    }

    
    public partial class FormViewOptions : Form
    {
        private ViewOptions _selectedOption;

        public FormViewOptions()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                buttonXOpen.Font = new System.Drawing.Font(buttonXOpen.Font.FontFamily, buttonXOpen.Font.Size - 3, buttonXOpen.Font.Style);
                buttonXSave.Font = new System.Drawing.Font(buttonXSave.Font.FontFamily, buttonXSave.Font.Size - 3, buttonXSave.Font.Style);
                buttonXPrint.Font = new System.Drawing.Font(buttonXPrint.Font.FontFamily, buttonXPrint.Font.Size - 3, buttonXPrint.Font.Style);
                buttonXEmail.Font = new System.Drawing.Font(buttonXEmail.Font.FontFamily, buttonXEmail.Font.Size - 3, buttonXEmail.Font.Style);
                buttonXClose.Font = new System.Drawing.Font(buttonXClose.Font.FontFamily, buttonXClose.Font.Size - 3, buttonXClose.Font.Style);
            }
        }

        public ViewOptions SelectedOption
        {
            get
            {
                return _selectedOption;
            }
        }

        private void ViewOptionsForm_Load(object sender, EventArgs e)
        {
            if ((ConfigurationClasses.SettingsManager.Instance.EmailButtons & ConfigurationClasses.EmailButtonsDisplayOptions.DisplayViewOptions) == ConfigurationClasses.EmailButtonsDisplayOptions.DisplayViewOptions)
                buttonXEmail.Enabled = true;
            else
                buttonXEmail.Enabled = false;
        }

        private void buttonXOpen_Click(object sender, EventArgs e)
        {
            _selectedOption = ViewOptions.Open;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonXSave_Click(object sender, EventArgs e)
        {
            _selectedOption = ViewOptions.Save;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonXPrint_Click(object sender, EventArgs e)
        {
            _selectedOption = ViewOptions.Print;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonXEmail_Click(object sender, EventArgs e)
        {
            _selectedOption = ViewOptions.Email;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonXClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}