using System;
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
		private readonly Form _form;
		private readonly bool _showMaximized;
		private readonly StorageFile _stateStorageFile;

		private FormStateHelper(Form targetForm, StorageDirectory storagePath, string filePrefix, bool showMaximized)
		{
			_form = targetForm;
			_stateStorageFile = new StorageFile(storagePath.RelativePathParts.Merge(String.Format("{0}-{1}", filePrefix, StorageName)));
			_showMaximized = showMaximized;
			_form.WindowState = FormWindowState.Normal;
			_form.Load += (o, e) => LoadState();
			_form.FormClosed += (o, e) => SaveState();
		}

		public static FormStateHelper Init(Form targetForm, StorageDirectory storagePath, string filePrefix, bool showMaximized)
		{
			return new FormStateHelper(targetForm, storagePath, filePrefix, showMaximized);
		}

		public void LoadState()
		{
			int? x = null, y = null, width = null, height = null;
			if (_stateStorageFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(_stateStorageFile.LocalPath);

				var node = document.SelectSingleNode(@"/Location/X");
				if (node != null)
				{
					int temp;
					if (Int32.TryParse(node.InnerText, out temp))
						x = temp;
				}
				node = document.SelectSingleNode(@"/Location/Y");
				if (node != null)
				{
					int temp;
					if (Int32.TryParse(node.InnerText, out temp))
						y = temp;
				}
				node = document.SelectSingleNode(@"/Location/Width");
				if (node != null)
				{
					int temp;
					if (Int32.TryParse(node.InnerText, out temp))
						width = temp;
				}
				node = document.SelectSingleNode(@"/Location/Height");
				if (node != null)
				{
					int temp;
					if (Int32.TryParse(node.InnerText, out temp))
						height = temp;
				}
			}
			if (x.HasValue && y.HasValue && Screen.AllScreens.Any(s => s.Bounds.Contains(x.Value, y.Value)))
			{
				_form.StartPosition = FormStartPosition.Manual;
				_form.Location = new Point(x.Value, y.Value);
				if (width.HasValue && height.HasValue)
				{
					_form.Width = width.Value;
					_form.Height = height.Value;
				}
			}
			else
			{
				_form.StartPosition = FormStartPosition.CenterScreen;
				if (_showMaximized)
					_form.WindowState = FormWindowState.Maximized;
			}
		}

		private void SaveState()
		{
			var xml = new StringBuilder();
			xml.AppendLine(@"<Location>");
			if (_form.WindowState != FormWindowState.Maximized)
			{
				xml.AppendLine(@"<X>" + _form.Location.X + @"</X>");
				xml.AppendLine(@"<Y>" + _form.Location.Y + @"</Y>");
				xml.AppendLine(@"<Width>" + _form.Width + @"</Width>");
				xml.AppendLine(@"<Height>" + _form.Height + @"</Height>");
			}
			xml.AppendLine(@"</Location>");
			using (var sw = new StreamWriter(_stateStorageFile.LocalPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}
	}
}
