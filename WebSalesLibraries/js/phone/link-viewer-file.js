(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.FileViewer = function (parameters, parentPageData)
	{
		var viewerData = new $.SalesPortal.SimpleViewerData(parameters.data);

		this.show = function ()
		{
			$('body .link-viewer-page').remove();
			$('body').append($(parameters.content));

			$('.link-viewer-page .page-header .header-title').html(parentPageData.name);

			var baseLinkViewerPage = $('#link-viewer');
			baseLinkViewerPage.find('.main-content .content-header .back a').prop('href', parentPageData.id);

			baseLinkViewerPage.find('.actions .action').off('click').on('click', processAction);

			$('.logout-button').off('click').on('click', function (e)
			{
				e.stopPropagation();
				e.preventDefault();
				$.SalesPortal.Auth.logout();
			});

			$.SalesPortal.EmailManager.init();
			$.SalesPortal.Favorites.initAddPage();

			$.mobile.initializePage();
			$.mobile.pageContainer.pagecontainer("change", "#link-viewer", {
				transition: "slidefade"
			});
		};

		var open = function ()
		{
			$.SalesPortal.LinkManager.openFile(viewerData.url);
		};

		var processAction = function ()
		{
			var tag = $(this).find('.service-data .tag').text();
			switch (tag)
			{
				case 'download':
				case 'open':
					open();
					break;
				case 'quicksite':
					$.SalesPortal.EmailManager.show();
					break;
				case 'favorites':
					$.SalesPortal.Favorites.showAddPage();
					break;
			}
		};
	};
})(jQuery);
