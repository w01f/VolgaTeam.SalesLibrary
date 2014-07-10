(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var LayoutManager = function ()
	{
		this.init = function ()
		{
			$('#ribbon').ribbon({
				onTabChange: function (index, id, name)
				{
					$.cookie("selectedRibbonTabIndex", index, {
						expires: (60 * 60 * 24 * 7)
					});
					$.cookie("selectedRibbonTabId", id, {
						expires: (60 * 60 * 24 * 7)
					});
					$.ajax({
						type: "POST",
						url: window.BaseUrl + "statistic/writeActivity",
						data: {
							type: 'System',
							subType: 'Tab Changed',
							data: $.toJSON({
								Name: name
							})
						},
						async: true,
						dataType: 'html'
					});
					var minibar = $('.jx-bar, .jx-show');
					switch (id)
					{
						case 'home-tab':
							minibar.css({
								'height': '30px'
							});
							$.SalesPortal.Wallbin.init();
							break;
						case 'search-full-tab':
							minibar.css({
								'height': '0px'
							});
							$.SalesPortal.Search.init();
							break;
						case 'favorites-tab':
							minibar.css({
								'height': '0px'
							});
							$.SalesPortal.Favorites.init();
							break;
						case 'quiz-tab':
							minibar.css({
								'height': '0px'
							});
							$.SalesPortal.QuizManager.init();
							break;
						case 'qbuilder-tab':
							minibar.css({
								'height': '0px'
							});
							$.SalesPortal.QBuilder.Manager.init();
							break;
						default:
							minibar.css({
								'height': '0px'
							});
							if (id != null && id.indexOf("shortcuts-tab-") >= 0)
								$.SalesPortal.Shortcuts.init(id);
							else
								$.SalesPortal.Wallbin.init();
							break;
					}
				}
			});
			$('a#view-dialog-link').fancybox();
			$('.ribbon-button').tooltip({animation: false, trigger: 'hover', placement: 'bottom', delay: { show: 500, hide: 100 }});
			$('.logout-button').off('click').on('click', function ()
			{
				$.SalesPortal.Auth.logout();
			});
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
			if ($.SalesPortal.Ticker != undefined)
				$.SalesPortal.Ticker.updateContentSize();
		};
	};
	$.SalesPortal.Layout = new LayoutManager();
	$(document).ready(function ()
	{
		$.SalesPortal.Layout.init();
	});
})(jQuery);