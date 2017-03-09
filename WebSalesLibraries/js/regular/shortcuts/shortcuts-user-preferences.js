(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsUserPreferences = function ()
	{
		var content = undefined;
		this.init = function ()
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "linkUserProfile/getEditor",
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show();
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					content = $(msg);

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
								settingsGroup.find('.checkbox.default input').prop('checked', false);
								thisCheck.prop('checked', true);
							}
							else
								settingsGroup.find('.checkbox.default input').prop('checked', settingsGroup.find('.checkbox input:checked').length == 0);
							updateWarningVisibility();
						});
						settingsGroup.find('.checkbox.default input').off('click.user-preferences').on('click.user-preferences', function ()
						{
							var thisCheck = $(this);
							if (thisCheck.is(':checked'))
							{
								settingsGroup.find('.checkbox input').prop('checked', false);
								thisCheck.prop('checked', true);
							}
							else
								thisCheck.prop('checked', true);
						});
					});

					content.find('.accept-button').off('click.search-app').on('click.search-app', function ()
					{
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "linkUserProfile/applyEditorValues",
							data: {
								userProfile: JSON.stringify({
									powerPointSettings: {
										forceEOOpen: content.find('#user-link-preferences-power-point-force-EO-open').prop('checked'),
										forceWebOpen: false
									},
									docSettings: {
										forceEOOpen: content.find('#user-link-preferences-doc-force-EO-open').prop('checked'),
										forceWebOpen: false
									},
									xlsSettings: {
										forceEOOpen: content.find('#user-link-preferences-xls-force-EO-open').prop('checked'),
										forceWebOpen: false
									},
									pdfSettings: {
										forceEOOpen: content.find('#user-link-preferences-pdf-force-EO-open').prop('checked'),
										forceWebOpen: content.find('#user-link-preferences-pdf-force-web-open').prop('checked')
									},
									imageSettings: {
										forceEOOpen: content.find('#user-link-preferences-image-force-EO-open').prop('checked'),
										forceWebOpen: content.find('#user-link-preferences-image-force-web-open').prop('checked')
									}
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
						width: 650,
						autoSize: false,
						autoHeight: true,
						openEffect: 'none',
						closeEffect: 'none',
						helpers: {
							title: false
						}
					});

					updateWarningVisibility();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		var updateWarningVisibility = function ()
		{
			if (content.find('#user-link-preferences-pdf-force-web-open').prop('checked') ||
				content.find('#user-link-preferences-image-force-web-open').prop('checked'))
				content.find('.popup-blocker-warning').show();
			else
				content.find('.popup-blocker-warning').hide();
		};
	};
})(jQuery);
