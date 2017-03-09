(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.LinkBundleViewer = function (parameters, parentPageData)
	{
		var viewerData = new $.SalesPortal.SimpleViewerData(parameters.data);

		this.show = function ()
		{
			var body = $('body');
			body.find('#link-bundle-viewer').remove();
			body.append($(parameters.content));

			var baseLinkViewerPage = $('#link-bundle-viewer');
			baseLinkViewerPage.find('.page-header .header-title').html(parentPageData.name);
			baseLinkViewerPage.find('.main-content .content-header .back a').prop('href', parentPageData.id);

			baseLinkViewerPage.find('.preview-link').off('click').on('click', function ()
			{
				$.SalesPortal.LinkManager.requestViewDialog(
					$(this).find('.library-link-id').text(),
					{
						id: '#link-bundle-viewer',
						name: baseLinkViewerPage.find('.header-title').text()
					},
					false
				);
			});

			$('.logout-button').off('click').on('click', function (e)
			{
				e.stopPropagation();
				e.preventDefault();
				$.SalesPortal.Auth.logout();
			});

			$.mobile.initializePage();
			$.mobile.pageContainer.pagecontainer("change", "#link-bundle-viewer", {
				transition: "slidefade"
			});
		};
	};
})(jQuery);
