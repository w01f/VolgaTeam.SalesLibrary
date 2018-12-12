using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	public partial class InternalLinkEditControl : UserControl, IHyperLinkEditComplexControl
	{
		private readonly Dictionary<InternalLinkType, IInternalLinkEditControl> _editors = new Dictionary<InternalLinkType, IInternalLinkEditControl>();

		public InternalLinkType SelectedEditorType { get; private set; }
		public IInternalLinkEditControl SelectedEditor { get; private set; }

		public InternalLinkEditControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public void InitControl()
		{
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
			var linkInfo = (InternalLinkInfo)PrepareHyperLinkInfo();
			if (String.IsNullOrEmpty(linkInfo.Name))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the link name before saving");
				return false;
			}
			var result = SelectedEditor?.ValidateLinkInfo();
			return result.HasValue && result.Value;
		}

		public BaseNetworkLinkInfo PrepareHyperLinkInfo()
		{
			var linkInfo = SelectedEditor?.PrepareHyperLinkInfo();
			if (linkInfo != null)
			{
				linkInfo.Name = textEditLinkName.EditValue as String;
				linkInfo.FormatAsBluelink = checkEditBlueHyperlink.Checked;
				linkInfo.FormatBold = checkEditBold.Checked;
			}
			return linkInfo;
		}

		public BaseNetworkLinkInfo GetFinalHyperLinkInfo()
		{
			var linkInfo = SelectedEditor?.GetFinalHyperLinkInfo();
			if (linkInfo != null)
			{
				linkInfo.Name = textEditLinkName.EditValue as String;
				linkInfo.FormatAsBluelink = checkEditBlueHyperlink.Checked;
				linkInfo.FormatBold = checkEditBold.Checked;
			}
			return linkInfo;
		}
		
		public void ApplyDataFromTemplate(BaseNetworkLinkInfo templateInfo)
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
			var templateSettings = SelectedEditor?.PrepareHyperLinkInfo();
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
				_editors.Add(SelectedEditorType, SelectedEditor);}
			else
				SelectedEditor = _editors[SelectedEditorType];
			var control = (Control)SelectedEditor;
			if (!pnPropertyEditorsContainer.Controls.Contains(control))
				pnPropertyEditorsContainer.Controls.Add(control);
			control.BringToFront();

			SelectedEditor.InitControl();
			SelectedEditor.ApplySharedSettings(templateSettings);
		}
	}
}
