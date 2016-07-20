using System;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.CommonGUI.Wallbin.Folders;
using SalesLibraries.CommonGUI.Wallbin.Views;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Folders
{
	public partial class AccordionFolderBox : BaseFolderBox
	{
		#region Public Properties
		public override IWallbinViewFormat FormatState => MainController.Instance.WallbinViews.FormatState;

		private bool _isExpanded;
		public bool IsExpanded
		{
			get { return _isExpanded; }
			set
			{
				_isExpanded = value;
				buttonXHeader.Checked = _isExpanded;
				if (_isExpanded && ContentExpanded != null)
					ContentExpanded(this, EventArgs.Empty);
				UpdateContent(true);
			}
		}

		public event EventHandler<EventArgs> ContentExpanded;
		#endregion

		public AccordionFolderBox(LibraryFolder dataSource) : base(dataSource)
		{
			new FolderBoxInitializer().Initialize(this);
		}

		public override void UpdateContent(bool handleEvents)
		{
			if (IsExpanded)
			{
				pnBorders.Padding = new Padding(1);
				pnHeaderBorder.Padding = new Padding(0, 0, 0, 1);
			}
			else
			{
				pnBorders.Padding = new Padding(0);
				pnHeaderBorder.Padding = new Padding(0);
			}
			base.UpdateContent(handleEvents);
		}

		protected override void SetupView()
		{
			buttonXHeader.Text = String.Format("{0} <font color=\"#727272\">({1})</font>",
				DataSource.Banner.Enable && DataSource.Banner.ShowText ? DataSource.Banner.Text : DataSource.Name,
				DataSource.Links.GetDisplayedLinks().Count()
				);
			base.SetupView();
		}

		protected override void UpdateControlHeight()
		{
			Height = pnHeaderBorder.Height +
				pnBorders.Padding.Top +
				pnBorders.Padding.Bottom +
				Padding.Top +
				Padding.Bottom
				+ (IsExpanded ? grFiles.Height : 0);
		}

		private void buttonXHeader_Click(object sender, EventArgs e)
		{
			IsExpanded = !IsExpanded;
		}
	}
}
