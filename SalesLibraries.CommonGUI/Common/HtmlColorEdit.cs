using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CommonGUI.Common
{
	[UserRepositoryItem("RegisterHtmlColorEdit")]
	public class RepositoryItemHtmlColorEdit : RepositoryItemButtonEdit
	{
		public const string CustomEditName = "HtmlColorEdit";

		private Color _color = Color.White;
		public Color Color
		{
			get { return _color; }
			set
			{
				if (_color != value)
				{
					_color = value;
					OnPropertiesChanged();
				}
			}
		}

		public new HtmlColorEdit OwnerEdit => base.OwnerEdit as HtmlColorEdit;
		public HtmlColorEdit Editor { get; private set; }
		public event EventHandler<EventArgs> OnColorSelected;

		static RepositoryItemHtmlColorEdit()
		{
			RegisterHtmlColorEdit();
		}

		public RepositoryItemHtmlColorEdit()
		{
			ButtonClick += OnButtonClick;
			this.EnableSelectAll();
		}

		public override string EditorTypeName => CustomEditName;

		public static void RegisterHtmlColorEdit()
		{
			EditorRegistrationInfo.Default.Editors.Add(
				new EditorClassInfo(
					CustomEditName,
					typeof(HtmlColorEdit),
					typeof(RepositoryItemHtmlColorEdit),
					typeof(HtmlColorEditViewInfo),
					new HtmlColorEditPainter(), true));
		}

		public override BaseEditViewInfo CreateViewInfo()
		{
			return new HtmlColorEditViewInfo(this);
		}

		public override BaseEditPainter CreatePainter()
		{
			return new HtmlColorEditPainter();
		}

		public override BaseEdit CreateEditor()
		{
			Editor = (HtmlColorEdit)base.CreateEditor();
			return Editor;
		}

		protected override void Dispose(bool disposing)
		{
			Editor = null;
			base.Dispose(disposing);
		}

		public override void Assign(RepositoryItem item)
		{
			BeginUpdate();
			try
			{
				base.Assign(item);
				var source = item as RepositoryItemHtmlColorEdit;
				if (source == null) return;
				Color = source.Color;
			}
			finally
			{
				EndUpdate();
			}
		}

		public override string GetDisplayText(object editValue)
		{
			var editor = OwnerEdit ?? Editor;
			return editor?.Color.ToHex();
		}

		private void OnButtonClick(object sender, ButtonPressedEventArgs e)
		{
			var editor = OwnerEdit ?? Editor;
			if (editor == null) return;
			using (var dialog = new ColorDialog())
			{
				dialog.Color = (Color)editor.EditValue;
				if (dialog.ShowDialog(editor) != DialogResult.OK) return;
				editor.EditValue = dialog.Color;
				OnColorSelected?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	[ToolboxItem(true)]
	public class HtmlColorEdit : ButtonEdit
	{
		public static Color DisabledColor = Color.LightGray;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new RepositoryItemHtmlColorEdit Properties => base.Properties as RepositoryItemHtmlColorEdit;

		public override string EditorTypeName => RepositoryItemHtmlColorEdit.CustomEditName;

		public Color Color
		{
			get { return (Color)EditValue; }
			set { EditValue = value; }
		}

		public override object EditValue
		{
			get
			{
				return base.EditValue ?? Properties.Color;
			}
			set
			{
				var color = Properties.Color;
				if (value is String)
				{
					var hexColorValue = (String)value;
					if (hexColorValue.Length == 7)
						color = ParseHtmlColor(hexColorValue);
				}
				else if (value is Color)
				{
					color = (Color)value;
				}
				base.EditValue = color;
				Properties.Color = color;
				UpdateAppearance();
			}
		}

		static HtmlColorEdit()
		{
			RepositoryItemHtmlColorEdit.RegisterHtmlColorEdit();
		}

		public HtmlColorEdit()
		{
			CustomDisplayText += OnCustomDisplayText;
			EnabledChanged += OnEnabledChanged;
		}

		private void OnEnabledChanged(object sender, EventArgs e)
		{
			UpdateAppearance();
		}

		private void OnCustomDisplayText(Object sender, CustomDisplayTextEventArgs e)
		{
			e.DisplayText = ((Color)e.Value).ToHex();
		}

		private void UpdateAppearance()
		{
			if (Enabled)
			{
				BackColor = (Color)EditValue;
				ForeColor = BackColor.GetNegativeColor();
			}
			else
			{
				BackColor = DisabledColor;
				ForeColor = DisabledColor;
			}
		}

		private static Color ParseHtmlColor(string hexColorCode)
		{
			if (!String.IsNullOrEmpty(hexColorCode))
			{
				try
				{
					return ColorTranslator.FromHtml(hexColorCode);
				}
				catch
				{
				}
			}
			return Color.White;
		}
	}

	public class HtmlColorEditViewInfo : ButtonEditViewInfo
	{
		public string ColorHexValue { get; set; }
		public override String DisplayText => ColorHexValue;
		public HtmlColorEditViewInfo(RepositoryItem item) : base(item) { }
	}

	public class HtmlColorEditPainter : ButtonEditPainter
	{
		protected override void DrawContent(ControlGraphicsInfoArgs info)
		{
			var viewInfo = (HtmlColorEditViewInfo)info.ViewInfo;
			if ((viewInfo.OwnerEdit?.Enabled ?? true))
			{
				var color = (Color)viewInfo.EditValue;
				viewInfo.PaintAppearance.BackColor = color;
				viewInfo.PaintAppearance.ForeColor = color.GetNegativeColor();
				viewInfo.ColorHexValue = color.ToHex();
			}
			else
			{
				viewInfo.PaintAppearance.BackColor = HtmlColorEdit.DisabledColor;
				viewInfo.PaintAppearance.ForeColor = HtmlColorEdit.DisabledColor;
				viewInfo.ColorHexValue = Color.Black.ToHex();
			}
			base.DrawContent(info);
		}
	}
}
