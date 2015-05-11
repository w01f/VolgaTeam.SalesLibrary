(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.VideoViewer = function (parameters)
	{
		var viewerData = new VideoViewerData($.parseJSON(parameters.data));
		var dialogContent = undefined;

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
					var myPlayer = _V_("video-player", {
							controls: true,
							autoplay: false,
							preload: 'auto',
							poster: viewerData.thumbImageSrc
						},
						function ()
						{
						});
					myPlayer.src([
						{
							src: viewerData.mp4Src.href,
							href: viewerData.mp4Src.href,
							title: viewerData.fileName,
							type: viewerData.mp4Src.type
						},
						{
							src: viewerData.ogvSrc.href,
							href: viewerData.ogvSrc.href,
							title: viewerData.fileName,
							type: viewerData.ogvSrc.type
						}
					]);

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
				name: viewerData.fileName,
				path: viewerData.filePath
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

		var playVideo = function ()
		{

			myPlayer.play();
		};

		var processSaveAction = function ()
		{
			var tag = $(this).find('.service-data .tag').text();
			switch (tag)
			{
				case 'download-mp4':
					$.SalesPortal.LinkManager.downloadFile({
						name: viewerData.mp4Src.title,
						path: viewerData.mp4Src.path
					});
					break;
				case 'download-wmv':
					$.SalesPortal.LinkManager.downloadFile({
						name: viewerData.wmvSrc.title,
						path: viewerData.wmvSrc.path
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

	var VideoViewerData = function (source)
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
		this.wmvSrc = undefined;
		this.ogvSrc = undefined;

		this.rateData = undefined;

		for (var prop in source)
			if (source.hasOwnProperty(prop))
				this[prop] = source[prop];
	};
})(jQuery);
