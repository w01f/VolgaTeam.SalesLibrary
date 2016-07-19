using System;
using Newtonsoft.Json;

namespace SalesLibraries.Common.JsonConverters
{
	class GuidConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(Guid) == objectType;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			switch (reader.TokenType)
			{
				case JsonToken.Null:
					return Guid.Empty;
				case JsonToken.String:
					var str = reader.Value as string;
					return String.IsNullOrEmpty(str) ? Guid.Empty : new Guid(str);
				default:
					throw new ArgumentException("Invalid token type");
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (Guid.Empty.Equals(value))
				writer.WriteValue("");
			else
				writer.WriteValue(((Guid)value).ToString());
		}
	}
}
