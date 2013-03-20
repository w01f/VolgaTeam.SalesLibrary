(function ($)
{
	$.openViewDialog = function (linkId, isAttachment)
	{
		$.ajax({
			type:"POST",
			url:"preview/getViewDialog",
			data:{
				linkId:linkId,
				isAttachment:isAttachment
			},
			beforeSend:function ()
			{
				$.showOverlayLight();
			},
			complete:function ()
			{
				$.hideOverlayLight();
			},
			success:function (msg)
			{
				var content = $(msg);
				var formatItems = content.find('li');
				var warning = content.find('.warning');
				var fullScreenSelector = content.find('.use-fullscreen');
				if (formatItems.length > 1 || warning.length)
				{
					var selectedLinkName = $(formatItems[0]).find('.service-data .link-name').html();
					var selectedFileName = $(formatItems[0]).find('.service-data .file-name').html();
					formatItems.tooltip({animation:false, trigger:'hover', delay:{ show:500, hide:100 }});
					formatItems.off('click').on('click', function ()
					{
						$.viewSelectedFormat($(this), fullScreenSelector.is(':checked'), false);
					});
					var viewDialogContent = content.find('.view-dialog-content').html();
					$.fancybox({
						content:content,
						title:'How Do you want to Open this File?',
						width:490,
						autoSize:false,
						autoHeight:true,
						openEffect:'none',
						closeEffect:'none'
					});
				}
				else
				{
					$.viewSelectedFormat(formatItems[0], formatItems[0], false, false);
				}
			},
			error:function ()
			{
				$('#search-result').html('');
			},
			async:true,
			dataType:'html'
		});
	};

	$.openFileCard = function (linkId)
	{
		$.ajax({
			type:"POST",
			url:"preview/getFileCard",
			data:{
				linkId:linkId
			},
			beforeSend:function ()
			{
				$.showOverlayLight();
			},
			complete:function ()
			{
				$.hideOverlayLight();
			},
			success:function (msg)
			{
				var fileCardContent = $(msg);
				$.fancybox({
					content:fileCardContent,
					title:'File Card',
					minWidth:400,
					openEffect:'none',
					closeEffect:'none'
				});
			},
			async:true,
			dataType:'html'
		});
	};

	var openFile = function (url)
	{
		window.open(url.replace(/&amp;/g, '%26'));
	};

	var downloadFile = function (linkId, title)
	{
		$.ajax({
			type:"POST",
			url:"site/downloadDialog",
			data:{
				linkId:linkId
			},
			beforeSend:function ()
			{
				$.showOverlayLight();
			},
			complete:function ()
			{
				$.hideOverlayLight();
			},
			success:function (msg)
			{
				var content = $(msg);
				content.find(".download-type button").on('click', function ()
				{
					if (!$(this).hasClass('active'))
					{
						$('.download-type button').removeClass('active');
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
					content:content,
					title:title,
					openEffect:'none',
					closeEffect:'none'
				});
			},
			error:function ()
			{
			},
			async:true,
			dataType:'html'
		});
	};

	var favoritesDialogObject = [];
	var addToFavorites = function (linkId, title)
	{
		$.ajax({
			type:"POST",
			url:"favorites/addLinkDialog",
			data:{
				linkId:linkId
			},
			beforeSend:function ()
			{
				$.showOverlayLight();
			},
			complete:function ()
			{
				$.hideOverlayLight();
			},
			success:function (msg)
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
						type:"POST",
						url:"favorites/addLink",
						data:{
							linkId:linkId,
							linkName:favoritesDialogObject.mainView.find('#favorites-link-name').val(),
							folderName:favoritesDialogObject.mainView.find('#favorites-folder-name').val()
						},
						beforeSend:function ()
						{
							$.showOverlayLight();
						},
						complete:function ()
						{
							$.hideOverlayLight();
						},
						success:function (msg)
						{
							favoritesDialogObject.content = $(msg);
							favoritesDialogObject.content.find('.accept-button').on('click', function ()
							{
								$.fancybox.close();
							});
							favoritesDialogObject.content.find('.favorites-button').on('click', function ()
							{
								$.cookie("selectedRibbonTabId", 'favorites-tab', {
									expires:(60 * 60 * 24 * 7)
								});
								location.reload();
							});
							$.fancybox({
								content:favoritesDialogObject.content,
								title:title,
								openEffect:'none',
								closeEffect:'none'
							});
						},
						error:function ()
						{
						},
						async:true,
						dataType:'html'
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
					content:favoritesDialogObject.content,
					title:title,
					width:370,
					height:270,
					scrolling:'no',
					autoSize:false,
					openEffect:'none',
					closeEffect:'none'
				});
			},
			error:function ()
			{
			},
			async:true,
			dataType:'html'
		});
	};

	var emailDialogObject = [];
	var emailFile = function (linkId, title)
	{
		$.ajax({
			type:"POST",
			url:"site/emailLinkDialog",
			data:{
			},
			beforeSend:function ()
			{
				$.showOverlayLight();
			},
			complete:function ()
			{
				$.hideOverlayLight();
			},
			success:function (msg)
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
					$(this).parent.dropdown('toggle');
					event.stopPropagation();
				});
				emailDialogObject.content.find('.dropdown .dropdown-toggle').on('touchstart', function (event)
				{
					$(this).parent.dropdown('toggle');
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

					$(this).parent.dropdown('toggle');
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

					$(this).parent.dropdown('toggle');
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

					$(this).parent.dropdown('toggle');
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

					$(this).parent.dropdown('toggle');
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
						type:"POST",
						url:"site/emailLinkSend",
						data:{
							linkId:linkId,
							emailTo:emailDialogObject.content.find('#email-to').val(),
							emailCopyTo:emailDialogObject.content.find('#email-to-copy').val(),
							emailFrom:emailDialogObject.content.find('#email-from').val(),
							emailToMe:emailDialogObject.content.find('#email-to-me').is(':checked'),
							emailSubject:emailDialogObject.content.find('#email-subject').val(),
							emailBody:emailDialogObject.content.find('#email-body').val(),
							expiresIn:emailDialogObject.content.find('#expires-in').val()
						},
						success:function ()
						{
							$.fancybox.close();
							$.ajax({
								type:"POST",
								url:"site/emailLinkSuccess",
								data:{},
								beforeSend:function ()
								{
									$.showOverlayLight();
								},
								complete:function ()
								{
									$.hideOverlayLight();
								},
								success:function (msg)
								{
									var content = $(msg);
									content.find('#accept-button').off('click');
									content.find('#accept-button').on('click', function ()
									{
										$.fancybox.close();
									});
									$.fancybox({
										content:content,
										title:title,
										openEffect:'none',
										closeEffect:'none'
									});
								},
								error:function ()
								{
								},
								async:true,
								dataType:'html'
							});
						},
						complete:function ()
						{
							$.fancybox.close();
						},
						async:true,
						dataType:'html'
					});
				});
				emailDialogObject.content.find('#cancel-button').on('click', function ()
				{
					$.fancybox.close();
				});
				$.fancybox({
					content:emailDialogObject.content,
					title:title,
					openEffect:'none',
					closeEffect:'none'
				});
			},
			error:function ()
			{
			},
			async:true,
			dataType:'html'
		});
	};

	$.viewSelectedFormat = function (target, fullScreen, isHelp)
	{
		var selectedFileId = $(target).find('.service-data .link-id').html();
		var selectedLinkName = $(target).find('.service-data .link-name').html();
		var selectedFileName = $(target).find('.service-data .file-name').html();
		var selectedFileType = $(target).find('.service-data .file-type').html();
		var selectedViewType = $(target).find('.service-data .view-type').html();
		var selectedLinks = $(target).find('.service-data .links').html();
		var selectedThumbs = $(target).find('.service-data .thumbs').html();

		$.fancybox.close();

		if (selectedFileType != '' && selectedViewType != '' && selectedLinks != '')
		{
			selectedLinks = $.parseJSON(selectedLinks);
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
							$.ajax({
								type:"POST",
								url:"statistic/writeActivity",
								data:{
									type:'Link',
									subType:'Preview',
									data:$.toJSON({
										Name:selectedLinkName,
										File:selectedFileName,
										'Original Format':selectedFileType,
										Format:selectedViewType,
										Mode:fullScreen ? 'Fullscreen' : 'Modal'
									})
								},
								async:true,
								dataType:'html'
							});
							if (fullScreen)
								window.open("preview/runFullscreenGallery?linkId=" + selectedFileId + "&format=" + selectedViewType);
							else
							{
								$.fancybox(selectedLinks, {
									openEffect:'none',
									closeEffect:'none',
									afterClose:function ()
									{
										//$.viewDialogBar.close();
									},
									helpers:{
										thumbs:{
											height:selectedThumbs[0].height,
											width:selectedThumbs[0].width,
											source:this.thumb
										}
									}
								});
								//$.viewDialogBar.show($('.fancybox-skin'));
							}
							break;
						case 'email':
							emailFile(selectedFileId, selectedLinks[0].title);
							break;
						case 'favorites':
							addToFavorites(selectedFileId, selectedLinks[0].title);
							break;
						default:
							$.ajax({
								type:"POST",
								url:"statistic/writeActivity",
								data:{
									type:isHelp ? 'Help Link' : 'Link',
									subType:'Open',
									data:$.toJSON({
										Name:selectedLinkName,
										File:selectedFileName,
										'Original Format':selectedFileType,
										Format:selectedViewType
									})
								},
								async:true,
								dataType:'html'
							});
							openFile(selectedLinks[0].href);
							break;
					}
					break;
				case 'xls':
					switch (selectedViewType)
					{
						case 'email':
							emailFile(selectedFileId, selectedLinks[0].title);
							break;
						case 'favorites':
							addToFavorites(selectedFileId, selectedLinks[0].title);
							break;
						default:
							$.ajax({
								type:"POST",
								url:"statistic/writeActivity",
								data:{
									type:isHelp ? 'Help Link' : 'Link',
									subType:'Open',
									data:$.toJSON({
										Name:selectedLinkName,
										File:selectedFileName,
										'Original Format':selectedFileType,
										Format:selectedFileType != selectedViewType ? selectedViewType : null
									})
								},
								async:true,
								dataType:'html'
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
							emailFile(selectedFileId, selectedLinks[0].title);
							break;
						case 'favorites':
							addToFavorites(selectedFileId, selectedLinks[0].title);
							break;
						default:
							$.ajax({
								type:"POST",
								url:"statistic/writeActivity",
								data:{
									type:isHelp ? 'Help Link' : 'Link',
									subType:'Open',
									data:$.toJSON({
										Name:selectedLinkName,
										File:selectedFileName,
										'Original Format':selectedFileType
									})
								},
								async:true,
								dataType:'html'
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
							emailFile(selectedFileId, selectedLinks[0].title);
							break;
						case 'favorites':
							addToFavorites(selectedFileId, selectedLinks[0].title);
							break;
						default:
							$.ajax({
								type:"POST",
								url:"statistic/writeActivity",
								data:{
									type:'Link',
									subType:'Preview',
									data:$.toJSON({
										Name:selectedLinkName,
										File:selectedFileName,
										'Original Format':selectedFileType
									})
								},
								async:true,
								dataType:'html'
							});
							$.fancybox(selectedLinks, {
								openEffect:'none',
								closeEffect:'none'
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
							$.ajax({
								type:"POST",
								url:"statistic/writeActivity",
								data:{
									type:isHelp ? 'Help Link' : 'Link',
									subType:'Open',
									data:$.toJSON({
										Name:selectedLinkName,
										File:decodeURI(selectedLinks[0].src.substr(selectedLinks[0].src.lastIndexOf('/') + 1)),
										'Original Format':selectedFileType,
										Format:selectedFileType != selectedViewType ? selectedViewType : null
									})
								},
								async:true,
								dataType:'html'
							});
							openFile(selectedLinks[0].href);
							break;
						case 'email':
							emailFile(selectedFileId, selectedLinks[0].title);
							break;
						case 'download':
							downloadFile(selectedFileId, selectedLinks[0].title);
							break;
						case 'favorites':
							addToFavorites(selectedFileId, selectedLinks[0].title);
							break;
						case 'mp4':
							$.ajax({
								type:"POST",
								url:"statistic/writeActivity",
								data:{
									type:isHelp ? 'Help Link' : 'Link',
									subType:'Play Video',
									data:$.toJSON({
										Name:selectedLinkName,
										File:decodeURI(selectedLinks[0].src.substr(selectedLinks[0].src.lastIndexOf('/') + 1)),
										'Original Format':selectedFileType,
										Format:selectedFileType != selectedViewType ? selectedViewType : null
									})
								},
								async:true,
								dataType:'html'
							});

							playVideo(selectedLinks, isHelp);
							break;
					}
					break;
			}
		}
	};

	var playVideo = function (links, isHelp)
	{
		VideoJS.players = {};
		$.fancybox({
			title:links[0].title,
			content:$('<div style="height:480px; width:640px;"><video id="video-player" class="video-js vjs-default-skin" height = "480" width="640"></video><div>'),
			openEffect:'none',
			closeEffect:'none',
			afterClose:function ()
			{
				$('#video-player').remove();
			}
		});
		_V_.options.flash.swf = links[0].swf;
		var myPlayer = _V_("video-player", {
			controls:true,
			autoplay:true,
			preload:'auto',
			width:640,
			height:480
		});
		myPlayer.src(links);
	}
})(jQuery);

