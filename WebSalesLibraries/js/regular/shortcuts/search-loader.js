(function ($)
{
	var updateLinksGridSize = function ()
	{
		var height = $('#content').height();
		var searchResult = $('#search-result');
		searchResult.find('> div').css({
			'height': height + 'px'
		});
		var gridHeader = searchResult.find('.links-grid-header');
		var searchResultBar = searchResult.find('.search-grid-info');
		searchResult.find('.links-grid-body-container').css({
			'height': (height - (searchResultBar.length > 0 ? (searchResultBar.height() + 12) : 0) - gridHeader.height()) + 'px'
		});

		var linkDateWidth = 100;

		var linkNameHeaderWidth = searchResult.width() -
			gridHeader.find('td.library-column').width() -
			gridHeader.find('td.link-type-column').width() -
			gridHeader.find('td.link-tag-column').width() -
			linkDateWidth;
		gridHeader.find('td.link-name-column').css({
			'width': linkNameHeaderWidth + 'px'
		});

		var gridBody = searchResult.find('.links-grid-body');
		var linkNameBodyWidth = searchResult.width() -
			gridBody.find('td.library-column').width() -
			gridBody.find('td.link-type-column').width() -
			gridBody.find('td.link-tag-column').width() -
			linkDateWidth;
		gridBody.find('td.link-name-column').css({
			'width': linkNameBodyWidth + 'px'
		});
	};

	$(document).ready(function ()
	{
		$.SalesPortal.Shortcuts.processSearchLink($('#content'));
		$.SalesPortal.Overlay.hide();
	});
})(jQuery);