using System;
using System.Windows.Forms;

namespace FileManager.SettingsForms
{
    public partial class FormBranding : Form
    {
        public FormBranding()
        {
            InitializeComponent();
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
                if (textEditBrandingName.EditValue == null)
                {
                    AppManager.Instance.ShowWarning("Branding Name is not set");
                    return;
                }
                else if (string.IsNullOrEmpty(textEditBrandingName.EditValue.ToString()))
                {
                    AppManager.Instance.ShowWarning("Branding Name is not set");
                    return;
                }
                e.Cancel = false;
                PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.BrandingText = textEditBrandingName.EditValue.ToString();
                PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.Save();
            }
        }

        private void FormApplicationSettings_Load(object sender, EventArgs e)
        {
            textEditBrandingName.EditValue = PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.BrandingText;
        }
    }
}
