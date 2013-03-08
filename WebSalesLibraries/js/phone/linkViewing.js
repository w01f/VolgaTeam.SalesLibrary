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
			transition:"slidefade"
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
										Resolution:resolution == 'hi' ? 'Hi' : 'Low'
									})
								},
								async:true,
								dataType:'html'
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
								transition:"slidefade"
							});
							break;
						case 'email':
							runEmailPage(selectedFileId, selectedLinks);
							break;
						default:
							$.ajax({
								type:"POST",
								url:"statistic/writeActivity",
								data:{
									type:'Link',
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
						default:
							$.ajax({
								type:"POST",
								url:"statistic/writeActivity",
								data:{
									type:'Link',
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
						default:
							$.ajax({
								type:"POST",
								url:"statistic/writeActivity",
								data:{
									type:'Link',
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
						default:
							$.ajax({
								type:"POST",
								url:"statistic/writeActivity",
								data:{
									type:'Link',
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
							$.downloadFile(selectedLinks[0].href);
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
									type:'Link',
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
							$.downloadFile(selectedLinks[0].href);
							break;
						case 'email':
							runEmailPage(selectedFileId, selectedLinks);
							break;
					}
					break;
			}
		}
	};

	$.viewFileCard = function (linkId)
	{
		$.ajax({
			type:"POST",
			url:"wallbin/getFileCard",
			data:{
				linkId:linkId
			},
			beforeSend:function ()
			{
				$('#preview').find('.page-content').html('');
				$.mobile.loading('show', {
					textVisible:false,
					html:""
				});
			},
			complete:function ()
			{
				$.mobile.loading('hide', {
					textVisible:false,
					html:""
				});
			},
			success:function (msg)
			{
				var previewPage = $('#preview');
				previewPage.find('.page-content').html(msg);
				previewPage.find('.library-title').html('Important Info');
				previewPage.find('.link.back').attr('href', '#link-details');
				$.mobile.changePage("#preview", {
					transition:"slidefade"
				});
				previewPage.find('.page-content').children('ul').listview();
			},
			async:true,
			dataType:'html'
		});
	};
})(jQuery);

