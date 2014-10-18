using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using OutlookSalesDepotAddIn.BusinessClasses;
using SalesDepot.CommonGUI.Forms;

namespace OutlookSalesDepotAddIn.Controls.Viewers
{
	[ToolboxItem(false)]
	public partial class PowerPointViewer : UserControl, IFileViewer
	{
		#region Properties
		public LibraryLink File { get; private set; }

		public string DisplayName
		{
			get { return File.DisplayName; }
		}

		public string CriteriaOverlap
		{
			get { return File.CriteriaOverlap; }
		}

		public Image Widget
		{
			get { return File.Widget; }
		}
		#endregion

		public PowerPointViewer(LibraryLink file)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			File = file;
			if (System.IO.File.Exists(File.LocalPath))
			{
				string tempPath = Path.Combine(Utils.Manager.TempFolder.FullName, DateTime.Now.ToString("yyyyMMdd-hmmsstt") + Path.GetExtension(File.LocalPath));
				System.IO.File.Copy(File.LocalPath, tempPath, true);
				new FileInfo(tempPath);
			}

			pictureBoxPreview.Image = null;
			laSlideNumber.Text = string.Empty;

			if (File.PreviewContainer != null)
			{
				laFileInfo.Text = "File Added: " + File.AddDate.ToString("MM/dd/yy");

				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Loading Presentation...";
					var thread = new Thread(delegate()
					{
						if (File.PreviewContainer != null)
							File.PreviewContainer.GetPreviewImages();
					});
					thread.Start();

					form.Show();

					while (thread.IsAlive)
						Application.DoEvents();

					form.Close();
				}
				
				comboBoxEditSlides.SelectedIndexChanged -= comboBoxEditSlides_SelectedIndexChanged;
				comboBoxEditSlides.Properties.Items.Clear();
				comboBoxEditSlides.Properties.Items.AddRange(File.PreviewContainer.Slides.Select(x => x.Index + 1).ToArray());
				if (File.PreviewContainer.Slides.Count > 0)
					comboBoxEditSlides.SelectedIndex = 0;
				comboBoxEditSlides_SelectedIndexChanged(null, null);
				comboBoxEditSlides.SelectedIndexChanged += comboBoxEditSlides_SelectedIndexChanged;
			}
		}

		#region IFileViewer Methods
		public void Attach()
		{
			LinkManager.Instance.AttachFile(File);
		}

		public void ReleaseResources()
		{
			pictureBoxPreview.Image = null;
			if (File.PreviewContainer != null)
				File.PreviewContainer.ReleasePreviewImages();
		}
		#endregion

		#region GUI Event Handlers
		private void comboBoxEditSlides_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (File != null)
			{
				if (File.PreviewContainer != null)
				{
					File.PreviewContainer.SelectedIndex = comboBoxEditSlides.SelectedIndex;
					pictureBoxPreview.Image = File.PreviewContainer.SelectedSlide;
					laSlideNumber.Text = string.Format("Slide {0} of {1}", new object[] { (File.PreviewContainer.SelectedIndex + 1).ToString(), File.PreviewContainer.Slides.Count.ToString() });
				}
			}
		}

		private void comboBoxEditSlides_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (File != null)
			{
				if (File.PreviewContainer != null)
				{
					if (e.Button.Index == 1)
					{
						File.PreviewContainer.SelectedIndex++;
						if (File.PreviewContainer.SelectedIndex >= File.PreviewContainer.Slides.Count)
							File.PreviewContainer.SelectedIndex = 0;
						comboBoxEditSlides.SelectedIndex = File.PreviewContainer.SelectedIndex;
					}
					else if (e.Button.Index == 2)
					{
						File.PreviewContainer.SelectedIndex--;
						if (File.PreviewContainer.SelectedIndex < 0)
							File.PreviewContainer.SelectedIndex = File.PreviewContainer.Slides.Count - 1;
						comboBoxEditSlides.SelectedIndex = File.PreviewContainer.SelectedIndex;
					}
				}
			}
		}

		private void pnNavigationArea_Resize(object sender, EventArgs e)
		{
			comboBoxEditSlides.Left = (pnNavigationArea.Width - comboBoxEditSlides.Width) / 2;
		}
		#endregion
	}
}