using System;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class DocumentLinkSettings : LibraryFileLinkSettings, IEquatable<DocumentLinkSettings>
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

		private bool _isArchiveResource;
		public bool IsArchiveResource
		{
			get { return _isArchiveResource; }
			set
			{
				if (_isArchiveResource != value)
					OnSettingsChanged();
				_isArchiveResource = value;
				if (_isArchiveResource)
				{
					GeneratePreviewImages = false;
					GenerateContentText = false;
					ForcePreview = true;
				}
			}
		}

		public DocumentLinkSettings()
		{
			_generatePreviewImages = true;
			_generateContentText = true;
		}

		public override void ResetToEmpty()
		{
			GenerateContentText = true;
			GeneratePreviewImages = true;
			ForcePreview = false;
			IsArchiveResource = false;
		}

		public bool Equals(DocumentLinkSettings other)
		{
			return other != null &&
				GeneratePreviewImages == other.GeneratePreviewImages &&
				GenerateContentText == other.GenerateContentText &&
				ForcePreview == other.ForcePreview &&
				IsArchiveResource == other.IsArchiveResource;
		}

		public override int GetHashCode()
		{
			return GeneratePreviewImages.GetHashCode() ^
				GenerateContentText.GetHashCode() ^
				ForcePreview.GetHashCode() ^
				IsArchiveResource.GetHashCode();
		}
	}
}
