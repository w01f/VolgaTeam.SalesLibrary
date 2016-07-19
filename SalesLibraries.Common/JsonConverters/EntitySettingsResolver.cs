using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SalesLibraries.Common.JsonConverters
{
	public class EntitySettingsResolver : DefaultContractResolver
	{
		public override JsonContract ResolveContract(Type type)
		{
			return base.ResolveContract(ExtractObjectTypeFromProxy(type));
		}

		public static Type ExtractObjectTypeFromProxy(Type objectType)
		{
			return !String.IsNullOrEmpty(objectType?.Namespace) && objectType.Namespace.StartsWith("System.Data.Entity.DynamicProxies") ?
				objectType.BaseType :
				objectType;
		}

		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var property = base.CreateProperty(member, memberSerialization);
			property.ShouldSerialize = targetObject => ShouldSerialize(member);
			return property;
		}

		internal static bool ShouldSerialize(MemberInfo memberInfo)
		{
			var propertyInfo = memberInfo as PropertyInfo;
			if (propertyInfo == null)
				return false;

			if (propertyInfo.SetMethod != null)
				return true;

			var getMethod = propertyInfo.GetMethod;
			return Attribute.GetCustomAttribute(getMethod, typeof(CompilerGeneratedAttribute)) != null;
		}
	}
}
