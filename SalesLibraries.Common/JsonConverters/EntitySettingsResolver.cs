using System;
using System.Collections.Generic;
using System.Linq;
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

			if (!property.Writable)
			{
				var propertyInfo = member as PropertyInfo;
				if (propertyInfo != null)
				{
					var hasPrivateSetter = propertyInfo.GetSetMethod(true) != null;
					property.Writable = hasPrivateSetter;
				}
			}

			return property;
		}

		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			var props = base.CreateProperties(type, memberSerialization).ToList();
			var fileds = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(f => f.Name.StartsWith("_"))
				.Select(f => base.CreateProperty(f, memberSerialization))
				.ToList();
			fileds.ForEach(p => { p.Writable = true; p.Readable = true; });
			props.AddRange(fileds);
			return props;
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
