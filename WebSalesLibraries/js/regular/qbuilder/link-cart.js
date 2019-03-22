(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.QBuilder = $.SalesPortal.QBuilder || {};
	var LinkCartManager = function () {
		var that = this;

		this.qBuilderData = undefined;

		this.init = function () {
			$('#link-cart-refresh').off('click.link-cart').on('click.link-cart', function () {
				ga('send', {
					hitType: 'pageview',
					title: "Link Cart/" + $(this).text(),
					location: window.BaseUrl + "/linkcart/" + $(this).text(),
					page: "Link Cart/" + $(this).text()
				});
				that.load();
			});
			$('#link-cart-clear').off('click.link-cart').on('click.link-cart', function () {
				ga('send', {
					hitType: 'pageview',
					title: "Link Cart/" + $(this).text(),
					location: window.BaseUrl + "/linkcart/" + $(this).text(),
					page: "Link Cart/" + $(this).text()
				});
				clear();
			});
			$('#link-cart-add-new-page').off('click.link-cart').on('click.link-cart', function () {
				ga('send', {
					hitType: 'pageview',
					title: "Link Cart/" + $(this).text(),
					location: window.BaseUrl + "/linkcart/" + $(this).text(),
					page: "Link Cart/" + $(this).text()
				});
				addNewPageWithLinks();
			});
			$('#link-cart-add-all').off('click.link-cart').on('click.link-cart', function () {
				that.load();
				addAllLinksToPage();
				ga('send', {
					hitType: 'pageview',
					title: "Link Cart/" + $(this).text(),
					location: window.BaseUrl + "/linkcart/" + $(this).text(),
					page: "Link Cart/" + $(this).text()
				})
			});
		};

		this.load = function () {
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qBuilder/getLinkCart",
				data: {},
				beforeSend: function () {
					$.SalesPortal.Overlay.show();
				},
				complete: function () {
					$.SalesPortal.Overlay.hide();
				},
				success: function (result) {
					lincCartTable.init({
						dataset: result.links,
						dataViewOptions: result.viewOptions
					});
					initLinks();
				},
				error: function () {
				},
				async: true,
				dataType: 'json'
			});
		};

		this.show = function () {
			$.cookie("showLinkCart", true, {
				expires: (60 * 60 * 24 * 7)
			});
			$('#link-cart').show();
		};

		this.hide = function () {
			$.cookie("showLinkCart", false, {
				expires: (60 * 60 * 24 * 7)
			});
			$('#link-cart').hide();
		};

		this.addLinks = function (linkIds) {
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qBuilder/addLinksToCart",
				data: {
					linkIds: linkIds
				},
				beforeSend: function () {
					$.SalesPortal.Overlay.show();
				},
				complete: function () {
					$.SalesPortal.Overlay.hide();
				},
				success: function () {
					var succesDescription = '';
					if (linkIds.length > 1)
						succesDescription = 'Links was added to Link Cart';
					else
						succesDescription = 'Link was added to Link Cart';
					var modalDialog = new $.SalesPortal.ModalDialog({
						title: 'Success!',
						logo: window.BaseUrl + 'images/qpages/add-complete.png',
						description: succesDescription,
						buttons: [
							{
								tag: 'open_qbuilder',
								title: 'Open QuickSites',
								width: 150,
								clickHandler: function () {
									modalDialog.close();
									$.SalesPortal.ShortcutsManager.openStaticShortcutByType('qbuilder', {showLinkCart: true});
								}
							},
							{
								tag: 'close',
								title: 'Return to Site',
								width: 150,
								clickHandler: function () {
									modalDialog.close();
								}
							}
						]
					});
					modalDialog.show();
				},
				error: function () {
				},
				async: true,
				dataType: 'html'
			});
		};

		this.addFolder = function (folderId) {
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qBuilder/prepareFolderToAddToLinkCart",
				data: {
					folderId: folderId
				},
				beforeSend: function () {
					$.SalesPortal.Overlay.show();
				},
				complete: function () {
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg) {
					var prepareLinkCartContent = $(msg);

					var folderName = prepareLinkCartContent.find('.service-data .folder-name').text();

					var formLogger = new $.SalesPortal.FormLogger();
					formLogger.init({
						logObject: {
							name: folderName
						},
						formContent: prepareLinkCartContent
					});

					prepareLinkCartContent.find('#prepare-link-cart-select-links').on('change', function () {
						prepareLinkCartContent.find('.link-item.link input').prop('checked', $(this).prop('checked'));
					});
					prepareLinkCartContent.find('#prepare-link-cart-select-linebreaks').on('change', function () {
						prepareLinkCartContent.find('.link-item.line-break input').prop('checked', $(this).prop('checked'));
					});

					prepareLinkCartContent.find('.btn.accept-button').on('click.preview', function () {
						var selectedLinkIds = [];
						prepareLinkCartContent.find('.link-item input:checked').each(function () {
							selectedLinkIds.push($(this).val());
						});
						$.fancybox.close();
						that.addLinks(selectedLinkIds);
					});
					prepareLinkCartContent.find('.btn.cancel-button').on('click.preview', function () {
						$.fancybox.close();
					});
					$.fancybox({
						content: prepareLinkCartContent,
						width: 430,
						scrolling: 'no',
						autoSize: false,
						autoHeight: true,
						openEffect: 'none',
						closeEffect: 'none'
					});
				},
				error: function () {
				},
				async: true,
				dataType: 'html'
			});
		};

		this.updateContentSize = function () {
			var navigationPanel = $.SalesPortal.Content.getNavigationPanel();
			var servicePanel = $('#service-panel');
			var pageContent = $('#page-content');

			var width = $(window).width() - navigationPanel.outerWidth(true) - pageContent.outerWidth(true);
			var height = $.SalesPortal.Content.getContentObject().height() -
				servicePanel.find('.headers').outerHeight(true) -
				$('#link-cart-buttons').outerHeight(true) - 20;

			servicePanel.css({
				'max-width': width + 'px'
			});

			$('#link-cart-grid').css({
				'width': width + 'px',
				'height': height + 'px'
			});

			lincCartTable.updateSize();
		};

		var initLinks = function () {
			var linkCart = $('#link-cart');

			var formLogger = new $.SalesPortal.FormLogger();
			formLogger.init({
				logObject: {name: that.qBuilderData.options.headerTitle},
				formContent: linkCart
			});

			linkCart.find('.link-cart-data-table-content-row').draggable({
					revert: "invalid",
					distance: 70,
					delay: 500,
					helper: function () {
						var linkInCartId = lincCartTable.getTable().row($(this)).data().extended_data.linkInCartId;
						return $('<span id="' + linkInCartId + '" class="glyphicon glyphicon-file"></span>');
					},
					appendTo: "body",
					cursorAt: {left: -10, top: 0}
				}
			);

			that.updateContentSize();
		};

		var clear = function () {
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qBuilder/clearLinkCart",
				data: {},
				beforeSend: function () {
					$.SalesPortal.Overlay.show();
				},
				complete: function () {
					$.SalesPortal.Overlay.hide();
				},
				success: function () {
					that.load();
				},
				error: function () {
				},
				async: true,
				dataType: 'html'
			});
		};

		var deleteLink = function (linkInCartId) {
			var modalDialog = new $.SalesPortal.ModalDialog({
				title: 'Delete Link',
				description: 'Are you SURE you want to delete selected link from Cart?',
				buttons: [
					{
						tag: 'yes',
						title: 'Yes',
						clickHandler: function () {
							modalDialog.close();
							$.ajax({
								type: "POST",
								url: window.BaseUrl + "qBuilder/deleteLinkFromCart",
								data: {
									linkInCartId: linkInCartId
								},
								beforeSend: function () {
									$.SalesPortal.Overlay.show();
								},
								complete: function () {
									$.SalesPortal.Overlay.hide();
									that.load();
								},
								async: true,
								dataType: 'html'
							});
						}
					},
					{
						tag: 'no',
						title: 'No',
						clickHandler: function () {
							modalDialog.close();
						}
					}
				]
			});
			modalDialog.show();
		};

		var addNewPageWithLinks = function () {
			$.SalesPortal.QBuilder.PageList.addPage({
				populateFromLinkCart: true
			});
		};

		var addAllLinksToPage = function () {
			var selectedPageId = $.SalesPortal.QBuilder.PageList.selectedPage.pageId;
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qBuilder/addAllLinksToPage",
				data: {
					pageId: selectedPageId
				},
				beforeSend: function () {
					$.SalesPortal.Overlay.show();
				},
				complete: function () {
					$.SalesPortal.Overlay.hide();
				},
				success: function () {
					that.load();
					$.SalesPortal.QBuilder.PageList.selectedPage.loadLinks();
				},
				error: function () {
				},
				async: true,
				dataType: 'html'
			});
		};

		var trackActivity = function () {
			var activityData = $.parseJSON($('<div>' + that.qBuilderData.options.serviceData + '</div>').find('.activity-data').text());
			$.SalesPortal.ShortcutsManager.trackActivity(
				activityData,
				'QBuilder',
				'QBuilder Activity');
		};

		var lincCartTable = new $.SalesPortal.SearchDataTable(
			{
				tableIdentifier: 'link-cart-data-table-content',
				tableContainerSelector: '#link-cart-grid',
				parentContainerSelector: '#link-cart-grid',
				saveState: false,
				paginate: false,
				subSearch: false,
				excelExport: false,
				deleteHandler: function (linkInfo) {
					deleteLink(linkInfo.extended_data.linkInCartId);
				},
				logHandler: trackActivity
			}
		);
	};
	$.SalesPortal.QBuilder.LinkCart = new LinkCartManager();
})(jQuery);
