(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };

	$.SalesPortal.SimpleViewerData = function (source)
	{
		this.linkId = undefined;
		this.folderId = undefined;
		this.name = undefined;
		this.format = undefined;
		this.tags = undefined;
		this.url = undefined;
		this.oneDriveUrl = undefined;
		this.fileName = undefined;
		this.filePath = undefined;
		this.fileSize = undefined;
		this.quickLinkUrl = undefined;
		this.rateData = undefined;
		this.totalViews = undefined;

		for (var prop in source)
			if (source.hasOwnProperty(prop))
				this[prop] = source[prop];
	};

	$.SalesPortal.DocumentViewerData = function (source)
	{
		var that = this;

		this.linkId = undefined;
		this.folderId = undefined;
		this.linkBundleId = undefined;
		this.name = undefined;
		this.format = undefined;
		this.tags = undefined;
		this.url = undefined;
		this.fileName = undefined;
		this.filePath = undefined;
		this.fileSize = undefined;
		this.quickLinkUrl = undefined;
		this.thumbWidth = undefined;
		this.thumbHeight = undefined;
		this.singlePage = undefined;
		this.documentInPdf = undefined;
		this.pages = undefined;
		this.pagesInPng = undefined;
		this.galleryPagesInPng = undefined;
		this.rateData = undefined;
		this.totalViews = undefined;
		this.forcePreview = undefined;
		this.slideWidth = undefined;
		this.slideHeight = undefined;

		this.savedState = undefined;

		for (var prop in source)
			if (source.hasOwnProperty(prop))
				this[prop] = source[prop];

		this.isPdf = this.format === 'pdf';
	};

	$.SalesPortal.VideoViewerData = function (source)
	{
		this.linkId = undefined;
		this.folderId = undefined;
		this.linkBundleId = undefined;
		this.name = undefined;
		this.format = undefined;
		this.tags = undefined;
		this.url = undefined;
		this.fileName = undefined;
		this.filePath = undefined;
		this.fileSize = undefined;
		this.quickLinkUrl = undefined;

		this.thumbImageSrc = undefined;
		this.mp4Src = undefined;
		this.forcePreview = undefined;

		this.rateData = undefined;
		this.totalViews = undefined;

		for (var prop in source)
			if (source.hasOwnProperty(prop))
				this[prop] = source[prop];
	};

	$.SalesPortal.YouTubeViewerData = function (source)
	{
		this.linkId = undefined;
		this.folderId = undefined;
		this.linkBundleId = undefined;
		this.name = undefined;
		this.format = undefined;
		this.tags = undefined;
		this.url = undefined;
		this.fileName = undefined;
		this.quickLinkUrl = undefined;
		this.youTubeId = undefined;
		this.rateData = undefined;
		this.totalViews = undefined;

		for (var prop in source)
			if (source.hasOwnProperty(prop))
				this[prop] = source[prop];
	};

	$.SalesPortal.VimeoViewerData = function (source)
	{
		this.linkId = undefined;
		this.folderId = undefined;
		this.linkBundleId = undefined;
		this.name = undefined;
		this.format = undefined;
		this.tags = undefined;
		this.url = undefined;
		this.fileName = undefined;
		this.quickLinkUrl = undefined;
		this.playerUrl = undefined;
		this.rateData = undefined;
		this.totalViews = undefined;

		for (var prop in source)
			if (source.hasOwnProperty(prop))
				this[prop] = source[prop];
	};

	$.SalesPortal.LanViewerData = function (source)
	{
		this.linkId = undefined;
		this.folderId = undefined;
		this.name = undefined;
		this.format = undefined;
		this.tags = undefined;
		this.url = undefined;
		this.fileName = undefined;
		this.isEOBrowser = undefined;
		this.rateData = undefined;
		this.totalViews = undefined;

		for (var prop in source)
			if (source.hasOwnProperty(prop))
				this[prop] = source[prop];
	};

	$.SalesPortal.AppLinkViewerData = function (source)
	{
		this.linkId = undefined;
		this.folderId = undefined;
		this.name = undefined;
		this.format = undefined;
		this.tags = undefined;
		this.url = undefined;
		this.fileName = undefined;
		this.secondPath = undefined;
		this.rateData = undefined;
		this.totalViews = undefined;

		for (var prop in source)
			if (source.hasOwnProperty(prop))
				this[prop] = source[prop];
	};

	$.SalesPortal.InternalLinkViewerData = function (source)
	{
		this.linkId = undefined;
		this.folderId = undefined;
		this.linkBundleId = undefined;
		this.name = undefined;
		this.format = undefined;
		this.tags = undefined;
		this.url = undefined;
		this.fileName = undefined;
		this.previewInfo = undefined;
		this.rateData = undefined;
		this.totalViews = undefined;

		for (var prop in source)
			if (source.hasOwnProperty(prop))
				this[prop] = source[prop];
	};

	$.SalesPortal.ExcelViewerData = function (source)
	{
		this.linkId = undefined;
		this.folderId = undefined;
		this.linkBundleId = undefined;
		this.name = undefined;
		this.format = undefined;
		this.tags = undefined;
		this.url = undefined;
		this.fileName = undefined;
		this.quickLinkUrl = undefined;
		this.isEOBrowser = undefined;
		this.rateData = undefined;
		this.totalViews = undefined;

		for (var prop in source)
			if (source.hasOwnProperty(prop))
				this[prop] = source[prop];
	};

	$.SalesPortal.LinkBundleViewerData = function (source)
	{
		this.linkId = undefined;
		this.folderId = undefined;
		this.name = undefined;
		this.format = undefined;
		this.tags = undefined;
		this.url = undefined;
		this.fileName = undefined;
		this.quickLinkUrl = undefined;
		this.rateData = undefined;
		this.totalViews = undefined;

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
