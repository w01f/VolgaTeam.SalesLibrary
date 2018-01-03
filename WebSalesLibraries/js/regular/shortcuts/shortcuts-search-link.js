(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsSearchLink = function (data) {
		var that = this;

		var parentSearchData = data;
		var optionsContainer = data.optionsContainer !== undefined ? data.optionsContainer :
			(parentSearchData.content !== undefined ? $('<div>' + parentSearchData.content + '</div>') : $.SalesPortal.Content.getContentObject());
		var searchShortcutOptions = new $.SalesPortal.SearchOptions($.parseJSON(optionsContainer.find('.search-conditions .encoded-object').text()));
		var searchViewOptions = new $.SalesPortal.SearchResultsDataViewOptions($.parseJSON(optionsContainer.find('.search-view-options .encoded-object').text()));

		var searchResultsManager = new $.SalesPortal.SearchResultsManager({
			containerObject: $.SalesPortal.Content.getContentObject(),
			baseSearchConditions: searchShortcutOptions.conditions,
			dataViewOptions: searchViewOptions,
			subSearchDefaultView: searchShortcutOptions.subSearchDefaultView,
			searchShortcutId: parentSearchData.options.linkId,
			searchShortcutTitle: searchShortcutOptions.title,
			isSearchBar: searchShortcutOptions.isSearchBar,
			backHandler: parentSearchData.backHandler
		});

		this.runSearch = function (resultCallback) {
			$.SalesPortal.SearchHelper.runSearch(
				{
					datasetKey: undefined,
					conditions: $.toJSON(searchShortcutOptions.conditions)
				},
				function () {
					$.SalesPortal.Overlay.show();
				},
				function () {
					$.SalesPortal.Overlay.hide();
				},
				function (data) {
					if (data.dataset.length > 0)
					{
						if (parentSearchData.content !== undefined)
						{
							$.SalesPortal.Content.fillContent({
								content: parentSearchData.content,
								headerOptions: parentSearchData.options.headerOptions,
								actions: parentSearchData.actions,
								navigationPanel: parentSearchData.navigationPanel,
								resizeCallback: function () {
									updateContentSize();
								}
							});
						}

						searchResultsManager.loadResults(data);

						initActionButtons();

						updateContentSize();
						$(window).off('resize.search-link').on('resize.search-link', updateContentSize);
					}
					if (resultCallback !== undefined)
						resultCallback(data);
					else if (data.dataset.length === 0)
					{
						var modalDialog = new $.SalesPortal.ModalDialog({
							title: 'Site Update',
							description: 'This section is not yet updated today.<br><br>' +
							'Check back later and maybe this page will be ready…',
							width: 300,
							buttons: [
								{
									tag: 'ok',
									title: 'OK',
									clickHandler: function () {
										modalDialog.close();
									}
								}
							]
						});
						modalDialog.show();
					}
				}
			);
		};

		var initActionButtons = function () {
			var shortcutActionsContainer = $('#shortcut-action-container');

			if (searchShortcutOptions.enableSubSearch)
			{
				shortcutActionsContainer.find('.sub-search-action').show();
				if (searchShortcutOptions.subSearchDefaultView === 'all')
					shortcutActionsContainer.find('.sub-search-all').hide();
				else if (searchShortcutOptions.subSearchDefaultView === 'search')
					shortcutActionsContainer.find('.sub-search-criteria').hide();
				else if (searchShortcutOptions.subSearchDefaultView === 'links')
					shortcutActionsContainer.find('.sub-search-links').hide();

				shortcutActionsContainer.find('.sub-search-all').off('click.action').on('click.action', function () {
					searchResultsManager.showSimpleSearchView();
					shortcutActionsContainer.find('.sub-search-action').show();
					$(this).hide();
				});

				shortcutActionsContainer.find('.sub-search-criteria').off('click.action').on('click.action', function () {
					searchResultsManager.showSubSearchView();
					shortcutActionsContainer.find('.sub-search-action').show();
					$(this).hide();
				});

				shortcutActionsContainer.find('.sub-search-links').off('click.action').on('click.action', function () {
					searchResultsManager.showSearchByTemplateView();
					shortcutActionsContainer.find('.sub-search-action').show();
					$(this).hide();
				});
			}
			else
			{
				shortcutActionsContainer.find('.sub-search-action').hide();
			}
		};

		var updateContentSize = function () {
			$.SalesPortal.ShortcutsManager.updateContentSize();

			var content = $.SalesPortal.Content.getContentObject();
			var navigationPanel = $.SalesPortal.Content.getNavigationPanel();
			var width = $(window).width() - navigationPanel.outerWidth(true) - 5;

			$('#content').css({
				'overflow': 'hidden'
			});

			content.css({
				'max-width': width + 'px',
				'width': width + 'px',
				'overflow': 'hidden'
			});

			searchResultsManager.updateContentSize();
		};

		return that;
	};
})(jQuery);