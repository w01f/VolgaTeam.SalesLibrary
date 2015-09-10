using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FileManager.BusinessClasses
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
			var cacheFile = Path.Combine(SettingsManager.Instance.ApplicationLocalDataPath, "Cache", String.Format("{0}.cache", dataTag));
			if (!File.Exists(cacheFile)) return null;
			return JsonConvert.DeserializeObject<MetaDataContainer>(File.ReadAllText(cacheFile));
		}

		public void Save()
		{
			LastUpdate = DateTime.Now;
			var cacheFolder = Path.Combine(SettingsManager.Instance.ApplicationLocalDataPath, "Cache");
			if (!Directory.Exists(cacheFolder))
				Directory.CreateDirectory(cacheFolder);
			File.WriteAllText(Path.Combine(cacheFolder, String.Format("{0}.cache", DataTag)), JsonConvert.SerializeObject(this));
		}

		public T GetData<T>() where T : class
		{
			return JsonConvert.DeserializeObject<T>(Content);
		}
	}
}
