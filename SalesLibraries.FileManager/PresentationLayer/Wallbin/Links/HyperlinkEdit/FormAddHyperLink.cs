using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.Common.Helpers;

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

			layoutControlItemSave.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSave.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSave.MinSize = RectangleHelper.ScaleSize(layoutControlItemSave.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSave.MinSize = RectangleHelper.ScaleSize(layoutControlItemSave.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
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