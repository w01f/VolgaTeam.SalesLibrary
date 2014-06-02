using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SalesDepot.ToolClasses
{
	public class FormStateHelper
	{
		private const string StorageName = "Location.xml";
		private readonly string _storagePath;
		private readonly Form _form;
		private readonly bool _showMaximized;

		private FormStateHelper(Form targetForm, string storagePath, bool showMaximized)
		{
			_form = targetForm;
			_storagePath = storagePath;
			_showMaximized = showMaximized;
			_form.WindowState = FormWindowState.Normal;
			_form.Load += (o, e) => LoadState();
			_form.FormClosed += (o, e) => SaveState();
		}

		public static void Init(Form targetForm, string storagePath, bool showMaximized)
		{
			new FormStateHelper(targetForm, storagePath, showMaximized);
		}

		private void LoadState()
		{
			var filePath = Path.Combine(_storagePath, StorageName);
			int? x = null, y = null;
			if (File.Exists(filePath))
			{
				var document = new XmlDocument();
				document.Load(filePath);

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
			}
			if (x.HasValue && y.HasValue)
			{
				_form.StartPosition = FormStartPosition.Manual;
				_form.Location = new Point(x.Value, y.Value);
			}
			if (_showMaximized)
				_form.WindowState = FormWindowState.Maximized;
		}

		private void SaveState()
		{
			var xml = new StringBuilder();
			xml.AppendLine(@"<Location>");
			xml.AppendLine(@"<X>" + _form.Location.X + @"</X>");
			xml.AppendLine(@"<Y>" + _form.Location.Y + @"</Y>");
			xml.AppendLine(@"</Location>");
			var filePath = Path.Combine(_storagePath, StorageName);
			if (!Directory.Exists(_storagePath))
				Directory.CreateDirectory(_storagePath);
			using (var sw = new StreamWriter(filePath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}
	}
}
