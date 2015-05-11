(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.PreviewGallery = function (parameters)
	{
		var that = this;
		var sliderData = new GalleryData(parameters);

		var pageImage = sliderData.container.find('.page-image');

		var navButtonNext = sliderData.container.find('.nav-image-button.move-next');
		var navButtonPrev = sliderData.container.find('.nav-image-button.move-previous');

		var totalPages = sliderData.pages.length;
		this.currentPageIndex = sliderData.startIndex;

		var initControls = function ()
		{
			if (!sliderData.singlePage)
			{
				sliderData.pageSelector.off('change');
				var options = '';
				for (i = 0; i < totalPages; i++)
				{
					var index = sliderData.pages[i].index;
					options += '<option value="' + index + '"';
					if (i == that.currentPageIndex)
						options += ' selected = "selected"';
					options += '>' + (index + 1) + '</option>';
				}
				sliderData.pageSelector.html(options);
				sliderData.pageSelector.selectpicker('refresh');
				sliderData.pageSelector.on('change', function ()
				{
					that.currentPageIndex = sliderData.pageSelector.selectpicker('val');
					showCurrentPage();
					updateNavButtons();
				});
				navButtonNext.off('click').on('click', function ()
				{
					if (!$(this).hasClass('disabled'))
					{
						that.currentPageIndex++;
						showCurrentPage();
						updateNavButtons();
					}
				});
				navButtonPrev.off('click').on('click', function ()
				{
					if (!$(this).hasClass('disabled'))
					{
						that.currentPageIndex--;
						showCurrentPage();
						updateNavButtons();
					}
				});
				updateNavButtons();
			}

			showCurrentPage();
		};

		var showCurrentPage = function ()
		{
			var page = sliderData.pages[that.currentPageIndex];

			pageImage.fadeOut(500, function ()
			{
				var image = $(this);
				image.one('load', function ()
				{
					image.fadeIn(500);
				});
				image.prop('src', page.href);
			});

			if ($.isFunction(sliderData.pageChanged))
				sliderData.pageChanged(that.currentPageIndex);
		};

		var updateNavButtons = function ()
		{
			navButtonNext.removeClass('disabled');
			navButtonPrev.removeClass('disabled');
			if (that.currentPageIndex == 0)
			{
				navButtonPrev.addClass('disabled');
			}
			if (that.currentPageIndex == (totalPages - 1))
			{
				navButtonNext.addClass('disabled');
			}

			sliderData.pageSelector.selectpicker('val',that.currentPageIndex);
			sliderData.pageSelector.selectpicker('render');
		};

		initControls();
	};

	var GalleryData = function (source)
	{
		this.container = undefined;
		this.singlePage = undefined;
		this.pageSelector = undefined;
		this.pages = undefined;
		this.pageChanged = undefined;
		this.startIndex = undefined;

		for (var prop in source)
			if (source.hasOwnProperty(prop))
				this[prop] = source[prop];
	};

})(jQuery);
