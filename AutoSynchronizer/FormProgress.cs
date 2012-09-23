using System;
using System.Windows.Forms;
using System.Drawing;

namespace AutoSynchronizer
{
    public partial class FormProgress : Form
    {
        public event EventHandler<EventArgs> ProcessAborted;

        public FormProgress()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laProgress.Font = new System.Drawing.Font(laProgress.Font.FontFamily, laProgress.Font.Size - 2, laProgress.Font.Style);
                laTime.Font = new System.Drawing.Font(laTime.Font.FontFamily, laTime.Font.Size - 2, laTime.Font.Style);
            }

            this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width - 20;
            this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height - 20;

            InitForm();
        }

        protected virtual void InitForm()
        {
        }

        private void FormProgress_Shown(object sender, System.EventArgs e)
        {
            laProgress.Focus();
            circularProgress.IsRunning = true;

            Timer timer = new Timer();
            timer.Interval = 1000;
            int ticks = 0;
            timer.Tick += new EventHandler((timerSender, timerE) =>
            {
                ticks++;
                int hours = ticks / 3600;
                int minutes = (ticks - (hours * 3600)) / 60;
                int seconds = ticks - (hours * 3600) - (minutes * 60);
                FormHidden.Instance.Invoke((MethodInvoker)delegate()
                {
                    laTime.Text = string.Format("{0}:{1}:{2}", hours.ToString("00"), minutes.ToString("00"), seconds.ToString("00"));
                    Application.DoEvents();
                });
            });
            timer.Start();
        }

        #region Picture Box Clicks Habdlers
        /// <summary>
        /// Buttonize the PictureBox 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)(sender);
            pic.Top += 1;
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)(sender);
            pic.Top -= 1;
        }
        #endregion

        private void pbCancel_Click(object sender, EventArgs e)
        {
            laProgress.Text = "Aborting process...";
            if (this.ProcessAborted != null)
                this.ProcessAborted(this, new EventArgs());
        }
    }

    public class FormProgressSyncFilesIPad : FormProgress
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

    public class FormProgressSyncFilesRegular : FormProgress
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