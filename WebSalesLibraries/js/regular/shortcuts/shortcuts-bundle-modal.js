(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsBundleModal = function () {
		let bundleData = undefined;
		let modalContainer = undefined;

		this.load = function (data) {
			bundleData = data;

			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/getShortcutBundleDialog",
				data: {
					shortcutId: bundleData.shortcutId
				},
				beforeSend: function () {
					$.SalesPortal.Overlay.show();
				},
				complete: function () {
					$.SalesPortal.Overlay.hide();
				},
				success: function (data) {
					$.fancybox({
						content: data.content,
						title: 'Bundle',
						width: $(window).width() * 0.8,
						height: $(window).height() * 0.8,
						autoSize: false,
						openEffect: 'none',
						closeEffect: 'none',
						helpers: {
							title: false
						},
						afterShow: function () {
							modalContainer = $('.fancybox-inner');

							modalContainer.find('.tab-page-container .nav-tabs a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
								let targetId = $(e.target).attr("href");
								modalContainer.find('.tab-page-container .tab-logo-container img').prop('src', $(targetId).find('.service-data .tab-logo').text());
							});

							initializeBundleItems(modalContainer.find('.left-panel .items-container'));
							$.each(modalContainer.find('.tab-page-container .items-container'), function () {
								initializeBundleItems($(this));
							});

							updateSize();
						}
					});
				},
				error: function () {
					$.SalesPortal.Overlay.hide();
				},
				async: true,
				dataType: 'json'
			});
		};

		let initializeBundleItems = function (bundleContainer) {
			$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(bundleContainer, function () {
				$.fancybox.close();
			});

			bundleContainer.find('.tab-toggle-link').off('click').on('click', function (e) {
				e.preventDefault();
				let tabId = $(this).find('.service-data .tab-id').text();
				modalContainer.find('.tab-page-container .nav-tabs a[href$="#' + tabId + '"]').tab('show');
			});

			bundleContainer.find('.bundle-modal-link').on('contextmenu', function (event) {
				$.SalesPortal.LinkManager.cleanupContextMenu();

				var itemId = $(this).find('.service-data .bundle-item-id').text();
				var itemType = $(this).find('.service-data .bundle-item-type').text();
				var itemContent = $(this).find('.service-data .bundle-encoded-item').text();

				let menu = undefined;
				if (bundleContainer.hasClass('favorites-page'))
				{
					menu = $('<ul class="dropdown-menu context-menu-content logger-form" role="menu">' +
						'<li><a tabindex="-1" href="#" class="menu-item" data-action-tag="delete">Delete</a></li>' +
						'</ul>');
				}
				else
				{
					menu = $('<ul class="dropdown-menu context-menu-content logger-form" role="menu">' +
						'<li><a tabindex="-1" href="#" class="menu-item" data-action-tag="add">Add to Favorites</a></li>' +
						'</ul>');
				}
				$('body').append(menu);

				menu
					.show()
					.css({
						position: "absolute",
						left: $.SalesPortal.LinkManager.getMenuPosition(menu, event.clientX, 'width', 'scrollLeft'),
						top: $.SalesPortal.LinkManager.getMenuPosition(menu, event.clientY, 'height', 'scrollTop'),
						'z-index': 10000
					})
					.off('click')
					.on('click', 'a.menu-item', function () {
						menu.hide();
						let tag = $(this).data('action-tag');
						switch (tag)
						{
							case 'add':
								$.ajax({
									type: "POST",
									url: window.BaseUrl + "shortcuts/addBundleModalFavoriteItem",
									data: {
										itemId: itemId,
										itemType: itemType,
										itemContent: itemContent
									},
									beforeSend: function () {
										$.SalesPortal.Overlay.show();
									},
									complete: function () {
										$.SalesPortal.Overlay.hide();
									},
									success: function () {
										reloadFavorites();
									},
									error: function () {
										$.SalesPortal.Overlay.hide();
									},
									async: true,
									dataType: 'json'
								});
								break;
							case 'delete':
								$.ajax({
									type: "POST",
									url: window.BaseUrl + "shortcuts/deleteBundleModalFavoriteItem",
									data: {
										itemId: itemId
									},
									beforeSend: function () {
										$.SalesPortal.Overlay.show();
									},
									complete: function () {
										$.SalesPortal.Overlay.hide();
									},
									success: function () {
										reloadFavorites();
									},
									error: function () {
										$.SalesPortal.Overlay.hide();
									},
									async: true,
									dataType: 'json'
								});
								break;
						}
					});
				return false;
			});
		};

		let reloadFavorites = function(){
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/getBundleModalFavoriteItems",
				data: {
					shortcutId: bundleData.shortcutId
				},
				beforeSend: function () {
					$.SalesPortal.Overlay.show();
				},
				complete: function () {
					$.SalesPortal.Overlay.hide();
				},
				success: function (content) {
					let favoritesPage = modalContainer.find('.tab-page-container .favorites-page');
					favoritesPage.find('.page-content').html(content);
					initializeBundleItems(favoritesPage);
					modalContainer.find('.tab-page-container .nav-tabs a[href$="#bundle-modal-favorites"]').tab('show');
				},
				error: function () {
					$.SalesPortal.Overlay.hide();
				},
				async: true,
				dataType: 'html'
			});
		};

		let updateSize = function () {
			$('.fancybox-skin').css({
				'padding': 0
			});

			let innerContent = $('.fancybox-inner');

			let modalHeight = innerContent.outerHeight(true);

			innerContent.find('.left-panel .items-container').css({
				'height': (modalHeight - 5) + 'px'
			});

			let tabPageHeight = modalHeight -
				innerContent.find('.tab-page-container .tab-logo-container').outerHeight(true) -
				innerContent.find('.tab-page-container .nav-tabs').outerHeight(true) - 5;

			innerContent.find('.tab-page-container .tab-pane').css({
				'height': tabPageHeight + 'px'
			});
		};
	};
})(jQuery);
