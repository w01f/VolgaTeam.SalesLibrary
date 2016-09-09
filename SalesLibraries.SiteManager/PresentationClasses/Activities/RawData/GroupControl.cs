using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraTab;
using SalesLibraries.ServiceConnector.StatisticService;
using SalesLibraries.SiteManager.PresentationClasses.Common;
using BorderSide = DevExpress.XtraPrinting.BorderSide;

namespace SalesLibraries.SiteManager.PresentationClasses.Activities.RawData
{
	[ToolboxItem(false)]
	//public partial class GroupControl : UserControl, IGroupControl
	public partial class GroupControl : XtraTabPage, IGroupControl
	{
		private readonly DateTime _startDate;
		private readonly DateTime _endDate;
		public List<UserActivity> Records { get; private set; }

		private string _groupName;
		public string GroupName
		{
			get { return _groupName; }
			set
			{
				_groupName = String.IsNullOrEmpty(value) ? "No Group" : value;
				Text = _groupName;
			}
		}

		public GroupControl(IEnumerable<UserActivity> records, DateTime startDate, DateTime endDate)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			Records = new List<UserActivity>();
			Records.AddRange(records);

			_startDate = startDate;
			_endDate = endDate;

			gridControlData.DataSource = Records;
		}

		public PrintableComponentLink GetPrintLink()
		{
			gridViewData.CheckLoaded();
			var printLink = new PrintableComponentLink()
			{
				Landscape = true,
				PaperKind = PaperKind.A4,
				Component = gridControlData
			};
			printLink.CreateReportHeaderArea += OnCreateReportHeaderArea;
			return printLink;
		}

		public void ApplyColumns(Filter filter)
		{
			gridColumnType.Visible = filter.ShowActionGroup;
			gridColumnSubType.Visible = filter.ShowAction;
			gridColumnFile.Visible = filter.ShowFile;
			gridViewData.OptionsView.ShowPreview = filter.ShowDetails;
		}

		private void OnCreateReportHeaderArea(object sender, CreateAreaEventArgs e)
		{
			var reportHeader = string.Format("Activities: {0} - {1}", _startDate.ToString("MM/dd/yy"), _endDate.AddDays(-1).ToString("MM/dd/yy"));
			e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
			e.Graph.Font = new Font("Arial", 12, FontStyle.Bold);
			var rec = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
			e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
		}

		private void OnCustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
		{
			if (e.Column != gridColumnFile) return;
			var dataRow = gridViewData.GetRow(e.RowHandle) as UserActivity;
			if (dataRow != null && dataRow.IsUrl)
				e.RepositoryItem = repositoryItemHyperLinkEdit;
			else
				e.RepositoryItem = repositoryItemButtonEdit;
		}

		private void OnGridViewShownEditor(object sender, EventArgs e)
		{
			var view = (GridView)sender;
			view.ActiveEditor.MouseWheel -= OnActiveEditorMouseWheel;
			view.ActiveEditor.MouseWheel += OnActiveEditorMouseWheel;
		}

		private void OnActiveEditorMouseWheel(Object sender, MouseEventArgs e)
		{
			gridViewData.HideEditor();
			gridViewData.Focus();
		}

		private void OnGridViewMouseMove(object sender, MouseEventArgs e)
		{
			gridViewData.HideEditor();
			gridViewData.Focus();
		}

		private void OnOpenFileLink(object sender, OpenLinkEventArgs e)
		{
			e.EditValue = (gridViewData.GetFocusedRow() as UserActivity)?.FileLink;
		}
	}
}