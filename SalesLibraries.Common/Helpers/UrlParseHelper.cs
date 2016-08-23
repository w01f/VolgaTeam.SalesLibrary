﻿namespace SalesLibraries.Common.Helpers
{
	public class UrlParseHelper
	{
		public const string UrlParseRegExp = @"^((http[s]?|ftp):\/\/)?\/?([^\/\.]+\.)*?([^\/\.]+\.[^:\/\s\.]{2,3}(\.[^:\/\s\.]‌​{2,3})?(:\d+)?)($|\/)([^#?\s]+)?(.*?)?(#[\w\-]+)?$";
	}
}
