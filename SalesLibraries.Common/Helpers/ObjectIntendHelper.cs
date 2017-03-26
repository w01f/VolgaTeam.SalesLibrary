using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SalesLibraries.Common.Helpers
{
	public static class ObjectIntendHelper
	{
		private static readonly List<Assembly> LoadedAssemblies = new List<Assembly>();
		private static readonly Dictionary<string, IEnumerable<Type>> TypesDictionary = new Dictionary<string, IEnumerable<Type>>();

		private static IEnumerable<object> FindObjectsForType(Type baseType, Type intendedClass, IEnumerable<Type> assemblyTypes, object[] parameters)
		{
			var lKey = baseType.FullName + (intendedClass != null ? intendedClass.FullName : "Undefined");
			if (TypesDictionary.ContainsKey(lKey))
				return TypesDictionary[lKey].Select(t => Activator.CreateInstance(t, parameters));

			var targetTypes = new List<Type>();
			foreach (var type in assemblyTypes)
			{
				if (type.IsInterface || !(type == baseType || type.IsSubclassOf(baseType) || baseType.IsAssignableFrom(type))) continue;
				var attrs = type.GetCustomAttributes(typeof(IntendForClassAttribute), false);
				if (intendedClass == null)
				{
					targetTypes.Add(type);
					continue;
				}
				foreach (IntendForClassAttribute attr in attrs)
				{
					if (attr.BusinessObjectClass != intendedClass && !intendedClass.IsSubclassOf(attr.BusinessObjectClass)) continue;
					targetTypes.Add(type);
				}
			}
			if (targetTypes.Any())
				TypesDictionary.Add(lKey, targetTypes);
			return targetTypes.Select(t => parameters != null ? Activator.CreateInstance(t, parameters) : Activator.CreateInstance(t));
		}

		public static IEnumerable<object> GetObjectInstances(
			Type baseType,
			Type intendedClass,
			params object[] parameters)
		{
			if (!LoadedAssemblies.Any())
				LoadedAssemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies());
			var assemblyTypes = new List<Type>();
			foreach (var assembly in LoadedAssemblies)
			{
				assemblyTypes.Clear();
				try
				{
					assemblyTypes.AddRange(assembly.GetTypes());
				}
				catch { continue; }
				if (!assemblyTypes.Any()) continue;
				var targetObjects = FindObjectsForType(baseType, intendedClass, assemblyTypes, parameters).ToList();
				if (!targetObjects.Any()) continue;
				return targetObjects;
			}
			return new object[] { };
		}
	}

	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class IntendForClassAttribute : Attribute
	{
		public Type BusinessObjectClass { get; private set; }

		public IntendForClassAttribute(Type businessObjectClass)
		{
			BusinessObjectClass = businessObjectClass;
		}
	}
}
