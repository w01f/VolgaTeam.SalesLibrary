(function ($)
{
	var currentFolderId = null;
	$.initFavorites = function ()
	{
		$('.tab-favorites').off('click').on('click', function ()
		{
			loadFolder(null);
		});
	};

	$.addFavoriteLink = function (linkId)
	{
		$.ajax({
			type:"POST",
			url:"favorites/addLink",
			data:{
				linkId:linkId,
				linkName:$('#favorite-link-name').val(),
				folderName:$('#favorite-folder-name').val()
			},
			beforeSend: function(){
				$.mobile.loading( 'show', {
					textVisible: false,
					html: ""
				});
			},
			complete: function(){
				$.mobile.loading( 'hide', {
					textVisible: false,
					html: ""
				});
			},
			success:function (msg)
			{
				$.mobile.changePage('#favorites-success-popup', {
					transition: "pop"
				});
			},
			error:function ()
			{
			},
			async:true,
			dataType:'html'
		});
	}

	var loadFolder = function (folderId)
	{
		$.ajax({
			type:"POST",
			url:"favorites/getFoldersAndLinks",
			data:{
				folderId:folderId
			},
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
				var newPageId = 'favorites-folder-' + (folderId != null ? folderId : 'root');
				var oldPageId = folderId != null ? $.mobile.activePage.data('url') : newPageId;
				var folderContent = $(newPageId);
				if (!folderContent[0])
				{
					var favoritesTemplate = $('#favorites-template');
					folderContent = favoritesTemplate.clone(true)
						.insertAfter(favoritesTemplate)
						.attr('id', newPageId);
					var linkBack = folderContent.find('.link.back');
					if (folderId != null)
						linkBack.attr('href', '#' + oldPageId);
					else
						linkBack.remove();
				}
				folderContent.find('.page-content').html(msg);
				currentFolderId = folderId;
				$.mobile.changePage('#' + newPageId, {
					transition:"slidefade"
				});
				$(".favorite-folder-link").off('click').on('click', function ()
				{
					var selectedFolderId = $.trim($(this).attr("href").replace('#folder', ''));
					loadFolder(selectedFolderId);
				});
				$(".favorite-folder-link-delete").off('click').on('click', function (event)
				{
					var selectedLinkNode = $(this).closest('li');
					var selectedFolderId = $.trim($(this).attr("href").replace('#folder', ''));
					event.stopPropagation();

					var confirmationDialog = $("#confirmation-dialog");
					confirmationDialog.find('.dialog-description').text('You are going to exclude folder from favorites');
					confirmationDialog.find('.dialog-title').text('Are you sure?');
					confirmationDialog.find('.dialog-confirm').on("click.confirm", function ()
					{
						deleteFolder(selectedFolderId);
						selectedLinkNode.remove();
						confirmationDialog.dialog("close");
						$(this).off("click.confirm");
					});
					confirmationDialog.find('.dialog-cancel').on("click.cancel", function ()
					{
						confirmationDialog.dialog("close");
						$(this).off("click.cancel");
					});
					$.mobile.changePage("#confirmation-dialog");
				});
				$(".favorite-file-link").off('click').on('click', function ()
				{
					var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
					$.loadLink(selectedLink, 'Favorites', false, '#' + $.mobile.activePage.data('url'));
				});
				$(".favorite-file-link-detail").off('click').on('click', function (event)
				{
					var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
					$.loadLinkDeatils(selectedLink, 'Favorites', '#' + $.mobile.activePage.data('url'));
					event.stopPropagation();
				});
				$(".favorite-file-link-delete").off('click').on('click', function (event)
				{
					var selectedLinkNode = $(this).closest('li');
					var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
					var selectedLinkFolder = $.mobile.activePage.data('url').replace('favorites-folder-', '');
					if (selectedLinkFolder == 'root')
						selectedLinkFolder = null;
					event.stopPropagation();

					var confirmationDialog = $("#confirmation-dialog");
					confirmationDialog.find('.dialog-description').text('You are going to exclude link from favorites');
					confirmationDialog.find('.dialog-title').text('Are you sure?');
					confirmationDialog.find('.dialog-confirm').on("click.confirm", function ()
					{
						deleteLink(selectedLink, selectedLinkFolder);
						selectedLinkNode.remove();
						confirmationDialog.dialog("close");
						$(this).off("click.confirm");
					});
					confirmationDialog.find('.dialog-cancel').on("click.cancel", function ()
					{
						confirmationDialog.dialog("close");
						$(this).off("click.cancel");
					});
					$.mobile.changePage("#confirmation-dialog");
				});
			},
			async:true,
			dataType:'html'
		});
	};

	var deleteLink = function (linkId, folderId)
	{
		$.ajax({
			type:"POST",
			url:"favorites/deleteLink",
			data:{
				linkId:linkId,
				folderId:folderId
			},
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
			async:true,
			dataType:'html'
		});
	};

	var deleteFolder = function (folderId)
	{
		$.ajax({
			type:"POST",
			url:"favorites/deleteFolder",
			data:{
				folderId:folderId
			},
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
			async:true,
			dataType:'html'
		});
	};

	$(document).ready(function ()
	{
		$('#favorite-folder-select-button').off('click').on('click', function ()
		{
			$.mobile.changePage('#favorites-folder-list-dialog', {
				transition:"pop"
			});
		});
	});
})(jQuery);