(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};

	$(document).one('ready', function ()
	{
		var content = $.SalesPortal.Content.getContentObject();

		var serviceData = content.find('.service-data');

		if (serviceData.length > 0)
		{
			var objectId = serviceData.find('.object-id').html();

			var searchBarOptions = new $.SalesPortal.SearchOptions($.parseJSON(serviceData.find('.search-conditions .encoded-object').text()));
			var searchViewOptions = new $.SalesPortal.SearchResultsDataViewOptions($.parseJSON(serviceData.find('.search-view-options .encoded-object').text()));

			$.SalesPortal.MainMenu.init();
			$.SalesPortal.Content.init();
			$.SalesPortal.HistoryManager.init();
			$.SalesPortal.ScreenManager.init();

			if (searchBarOptions !== '' && searchViewOptions !== '')
			{
				var options = $('<div>' +
					'<div class="search-conditions" style="display: none;"><div class="encoded-object">' + $.toJSON(searchBarOptions) + '</div></div>' +
					'<div class="search-view-options" style="display: none;"><div class="encoded-object">' + $.toJSON(searchViewOptions) + '</div></div>' +
					'</div>');
				$.SalesPortal.ShortcutsSearchLink({
					optionsContainer: options,
					options: {
						linkId: objectId
					}
				}).runSearch();
			}
		}
	});
})(jQuery);
