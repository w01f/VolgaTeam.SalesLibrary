(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.DocumentViewer = function (parameters)
	{
		var viewerData = new $.SalesPortal.DocumentViewerData(parameters.data);
		var dialogContent = undefined;
		var imageViewer = undefined;
		var imageViewerStartIndex = viewerData.startIndex;

		this.show = function ()
		{
			if (viewerData.config.isEOBrowser == true && viewerData.config.forceOpen == true)
				$.SalesPortal.SalesLibraryExtensions.openLink(viewerData);
			else if (viewerData.config.forceDownload == true)
				downloadFile();
			else
				$.fancybox({
					content: parameters.content,
					title: viewerData.name,
					autoSize: true,
					openEffect: 'none',
					closeEffect: 'none',
					afterShow: function ()
					{
						$.SalesPortal.SalesLibraryExtensions.sendLinkData(viewerData);

						dialogContent = $('.fancybox-wrap');

						if (viewerData.config.enableLogging)
						{
							var formLogger = new $.SalesPortal.FormLogger();
							formLogger.init({
								logObject: {
									name: viewerData.name,
									fileName: viewerData.fileName,
									format: viewerData.format,
									linkId: viewerData.linkId
								},
								formContent: dialogContent
							});
						}

						dialogContent.find('.tab-above-header').first().addClass('active');
						dialogContent.find('#link-viewer-body-tabs li').first().addClass('active');
						dialogContent.find('.tab-content .tab-pane').first().addClass('active');

						dialogContent.find('#link-viewer-body-tabs a[data-toggle="tab"]').on('shown.bs.tab', function (e)
						{
							dialogContent.find('.tab-above-header').removeClass('active');
							var tabTag = e.target.attributes['href'].value.replace("#link-viewer-tab-", "");
							dialogContent.find('#tab-above-header-' + tabTag).addClass('active');
						});

						dialogContent.find('.file-size').html('(' + viewerData.fileSize + ')');

						dialogContent.find('.download-file').off('click.preview').on('click.preview', downloadFile);
						dialogContent.find('.download-page').off('click.preview').on('click.preview', downloadPage);
						dialogContent.find('.add-quicksite').off('click.preview').on('click.preview', addToQuickSite);
						dialogContent.find('.add-favorites').off('click.preview').on('click.preview', addToFavorites);

						dialogContent.find("#image-viewer-slide-selector").selectpicker({
							dropupAuto: false,
							container: 'body',
							width: '60px'
						});

						updateImageViewer();

						dialogContent.find('.open-quick-link').off('click.preview').on('click.preview', openQuickLink);
						dialogContent.find('.open-pdf').off('click.preview').on('click.preview', openPdf);
						dialogContent.find('.open-gallery-modal').off('click.preview').on('click.preview', showGalleryModal);
						dialogContent.find('.open-gallery-fullscreen').off('click.preview').on('click.preview', showGalleryFullScreen);

						dialogContent.find('.action-container .action').off('click.preview').on('click.preview', processSaveAction);

						new $.SalesPortal.RateManager().init(
							{
								id: viewerData.linkId,
								name: viewerData.name,
								file: viewerData.fileName,
								format: viewerData.format
							},
							dialogContent.find('#user-link-rate-container'),
							viewerData.rateData);

						new $.SalesPortal.PreviewEmailer(viewerData, false);
						new $.SalesPortal.PreviewEmailer(viewerData, true);
					},
					afterClose: function ()
					{
						$.SalesPortal.SalesLibraryExtensions.releaseLinkData();
					}
				});
		};

		var updateImageViewer = function ()
		{
			imageViewer = new $.SalesPortal.PreviewGallery({
				container: dialogContent.find('.preview-gallery'),
				singlePage: viewerData.singlePage,
				pageSelector: dialogContent.find("#image-viewer-slide-selector"),
				pages: viewerData.pagesInPng,
				pageChanged: updateCurrentPageInfo,
				startIndex: imageViewerStartIndex
			});
		};

		var updateCurrentPageInfo = function (pageIndex)
		{
			imageViewerStartIndex = pageIndex;

			$.SalesPortal.SalesLibraryExtensions.switchDocumentPage(pageIndex);

			if (!viewerData.isPdf)
				dialogContent.find('.page-size').html('(' + viewerData.pages[pageIndex].size + ')');

			var pageSlides = viewerData.pagesInPng;
			dialogContent.find('.fancybox-title .child').html(pageSlides[pageIndex].title);
			dialogContent.find('.current-slide-info .text').html(pageSlides[pageIndex].itemIndexInfo);
		};

		var downloadFile = function ()
		{
			$.SalesPortal.LinkManager.downloadFile({
				name: viewerData.fileName,
				path: viewerData.filePath
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
			}
		};

		var openPdf = function ()
		{
			$.SalesPortal.LinkManager.openFile(viewerData.documentInPdf.link);
		};

		var openQuickLink = function ()
		{
			$.SalesPortal.LinkManager.openFile(viewerData.quickLinkUrl);
		};

		var addToQuickSite = function ()
		{
			$.fancybox.close();
			$.SalesPortal.QBuilder.LinkCart.addLinks([viewerData.linkId]);
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
			var images = viewerData.galleryPagesInPng;

			$.fancybox.close();
			$.fancybox(images,
				{
					openEffect: 'none',
					closeEffect: 'none',
					tpl: $.SalesPortal.Content.isMobileDevice() ?
					{
						next: '<a title="Next" class="fancybox-nav fancybox-next" href="javascript:;"></a>',
						prev: '<a title="Previous" class="fancybox-nav fancybox-prev" href="javascript:;"></a>'
					} :
					{
						next: '<a title="Next" class="fancybox-nav fancybox-next" href="javascript:;"><span></span></a>',
						prev: '<a title="Previous" class="fancybox-nav fancybox-prev" href="javascript:;"><span></span></a>'
					},
					afterShow: function ()
					{
						$.SalesPortal.SalesLibraryExtensions.sendLinkData(viewerData);
					},
					afterClose: function ()
					{
						$.SalesPortal.SalesLibraryExtensions.releaseLinkData();
						documentBar.close();
					},
					onUpdate: function ()
					{
						$.SalesPortal.SalesLibraryExtensions.switchDocumentPage(this.index);
						documentBar.resize();
						if (viewerData.config.enableLogging)
						{
							$.SalesPortal.LogHelper.write({
								type: 'Link',
								subType: 'Preview Page',
								linkId: viewerData.linkId,
								data: {
									name: viewerData.name,
									file: viewerData.fileName,
									originalFormat: viewerData.format,
									format: 'png'
								}
							});
						}
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
					viewerData.startIndex = imageViewerStartIndex;
					parameters.data = viewerData;
					new $.SalesPortal.DocumentViewer(parameters).show();
					if (viewerData.config.enableLogging)
					{
						$.SalesPortal.LogHelper.write({
							type: 'Link',
							subType: 'Preview Activity',
							linkId: viewerData.linkId,
							data: {
								name: viewerData.name,
								file: viewerData.fileName,
								originalFormat: viewerData.format,
								format: 'png'
							}
						});
					}
				}
			});
		};

		var showGalleryFullScreen = function ()
		{
			$.fancybox.close();
			window.open(window.BaseUrl + "preview/runFullScreenGallery?linkId=" + viewerData.linkId);
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
})(jQuery);
