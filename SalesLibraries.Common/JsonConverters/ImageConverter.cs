﻿using System;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Common.JsonConverters
{
	public class ImageConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(Image) || objectType.IsSubclassOf(typeof(Image));
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return !String.IsNullOrEmpty(reader.Value as String) ? Image.FromStream(new MemoryStream(Convert.FromBase64String((String)reader.Value))) : null;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var bmp = value as Bitmap;
			if (bmp == null) return;
			var m = new MemoryStream();
			bmp.Save(m, Utils.GetImageFormat(bmp));
			writer.WriteValue(Convert.ToBase64String(m.ToArray()));
		}
	}
}
