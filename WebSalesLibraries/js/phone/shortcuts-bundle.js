(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsBundle = function (data)
	{
		var shortcutData = data;
		var contentContainer = $('#shortcut-link-page-' + shortcutData.options.linkId).find('.content-data');

		this.init = function ()
		{
			$.mobile.pageContainer.pagecontainer("change", "#shortcut-link-page-" + shortcutData.options.linkId, {
				transition: "slidefade"
			});

			if (contentContainer.find('ul').length > 0)
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
				$.SalesPortal.ShortcutsManager.trackActivity(data);

				var hasPageContent = data.find('.has-page-content').length > 0;
				var samePage = data.find('.same-page').length > 0;

				if (hasPageContent == true && samePage == true)
				{
					e.preventDefault();
					$.SalesPortal.ShortcutsManager.openShortcut(data, shortcutData.options.linkId);
				}
			});
		};
	};
})(jQuery);