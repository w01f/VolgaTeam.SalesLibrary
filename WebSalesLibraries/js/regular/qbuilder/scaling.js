(function ($)
{
	$.updateContentAreaDimensions = function ()
	{
		var ribbon = $('#ribbon');
		var height = $(window).height() - ribbon.height() - 10;
		$('html').css({
			'height': $(window).height() + 'px'
		});
		$('body').css({
			'height': height + 'px'
		});
		var content = $('#content');
		content.css({
			'height': height + 'px'
		});
		updatePageList();
		updateLinkCart();
		updatePageContent();
	};

	var updatePageList = function ()
	{
		var height = $('#content').height() - $('#page-list-buttons').height() - 6;
		$('#page-list-container').css({
			'height': height + 'px'
		});
	};

	var updateLinkCart = function ()
	{
		var height = $('#content').height() - $('#link-cart-buttons').height();
		$('#link-cart-grid').css({
			'height': height + 'px'
		});
	};

	var updatePageContent = function ()
	{
		var content = $('#content');
		var pageContent = $('#page-content').children('div');
		var height = content.height();
		pageContent.css({
			'height': height + 'px'
		});
		updatePageLinks();
		updatePageLogos();
		$.pageContent.updateEditors();
	};

	var updatePageLinks = function ()
	{
		var content = $('#content');
		var height = content.height() - $('#page-content .page-title').height() - $('#page-content .page-url').height() - $('#page-content .ui-tabs-nav').height() - $('#page-content-tab-links .header').height() - 40;
		$('#page-content-links-container').css({
			'height': height + 'px'
		});
	};

	var updatePageLogos = function ()
	{
		var content = $('#content');
		var height = content.height() - $('#page-content .page-title').height() - $('#page-content .page-url').height() - $('#page-content .ui-tabs-nav').height() - 25;
		$('#page-content-tab-logo').find('.logo-list').css({
			'height': height + 'px'
		});
	};
})(jQuery);