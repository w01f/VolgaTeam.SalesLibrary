window.salesDepot = window.salesDepot || { };

(function ($)
{
	$.updateContentAreaDimensions = function ()
	{
		var height = $(window).height() - $('#ribbon').height() - $('#ribbon').offset().top - 10;
		$('body').css({
			'height': 'auto'
		});
		var content = $('#content');
		content.css({
			'height': height + 'px'
		});
		updateWallbinTabs();
		updateSearchArea();
		updateFavoritesArea();
		updateTicker();
		updateShorcutsPage();
	};

	var updateWallbinTabs = function ()
	{
		var content = $('#content');
		var height = content.height();
		content.find('>div.wallbin-content').css({
			'height': height + 'px'
		});
		var tabPanelHeight = 0;
		var tabPanel = content.find('.ui-tabs-nav');
		if (tabPanel.length > 0)
		{
			tabPanelHeight = tabPanel.height();
			if (tabPanelHeight == 0)
				tabPanelHeight = 36;
		}
		$.each(content.find('.wallbin-container'), function ()
		{
			var pageHeaderHeight = 0;
			var pageHeader = $(this).find('.wallbin-header');
			if (pageHeader.length > 0)
				pageHeaderHeight = pageHeader.height() + 1;
			if (tabPanelHeight > 0)
				$(this).find('.wallbin-tab').css({
					'height': (height - pageHeaderHeight - tabPanelHeight - 5) + 'px'
				});
			else
				$(this).find('.wallbin-tab').css({
					'height': (height - pageHeaderHeight) + 'px'
				});
		});
	};

	var updateSearchArea = function ()
	{
		var height = $('#content').height();
		$('#right-navbar').find('> div').css({
			'height': height + 'px'
		});
		$('#categories-container').css({
			'height': (height - 198) + 'px'
		});
		$('#libraries-container').css({
			'height': (height - 142) + 'px'
		});

		$('#file-types-container').css({
			'height': (height - 47) + 'px'
		});
		updateLinksGrid();
	};

	var updateLinksGrid = function ()
	{
		var searchResult = $('#search-result');
		var height = $('#search-container').parent().height();
		searchResult.find('> div').css({
			'height': height + 'px'
		});
		var gridHeader = searchResult.find('.links-grid-header');
		var serchResultBar = searchResult.find('.search-grid-info');
		searchResult.find('.links-grid-body-container').css({
			'height': (searchResult.find('> div').height() - (serchResultBar.length > 0 ? (serchResultBar.height() + 12) : 0) - gridHeader.height()) + 'px'
		});

		var linkDateWidth = 100;

		var linkNameHeaderWidth = searchResult.width() - gridHeader.find('td.details-button').width() - gridHeader.find('td.library-column').width() - gridHeader.find('td.link-type-column').width() - gridHeader.find('td.link-rate-column').width() - gridHeader.find('td.link-tag-column').width() - linkDateWidth;
		gridHeader.find('td.link-name-column').css({
			'width': linkNameHeaderWidth + 'px'
		});

		var gridBody = searchResult.find('.links-grid-body');
		var linkNameBodyWidth = searchResult.width() - gridBody.find('td.details-button').width() - gridBody.find('td.library-column').width() - gridBody.find('td.link-type-column').width() - gridBody.find('td.link-rate-column').width() - gridBody.find('td.link-tag-column').width() - linkDateWidth;
		gridBody.find('td.link-name-column').css({
			'width': linkNameBodyWidth + 'px'
		});
	};

	var updateFavoritesArea = function ()
	{
		var height = $('#content').height();
		$('#favorites-panel-folders').find('> div').css({
			'height': (height - 3) + 'px'
		});

		var favoriteLinks = $('#favorites-panel-links');
		favoriteLinks.find('> div').css({
			'height': height + 'px'
		});
		var gridHeader = favoriteLinks.find('.links-grid-header');
		favoriteLinks.find('.links-grid-body-container').css({
			'height': (favoriteLinks.find('> div').height() - gridHeader.height()) + 'px'
		});

		var linkDateWidth = 140;

		var linkNameHeaderWidth = favoriteLinks.width() - gridHeader.find('td.details-button').width() - gridHeader.find('td.library-column').width() - gridHeader.find('td.link-type-column').width() - linkDateWidth;
		gridHeader.find('td.link-name-column').css({
			'width': linkNameHeaderWidth + 'px'
		});

		var gridBody = favoriteLinks.find('.links-grid-body');
		var linkNameBodyWidth = favoriteLinks.width() - gridBody.find('td.details-button').width() - gridBody.find('td.library-column').width() - gridBody.find('td.link-type-column').width() - linkDateWidth;
		gridBody.find('td.link-name-column').css({
			'width': linkNameBodyWidth + 'px'
		});
	};

	var updateTicker = function ()
	{
		var tickerWidth = $(window).width() - 5;
		var newsWidth = tickerWidth - $('.mt-label').width() - $('.mt-controls').width();
		$('.modern-ticker').css({
			'width': tickerWidth + 'px'
		});
		$('.mt-news').css({
			'width': newsWidth + 'px'
		});
	};

	var updateShorcutsPage = function ()
	{
		var content = $('#content');
		var height = content.height();
		content.find('.shortcuts-page-content').css({
			'height': (height - content.find('.shortcuts-home-bar img').height() - 20) + 'px'
		});
	}
})(jQuery);