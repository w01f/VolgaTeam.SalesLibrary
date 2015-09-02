(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsLibraryWindow = function ()
	{
		var libraryWindowData = undefined;

		this.init = function (data)
		{
			libraryWindowData = data;
			var pageContent = $.SalesPortal.Content.getContentObject();
			$.SalesPortal.Content.fillContent(libraryWindowData.content,
				{
					title: libraryWindowData.options.headerTitle,
					icon: libraryWindowData.options.headerIcon
				},
				libraryWindowData.actions);
			switch (libraryWindowData.options.windowViewType)
			{
				case 'columns':
					$.SalesPortal.Wallbin.assignLinkEvents(pageContent);
					break;
				case 'accordion':
					$.SalesPortal.Wallbin.assignAccordionEvents(pageContent);
					break;
			}
		};
	};
})(jQuery);
