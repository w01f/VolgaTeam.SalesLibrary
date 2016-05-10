(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var LinkManager = function ()
	{
		var that = this;
		this.requestViewDialog = function (linkId, isQuickSite)
		{
			$('body').find('.mtContent').remove();
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
							case 'xls':
								new $.SalesPortal.ExcelViewer(parameters).show();
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

		this.requestSpecialDialog = function (linkIds, folderId)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "preview/getSpecialDialog",
				data: {
					linkIds: linkIds,
					folderId: folderId
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
					if (msg != '')
					{
						var content = $(msg);
						content.find('#context-add').off('click').on('click', function ()
						{
							$.fancybox.close();
							if (linkIds != undefined)
								$.SalesPortal.QBuilder.LinkCart.addLinks(linkIds);
							else if (folderId != undefined)
								$.SalesPortal.QBuilder.LinkCart.addFolder(folderId);
						});
						$.fancybox({
							content: content,
							title: 'Advanced LINK Options',
							width: 490,
							autoSize: false,
							autoHeight: true,
							openEffect: 'none',
							closeEffect: 'none'
						});
					}
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
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

		this.openFile = function (url)
		{
			window.open(url.replace(/&amp;/g, '%26'));
		};

		this.downloadFile = function (fileData)
		{
			window.open(window.BaseUrl + "preview/downloadFile?data=" + $.toJSON(fileData).replace(/&/g, '%26'));
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
					' height = "480" width="680">'+
						'<source src="'+links[0].href+'" type="video/mp4">' +
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
	};
	$.SalesPortal.LinkManager = new LinkManager();
})(jQuery);