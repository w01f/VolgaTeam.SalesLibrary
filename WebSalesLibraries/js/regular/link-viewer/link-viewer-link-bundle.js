(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.LinkBundleViewer = function (parameters) {
		var that = this;
		var dialogContent = undefined;
		var openedViewer = undefined;
		var viewerData = new $.SalesPortal.LinkBundleViewerData(parameters.data);

		this.show = function () {
			$.fancybox({
				content: parameters.content,
				title: "Link Bundle",
				autoSize: true,
				openEffect: 'none',
				closeEffect: 'none',
				afterShow: that.afterShow,
				afterClose: that.afterClose
			});
		};

		this.afterShow = function () {
			dialogContent = $('.bundle-container');

			initDialogTitle();

			dialogContent.find('.content-item').off('click.preview').on('click.preview', processBundleItem);

			var defaultItemId = parameters.data.defaultItemId;
			var defaultItem = dialogContent.find('#bundle-item-' + defaultItemId);
			defaultItem.find('>a').addClass('selected');
			var defaultItemData = defaultItem.find('.service-data');

			openBundleItem(defaultItemData);
		};

		this.afterClose = function () {
			releaseOpenedBundleItem();
		};

		var initDialogTitle = function () {
			if (viewerData.totalViews > 0)
				$('.fancybox-title').addClass('link-viewer-title');
		};

		var processBundleItem = function () {
			var itemData = $(this).find('.service-data');
			dialogContent.find('.content-item > a').removeClass('selected');
			$(this).find('>a').addClass('selected');
			openBundleItem(itemData);
		};

		var openBundleItem = function (itemData) {
			releaseOpenedBundleItem();

			parameters.data.defaultItemId = itemData.find('.item-id').text();

			var itemTitle = itemData.find('.item-title').text();
			var fancyBoxTitleArea = $('.fancybox-title .child');
			if (viewerData.totalViews > 0)
				fancyBoxTitleArea.html('<div class="row"><div class="col col-xs-10 text-left">' + itemTitle + '</div><div class="col col-xs-2 text-right">views (' + viewerData.totalViews + ')</div></div>');
			else
				fancyBoxTitleArea.html(title);

			var itemType = itemData.find('.item-type').text();
			switch (itemType)
			{
				case 'content':
					var itemContent = itemData.find('.item-info-content').html();
					dialogContent.find('.link-viewer-container').html(itemContent);
					break;
				case 'cover':
					var coverContent = itemData.find('.item-cover-content');
					var linkViewContainer = dialogContent.find('.link-viewer-container');
					linkViewContainer.html(coverContent.html());

					linkViewContainer.find('.download-link-bundle').off('click.preview').on('click.preview', downloadLinkBundle);
					linkViewContainer.find('.add-quicksite').off('click.preview').on('click.preview', addToQuickSite);
					linkViewContainer.find('.add-favorites').off('click.preview').on('click.preview', addToFavorites);

					var linkRateContainer =linkViewContainer.find('.user-link-rate-container');
					linkRateContainer.hide();
					$.ajax({
						type: "POST",
						url: window.BaseUrl + "rate/getRate",
						data: {
							linkId: viewerData.rateData.linkId
						},
						success: function (actualRateData) {
							linkRateContainer.show();
							new $.SalesPortal.RateManager().init(
								{
									id: viewerData.linkId,
									name: viewerData.name,
									file: viewerData.fileName,
									format: viewerData.format
								},
								linkRateContainer,
								actualRateData,
								function (newRateData) {
									viewerData.rateData = newRateData;
								}
							);
						},
						error: function () {
						},
						async: true,
						dataType: 'json'
					});
					break;
				case 'link':
					$.SalesPortal.Overlay.show();
					dialogContent.find('.link-viewer-container').html('');
					var libraryLinkId = itemData.find('.library-link-id').text();
					$.SalesPortal.LinkManager.requestViewDialog({
						linkId: libraryLinkId,
						isQuickSite: false,
						viewContainer: dialogContent.find('.link-viewer-container'),
						parentPreviewParameters: parameters,
						afterViewerOpenedCallback: function (viewer) {
							openedViewer = viewer;
						}
					});
					$.SalesPortal.Overlay.hide();
					break;
			}


		};

		var releaseOpenedBundleItem = function () {
			if (openedViewer !== undefined)
				openedViewer.afterClose();
			openedViewer = undefined;
			dialogContent.find('.link-viewer-container').html('');
		};

		var downloadLinkBundle = function () {
			$.SalesPortal.ZipDownloadFilesHelper.processLinkBundle(viewerData.linkId);
		};

		var addToQuickSite = function () {
			$.fancybox.close();
			$.SalesPortal.QBuilder.LinkCart.addLinks([viewerData.linkId]);
		};

		var addToFavorites = function () {
			$.SalesPortal.LinkManager.addToFavorites(
				viewerData.linkId,
				viewerData.name,
				viewerData.name,
				viewerData.format);
		};
	};
})(jQuery);
