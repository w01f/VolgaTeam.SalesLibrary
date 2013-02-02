(function ($)
{
	$.initFavoritesView = function ()
	{
		$.ajax({
			type:"POST",
			url:"favorites/getFavoritesView",
			beforeSend:function ()
			{
				$('#content').html('');
				$.showOverlay();
			},
			complete:function ()
			{
				$.hideOverlay();
			},
			success:function (msg)
			{
				$('#content').html('<div>' + msg + '</div>');
				getFolderLinks(null, 0);
				var foldersPanel = $('#favorites-panel-folders');
				foldersPanel.find('li')
					.on('click', function (event)
					{
						openFolder($(this));
						event.stopPropagation();
					})
					.on('touchstart', function ()
					{
						isScrolling = false;
					})
					.on('touchmove', function ()
					{
						isScrolling = true;
					})
					.on('touchend', function (e)
					{
						if (!isScrolling)
							openFolder($(this));
						e.stopPropagation();
						e.preventDefault();
						return false;
					});
				$.updateContentAreaDimensions();
			},
			async:true,
			dataType:'html'
		});
	};

	var openFolder = function (listItem)
	{
		var foldersPanel = $('#favorites-panel-folders');
		foldersPanel.find('li a').removeClass('opened');
		foldersPanel.find('li i').removeClass('icon-folder-open').addClass('icon-folder-close');
		listItem.children('a').addClass('opened');
		listItem.children('a').children('i').removeClass('icon-folder-close').addClass('icon-folder-open');
		var folderId = listItem.children('.service-data').children('.folder-id').html();
		getFolderLinks(folderId, 0);
	};

	var getFolderLinks = function (folderId, isSort)
	{
		$.ajax({
			type:"POST",
			url:"favorites/getLinks",
			data:{
				folderId:folderId,
				isSort:isSort
			},
			beforeSend:function ()
			{
				$.showOverlayLight();
			},
			complete:function ()
			{
				$.hideOverlayLight();
				$.linkGrid.refreshData = function ()
				{
					getFolderLinks(folderId, 1);
				};
				$.linkGrid.init();
				$.updateContentAreaDimensions();
			},
			success:function (msg)
			{
				$('#favorites-panel-links').find('>div').html('').append(msg);
			},
			error:function ()
			{
				$('#favorites-panel-links').find('>div').html('');
			},
			async:true,
			dataType:'html'
		});
	};

	$(document).ready(function ()
	{
	});
})(jQuery);