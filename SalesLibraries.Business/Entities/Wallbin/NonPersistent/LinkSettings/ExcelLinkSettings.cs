using System;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class ExcelLinkSettings : LibraryFileLinkSettings, IEquatable<ExcelLinkSettings>
	{
		private bool _generateContentText;
		public bool GenerateContentText
		{
			get => _generateContentText;
			set
			{
				if (_generateContentText != value)
					OnSettingsChanged();
				_generateContentText = value;
			}
		}

		private bool _forceDownload;
		public bool ForceDownload
		{
			get => _forceDownload;
			set
			{
				if (_forceDownload != value)
					OnSettingsChanged();
				_forceDownload = value;
			}
		}

		private bool _forceOpen;
		public bool ForceOpen
		{
			get => _forceOpen;
			set
			{
				if (_forceOpen != value)
					OnSettingsChanged();
				_forceOpen = value;
			}
		}

		private bool _isArchiveResource;
		public bool IsArchiveResource
		{
			get => _isArchiveResource;
			set
			{
				if (_isArchiveResource != value)
					OnSettingsChanged();
				_isArchiveResource = value;
				if (_isArchiveResource)
				{
					GenerateContentText = false;
					ForceDownload = true;
				}
			}
		}

		protected override void AfterConstruction()
		{
			base.AfterConstruction();
			_generateContentText = false;
		}

		public override void ResetToEmpty()
		{
			GenerateContentText = true;
			ForceDownload = false;
			ForceOpen = false;
			IsArchiveResource = false;
		}

		public bool Equals(ExcelLinkSettings other)
		{
			return other != null &&
				GenerateContentText == other.GenerateContentText &&
				ForceDownload == other.ForceDownload &&
				ForceOpen == other.ForceOpen &&
				IsArchiveResource == other.IsArchiveResource;
		}

		public override int GetHashCode()
		{
			return GenerateContentText.GetHashCode() ^
				ForceDownload.GetHashCode() ^
				ForceOpen.GetHashCode() ^
				IsArchiveResource.GetHashCode();
		}
	}
}
