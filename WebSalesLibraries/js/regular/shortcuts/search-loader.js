(function ($)
{
	var updateLinksGridSize = function ()
	{
		var height = $('#content').height();
		var searchResult = $('#search-result');
		searchResult.find('> div').css({
			'height': height + 'px'
		});
		var gridHeader = $('#links-grid-header');
		$('#links-grid-body-container').css({
			'height': (searchResult.find('> div').height() - (searchResult.find('.search-grid-info').height() + 12) - gridHeader.height()) + 'px'
		});

		var linkDateWidth = 100;

		var linkNameHeaderWidth = searchResult.width() - gridHeader.find('td.details-button').width() - gridHeader.find('td.library-column').width() - gridHeader.find('td.link-type-column').width() - gridHeader.find('td.link-rate-column').width() - gridHeader.find('td.link-tag-column').width() - linkDateWidth;
		gridHeader.find('td.link-name-column').css({
			'width': linkNameHeaderWidth + 'px'
		});

		var gridBody = $('#links-grid-body');
		var linkNameBodyWidth = searchResult.width() - gridBody.find('td.details-button').width() - gridBody.find('td.library-column').width() - gridBody.find('td.link-type-column').width() - gridBody.find('td.link-rate-column').width() - gridBody.find('td.link-tag-column').width() - linkDateWidth;
		gridBody.find('td.link-name-column').css({
			'width': linkNameBodyWidth + 'px'
		});
	};

	$(document).ready(function ()
	{
		$.showOverlay();
		$.processSearchLink($('#content'));
	});
})(jQuery);