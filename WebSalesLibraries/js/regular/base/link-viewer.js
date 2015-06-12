(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var LinkManager = function ()
	{
		var that = this;
		this.requestViewDialog = function (linkId, isQuickSite)
		{
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
					switch (parameters.format)
					{
						case 'document':
							new $.SalesPortal.DocumentViewer(parameters).show();
							break;
						case 'video':
							new $.SalesPortal.VideoViewer(parameters).show();
							break;
						case 'image':
							new $.SalesPortal.ImageViewer(parameters).show();
							break;
						default :
							new $.SalesPortal.FileViewer(parameters).show();
							break;
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
						that.showSpecialDialog(content, linkIds, folderId);
					}
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		this.showSpecialDialog = function (content, linkIds, folderId)
		{
			var singleLinkId = linkIds != undefined && linkIds.length > 0 ? linkIds[0] : undefined;
			var linkName = content.find('.object-name').text();
			var fileName = content.find('.object-file-name').text();
			var fileType = content.find('.object-file-type').text();
			content.find('#context-add').off('click').on('click', function ()
			{
				$.fancybox.close();
				if (linkIds != undefined)
					$.SalesPortal.QBuilder.LinkCart.addLinks(linkIds);
				else if (folderId != undefined)
					$.SalesPortal.QBuilder.LinkCart.addFolder(folderId);
			});
			content.find('#context-email').off('click').on('click', function ()
			{
				$.fancybox.close();
				$.SalesPortal.QBuilder.PageList.addLitePage(singleLinkId, linkName, fileName, fileType);
			});
			content.find('#context-manager').off('click').on('click', function ()
			{
				$.fancybox.close();
			});
			content.find('.accept-button').off('click').on('click', function ()
			{
				$.fancybox.close();
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
		};

		this.openFile = function (url)
		{
			window.open(url.replace(/&amp;/g, '%26'));
		};

		this.downloadFile = function (fileData)
		{
			window.open("preview/downloadFile?data=" + $.toJSON(fileData).replace(/&/g, '%26'));
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
					favoritesDialogObject.mainView = favoritesDialogObject.content.find('.main-view');
					favoritesDialogObject.mainView.find('.dropdown .dropdown-toggle').dropdown();
					favoritesDialogObject.mainView.find('#show-folder-selector').on('click', function ()
					{
						if (!$(this).hasClass('disabled'))
						{
							favoritesDialogObject.mainView.hide();
							favoritesDialogObject.folderSelector.show();
							$.ajax({
								type: "POST",
								url: window.BaseUrl + "statistic/writeActivity",
								data: {
									type: 'Link',
									subType: 'Favorites Activity',
									data: $.toJSON({
										Name: title,
										File: fileName,
										'Original Format': fileType
									})
								},
								async: true,
								dataType: 'html'
							});
						}
					});
					favoritesDialogObject.mainView.find('#clear-folder').on('click', function ()
					{
						favoritesDialogObject.mainView.find('#favorites-folder-name').val('');
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "statistic/writeActivity",
							data: {
								type: 'Link',
								subType: 'Favorites Activity',
								data: $.toJSON({
									Name: title,
									File: fileName,
									'Original Format': fileType
								})
							},
							async: true,
							dataType: 'html'
						});
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
								favoritesDialogObject.content.find('.favorites-button').on('click', function ()
								{
									$.cookie("selectedRibbonTabId", 'favorites-tab', {
										expires: (60 * 60 * 24 * 7)
									});
									location.reload();
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
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "statistic/writeActivity",
							data: {
								type: 'Link',
								subType: 'Favorites Activity',
								data: $.toJSON({
									Name: title,
									File: fileName,
									'Original Format': fileType
								})
							},
							async: true,
							dataType: 'html'
						});
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
							$.ajax({
								type: "POST",
								url: window.BaseUrl + "statistic/writeActivity",
								data: {
									type: 'Link',
									subType: 'Favorites Activity',
									data: $.toJSON({
										Name: title,
										File: fileName,
										'Original Format': fileType
									})
								},
								async: true,
								dataType: 'html'
							});
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

		this.playVideo = function (links, viewerBar)
		{
			VideoJS.players = {};
			$.fancybox({
				title: links[0].title,
				content: $('<video id="video-player" class="video-js vjs-default-skin" height = "480" width="640"></video>'),
				openEffect: 'none',
				closeEffect: 'none',
				afterShow: function ()
				{
					$('.fancybox-wrap').addClass('content-boxed');
				},
				afterClose: function ()
				{
					if (viewerBar != undefined)
						viewerBar.close();
					$('#video-player').remove();
				}
			});
			_V_.options.flash.swf = links[0].swf;
			var myPlayer = _V_("video-player", {
					controls: true,
					autoplay: false,
					preload: 'auto',
					width: 640,
					height: 480,
					poster: '//:0'
				},
				function ()
				{
				});
			myPlayer.src(links);
			myPlayer.play();
		};
	};
	$.SalesPortal.LinkManager = new LinkManager();
})(jQuery);