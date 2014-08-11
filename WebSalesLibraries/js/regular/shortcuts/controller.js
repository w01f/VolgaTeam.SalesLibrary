(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var LayoutManager = function ()
	{
		var that = this;
		this.init = function ()
		{
			var content = $('#content');
			var objectId = content.find('.object-id').html();
			var isPage = content.find('.is-page').length > 0;
			$('#ribbon').ribbon();
			$('a#view-dialog-link').fancybox();
			$.SalesPortal.Overlay.show(true);
			$.SalesPortal.Shortcuts.processSearchLink(content, objectId, isPage);
			$.SalesPortal.Overlay.hide();
			that.updateContentSize();
			$(window).on('resize', that.updateContentSize);
		};

		this.updateContentSize = function ()
		{
			var ribbon = $('#ribbon');
			var height = $(window).height() - ribbon.height() - ribbon.offset().top - 10;
			$('body').css({
				'height': 'auto'
			});
			var content = $('#content');
			content.css({
				'height': height + 'px'
			});
			height = height - content.find('.shortcuts-home-bar').height();
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
				gridHeader.find('td.link-rate-column').width() -
				linkDateWidth;
			gridHeader.find('td.link-name-column').css({
				'width': linkNameHeaderWidth + 'px'
			});

			var gridBody = searchResult.find('.links-grid-body');
			var linkNameBodyWidth = searchResult.width() -
				gridBody.find('td.library-column').width() -
				gridBody.find('td.link-type-column').width() -
				gridBody.find('td.link-tag-column').width() -
				gridBody.find('td.link-rate-column').width() -
				linkDateWidth;
			gridBody.find('td.link-name-column').css({
				'width': linkNameBodyWidth + 'px'
			});
		};
	};
	$.SalesPortal.Layout = new LayoutManager();
	$(document).ready(function ()
	{
		$.SalesPortal.Layout.init();
	});
})(jQuery);