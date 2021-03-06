(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.DocumentViewer = function (parameters)
	{
		var that = this;
		var viewerData = new $.SalesPortal.DocumentViewerData(parameters.data);
		var dialogContent = undefined;
		var imageViewer = undefined;
		var embeddedViewer = false;

		var imageViewerStartIndex = viewerData.savedState && viewerData.savedState.startIndex ? viewerData.savedState.startIndex : 0;

		this.show = function ()
		{
			embeddedViewer = parameters.viewContainer !== undefined;

			if (viewerData.config.isEOBrowser == true && viewerData.config.forceEOOpen == true)
				$.SalesPortal.SalesLibraryExtensions.openLink(viewerData);
			else if (viewerData.config.forceDownload == true)
				downloadFile();
			else if (viewerData.config.forceWebOpen == true)
				open();
			else if (viewerData.config.forceOpenOneDrive == true)
				openOneDrive();
			else
			{
				if (parameters.viewContainer === undefined)
					$.fancybox({
						content: parameters.content,
						title: viewerData.name,
						autoSize: true,
						openEffect: 'none',
						closeEffect: 'none',
						afterShow: that.afterShow,
						afterClose: that.afterClose
					});
				else
				{
					parameters.viewContainer.html(parameters.content);
					parameters.viewContainer = undefined;
					that.afterShow();
					return that;
				}
			}
		};

		this.afterShow = function ()
		{
			$.SalesPortal.SalesLibraryExtensions.sendLinkData(viewerData);

			initDialogTitle();

			dialogContent = embeddedViewer ? $('.link-viewer-container .link-viewer') : $('.link-viewer');

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
			dialogContent.find('.download-link-bundle').off('click.preview').on('click.preview', downloadLinkBundle);
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
			dialogContent.find('.one-drive-link-copy').off('click.preview').on('click.preview', function (e) {
				$.SalesPortal.LinkManager.copyTextToClipboard($(this).prop('href'));
				e.preventDefault();
			});

			dialogContent.find('.action-container .action').off('click.preview').on('click.preview', processSaveAction);

			new $.SalesPortal.RateManager().init(
				{
					id: viewerData.linkId,
					name: viewerData.name,
					file: viewerData.fileName,
					format: viewerData.format
				},
				dialogContent.find('.user-link-rate-container'),
				viewerData.rateData,
				function (newRateData)
				{
					viewerData.rateData = newRateData;
				}
			);

			new $.SalesPortal.PreviewEmailer(viewerData);

			dialogContent.find('.btn').off('mouseup.preview').on('mouseup.preview', function ()
			{
				$(this).blur();
			});
		};

		this.afterClose = function ()
		{
			$.SalesPortal.SalesLibraryExtensions.releaseLinkData();
			if (parameters.afterViewerClosedCallback !== undefined)
				parameters.afterViewerClosedCallback();
		};

		var initDialogTitle = function ()
		{
			var fancyboxTitle = $('.fancybox-title');
			if (viewerData.totalViews > 0 && !fancyboxTitle.hasClass('link-viewer-title'))
				fancyboxTitle.addClass('link-viewer-title');
		};

		var setDialogTitle = function (title)
		{
			if (!embeddedViewer)
			{
				if (viewerData.totalViews > 0)
					$('.fancybox-title .child').html('<div class="row"><div class="col col-xs-10 text-left">' + title + '</div><div class="col col-xs-2 text-right">views (' + viewerData.totalViews + ')</div></div>');
				else
					$('.fancybox-title .child').html(title);
			}
			else
			{
				$('.fancybox-title .child .text-left').html(title);
			}
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
			dialogContent.find('.current-slide-info .text').html(pageSlides[pageIndex].itemIndexInfo);
			setDialogTitle(pageSlides[pageIndex].title);
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
			if (imageViewer !== undefined)
			{
				var page = viewerData.pages[imageViewer.currentPageIndex];
				$.SalesPortal.LinkManager.downloadFile({
					name: page.fileName,
					path: page.path
				});
			}
		};

		var downloadLinkBundle = function ()
		{
			$.SalesPortal.ZipDownloadFilesHelper.processLinkBundle(viewerData.linkBundleId);
		};

		var open = function ()
		{
			$.SalesPortal.LinkManager.openFile(viewerData.url, "_blank");
		};

		var openOneDrive = function ()
		{
			$.SalesPortal.LinkManager.openFile(viewerData.oneDriveUrl, "_blank");
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
			var dragAndDropSource = dialogContent.find('.preview-gallery .page-preview-image');

			$.fancybox.close();
			$.fancybox(images,
				{
					openEffect: 'none',
					index: imageViewerStartIndex,
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
						imageViewerStartIndex = this.index;
						documentBar.resize();

						$('#' + this.id).off('dragstart').on('dragstart', function (e)
						{
							var urlHeader = dragAndDropSource.data("url-header");
							var url = dragAndDropSource.data('url');
							if (url !== '')
								e.originalEvent.dataTransfer.setData(urlHeader, url);
						});

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
					var parentPreviewParameters = parameters.parentPreviewParameters;
					parentPreviewParameters.data.savedState = {startIndex: imageViewerStartIndex};
					$.SalesPortal.LinkManager.openViewerDialog(parentPreviewParameters);
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
