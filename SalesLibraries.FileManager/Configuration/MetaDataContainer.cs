using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SalesLibraries.FileManager.Configuration
{
	class MetaDataContainer
	{
		public string DataTag { get; private set; }

		[JsonProperty(ItemConverterType = typeof(JavaScriptDateTimeConverter))]
		public DateTime? LastUpdate { get; private set; }

		public string Content { get; set; }

		public MetaDataContainer(string dataTag)
		{
			DataTag = dataTag;
		}

		public static MetaDataContainer Load(string dataTag)
		{
			var cacheFile = Path.Combine(RemoteResourceManager.Instance.MetaDataCacheFolder.LocalPath, String.Format("{0}.cache", dataTag));
			if (!File.Exists(cacheFile)) return null;
			return JsonConvert.DeserializeObject<MetaDataContainer>(File.ReadAllText(cacheFile));
		}

		public void Save()
		{
			LastUpdate = DateTime.Now;
			File.WriteAllText(Path.Combine(RemoteResourceManager.Instance.MetaDataCacheFolder.LocalPath, String.Format("{0}.cache", DataTag)), JsonConvert.SerializeObject(this));
		}

		public T GetData<T>() where T : class
		{
			return JsonConvert.DeserializeObject<T>(Content);
		}
	}
}
