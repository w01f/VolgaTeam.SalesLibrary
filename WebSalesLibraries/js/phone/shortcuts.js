(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var ShortcutsManager = function ()
	{
		this.init = function ()
		{
			$('#page-selector').on('change', function ()
			{
				pageChanged();
			});
			initPageContent();
			$.mobile.changePage($('#shortcuts'));

			$('.logout-button-accept').off('click').on('click', function (e)
			{
				e.stopPropagation();
				e.preventDefault();
				$.SalesPortal.Auth.logout();
			})
		};

		var pageChanged = function ()
		{
			var selectedPageId = $("#page-selector").find(":selected").val();
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/getPage",
				data: {
					pageId: selectedPageId
				},
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
				success: function (data)
				{
					$('#shortcuts-links').html(data.content);
					initPageContent();
				},
				async: true,
				dataType: 'json'
			});
		};

		var initPageContent = function ()
		{
			var shortcutsLinks = $('#shortcuts-links');

			shortcutsLinks.find('ul').listview();

			shortcutsLinks.find('.shortcuts-link.empty').off('click').on('click', function (e)
			{
				e.stopPropagation();
			});

			shortcutsLinks.find('.shortcuts-link.search').off('click').on('click', function (e)
			{
				e.stopPropagation();
				e.preventDefault();
				var shortcutData = $(this).find('.service-data');
				$.SalesPortal.ShortcutsSearchManager(shortcutData);
			});
		};
	};
	$.SalesPortal.Shortcuts = new ShortcutsManager();
	$(document).ready(function ()
	{
		$.SalesPortal.Shortcuts.init();
	});
})(jQuery);