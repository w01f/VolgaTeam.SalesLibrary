(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsUserPreferences = function () {
		var content = undefined;
		this.init = function () {
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "linkUserProfile/getEditor",
				beforeSend: function () {
					$.SalesPortal.Overlay.show();
				},
				complete: function () {
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg) {
					content = $(msg);

					var formLogger = new $.SalesPortal.FormLogger();
					formLogger.init({
						logObject: {name: 'User Preferences'},
						formContent: content
					});


					$.each(content.find('.link-settings-row'), function () {
						var settingsGroup = $(this);
						settingsGroup.find('.checkbox input').off('click.user-preferences').on('click.user-preferences', function () {
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
						settingsGroup.find('.checkbox.default input').off('click.user-preferences').on('click.user-preferences', function () {
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

					content.find('.email-expires-value input').off('click.user-preferences').on('click.user-preferences', function () {
						var thisCheck = $(this);
						content.find('.email-expires-value input').prop('checked', false);
						thisCheck.prop('checked', true);
					});

					content.find('.accept-button').off('click.search-app').on('click.search-app', function () {
						var expireInDays = 0;
						if (content.find('#user-link-preferences-email-expires-7').prop('checked'))
							expireInDays = 7;
						else if (content.find('#user-link-preferences-email-expires-14').prop('checked'))
							expireInDays = 14;
						else if (content.find('#user-link-preferences-email-expires-30').prop('checked'))
							expireInDays = 30;

						$.ajax({
							type: "POST",
							url: window.BaseUrl + "linkUserProfile/applyEditorValues",
							data: {
								userProfile: JSON.stringify({
									powerPointSettings: {
										forceEOOpen: content.find('#user-link-preferences-power-point-force-EO-open').prop('checked'),
										forceWebOpen: false,
										forceOneDriveOpen: content.find('#user-link-preferences-power-point-open-one-drive').prop('checked'),
										forceOpenGallery: false
									},
									docSettings: {
										forceEOOpen: content.find('#user-link-preferences-doc-force-EO-open').prop('checked'),
										forceWebOpen: false,
										forceOneDriveOpen: content.find('#user-link-preferences-doc-open-one-drive').prop('checked'),
										forceOpenGallery: false
									},
									xlsSettings: {
										forceEOOpen: content.find('#user-link-preferences-xls-force-EO-open').prop('checked'),
										forceWebOpen: false,
										forceOneDriveOpen: content.find('#user-link-preferences-xls-open-one-drive').prop('checked'),
										forceOpenGallery: content.find('#user-link-preferences-xls-force-open-gallery').prop('checked')
									},
									pdfSettings: {
										forceEOOpen: content.find('#user-link-preferences-pdf-force-EO-open').prop('checked'),
										forceWebOpen: content.find('#user-link-preferences-pdf-force-web-open').prop('checked'),
										forceOneDriveOpen: content.find('#user-link-preferences-pdf-open-one-drive').prop('checked'),
										forceOpenGallery: false
									},
									imageSettings: {
										forceEOOpen: content.find('#user-link-preferences-image-force-EO-open').prop('checked'),
										forceWebOpen: content.find('#user-link-preferences-image-force-web-open').prop('checked'),
										forceOneDriveOpen: content.find('#user-link-preferences-image-open-one-drive').prop('checked'),
										forceOpenGallery: false
									},
									defaultQPageSettings: {
										showLinksAsUrl: content.find('#user-link-preferences-qpage-show-links-as-url').prop('checked'),
										disableWidgets: content.find('#user-link-preferences-qpage-disable-widgets').prop('checked'),
										disableBanners: content.find('#user-link-preferences-qpage-disable-banners').prop('checked'),
										requireLogin: content.find('#user-link-preferences-qpage-require-login').prop('checked'),
										requirePinCode: content.find('#user-link-preferences-qpage-require-pin-code').prop('checked'),
										autoLaunch: content.find('#user-link-preferences-qpage-auto-launch').prop('checked')
									},
									defaultEmailSettings: {
										expiresInDays: expireInDays,
										showLinksAsUrl: content.find('#user-link-preferences-email-show-links-as-url').prop('checked'),
										disableWidgets: content.find('#user-link-preferences-email-disable-widgets').prop('checked'),
										disableBanners: content.find('#user-link-preferences-email-disable-banners').prop('checked'),
										autoLaunch: content.find('#user-link-preferences-email-auto-launch').prop('checked')
									}
								})
							},
							async: true,
							dataType: 'json'
						});
						$.fancybox.close();
					});

					content.find('.cancel-button').off('click.search-app').on('click.search-app', function () {
						$.fancybox.close();
					});

					$.fancybox({
						content: content,
						width: 850,
						autoSize: false,
						autoHeight: true,
						openEffect: 'none',
						closeEffect: 'none',
						helpers: {
							title: false
						},
						afterShow: function () {
							var tabContainer = content.find('#user-preferences-tabs-headers');
							tabContainer.scrollTabs({
								click_callback: function () {
									tabContainer.find('.page-tab-header').removeClass('selected');
									$(this).addClass('selected');
									var relatedContentId = $(this).find('.service-data .tab-id').text();
									content.find('#user-preferences-tabs-content').find('>div').removeClass('selected');
									content.find(relatedContentId).addClass('selected');
								}
							});
						}
					});

					updateWarningVisibility();
				},
				error: function () {
				},
				async: true,
				dataType: 'html'
			});
		};

		var updateWarningVisibility = function () {
			if (content.find('#user-link-preferences-power-point-open-one-drive').prop('checked') ||
				content.find('#user-link-preferences-doc-open-one-drive').prop('checked') ||
				content.find('#user-link-preferences-xls-open-one-drive').prop('checked') ||
				content.find('#user-link-preferences-pdf-open-one-drive').prop('checked') ||
				content.find('#user-link-preferences-image-open-one-drive').prop('checked') ||
				content.find('#user-link-preferences-pdf-force-web-open').prop('checked') ||
				content.find('#user-link-preferences-image-force-web-open').prop('checked'))
				content.find('.popup-blocker-warning').show();
			else
				content.find('.popup-blocker-warning').hide();
		};
	};
})(jQuery);
