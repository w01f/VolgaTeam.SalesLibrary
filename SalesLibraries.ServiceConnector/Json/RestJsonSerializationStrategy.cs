using RestSharp;

namespace SalesLibraries.ServiceConnector.Json
{
	class RestJsonSerializationStrategy : PocoJsonSerializerStrategy
	{
		protected override string MapClrMemberNameToJsonFieldName(string clrPropertyName)
		{
			return char.ToLower(clrPropertyName[0]) + clrPropertyName.Substring(1);
		}
	}
}
