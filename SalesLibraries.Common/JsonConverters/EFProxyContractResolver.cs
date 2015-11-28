using System;
using Newtonsoft.Json.Serialization;

namespace SalesLibraries.Common.JsonConverters
{
	public class EFProxyContractResolver : DefaultContractResolver
	{
		public override JsonContract ResolveContract(Type type)
		{
			return base.ResolveContract(ExtractObjectTypeFromProxy(type));
		}

		public static Type ExtractObjectTypeFromProxy(Type objectType)
		{
			return objectType != null &&
					!String.IsNullOrEmpty(objectType.Namespace) &&
					objectType.Namespace.StartsWith("System.Data.Entity.DynamicProxies") ?
				objectType.BaseType :
				objectType;
		}
	}
}
