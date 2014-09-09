﻿using System.Collections.Generic;
using System.Windows.Forms;

namespace OutlookSalesDepotAddIn.BusinessClasses
{
	public class DecoratorManager
	{
		private static DecoratorManager _instance;

		private DecoratorManager()
		{
			PackageViewers = new List<PackageDecorator>();
		}

		public List<PackageDecorator> PackageViewers { get; private set; }
		public PackageDecorator ActivePackageViewer { get; set; }

		public static DecoratorManager Instance
		{
			get
			{
				if (_instance == null)
					_instance = new DecoratorManager();
				return _instance;
			}
		}

		public void BuildPackageViewers()
		{
			PackageViewers.Clear();
			foreach (var package in LibraryManager.Instance.LibraryPackageCollection)
			{
				PackageViewers.Add(new PackageDecorator(package));
				Application.DoEvents();
			}
		}
	}
}