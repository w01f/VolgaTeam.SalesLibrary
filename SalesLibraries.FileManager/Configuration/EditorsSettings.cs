using System.Xml;

namespace SalesLibraries.FileManager.Configuration
{
	class EditorsSettings
	{
		public bool EnableTagsEdit { get; private set; }
		public bool EnableSecurityEdit { get; private set; }

		public EditorsSettings()
		{
			EnableTagsEdit = true;
			EnableSecurityEdit = true;
		}

		public void Load()
		{
			if (!RemoteResourceManager.Instance.TabSettingsFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(RemoteResourceManager.Instance.TabSettingsFile.LocalPath);

			bool tempBool;
			var node = document.SelectSingleNode(@"/ribbon/Tags");
			if (node != null)
				if (bool.TryParse(node.InnerText, out tempBool))
					EnableTagsEdit = tempBool;
			node = document.SelectSingleNode(@"/ribbon/Security");
			if (node != null)
				if (bool.TryParse(node.InnerText, out tempBool))
					EnableSecurityEdit = tempBool;			
		}
	}
}
