(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsBundle = function (data)
	{
		var shortcutData = data;
		var pageIdentifier = '#shortcut-link-page-' + shortcutData.options.linkId;
		var bundlePage = $(pageIdentifier);
		var contentContainer = bundlePage.find('.content-data');

		this.init = function ()
		{
			$(window).off("pagecontainerchange.bundle").on("pagecontainerchange.bundle", function (event, ui)
			{
				if ((ui.toPage !== undefined && ui.toPage.prop('id') === bundlePage.prop('id')) || ui.options.target === bundlePage.prop('id'))
				{
					if (contentContainer.find('.cbp-l-grid-masonry').length > 0)
					{
						try
						{
							contentContainer.cubeportfolio({
								gridAdjustment: 'alignCenter',
								caption: ''
							});
							initPageContent();
						}
						catch (err)
						{
							contentContainer.cubeportfolio('destroy', function ()
							{
								contentContainer.cubeportfolio({
									gridAdjustment: 'alignCenter',
									caption: ''
								});
								initPageContent();
							});
						}
					}
					else
					{
						initPageContent();
					}
				}
			});

			$.mobile.pageContainer.pagecontainer("change", "#shortcut-link-page-" + shortcutData.options.linkId, {
				transition: "slidefade"
			});
			$.mobile.loading('hide', {
				textVisible: false,
				html: ""
			});

			$('.logout-button').off('click').on('click', function (e)
			{
				e.stopPropagation();
				e.preventDefault();
				$.SalesPortal.Auth.logout();
			});
		};

		var initPageContent = function ()
		{
			contentContainer.find('.shortcuts-link').off('click').on('click', function (e)
			{
				var data = $(this).find('.service-data');
				var hasCustomHandler = data.find('.has-custom-handler').length > 0;
				var samePage = data.find('.same-page').length > 0;

				if (hasCustomHandler === true && samePage === true)
				{
					e.preventDefault();
					$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(data, "#shortcut-link-page-" + shortcutData.options.linkId);
				}
				else
					$.SalesPortal.ShortcutsManager.trackActivity(data);
			});
		};
	};
})(jQuery);