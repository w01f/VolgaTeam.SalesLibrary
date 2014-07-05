using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using OvernightsCalendarViewer.ConfigurationClasses;
using OvernightsCalendarViewer.PresentationClasses.Decorators;
using OvernightsCalendarViewer.ToolForms;

namespace OvernightsCalendarViewer.PresentationClasses.OvernightsCalendar
{
	[ToolboxItem(false)]
	public partial class OvernightsCalendarControl : UserControl
	{
		private bool _buildInProgress;

		public OvernightsCalendarControl(LibraryDecorator parent)
		{
			InitializeComponent();
			ParentDecorator = parent;
			Dock = DockStyle.Fill;
			Parts = new List<CalendarPartControl>();
			PartToggles = new List<ButtonItem>();
		}

		public void DisposeCalendar()
		{
			foreach (var calendarPartControl in Parts)
			{
				calendarPartControl.Parent = null;
				calendarPartControl.Dispose();
				Application.DoEvents();
			}
		}

		public LibraryDecorator ParentDecorator { get; private set; }
		public List<CalendarPartControl> Parts { get; private set; }
		public List<ButtonItem> PartToggles { get; private set; }
		public bool ViewBuilded { get; set; }

		public void Build()
		{
			if (!ViewBuilded)
			{
				_buildInProgress = true;
				Controls.Clear();
				Parts.Clear();
				PartToggles.Clear();
				foreach (var calendarPart in ParentDecorator.Library.OvernightsCalendar.Parts)
				{
					var partControl = new CalendarPartControl(calendarPart);
					Parts.Add(partControl);
					Application.DoEvents();
				}
				Controls.AddRange(Parts.ToArray());

				var selectedPart = Parts.FirstOrDefault(p => p.PartData.Name == SettingsManager.Instance.SelectedCalendar && p.PartData.Enabled);
				if (selectedPart == null)
					selectedPart = Parts.FirstOrDefault(p => p.PartData.Enabled);
				if (selectedPart != null)
				{
					selectedPart.Build();
					selectedPart.BringToFront();
				}

				foreach (var partControl in Parts)
				{
					var button = new ButtonItem();
					button.Text = partControl.PartData.Name;
					button.Tag = partControl;
					button.Enabled = partControl.PartData.Enabled;
					button.Checked = partControl == selectedPart;
					button.Click += SelectedPartClick;
					button.CheckedChanged += SelectedPartChanged;
					PartToggles.Add(button);
				}
				_buildInProgress = false;
			}
			ViewBuilded = true;
		}

		public void RefreshColors()
		{
			foreach (var partControl in Parts)
				partControl.RefreshColors();
		}

		public void RefreshFont()
		{
			foreach (var partControl in Parts)
				partControl.RefreshFont();
		}

		private void SelectedPartClick(object sender, EventArgs e)
		{
			var selectedPartButton = sender as ButtonItem;
			if (selectedPartButton == null || selectedPartButton.Checked) return;
			PartToggles.ForEach(b => b.Checked = false);
			selectedPartButton.Checked = true;
		}

		private void SelectedPartChanged(object sender, EventArgs e)
		{
			if (_buildInProgress) return;
			var selectedPartButton = sender as ButtonItem;
			if (selectedPartButton == null || !selectedPartButton.Checked) return;
			var selectedPart = selectedPartButton.Tag as CalendarPartControl;
			if (selectedPart == null) return;
			SettingsManager.Instance.SelectedCalendar = selectedPart.PartData.Name;
			SettingsManager.Instance.SaveSettings();
			if (!selectedPart.ViewBuilded)
			{
				using (var formProgress = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					formProgress.TopMost = true;
					formProgress.laProgress.Text = "Chill-Out for a few seconds....\nLoading Your Overnights Calendar";
					var thread = new Thread(() => Invoke((MethodInvoker)(selectedPart.Build)));
					formProgress.Show();
					Application.DoEvents();
					thread.Start();
					while (thread.IsAlive)
						Application.DoEvents();
					formProgress.Close();
					FormMain.Instance.ribbonControl.Enabled = true;
				}
			}
			selectedPart.BringToFront();
		}
	}
}