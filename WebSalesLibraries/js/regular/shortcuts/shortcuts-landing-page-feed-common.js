(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.LandingPage = $.SalesPortal.LandingPage || {};
	$.SalesPortal.LandingPage.LinkFeedQuerySettings = function (data)
	{
		this.feedType = undefined;
		this.maxLinks = undefined;
		this.linkFormatsInclude = undefined;
		this.libraries = undefined;
		this.dateRangeType = undefined;
		this.text = undefined;
		this.textExactMatch = undefined;
		this.thumbnailSettings = undefined;
		this.categories = undefined;
		this.conditions = undefined;
		this.excludeQueryConditions = undefined;
		this.linkConditions = undefined;
		this.sortSettings = undefined;

		for (var property in data)
			if (data.hasOwnProperty(property))
				this[property] = data[property];
	};
})(jQuery);