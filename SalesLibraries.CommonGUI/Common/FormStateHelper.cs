using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Objects.RemoteStorage;

namespace SalesLibraries.CommonGUI.Common
{
	public class FormStateHelper
	{
		private const string StorageName = "Location.xml";

		private static readonly Dictionary<string, FormState> SessionStates = new Dictionary<string, FormState>();

		private readonly Form _form;
		private readonly bool _showMaximized;
		private readonly string _formKey;
		private readonly StorageFile _stateStorageFile;
		private readonly bool _saveSate;

		private FormStateHelper(Form targetForm, StorageDirectory storagePath, string filePrefix, bool showMaximized, bool saveSate)
		{
			_form = targetForm;
			_formKey = filePrefix;
			_stateStorageFile = new StorageFile(storagePath.RelativePathParts.Merge(String.Format("{0}-{1}", filePrefix, StorageName)));
			_showMaximized = showMaximized;
			_saveSate = saveSate;
			_form.WindowState = FormWindowState.Normal;
			_form.Load += (o, e) => LoadState();
			_form.FormClosed += (o, e) => SaveState();
		}

		public static FormStateHelper Init(Form targetForm, StorageDirectory storagePath, string filePrefix, bool showMaximized, bool saveSate)
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
				state = FormState.LoadFromFile(_stateStorageFile.LocalPath);
				SessionStates.Add(_formKey, state);
			}

			_form.StartPosition = FormStartPosition.CenterScreen;
			if (state.WindowState == FormWindowState.Maximized)
				_form.WindowState = FormWindowState.Maximized;
			else
			{
				_form.WindowState = _showMaximized ? FormWindowState.Maximized : FormWindowState.Normal;
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
				}
			}
		}

		private void SaveState()
		{
			SessionStates[_formKey].WindowState = _form.WindowState;
			SessionStates[_formKey].Left = _form.WindowState == FormWindowState.Normal ? _form.Location.X : (int?)null;
			SessionStates[_formKey].Top = _form.WindowState == FormWindowState.Normal ? _form.Location.Y : (int?)null;
			SessionStates[_formKey].Width = _form.WindowState == FormWindowState.Normal ? _form.Width : (int?)null;
			SessionStates[_formKey].Height = _form.WindowState == FormWindowState.Normal ? _form.Height : (int?)null;
			if (_saveSate)
				SessionStates[_formKey].SaveToFile(_stateStorageFile.LocalPath);
		}

		internal class FormState
		{
			public int? Left { get; set; }
			public int? Top { get; set; }
			public int? Width { get; set; }
			public int? Height { get; set; }
			public FormWindowState? WindowState { get; set; }

			public bool Configured => Left.HasValue && Top.HasValue && Width.HasValue && Height.HasValue;

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
						int temp;
						if (Int32.TryParse(node.InnerText, out temp))
							state.Left = temp;
					}
					node = document.SelectSingleNode(@"/Location/Y");
					if (node != null)
					{
						int temp;
						if (Int32.TryParse(node.InnerText, out temp))
							state.Top = temp;
					}
					node = document.SelectSingleNode(@"/Location/Width");
					if (node != null)
					{
						int temp;
						if (Int32.TryParse(node.InnerText, out temp))
							state.Width = temp;
					}
					node = document.SelectSingleNode(@"/Location/Height");
					if (node != null)
					{
						int temp;
						if (Int32.TryParse(node.InnerText, out temp))
							state.Height = temp;
					}
					node = document.SelectSingleNode(@"/Location/State");
					if (node != null)
					{
						int temp;
						if (Int32.TryParse(node.InnerText, out temp))
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
				if (WindowState != FormWindowState.Maximized)
				{
					xml.AppendLine(@"<X>" + Left + @"</X>");
					xml.AppendLine(@"<Y>" + Top + @"</Y>");
					xml.AppendLine(@"<Width>" + Width + @"</Width>");
					xml.AppendLine(@"<Height>" + Height + @"</Height>");
				}
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
