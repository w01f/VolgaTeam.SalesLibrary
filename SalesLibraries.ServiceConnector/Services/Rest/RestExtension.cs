﻿using Newtonsoft.Json;
using RestSharp;
using SalesLibraries.Common.JsonConverters;
using SalesLibraries.ServiceConnector.Models.Rest.Common;
using RestResponse = SalesLibraries.ServiceConnector.Models.Rest.Common.RestResponse;

namespace SalesLibraries.ServiceConnector.Services.Rest
{
	public static class RestExtension
	{
		public static string Encode(this IRequestData target)
		{
			var serializerSettings = new RestSerializeSettings();
			return JsonConvert.SerializeObject(target, serializerSettings);
		}

		public static RestResponse Decode(this IRestResponse target)
		{
			var serializerSettings = new RestSerializeSettings();
			return JsonConvert.DeserializeObject<RestResponse>(target.Content, serializerSettings);
		}

		public static TData GetData<TData>(this RestResponse target) where TData : class
		{
			var serializerSettings = new RestSerializeSettings();
			return JsonConvert.DeserializeObject<TData>(target.DataEncoded, serializerSettings);
		}
	}
}