(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };

	$.SalesPortal.SimpleViewerData = function (source)
	{
		this.linkId = undefined;
		this.name = undefined;
		this.format = undefined;
		this.tags = undefined;
		this.url = undefined;
		this.fileName = undefined;
		this.filePath = undefined;
		this.fileSize = undefined;
		this.rateData = undefined;

		for (var prop in source)
			if (source.hasOwnProperty(prop))
				this[prop] = source[prop];
	};

	$.SalesPortal.DocumentViewerData = function (source)
	{
		var that = this;

		this.linkId = undefined;
		this.name = undefined;
		this.format = undefined;
		this.tags = undefined;
		this.url = undefined;
		this.fileName = undefined;
		this.filePath = undefined;
		this.fileSize = undefined;
		this.thumbWidth = undefined;
		this.thumbHeight = undefined;
		this.singlePage = undefined;
		this.documentInPdf = undefined;
		this.pages = undefined;
		this.pagesInPng = undefined;
		this.galleryPagesInPng = undefined;
		this.pagesInJpeg = undefined;
		this.galleryPagesInJpeg = undefined;
		this.rateData = undefined;

		this.startIndex = 0;
		this.startFormat = 'png';

		for (var prop in source)
			if (source.hasOwnProperty(prop))
				this[prop] = source[prop];

		this.isPdf = this.format == 'pdf';

		this.getPageImages = function (format)
		{
			switch (format)
			{
				case 'png':
					return that.pagesInPng;
				case 'jpeg':
					return that.pagesInJpeg;
			}
			return [];
		};

		this.getGalleryImages = function (format)
		{
			switch (format)
			{
				case 'png':
					return that.galleryPagesInPng;
				case 'jpeg':
					return that.galleryPagesInJpeg;
			}
			return [];
		};
	};

	$.SalesPortal.VideoViewerData = function (source)
	{
		this.linkId = undefined;
		this.name = undefined;
		this.format = undefined;
		this.tags = undefined;
		this.url = undefined;
		this.fileName = undefined;
		this.filePath = undefined;
		this.fileSize = undefined;

		this.thumbImageSrc = undefined;
		this.playerSrc = undefined;
		this.mp4Src = undefined;

		this.rateData = undefined;

		for (var prop in source)
			if (source.hasOwnProperty(prop))
				this[prop] = source[prop];
	};

	$.SalesPortal.GalleryData = function (source)
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
