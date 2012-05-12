using System;
using System.Windows.Forms;

namespace SalesDepot.ToolForms.Settings
{
    public partial class FormEmailSettings : Form
    {
        public FormEmailSettings()
        {
            InitializeComponent();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (rbEmailButtonsEnableAll.Checked)
                ConfigurationClasses.SettingsManager.Instance.EmailButtons = ConfigurationClasses.EmailButtonsDisplayOptions.DisplayEmailBin | ConfigurationClasses.EmailButtonsDisplayOptions.DisplayQuickView | ConfigurationClasses.EmailButtonsDisplayOptions.DisplayViewOptions;
            else if (rbEmailButtonsEnaiblePartial.Checked)
            {
                ConfigurationClasses.SettingsManager.Instance.EmailButtons = ConfigurationClasses.EmailButtonsDisplayOptions.DisplayNone;
                if (ckEnableEmailBin.Checked)
                    ConfigurationClasses.SettingsManager.Instance.EmailButtons = ConfigurationClasses.SettingsManager.Instance.EmailButtons | ConfigurationClasses.EmailButtonsDisplayOptions.DisplayEmailBin;
                if (ckEnableQuickView.Checked)
                    ConfigurationClasses.SettingsManager.Instance.EmailButtons = ConfigurationClasses.SettingsManager.Instance.EmailButtons | ConfigurationClasses.EmailButtonsDisplayOptions.DisplayQuickView;
                if (ckEnableViewOptions.Checked)
                    ConfigurationClasses.SettingsManager.Instance.EmailButtons = ConfigurationClasses.SettingsManager.Instance.EmailButtons | ConfigurationClasses.EmailButtonsDisplayOptions.DisplayViewOptions;
            }
            else if (rbEmailButtonsDisableAll.Checked)
                ConfigurationClasses.SettingsManager.Instance.EmailButtons = ConfigurationClasses.EmailButtonsDisplayOptions.DisplayNone;

            ConfigurationClasses.SettingsManager.Instance.SaveSettings();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            if (ConfigurationClasses.SettingsManager.Instance.EmailButtons == (ConfigurationClasses.EmailButtonsDisplayOptions.DisplayEmailBin | ConfigurationClasses.EmailButtonsDisplayOptions.DisplayQuickView | ConfigurationClasses.EmailButtonsDisplayOptions.DisplayViewOptions))
                rbEmailButtonsEnableAll.Checked = true;
            else if (ConfigurationClasses.SettingsManager.Instance.EmailButtons == ConfigurationClasses.EmailButtonsDisplayOptions.DisplayNone)
                rbEmailButtonsDisableAll.Checked = true;
            else
            {
                rbEmailButtonsEnaiblePartial.Checked = true;
                if ((ConfigurationClasses.SettingsManager.Instance.EmailButtons & ConfigurationClasses.EmailButtonsDisplayOptions.DisplayEmailBin) == ConfigurationClasses.EmailButtonsDisplayOptions.DisplayEmailBin)
                    ckEnableEmailBin.Checked = true;
                if ((ConfigurationClasses.SettingsManager.Instance.EmailButtons & ConfigurationClasses.EmailButtonsDisplayOptions.DisplayQuickView) == ConfigurationClasses.EmailButtonsDisplayOptions.DisplayQuickView)
                    ckEnableQuickView.Checked = true;
                if ((ConfigurationClasses.SettingsManager.Instance.EmailButtons & ConfigurationClasses.EmailButtonsDisplayOptions.DisplayViewOptions) == ConfigurationClasses.EmailButtonsDisplayOptions.DisplayViewOptions)
                    ckEnableViewOptions.Checked = true;
            }
            rbEmailButtons_CheckedChanged(null, null);
        }

        private void rbEmailButtons_CheckedChanged(object sender, EventArgs e)
        {
            ckEnableEmailBin.Enabled = rbEmailButtonsEnaiblePartial.Checked;
            ckEnableQuickView.Enabled = rbEmailButtonsEnaiblePartial.Checked;
            ckEnableViewOptions.Enabled = rbEmailButtonsEnaiblePartial.Checked;
        }
    }
}
