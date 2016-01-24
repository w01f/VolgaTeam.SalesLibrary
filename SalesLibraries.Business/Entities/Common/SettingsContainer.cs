using System;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Common.JsonConverters;

namespace SalesLibraries.Business.Entities.Common
{
	public abstract class SettingsContainer
	{
		protected IChangable Parent { get; set; }

		public static TSettings CreateInstance<TSettings>(IChangable parent, string encodedSource = "") where TSettings : SettingsContainer
		{
			var createNew = String.IsNullOrEmpty(encodedSource);
			var settings = !createNew ?
				JsonConvert.DeserializeObject<TSettings>(encodedSource, new ImageConverter()) :
				Activator.CreateInstance<TSettings>();
			settings.Parent = parent;
			if (createNew)
				settings.AfterConstruction();
			settings.AfterCreate();
			return settings;
		}

		protected virtual void AfterConstruction() { }

		protected virtual void AfterCreate() { }

		protected void OnSettingsChanged()
		{
			if (Parent != null)
				Parent.MarkAsModified();
		}

		public string Serialize()
		{
			return JsonConvert.SerializeObject(this,
				new ImageConverter());
		}

		public TSettings Clone<TSettings>(IChangable parent) where TSettings : SettingsContainer
		{
			return CreateInstance<TSettings>(parent, Serialize());
		}
	}
}
