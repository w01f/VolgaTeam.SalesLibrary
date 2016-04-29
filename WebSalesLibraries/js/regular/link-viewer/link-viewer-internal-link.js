(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.InternalLinkViewer = function (parameters)
	{
		var viewerData = new $.SalesPortal.InternalLinkViewerData(parameters.data);
		this.show = function ()
		{
			if (!viewerData.forcePreview || !viewerData.runLinkPreview)
			{
				new $.SalesPortal.ShortcutsWallbin().init({
					content: parameters.content,
					options: {
						headerTitle: viewerData.name,
						headerIcon: '',
						libraryId: viewerData.libraryId,
						pageSelectorMode: 'tabs',
						pageViewType: 'columns'
					}
				});
			}

			if(viewerData.runLinkPreview)
			{
				if (viewerData.libraryLinkId != null)
					$.SalesPortal.LinkManager.requestViewDialog(viewerData.libraryLinkId)
				else
				{
					var modalDialog = new $.SalesPortal.ModalDialog({
						title: '<span class="text-danger">Sorry...</span>',
						description: 'Link not found.',
						buttons: [
							{
								tag: 'close',
								title: 'Close',
								width: 80,
								clickHandler: function ()
								{
									modalDialog.close();
								}
							}
						],
						closeOnOutsideClick: true
					});
					modalDialog.show();
				}
			}
		};
	};
})(jQuery);
