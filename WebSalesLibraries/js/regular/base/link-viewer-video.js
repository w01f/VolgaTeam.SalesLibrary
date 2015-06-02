(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.VideoViewer = function (parameters)
	{
		var viewerData = new $.SalesPortal.VideoViewerData($.parseJSON(parameters.data));
		var dialogContent = undefined;
		var player = undefined;

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
					dialogContent.find('.add-quicksite').off('click').on('click', addToQuickSite);
					dialogContent.find('.add-favorites').off('click').on('click', addToFavorites);
					dialogContent.find('.action-container .action').off('click').on('click', processSaveAction);

					VideoJS.players = {};
					dialogContent.find('.page-image').hide();
					dialogContent.find('.play-video').hide();
					dialogContent.find('#video-player').show();
					_V_.options.flash.swf = viewerData.playerSrc;
					player = _V_("video-player", {
							controls: true,
							autoplay: false,
							preload: 'auto',
							poster: viewerData.thumbImageSrc
						},
						function ()
						{
						});
					player.src([
						{
							src: viewerData.mp4Src.href,
							href: viewerData.mp4Src.href,
							title: viewerData.fileName,
							type: viewerData.mp4Src.type
						}
					]);
					player.play();

					dialogContent.find('.open-video-modal').off('click').on('click', showVideoModal);
					dialogContent.find('.open-video-fullscreen').off('click').on('click', showVideoFullScreen);

					new $.SalesPortal.RateManager().init(
						viewerData.linkId,
						dialogContent.find('#user-link-rate-container'),
						viewerData.rateData);

					new $.SalesPortal.PreviewEmailer(viewerData);
				}
			});
		};

		var downloadFile = function ()
		{
			$.SalesPortal.LinkManager.downloadFile({
				name: viewerData.mp4Src.title,
				path: viewerData.mp4Src.path
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

		var showVideoModal = function ()
		{
			$.fancybox.close();
			var viewerBar = new $.SalesPortal.ViewerBar();
			$.SalesPortal.LinkManager.playVideo([
				{
					src: viewerData.mp4Src.href,
					href: viewerData.mp4Src.href,
					title: viewerData.fileName,
					type: viewerData.mp4Src.type,
					swf: viewerData.playerSrc
				}
			], viewerBar);
			viewerBar.show({
				returnCallback: function ()
				{
					$.fancybox.close();
					parameters.data = $.toJSON(viewerData);
					new $.SalesPortal.VideoViewer(parameters).show();
				}
			});
		};

		var showVideoFullScreen = function ()
		{
			player.requestFullScreen();
		};

		var processSaveAction = function ()
		{
			var tag = $(this).find('.service-data .tag').text();
			switch (tag)
			{
				case 'download':
					$.SalesPortal.LinkManager.downloadFile({
						name: viewerData.mp4Src.title,
						path: viewerData.mp4Src.path
					});
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
