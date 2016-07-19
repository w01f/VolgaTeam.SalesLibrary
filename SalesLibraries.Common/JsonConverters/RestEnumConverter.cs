using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SalesLibraries.Common.JsonConverters
{
	class RestEnumConverter : StringEnumConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteValue((Int32)value);
		}
	}
}
