using System.Drawing;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.OvernightsCalendar
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class DayControl : Label
    {
        private Cursor _storedCursor;

        public BusinessClasses.CalendarDay Data { get; private set; }

        public DayControl(BusinessClasses.CalendarDay data)
        {
            InitializeComponent();
            this.Data = data;
            this.Text = data.Date.ToString("dd");
            RefreshColors();
            RefreshFont();
        }

        public void RefreshColors()
        {
            if (this.Data.IsSweepDay)
            {
                this.BackColor = this.Data.Parent.Parent.Parent.SweepBackColor;
                if (this.Data.LinkedFile == null)
                    this.ForeColor = this.Data.Parent.Parent.Parent.DeadLinksForeColor;
                else
                    this.ForeColor = this.Data.Parent.Parent.Parent.SweepForeColor;
            }
            else
            {
                this.BackColor = this.Data.Parent.Parent.Parent.MonthBodyBackColor;
                if (this.Data.LinkedFile == null)
                    this.ForeColor = this.Data.Parent.Parent.Parent.DeadLinksForeColor;
                else
                    this.ForeColor = this.Data.Parent.Parent.Parent.MonthBodyForeColor;
            }
            this.Refresh();
        }

        public void RefreshFont()
        {
            this.Font = new System.Drawing.Font(this.Font.Name, ConfigurationClasses.SettingsManager.Instance.CalendarFontSize, this.Font.Style);
        }

        private void DayControl_Click(object sender, System.EventArgs e)
        {
            if (this.Data.LinkedFile != null)
                BusinessClasses.LinkManager.Instance.OpenLink(this.Data.LinkedFile);
        }

        private void DayControl_MouseEnter(object sender, System.EventArgs e)
        {
            _storedCursor = this.Cursor;
            this.Cursor = Cursors.Hand;
        }

        private void DayControl_MouseLeave(object sender, System.EventArgs e)
        {
            this.Cursor = _storedCursor;
        }

        private void DayControl_MouseDown(object sender, MouseEventArgs e)
        {
            this.BackColor = Color.Blue;
            this.ForeColor = Color.White;
        }

        private void DayControl_MouseUp(object sender, MouseEventArgs e)
        {
            RefreshColors();
        }
    }
}
