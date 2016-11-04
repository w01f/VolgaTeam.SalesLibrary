using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Utils;
using SalesLibraries.Common.Objects.Graphics;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.ImageGallery
{
	public partial class FormImageGallery : MetroForm
	{
		private readonly IEnumerable<ImageSourceGroup> _imageGroups;

		public FormImageGallery(IEnumerable<ImageSourceGroup> imageGroups)
		{
			_imageGroups = imageGroups;
			InitializeComponent();
		}

		public BaseImageSource SelectedImageSource { get; private set; }

		private void OnGroupDoubleClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void FormImageGallery_Load(object sender, EventArgs e)
		{
			xtraTabControlGroups.TabPages.Clear();
			foreach (var imageGroup in _imageGroups)
			{
				var tabPage = new RegularImagesContainer(imageGroup);
				tabPage.OnImageDoubleClick += OnGroupDoubleClick;
				xtraTabControlGroups.TabPages.Add(tabPage);
			}
			xtraTabControlGroups.SelectedPageChanged += (o, args) =>
			{
				if (args.Page == null) return;
				((BaseLinkImagesContainer)args.Page).Init();
			};
			((BaseLinkImagesContainer)xtraTabControlGroups.SelectedTabPage).Init();
			xtraTabControlGroups.ShowTabHeader = _imageGroups.Count() > 1 ? DefaultBoolean.True : DefaultBoolean.False;
		}

		private void FormImageGallery_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				var selectedPage = (BaseLinkImagesContainer)xtraTabControlGroups.SelectedTabPage;
				SelectedImageSource = selectedPage.SelectedImageSource;
			}
			foreach (var imageGroup in xtraTabControlGroups.TabPages.OfType<BaseLinkImagesContainer>())
				imageGroup.Release();
			xtraTabControlGroups.TabPages.Clear();
		}
	}
}