using System.Drawing;
using SalesLibraries.Business.Entities.Common;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent
{
	public class CalendarSettings : SettingsContainer
	{
		private bool _enabled;
		public bool Enabled
		{
			get { return _enabled; }
			set
			{
				if (_enabled != value)
					OnSettingsChanged();
				_enabled = value;
			}
		}

		private string _path;
		public string Path
		{
			get { return _path; }
			set
			{
				if (_path != value)
					OnSettingsChanged();
				_path = value;
			}
		}

		private Color _calendarBackColor;
		public Color CalendarBackColor
		{
			get { return _calendarBackColor; }
			set
			{
				if (_calendarBackColor != value)
					OnSettingsChanged();
				_calendarBackColor = value;
			}
		}

		private Color _calendarBorderColor;
		public Color CalendarBorderColor
		{
			get { return _calendarBorderColor; }
			set
			{
				if (_calendarBorderColor != value)
					OnSettingsChanged();
				_calendarBorderColor = value;
			}
		}

		private Color _calendarHeaderBackColor;
		public Color CalendarHeaderBackColor
		{
			get { return _calendarHeaderBackColor; }
			set
			{
				if (_calendarHeaderBackColor != value)
					OnSettingsChanged();
				_calendarHeaderBackColor = value;
			}
		}

		private Color _calendarHeaderForeColor;
		public Color CalendarHeaderForeColor
		{
			get { return _calendarHeaderForeColor; }
			set
			{
				if (_calendarHeaderForeColor != value)
					OnSettingsChanged();
				_calendarHeaderForeColor = value;
			}
		}

		private Color _monthHeaderBackColor;
		public Color MonthHeaderBackColor
		{
			get { return _monthHeaderBackColor; }
			set
			{
				if (_monthHeaderBackColor != value)
					OnSettingsChanged();
				_monthHeaderBackColor = value;
			}
		}

		private Color _monthHeaderForeColor;
		public Color MonthHeaderForeColor
		{
			get { return _monthHeaderForeColor; }
			set
			{
				if (_monthHeaderForeColor != value)
					OnSettingsChanged();
				_monthHeaderForeColor = value;
			}
		}

		private Color _monthBodyBackColor;
		public Color MonthBodyBackColor
		{
			get { return _monthBodyBackColor; }
			set
			{
				if (_monthBodyBackColor != value)
					OnSettingsChanged();
				_monthBodyBackColor = value;
			}
		}

		private Color _monthBodyForeColor;
		public Color MonthBodyForeColor
		{
			get { return _monthBodyForeColor; }
			set
			{
				if (_monthBodyForeColor != value)
					OnSettingsChanged();
				_monthBodyForeColor = value;
			}
		}

		private Color _sweepBackColor;
		public Color SweepBackColor
		{
			get { return _sweepBackColor; }
			set
			{
				if (_sweepBackColor != value)
					OnSettingsChanged();
				_sweepBackColor = value;
			}
		}

		private Color _sweepForeColor;
		public Color SweepForeColor
		{
			get { return _sweepForeColor; }
			set
			{
				if (_sweepForeColor != value)
					OnSettingsChanged();
				_sweepForeColor = value;
			}
		}

		private Color _deadLinksForeColor;
		public Color DeadLinksForeColor
		{
			get { return _deadLinksForeColor; }
			set
			{
				if (_deadLinksForeColor != value)
					OnSettingsChanged();
				_deadLinksForeColor = value;
			}
		}

		public CalendarSettings()
		{
			ResetColors();
		}

		public void ResetColors()
		{
			CalendarBackColor = Color.AliceBlue;
			CalendarBorderColor = Color.DarkGray;
			CalendarHeaderBackColor = Color.Azure;
			CalendarHeaderForeColor = Color.Black;
			MonthHeaderBackColor = Color.AliceBlue;
			MonthHeaderForeColor = Color.Black;
			MonthBodyBackColor = Color.AliceBlue;
			MonthBodyForeColor = Color.Black;
			SweepBackColor = Color.LightGray;
			SweepForeColor = Color.Black;
			DeadLinksForeColor = Color.Black;
		}
	}
}
