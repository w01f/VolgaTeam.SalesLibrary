(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.PreviewGallery = function (parameters)
	{
		var that = this;
		var sliderData = new $.SalesPortal.GalleryData(parameters);

		var pageImageContainer = sliderData.container.find('.preview-image-container');
		var pageImage = sliderData.container.find('.page-preview-image');

		var navButtonNext = sliderData.container.find('.nav-image-button.move-next');
		var navButtonPrev = sliderData.container.find('.nav-image-button.move-previous');

		var totalPages = sliderData.pages.length;
		this.currentPageIndex = sliderData.startIndex;

		var initControls = function ()
		{
			if (!sliderData.singlePage)
			{
				sliderData.pageSelector.off('change.gallery');
				var options = '';
				for (i = 0; i < totalPages; i++)
				{
					var index = sliderData.pages[i].index;
					options += '<option value="' + index + '"';
					if (i === that.currentPageIndex)
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
				navButtonNext.off('click.gallery').on('click.gallery', function ()
				{
					if (!$(this).hasClass('disabled'))
					{
						that.currentPageIndex++;
						showCurrentPage();
						updateNavButtons();
					}
				});
				navButtonPrev.off('click.gallery').on('click.gallery', function ()
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
			var newPageImage = pageImage.clone();
			newPageImage.hide().appendTo(pageImageContainer);
			newPageImage.one('load', function ()
			{
				newPageImage.fadeIn(600, function ()
				{
					pageImage = newPageImage;
					if ($.isFunction(sliderData.pageChanged))
						sliderData.pageChanged(that.currentPageIndex);
				});
				pageImage.fadeOut(200, function ()
				{
					pageImage.remove();
				});
			});
			newPageImage.prop('src', page.href);
		};

		var updateNavButtons = function ()
		{
			navButtonNext.removeClass('disabled');
			navButtonPrev.removeClass('disabled');
			if (that.currentPageIndex === 0)
			{
				navButtonPrev.addClass('disabled');
			}
			if (that.currentPageIndex === (totalPages - 1))
			{
				navButtonNext.addClass('disabled');
			}

			sliderData.pageSelector.selectpicker('val', that.currentPageIndex);
			sliderData.pageSelector.selectpicker('refresh');
		};

		initControls();
	};
})(jQuery);
