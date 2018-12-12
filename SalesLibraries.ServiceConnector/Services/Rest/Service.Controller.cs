using System;
using RestSharp;
using SalesLibraries.ServiceConnector.Json;
using SalesLibraries.ServiceConnector.Models.Rest.Common;
using RestResponse = SalesLibraries.ServiceConnector.Models.Rest.Common.RestResponse;

namespace SalesLibraries.ServiceConnector.Services.Rest
{
	public partial class RestServiceConnection
	{
		public RestResponse DoRequest(IRequestData data)
		{
			var client = GetClient();

			var request = new RestRequest(String.Format("{0}/{1}", Controller, "{model}"), data.Method);
			SimpleJson.CurrentJsonSerializerStrategy = new RestJsonSerializationStrategy();

			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Accept", "*/*");
			request.RequestFormat = DataFormat.Json;

			request.AddUrlSegment("model", data.ModelName);

			if (data.Method == Method.POST)
				request.AddParameter("dataEncoded", data.Encode());
			else
				request.AddBody(new RequestBody { DataEncoded = data.Encode() });

			//request.AddParameter("XDEBUG_SESSION_START", "15636", ParameterType.QueryString);

			var response = client.Execute(request);
			return response.Decode();
		}

		public RestResponse DoRequest(IRequestData data, string exceptionText)
		{
			RestResponse response;
			try
			{
				response = DoRequest(data);
				if (response == null) throw new Exception();
			}
			catch (Exception ex)
			{
				throw new RestServiceException(exceptionText, ex);
			}
			if (response.Result == ResponseResult.Error)
			{
				var error = response.GetData<RestError>();
				throw new RestServiceException(error.Message);
			}
			return response;
		}
	}
}
