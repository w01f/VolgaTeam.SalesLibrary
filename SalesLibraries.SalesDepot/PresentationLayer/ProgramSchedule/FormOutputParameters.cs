using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors.Calendar;
using DevExpress.XtraEditors.Controls;
using ProgramManager.CoreObjects;
using SalesLibraries.SalesDepot.Business.ProgramSchedule;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.ProgramSchedule
{
	public partial class FormOutputParameters : MetroForm
	{
		private readonly ProgramScheduleContext _dataContext;
		private readonly DateTime _selectedDate;
		private readonly List<Week> _weeks = new List<Week>();

		public FormOutputParameters(ProgramScheduleContext dataContext, DateTime selectedDate)
		{
			_dataContext = dataContext;
			_selectedDate = selectedDate;
			InitializeComponent();
		}

		public Station Station
		{
			get { return (Station)comboBoxEditStation.EditValue; }
		}

		public Week[] Weeks
		{
			get { return _weeks.ToArray(); }
		}

		public bool Landscape
		{
			get { return buttonXLandscape.Checked; }
		}

		private void FormOutputParameters_Load(object sender, EventArgs e)
		{
			#region Schedule Tab
			comboBoxEditStation.Properties.Items.AddRange(_dataContext.Stations);
			comboBoxEditStation.EditValue = _dataContext.ActiveStation;

			var currentDate = _selectedDate;
			while (currentDate.DayOfWeek != DayOfWeek.Monday)
				currentDate = currentDate.AddDays(-1);
			dateEditWeekStart.DateTime = currentDate;

			var week = new Week();
			week.DateStart = dateEditWeekStart.DateTime;
			week.DateEnd = week.DateStart.AddDays(6);
			_weeks.Add(week);

			gridControlWeeks.DataSource = _weeks;
			#endregion

			#region Text Settings Tab
			comboBoxEditHeaderFont.Properties.Items.Clear();
			comboBoxEditHeaderFont.Properties.Items.AddRange(MainController.Instance.Settings.ProgramScheduleSettings.HeaderFonts);
			comboBoxEditHeaderFont.EditValue = MainController.Instance.Settings.ProgramScheduleSettings.HeaderFonts.FirstOrDefault(font =>
				font.ToString() == MainController.Instance.Settings.ProgramScheduleSettings.HeaderFont.ToString()) ??
				MainController.Instance.Settings.ProgramScheduleSettings.HeaderFonts.FirstOrDefault();

			comboBoxEditFooterFont.Properties.Items.Clear();
			comboBoxEditFooterFont.Properties.Items.AddRange(MainController.Instance.Settings.ProgramScheduleSettings.FooterFonts);
			comboBoxEditFooterFont.EditValue = MainController.Instance.Settings.ProgramScheduleSettings.FooterFonts.FirstOrDefault(font =>
				font.ToString() == MainController.Instance.Settings.ProgramScheduleSettings.FooterFonts.ToString()) ??
				MainController.Instance.Settings.ProgramScheduleSettings.FooterFonts.FirstOrDefault();

			comboBoxEditBodyFont.Properties.Items.Clear();
			comboBoxEditBodyFont.Properties.Items.AddRange(MainController.Instance.Settings.ProgramScheduleSettings.BodyFonts);
			comboBoxEditBodyFont.EditValue = MainController.Instance.Settings.ProgramScheduleSettings.BodyFonts.FirstOrDefault(font =>
				font.ToString() == MainController.Instance.Settings.ProgramScheduleSettings.BodyFonts.ToString()) ??
				MainController.Instance.Settings.ProgramScheduleSettings.BodyFonts.FirstOrDefault();

			checkEditPrimeTimeSpecialFontSize.Checked = MainController.Instance.Settings.ProgramScheduleSettings.UsePrimeTimeSpecialFontSize;
			timeEditWeekPrimeTimeStart.Time = MainController.Instance.Settings.ProgramScheduleSettings.WeekPrimeTimeStart;
			timeEditWeekPrimeTimeEnd.Time = MainController.Instance.Settings.ProgramScheduleSettings.WeekPrimeTimeEnd;
			timeEditSundayPrimeTimeStart.Time = MainController.Instance.Settings.ProgramScheduleSettings.SundayPrimeTimeStart;
			timeEditSundayPrimeTimeEnd.Time = MainController.Instance.Settings.ProgramScheduleSettings.SundayPrimeTimeEnd;
			#endregion
		}

		private void FormOutputParameters_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				#region Text Settings Tab
				MainController.Instance.Settings.ProgramScheduleSettings.HeaderFont = comboBoxEditHeaderFont.EditValue as OutputFont;
				MainController.Instance.Settings.ProgramScheduleSettings.FooterFont = comboBoxEditFooterFont.EditValue as OutputFont;
				MainController.Instance.Settings.ProgramScheduleSettings.BodyFont = comboBoxEditBodyFont.EditValue as OutputFont;
				MainController.Instance.Settings.ProgramScheduleSettings.UsePrimeTimeSpecialFontSize = checkEditPrimeTimeSpecialFontSize.Checked;
				MainController.Instance.Settings.ProgramScheduleSettings.WeekPrimeTimeStart = timeEditWeekPrimeTimeStart.Time;
				MainController.Instance.Settings.ProgramScheduleSettings.WeekPrimeTimeEnd = timeEditWeekPrimeTimeEnd.Time;
				MainController.Instance.Settings.ProgramScheduleSettings.SundayPrimeTimeStart = timeEditSundayPrimeTimeStart.Time;
				MainController.Instance.Settings.ProgramScheduleSettings.SundayPrimeTimeEnd = timeEditSundayPrimeTimeEnd.Time;
				MainController.Instance.Settings.SaveSettings();
				#endregion
			}
		}

		#region Schedule Event Handlers
		private void dateEditWeekStart_EditValueChanged(object sender, EventArgs e)
		{
			laWeekEnd.Text = string.Format("Sunday:   {0}", dateEditWeekStart.DateTime.AddDays(6).ToString("MM/dd/yy"));
		}

		private void dateEditWeekStart_DrawItem(object sender, CustomDrawDayNumberCellEventArgs e)
		{
			if (e.Date.DayOfWeek == DayOfWeek.Monday)
			{
				e.Style.ForeColor = Color.Black;
				e.Style.Font = new Font(e.Style.Font.Name, e.Style.Font.Size, FontStyle.Bold);
			}
			else
			{
				e.Style.ForeColor = Color.Gray;
				if (e.Date == DateTime.Today)
				{
					var rect = new RectangleF(e.Bounds.Location, e.Bounds.Size);
					Color backColor = Color.White;
					e.Graphics.FillRectangle(new SolidBrush(backColor), rect);
					e.Graphics.DrawString(e.Date.Day.ToString(), e.Style.Font,
						new SolidBrush(e.Style.ForeColor), rect, e.Style.GetStringFormat());
					e.Handled = true;
				}
			}
		}

		private void dateEditWeekStart_CloseUp(object sender, CloseUpEventArgs e)
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
			var week = new Week();
			week.DateStart = dateEditWeekStart.DateTime;
			week.DateEnd = week.DateStart.AddDays(6);
			if (_weeks.Count(x => x.DateStart.Year == week.DateStart.Year && x.DateStart.Month == week.DateStart.Month && x.DateStart.Day == week.DateStart.Day) == 0)
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
			var button = sender as ButtonX;
			if (button != null && !button.Checked)
			{
				buttonXLandscape.Checked = false;
				buttonXPortrait.Checked = false;
			}
			button.Checked = true;
		}
		#endregion

		#region Text Settings Event Handlers
		private void comboBoxEditHeaderFont_EditValueChanged(object sender, EventArgs e)
		{
			var font = comboBoxEditHeaderFont.EditValue as OutputFont;
			comboBoxEditHeaderFont.Font = font.FontObject;
			comboBoxEditHeaderFont.Properties.AppearanceFocused.Font = font.FontObject;
		}

		private void comboBoxEditFooterFont_EditValueChanged(object sender, EventArgs e)
		{
			var font = comboBoxEditFooterFont.EditValue as OutputFont;
			comboBoxEditFooterFont.Font = font.FontObject;
			comboBoxEditFooterFont.Properties.AppearanceFocused.Font = font.FontObject;
		}

		private void comboBoxEditBodyFont_EditValueChanged(object sender, EventArgs e)
		{
			var font = comboBoxEditBodyFont.EditValue as OutputFont;
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