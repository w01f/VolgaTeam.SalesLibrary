(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	var ScreenManager = function () {
		var that = this;
		var currentScreenSizeType = 'large';

		this.init = function () {
			that.processScreenSizeChange();
		};

		this.getScreenSettings = function () {
			return {
				screenSizeType: currentScreenSizeType
			};
		};

		this.processScreenSizeChange = function (eventHandler) {
			var windowWidth = $(window).width();
			var newScreenSizeType = 'large';
			if (windowWidth >= 1200)
				newScreenSizeType = 'large';
			else if (windowWidth >= 992)
				newScreenSizeType = 'medium';
			else if (windowWidth >= 768)
				newScreenSizeType = 'small';
			else
				newScreenSizeType = 'extrasmall';

			var oldScreenSizeType = currentScreenSizeType;
			currentScreenSizeType = newScreenSizeType;
			if (newScreenSizeType !== oldScreenSizeType && eventHandler !== undefined)
				eventHandler();
		};
	};
	$.SalesPortal.ScreenManager = new ScreenManager();
})(jQuery);