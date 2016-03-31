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
		this.rateData = undefined;
		this.slideWidth = undefined;
		this.slideHeight = undefined;

		this.startIndex = 0;

		for (var prop in source)
			if (source.hasOwnProperty(prop))
				this[prop] = source[prop];

		this.isPdf = this.format == 'pdf';
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
