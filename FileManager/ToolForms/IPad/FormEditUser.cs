using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace FileManager.ToolForms.IPad
{
    public partial class FormEditUser : Form
    {
        private bool _newUser = false;
        private List<string> _existedUsers = new List<string>();

        public FormEditUser(bool newUser, string[] existedUsers)
        {
            InitializeComponent();
            _newUser = newUser;
            _existedUsers.AddRange(existedUsers);

            textEditLogin.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            textEditLogin.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            textEditLogin.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            textEditFirstName.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            textEditFirstName.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            textEditFirstName.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            textEditLastName.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            textEditLastName.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            textEditLastName.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            textEditEmail.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            textEditEmail.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            textEditEmail.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            textEditPassword.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            textEditPassword.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            textEditPassword.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);

            if (_newUser)
            {
                this.Text = "Add User";
                checkEditPassword.Visible = false;
                laPassword.Visible = true;
                textEditLogin.Enabled = true;
            }
            else
            {
                this.Text = "Edit User";
                checkEditPassword.Visible = true;
                checkEditPassword.Checked = false;
                laPassword.Visible = false;
                textEditLogin.Enabled = false;
            }
        }

        private void checkEditPassword_CheckedChanged(object sender, EventArgs e)
        {
            textEditPassword.Enabled = checkEditPassword.Checked;
            if (!checkEditPassword.Checked)
                textEditPassword.EditValue = null;
        }

        private void textEdit_Validating(object sender, CancelEventArgs e)
        {
            TextEdit edit = sender as TextEdit;
            if (edit != null && edit.Enabled && string.IsNullOrEmpty(edit.Text))
            {
                edit.ErrorText = "Empty value is not allowed";
                e.Cancel = true;
            }
        }

        private void FormEditUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                textEditLogin.Focus();
                textEditFirstName.Focus();
                textEditLastName.Focus();
                textEditEmail.Focus();
                textEditPassword.Focus();
                if (!this.ValidateChildren())
                    e.Cancel = true;
                else if (_newUser && textEditLogin.EditValue != null && _existedUsers.Contains(textEditLogin.EditValue.ToString()))
                {
                    if (AppManager.Instance.ShowWarningQuestion("User with given login already exist.\nDo you want to update his data?") == System.Windows.Forms.DialogResult.No)
                        e.Cancel = true;
                }
            }
        }
    }
}
