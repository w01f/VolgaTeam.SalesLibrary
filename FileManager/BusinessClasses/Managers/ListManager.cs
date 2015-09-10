using System;
using System.Collections.Generic;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.BusinessClasses
{
	public class ListManager
	{
		private static readonly ListManager _instance = new ListManager();
		private ListManager() { }

		public WidgetList Widgets { get; private set; }
		public BannerList Banners { get; private set; }

		public SecurityGroups SecurityGroups { get; private set; }

		public CategoryList SearchTags { get; set; }
		public SuperFilterList SuperFilters { get; private set; }

		public static ListManager Instance
		{
			get { return _instance; }
		}

		public void Init()
		{
			Widgets = new WidgetList();
			Widgets.Load();

			Banners = new BannerList();
			Banners.Load();

			SecurityGroups = new SecurityGroups();
			SecurityGroups.Load();

			SearchTags = new CategoryList();
			SearchTags.Load();

			SuperFilters = new SuperFilterList();
			SuperFilters.Load();
		}
	}
}