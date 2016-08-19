(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.YouTubeViewer = function (parameters)
	{
		var viewerData = new $.SalesPortal.YouTubeViewerData(parameters.data);
		var dialogContent = undefined;

		this.show = function ()
		{
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
				$.fancybox({
					content: parameters.content,
					title: viewerData.name,
					autoSize: true,
					openEffect: 'none',
					closeEffect: 'none',
					afterShow: function ()
					{
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

						dialogContent.find('.add-quicksite').off('click.preview').on('click.preview', addToQuickSite);
						dialogContent.find('.add-favorites').off('click.preview').on('click.preview', addToFavorites);
						dialogContent.find('.action-container .action').off('click.preview').on('click.preview', processSaveAction);

						dialogContent.find('.open-modal').off('click.preview').on('click.preview', showModal);
						dialogContent.find('.open-fullscreen').off('click.preview').on('click.preview', showFullScreen);

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
					}
				});
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
					viewerData.forcePreview = false;
					parameters.data = viewerData;
					new $.SalesPortal.YouTubeViewer(parameters).show();
				}
			});
		};

		var showFullScreen = function ()
		{
			$.fancybox.close();
			$.SalesPortal.LinkManager.openFile(viewerData.url);
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
