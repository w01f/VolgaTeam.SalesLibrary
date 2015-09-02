(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsQBuilder = function ()
	{
		var qBuilderData = undefined;

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

			$.SalesPortal.QBuilder.PageList.init();
			$.SalesPortal.QBuilder.PageList.show();

			$.SalesPortal.QBuilder.LinkCart.init();
			$.SalesPortal.QBuilder.LinkCart.show();

			initActionButtons();

			$(window).off('resize.qbuilder').on('resize.qbuilder', updateContentSize);
			updateContentSize();
		};

		var initActionButtons = function ()
		{
			var shortcutActionsContainer = $('#shortcut-action-container');

			if ($.cookie("showQPageList") == "true")
				shortcutActionsContainer.find('.qbuilder-page-list-show').hide();
			else
				shortcutActionsContainer.find('.qbuilder-page-list-hide').show();
			shortcutActionsContainer.find('.qbuilder-page-list-show').off('click').on('click', function ()
			{
				$.SalesPortal.QBuilder.PageList.show();
				shortcutActionsContainer.find('.qbuilder-page-list-hide').show();
				$(this).hide();
				updateContentSize();
			});
			shortcutActionsContainer.find('.qbuilder-page-list-hide').off('click').on('click', function ()
			{
				$.SalesPortal.QBuilder.PageList.hide();
				shortcutActionsContainer.find('.qbuilder-page-list-show').show();
				$(this).hide();
				updateContentSize();
			});

			if ($.cookie("showLinkCart") == "true")
				shortcutActionsContainer.find('.qbuilder-link-cart-show').hide();
			else
				shortcutActionsContainer.find('.qbuilder-link-cart-hide').show();
			shortcutActionsContainer.find('.qbuilder-link-cart-show').off('click').on('click', function ()
			{
				$.SalesPortal.QBuilder.LinkCart.show();
				shortcutActionsContainer.find('.qbuilder-link-cart-hide').show();
				$(this).hide();
				updateContentSize();
			});
			shortcutActionsContainer.find('.qbuilder-link-cart-hide').off('click').on('click', function ()
			{
				$.SalesPortal.QBuilder.LinkCart.hide();
				shortcutActionsContainer.find('.qbuilder-link-cart-show').show();
				$(this).hide();
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
