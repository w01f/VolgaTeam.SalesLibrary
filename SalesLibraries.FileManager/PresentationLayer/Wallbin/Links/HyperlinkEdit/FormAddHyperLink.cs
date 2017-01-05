using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	public partial class FormAddHyperLink : MetroForm
	{
		private readonly Dictionary<HyperLinkTypeEnum, IHyperLinkEditControl> _editors = new Dictionary<HyperLinkTypeEnum, IHyperLinkEditControl>();
		private readonly List<ButtonX> _editorSelectors;

		public HyperLinkTypeEnum SelectedEditorType { get; private set; }
		public IHyperLinkEditControl SelectedEditor { get; private set; }

		public FormAddHyperLink(BaseNetworkLinkInfo initialLinkInfo = null)
		{
			InitializeComponent();

			if (CreateGraphics().DpiX > 96)
			{
				buttonXUrl.Font = new Font(buttonXUrl.Font.FontFamily, buttonXUrl.Font.Size - 2, buttonXUrl.Font.Style);
				buttonXYouTube.Font = new Font(buttonXYouTube.Font.FontFamily, buttonXYouTube.Font.Size - 2, buttonXYouTube.Font.Style);
				buttonXVimeo.Font = new Font(buttonXVimeo.Font.FontFamily, buttonXVimeo.Font.Size - 2, buttonXVimeo.Font.Style);
				buttonXLan.Font = new Font(buttonXLan.Font.FontFamily, buttonXLan.Font.Size - 2, buttonXLan.Font.Style);
				buttonXQuickSite.Font = new Font(buttonXQuickSite.Font.FontFamily, buttonXQuickSite.Font.Size - 2, buttonXQuickSite.Font.Style);
				buttonXHtml5.Font = new Font(buttonXHtml5.Font.FontFamily, buttonXHtml5.Font.Size - 2, buttonXHtml5.Font.Style);
				buttonXApp.Font = new Font(buttonXApp.Font.FontFamily, buttonXApp.Font.Size - 2, buttonXApp.Font.Style);
				buttonXInternal.Font = new Font(buttonXInternal.Font.FontFamily, buttonXInternal.Font.Size - 2, buttonXInternal.Font.Style);
				buttonXSave.Font = new Font(buttonXSave.Font.FontFamily, buttonXSave.Font.Size - 2, buttonXSave.Font.Style);
				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
			}

			_editorSelectors = new List<ButtonX>(new[]
			{
				buttonXUrl,
				buttonXYouTube,
				buttonXLan,
				buttonXQuickSite,
				buttonXHtml5,
				buttonXApp,
				buttonXInternal,
				buttonXVimeo,
			});

			_editorSelectors.ForEach(button =>
			{
				button.Click += OnSelectEditorClick;
				button.CheckedChanged += OnSelectEditorChecked;
			});
			var defaultSelector = _editorSelectors.FirstOrDefault(e => initialLinkInfo != null && (HyperLinkTypeEnum)Enum.Parse(typeof(HyperLinkTypeEnum), e.Tag.ToString()) == initialLinkInfo.LinkType) ?? _editorSelectors.First();
			defaultSelector.Checked = true;

			(SelectedEditor as IHyperLinkEditComplexControl)?.InitControl();

			if (initialLinkInfo != null)
				SelectedEditor.ApplyDataFromTemplate(initialLinkInfo);
		}

		private void OnSelectEditorClick(Object sender, EventArgs e)
		{
			var selectedButton = (ButtonX)sender;
			if (selectedButton.Checked) return;
			_editorSelectors.ForEach(button => button.Checked = false);
			selectedButton.Checked = true;
		}

		private void OnSelectEditorChecked(Object sender, EventArgs e)
		{
			var selectedButton = (ButtonX)sender;
			if (!selectedButton.Checked) return;
			var templateSettings = SelectedEditor?.GetHyperLinkInfo();
			SelectedEditorType = (HyperLinkTypeEnum)Enum.Parse(typeof(HyperLinkTypeEnum), selectedButton.Tag.ToString());
			if (!_editors.ContainsKey(SelectedEditorType))
			{
				switch (SelectedEditorType)
				{
					case HyperLinkTypeEnum.Url:
						SelectedEditor = new UrlEditControl();
						break;
					case HyperLinkTypeEnum.YouTube:
						SelectedEditor = new YouTubeEditControl();
						break;
					case HyperLinkTypeEnum.Network:
						SelectedEditor = new LanLinkEditControl();
						break;
					case HyperLinkTypeEnum.QuickSite:
						SelectedEditor = new QuickSiteEditControl();
						break;
					case HyperLinkTypeEnum.App:
						SelectedEditor = new AppLinkEditControl();
						break;
					case HyperLinkTypeEnum.Internal:
						SelectedEditor = new InternalLinkEditControl();
						break;
					case HyperLinkTypeEnum.Html5:
						SelectedEditor = new Html5EditControl();
						break;
					case HyperLinkTypeEnum.Vimeo:
						SelectedEditor = new VimeoEditControl();
						break;
					default:
						throw new ArgumentOutOfRangeException("Link type is not found");
				}
				_editors.Add(SelectedEditorType, SelectedEditor);
			}
			else
				SelectedEditor = _editors[SelectedEditorType];
			var control = (Control)SelectedEditor;
			if (!pnEditContainer.Controls.Contains(control))
				pnEditContainer.Controls.Add(control);
			control.BringToFront();

			(SelectedEditor as IHyperLinkEditComplexControl)?.InitControl();

			SelectedEditor.ApplyDataFromTemplate(templateSettings);
		}

		private void AddLinkForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			e.Cancel = !SelectedEditor.ValidateLinkInfo();
		}
	}
}