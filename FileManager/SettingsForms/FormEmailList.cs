using System;
using System.Text;
using System.Windows.Forms;

namespace FileManager.SettingsForms
{
    public partial class FormEmailList : Form
    {
        public FormEmailList()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            memoEditEmails.EditValue = string.Join("; ", PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.EmailList.ToArray());
            radioButtonCreateEmail.Checked = !PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.SendEmail;
            radioButtonSendEmail.Checked = PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.SendEmail;
        }

        private void FormEmailList_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
                if (memoEditEmails.EditValue != null)
                {
                    PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.EmailList.Clear();
                    foreach (string email in memoEditEmails.EditValue.ToString().Split(new char[] { ';' }))
                        if (!string.IsNullOrEmpty(email.Trim()))
                            PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.EmailList.Add(email.Trim());
                    PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.SendEmail = radioButtonSendEmail.Checked;
                    PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.Save();
                }
        }
    }
}
