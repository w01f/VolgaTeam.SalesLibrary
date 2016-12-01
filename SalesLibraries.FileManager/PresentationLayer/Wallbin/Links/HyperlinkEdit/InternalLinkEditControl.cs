using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	public partial class InternalLinkEditControl : UserControl, IHyperLinkEditControl
	{
		private readonly Dictionary<InternalLinkType, IInternalLinkEditControl> _editors = new Dictionary<InternalLinkType, IInternalLinkEditControl>();

		public InternalLinkType SelectedEditorType { get; private set; }
		public IInternalLinkEditControl SelectedEditor { get; private set; }

		public InternalLinkEditControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				laLinkName.Font = new Font(laLinkName.Font.FontFamily, laLinkName.Font.Size - 2, laLinkName.Font.Style);
				laLinkType.Font = new Font(laLinkType.Font.FontFamily, laLinkType.Font.Size - 2, laLinkType.Font.Style);
			}

			var editorSelectors = new List<CheckEdit>(new[]
			{
				checkEditLinkTypeWallbin,
				checkEditLinkTypeLibraryPage,
				checkEditLinkTypeLibraryFolder,
				checkEditLinkTypeLibraryObject,
				checkEditLinkTypeShortcut,
			});
			editorSelectors.ForEach(checkEdit =>
			{
				checkEdit.CheckedChanged += OnSelectEditorChecked;
			});
			editorSelectors.First().Checked = true;
		}

		public bool ValidateLinkInfo()
		{
			var linkInfo = (InternalLinkInfo)GetHyperLinkInfo();
			if (String.IsNullOrEmpty(linkInfo.Name))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the link name before saving");
				return false;
			}
			var result = SelectedEditor?.ValidateLinkInfo();
			return result.HasValue && result.Value;
		}

		public BaseNetworkLinkInfo GetHyperLinkInfo()
		{
			var linkInfo = SelectedEditor?.GetHyperLinkInfo();
			if (linkInfo != null)
			{
				linkInfo.Name = textEditLinkName.EditValue as String;
				linkInfo.FormatAsBluelink = checkEditBlueHyperlink.Checked;
				linkInfo.FormatBold = checkEditBold.Checked;
			}
			return linkInfo;
		}

		public void ApplySharedSettings(BaseNetworkLinkInfo templateInfo)
		{
			if (templateInfo != null)
			{
				textEditLinkName.EditValue = templateInfo.Name;
				checkEditBlueHyperlink.Checked = templateInfo.FormatAsBluelink;
				checkEditBold.Checked = templateInfo.FormatBold;
			}
		}

		private void OnSelectEditorChecked(object sender, EventArgs e)
		{
			var selectedToggle = (CheckEdit)sender;
			if (!selectedToggle.Checked) return;
			var templateSettings = SelectedEditor?.GetHyperLinkInfo();
			SelectedEditorType = (InternalLinkType)Enum.Parse(typeof(InternalLinkType), selectedToggle.Tag.ToString());
			if (!_editors.ContainsKey(SelectedEditorType))
			{
				switch (SelectedEditorType)
				{
					case InternalLinkType.Wallbin:
						SelectedEditor = new InternalWallbinLinkEditControl();
						break;
					case InternalLinkType.LibraryPage:
						SelectedEditor = new InternalLibraryPageLinkEditControl();
						break;
					case InternalLinkType.LibraryFolder:
						SelectedEditor = new InternalLibraryFolderLinkEditControl();
						break;
					case InternalLinkType.LibraryObject:
						SelectedEditor = new InternalLibraryObjectLinkEditControl();
						break;
					case InternalLinkType.Shortcut:
						SelectedEditor = new InternalShortcutLinkEditControl();
						break;
					default:
						throw new ArgumentOutOfRangeException("Link type is not found");
				}
			}
			else
				SelectedEditor = _editors[SelectedEditorType];
			SelectedEditor.ApplySharedSettings(templateSettings);
			var control = (Control)SelectedEditor;
			if (!pnPropertyEditorsContainer.Controls.Contains(control))
				pnPropertyEditorsContainer.Controls.Add(control);
			control.BringToFront();
		}
	}
}
