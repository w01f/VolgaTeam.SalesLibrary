(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsUserPreferences = function ()
	{
		this.init = function ()
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "linkUserProfile/getEditor",
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					var content = $(msg);

					var formLogger = new $.SalesPortal.FormLogger();
					formLogger.init({
						logObject: {name: 'User Preferences'},
						formContent: content
					});

					$.each(content.find('.settings-row'), function ()
					{
						var settingsGroup = $(this);
						settingsGroup.find('.checkbox input').off('click.user-preferences').on('click.user-preferences', function ()
						{
							var thisCheck = $(this);
							if (thisCheck.is(':checked'))
							{
								settingsGroup.find('.checkbox input').prop('checked', false);
								thisCheck.prop('checked', true);
							}
							else
								settingsGroup.find('.checkbox.default input').prop('checked', true);
						});
					});

					content.find('.accept-button').off('click.search-app').on('click.search-app', function ()
					{
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "linkUserProfile/applyEditorValues",
							data: {
								userProfile: JSON.stringify({
									powerPointSettings: {forceOpen: content.find('#user-link-preferences-power-point-force-open').prop('checked')},
									docSettings: {forceOpen: content.find('#user-link-preferences-doc-force-open').prop('checked')},
									xlsSettings: {forceOpen: content.find('#user-link-preferences-xls-force-open').prop('checked')},
									pdfSettings: {forceOpen: content.find('#user-link-preferences-pdf-force-open').prop('checked')},
									imageSettings: {forceOpen: content.find('#user-link-preferences-image-force-open').prop('checked')}
								})
							},
							async: true,
							dataType: 'json'
						});
						$.fancybox.close();
					});

					content.find('.cancel-button').off('click.search-app').on('click.search-app', function ()
					{
						$.fancybox.close();
					});

					$.fancybox({
						content: content,
						width: 500,
						autoSize: false,
						autoHeight: true,
						openEffect: 'none',
						closeEffect: 'none',
						helpers: {
							title: false
						}
					});
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};
	};
})(jQuery);
