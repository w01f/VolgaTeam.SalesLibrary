using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SalesLibraries.CommonGUI.Common
{
	public class FormStateHelper
	{
		private const string StorageName = "Location.xml";

		private static readonly Dictionary<string, FormState> SessionStates = new Dictionary<string, FormState>();

		private readonly Form _form;
		private readonly bool _showMaximized;
		private readonly string _formKey;
		private readonly string _stateStorageFilePath;
		private readonly bool _saveSate;

		private FormStateHelper(Form targetForm, string storagePath, string filePrefix, bool showMaximized, bool saveSate)
		{
			_form = targetForm;
			_formKey = filePrefix;
			_stateStorageFilePath = Path.Combine(storagePath, String.Format("{0}-{1}", filePrefix, StorageName));
			_showMaximized = showMaximized;
			_saveSate = saveSate;
			_form.Load += (o, e) => LoadState();
		}
	
		public static FormStateHelper Init(Form targetForm, string storagePath, string filePrefix, bool showMaximized, bool saveSate)
		{
			return new FormStateHelper(targetForm, storagePath, filePrefix, showMaximized, saveSate);
		}

		public void LoadState()
		{
			FormState state;
			if (SessionStates.ContainsKey(_formKey))
				state = SessionStates[_formKey];
			else
			{
				state = FormState.LoadFromFile(_stateStorageFilePath);
				SessionStates.Add(_formKey, state);
			}

			if (state.Configured)
			{
				_form.StartPosition = FormStartPosition.Manual;
				if (state.Left.HasValue && state.Top.HasValue)
					_form.Location = new Point(state.Left.Value, state.Top.Value);
				if (state.Width.HasValue && state.Height.HasValue)
				{
					_form.Width = state.Width.Value;
					_form.Height = state.Height.Value;
				}
				_form.WindowState = state.WindowState ??
					(_showMaximized ? FormWindowState.Maximized : FormWindowState.Normal);
			}
			else
			{
				_form.StartPosition = FormStartPosition.CenterScreen;
				_form.WindowState = _showMaximized ? FormWindowState.Maximized : FormWindowState.Normal;
			}


			_form.FormClosed += (o, e) => SaveState();
			_form.Resize += (o, e) => SaveState();
			_form.LocationChanged += (o, e) => SaveState();
		}

		private void SaveState()
		{
			if (_form.WindowState != FormWindowState.Minimized)
			{
				SessionStates[_formKey].Left = _form.Location.X;
				SessionStates[_formKey].Top = _form.Location.Y;
				SessionStates[_formKey].Width = _form.WindowState == FormWindowState.Normal ? _form.Width : (int?)null;
				SessionStates[_formKey].Height = _form.WindowState == FormWindowState.Normal ? _form.Height : (int?)null;
				SessionStates[_formKey].WindowState = _form.WindowState;
			}
			if (_saveSate)
				SessionStates[_formKey].SaveToFile(_stateStorageFilePath);
		}

		internal class FormState
		{
			public int? Left { get; set; }
			public int? Top { get; set; }
			public int? Width { get; set; }
			public int? Height { get; set; }
			public FormWindowState? WindowState { get; set; }

			public bool Configured => Left.HasValue && Top.HasValue;

			public static FormState LoadFromFile(string filePath)
			{
				var state = new FormState();
				if (File.Exists(filePath))
				{
					var document = new XmlDocument();
					document.Load(filePath);

					var node = document.SelectSingleNode(@"/Location/X");
					if (node != null)
					{
						if (Int32.TryParse(node.InnerText, out var temp))
							state.Left = temp;
					}
					node = document.SelectSingleNode(@"/Location/Y");
					if (node != null)
					{
						if (Int32.TryParse(node.InnerText, out var temp))
							state.Top = temp;
					}
					node = document.SelectSingleNode(@"/Location/Width");
					if (node != null)
					{
						if (Int32.TryParse(node.InnerText, out var temp))
							state.Width = temp;
					}
					node = document.SelectSingleNode(@"/Location/Height");
					if (node != null)
					{
						if (Int32.TryParse(node.InnerText, out var temp))
							state.Height = temp;
					}
					node = document.SelectSingleNode(@"/Location/State");
					if (node != null)
					{
						if (Int32.TryParse(node.InnerText, out var temp))
							state.WindowState = (FormWindowState)temp;
					}
				}
				return state;
			}

			public void SaveToFile(string filePath)
			{
				var xml = new StringBuilder();
				xml.AppendLine(@"<Location>");
				if (WindowState.HasValue)
					xml.AppendLine(@"<State>" + (Int32)WindowState + @"</State>");
				if (Left.HasValue)
					xml.AppendLine(@"<X>" + Left + @"</X>");
				if (Top.HasValue)
					xml.AppendLine(@"<Y>" + Top + @"</Y>");
				if (Width.HasValue)
					xml.AppendLine(@"<Width>" + Width + @"</Width>");
				if (Height.HasValue)
					xml.AppendLine(@"<Height>" + Height + @"</Height>");
				xml.AppendLine(@"</Location>");
				using (var sw = new StreamWriter(filePath, false))
				{
					sw.Write(xml);
					sw.Flush();
				}
			}
		}
	}
}
