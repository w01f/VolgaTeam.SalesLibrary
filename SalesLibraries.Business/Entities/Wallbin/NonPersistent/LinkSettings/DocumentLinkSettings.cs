﻿namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class DocumentLinkSettings : LibraryFileLinkSettings
	{
		private bool _generatePreviewImages;
		public bool GeneratePreviewImages
		{
			get { return _generatePreviewImages; }
			set
			{
				if (_generatePreviewImages != value)
					OnSettingsChanged();
				_generatePreviewImages = value;
			}
		}

		private bool _generateContentText;
		public bool GenerateContentText
		{
			get { return _generateContentText; }
			set
			{
				if (_generateContentText != value)
					OnSettingsChanged();
				_generateContentText = value;
			}
		}

		private bool _forcePreview;
		public bool ForcePreview
		{
			get { return _forcePreview; }
			set
			{
				if (_forcePreview != value)
					OnSettingsChanged();
				_forcePreview = value;
			}
		}

		public DocumentLinkSettings()
		{
			_generatePreviewImages = true;
			_generateContentText = true;
		}
	}
}
