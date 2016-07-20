using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DevExpress.XtraEditors;
using SalesLibraries.CommonGUI.Properties;

namespace SalesLibraries.CommonGUI.RetractableBar
{
	[Designer(typeof(RetractableBarDesigner))]
	public partial class RetractableBarControl : UserControl
	{
		private const int DefaultContentSize = 270;
		private const int DefaultAnimationDelay = 1500;

		public event EventHandler<StateChangedEventArgs> StateChanged;

		[Category("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Panel Content => pnContent;

		[Category("Header")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Panel Header => pnHeaderContent;

		[Browsable(true), DefaultValue(DefaultContentSize), Category("Appearance")]
		public int ContentSize { get; set; }

		[Browsable(true), DefaultValue(1500), Category("Appearance")]
		public int AnimationDelay { get; set; }

		protected RetractableBarControl()
		{
			InitializeComponent();
			ContentSize = DefaultContentSize;
			AnimationDelay = DefaultAnimationDelay;
			AddButtons(new[] { new ButtonInfo { Logo = Resources.RetractableBarLogo, Tooltip = "Expand bar" } });
		}

		public void AddButtons(IEnumerable<ButtonInfo> buttonInfos)
		{
			pnAdditionalButtons.Controls.Clear();
			var buttons = buttonInfos.Select(ButtonInfo.CreateButton).Reverse().ToList();
			var buttonHeight = (pnAdditionalButtons.Height - pnAdditionalButtons.Padding.Top - pnAdditionalButtons.Padding.Bottom) / buttons.Count;
			buttons.ForEach(b =>
			{
				b.Height = buttonHeight;
				b.Click += (o, e) => Expand();
			});
			pnAdditionalButtons.Controls.AddRange(buttons.ToArray());
		}

		public void Collapse(bool silent = false)
		{
			if (silent || AnimationDelay == 0)
			{
				Width = pnClosed.Width;
				pnOpened.Visible = false;
				pnClosed.Visible = true;
			}
			else
			{
				var timer = new Timer();
				timer.Interval = AnimationDelay > ContentSize ? AnimationDelay / ContentSize : 100;
				timer.Tick += (o, e) =>
				{
					if (Width > (pnClosed.Width + 50))
						Width -= 50;
					else
					{
						Width = pnClosed.Width;
						pnOpened.Visible = false;
						pnClosed.Visible = true;
						timer.Stop();
						timer.Dispose();
						timer = null;
					}
					Application.DoEvents();
				};
				timer.Start();
			}
			if (!silent)
				if (StateChanged != null)
					StateChanged(this, new StateChangedEventArgs(false));
		}

		public void Expand(bool silent = false)
		{
			if (silent || AnimationDelay == 0)
			{
				Width = ContentSize;
				pnOpened.Visible = true;
				pnClosed.Visible = false;
			}
			else
			{
				var timer = new Timer();
				timer.Interval = AnimationDelay / ContentSize;
				timer.Tick += (o, e) =>
				{
					if (Width < (ContentSize - 50))
						Width += 50;
					else
					{
						Width = ContentSize;
						pnOpened.Visible = true;
						pnClosed.Visible = false;
						timer.Stop();
						timer.Dispose();
						timer = null;
					}
					Application.DoEvents();
				};
				timer.Start();
			}
			if (!silent)
				if (StateChanged != null)
					StateChanged(this, new StateChangedEventArgs(true));
		}

		private void simpleButtonExpand_Click(object sender, EventArgs e)
		{
			Expand();
		}

		private void simpleButtonCollapse_Click(object sender, EventArgs e)
		{
			Collapse();
		}

		private void pnAdditionalButtons_Resize(object sender, EventArgs e)
		{
			var buttonHeight = (pnAdditionalButtons.Height - pnAdditionalButtons.Padding.Top - pnAdditionalButtons.Padding.Bottom) / pnAdditionalButtons.Controls.Count;
			foreach (var button in pnAdditionalButtons.Controls.OfType<Control>())
				button.Height = buttonHeight;
		}
	}

	class RetractableBarDesigner : ParentControlDesigner
	{
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			if (Control is RetractableBarControl)
			{
				EnableDesignMode(((RetractableBarControl)Control).Content, "Content");
				EnableDesignMode(((RetractableBarControl)Control).Header, "Header");
			}
		}
	}

	public class StateChangedEventArgs : EventArgs
	{
		public bool Expaned { get; private set; }

		public StateChangedEventArgs(bool expaned)
		{
			Expaned = expaned;
		}
	}

	public class ButtonInfo
	{
		public Image Logo { get; set; }
		public string Tooltip { get; set; }
		public Action Action { get; set; }

		public static SimpleButton CreateButton(ButtonInfo info)
		{
			var button = new SimpleButton();
			button.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			button.Dock = DockStyle.Top;
			button.Image = info.Logo;
			button.ImageLocation = ImageLocation.MiddleCenter;
			button.ToolTip = info.Tooltip;
			if (info.Action != null)
				button.Click += (o, e) => info.Action();
			return button;
		}
	}
}