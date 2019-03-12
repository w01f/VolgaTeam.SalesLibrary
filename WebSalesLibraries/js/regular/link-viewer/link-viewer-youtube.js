(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.YouTubeViewer = function (parameters)
	{
		var that = this;
		var viewerData = new $.SalesPortal.YouTubeViewerData(parameters.data);
		var dialogContent = undefined;
		var embeddedViewer = false;

		this.show = function ()
		{
			embeddedViewer = parameters.viewContainer !== undefined;

			if (viewerData.forcePreview == true)
			{
				showModal();
				$.SalesPortal.LogHelper.write({
					type: 'Link',
					subType: 'Preview Modal',
					linkId: viewerData.linkId,
					data: {
						name: viewerData.name,
						file: viewerData.fileName,
						originalFormat: viewerData.format
					}
				});
			}
			else
			{
				if (parameters.viewContainer === undefined)
					$.fancybox({
						content: parameters.content,
						title: viewerData.name,
						autoSize: true,
						openEffect: 'none',
						closeEffect: 'none',
						afterShow: that.afterShow
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
			dialogContent = embeddedViewer ? $('.link-viewer-container .link-viewer') : $('.link-viewer');

			initDialogTitle();

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

			dialogContent.find('.add-quicksite').off('click.preview').on('click.preview', addToQuickSite);
			dialogContent.find('.add-favorites').off('click.preview').on('click.preview', addToFavorites);
			dialogContent.find('.action-container .action').off('click.preview').on('click.preview', processSaveAction);

			dialogContent.find('.open-quick-link').off('click.preview').on('click.preview', openQuickLink);
			dialogContent.find('.open-video-modal').off('click.preview').on('click.preview', showModal);
			dialogContent.find('.open-video-fullscreen').off('click.preview').on('click.preview', showFullScreen);

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

			setDialogTitle(viewerData.name)
		};

		this.afterClose = function ()
		{
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

		var showModal = function ()
		{
			$.fancybox.close();
			var viewerBar = new $.SalesPortal.ViewerBar();
			$.fancybox({
				title: viewerData.name,
				content: '<iframe ' +
				'height = "480" width="680" frameborder="0" allowfullscreen ' +
				'src="https://www.youtube.com/embed/' + viewerData.youTubeId + '?autoplay=1">' +
				'</iframe>',
				openEffect: 'none',
				closeEffect: 'none',
				afterShow: function ()
				{
					$('.fancybox-wrap').addClass('content-boxed');
				},
				afterClose: function ()
				{
					viewerBar.close();
				}
			});
			viewerBar.show({
				returnCallback: function ()
				{
					$.fancybox.close();
					var parentPreviewParameters = parameters.parentPreviewParameters;
					$.SalesPortal.LinkManager.openViewerDialog(parentPreviewParameters);
				}
			});
		};

		var showFullScreen = function ()
		{
			$.fancybox.close();
			$.SalesPortal.LinkManager.openFile(viewerData.url);
		};

		var openQuickLink = function ()
		{
			$.SalesPortal.LinkManager.openFile(viewerData.quickLinkUrl);
		};

		var processSaveAction = function ()
		{
			var tag = $(this).find('.service-data .tag').text();
			switch (tag)
			{
				case 'open':
					$.fancybox.close();
					$.SalesPortal.LinkManager.openFile(viewerData.url);
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
