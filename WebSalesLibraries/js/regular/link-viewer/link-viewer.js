(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	var LinkManager = function ()
	{
		var that = this;

		this.requestViewDialog = function (requestData)
		{
			if (requestData === undefined)
				requestData = {
					linkId: undefined,
					isQuickSite: undefined,
					viewContainer: undefined,
					parentPreviewParameters: undefined,
					savedState: undefined,
					afterViewerOpenedCallback: undefined
				};
			that.cleanupContextMenu();
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "preview/getViewDialog",
				data: {
					linkId: requestData.linkId,
					parentBundleId: requestData.parentPreviewParameters !== undefined ? requestData.parentPreviewParameters.data.linkId : undefined,
					isQuickSite: requestData.isQuickSite,
					screenSettings: $.SalesPortal.ScreenManager.getScreenSettings()
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show();
				},
				success: function (parameters)
				{
					$.SalesPortal.Overlay.hide();

					var previewParameters = parameters;
					previewParameters.viewContainer = requestData.viewContainer;
					previewParameters.afterViewerOpenedCallback = requestData.afterViewerOpenedCallback;

					previewParameters.data.doNotPushHistory = requestData.doNotPushHistory;

					if (requestData.parentPreviewParameters)
						previewParameters.parentPreviewParameters = requestData.parentPreviewParameters;
					else
						previewParameters.parentPreviewParameters = previewParameters;

					if (requestData.parentPreviewParameters && requestData.parentPreviewParameters.data && requestData.parentPreviewParameters.data.savedState)
						previewParameters.data.savedState = requestData.parentPreviewParameters.data.savedState;
					else
						previewParameters.data.savedState = requestData.savedState;

					that.openViewerDialog(previewParameters)
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'json'
			});
		};

		this.openViewerDialog = function (previewParameters)
		{
			var openedViewer = undefined;
			if (previewParameters.data.config.allowPreview)
			{
				if (previewParameters.data.config.enableLogging)
					$.SalesPortal.LogHelper.write({
						type: 'Link',
						subType: 'Preview Options',
						linkId: previewParameters.data.linkId,
						data: {
							name: previewParameters.data.name,
							file: previewParameters.data.fileName,
							originalFormat: previewParameters.format
						}
					});
				switch (previewParameters.format)
				{
					case 'document':
						openedViewer = new $.SalesPortal.DocumentViewer(previewParameters).show();
						break;
					case 'xls':
						openedViewer = new $.SalesPortal.ExcelViewer(previewParameters).show();
						break;
					case 'video':
						openedViewer = new $.SalesPortal.VideoViewer(previewParameters).show();
						break;
					case 'youtube':
						openedViewer = new $.SalesPortal.YouTubeViewer(previewParameters).show();
						break;
					case 'vimeo':
						openedViewer = new $.SalesPortal.VimeoViewer(previewParameters).show();
						break;
					case 'lan':
						openedViewer = new $.SalesPortal.LanViewer(previewParameters).show();
						break;
					case 'app':
						openedViewer = new $.SalesPortal.AppLinkViewer(previewParameters).show();
						break;
					case 'internal':
						openedViewer = new $.SalesPortal.InternalLinkViewer(previewParameters).show();
						break;
					case 'image':
						openedViewer = new $.SalesPortal.ImageViewer(previewParameters).show();
						break;
					case 'link bundle':
						openedViewer = new $.SalesPortal.LinkBundleViewer(previewParameters).show();
						break;
					default :
						openedViewer = new $.SalesPortal.FileViewer(previewParameters).show();
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

			if (previewParameters.afterViewerOpenedCallback !== undefined)
				previewParameters.afterViewerOpenedCallback(openedViewer);
		};

		this.requestLinkContextMenu = function (linkId, isQuickSite, pointX, pointY)
		{
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
					$.SalesPortal.Overlay.show();
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
								linkId: parameters.data.linkId,
								data: {
									name: parameters.data.name,
									file: parameters.data.fileName,
									originalFormat: parameters.format
								}
							});
						if (parameters.content !== '')
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
										format: parameters.format,
										linkId: parameters.data.linkId
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
								.on('click', 'a.regular-open', function ()
								{
									menu.hide();
									var tag = $(this).find('.service-data .tag').text();
									switch (tag)
									{
										case 'open':
											that.requestViewDialog({
												linkId: linkId,
												isQuickSite: isQuickSite
											});
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
										case 'linkcart-all-window':
											$.SalesPortal.QBuilder.LinkCart.addFolder(parameters.data.folderId);
											break;
										case 'quicksite':
											that.requestEmailDialog(linkId);
											break;
										case 'zip-folder':
											$.SalesPortal.ZipDownloadFilesHelper.processFolderLink(parameters.data);
											break;
										case 'zip-link-bundle':
											$.SalesPortal.ZipDownloadFilesHelper.processLinkBundle(parameters.data.linkId);
											break;
										case 'zip-library-folder':
											$.SalesPortal.ZipDownloadFilesHelper.processLibraryFolder(parameters.data.folderId);
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
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "preview/getRateDialog",
				data: {
					linkId: linkId
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show();
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
								linkId: parameters.data.linkId,
								data: {
									name: parameters.data.name,
									file: parameters.data.fileName,
									originalFormat: parameters.format
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
									dialogContent.find('.user-link-rate-container'),
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
			that.cleanupContextMenu();
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "preview/getEmailDialog",
				data: {
					linkId: linkId
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show();
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
			var form = document.getElementById('form-download-file');
			if (form === null)
			{
				form = document.createElement("form");
				form.setAttribute("id", "form-download-file");
				form.setAttribute("method", "post");
				form.setAttribute("action", window.BaseUrl + 'preview/downloadFile');
				form._submit_function_ = form.submit;

				var hiddenField = document.createElement("input");
				hiddenField.setAttribute("id", "input-file-data");
				hiddenField.setAttribute("type", "hidden");
				hiddenField.setAttribute("name", 'fileData');
				form.appendChild(hiddenField);
				document.body.appendChild(form);
			}
			document.getElementById('input-file-data').setAttribute("value", $.toJSON(fileData));
			form._submit_function_();
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
					$.SalesPortal.Overlay.show();
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
							name: title,
							file: fileName,
							originalFormat: fileType
						}
					});

					var formLogger = new $.SalesPortal.FormLogger();
					formLogger.init({
						logObject: {
							name: title,
							fileName: fileName,
							format: fileType,
							linkId: linkId
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
								$.SalesPortal.Overlay.show();
							},
							complete: function ()
							{
								$.SalesPortal.Overlay.hide();
							},
							success: function ()
							{
								var modalDialog = new $.SalesPortal.ModalDialog({
									title: 'SUCCESS!',
									description: fileName + ' was saved to your favorites...',
									buttons: [
										{
											tag: 'open_favorites',
											title: 'Open Favorites',
											width: 150,
											clickHandler: function ()
											{
												modalDialog.close();
												$.SalesPortal.ShortcutsManager.openStaticShortcutByType('favorites');
											}
										},
										{
											tag: 'close',
											title: 'Return to Site',
											width: 150,
											clickHandler: function ()
											{
												modalDialog.close();
											}
										}
									],
									closeOnOutsideClick: true
								});
								modalDialog.show();
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
					if (viewerBar !== undefined)
						viewerBar.close();
					$('#video-player').remove();
					if (callbackAfterClose !== undefined)
						callbackAfterClose();
				}
			});
		};

		this.playYouTube = function (title, youTubeId)
		{
			$.fancybox({
				title: title,
				content: '<iframe ' +
				'height = "480" width="680" frameborder="0" allowfullscreen ' +
				'src="https://www.youtube.com/embed/' + youTubeId + '?autoplay=1">' +
				'</iframe>',
				openEffect: 'none',
				closeEffect: 'none',
				afterShow: function ()
				{
					$('.fancybox-wrap').addClass('content-boxed');
				}
			});
		};

		this.playVimeo = function (title, playerUrl)
		{
			$.fancybox({
				title: title,
				content: '<iframe ' +
				'height = "480" width="680" frameborder="0" allowfullscreen ' +
				'src="' + playerUrl + '?autoplay=1">' +
				'</iframe>',
				openEffect: 'none',
				closeEffect: 'none',
				afterShow: function ()
				{
					$('.fancybox-wrap').addClass('content-boxed');
				}
			});
		};

		this.cleanupContextMenu = function ()
		{
			var body = $('body');
			body.find('.context-menu-content').remove();
		};

		this.copyTextToClipboard = function(text){
			var $temp = $("<input>");
			$("body").append($temp);
			$temp.val(text).select();
			document.execCommand("copy");
			$temp.remove();
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