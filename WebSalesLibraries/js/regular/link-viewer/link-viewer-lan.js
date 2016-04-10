(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.LanViewer = function (parameters)
	{
		var viewerData = new $.SalesPortal.LanViewerData(parameters.data);
		var dialogContent = undefined;

		this.show = function ()
		{
			if (viewerData.isEOBrowser == true)
				$.SalesPortal.SalesLibraryExtensions.sendLinkData(viewerData);
			else
			{
				var modalDialog = new $.SalesPortal.ModalDialog({
					title: '<span class="text-danger">Sorry...</span>',
					description: 'Your Browser does not allow access to this network location…',
					buttons: [
						{
							tag: 'close',
							title: 'OK',
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
		};
	};
})(jQuery);
