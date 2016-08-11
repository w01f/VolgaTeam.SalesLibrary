(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	var LinkManager = function ()
	{
		var that = this;

		this.requestViewDialog = function (linkId, isQuickSite)
		{
			$('body').find('.mtContent').remove();
			that.cleanupContextMenu();
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "preview/getViewDialog",
				data: {
					linkId: linkId,
					isQuickSite: isQuickSite
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (parameters)
				{
					if (parameters.data.config.allowPreview)
					{
						if (parameters.data.config.enableLogging)
							$.SalesPortal.LogHelper.write({
								type: 'Link',
								subType: 'Preview Options',
								data: {
									Name: parameters.data.name,
									File: parameters.data.fileName,
									'Original Format': parameters.format
								}
							});

						switch (parameters.format)
						{
							case 'document':
								new $.SalesPortal.DocumentViewer(parameters).show();
								break;
							case 'video':
								new $.SalesPortal.VideoViewer(parameters).show();
								break;
							case 'youtube':
								new $.SalesPortal.YouTubeViewer(parameters).show();
								break;
							case 'lan':
								new $.SalesPortal.LanViewer(parameters).show();
								break;
							case 'app':
								new $.SalesPortal.AppLinkViewer(parameters).show();
								break;
							case 'internal':
								new $.SalesPortal.InternalLinkViewer(parameters).show();
								break;
							case 'image':
								new $.SalesPortal.ImageViewer(parameters).show();
								break;
							default :
								new $.SalesPortal.FileViewer(parameters).show();
								break;
						}
					}
					else
					{
						var modalDialog = new $.SalesPortal.ModalDialog({
							title: '<span class="text-danger">Sorry...</span>',
							description: 'You are not authorized to view this link.',
							buttons: [
								{
									tag: 'close',
									title: 'Close',
									width: 80,
									clickHandler: function ()
									{
										modalDialog.close();
									}
								}
							],
							closeOnOutsideClick: true
						});
						modalDialog.show();
					}
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'json'
			});
		};

		this.requestLinkContextMenu = function (linkId, isQuickSite, pointX, pointY)
		{
			$('body').find('.mtContent').remove();
			that.cleanupContextMenu();
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "preview/getLinkContextMenu",
				data: {
					linkId: linkId,
					isQuickSite: isQuickSite
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (parameters)
				{
					if (parameters.data.config.allowPreview)
					{
						if (parameters.data.config.enableLogging)
							$.SalesPortal.LogHelper.write({
								type: 'Link',
								subType: 'Context Menu',
								data: {
									Name: parameters.data.name,
									File: parameters.data.fileName,
									'Original Format': parameters.format
								}
							});
						if (parameters.content != '')
						{
							var menu = $(parameters.content);
							$('body').append(menu);

							if (parameters.data.config.enableLogging)
							{
								var formLogger = new $.SalesPortal.FormLogger();
								formLogger.init({
									logObject: {
										name: parameters.data.name,
										fileName: parameters.data.fileName,
										format: parameters.format
									},
									formContent: menu
								});
							}

							menu
								.show()
								.css({
									position: "absolute",
									left: getMenuPosition(menu, pointX, 'width', 'scrollLeft'),
									top: getMenuPosition(menu, pointY, 'height', 'scrollTop')
								})
								.off('click')
								.on('click', 'a', function ()
								{
									menu.hide();

									var tag = $(this).find('.service-data .tag').text();
									switch (tag)
									{
										case 'open':
											that.requestViewDialog(linkId, isQuickSite);
											break;
										case 'download':
											that.downloadFile({
												name: parameters.data.fileName,
												path: parameters.data.filePath
											});
											break;
										case 'linkcart':
											$.SalesPortal.QBuilder.LinkCart.addLinks([parameters.data.linkId]);
											break;
										case 'quicksite':
											that.requestEmailDialog(linkId);
											break;
										case 'favorites':
											that.addToFavorites(
												parameters.data.linkId,
												parameters.data.name,
												parameters.data.fileName,
												parameters.data.format);
											break;
										case 'rate':
											that.requestRateDialog(parameters.data.linkId);
											//$.SalesPortal.QBuilder.LinkCart.addLinks([parameters.data.linkId]);
											break;
									}
								});
						}
					}
					else
					{
						var modalDialog = new $.SalesPortal.ModalDialog({
							title: '<span class="text-danger">Sorry...</span>',
							description: 'You are not authorized to view this link.',
							buttons: [
								{
									tag: 'close',
									title: 'Close',
									width: 80,
									clickHandler: function ()
									{
										modalDialog.close();
									}
								}
							],
							closeOnOutsideClick: true
						});
						modalDialog.show();
					}
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'json'
			});
		};

		this.requestRateDialog = function (linkId, afterClose)
		{
			$('body').find('.mtContent').remove();
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "preview/getRateDialog",
				data: {
					linkId: linkId
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (parameters)
				{
					if (parameters.data.config.allowPreview && parameters.data.config.enableRating)
					{
						if (parameters.data.config.enableLogging)
							$.SalesPortal.LogHelper.write({
								type: 'Link',
								subType: 'Preview Options',
								data: {
									Name: parameters.data.name,
									File: parameters.data.fileName,
									'Original Format': parameters.format
								}
							});

						$.fancybox({
							content: parameters.content,
							title: parameters.data.name,
							autoSize: true,
							openEffect: 'none',
							closeEffect: 'none',
							afterShow: function ()
							{
								var dialogContent = $('.fancybox-wrap');

								new $.SalesPortal.RateManager().init(
									{
										id: parameters.data.linkId,
										name: parameters.data.name,
										file: parameters.data.fileName,
										format: parameters.data.format
									},
									dialogContent.find('#user-link-rate-container'),
									parameters.data.rateData);
							},
							afterClose: afterClose
						});
					}
					else
					{
						var modalDialog = new $.SalesPortal.ModalDialog({
							title: '<span class="text-danger">Sorry...</span>',
							description: 'You are not authorized to rate this link.',
							buttons: [
								{
									tag: 'close',
									title: 'Close',
									width: 80,
									clickHandler: function ()
									{
										modalDialog.close();
									}
								}
							],
							closeOnOutsideClick: true
						});
						modalDialog.show();
					}
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'json'
			});
		};

		this.requestEmailDialog = function (linkId)
		{
			$('body').find('.mtContent').remove();
			that.cleanupContextMenu();
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "preview/getEmailDialog",
				data: {
					linkId: linkId
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (parameters)
				{
					var viewerData = new $.SalesPortal.SimpleViewerData(parameters.data);
					$.fancybox({
						content: parameters.content,
						title: parameters.data.name,
						width: 800,
						autoSize: false,
						autoHeight: true,
						openEffect: 'none',
						closeEffect: 'none',
						afterShow: function ()
						{
							var dialogContent = $('.fancybox-wrap');

							if (parameters.data.config.enableLogging)
							{
								var formLogger = new $.SalesPortal.FormLogger();
								formLogger.init({
									logObject: {
										name: viewerData.name,
										fileName: viewerData.fileName,
										format: viewerData.format
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

							new $.SalesPortal.PreviewEmailer(viewerData, false);
							new $.SalesPortal.PreviewEmailer(viewerData, true);
						}
					});
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'json'
			});
		};

		this.openFile = function (url)
		{
			window.open(url.replace(/&amp;/g, '%26'));
		};

		this.downloadFile = function (fileData)
		{
			window.location = window.BaseUrl + "preview/downloadFile?data=" + $.toJSON(fileData).replace(/&/g, '%26');
		};

		var favoritesDialogObject = [];
		this.addToFavorites = function (linkId, title, fileName, fileType)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "favorites/addLinkDialog",
				data: {
					linkId: linkId
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					favoritesDialogObject.content = $(msg);

					$.SalesPortal.LogHelper.write({
						type: 'Link',
						subType: 'Favorites Activity',
						data: {
							Name: title,
							File: fileName,
							'Original Format': fileType
						}
					});

					var formLogger = new $.SalesPortal.FormLogger();
					formLogger.init({
						logObject: {
							name: title,
							fileName: fileName,
							format: fileType
						},
						formContent: favoritesDialogObject.content
					});

					favoritesDialogObject.mainView = favoritesDialogObject.content.find('.main-view');
					favoritesDialogObject.mainView.find('.dropdown .dropdown-toggle').dropdown();
					favoritesDialogObject.mainView.find('#show-folder-selector').on('click', function ()
					{
						if (!$(this).hasClass('disabled'))
						{
							favoritesDialogObject.mainView.hide();
							favoritesDialogObject.folderSelector.show();
						}
					});
					favoritesDialogObject.mainView.find('#clear-folder').on('click', function ()
					{
						favoritesDialogObject.mainView.find('#favorites-folder-name').val('');
					});
					favoritesDialogObject.mainView.find('.btn.cancel-button').on('click', function ()
					{
						$.fancybox.close();
					});
					favoritesDialogObject.mainView.find('.btn.accept-button').on('click', function ()
					{
						$.fancybox.close();
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "favorites/addLink",
							data: {
								linkId: linkId,
								linkName: favoritesDialogObject.mainView.find('#favorites-link-name').val(),
								folderName: favoritesDialogObject.mainView.find('#favorites-folder-name').val()
							},
							beforeSend: function ()
							{
								$.SalesPortal.Overlay.show(false);
							},
							complete: function ()
							{
								$.SalesPortal.Overlay.hide();
							},
							success: function (msg)
							{
								favoritesDialogObject.content = $(msg);
								favoritesDialogObject.content.find('.accept-button').on('click', function ()
								{
									$.fancybox.close();
								});
								$.fancybox({
									content: favoritesDialogObject.content,
									title: title,
									openEffect: 'none',
									closeEffect: 'none'
								});
							},
							error: function ()
							{
							},
							async: true,
							dataType: 'html'
						});
					});

					favoritesDialogObject.folderSelector = favoritesDialogObject.content.find('.folder-selector');
					favoritesDialogObject.folderSelector.find('.btn.cancel-button').on('click', function ()
					{
						favoritesDialogObject.folderSelector.hide();
						favoritesDialogObject.mainView.show();
					});
					favoritesDialogObject.folderSelector.find('.btn.accept-button').on('click', function ()
					{
						favoritesDialogObject.mainView.find('#favorites-folder-name').val(favoritesDialogObject.folderSelector.find('li.active').find('a').html());
						favoritesDialogObject.folderSelector.hide();
						favoritesDialogObject.mainView.show();
					});
					favoritesDialogObject.folderSelector.find('li')
						.on('click', function ()
						{
							favoritesDialogObject.folderSelector.find('li').removeClass('active');
							$(this).addClass('active');
						})
						.on('dblclick', function ()
						{
							favoritesDialogObject.folderSelector.find('li').removeClass('active');
							$(this).addClass('active');
							favoritesDialogObject.folderSelector.find('.btn.accept-button').click();
						});
					$.fancybox({
						content: favoritesDialogObject.content,
						title: title,
						width: 370,
						height: 270,
						scrolling: 'no',
						autoSize: false,
						openEffect: 'none',
						closeEffect: 'none'
					});
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		this.playVideo = function (links, viewerBar, callbackAfterShow, callbackAfterClose)
		{
			$.fancybox({
				title: links[0].title,
				content: $('<video controls autoplay preload="auto"' +
					' id="video-player"' +
					' height = "480" width="680">' +
					'<source src="' + links[0].href + '" type="video/mp4">' +
					'</video>'),
				openEffect: 'none',
				closeEffect: 'none',
				afterShow: function ()
				{
					$('.fancybox-wrap').addClass('content-boxed');
					if (callbackAfterShow !== undefined)
						callbackAfterShow();
				},
				afterClose: function ()
				{
					if (viewerBar != undefined)
						viewerBar.close();
					$('#video-player').remove();
					if (callbackAfterClose !== undefined)
						callbackAfterClose();
				}
			});
		};

		this.cleanupContextMenu = function ()
		{
			var body = $('body');
			body.find('.context-menu-content').remove();
		};

		var getMenuPosition = function (menuObject, mouse, direction, scrollDir)
		{
			var win = $(window)[direction](),
				scroll = $(window)[scrollDir](),
				menu = menuObject[direction](),
				position = mouse + scroll;

			// opening menu would pass the side of the page
			if (mouse + menu > win && menu < mouse)
				position -= menu;

			return position;
		}
	};


	$.SalesPortal.LinkManager = new LinkManager();

	$(document).one('ready', function ()
	{
		$('body')
			.on('click', function ()
			{
				$.SalesPortal.LinkManager.cleanupContextMenu();
			})
			.on('contextmenu', function ()
			{
				$.SalesPortal.LinkManager.cleanupContextMenu();
			});
	});
})(jQuery);