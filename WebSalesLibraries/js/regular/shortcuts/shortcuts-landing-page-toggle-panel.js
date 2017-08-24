(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.LandingPage = $.SalesPortal.LandingPage || {};
	$.SalesPortal.LandingPage.TogglePanel = function (parameters) {
		var togglePanelId = parameters.containerId;
		var togglePanel = undefined;

		this.init = function () {
			togglePanel = $('#toggle-panel-' + togglePanelId);
			initTogglePanel();
		};

		var initTogglePanel = function () {
			var buttons = togglePanel.find('.toggle-button');
			var items = togglePanel.find('.toggle-item');
			buttons.off('click').on('click', function () {
				var selectedButton = $(this);
				var tag = selectedButton.data('toggle-tag');

				buttons.removeClass('toggle-button-active');
				items.removeClass('toggle-item-active');

				selectedButton.addClass('toggle-button-active');

				var selectedPanel = togglePanel.find('.toggle-item[data-toggle-tag="' + tag + '"]');
				selectedPanel.addClass('toggle-item-active');

				var masonry = selectedPanel.find('.masonry-container');
				if (masonry.length > 0)
					$.each(masonry, function (key, value) {
						var masonryBlock = $(value);
						var masonryId = masonryBlock.prop('id').replace('masonry-container-', '');
						var querySettingsEncoded = masonryBlock.find('>.service-data .encoded-object .query-settings').text();
						var querySettings = querySettingsEncoded !== undefined && querySettingsEncoded.length ? $.parseJSON(querySettingsEncoded) : undefined;
						var viewSettingsEncoded = masonryBlock.find('>.service-data .encoded-object .view-settings').text();
						var viewSettings = viewSettingsEncoded !== undefined && viewSettingsEncoded.length ? $.parseJSON(viewSettingsEncoded) : undefined;
						new $.SalesPortal.LandingPage.Masonry({
							containerId: masonryId,
							querySettings: querySettings,
							viewSettings: viewSettings
						}).init();
					});
			});
		};
	};
})(jQuery);