(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.DocumentViewer = function (parameters)
	{
		var viewerData = new DocumentViewerData($.parseJSON(parameters.data));
		var dialogContent = undefined;

		var imageViewerType = viewerData.startFormat;
		var imageViewer = undefined;
		var imageViewerStartIndex = viewerData.startIndex;

		this.show = function ()
		{
			$.fancybox({
				content: parameters.content,
				title: viewerData.name,
				autoSize: true,
				openEffect: 'none',
				closeEffect: 'none',
				afterShow: function ()
				{
					dialogContent = $('.fancybox-wrap');

					dialogContent.find('#link-viewer-body-tabs a[data-toggle="tab"]').on('shown.bs.tab', function (e)
					{
						dialogContent.find('.tab-above-header').removeClass('active');
						var tabTag = e.target.attributes['href'].value.replace("#link-viewer-tab-", "");
						dialogContent.find('#tab-above-header-' + tabTag).addClass('active');
					});

					dialogContent.find('.file-size').html('(' + viewerData.fileSize + ')');

					dialogContent.find('.download-file').off('click').on('click', downloadFile);
					dialogContent.find('.download-page').off('click').on('click', downloadPage);
					dialogContent.find('.add-quicksite').off('click').on('click', addToQuickSite);
					dialogContent.find('.add-favorites').off('click').on('click', addToFavorites);

					dialogContent.find("#image-viewer-slide-selector").selectpicker({
						dropupAuto: false,
						container: 'body',
						width: '60px'
					});

					updateImageViewer();
					dialogContent.find('.image-type-toggle.png').off('click').on('click', function ()
					{
						if (!$(this).hasClass('selected'))
						{
							imageViewerType = 'png';
							updateImageViewer();
						}
					});
					dialogContent.find('.image-type-toggle.jpeg').off('click').on('click', function ()
					{
						if (!$(this).hasClass('selected'))
						{
							imageViewerType = 'jpeg';
							updateImageViewer();
						}
					});

					dialogContent.find('.open-pdf').off('click').on('click', openPdf);
					dialogContent.find('.open-gallery-modal').off('click').on('click', showGalleryModal);
					dialogContent.find('.open-gallery-fullscreen').off('click').on('click', showGalleryFullScreen);

					dialogContent.find('.action-container .action').off('click').on('click', processSaveAction);

					new $.SalesPortal.RateManager().init(
						viewerData.linkId,
						dialogContent.find('#user-link-rate-container'),
						viewerData.rateData);

					new $.SalesPortal.PreviewEmailer(viewerData);
				}
			});
		};

		var updateImageViewer = function ()
		{
			imageViewer = new $.SalesPortal.PreviewGallery({
				container: dialogContent.find('.preview-gallery'),
				singlePage: viewerData.singlePage,
				pageSelector: dialogContent.find("#image-viewer-slide-selector"),
				pages: viewerData.getPageImages(imageViewerType),
				pageChanged: updateCurrentPageInfo,
				startIndex: imageViewerStartIndex
			});

			switch (imageViewerType)
			{
				case 'png':
					dialogContent.find('.image-type-toggle.jpeg').removeClass('selected');
					dialogContent.find('.image-type-toggle.png').addClass('selected');
					break;
				case 'jpeg':
					dialogContent.find('.image-type-toggle.png').removeClass('selected');
					dialogContent.find('.image-type-toggle.jpeg').addClass('selected');
					break;
			}
		};

		var updateCurrentPageInfo = function (pageIndex)
		{
			imageViewerStartIndex = pageIndex;

			if (!viewerData.isPdf)
				dialogContent.find('.page-size').html('(' + viewerData.pages[pageIndex].size + ')');

			var pageSlides = viewerData.getPageImages(imageViewerType);
			dialogContent.find('.fancybox-title .child').html(pageSlides[pageIndex].title);
			dialogContent.find('.current-slide-info .text').html(pageSlides[pageIndex].itemIndexInfo);

			$.ajax({
				type: "POST",
				url: window.BaseUrl + "statistic/writeActivity",
				data: {
					type: 'Link',
					subType: 'Preview Page',
					data: $.toJSON({
						Name: viewerData.name,
						File: viewerData.fileName,
						'Original Format': viewerData.format,
						Format: imageViewerType,
						Mode: 'Modal'
					})
				},
				async: true,
				dataType: 'html'
			});
		};

		var downloadFile = function ()
		{
			$.SalesPortal.LinkManager.downloadFile({
				name: viewerData.fileName,
				path: viewerData.filePath
			});
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "statistic/writeActivity",
				data: {
					type: 'Link',
					subType: 'Download File',
					data: $.toJSON({
						Name: viewerData.name,
						File: viewerData.fileName,
						'Original Format': viewerData.format,
						Format: imageViewerType
					})
				},
				async: true,
				dataType: 'html'
			});
		};

		var downloadPage = function ()
		{
			if (imageViewer != undefined)
			{
				var page = viewerData.pages[imageViewer.currentPageIndex];
				$.SalesPortal.LinkManager.downloadFile({
					name: page.fileName,
					path: page.path
				});
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "statistic/writeActivity",
					data: {
						type: 'Link',
						subType: 'Download File',
						data: $.toJSON({
							Name: viewerData.name,
							File: viewerData.fileName,
							'Original Format': viewerData.format,
							Format: imageViewerType
						})
					},
					async: true,
					dataType: 'html'
				});
			}
		};

		var openPdf = function ()
		{
			$.SalesPortal.LinkManager.openFile(viewerData.documentInPdf.link);
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "statistic/writeActivity",
				data: {
					type: 'Link',
					subType: 'Download File',
					data: $.toJSON({
						Name: viewerData.name,
						File: viewerData.fileName,
						'Original Format': viewerData.format,
						Format: 'pdf'
					})
				},
				async: true,
				dataType: 'html'
			});
		};

		var addToQuickSite = function ()
		{
			$.fancybox.close();
			$.SalesPortal.QBuilder.LinkCart.addLinks([viewerData.linkId]);
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "statistic/writeActivity",
				data: {
					type: 'Link',
					subType: 'Add to QS',
					data: $.toJSON({
						Name: viewerData.name,
						File: viewerData.fileName,
						'Original Format': viewerData.format,
						Format: imageViewerType
					})
				},
				async: true,
				dataType: 'html'
			});
		};

		var addToFavorites = function ()
		{
			$.SalesPortal.LinkManager.addToFavorites(
				viewerData.linkId,
				viewerData.name,
				viewerData.fileName,
				viewerData.format);
		};

		var showGalleryModal = function ()
		{
			var documentBar = new $.SalesPortal.ViewerBar();
			var images = viewerData.getGalleryImages(imageViewerType);

			$.fancybox.close();

			$.fancybox(images,
				{
					openEffect: 'none',
					closeEffect: 'none',
					afterClose: function ()
					{
						documentBar.close();
					},
					onUpdate: function ()
					{
						documentBar.resize();
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "statistic/writeActivity",
							data: {
								type: 'Link',
								subType: 'Preview Page',
								data: $.toJSON({
									Name: viewerData.name,
									File: viewerData.fileName,
									'Original Format': viewerData.format,
									Format: imageViewerType,
									Mode: 'Modal'
								})
							},
							async: true,
							dataType: 'html'
						});
					},
					helpers: {
						thumbs: {
							height: viewerData.thumbHeight,
							width: viewerData.thumbWidth
						}
					}
				});
			documentBar.show({
				returnCallback: function ()
				{
					viewerData.startFormat = imageViewerType;
					viewerData.startIndex = imageViewerStartIndex;
					parameters.data = $.toJSON(viewerData);
					new $.SalesPortal.DocumentViewer(parameters).show();
				}
			});
		};

		var showGalleryFullScreen = function ()
		{
			$.fancybox.close();

			$.ajax({
				type: "POST",
				url: window.BaseUrl + "statistic/writeActivity",
				data: {
					type: 'Link',
					subType: 'Preview',
					data: $.toJSON({
						Name: viewerData.name,
						File: viewerData.fileName,
						'Original Format': viewerData.format,
						Format: imageViewerType,
						Mode: 'Fullscreen'
					})
				},
				async: true,
				dataType: 'html'
			});
			window.open("preview/runFullScreenGallery?linkId=" + viewerData.linkId + "&format=" + imageViewerType);
		};


		var processSaveAction = function ()
		{
			var tag = $(this).find('.service-data .tag').text();
			switch (tag)
			{
				case 'download':
					downloadFile();
					break;
				case 'download-pdf':
					openPdf();
					break;
				case 'quicksite':
					addToQuickSite();
					break;
				case 'favorites':
					addToFavorites();
					break;
			}
		};
	};

	var DocumentViewerData = function (source)
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
})(jQuery);
