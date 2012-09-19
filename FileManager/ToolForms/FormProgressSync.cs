using System;
using System.Windows.Forms;
using System.Drawing;

namespace FileManager.ToolForms
{
    public partial class FormProgressSync : Form
    {
        public event EventHandler<EventArgs> ProcessAborted;

        public bool CloseAfterSync
        {
            get
            {
                return ckCloseAfterSync.Checked;
            }
            set
            {
                ckCloseAfterSync.Checked = value;
            }
        }

        public FormProgressSync()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laProgress.Font = new System.Drawing.Font(laProgress.Font.FontFamily, laProgress.Font.Size - 2, laProgress.Font.Style);
                laTime.Font = new System.Drawing.Font(laTime.Font.FontFamily, laTime.Font.Size - 2, laTime.Font.Style);
                ckCloseAfterSync.Font = new System.Drawing.Font(ckCloseAfterSync.Font.FontFamily, ckCloseAfterSync.Font.Size - 2, ckCloseAfterSync.Font.Style);
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
                int hours = ticks / 360;
                int minutes = (ticks - (hours * 360)) / 60;
                int seconds = ticks - (hours * 360) - (minutes * 60);
                FormMain.Instance.Invoke((MethodInvoker)delegate()
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

    public class FormProgressSyncData : FormProgressSync
    {
        protected override void InitForm()
        {
            laProgress.Text = "Syncing Database...";
            panelEx.Style.BackColor1.Color = Color.Red;
            panelEx.Style.BackColor2.Color = Color.Red;
            panelExCancel.Style.BackColor1.Color = Color.Red;
            panelExCancel.Style.BackColor2.Color = Color.Red;
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