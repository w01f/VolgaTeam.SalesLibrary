(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var LinkManager = function ()
	{
		var that = this;
		this.requestViewDialog = function (linkId, isAttachment)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "preview/getViewDialog",
				data: {
					linkId: linkId,
					isAttachment: isAttachment
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
					that.showViewDialog(msg);
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'json'
			});
		};

		this.showViewDialog = function (dialogData)
		{
			var content = $(dialogData.html);
			var formatItems = content.find('.format-item');
			var warning = content.find('.warning');
			var fullScreenSelector = content.find('.use-fullscreen');
			if (formatItems.length > 1 || warning.length)
			{
				var selectedLinkId = $(formatItems[0]).find('.service-data .link-id').html();
				var selectedFileFormat = $(formatItems[0]).find('.service-data .file-type').html();
				var tags = $(formatItems[0]).find('.service-data .tags').html();
				formatItems.tooltip({animation: false, trigger: 'hover', delay: { show: 500, hide: 100 }});
				formatItems.off('click').on('click', function ()
				{
					$(this).tooltip('hide');
					that.viewSelectedFormat($(this), fullScreenSelector.is(':checked'), false);
				});

				var modalTitle = 'How Do you want to Open this File?';
				if (tags != '')
					modalTitle = tags;
				else if (selectedFileFormat == 'url')
					modalTitle = 'Web Hyperlink Options';

				$.fancybox({
					content: content,
					title: modalTitle,
					width: 490,
					autoSize: false,
					autoHeight: true,
					openEffect: 'none',
					closeEffect: 'none',
					beforeShow: function ()
					{
						$.SalesPortal.Rate.init(selectedLinkId, $('.fancybox-wrap'), dialogData.rateData);
					}
				});

				content.find('.tooltip').remove();
				$.SalesPortal.ViewDialogBar.backToConent = content;

			}
			else
			{
				that.viewSelectedFormat(formatItems[0], formatItems[0], false);
			}
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

		this.openFileCard = function (linkId)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "preview/getFileCard",
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
					var fileCardContent = $(msg);
					$.fancybox({
						content: fileCardContent,
						title: 'File Card',
						minWidth: 400,
						openEffect: 'none',
						closeEffect: 'none'
					});
				},
				async: true,
				dataType: 'html'
			});
		};

		this.viewSelectedFormat = function (target, fullScreen, isHelp)
		{
			var selectedFileId = $(target).find('.service-data .link-id').html();
			var selectedLinkName = $(target).find('.service-data .link-name').html();
			var selectedFileName = $(target).find('.service-data .file-name').html();
			var selectedFileType = $(target).find('.service-data .file-type').html();
			var selectedViewType = $(target).find('.service-data .view-type').html();
			var selectedLinks = $(target).find('.service-data .links').html();
			var selectedThumbs = $(target).find('.service-data .thumbs').html();

			$.fancybox.close();

			if (selectedFileType != '' && selectedViewType != '')
			{
				if (selectedLinks != undefined)
					selectedLinks = $.parseJSON(selectedLinks);
				if (selectedThumbs != undefined)
					selectedThumbs = $.parseJSON(selectedThumbs);
				switch (selectedFileType)
				{
					case 'ppt':
					case 'doc':
					case 'pdf':
						switch (selectedViewType)
						{
							case 'png':
							case 'jpeg':
								if (fullScreen)
								{
									$.ajax({
										type: "POST",
										url: window.BaseUrl + "statistic/writeActivity",
										data: {
											type: 'Link',
											subType: 'Preview',
											data: $.toJSON({
												Name: selectedLinkName,
												File: selectedFileName,
												'Original Format': selectedFileType,
												Format: selectedViewType,
												Mode: 'Fullscreen'
											})
										},
										async: true,
										dataType: 'html'
									});
									window.open("preview/runFullscreenGallery?linkId=" + selectedFileId + "&format=" + selectedViewType);
								}
								else
								{
									$.fancybox(selectedLinks, {
										openEffect: 'none',
										closeEffect: 'none',
										afterClose: function ()
										{
											if (selectedFileType != 'pdf')
												$.SalesPortal.ViewDialogBar.close();
										},
										onUpdate: function ()
										{
											if (selectedFileType != 'pdf')
												$.SalesPortal.ViewDialogBar.resize();
											$.ajax({
												type: "POST",
												url: window.BaseUrl + "statistic/writeActivity",
												data: {
													type: 'Link',
													subType: 'Preview Page',
													data: $.toJSON({
														Name: selectedLinkName,
														File: selectedFileName,
														'Original Format': selectedFileType,
														Format: selectedViewType,
														Mode: 'Modal'
													})
												},
												async: true,
												dataType: 'html'
											});
										},
										helpers: {
											thumbs: {
												height: selectedThumbs[0].height,
												width: selectedThumbs[0].width
											}
										}
									});
									if (selectedFileType != 'pdf')
									{
										$.SalesPortal.ViewDialogBar.show({
											format: selectedFileType,
											linkId: selectedFileId,
											linkName: selectedLinkName,
											fileName: selectedFileName,
											fileType: selectedFileType
										});
									}
								}
								break;
							case 'email':
								emailFile(selectedFileId, null, null, selectedLinks[0].title);
								break;
							case 'outlook':
								$.SalesPortal.QBuilder.PageList.addLitePage(selectedFileId, selectedLinkName, selectedFileName, selectedFileType);
								break;
							case 'favorites':
								addToFavorites(selectedFileId, selectedLinkName, selectedFileName, selectedFileType);
								break;
							default:
								$.ajax({
									type: "POST",
									url: window.BaseUrl + "statistic/writeActivity",
									data: {
										type: isHelp ? 'Help Link' : 'Link',
										subType: 'Open',
										data: $.toJSON({
											Name: selectedLinkName,
											File: selectedFileName,
											'Original Format': selectedFileType,
											Format: selectedViewType
										})
									},
									async: true,
									dataType: 'html'
								});
								openFile(selectedLinks[0].href);
								break;
						}
						break;
					case 'xls':
						switch (selectedViewType)
						{
							case 'email':
								emailFile(selectedFileId, null, null, selectedLinks[0].title);
								break;
							case 'outlook':
								$.SalesPortal.QBuilder.PageList.addLitePage(selectedFileId, selectedLinkName, selectedFileName, selectedFileType);
								break;
							case 'favorites':
								addToFavorites(selectedFileId, selectedLinkName, selectedFileName, selectedFileType);
								break;
							default:
								$.ajax({
									type: "POST",
									url: window.BaseUrl + "statistic/writeActivity",
									data: {
										type: isHelp ? 'Help Link' : 'Link',
										subType: 'Open',
										data: $.toJSON({
											Name: selectedLinkName,
											File: selectedFileName,
											'Original Format': selectedFileType,
											Format: selectedFileType != selectedViewType ? selectedViewType : null
										})
									},
									async: true,
									dataType: 'html'
								});
								openFile(selectedLinks[0].href);
								break;
						}
						break;
					case 'key':
					case 'url':
					case 'other':
						switch (selectedViewType)
						{
							case 'email':
								emailFile(selectedFileId, null, null, selectedLinks[0].title);
								break;
							case 'outlook':
								$.SalesPortal.QBuilder.PageList.addLitePage(selectedFileId, selectedLinkName, selectedFileName, selectedFileType);
								break;
							case 'favorites':
								addToFavorites(selectedFileId, selectedLinkName, selectedFileName, selectedFileType);
								break;
							default:
								$.ajax({
									type: "POST",
									url: window.BaseUrl + "statistic/writeActivity",
									data: {
										type: isHelp ? 'Help Link' : 'Link',
										subType: 'Open',
										data: $.toJSON({
											Name: selectedLinkName,
											File: selectedFileName,
											'Original Format': selectedFileType
										})
									},
									async: true,
									dataType: 'html'
								});
								openFile(selectedLinks[0].href);
								break;
						}
						break;
					case 'png':
					case 'jpeg':
						switch (selectedViewType)
						{
							case 'email':
								emailFile(selectedFileId, null, null, selectedLinks[0].title);
								break;
							case 'outlook':
								$.SalesPortal.QBuilder.PageList.addLitePage(selectedFileId, selectedLinkName, selectedFileName, selectedFileType);
								break;
							case 'download':
								downloadFile(selectedFileId, selectedLinkName, selectedFileType);
								break;
							case 'favorites':
								addToFavorites(selectedFileId, selectedLinkName, selectedFileName, selectedFileType);
								break;
							default:
								selectedLinks[0].href = selectedLinks[0].href.replace(/&amp;/g, '%26');
								$.ajax({
									type: "POST",
									url: window.BaseUrl + "statistic/writeActivity",
									data: {
										type: 'Link',
										title: selectedLinkName,
										subType: 'Preview',
										data: $.toJSON({
											Name: selectedLinkName,
											File: selectedFileName,
											'Original Format': selectedFileType
										})
									},
									async: true,
									dataType: 'html'
								});
								$.fancybox(selectedLinks, {
									openEffect: 'none',
									closeEffect: 'none'
								});
								break;
						}
						break;
					case 'mp4':
					case 'wmv':
					case 'video':
						switch (selectedViewType)
						{
							case 'video':
							case 'tab':
							case 'ogv':
							case 'lp':
								$.ajax({
									type: "POST",
									url: window.BaseUrl + "statistic/writeActivity",
									data: {
										type: isHelp ? 'Help Link' : 'Link',
										subType: 'Play Video',
										data: $.toJSON({
											Name: selectedLinkName,
											File: decodeURI(selectedLinks[0].src.substr(selectedLinks[0].src.lastIndexOf('/') + 1)),
											'Original Format': selectedFileType,
											Format: selectedFileType != selectedViewType ? selectedViewType : null
										})
									},
									async: true,
									dataType: 'html'
								});
								openFile(selectedLinks[0].href);
								break;
							case 'email':
								emailFile(selectedFileId, null, null, selectedLinks[0].title);
								break;
							case 'outlook':
								$.SalesPortal.QBuilder.PageList.addLitePage(selectedFileId, selectedLinkName, selectedFileName, selectedFileType);
								break;
							case 'download':
								downloadFile(selectedFileId, selectedLinks[0].title, selectedFileType);
								break;
							case 'favorites':
								addToFavorites(selectedFileId, selectedLinkName, selectedFileName, selectedFileType);
								break;
							case 'mp4':
								$.ajax({
									type: "POST",
									url: window.BaseUrl + "statistic/writeActivity",
									data: {
										type: isHelp ? 'Help Link' : 'Link',
										subType: 'Play Video',
										data: $.toJSON({
											Name: selectedLinkName,
											File: decodeURI(selectedLinks[0].src.substr(selectedLinks[0].src.lastIndexOf('/') + 1)),
											'Original Format': selectedFileType,
											Format: selectedFileType != selectedViewType ? selectedViewType : null
										})
									},
									async: true,
									dataType: 'html'
								});
								playVideo(selectedLinks);
								break;
						}
						break;
				}
			}
		};

		var openFile = function (url)
		{
			window.open(url.replace(/&amp;/g, '%26'));
		};

		var downloadFile = function (linkId, title, format)
		{
			if (format == 'mp4' || format == 'wmv' || format == 'video')
			{
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "site/downloadDialog",
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
						var content = $(msg);
						content.find(".download-type button").on('click', function ()
						{
							if (!$(this).hasClass('active'))
							{
								$('.download-type button').removeClass('active').blur();
								$(this).addClass('active');
							}
						});
						content.find('#cancel-button').on('click', function ()
						{
							$.fancybox.close();
						});
						content.find('#accept-button').on('click', function ()
						{
							window.open("site/downloadFile?linkId=" + linkId + "&format=" + $('.download-type button.active img').attr('alt'));
							$.fancybox.close();
						});
						$.fancybox({
							content: content,
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
			}
			else
				window.open("site/downloadFile?linkId=" + linkId + "&format=" + format);
		};

		var favoritesDialogObject = [];
		var addToFavorites = function (linkId, title, fileName, fileType)
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

		var emailDialogObject = [];
		var emailFile = function (linkId, partId, partFormat, title)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "site/emailLinkDialog",
				data: {
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
					emailDialogObject.content = $(msg);
					emailDialogObject.content.find('.dropdown .dropdown-toggle').dropdown();

					emailDialogObject.content.find('.dropdown .dropdown-menu li').on('click', function (event)
					{
						event.stopPropagation();
					});
					emailDialogObject.content.find('.dropdown .dropdown-menu li').on('touchstart', function (event)
					{
						event.stopPropagation();
					});

					emailDialogObject.content.find('.dropdown .dropdown-toggle').on('click', function (event)
					{
						event.stopPropagation();
					});
					emailDialogObject.content.find('.dropdown .dropdown-toggle').on('touchstart', function (event)
					{
						event.stopPropagation();
					});

					emailDialogObject.toSelector = emailDialogObject.content.find('#email-to-select');
					emailDialogObject.toSelector.find('button.apply-selection').on('click', function (event)
					{
						var selectedEmails = [];
						$.each(emailDialogObject.toSelector.find(':checked'), function ()
						{
							selectedEmails.push($(this).val());
						});
						if (selectedEmails.length > 0)
							emailDialogObject.content.find('#email-to').val(selectedEmails.join('; '));
						else
							emailDialogObject.content.find('#email-to').val('');

						$(this).parent().dropdown('toggle');
						event.stopPropagation();
					});
					emailDialogObject.toSelector.find('button.apply-selection').on('touchstart',function (event)
					{
						var selectedEmails = [];
						$.each(emailDialogObject.toSelector.find(':checked'), function ()
						{
							selectedEmails.push($(this).val());
						});
						if (selectedEmails.length > 0)
							emailDialogObject.content.find('#email-to').val(selectedEmails.join('; '));
						else
							emailDialogObject.content.find('#email-to').val('');

						$(this).parent().dropdown('toggle');
						event.stopPropagation();
						event.preventDefault();
					}).on('touchend', function (event)
					{
						event.stopPropagation();
						event.preventDefault();
					});

					emailDialogObject.toCopySelector = emailDialogObject.content.find('#email-to-copy-select');
					emailDialogObject.toCopySelector.find('button.apply-selection').on('click', function (event)
					{
						var selectedEmails = [];
						$.each(emailDialogObject.toCopySelector.find(':checked'), function ()
						{
							selectedEmails.push($(this).val());
						});
						if (selectedEmails.length > 0)
							emailDialogObject.content.find('#email-to-copy').val(selectedEmails.join('; '));
						else
							emailDialogObject.content.find('#email-to-copy').val('');

						$(this).parent().dropdown('toggle');
						event.stopPropagation();
					});
					emailDialogObject.toCopySelector.find('button.apply-selection').on('touchstart',function (event)
					{
						var selectedEmails = [];
						$.each(emailDialogObject.toCopySelector.find(':checked'), function ()
						{
							selectedEmails.push($(this).val());
						});
						if (selectedEmails.length > 0)
							emailDialogObject.content.find('#email-to-copy').val(selectedEmails.join('; '));
						else
							emailDialogObject.content.find('#email-to-copy').val('');

						$(this).parent().dropdown('toggle');
						event.stopPropagation();
						event.preventDefault();
					}).on('touchend', function (event)
					{
						event.stopPropagation();
						event.preventDefault();
					});

					emailDialogObject.content.find('#accept-button').on('click', function ()
					{
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "site/emailLinkSend",
							data: {
								linkId: linkId,
								partId: partId,
								partFormat: partFormat,
								emailTo: emailDialogObject.content.find('#email-to').val(),
								emailCopyTo: emailDialogObject.content.find('#email-to-copy').val(),
								emailFrom: emailDialogObject.content.find('#email-from').val(),
								emailToMe: emailDialogObject.content.find('#email-to-me').is(':checked'),
								emailSubject: emailDialogObject.content.find('#email-subject').val(),
								emailBody: emailDialogObject.content.find('#email-body').val(),
								expiresIn: emailDialogObject.content.find('#expires-in').val()
							},
							success: function ()
							{
								$.fancybox.close();
								$.ajax({
									type: "POST",
									url: window.BaseUrl + "site/emailLinkSuccess",
									data: {},
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
										var content = $(msg);
										content.find('#accept-button').off('click');
										content.find('#accept-button').on('click', function ()
										{
											$.fancybox.close();
										});
										$.fancybox({
											content: content,
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
							},
							complete: function ()
							{
								$.fancybox.close();
							},
							async: true,
							dataType: 'html'
						});
					});
					emailDialogObject.content.find('#cancel-button').on('click', function ()
					{
						$.fancybox.close();
					});
					$.fancybox({
						content: emailDialogObject.content,
						title: title,
						width: 470,
						scrolling: 'no',
						autoSize: false,
						autoHeight: true,
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

		var playVideo = function (links)
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

