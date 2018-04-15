using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Common.JsonConverters;

namespace SalesLibraries.Business.Entities.Common
{
	public abstract class SettingsContainer
	{
		[JsonIgnore]
		protected IChangable Parent { get; set; }

		[JsonIgnore]
		protected bool AllowToHandleChanges { get; set; }

		public static TSettings CreateInstance<TSettings>(IChangable parent, string encodedSource = "") where TSettings : SettingsContainer
		{
			var createNew = String.IsNullOrEmpty(encodedSource);
			var deserializerSettings = new DefaultSerializeSettings();
			var settings = !createNew ?
				JsonConvert.DeserializeObject<TSettings>(encodedSource, deserializerSettings) :
				Activator.CreateInstance<TSettings>();
			settings.Parent = parent;
			if (createNew)
				settings.AfterConstruction();
			settings.AfterCreate();
			return settings;
		}

		protected virtual void AfterConstruction() { }

		protected virtual void AfterCreate()
		{
			AllowToHandleChanges = true;
		}

		public void OnSettingsChanged()
		{
			if (AllowToHandleChanges)
				Parent?.MarkAsModified();
		}

		public string Serialize()
		{
			var serializerSettings = new DefaultSerializeSettings();
			return JsonConvert.SerializeObject(this, serializerSettings);
		}

		public TSettings Clone<TSettings>(IChangable parent) where TSettings : SettingsContainer
		{
			return CreateInstance<TSettings>(parent, Serialize());
		}

		[OnDeserialized]
		public void AfterDeserialize(StreamingContext context)
		{
			AllowToHandleChanges = true;
		}

		[OnDeserializing]
		public void BeforeDeserialize(StreamingContext context)
		{
			AllowToHandleChanges = false;
		}
	}
}
