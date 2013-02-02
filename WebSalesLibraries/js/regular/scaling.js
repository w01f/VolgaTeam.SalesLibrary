(function ($)
{
	$.updateContentAreaDimensions = function ()
	{
		var height = $(window).height() - $('#ribbon').height() - 10;
		$('body').css({
			'height':'auto'
		});
		var content = $('#content');
		content.css({
			'height':height + 'px'
		});
		content.find('>div').css({
			'height':height + 'px'
		});
		updateSearchAreaDimensions();
		updateFavoritesDimensions();
	};

	var updateSearchAreaDimensions = function ()
	{
		var height = $('#content').height();
		$('#right-navbar').find('> div').css({
			'height':height + 'px'
		});
		$('#search-result').find('> div').css({
			'height':height + 'px'
		});
		$('#categories-container').css({
			'height':(height - 147) + 'px'
		});
		$('#libraries-container').css({
			'height':(height - 142) + 'px'
		});

		$('#file-types-container').css({
			'height':(height - 47) + 'px'
		});
		updateSearchGridDimensions();
	};

	var updateSearchGridDimensions = function ()
	{
		var searchResult = $('#search-result');

		var gridHeader = $('#links-grid-header');
		$('#links-grid-body-container').css({
			'height':(searchResult.find('> div').height() - ($('#search-grid-info').height() + 12) - gridHeader.height()) + 'px'
		});

		var linkDateWidth = 140;

		var linkNameHeaderWidth = searchResult.width() - gridHeader.find('td.details-button').width() - gridHeader.find('td.library-column').width() - gridHeader.find('td.link-type-column').width() - linkDateWidth;
		gridHeader.find('td.link-name-column').css({
			'width':linkNameHeaderWidth + 'px'
		});

		var gridBody = $('#links-grid-body');
		var linkNameBodyWidth = searchResult.width() - gridBody.find('td.details-button').width() - gridBody.find('td.library-column').width() - gridBody.find('td.link-type-column').width() - linkDateWidth;
		gridBody.find('td.link-name-column').css({
			'width':linkNameBodyWidth + 'px'
		});
	};

	var updateFavoritesDimensions = function ()
	{
		var height = $('#content').height();
		$('#favorites-panel-folders').find('> div').css({
			'height':(height - 3) + 'px'
		});

		var favoriteLinks = $('#favorites-panel-links');
		favoriteLinks.find('> div').css({
			'height':height + 'px'
		});
		var gridHeader = $('#links-grid-header');
		$('#links-grid-body-container').css({
			'height':(favoriteLinks.find('> div').height() - gridHeader.height()) + 'px'
		});

		var linkDateWidth = 140;

		var linkNameHeaderWidth = favoriteLinks.width() - gridHeader.find('td.details-button').width() - gridHeader.find('td.library-column').width() - gridHeader.find('td.link-type-column').width() - linkDateWidth;
		gridHeader.find('td.link-name-column').css({
			'width':linkNameHeaderWidth + 'px'
		});

		var gridBody = $('#links-grid-body');
		var linkNameBodyWidth = favoriteLinks.width() - gridBody.find('td.details-button').width() - gridBody.find('td.library-column').width() - gridBody.find('td.link-type-column').width() - linkDateWidth;
		gridBody.find('td.link-name-column').css({
			'width':linkNameBodyWidth + 'px'
		});
	};
})(jQuery);