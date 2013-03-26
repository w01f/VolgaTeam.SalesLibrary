(function ($)
{
	$.initLibraries = function ()
	{
		$.ajax({
			type:"POST",
			url:"wallbin/getLibraryDropDownList",
			beforeSend:function ()
			{
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
				$('#libraries-selector').html(msg).selectmenu('refresh', true);
				$.libraryChanged();
			},
			async:true,
			dataType:'html'
		});
	};

	$.libraryChanged = function ()
	{
		var selectedLibraryName = $("#libraries-selector").find(":selected").text();
		$.cookie("selectedLibraryName", selectedLibraryName, {
			expires:60 * 60 * 24 * 7
		});
		$.ajax({
			type:"POST",
			url:"statistic/writeActivity",
			data:{
				type:'Wallbin',
				subType:'Library Changed',
				data:$.toJSON({
					Library:selectedLibraryName
				})
			},
			async:true,
			dataType:'html'
		});
		$.ajax({
			type:"POST",
			url:"wallbin/getPageDropDownList",
			beforeSend:function ()
			{
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
				$('#page-selector').html(msg).selectmenu('refresh', true);
			},
			async:true,
			dataType:'html'
		});
	};

	$.pageChanged = function ()
	{
		var selectedPageName = $("#page-selector").find(":selected").text();
		$.cookie("selectedPageName", selectedPageName, {
			expires:60 * 60 * 24 * 7
		});
		$.ajax({
			type:"POST",
			url:"statistic/writeActivity",
			data:{
				type:'Wallbin',
				subType:'Page Changed',
				data:$.toJSON({
					Page:selectedPageName
				})
			},
			async:true,
			dataType:'html'
		});
	};

	$.loadPage = function ()
	{
		$.ajax({
			type:"POST",
			url:"wallbin/getFoldersList",
			data:{},
			beforeSend:function ()
			{
				$('#folders').find('.page-content').html('');
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
				var foldersPage = $('#folders');
				foldersPage.find('.page-content').html(msg);
				var selctedLibraryName = $.cookie("selectedLibraryName");
				foldersPage.find('.library-title').html(selctedLibraryName);
				$('#links').find('.library-title').html(selctedLibraryName);
				$('#link-details').find('.library-title').html(selctedLibraryName);
				$('#gallery-page').find('.library-title').html(selctedLibraryName);
				$.mobile.changePage("#folders", {
					transition:"slidefade"
				});
				foldersPage.find('.page-content').children('ul').listview();
				$(".folder-link").on('click', function ()
				{
					var folderId = $.trim($(this).attr("href").replace('#folder', ''));
					$.loadFolder(folderId);
				});
			},
			async:true,
			dataType:'html'
		});
	};

	$.loadFolder = function (folderId)
	{
		$.ajax({
			type:"POST",
			url:"wallbin/getFolderLinksList",
			data:{
				folderId:folderId
			},
			beforeSend:function ()
			{
				$('#links').find('.page-content').html('');
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
				var linksPage = $('#links');
				linksPage.find('.page-content').html(msg);
				$.mobile.changePage("#links", {
					transition:"slidefade"
				});
				linksPage.find('.page-content').children('ul').listview();
				linksPage.find(".file-link").on('click', function ()
				{
					var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
					$.loadLink(selectedLink, $.cookie("selectedLibraryName"), false, '#links');
				});
				linksPage.find(".folder-content-link").on('click', function ()
				{
					var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
					$.loadFolderContent(selectedLink, null);
				});
				linksPage.find(".file-link-detail").on('click', function (event)
				{
					var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
					$.loadLinkDeatils(selectedLink, $.cookie("selectedLibraryName"), '#links');
					event.stopPropagation();
				});
			},
			async:true,
			dataType:'html'
		});
	};

	$.loadLink = function (linkId, parentTitle, isAttachment, backLink)
	{
		$.ajax({
			type:"POST",
			url:isAttachment ? "preview/getAttachmentPreviewList" : "preview/getLinkPreviewList",
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
				previewPage.find('.library-title').html(parentTitle);
				$('.email-tab .library-title').html(parentTitle);
				$('.favorites-tab .library-title').html(parentTitle);
				previewPage.find('.link.back').attr('href', backLink);
				$.mobile.changePage("#preview", {
					transition:"slidefade"
				});
				previewPage.find('.page-content').children('ul').listview();
				previewPage.find('.res-selector').navbar();

				$('.file-size.regular').hide();
				$('.file-size.phone').show();
				$('.res-selector a').on('click', function ()
				{
					if ($('a.low-res-button').hasClass('ui-btn-active'))
					{
						$('.file-size.regular').hide();
						$('.file-size.phone').show();

					}
					else
					{
						$('.file-size.regular').show();
						$('.file-size.phone').hide();
					}
				});

				$(".preview-link").on('click', function ()
				{
					var itemContent = $(this).find('.item-content');
					var viewFormat = itemContent.find('.view-type').html().toUpperCase();

					var resolution = 'hi';
					if ($('.res-selector .low-res-button').hasClass('ui-btn-active'))
						resolution = 'low';
					else if ($('.res-selector .hi-res-button').hasClass('ui-btn-active'))
						resolution = 'hi';

					if (viewFormat == 'PNG' || viewFormat == 'JPEG')
					{
						var galleryHeader = $('#preview').find('.link-container').first().clone();
						var previewInfo = '';
						if (resolution == 'hi')
							previewInfo += 'High Resolution - ';
						else if (resolution == 'low')
							previewInfo += 'Low Resolution - ';
						previewInfo += viewFormat + ' Images';
						galleryHeader.find('.file').html(previewInfo);

						$('#gallery-title').html('').append(galleryHeader);
					}

					$.viewSelectedFormat(itemContent, resolution);
				});
			},
			async:true,
			dataType:'html'
		});
	};

	$.loadLinkDeatils = function (linkId, parentTitle, backLink)
	{
		$.ajax({
			type:"POST",
			url:"preview/getLinkDetails",
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
				var linkDetailsPage = $('#link-details');
				linkDetailsPage.find('.page-content').html(msg);
				linkDetailsPage.find('.link.back').attr('href', backLink);
				$.mobile.changePage("#link-details", {
					transition:"slidefade"
				});
				linkDetailsPage.find('.page-content').children('ul').listview();
				$(".file-card-link").on('click', function ()
				{
					var linkId = $.trim($(this).attr("href").replace('#file-card', ''));
					$.viewFileCard(linkId);
				});
				$(".attachment-link").on('click', function ()
				{
					var attachmentId = $.trim($(this).attr("href").replace('#attachment', ''));
					$.loadLink(attachmentId, parentTitle, true, '#link-details');
				});
			},
			async:true,
			dataType:'html'
		});
	};

	$.loadFolderContent = function (linkId, parentLinkId)
	{
		$.ajax({
			type:"POST",
			url:"wallbin/getLinkFolderContent",
			data:{
				linkId:linkId
			},
			beforeSend:function ()
			{
				$('#link-folder-content-' + linkId).find('.page-content').html('');
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
				var linkFolderContent = $('#link-folder-content-' + linkId);
				if (!linkFolderContent[0])
				{
					var linkFolderContentTemplate = $('#link-folder-content-template');
					linkFolderContent = linkFolderContentTemplate.clone(true)
						.insertAfter(linkFolderContentTemplate)
						.attr('id', 'link-folder-content-' + linkId);
					linkFolderContent.find('.link.back').attr('href', parentLinkId == null ? '#links' : ('#link-folder-content-' + parentLinkId));
					linkFolderContent.find('.library-title').html($.cookie("selectedLibraryName"));
				}
				linkFolderContent.find('.page-content').html(msg);
				$.mobile.changePage('#link-folder-content-' + linkId, {
					transition:"slidefade"
				});
				linkFolderContent.find('.page-content').children('ul').listview();
				linkFolderContent.find(".file-link").on('click', function ()
				{
					var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
					$.loadLink(selectedLink, $.cookie("selectedLibraryName"), false, ('#link-folder-content-' + linkId));
				});
				linkFolderContent.find(".folder-content-link").on('click', function ()
				{
					var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
					$.loadFolderContent(selectedLink, linkId);
				});
				linkFolderContent.find(".file-link-detail").on('click', function (event)
				{
					var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
					$.loadLinkDeatils(selectedLink, $.cookie("selectedLibraryName"), ('#link-folder-content-' + linkId));
					event.stopPropagation();
				});
			},
			async:true,
			dataType:'html'
		});
	};

	$(document).ready(function ()
	{
		$('#libraries-selector').on('change', function ()
		{
			$.libraryChanged();
		});
		$('#page-selector').on('change', function ()
		{
			$.pageChanged();
		});
		$('#load-page-button').off('click').on('click', function ()
		{
			$.loadPage();
		});
	});
})(jQuery);