(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsLibraryPage = function ()
	{
		var libraryPageData = undefined;

		this.init = function (data)
		{
			libraryPageData = data;
			var pageContent = $.SalesPortal.Content.getContentObject();
			$.SalesPortal.Content.fillContent(
				libraryPageData.content,
				{
					title: libraryPageData.options.headerTitle,
					icon: libraryPageData.options.headerIcon
				},
				libraryPageData.actions,
				function ()
				{
					switch (libraryPageData.options.pageViewType)
					{
						case 'columns':
							$.SalesPortal.Wallbin.assignLinkEvents(pageContent);
							break;
						case 'accordion':
							$.SalesPortal.Wallbin.assignAccordionEvents(pageContent);
							break;
					}
					updateContentSize();
				}
			);

			$(window).off('resize.library-page').on('resize.library-page', updateContentSize);
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.ShortcutsManager.updateContentSize();
			$.SalesPortal.Wallbin.updateContentSize();
		};
	};
})(jQuery);
