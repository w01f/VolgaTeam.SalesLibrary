(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.LandingPage = $.SalesPortal.LandingPage || {};

	$.SalesPortal.LandingPage.SearchResultsBlock = function (parameters) {
		var that = this;
		var contentContainerId = parameters.containerId;
		var searchResultsManager = undefined;
		this.init = function () {
			var contentContainer = $('#search-results-block-' + contentContainerId);
			var searchShortcutOptions = new $.SalesPortal.SearchOptions($.parseJSON(contentContainer.find('.search-conditions .encoded-object').text()));
			var searchViewOptions = new $.SalesPortal.SearchResultsDataViewOptions($.parseJSON(contentContainer.find('.search-view-options .encoded-object').text()));

			var tableSizeSettings = undefined;
			var fixedHeightObject = contentContainer.find('.fixed-height');
			if (fixedHeightObject.length > 0)
			{
				tableSizeSettings = {
					useFixedSize: true,
					size: fixedHeightObject.text()
				};
			}
			else
				tableSizeSettings = {
					useFixedSize: false
				};

			searchResultsManager = new $.SalesPortal.SearchResultsManager({
				containerObject: contentContainer,
				baseSearchConditions: searchShortcutOptions.conditions,
				dataViewOptions: searchViewOptions,
				subSearchDefaultView: searchShortcutOptions.subSearchDefaultView,
				searchShortcutId: contentContainer.find('.shortcut-id').text(),
				searchShortcutTitle: searchShortcutOptions.title,
				isSearchBar: false,
				tableSizeSettings: tableSizeSettings
			});

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
						searchResultsManager.loadResults(data);
						$(window).off('resize.landing-page-search-results').on('resize.landing-page-search-results', that.updateContentSize);
					}
				}
			);
		};

		this.updateContentSize = function () {
			searchResultsManager.updateContentSize();
		};
	};
})(jQuery);