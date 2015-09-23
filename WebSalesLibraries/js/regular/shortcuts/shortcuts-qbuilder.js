(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsQBuilder = function ()
	{
		var qBuilderData = undefined;

		var servicePanel = undefined;

		this.init = function (data)
		{
			qBuilderData = data;

			$.SalesPortal.Content.fillContent(
				qBuilderData.content,
				{
					title: qBuilderData.options.headerTitle,
					icon: qBuilderData.options.headerIcon
				},
				qBuilderData.actions
			);

			servicePanel = $('#service-panel');

			if ($.cookie("showServicePanel") == "false")
				servicePanel.hide();
			else
				servicePanel.show();

			var headerContainer = servicePanel.find('.headers');
			headerContainer.find('.btn').off('click').on('click', function ()
			{
				headerContainer.find('.btn').removeClass('selected');
				$(this).addClass('selected');
				var relatedContentId = $(this).find('.service-data .tab-id').text();
				servicePanel.find('.service-panel-page').hide();
				$(relatedContentId).show();
				$.SalesPortal.QBuilder.PageList.updateContentSize();
				$.SalesPortal.QBuilder.LinkCart.updateContentSize();
			});

			$.SalesPortal.QBuilder.PageList.init();
			$.SalesPortal.QBuilder.LinkCart.init();

			initActionButtons();

			$(window).off('resize.qbuilder').on('resize.qbuilder', updateContentSize);
			updateContentSize();
		};

		var initActionButtons = function ()
		{
			var shortcutActionsContainer = $('#shortcut-action-container');

			if ($.cookie("showServicePanel") == "true")
			{
				shortcutActionsContainer.find('.qbuilder-panel-show').hide();
			}
			else
				shortcutActionsContainer.find('.qbuilder-panel-hide').hide();
			shortcutActionsContainer.find('.qbuilder-panel-show').off('click').on('click', function ()
			{
				servicePanel.show();
				shortcutActionsContainer.find('.qbuilder-panel-hide').show();
				$(this).hide();

				$.cookie("showServicePanel", true, {
					expires: (60 * 60 * 24 * 7)
				});

				updateContentSize();
			});
			shortcutActionsContainer.find('.qbuilder-panel-hide').off('click').on('click', function ()
			{
				servicePanel.hide();
				shortcutActionsContainer.find('.qbuilder-panel-show').show();
				$(this).hide();

				$.cookie("showServicePanel", false, {
					expires: (60 * 60 * 24 * 7)
				});

				updateContentSize();
			});

			shortcutActionsContainer.find('.qbuilder-qsite-add').off('click').on('click', function ()
			{
				$.SalesPortal.QBuilder.PageList.addPage();
			});

			shortcutActionsContainer.find('.qbuilder-qsite-delete').off('click').on('click', function ()
			{
				$.SalesPortal.QBuilder.PageList.deletePage();
			});

			shortcutActionsContainer.find('.qbuilder-qsite-save').off('click').on('click', function ()
			{
				$.SalesPortal.QBuilder.PageList.savePage(null);
			});

			shortcutActionsContainer.find('.qbuilder-qsite-preview')
				.prop('target', "_blank")
				.off('click').on('click', function ()
				{
					$.SalesPortal.QBuilder.PageList.savePage(function ()
					{
					});
				});

			shortcutActionsContainer.find('.qbuilder-qsite-email').off('click').on('click', function ()
			{
				$.SalesPortal.QBuilder.PageList.emailPage();
			});
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.ShortcutsManager.updateContentSize();
			$.SalesPortal.QBuilder.PageList.updateContentSize();
			$.SalesPortal.QBuilder.LinkCart.updateContentSize();
		};
	};
})(jQuery);
