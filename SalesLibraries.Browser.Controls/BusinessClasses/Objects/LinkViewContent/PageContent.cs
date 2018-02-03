using System;
using System.IO;
using System.Linq;
using SalesLibraries.Browser.Controls.BusinessClasses.Interfaces;

namespace SalesLibraries.Browser.Controls.BusinessClasses.Objects.LinkViewContent
{
	abstract class PageContent : ViewContent, IPrintableContent
	{
		protected string[] _partFileUrls;
		protected int _currentPartIndex;

		public string PrintableFileUrl => OriginalFileUrl;
		public int? CurrentPage => _currentPartIndex;
		public int PartsCount=> _partFileUrls.Length;

		public override void Load(object[] data)
		{
			base.Load(data);
			_partFileUrls = (from object item in (object[])data[3] select item.ToString()).ToArray();
		}
		
		public void SwitchCurrentPart(int partIndex)
		{
			_currentPartIndex = partIndex;
		}

		public string GetPartFileUrl()
		{
			return _partFileUrls[_currentPartIndex];
		}

		public string GetPartFileName()
		{
			return String.Format("{0}{1}{2}", Path.GetFileNameWithoutExtension(OriginalFileName), (_currentPartIndex + 1), Path.GetExtension(OriginalFileName));
		}
	}
}
