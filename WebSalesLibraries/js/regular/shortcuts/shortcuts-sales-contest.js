(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsSalesContest = function () {
		let shortcutData = undefined;
		let servicePanel = undefined;

		let itemList = new ItemList();

		this.init = function (data) {
			shortcutData = data;
			shortcutData.options.trackActivityDelegate = trackActivity;

			$.SalesPortal.Content.fillContent({
				content: shortcutData.content,
				headerOptions: shortcutData.options.headerOptions,
				actions: shortcutData.actions,
				navigationPanel: shortcutData.navigationPanel,
				fixedPanels: shortcutData.fixedPanels,
				resizeCallback: updateContentSize
			});

			servicePanel = $.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .service-panel');

			let formLogger = new $.SalesPortal.FormLogger();
			formLogger.init({
				logObject: {name: shortcutData.options.headerTitle},
				formContent: servicePanel
			});

			if ($.cookie("showServicePanel") === "false")
				servicePanel.hide();
			else
				servicePanel.show();

			itemList.shortcutData = shortcutData;
			itemList.load(shortcutData.options.selectedItemId);

			initActionButtons();

			$.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .service-panel').resizable({
				resizeHeight: false,
				resize: function (event, ui) {
					updateContentSize();
				}
			});

			$(window).off('resize.sales-contest').on('resize.sales-contest', updateContentSize);
			updateContentSize();
		};

		let requestSaveItem = function(){
			let modalDialog = new $.SalesPortal.ModalDialog({
				title: 'Win of the Week',
				description: 'Submit your Win of the Week?',
				buttons: [
					{
						tag: 'yes',
						title: 'Yes! Submit!',
						width: 160,
						clickHandler: function () {
							modalDialog.close();
							submitItem();
						}
					},
					{
						tag: 'no',
						title: 'Not Now',
						width: 160,
						clickHandler: function () {
							modalDialog.close();
							saveItem();
						}
					}
				]
			});
			modalDialog.show();
		};

		let saveItem = function (isSubmit, onSuccessHandler) {
			if (itemList.selectedItem !== undefined)
			{
				let itemContent = $.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .item-content');
				let allowSave = itemContent.find(">div.editable").length > 0 || shortcutData.options.isAdminRole;
				if (allowSave)
				{
					let selectedPlatforms = [];
					$.each(itemContent.find('.sales-contest-item-details-platform:checked'), function () {
						let selectedPlatform = $(this).val();
						selectedPlatforms.push(selectedPlatform);
					});


					$.ajax({
						type: "POST",
						url: window.BaseUrl + "salesContest/saveItem",
						data: {
							selectedItemId: itemList.selectedItem.itemId,
							shortcutId: shortcutData.options.linkId,
							submit: isSubmit != undefined,
							title: itemList.selectedItem.itemTitle,
							advertiser: $('#sales-contest-item-advertiser').val(),
							revenue: $('#sales-contest-item-revenue').val(),
							content: {
								nominationType: itemContent.find('.sales-contest-item-nomination-type:checked').val(),
								seller: $('#sales-contest-item-seller').val(),
								market: $('#sales-contest-item-market').val(),
								station: $('#sales-contest-item-station').val(),
								revenueType: itemContent.find('.sales-contest-item-revenue-type:checked').val(),
								category: $('#sales-contest-item-category').val(),
								mediaRevenue: $('#sales-contest-item-revenue-media').val(),
								digitalRevenue: $('#sales-contest-item-revenue-digital').val(),
								productionRevenue: $('#sales-contest-item-revenue-production').val(),
								otherRevenue: $('#sales-contest-item-revenue-other').val(),
								platforms: selectedPlatforms,
								teamMembers: $('#sales-contest-item-team-members').val(),
								successStory: $('#sales-contest-item-success-story').val(),
								milestones: $('#sales-contest-item-milestones').val(),
							}
						},
						beforeSend: function () {
							$.SalesPortal.Overlay.show();
						},
						complete: function () {
							$.SalesPortal.Overlay.hide();
						},
						success: function () {
							if (onSuccessHandler != null)
								onSuccessHandler();
							else
							{
								$.fancybox({
									content: $('<div class="row" style="margin: 0;">' +
										'<div class="col-xs-3"><img src="' + window.BaseUrl + 'images/qpages/save.png">' +
										'</div>' +
										'<div class="col-xs-8 col-xs-offset-1">' +
										'<h3 style="margin-left: 0">Boo Yah!</h3>' +
										'<p class="text-muted">Your Data Saved</p>' +
										'</div>' +
										'</div>' +
										'<div class="row" style="margin: 0;"><div class="col-xs-12 text-center"><button type="button" class="btn btn-default" style="width: 80px; margin-top: 20px" onclick="$.fancybox.close()">OK</button></div></div>'),
									title: 'Item',
									width: 400,
									autoSize: false,
									autoHeight: true,
									openEffect: 'none',
									closeEffect: 'none',
									helpers: {
										title: false
									}
								});
							}

							itemList.load();
						},
						error: function () {
						},
						async: false,
						dataType: 'html'
					});
				}
			}
		};

		let submitItem = function () {
			if (itemList.selectedItem !== undefined && !itemList.selectedItem.isSubmitted)
			{
				let itemContent = $.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .item-content');
				let allowSave = itemContent.find(">div.editable").length > 0 || shortcutData.options.isAdminRole;
				if (allowSave)
				{
					saveItem(true, function () {
						$.fancybox({
							content: $('<div class="row" style="margin: 0;">' +
								'<div class="col-xs-3"><img style="width: 128px; height: 128px;" src="' + window.BaseUrl + 'images/qpages/submit.png">' +
								'</div>' +
								'<div class="col-xs-8 col-xs-offset-1">' +
								'<h3 style="margin-left: 0">Boom Diggity!</h3>' +
								'<p class="text-muted">Your Win of the Week has been sent...</p>' +
								'</div>' +
								'</div>' +
								'<div class="row" style="margin: 0;"><div class="col-xs-12 text-center"><button type="button" class="btn btn-default" style="width: 80px; margin-top: 20px" onclick="$.fancybox.close()">OK</button></div></div>'),
							title: 'Item',
							width: 400,
							autoSize: false,
							autoHeight: true,
							openEffect: 'none',
							closeEffect: 'none',
							helpers: {
								title: false
							}
						});
					});
				}
			}
		};

		let initActionButtons = function () {
			let shortcutActionsContainer = $('#shortcut-action-container');

			if ($.cookie("showServicePanel") === "true")
			{
				shortcutActionsContainer.find('.sales-contest-panel-show').hide();
			}
			else
				shortcutActionsContainer.find('.sales-contest-panel-hide').hide();
			shortcutActionsContainer.find('.sales-contest-panel-show').off('click.action').on('click.action', function () {
				servicePanel.show();
				shortcutActionsContainer.find('.sales-contest-panel-hide').show();
				$(this).hide();

				$.cookie("showServicePanel", true, {
					expires: (60 * 60 * 24 * 7)
				});

				updateContentSize();
			});
			shortcutActionsContainer.find('.sales-contest-panel-hide').off('click.action').on('click.action', function () {
				servicePanel.hide();
				shortcutActionsContainer.find('.sales-contest-panel-show').show();
				$(this).hide();

				$.cookie("showServicePanel", false, {
					expires: (60 * 60 * 24 * 7)
				});

				updateContentSize();
			});
			shortcutActionsContainer.find('.sales-contest-item-add').off('click.action').on('click.action', function () {
				itemList.addItem();
			});
			shortcutActionsContainer.find('.sales-contest-item-delete').off('click.action').on('click.action', function () {
				itemList.deleteItem();
			});
			shortcutActionsContainer.find('.sales-contest-item-save').off('click.action').on('click.action', function () {
				requestSaveItem();
			});
			shortcutActionsContainer.find('.sales-contest-item-submit').off('click.action').on('click.action', function () {
				submitItem();
			});

			let itemListButtons = $.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .service-panel .item-list-buttons');
			itemListButtons.find('.item-list-add').off('click.sales-contest').on('click.sales-contest', function () {
				itemList.addItem();
			});
			itemListButtons.find('.item-list-save').off('click.sales-contest').on('click.sales-contest', function () {
				requestSaveItem()
			});
			itemListButtons.find('.item-list-submit').off('click.sales-contest').on('click.sales-contest', function () {
				submitItem();
			});

			let contentButtons = $.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .content-panel .content-buttons');
			contentButtons.find('.item-save').off('click.sales-contest').on('click.sales-contest', function () {
				requestSaveItem()
			});
			contentButtons.find('.item-submit').off('click.sales-contest').on('click.sales-contest', function () {
				submitItem();
			});

			$.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .service-panel .item-list-tabs .nav-tabs a[data-toggle="tab"]').on('shown.bs.tab', function () {
				itemList.load();
			});
		};

		let updateContentSize = function () {
			let content = $.SalesPortal.Content.getContentObject();
			let navigationPanel = $.SalesPortal.Content.getNavigationPanel();

			let width = $(window).width() - navigationPanel.outerWidth(true) - 5;

			$('#content').css({
				'overflow': 'hidden'
			});

			content.css({
				'max-width': width + 'px',
				'width': width + 'px',
				'overflow': 'hidden'
			});

			$.SalesPortal.ShortcutsManager.updateContentSize();
			itemList.updateContentSize();
		};

		let trackActivity = function () {
			let activityData = $.parseJSON($('<div>' + shortcutData.options.serviceData + '</div>').find('.activity-data').text());
			$.SalesPortal.ShortcutsManager.trackActivity(
				activityData,
				'Sales Contest Activity',
				'Sales Contest Activity');
		};
	};

	let ItemList = function () {
		let that = this;

		let itemListTable = undefined;
		let itemListOrderState = undefined;

		this.selectedItem = undefined;
		this.shortcutData = undefined;

		this.load = function (itemToSelectId) {
			let itemListConditions = new ItemListCondition();

			if (itemToSelectId === undefined && that.selectedItem !== undefined)
				itemListConditions.selectedItemId = that.selectedItem.itemId;
			else
				itemListConditions.selectedItemId = itemToSelectId;

			itemListConditions.shortcutId = that.shortcutData.options.linkId;

			if ($('#sales-contest-item-list-all').hasClass('active'))
				itemListConditions.listType = 'all-items';
			else if ($('#sales-contest-item-list-archive').hasClass('active'))
				itemListConditions.listType = 'archive-items';
			else
				itemListConditions.listType = 'own-items';

			let itemList = $.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .service-panel .item-list-container');
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "salesContest/getItemList",
				data: itemListConditions.getConditionObject(),
				beforeSend: function () {
					itemList.html('');
					if (that.selectedItem !== undefined)
						that.selectedItem.clear();
					that.selectedItem = undefined;
					$.SalesPortal.Overlay.show();
				},
				complete: function () {
					$.SalesPortal.Overlay.hide();
				},
				success: function (data) {
					init(data);
				},
				error: function () {
				},
				async: true,
				dataType: 'json'
			});
		};

		this.addItem = function (parameters) {
			if (parameters === undefined)
				parameters = {};
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "salesContest/addItemDialog",
				data: {
					isCloning: parameters.templateItemId !== undefined
				},
				beforeSend: function () {
					$.SalesPortal.Overlay.show();
				},
				complete: function () {
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg) {
					let addItemContent = $(msg);

					let formLogger = new $.SalesPortal.FormLogger();
					formLogger.init({
						logObject: {name: that.shortcutData.options.headerTitle},
						formContent: addItemContent
					});

					addItemContent.find('.btn.accept-button').on('click.sales-contest', function () {
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "salesContest/addItem",
							data: {
								title: $('#add-item-name').val(),
								templateItemId: parameters.templateItemId
							},
							beforeSend: function () {
								$.SalesPortal.Overlay.show();
							},
							complete: function () {
								$.SalesPortal.Overlay.hide();
							},
							success: function (selectedItemId) {
								that.load(selectedItemId);
							},
							error: function () {
							},
							async: true,
							dataType: 'html'
						});
						$.fancybox.close();
					});
					addItemContent.find('.btn.cancel-button').on('click.sales-contest', function () {
						$.fancybox.close();
					});

					$.fancybox({
						content: addItemContent,
						title: parameters.templatePageId === undefined ? "Add Win of the Week!" : "Clone Win of the Week!",
						scrolling: 'no',
						autoSize: true,
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

		this.deleteItem = function (itemId) {
			let selectedItemId = that.selectedItem.itemId;
			if (itemId !== undefined)
				selectedItemId = itemId;
			if (selectedItemId != null)
			{
				let modalDialog = new $.SalesPortal.ModalDialog({
					title: 'Delete item',
					description: 'Are you SURE you want to delete selected item?',
					buttons: [
						{
							tag: 'yes',
							title: 'Yes',
							clickHandler: function () {
								modalDialog.close();
								$.ajax({
									type: "POST",
									url: window.BaseUrl + "salesContest/deleteItem",
									data: {
										selectedItemId: selectedItemId
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
			}
		};

		this.updateContentSize = function () {
			let itemListContainer = $.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .service-panel .item-list-container');
			let itemListButtons = $.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .service-panel .item-list-buttons');
			let itemListTabs = $.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .service-panel .item-list-tabs');

			let height = $.SalesPortal.Content.getContentObject().height()
				- itemListButtons.outerHeight(true)
				- itemListTabs.outerHeight(true)
				- 5;
			itemListContainer.css({
				'height': height + 'px'
			});

			if (that.selectedItem !== undefined)
				that.selectedItem.updateContentSize();
		};

		let init = function (data) {
			let itemListContainer = $.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .service-panel .item-list-container');

			let formLogger = new $.SalesPortal.FormLogger();
			formLogger.init({
				logObject: {name: that.shortcutData.options.headerTitle},
				formContent: itemListContainer
			});

			let tabHeaders = $.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .service-panel .item-list-tabs .nav-tabs');
			tabHeaders.find('a[href="#sales-contest-item-list-own"]').html('my nominations (' + data.info.ownItemsCount + ')');
			tabHeaders.find('a[href="#sales-contest-item-list-all"]').html('all nominations (' + data.info.allItemsCount + ')');
			tabHeaders.find('a[href="#sales-contest-item-list-archive"]').html('archive (' + data.info.archiveItemsCount + ')');

			let tableIdentifier = 'sales-contest-item-list';

			if (!$.SalesPortal.Content.isMobileDevice())
				itemListContainer.html('<table id="' + tableIdentifier + '" class="table item-list-table table-striped table-bordered"></table>');
			else
				itemListContainer.html('<table id="' + tableIdentifier + '" class="table item-list-table-table table-striped"></table>');

			let table = $("#" + tableIdentifier);

			let columnSettings = [];
			columnSettings.push({
				"data": "title",
				"title": "Nomination",
				"class": "",
				"width": "30%",
				"render": cellRenderer
			});
			columnSettings.push({
				"data": "advertiser",
				"title": "Advertiser",
				"class": "",
				"width": "30%",
				"render": cellRenderer
			});
			columnSettings.push({
				"data": "revenue",
				"title": "Dollars",
				"class": "",
				"width": "20%",
				"render": cellRenderer
			});
			columnSettings.push({
				"data": "dateSubmit",
				"title": "Submitted",
				"class": "",
				"width": "20%",
				"render": {
					_: cellRenderer,
					sort: 'value'
				}
			});
			columnSettings.push({
				"data": "id",
				"title": "Id",
				"visible": false,
				"searchable": false
			});

			$.extend($.fn.dataTableExt.oStdClasses, {
				"sFilterInput": "form-control",
			});

			jQuery.fn.dataTableExt.oSort['natural-asc'] = function (a, b) {
				return naturalSort(a, b);
			};

			jQuery.fn.dataTableExt.oSort['natural-desc'] = function (a, b) {
				return naturalSort(a, b) * -1;
			};

			if (itemListTable !== undefined)
				itemListTable.fnDestroy();
			itemListTable = table.dataTable({
				"data": data.dataset,
				columns: columnSettings,
				stateSave: false,
				paging: true,
				searching: true,
				autoWidth: false,
				order: itemListOrderState !== undefined ?
					[[itemListOrderState[0][0], itemListOrderState[0][1]]] :
					[[1, 'desc']],
				"scrollCollapse": false,
				"iDisplayLength": 500,
				"oLanguage": {
					"sEmptyTable": "",
					"sZeroRecords": "",
					"sSearch": "Filter:"
				},
				"dom": "<'row table-header-row'<'col-xs-12'f>>" +
					"<'row table-content-row'<'col-xs-12'tr>>",
				"createdRow": function (row, data, index) {
					$(row).addClass(tableIdentifier + '-row');
					$(row).addClass('item-data-row');
					if (data.isSelected)
						$(row).addClass('selected');
				}
			});

			table.on('order.dt', function () {
				itemListOrderState = itemListTable.api().order();
			});

			table.on('click', '.item-data-row', function () {
				$.SalesPortal.LinkManager.cleanupContextMenu();

				let row = $(this);
				if (!row.hasClass('selected'))
				{
					table.find('tr.item-data-row').removeClass('selected');
					row.addClass('selected');
					let rowObject = itemListTable.api().row(row);
					let itemId = rowObject.data().id;
					let itemTitle = rowObject.data().title;
					openItemInternal(
						itemId,
						itemTitle
					);
				}
			});

			table.on('contextmenu', '.item-data-row', function (event) {
				let row = $(this);
				let rowObject = itemListTable.api().row(row);

				$.SalesPortal.LinkManager.cleanupContextMenu();

				if (rowObject.data().allowEdit || that.shortcutData.options.isAdminRole)
				{
					let menu = $('<ul class="dropdown-menu context-menu-content logger-form" role="menu" data-log-group="Sales Contest" data-log-action="Sales Contest Activity">' +
						'<li><a tabindex="-1" href="#" class="menu-item log-action" data-action-tag="clone">Clone</a></li>' +
						'<li><a tabindex="-1" href="#" class="menu-item log-action" data-action-tag="delete">Delete</a></li>' +
						'</ul>');
					$('body').append(menu);

					let formLogger = new $.SalesPortal.FormLogger();
					formLogger.init({
						logObject: {name: that.shortcutData.options.headerTitle},
						formContent: menu
					});

					menu
						.show()
						.css({
							position: "absolute",
							left: $.SalesPortal.LinkManager.getMenuPosition(menu, event.clientX, 'width', 'scrollLeft'),
							top: $.SalesPortal.LinkManager.getMenuPosition(menu, event.clientY, 'height', 'scrollTop')
						})
						.off('click')
						.on('click', 'a.menu-item', function () {
							menu.hide();
							let tag = $(this).data('action-tag');
							switch (tag)
							{
								case 'clone':
									that.addItem({templateItemId: rowObject.data().id});
									break;
								case 'delete':
									that.deleteItem(rowObject.data().id);
									break;
							}
						});
				}
				return false;
			});

			if (table.find('tr.item-data-row.selected').length === 0)
				table.find('tr.item-data-row').first().addClass('selected');

			if (table.find('tr.item-data-row').length > 0)
			{
				let selectedRow = table.find('tr.item-data-row.selected');
				let rowObject = itemListTable.api().row(selectedRow);
				openItemInternal(
					rowObject.data().id,
					rowObject.data().title
				);
			}
			else
			{
				$.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .content-buttons').hide();
				$.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .item-content').html('');
			}
		};

		let openItemInternal = function (selectedItemId, selectedItemTitle) {
			if (selectedItemId == undefined)
				selectedItemId = that.selectedItem.itemId;
			if (selectedItemTitle == undefined)
				selectedItemTitle = that.selectedItem.itemTitle;
			that.selectedItem = new ItemContent({
				selectedItemId: selectedItemId,
				selectedItemTitle: selectedItemTitle,
				shortcutData: that.shortcutData
			});
		};

		let cellRenderer = function (data, type, row, meta) {
			if (type === "display")
			{
				let displayValue = '';
				if (row && row !== '')
				{
					if (meta.col == 0 && row.itemOwner != '')
					{
						displayValue = '<span>' + row.title + '</span><span class="user-name">' + row.itemOwner + '</span>';
					}
					else
					{
						displayValue = data && typeof data === 'object' ? data.display : data;
						displayValue = displayValue !== null ? displayValue : '';
					}
				}
				return '<span class="clickable-area" title="' + row.tooltip + '">' + displayValue + '</span>';
			}
			else
				return data;
		};

		let naturalSort = function (a, b) {
			// setup temp-scope variables for comparison evauluation
			let x = a.toString().toLowerCase() || '', y = b.toString().toLowerCase() || '',
				nC = String.fromCharCode(0),
				xN = x.replace(/([-]{0,1}[0-9.]{1,})/g, nC + '$1' + nC).split(nC),
				yN = y.replace(/([-]{0,1}[0-9.]{1,})/g, nC + '$1' + nC).split(nC),
				xD = (new Date(x)).getTime(), yD = (new Date(y)).getTime();
			// natural sorting of dates
			if (xD && yD && xD < yD)
				return -1;
			else if (xD && yD && xD > yD)
				return 1;
			// natural sorting through split numeric strings and default strings
			for (let cLoc = 0, numS = Math.max(xN.length, yN.length); cLoc < numS; cLoc++)
				if ((parseFloat(xN[cLoc]) || xN[cLoc]) < (parseFloat(yN[cLoc]) || yN[cLoc]))
					return -1;
				else if ((parseFloat(xN[cLoc]) || xN[cLoc]) > (parseFloat(yN[cLoc]) || yN[cLoc]))
					return 1;
			return 0;
		};
	};

	let ItemListCondition = function (source) {
		let that = this;

		this.selectedItemId = undefined;
		this.shortcutId = undefined;
		this.listType = undefined;

		if (source !== undefined)
			for (let prop in source)
				if (source.hasOwnProperty(prop))
					this[prop] = source[prop];

		this.getConditionObject = function () {
			return {
				selectedItemId: that.selectedItemId,
				shortcutId: that.shortcutId,
				listType: that.listType,
			};
		}
	};

	let ItemContent = function (parameters) {
		let that = this;
		let dateFormat = 'MM/DD/YYYY';

		this.itemId = parameters.selectedItemId;
		this.itemTitle = parameters.selectedItemTitle;
		this.isSubmitted = false;
		let shortcutData = parameters.shortcutData;

		this.clear = function () {
			$.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .content-buttons').hide();
			$.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .item-content').html('');
		};

		this.updateContentSize = function () {
			let itemContent = $.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .item-content > div');
			let contentButtons = $.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .content-buttons');
			let height = $.SalesPortal.Content.getContentObject().height() - contentButtons.outerHeight(true);
			itemContent.css({
				'height': height + 'px'
			});
		};

		let load = function () {
			let itemContent = $.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .item-content');
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "salesContest/getItemContent",
				data: {
					shortcutId: shortcutData.options.linkId,
					selectedItemId: that.itemId,
					isAdminRole: shortcutData.options.isAdminRole,
				},
				beforeSend: function () {
					that.clear();
					$.SalesPortal.Overlay.show();
				},
				complete: function () {
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg) {
					itemContent.html(msg);
					afterLoad();
				},
				error: function () {
				},
				async: true,
				dataType: 'html'
			});
		};

		let loadFiles = function (filesContainer, fileType, afterLoadCallback) {
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "salesContest/getItemFiles",
				data: {
					itemId: that.itemId,
					fileType: fileType
				},
				beforeSend: function () {
					filesContainer.html('');
					$.SalesPortal.Overlay.show();
				},
				complete: function () {
					$.SalesPortal.Overlay.hide();
				},
				success: function (content) {
					filesContainer.html(content);
					afterLoadCallback();
				},
				error: function () {
				},
				async: true,
				dataType: 'html'
			});
		};

		let afterLoad = function () {
			let itemContent = $.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .item-content');

			let formLogger = new $.SalesPortal.FormLogger();
			formLogger.init({
				logObject: {name: shortcutData.options.headerTitle},
				formContent: itemContent
			});

			that.isSubmitted = itemContent.find('.submit-data .submitted-by').length > 0;
			let actionMenuSubmitButton = $('#shortcut-action-container').find('.sales-contest-item-submit');
			let itemListSubmitButton = $.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .service-panel .item-list-buttons .item-list-submit');
			let bottomBarSubmitButton = $.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .content-panel .content-buttons .item-submit');
			if (that.isSubmitted)
			{
				if (!bottomBarSubmitButton.hasClass('disabled'))
					bottomBarSubmitButton.addClass('disabled');
				itemListSubmitButton.hide();
				actionMenuSubmitButton.hide();
			}
			else
			{
				bottomBarSubmitButton.removeClass('disabled');
				itemListSubmitButton.show();
				actionMenuSubmitButton.show();
			}

			$('#sales-contest-item-revenue').off('keypress.sales-contest').on('keypress.sales-contest',validateNumbers);

			itemContent.find('.sales-contest-item-market-container .dropdown-menu a').click(function () {
				$('#sales-contest-item-market').val($(this).data('value'));
			});

			itemContent.find('.sales-contest-item-station-container .dropdown-menu a').click(function () {
				$('#sales-contest-item-station').val($(this).data('value'));
			});

			itemContent.find('.sales-contest-item-category-container .dropdown-menu a').click(function () {
				$('#sales-contest-item-category').val($(this).data('value'));
			});
			$('#sales-contest-item-revenue-digital').off('keypress.sales-contest').on('keypress.sales-contest',validateNumbers);
			$('#sales-contest-item-revenue-media').off('keypress.sales-contest').on('keypress.sales-contest',validateNumbers);
			$('#sales-contest-item-revenue-production').off('keypress.sales-contest').on('keypress.sales-contest',validateNumbers);
			$('#sales-contest-item-revenue-other').off('keypress.sales-contest').on('keypress.sales-contest',validateNumbers);

			Dropzone.autoDiscover = false;
			let allowEditAttachments = itemContent.find(">div.editable").length > 0 || shortcutData.options.isAdminRole;
			if (allowEditAttachments)
			{
				let attachmentsContainer = $("#sales-contest-item-attachments");
				let attachmentsDataContainer = $("#sales-contest-item-attachments-data");
				let dropZoneObject = undefined;
				attachmentsDataContainer.dropzone({
					url: window.BaseUrl + "salesContest/uploadFile?itemId=" + that.itemId + "&fileType=attachment",
					dictDefaultMessage: "",
					previewTemplate: "<div></div>",
					init: function () {
						dropZoneObject = this;
					},
					sending: function () {
						attachmentsContainer.find('.progress-bar').css({
							width: 0
						});
						attachmentsContainer.find('.progress').show();
					},
					accept: function (file, done) {
						if (file.size > parseInt(shortcutData.options.maxFileSize) * 1024 * 1024)
						{
							dropZoneObject.removeFile(file);
							var modalDialog = new $.SalesPortal.ModalDialog({
								title: 'File Too BIG?',
								description: shortcutData.options.maxFileSizeExcessMessage,
								buttons: [
									{
										tag: 'ok',
										title: 'Close',
										width: 160,
										clickHandler: function () {
											modalDialog.close();
										}
									}
								]
							});
							modalDialog.show();
						}
						else
						{
							done();
						}
					},
					complete: function () {
						attachmentsContainer.find('.progress').hide();
					},
					success: function () {
						loadFiles(attachmentsDataContainer, 'attachment', afterAttachmentsLoad);
					},
					uploadprogress: function (e, progress) {
						attachmentsContainer.find('.progress-bar').css({
							width: progress + "%"
						});
					}
				});
			}
			afterAttachmentsLoad();

			$.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .content-buttons').show();
			that.updateContentSize();
		};

		let afterAttachmentsLoad = function () {
			let itemContent = $.SalesPortal.Content.getContentObject().find('.sales-contest-main-page .item-content');
			let attachmentsDataContainer = $("#sales-contest-item-attachments-data");

			attachmentsDataContainer.find('.file-item .file-name').off('click').on('click', function () {
				let fileId = $(this).closest('.file-item').find('.service-data .file-id').text();
				let form = document.getElementById('form-download-sales-contest-file');
				if (form === null)
				{
					form = document.createElement("form");
					form.setAttribute("id", "form-download-sales-contest-file");
					form.setAttribute("method", "post");
					form.setAttribute("action", window.BaseUrl + 'salesContest/downloadFile');
					form._submit_function_ = form.submit;

					let hiddenField = document.createElement("input");
					hiddenField.setAttribute("id", "input-sales-contest-file-id");
					hiddenField.setAttribute("type", "hidden");
					hiddenField.setAttribute("name", 'fileId');
					form.appendChild(hiddenField);
					document.body.appendChild(form);
				}
				document.getElementById('input-sales-contest-file-id').setAttribute("value", fileId);
				form._submit_function_();
			});

			attachmentsDataContainer.find('.draggable').off('dragstart').on('dragstart', function (e) {
				let urlHeader = $(this).data("url-header");
				let url = $(this).data('url');
				if (url !== '')
					e.originalEvent.dataTransfer.setData(urlHeader, url);
			});

			let allowEdit = itemContent.find(">div.editable").length > 0 || shortcutData.options.isAdminRole;
			if (allowEdit)
			{
				attachmentsDataContainer.find('.file-item .file-delete').off('click').on('click', function () {
					let fileId = $(this).closest('.file-item').find('.service-data .file-id').text();
					$.ajax({
						type: "POST",
						url: window.BaseUrl + "salesContest/deleteFile",
						data: {
							fileId: fileId,
						},
						beforeSend: function () {
							$.SalesPortal.Overlay.show();
						},
						complete: function () {
							$.SalesPortal.Overlay.hide();
						},
						success: function () {
							loadFiles(attachmentsDataContainer, "attachment", afterAttachmentsLoad);
						},
						error: function () {
						},
						async: true,
						dataType: 'html'
					});
				});
			}
			else
				attachmentsDataContainer.find('.file-item .file-delete').hide();
		};

		let validateNumbers =  function validate(evt) {
			let theEvent = evt || window.event;

			if ($.inArray(theEvent.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
				// Allow: Ctrl+A
				(theEvent.keyCode == 65 && (theEvent.ctrlKey === true || theEvent.metaKey === true)) ||
				// Allow: Ctrl+C
				(theEvent.keyCode == 67 && (theEvent.ctrlKey === true || theEvent.metaKey === true)) ||
				// Allow: Ctrl+X
				(theEvent.keyCode == 88 && (theEvent.ctrlKey === true || theEvent.metaKey === true)) ||
				// Allow: home, end, left, right
				(theEvent.keyCode >= 35 && theEvent.keyCode <= 39) ||
				//Allow numbers and numbers + shift key
				((theEvent.shiftKey && (theEvent.keyCode >= 48 && theEvent.keyCode <= 57)) || (theEvent.keyCode >= 96 && theEvent.keyCode <= 105)))
			{
				// let it happen, don't do anything
				return;
			}

			// Handle paste
			let key = '';
			if (theEvent.type === 'paste') {
				key = event.clipboardData.getData('text/plain');
			} else {
				// Handle key press
				key = theEvent.keyCode || theEvent.which;
				key = String.fromCharCode(key);
			}
			let regex = /[0-9]|\./;
			if( !regex.test(key) ) {
				theEvent.returnValue = false;
				if(theEvent.preventDefault) theEvent.preventDefault();
			}
		};

		load();
	};
})(jQuery);
