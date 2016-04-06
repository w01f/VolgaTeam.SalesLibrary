﻿using System;
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

		public HyperLinkTypeEnum SelctedEditorType { get; private set; }
		public IHyperLinkEditControl SelectedEditor { get; private set; }

		public FormAddHyperLink()
		{
			InitializeComponent();

			if (CreateGraphics().DpiX > 96)
			{
				buttonXUrl.Font = new Font(buttonXUrl.Font.FontFamily, buttonXUrl.Font.Size - 2, buttonXUrl.Font.Style);
				buttonXYouTube.Font = new Font(buttonXYouTube.Font.FontFamily, buttonXYouTube.Font.Size - 2, buttonXYouTube.Font.Style);
				buttonXLan.Font = new Font(buttonXLan.Font.FontFamily, buttonXLan.Font.Size - 2, buttonXLan.Font.Style);
				buttonXSave.Font = new Font(buttonXSave.Font.FontFamily, buttonXSave.Font.Size - 2, buttonXSave.Font.Style);
				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
			}

			_editorSelectors = new List<ButtonX>(new[]
			{
				buttonXUrl,
				buttonXYouTube,
				buttonXLan
			});

			_editorSelectors.ForEach(button =>
			{
				button.Click += OnSelectEditorClick;
				button.CheckedChanged += OnSelectEditorChecked;
			});
			_editorSelectors.First().Checked = true;
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
			SelctedEditorType = (HyperLinkTypeEnum)Enum.Parse(typeof(HyperLinkTypeEnum), selectedButton.Tag.ToString());
			if (!_editors.ContainsKey(SelctedEditorType))
			{
				switch (SelctedEditorType)
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
					default:
						throw new ArgumentOutOfRangeException("Link type is not found");
				}
			}
			else
				SelectedEditor = _editors[SelctedEditorType];
			SelectedEditor.ApplySharedSettings(templateSettings);
			var control = (Control)SelectedEditor;
			if (!pnEditContainer.Controls.Contains(control))
				pnEditContainer.Controls.Add(control);
			control.BringToFront();
		}

		private void AddLinkForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			e.Cancel = !SelectedEditor.ValidateLinkInfo();
		}
	}
}