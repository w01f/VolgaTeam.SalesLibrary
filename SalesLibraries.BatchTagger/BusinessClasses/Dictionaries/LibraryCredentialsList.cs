using System.Collections.Generic;
using Newtonsoft.Json;
using SalesLibraries.BatchTagger.Configuration;

namespace SalesLibraries.BatchTagger.BusinessClasses.Dictionaries
{
	public class LibraryCredentialsList
	{
		public List<LibraryCredentials> Items { get; private set; }

		public LibraryCredentialsList()
		{
			Items = new List<LibraryCredentials>();
		}

		public void Load()
		{
			var localMetaData = MetaDataContainer.Load(MetaDataConst.LibraryCredentialsDataTag);
			if (localMetaData == null) return;
			var localData = localMetaData.GetData<LibraryCredentialsList>();
			Items.AddRange(localData.Items);
		}

		public void Save()
		{
			var localMetaData = MetaDataContainer.Load(MetaDataConst.LibraryCredentialsDataTag) ??
				new MetaDataContainer(MetaDataConst.LibraryCredentialsDataTag);
			localMetaData.Content = JsonConvert.SerializeObject(this);
			localMetaData.Save();
		}
	}
}
