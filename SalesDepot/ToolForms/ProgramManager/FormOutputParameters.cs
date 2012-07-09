﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SalesDepot.ToolForms.ProgramManager
{
    public partial class FormOutputParameters : Form
    {
        private List<global::ProgramManager.CoreObjects.Week> _weeks = new List<global::ProgramManager.CoreObjects.Week>();

        public string Station
        {
            get
            {
                return comboBoxEditStation.EditValue != null ? comboBoxEditStation.EditValue.ToString() : string.Empty;
            }
        }

        public global::ProgramManager.CoreObjects.Week[] Weeks
        {
            get
            {
                return _weeks.ToArray();
            }
        }

        public bool Landscape
        {
            get
            {
                return buttonXLandscape.Checked;
            }
        }

        public FormOutputParameters()
        {
            InitializeComponent();
        }

        private void FormOutputParameters_Load(object sender, System.EventArgs e)
        {
            #region Schedule Tab
            comboBoxEditStation.Properties.Items.AddRange(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.Library.ProgramManager.GetStationList());
            comboBoxEditStation.EditValue = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.Library.ProgramManager.SelectedStation.Name;

            DateTime currentDate = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.Library.ProgramManager.SelectedDay.Date;
            while (currentDate.DayOfWeek != DayOfWeek.Monday)
                currentDate = currentDate.AddDays(-1);
            dateEditWeekStart.DateTime = currentDate;

            global::ProgramManager.CoreObjects.Week week = new global::ProgramManager.CoreObjects.Week();
            week.DateStart = dateEditWeekStart.DateTime;
            week.DateEnd = week.DateStart.AddDays(6);
            _weeks.Add(week);

            gridControlWeeks.DataSource = _weeks;
            #endregion

            #region Text Settings Tab
            comboBoxEditHeaderFont.Properties.Items.Clear();
            comboBoxEditHeaderFont.Properties.Items.AddRange(ConfigurationClasses.ListManager.Instance.HeaderFonts.Select(x => x.FontString).ToArray());
            int selectedIndex = comboBoxEditHeaderFont.Properties.Items.IndexOf(ConfigurationClasses.SettingsManager.Instance.ProgramScheduleOutputSettings.HeaderFont.FontString);
            if (selectedIndex >= 0 && comboBoxEditHeaderFont.Properties.Items.Count > 0)
                comboBoxEditHeaderFont.SelectedIndex = selectedIndex;

            comboBoxEditFooterFont.Properties.Items.Clear();
            comboBoxEditFooterFont.Properties.Items.AddRange(ConfigurationClasses.ListManager.Instance.FooterFonts.Select(x => x.FontString).ToArray());
            selectedIndex = comboBoxEditFooterFont.Properties.Items.IndexOf(ConfigurationClasses.SettingsManager.Instance.ProgramScheduleOutputSettings.FooterFont.FontString);
            if (selectedIndex >= 0 && comboBoxEditFooterFont.Properties.Items.Count > 0)
                comboBoxEditFooterFont.SelectedIndex = selectedIndex;

            comboBoxEditBodyFont.Properties.Items.Clear();
            comboBoxEditBodyFont.Properties.Items.AddRange(ConfigurationClasses.ListManager.Instance.BodyFonts.Select(x => x.FontString).ToArray());
            selectedIndex = comboBoxEditBodyFont.Properties.Items.IndexOf(ConfigurationClasses.SettingsManager.Instance.ProgramScheduleOutputSettings.BodyFont.FontString);
            if (selectedIndex >= 0 && comboBoxEditBodyFont.Properties.Items.Count > 0)
                comboBoxEditBodyFont.SelectedIndex = selectedIndex;

            checkEditPrimeTimeSpecialFontSize.Checked = ConfigurationClasses.SettingsManager.Instance.ProgramScheduleOutputSettings.UsePrimeTimeSpecialFontSize;
            timeEditWeekPrimeTimeStart.Time = ConfigurationClasses.SettingsManager.Instance.ProgramScheduleOutputSettings.WeekPrimeTimeStart;
            timeEditWeekPrimeTimeEnd.Time = ConfigurationClasses.SettingsManager.Instance.ProgramScheduleOutputSettings.WeekPrimeTimeEnd;
            timeEditSundayPrimeTimeStart.Time = ConfigurationClasses.SettingsManager.Instance.ProgramScheduleOutputSettings.SundayPrimeTimeStart;
            timeEditSundayPrimeTimeEnd.Time = ConfigurationClasses.SettingsManager.Instance.ProgramScheduleOutputSettings.SundayPrimeTimeEnd;
            #endregion
        }

        private void FormOutputParameters_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                #region Text Settings Tab
                ConfigurationClasses.SettingsManager.Instance.ProgramScheduleOutputSettings.HeaderFont = comboBoxEditHeaderFont.Tag as global::ProgramManager.CoreObjects.OutputFont;
                ConfigurationClasses.SettingsManager.Instance.ProgramScheduleOutputSettings.FooterFont = comboBoxEditFooterFont.Tag as global::ProgramManager.CoreObjects.OutputFont;
                ConfigurationClasses.SettingsManager.Instance.ProgramScheduleOutputSettings.BodyFont = comboBoxEditBodyFont.Tag as global::ProgramManager.CoreObjects.OutputFont;
                ConfigurationClasses.SettingsManager.Instance.ProgramScheduleOutputSettings.UsePrimeTimeSpecialFontSize = checkEditPrimeTimeSpecialFontSize.Checked;
                ConfigurationClasses.SettingsManager.Instance.ProgramScheduleOutputSettings.WeekPrimeTimeStart = timeEditWeekPrimeTimeStart.Time;
                ConfigurationClasses.SettingsManager.Instance.ProgramScheduleOutputSettings.WeekPrimeTimeEnd = timeEditWeekPrimeTimeEnd.Time;
                ConfigurationClasses.SettingsManager.Instance.ProgramScheduleOutputSettings.SundayPrimeTimeStart = timeEditSundayPrimeTimeStart.Time;
                ConfigurationClasses.SettingsManager.Instance.ProgramScheduleOutputSettings.SundayPrimeTimeEnd = timeEditSundayPrimeTimeEnd.Time;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
                #endregion
            }
        }

        #region Schedule Event Handlers
        private void dateEditWeekStart_EditValueChanged(object sender, System.EventArgs e)
        {
            laWeekEnd.Text = string.Format("Sunday:   {0}", dateEditWeekStart.DateTime.AddDays(6).ToString("MM/dd/yy"));
        }

        private void dateEditWeekStart_DrawItem(object sender, DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs e)
        {
            if (e.Date.DayOfWeek == DayOfWeek.Monday)
            {
                e.Style.ForeColor = Color.Black;
                e.Style.Font = new System.Drawing.Font(e.Style.Font.Name, e.Style.Font.Size, FontStyle.Bold);
            }
            else
            {
                e.Style.ForeColor = Color.Gray;
                if (e.Date == DateTime.Today)
                {
                    RectangleF rect = new RectangleF(e.Bounds.Location, e.Bounds.Size);
                    Color backColor = Color.White;
                    e.Graphics.FillRectangle(new SolidBrush(backColor), rect);
                    e.Graphics.DrawString(e.Date.Day.ToString(), e.Style.Font,
                        new SolidBrush(e.Style.ForeColor), rect, e.Style.GetStringFormat());
                    e.Handled = true;
                }
            }
        }

        private void dateEditWeekStart_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            if (e.Value != null)
            {
                DateTime temp = DateTime.MinValue;
                if (DateTime.TryParse(e.Value.ToString(), out temp))
                {
                    while (temp.DayOfWeek != DayOfWeek.Monday)
                        temp = temp.AddDays(-1);
                    e.Value = temp;
                }
            }
        }

        private void simpleButtonAddWeek_Click(object sender, EventArgs e)
        {
            global::ProgramManager.CoreObjects.Week week = new global::ProgramManager.CoreObjects.Week();
            week.DateStart = dateEditWeekStart.DateTime;
            week.DateEnd = week.DateStart.AddDays(6);
            if (_weeks.Where(x => x.DateStart.Year == week.DateStart.Year && x.DateStart.Month == week.DateStart.Month && x.DateStart.Day == week.DateStart.Day).Count() == 0)
                _weeks.Add(week);
            gridControlWeeks.RefreshDataSource();
        }

        private void repositoryItemButtonEdit_Click(object sender, EventArgs e)
        {
            _weeks.Remove(_weeks[gridViewWeeks.GetDataSourceRowIndex(gridViewWeeks.FocusedRowHandle)]);
            gridControlWeeks.RefreshDataSource();
        }

        private void buttonXOrientation_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonX button = sender as DevComponents.DotNetBar.ButtonX;
            if (!button.Checked)
            {
                buttonXLandscape.Checked = false;
                buttonXPortrait.Checked = false;
            }
            button.Checked = true;
        }
        #endregion

        #region Text Settings Event Handlers
        private void comboBoxEditHeaderFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            global::ProgramManager.CoreObjects.OutputFont font = ConfigurationClasses.ListManager.Instance.HeaderFonts[comboBoxEditHeaderFont.SelectedIndex];
            comboBoxEditHeaderFont.Tag = font;
            comboBoxEditHeaderFont.Font = font.FontObject;
            comboBoxEditHeaderFont.Properties.AppearanceFocused.Font = font.FontObject;
        }

        private void comboBoxEditFooterFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            global::ProgramManager.CoreObjects.OutputFont font = ConfigurationClasses.ListManager.Instance.FooterFonts[comboBoxEditFooterFont.SelectedIndex];
            comboBoxEditFooterFont.Tag = font;
            comboBoxEditFooterFont.Font = font.FontObject;
            comboBoxEditFooterFont.Properties.AppearanceFocused.Font = font.FontObject;
        }

        private void comboBoxEditBodyFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            global::ProgramManager.CoreObjects.OutputFont font = ConfigurationClasses.ListManager.Instance.BodyFonts[comboBoxEditBodyFont.SelectedIndex];
            comboBoxEditBodyFont.Tag = font;
            comboBoxEditBodyFont.Font = font.FontObject;
            comboBoxEditBodyFont.Properties.AppearanceFocused.Font = font.FontObject;
            groupBoxSpecialFont.Enabled = font.Size != 8;
        }

        private void checkEditSpecialFontSize_CheckedChanged(object sender, EventArgs e)
        {
            laWeekPrimeTime.Enabled = checkEditPrimeTimeSpecialFontSize.Checked;
            timeEditWeekPrimeTimeStart.Enabled = checkEditPrimeTimeSpecialFontSize.Checked;
            timeEditWeekPrimeTimeEnd.Enabled = checkEditPrimeTimeSpecialFontSize.Checked;
            laSundayPrimeTime.Enabled = checkEditPrimeTimeSpecialFontSize.Checked;
            timeEditSundayPrimeTimeStart.Enabled = checkEditPrimeTimeSpecialFontSize.Checked;
            timeEditSundayPrimeTimeEnd.Enabled = checkEditPrimeTimeSpecialFontSize.Checked;
        }
        #endregion
    }
}