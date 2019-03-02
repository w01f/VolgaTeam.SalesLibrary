(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsBundleModal = function ()
	{
		var bundleData = undefined;

		this.load = function (data)
		{
			bundleData = data;

			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/getShortcutBundleDialog",
				data: {
					shortcutId: bundleData.shortcutId
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show();
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (data)
				{
					$.fancybox({
						content: data.content,
						title: 'Bundle',
						width: $(window).width() * 0.8,
						height: $(window).height() * 0.8,
						autoSize: false,
						openEffect: 'none',
						closeEffect: 'none',
						helpers: {
							title: false
						},
						afterShow: function () {
							var innerContent = $('.fancybox-inner');

							innerContent.find('.tab-page-container .nav-tabs a[data-toggle="tab"]').on('shown.bs.tab', function (e)
							{
								let targetId = $(e.target).attr("href");
								innerContent.find('.tab-page-container .tab-logo-container img').prop('src',$(targetId).find('.service-data .tab-logo').text());
							});

							$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(innerContent, function () {
								$.fancybox.close();
							});

							innerContent.find('.tab-toggle-link').off('click').on('click', function (e) {
								e.preventDefault();
								let tabId = $(this).find('.service-data .tab-id').text();
								innerContent.find('.tab-page-container .nav-tabs a[href$="#' + tabId + '"]').tab('show');
							});

							updateSize();
						}
					});
				},
				error: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				async: true,
				dataType: 'json'
			});
		};

		let updateSize = function ()
		{
			$('.fancybox-skin').css({
				'padding': 0
			});

			var innerContent = $('.fancybox-inner');

			var modalHeight = innerContent.outerHeight(true);

			innerContent.find('.left-panel .items-container').css({
				'height': (modalHeight - 5) + 'px'
			});

			var tabPageHeight = modalHeight -
				innerContent.find('.tab-page-container .tab-logo-container').outerHeight(true)-
				innerContent.find('.tab-page-container .nav-tabs').outerHeight(true) - 5;

			innerContent.find('.tab-page-container .tab-pane').css({
				'height': tabPageHeight + 'px'
			});
		};
	};
})(jQuery);
