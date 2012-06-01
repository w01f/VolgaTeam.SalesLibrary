using System;
using System.Text;
using System.Windows.Forms;

namespace FileManager.ToolForms.Settings
{
    public partial class FormEmailList : Form
    {
        public FormEmailList()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laDescription.Font = new System.Drawing.Font(laDescription.Font.FontFamily, laDescription.Font.Size - 2, laDescription.Font.Style);
                laTitle.Font = new System.Drawing.Font(laTitle.Font.FontFamily, laTitle.Font.Size - 2, laTitle.Font.Style);
                radioButtonCreateEmail.Font = new System.Drawing.Font(radioButtonCreateEmail.Font.FontFamily, radioButtonCreateEmail.Font.Size - 2, radioButtonCreateEmail.Font.Style);
                radioButtonSendEmail.Font = new System.Drawing.Font(radioButtonSendEmail.Font.FontFamily, radioButtonSendEmail.Font.Size - 2, radioButtonSendEmail.Font.Style);
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            memoEditEmails.EditValue = string.Join("; ", PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.EmailList.ToArray());
            radioButtonCreateEmail.Checked = !PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.SendEmail;
            radioButtonSendEmail.Checked = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.SendEmail;
        }

        private void FormEmailList_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
                if (memoEditEmails.EditValue != null)
                {
                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.EmailList.Clear();
                    foreach (string email in memoEditEmails.EditValue.ToString().Split(new char[] { ';' }))
                        if (!string.IsNullOrEmpty(email.Trim()))
                            PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.EmailList.Add(email.Trim());
                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.SendEmail = radioButtonSendEmail.Checked;
                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.Save();
                }
        }
    }
}
