(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.LandingPage = $.SalesPortal.LandingPage || {};
	$.SalesPortal.LandingPage.Wallbin = $.SalesPortal.LandingPage.Wallbin || {};

	$.SalesPortal.LandingPage.Wallbin.LibraryBlock = function (parameters) {
		var contentContainerId = parameters.containerId;

		this.init = function () {
			var contentContainer = $('#library-block-' + contentContainerId);

			var wallbinSettings = new WallbinSettings($.parseJSON(atob(contentContainer.find('.wallbin-settings .encoded-data').text())));

			var wallbinManager = new $.SalesPortal.WallbinManager({
				contentObject: contentContainer,
				shortcutId: wallbinSettings.shortcutId,
				wallbinId: wallbinSettings.wallbinId,
				wallbinName: wallbinSettings.wallbinName,
				pageViewType: wallbinSettings.pageViewType,
				pageSelectorMode: wallbinSettings.pageSelectorMode,
				processResponsiveColumns: wallbinSettings.processResponsiveColumns,
				fitWallbinToWholeScreen: false
			});
			wallbinManager.initPageSelector();
			wallbinManager.initContent();
			contentContainer.find('.page-container').addClass('selected').show();

			$(window).off('resize.landing-page-wallbin-library').on('resize.landing-page-wallbin-library', wallbinManager.updateContentSize);
			wallbinManager.updateContentSize();
		};
	};

	$.SalesPortal.LandingPage.Wallbin.LibraryPageBundleBlock = function (parameters) {
		var contentContainerId = parameters.containerId;

		this.init = function () {
			var contentContainer = $('#library-page-bundle-block-' + contentContainerId);

			var wallbinSettings = new WallbinSettings($.parseJSON(atob(contentContainer.find('.wallbin-settings .encoded-data').text())));

			var wallbinManager = new $.SalesPortal.WallbinManager({
				contentObject: contentContainer,
				shortcutId: wallbinSettings.shortcutId,
				wallbinId: wallbinSettings.wallbinId,
				wallbinName: wallbinSettings.wallbinName,
				pageViewType: wallbinSettings.pageViewType,
				pageSelectorMode: wallbinSettings.pageSelectorMode,
				processResponsiveColumns: wallbinSettings.processResponsiveColumns,
				fitWallbinToWholeScreen: false
			});
			wallbinManager.initPageSelector();
			wallbinManager.initContent();
			contentContainer.find('.page-container').addClass('selected').show();

			$(window).off('resize.landing-page-wallbin-library-page-bundle').on('resize.landing-page-wallbin-library-page-bundle', wallbinManager.updateContentSize);
			wallbinManager.updateContentSize();
		};
	};

	$.SalesPortal.LandingPage.Wallbin.LibraryPageBlock = function (parameters) {
		var contentContainerId = parameters.containerId;

		this.init = function () {
			var contentContainer = $('#library-page-block-' + contentContainerId);

			var wallbinSettings = new WallbinSettings($.parseJSON(atob(contentContainer.find('.wallbin-settings .encoded-data').text())));

			var wallbinManager = new $.SalesPortal.WallbinManager({
				contentObject: contentContainer,
				shortcutId: wallbinSettings.shortcutId,
				pageViewType: wallbinSettings.pageViewType,
				processResponsiveColumns: wallbinSettings.processResponsiveColumns,
				fitWallbinToWholeScreen: false
			});
			wallbinManager.initContent();
			contentContainer.find('.page-container').addClass('selected').show();

			$(window).off('resize.landing-page-wallbin-library-page').on('resize.landing-page-wallbin-library-page', wallbinManager.updateContentSize);
			wallbinManager.updateContentSize();
		};
	};

	$.SalesPortal.LandingPage.Wallbin.LibraryWindowBlock = function (parameters) {
		var contentContainerId = parameters.containerId;

		this.init = function () {
			var contentContainer = $('#library-window-block-' + contentContainerId);

			var wallbinSettings = new WallbinSettings($.parseJSON(atob(contentContainer.find('.wallbin-settings .encoded-data').text())));

			var wallbinManager = new $.SalesPortal.WallbinManager({
				contentObject: contentContainer,
				shortcutId: wallbinSettings.shortcutId,
				pageViewType: wallbinSettings.pageViewType,
				processResponsiveColumns: wallbinSettings.processResponsiveColumns,
				fitWallbinToWholeScreen: false
			});
			wallbinManager.initContent();
		};
	};

	var WallbinSettings = function (data) {
		this.shortcutId = undefined;
		this.wallbinId = undefined;
		this.wallbinName = undefined;
		this.pageViewType = undefined;
		this.pageSelectorMode = undefined;
		this.processResponsiveColumns = false;

		for (var property in data)
			if (data.hasOwnProperty(property))
				this[property] = data[property];
	};
})(jQuery);