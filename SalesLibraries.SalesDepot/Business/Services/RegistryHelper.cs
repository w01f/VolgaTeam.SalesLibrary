using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace SalesLibraries.SalesDepot.Business.Services
{
	class RegistryHelper
	{
		public static IntPtr SalesDepotHandle
		{
			get
			{
				int result = 0;
				RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\NewBizWiz", RegistryKeyPermissionCheck.ReadSubTree);
				if (key != null)
				{
					object value = key.GetValue("SalesDepotHandle", false);
					if (value != null)
						int.TryParse(value.ToString(), out result);
				}
				return new IntPtr(result);
			}
			set
			{
				RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software", RegistryKeyPermissionCheck.ReadWriteSubTree).CreateSubKey("NewBizWiz", RegistryKeyPermissionCheck.ReadWriteSubTree);
				if (key != null)
					key.SetValue("SalesDepotHandle", value.ToInt32(), RegistryValueKind.DWord);
			}
		}

		public static bool MaximizeSalesDepot
		{
			get
			{
				bool result = false;
				RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\NewBizWiz", RegistryKeyPermissionCheck.ReadSubTree);
				if (key != null)
				{
					object value = key.GetValue("MaximizeSalesDepot", false);
					if (value != null)
					{
						int tempInt = 0;
						int.TryParse(value.ToString(), out tempInt);
						result = tempInt == 1;
					}
				}
				return result;
			}
			set
			{
				RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software", RegistryKeyPermissionCheck.ReadWriteSubTree).CreateSubKey("NewBizWiz", RegistryKeyPermissionCheck.ReadWriteSubTree);
				if (key != null)
					key.SetValue("MaximizeSalesDepot", value, RegistryValueKind.DWord);
			}
		}
	}
}
