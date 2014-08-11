using System;
using System.Drawing;
using System.Windows.Forms;

namespace FileManager.ToolForms
{
	public partial class FormProgressSync : Form
	{
		public FormProgressSync()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laProgress.Font = new Font(laProgress.Font.FontFamily, laProgress.Font.Size - 2, laProgress.Font.Style);
				laTime.Font = new Font(laTime.Font.FontFamily, laTime.Font.Size - 2, laTime.Font.Style);
				ckCloseAfterSync.Font = new Font(ckCloseAfterSync.Font.FontFamily, ckCloseAfterSync.Font.Size - 2, ckCloseAfterSync.Font.Style);
			}

			Left = Screen.PrimaryScreen.WorkingArea.Width - Width - 20;
			Top = Screen.PrimaryScreen.WorkingArea.Height - Height - 20;

			InitForm();
		}

		public bool CloseAfterSync
		{
			get { return ckCloseAfterSync.Checked; }
			set { ckCloseAfterSync.Checked = value; }
		}

		public event EventHandler<EventArgs> ProcessAborted;

		protected virtual void InitForm() { }

		private void FormProgress_Shown(object sender, EventArgs e)
		{
			laProgress.Focus();
			circularProgress.IsRunning = true;

			var timer = new Timer();
			timer.Interval = 1000;
			int ticks = 0;
			timer.Tick += (timerSender, timerE) =>
			{
				ticks++;
				int hours = ticks / 3600;
				int minutes = (ticks - (hours * 3600)) / 60;
				int seconds = ticks - (hours * 3600) - (minutes * 60);
				FormMain.Instance.Invoke((MethodInvoker)delegate
				{
					laTime.Text = string.Format("{0}:{1}:{2}", hours.ToString("00"), minutes.ToString("00"), seconds.ToString("00"));
					Application.DoEvents();
				});
			};
			timer.Start();
		}

		private void pbCancel_Click(object sender, EventArgs e)
		{
			laProgress.Text = "Aborting process...";
			if (ProcessAborted != null)
				ProcessAborted(this, new EventArgs());
		}

		#region Picture Box Clicks Habdlers
		/// <summary>
		///     Buttonize the PictureBox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top -= 1;
		}
		#endregion
	}

	public class FormProgressConverVideo : FormProgressSync
	{
		protected override void InitForm()
		{
			laProgress.Text = "Converting Video...";
			panelEx.Style.BackColor1.Color = Color.Black;
			panelEx.Style.BackColor2.Color = Color.Black;
			panelExCancel.Style.BackColor1.Color = Color.Black;
			panelExCancel.Style.BackColor2.Color = Color.Black;
		}
	}

	public class FormProgressSyncFilesIPad : FormProgressSync
	{
		protected override void InitForm()
		{
			laProgress.Text = "Syncing Files...";
			panelEx.Style.BackColor1.Color = Color.Blue;
			panelEx.Style.BackColor2.Color = Color.Blue;
			panelExCancel.Style.BackColor1.Color = Color.Blue;
			panelExCancel.Style.BackColor2.Color = Color.Blue;
		}
	}

	public class FormProgressSyncFilesRegular : FormProgressSync
	{
		protected override void InitForm()
		{
			laProgress.Text = "Syncing Libraries...";
			panelEx.Style.BackColor1.Color = Color.Green;
			panelEx.Style.BackColor2.Color = Color.Green;
			panelExCancel.Style.BackColor1.Color = Color.Green;
			panelExCancel.Style.BackColor2.Color = Color.Green;
		}
	}
}