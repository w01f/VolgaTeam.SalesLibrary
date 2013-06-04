using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.QBuilderControls
{
	public partial class HtmlEditorControl : UserControl
	{
		public HtmlEditorControl()
		{
			InitializeComponent();
		}

		public string SimpleText
		{
			get { return richEditControl.Text; }
		}

		public string HtmlText
		{
			get { return richEditControl.HtmlText; }
			set { richEditControl.HtmlText = value; }
		}
	}
}
