(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsDownload = function ()
	{
		var downloadData = undefined;

		this.init = function (data)
		{
			downloadData = data;

			var content = $(downloadData.content);
			content.find('#accept-button').off('click').on('click', function ()
			{
				$.fancybox.close();
				window.open(downloadData.options.url.replace(/&amp;/g, '%26'), "_self");
			});
			$.fancybox({
				content: content,
				title: 'Download',
				width: 300,
				autoSize: false,
				autoHeight: true,
				openEffect: 'none',
				closeEffect: 'none'
			});
		};
	};
})(jQuery);
