(function ($)
{
	$.downloadFile = function (url)
	{
		window.open(url.replace(/&amp;/g, '%26'));
	};

	var runEmailPage = function (linkId, selectedLinks)
	{
		$('.email-tab .link-container .name').html(selectedLinks[0].title);
		$('#email-send').off('click').on('click', function ()
		{
			$.sendEmail(linkId);
		});
		$.mobile.changePage("#email-address", {
			transition: "slidefade"
		});
	};

	var runAddPage = function (linkId, title, fileName, fileType)
	{
		//reset activity tracking
		$('#add-page-info, #add-page-logo, #add-page-tracking, #add-page-pin, #add-page-options').off('pageshow.activity');
		$('#add-page-restricted,#add-page-expires-in,#add-page-name-enabled,#add-page-access-code-enabled,#add-page-disable-widgets,#add-page-disable-banners,#add-page-record-activity,#add-page-show-links-as-url').off('change.activity');
		$.ajax({
			type: "POST",
			url: "statistic/writeActivity",
			data: {
				type: 'Email',
				subType: 'Create Email',
				data: $.toJSON({
					Name: title,
					File: fileName,
					'Original Format': fileType
				})
			},
			async: true,
			dataType: 'html'
		});

		$('.email-tab .link-container .name').html(title);
		$('#add-page-name').val(title);
		$('.email-tab .add-page-accept').off('click').on('click', function ()
		{
			$.addLitePage(linkId);
		});
		$.mobile.changePage("#add-page-info", {
			transition: "slidefade"
		});
		$('#add-page-expires-in').val("7").selectmenu('refresh', true);
		$('#add-page-restricted').removeAttr('checked').checkboxradio();
		$('#add-page-restricted,#add-page-access-code-enabled,#add-page-disable-widgets,#add-page-disable-banners,#add-page-record-activity,#add-page-show-links-as-url').removeAttr('checked').checkboxradio();
		$('#add-page-access-code,#add-page-activity-email-copy').val('').textinput().textinput('disable');
		$('#add-page-logo').find('.page-content').find('li').attr("data-theme", "c").removeClass("ui-btn-up-e").removeClass('ui-btn-hover-e').removeClass('qpage-logo-selected').addClass("ui-btn-up-c").addClass('ui-btn-hover-c');
		$('#add-page-logo').find('.page-content').find('li').first().attr("data-theme", "e").removeClass("ui-btn-up-c").removeClass('ui-btn-hover-c').addClass("ui-btn-up-e").addClass('ui-btn-hover-e').addClass('qpage-logo-selected');

		//set activity tracking
		$('#add-page-info, #add-page-logo, #add-page-tracking, #add-page-pin, #add-page-options').on('pageshow.activity', function (e)
		{
			$.ajax({
				type: "POST",
				url: "statistic/writeActivity",
				data: {
					type: 'Email',
					subType: 'Email Activity',
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
		$('#add-page-restricted,#add-page-expires-in,#add-page-name-enabled,#add-page-access-code-enabled,#add-page-disable-widgets,#add-page-disable-banners,#add-page-record-activity,#add-page-show-links-as-url').on('change.activity', function ()
		{
			$.ajax({
				type: "POST",
				url: "statistic/writeActivity",
				data: {
					type: 'Email',
					subType: 'Email Activity',
					data: $.toJSON({
						Name: title,
						File: fileName
					})
				},
				async: true,
				dataType: 'html'
			});
		});
		$('#add-page-logo').find('.page-content').find('li').off('click.activity').on('click.activity', function (e)
		{
			$.ajax({
				type: "POST",
				url: "statistic/writeActivity",
				data: {
					type: 'Email',
					subType: 'Email Activity',
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
		$('#add-page-info-disclaimer,#add-page-tracking-disclaimer,#add-page-options-disclaimer,#add-page-tracking-pin').off('click.activity').on('click.activity', function (e)
		{
			$.ajax({
				type: "POST",
				url: "statistic/writeActivity",
				data: {
					type: 'Email',
					subType: 'Email Activity',
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
	};

	var runFavoritesPage = function (linkId, title, fileName, fileType)
	{
		$.ajax({
			type: "POST",
			url: "statistic/writeActivity",
			data: {
				type: 'Link',
				subType: 'Favorites',
				data: $.toJSON({
					Name: title,
					File: fileName,
					'Original Format': fileType
				})
			},
			async: true,
			dataType: 'html'
		});

		$('.favorites-tab .link-container .name').html(title);
		$('#favorite-link-name').val(title);
		$('#favorite-folder-name').val('');
		$('#favorite-add-button').off('click').on('click', function ()
		{
			$.addFavoriteLink(linkId);
		});
		$('#favorite-folder-select-button').off('click.activity').on('click.activity', function ()
		{
			$.ajax({
				type: "POST",
				url: "statistic/writeActivity",
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
		$.mobile.changePage("#favorites-add", {
			transition: "slidefade"
		});
		$.ajax({
			type: "POST",
			url: "favorites/getFoldersList",
			beforeSend: function ()
			{
				$.mobile.loading('show', {
					textVisible: false,
					html: ""
				});
			},
			complete: function ()
			{
				$.mobile.loading('hide', {
					textVisible: false,
					html: ""
				});
			},
			success: function (msg)
			{
				$('#favorites-folder-list-dialog').find('.dialog-content').html(msg);
				$('#favorites-folders-list').find('input[type="radio"]').checkboxradio();
				$('#favorites-folders-apply-button').button().off('click').on('click', function ()
				{
					var selectedFolders = [];
					$.each($('#favorites-folders-list').find('.favorites-folder:checked'), function ()
					{
						selectedFolders.push($(this).val());
					});
					if (selectedFolders.length > 0)
						$('#favorite-folder-name').val(selectedFolders.join('; '));
					else
						$('#favorite-folder-name').val('');
					$("#favorites-folder-list-dialog").dialog("close");
					$.ajax({
						type: "POST",
						url: "statistic/writeActivity",
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
			},
			async: true,
			dataType: 'html'
		});
	};

	$.viewSelectedFormat = function (itemContent, resolution)
	{
		var selectedFileId = itemContent.find('.link-id').html();
		var selectedLinkName = itemContent.find('.link-name').html();
		var selectedFileName = itemContent.find('.file-name').html();
		var selectedFileType = itemContent.find('.file-type').html();
		var selectedViewType = itemContent.find('.view-type').html();
		var selectedLinks = itemContent.find('.links');

		if (selectedFileType != '' && selectedViewType != '' && selectedLinks != '')
		{
			if (!(selectedViewType == 'png' || selectedViewType == 'jpeg'))
				selectedLinks = $.parseJSON(selectedLinks.html());
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
								type: "POST",
								url: "statistic/writeActivity",
								data: {
									type: 'Link',
									subType: 'Preview',
									data: $.toJSON({
										Name: selectedLinkName,
										File: selectedFileName,
										'Original Format': selectedFileType,
										Format: selectedViewType,
										Resolution: resolution == 'hi' ? 'Hi' : 'Low'
									})
								},
								async: true,
								dataType: 'html'
							});
							var imageItems = '';
							var selector = 'li.low-res';
							if (resolution == 'hi')
								selector = 'li.hi-res';
							selectedLinks.find(selector).each(function ()
							{
								imageItems += '<li>' + $(this).html() + '</li>';
							});
							$('#gallery').html(imageItems);
							$.mobile.changePage("#gallery-page", {
								transition: "slidefade"
							});
							break;
						case 'email':
							runEmailPage(selectedFileId, selectedLinks);
							break;
						case 'outlook':
							runAddPage(selectedFileId, selectedLinkName, selectedFileName, selectedFileType);
							break;
						case 'favorites':
							runFavoritesPage(selectedFileId, selectedLinkName, selectedFileName, selectedFileType);
							break;
						default:
							$.ajax({
								type: "POST",
								url: "statistic/writeActivity",
								data: {
									type: 'Link',
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
							$.downloadFile(selectedLinks[0].href);
							break;
					}
					break;
				case 'xls':
					switch (selectedViewType)
					{
						case 'email':
							runEmailPage(selectedFileId, selectedLinks);
							break;
						case 'outlook':
							runAddPage(selectedFileId, selectedLinkName, selectedFileName, selectedFileType);
							break;
						case 'favorites':
							runFavoritesPage(selectedFileId, selectedLinkName, selectedFileName, selectedFileType);
							break;
						default:
							$.ajax({
								type: "POST",
								url: "statistic/writeActivity",
								data: {
									type: 'Link',
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
							$.downloadFile(selectedLinks[0].href);
							break;
					}
					break;
				case 'key':
				case 'url':
				case 'other':
					switch (selectedViewType)
					{
						case 'email':
							runEmailPage(selectedFileId, selectedLinks);
							break;
						case 'outlook':
							runAddPage(selectedFileId, selectedLinkName, selectedFileName, selectedFileType);
							break;
						case 'favorites':
							runFavoritesPage(selectedFileId, selectedLinkName, selectedFileName, selectedFileType);
							break;
						default:
							$.ajax({
								type: "POST",
								url: "statistic/writeActivity",
								data: {
									type: 'Link',
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
							$.downloadFile(selectedLinks[0].href);
							break;
					}
					break;
				case 'png':
				case 'jpeg':
					switch (selectedViewType)
					{
						case 'email':
							runEmailPage(selectedFileId, selectedLinks);
							break;
						case 'outlook':
							runAddPage(selectedFileId, selectedLinkName, selectedFileName, selectedFileType);
							break;
						case 'favorites':
							runFavoritesPage(selectedFileId, selectedLinkName, selectedFileName, selectedFileType);
							break;
						default:
							$.ajax({
								type: "POST",
								url: "statistic/writeActivity",
								data: {
									type: 'Link',
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
							var imageItems = '';
							selectedLinks.find('.hi-res').each(function ()
							{
								imageItems += '<li>' + $(this).html() + '</li>';
							});
							$('#gallery').html(imageItems);
							$.mobile.changePage("#gallery-page", {
								transition: "slidefade"
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
						case 'mp4':
						case 'wmv':
						case 'tab':
						case 'ogv':
							$.ajax({
								type: "POST",
								url: "statistic/writeActivity",
								data: {
									type: 'Link',
									subType: 'Open',
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
							$.downloadFile(selectedLinks[0].href);
							break;
						case 'email':
							runEmailPage(selectedFileId, selectedLinks);
							break;
						case 'outlook':
							runAddPage(selectedFileId, selectedLinkName, selectedFileName, selectedFileType);
							break;
						case 'favorites':
							runFavoritesPage(selectedFileId, selectedLinkName, selectedFileName, selectedFileType);
							break;
					}
					break;
			}
		}
	};

	$.viewFileCard = function (linkId)
	{
		$.ajax({
			type: "POST",
			url: "preview/getFileCard",
			data: {
				linkId: linkId
			},
			beforeSend: function ()
			{
				$('#preview').find('.page-content').html('');
				$.mobile.loading('show', {
					textVisible: false,
					html: ""
				});
			},
			complete: function ()
			{
				$.mobile.loading('hide', {
					textVisible: false,
					html: ""
				});
			},
			success: function (msg)
			{
				var previewPage = $('#preview');
				previewPage.find('.page-content').html(msg);
				previewPage.find('.header-title').html('Important Info');
				previewPage.find('.link.back').attr('href', '#link-details');
				$.mobile.changePage("#preview", {
					transition: "slidefade"
				});
				previewPage.find('.page-content').children('ul').listview();
			},
			async: true,
			dataType: 'html'
		});
	};
})(jQuery);

