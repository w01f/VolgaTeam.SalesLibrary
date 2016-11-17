(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsQBuilder = function ()
	{
		var qBuilderData = undefined;

		var servicePanel = undefined;

		this.init = function (data)
		{
			qBuilderData = data;
			qBuilderData.options.trackActivityDelegate = trackActivity;

			$.SalesPortal.Content.fillContent({
				content: qBuilderData.content,
				headerOptions: {
					title: qBuilderData.options.headerTitle,
					icon: qBuilderData.options.headerIcon
				},
				actions: qBuilderData.actions,
				navigationPanel: qBuilderData.navigationPanel
			});

			servicePanel = $('#service-panel');

			var formLogger = new $.SalesPortal.FormLogger();
			formLogger.init({
				logObject: {name: qBuilderData.options.headerTitle},
				formContent: servicePanel
			});

			if ($.cookie("showServicePanel") == "false")
				servicePanel.hide();
			else
				servicePanel.show();

			var headerContainer = servicePanel.find('.headers');
			headerContainer.find('.btn').off('click.qbuilder').on('click.qbuilder', function ()
			{
				headerContainer.find('.btn').removeClass('selected');
				$(this).addClass('selected');
				var relatedContentId = $(this).find('.service-data .tab-id').text();
				servicePanel.find('.service-panel-page').hide();
				$(relatedContentId).show();
				$.SalesPortal.QBuilder.PageList.updateContentSize();
				$.SalesPortal.QBuilder.LinkCart.updateContentSize();
			});

			$.SalesPortal.QBuilder.PageList.qBuilderData = qBuilderData;
			$.SalesPortal.QBuilder.PageList.load(qBuilderData.options.selectedPageId);
			$.SalesPortal.QBuilder.LinkCart.qBuilderData = qBuilderData;
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
			shortcutActionsContainer.find('.qbuilder-panel-show').off('click.action').on('click.action', function ()
			{
				servicePanel.show();
				shortcutActionsContainer.find('.qbuilder-panel-hide').show();
				$(this).hide();

				$.cookie("showServicePanel", true, {
					expires: (60 * 60 * 24 * 7)
				});

				updateContentSize();
			});
			shortcutActionsContainer.find('.qbuilder-panel-hide').off('click.action').on('click.action', function ()
			{
				servicePanel.hide();
				shortcutActionsContainer.find('.qbuilder-panel-show').show();
				$(this).hide();

				$.cookie("showServicePanel", false, {
					expires: (60 * 60 * 24 * 7)
				});

				updateContentSize();
			});

			shortcutActionsContainer.find('.qbuilder-qsite-add').off('click.action').on('click.action', function ()
			{
				$.SalesPortal.QBuilder.PageList.addPage();
			});

			shortcutActionsContainer.find('.qbuilder-qsite-delete').off('click.action').on('click.action', function ()
			{
				$.SalesPortal.QBuilder.PageList.deletePage();
			});

			shortcutActionsContainer.find('.qbuilder-qsite-save').off('click.action').on('click.action', function ()
			{
				$.SalesPortal.QBuilder.PageList.savePage(null);
			});

			shortcutActionsContainer.find('.qbuilder-qsite-preview')
				.prop('target', "_blank")
				.off('click.action').on('click.action', function (e)
			{
				var url = $(this).attr('href');
				e.preventDefault();
				$.SalesPortal.QBuilder.PageList.savePage(function ()
				{
					window.open(url);
				});
			});

			shortcutActionsContainer.find('.qbuilder-qsite-email').off('click.action').on('click.action', function ()
			{
				$.SalesPortal.QBuilder.PageList.emailPage();
			});

			shortcutActionsContainer.find('.qbuilder-qsite-delete-all-links').off('click.action').on('click.action', function ()
			{
				$.SalesPortal.QBuilder.PageList.selectedPage.deleteAllLinks();
			});
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.ShortcutsManager.updateContentSize();
			$.SalesPortal.QBuilder.PageList.updateContentSize();
			$.SalesPortal.QBuilder.LinkCart.updateContentSize();
		};

		var trackActivity = function ()
		{
			var activityData = $.parseJSON($('<div>' + qBuilderData.options.serviceData + '</div>').find('.activity-data').text());
			$.SalesPortal.ShortcutsManager.trackActivity(
				activityData,
				'QBuilder Activity',
				'QBuilder Activity');
		};
	};
})(jQuery);
