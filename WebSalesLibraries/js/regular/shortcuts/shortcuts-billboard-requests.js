(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsBillboardRequests = function () {
		var shortcutData = undefined;
		var servicePanel = undefined;

		var itemList = new ItemList();

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

			servicePanel = $.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .service-panel');

			var formLogger = new $.SalesPortal.FormLogger();
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

			$.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .service-panel').resizable({
				resizeHeight: false,
				resize: function (event, ui) {
					updateContentSize();
				}
			});

			$(window).off('resize.billboard-requests').on('resize.billboard-requests', updateContentSize);
			updateContentSize();
		};

		var requestSaveItem = function () {
			if(!itemList.selectedItem.isSubmitted)
			{
				var modalDialog = new $.SalesPortal.ModalDialog({
					title: 'Save Request',
					description: 'Do you want to SUBMIT this Request to your Research Team Now?',
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
			}
			else
				saveItem();
		};

		var saveItem = function (onSuccessHandler) {
			if (itemList.selectedItem !== undefined)
			{
				var itemContent = $.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .item-content');
				var allowSave = itemContent.find(">div.editable").length > 0 || shortcutData.options.isAdminRole;
				if (allowSave)
				{
					var status = $('#billboard-requests-item-status').val();

					$.ajax({
						type: "POST",
						url: window.BaseUrl + "billboardRequests/saveItem",
						data: {
							selectedItemId: itemList.selectedItem.itemId,
							shortcutId: shortcutData.options.linkId,
							title: itemList.selectedItem.itemTitle,
							status: status,
							assignedTo: $('#billboard-requests-item-assigned-to').val(),
							dateNeeded: $('#billboard-requests-item-date-needed').val(),
							dateCompleted: status == "complete" ?
								$('#billboard-requests-item-date-complete').val() :
								undefined,
							content: {
								submittedByUserId: itemContent.find('.submit-data .submitted-by').text(),
								advertiser: $('#billboard-requests-item-advertiser').val(),
								agency: $('#billboard-requests-item-agency').val(),
								length: $('#billboard-requests-item-length').val(),
								property: $('#billboard-requests-item-property').val(),
								details: $('#billboard-requests-item-details-data').val()
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

		var submitItem = function () {
			if (itemList.selectedItem !== undefined && !itemList.selectedItem.isSubmitted)
			{
				var itemContent = $.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .item-content');
				var allowSave = itemContent.find(">div.editable").length > 0 || shortcutData.options.isAdminRole;
				if (allowSave)
				{
					$('#billboard-requests-item-status').val('submitted');
					saveItem(function () {
						$.fancybox({
							content: $('<div class="row" style="margin: 0;">' +
								'<div class="col-xs-3"><img style="width: 128px; height: 128px;" src="' + window.BaseUrl + 'images/qpages/submit.png">' +
								'</div>' +
								'<div class="col-xs-8 col-xs-offset-1">' +
								'<h3 style="margin-left: 0">Boom Diggity!</h3>' +
								'<p class="text-muted">Your request has been sent...</p>' +
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

		var initActionButtons = function () {
			var shortcutActionsContainer = $('#shortcut-action-container');

			if ($.cookie("showServicePanel") === "true")
			{
				shortcutActionsContainer.find('.billboard-requests-panel-show').hide();
			}
			else
				shortcutActionsContainer.find('.billboard-requests-panel-hide').hide();
			shortcutActionsContainer.find('.billboard-requests-panel-show').off('click.action').on('click.action', function () {
				servicePanel.show();
				shortcutActionsContainer.find('.billboard-requests-panel-hide').show();
				$(this).hide();

				$.cookie("showServicePanel", true, {
					expires: (60 * 60 * 24 * 7)
				});

				updateContentSize();
			});
			shortcutActionsContainer.find('.billboard-requests-panel-hide').off('click.action').on('click.action', function () {
				servicePanel.hide();
				shortcutActionsContainer.find('.billboard-requests-panel-show').show();
				$(this).hide();

				$.cookie("showServicePanel", false, {
					expires: (60 * 60 * 24 * 7)
				});

				updateContentSize();
			});
			shortcutActionsContainer.find('.billboard-requests-item-add').off('click.action').on('click.action', function () {
				itemList.addItem();
			});
			shortcutActionsContainer.find('.billboard-requests-item-delete').off('click.action').on('click.action', function () {
				itemList.deleteItem();
			});
			shortcutActionsContainer.find('.billboard-requests-item-save').off('click.action').on('click.action', function () {
				requestSaveItem();
			});
			shortcutActionsContainer.find('.billboard-requests-item-submit').off('click.action').on('click.action', function () {
				submitItem();
			});

			var itemListButtons = $.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .service-panel .item-list-buttons');
			itemListButtons.find('.item-list-add').off('click.billboard-requests').on('click.billboard-requests', function () {
				itemList.addItem();
			});
			itemListButtons.find('.item-list-save').off('click.billboard-requests').on('click.billboard-requests', function () {
				requestSaveItem()
			});
			itemListButtons.find('.item-list-submit').off('click.billboard-requests').on('click.billboard-requests', function () {
				submitItem();
			});

			var contentButtons = $.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .content-panel .content-buttons');
			contentButtons.find('.item-save').off('click.billboard-requests').on('click.billboard-requests', function () {
				requestSaveItem()
			});
			contentButtons.find('.item-submit').off('click.billboard-requests').on('click.billboard-requests', function () {
				submitItem();
			});

			$.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .service-panel .item-list-tabs .nav-tabs a[data-toggle="tab"]').on('shown.bs.tab', function () {
				itemList.load();
			});
		};

		var updateContentSize = function () {
			var content = $.SalesPortal.Content.getContentObject();
			var navigationPanel = $.SalesPortal.Content.getNavigationPanel();

			var width = $(window).width() - navigationPanel.outerWidth(true) - 5;

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

		var trackActivity = function () {
			var activityData = $.parseJSON($('<div>' + shortcutData.options.serviceData + '</div>').find('.activity-data').text());
			$.SalesPortal.ShortcutsManager.trackActivity(
				activityData,
				'Billboard Request Activity',
				'Billboard Request Activity');
		};
	};

	var ItemList = function () {
		var that = this;

		var itemListTable = undefined;
		var itemListOrderState = undefined;

		this.selectedItem = undefined;
		this.shortcutData = undefined;

		this.load = function (itemToSelectId) {
			var itemListConditions = new ItemListCondition();

			if (itemToSelectId === undefined && that.selectedItem !== undefined)
				itemListConditions.selectedItemId = that.selectedItem.itemId;
			else
				itemListConditions.selectedItemId = itemToSelectId;

			itemListConditions.shortcutId = that.shortcutData.options.linkId;

			if ($('#billboard-requests-item-list-all').hasClass('active'))
				itemListConditions.listType = 'all-items';
			else if ($('#billboard-requests-item-list-archive').hasClass('active'))
				itemListConditions.listType = 'archive-items';
			else
				itemListConditions.listType = 'own-items';

			itemListConditions.statusFilter = $('#billboard-requests-filter-status').find("option:selected").val();

			var itemList = $.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .service-panel .item-list-container');
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "billboardRequests/getItemList",
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
				url: window.BaseUrl + "billboardRequests/addItemDialog",
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
					var addItemContent = $(msg);

					var formLogger = new $.SalesPortal.FormLogger();
					formLogger.init({
						logObject: {name: that.shortcutData.options.headerTitle},
						formContent: addItemContent
					});

					addItemContent.find('.btn.accept-button').on('click.billboard-requests', function () {
						ga('send', {
							hitType: 'pageview',
							title: "New Request/" + $('#add-item-name').val(),
							location: window.BaseUrl + "New Request",
							page: "New Request/" + $('#add-item-name').val()
						});
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "billboardRequests/addItem",
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
					addItemContent.find('.btn.cancel-button').on('click.billboard-requests', function () {
						$.fancybox.close();
					});

					$.fancybox({
						content: addItemContent,
						title: parameters.templatePageId === undefined ? "Add Request!" : "Clone Request!",
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
			var selectedItemId = that.selectedItem.itemId;
			if (itemId !== undefined)
				selectedItemId = itemId;
			if (selectedItemId != null)
			{
				var modalDialog = new $.SalesPortal.ModalDialog({
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
									url: window.BaseUrl + "billboardRequests/deleteItem",
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
			var itemListContainer = $.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .service-panel .item-list-container');
			var itemListButtons = $.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .service-panel .item-list-buttons');
			var itemListTabs = $.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .service-panel .item-list-tabs');

			var height = $.SalesPortal.Content.getContentObject().height()
				- itemListButtons.outerHeight(true)
				- itemListTabs.outerHeight(true)
				- 5;
			itemListContainer.css({
				'height': height + 'px'
			});

			if (that.selectedItem !== undefined)
				that.selectedItem.updateContentSize();
		};

		var init = function (data) {
			var itemListContainer = $.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .service-panel .item-list-container');

			var formLogger = new $.SalesPortal.FormLogger();
			formLogger.init({
				logObject: {name: that.shortcutData.options.headerTitle},
				formContent: itemListContainer
			});

			var tabHeaders = $.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .service-panel .item-list-tabs .nav-tabs');
			tabHeaders.find('a[href="#billboard-requests-item-list-own"]').html('my requests (' + data.info.ownItemsCount + ')');
			tabHeaders.find('a[href="#billboard-requests-item-list-all"]').html('all requests (' + data.info.allItemsCount + ')');
			tabHeaders.find('a[href="#billboard-requests-item-list-archive"]').html('archives (' + data.info.archiveItemsCount + ')');

			var tableIdentifier = 'billboard-requests-item-list';

			if (!$.SalesPortal.Content.isMobileDevice())
				itemListContainer.html('<table id="' + tableIdentifier + '" class="table item-list-table table-striped table-bordered"></table>');
			else
				itemListContainer.html('<table id="' + tableIdentifier + '" class="table item-list-table-table table-striped"></table>');

			var table = $("#" + tableIdentifier);

			var columnSettings = [];
			columnSettings.push({
				"data": "title",
				"title": "Requests",
				"class": "",
				"width": "32%",
				"render": cellRenderer
			});
			columnSettings.push({
				"data": "dateSubmit",
				"title": "Submitted",
				"class": "",
				"width": "17%",
				"render": {
					_: cellRenderer,
					sort: 'value'
				}
			});
			columnSettings.push({
				"data": "dateNeeded",
				"title": "Needed",
				"class": "",
				"width": "17%",
				"render": {
					_: cellRenderer,
					sort: 'value'
				}
			});
			columnSettings.push({
				"data": "assignedTo",
				"title": "Assigned",
				"class": "",
				"width": "17%",
				"render": cellRenderer
			});
			columnSettings.push({
				"data": "status",
				"title": "Status",
				"class": "",
				"width": "17%",
				"render": cellRenderer
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
				"dom": "<'row table-header-row'<'col-xs-6 status-filter-container'><'col-xs-6'f>>" +
					"<'row table-content-row'<'col-xs-12'tr>>",
				"createdRow": function (row, data, index) {
					$(row).addClass(tableIdentifier + '-row');
					$(row).addClass('item-data-row');
					if (data.isSelected)
						$(row).addClass('selected');
				}
			});

			var statusFilterMarkup = "<label>Status:<select id=\"billboard-requests-filter-status\" class=\"form-control\">";
			statusFilterMarkup += data.settings.statusFilterMarkup;
			statusFilterMarkup += "</select></label>";
			itemListContainer.find('.status-filter-container').html(statusFilterMarkup);
			var statusFilter = $('#billboard-requests-filter-status');
			statusFilter.selectpicker();
			statusFilter.on('change', function () {
				that.load();
			});

			table.on('order.dt', function () {
				itemListOrderState = itemListTable.api().order();
			});

			table.on('click', '.item-data-row', function () {
				$.SalesPortal.LinkManager.cleanupContextMenu();

				var row = $(this);
				if (!row.hasClass('selected'))
				{
					table.find('tr.item-data-row').removeClass('selected');
					row.addClass('selected');
					var rowObject = itemListTable.api().row(row);
					var itemId = rowObject.data().id;
					var itemTitle = rowObject.data().title;
					openItemInternal(
						itemId,
						itemTitle
					);
					ga('send', {
						hitType: 'pageview',
						title: $('.item-list-tabs ul li.active').text().split('(')[0] + "/" + itemTitle,
						location: window.BaseUrl + $('.item-list-tabs ul li.active').text().split('(')[0],
						page: $('.item-list-tabs ul li.active').text().split('(')[0] + "/" + itemTitle
					})
				}
			});

			table.on('contextmenu', '.item-data-row', function (event) {
				var row = $(this);
				var rowObject = itemListTable.api().row(row);

				$.SalesPortal.LinkManager.cleanupContextMenu();

				if (rowObject.data().allowEdit || that.shortcutData.options.isAdminRole)
				{
					var menu = $('<ul class="dropdown-menu context-menu-content logger-form" role="menu" data-log-group="Billboard Request" data-log-action="Billboard Request Activity">' +
						'<li><a tabindex="-1" href="#" class="menu-item log-action" data-action-tag="clone">Clone</a></li>' +
						'<li><a tabindex="-1" href="#" class="menu-item log-action" data-action-tag="delete">Delete</a></li>' +
						'</ul>');
					$('body').append(menu);

					var formLogger = new $.SalesPortal.FormLogger();
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
							var tag = $(this).data('action-tag');
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
				var selectedRow = table.find('tr.item-data-row.selected');
				var rowObject = itemListTable.api().row(selectedRow);
				openItemInternal(
					rowObject.data().id,
					rowObject.data().title
				);
			}
			else
			{
				$.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .content-buttons').hide();
				$.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .item-content').html('');
			}
		};

		var openItemInternal = function (selectedItemId, selectedItemTitle) {
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

		var cellRenderer = function (data, type, row, meta) {
			if (type === "display")
			{
				if (row && row !== '')
				{
					if (meta.col == 0 && row.itemOwner != '')
					{
						displayValue = '<span>' + row.title + '</span><span class="user-name">' + row.itemOwner + '</span>';
					}
					else
					{
						var displayValue = data && typeof data === 'object' ? data.display : data;
						displayValue = displayValue !== null ? displayValue : '';
					}
				}
				return '<span class="clickable-area" title="' + row.tooltip + '">' + displayValue + '</span>';
			}
			else
				return data;
		};

		var naturalSort = function (a, b) {
			// setup temp-scope variables for comparison evauluation
			var x = a.toString().toLowerCase() || '', y = b.toString().toLowerCase() || '',
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
			for (var cLoc = 0, numS = Math.max(xN.length, yN.length); cLoc < numS; cLoc++)
				if ((parseFloat(xN[cLoc]) || xN[cLoc]) < (parseFloat(yN[cLoc]) || yN[cLoc]))
					return -1;
				else if ((parseFloat(xN[cLoc]) || xN[cLoc]) > (parseFloat(yN[cLoc]) || yN[cLoc]))
					return 1;
			return 0;
		};
	};

	var ItemListCondition = function (source) {
		var that = this;

		this.selectedItemId = undefined;
		this.shortcutId = undefined;
		this.listType = undefined;
		this.statusFilter = undefined;

		if (source !== undefined)
			for (var prop in source)
				if (source.hasOwnProperty(prop))
					this[prop] = source[prop];

		this.getConditionObject = function () {
			return {
				selectedItemId: that.selectedItemId,
				shortcutId: that.shortcutId,
				listType: that.listType,
				statusFilter: that.statusFilter,
			};
		}
	};

	var ItemContent = function (parameters) {
		var that = this;
		var dateFormat = 'MM/DD/YYYY';

		this.itemId = parameters.selectedItemId;
		this.itemTitle = parameters.selectedItemTitle;
		this.isSubmitted = false;
		var shortcutData = parameters.shortcutData;

		this.clear = function () {
			$.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .content-buttons').hide();
			$.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .item-content').html('');
		};

		this.updateContentSize = function () {
			var itemContent = $.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .item-content > div');
			var contentButtons = $.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .content-buttons');
			var height = $.SalesPortal.Content.getContentObject().height() - contentButtons.outerHeight(true);
			itemContent.css({
				'height': height + 'px'
			});
		};

		var load = function () {
			var itemContent = $.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .item-content');
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "billboardRequests/getItemContent",
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

		var loadFiles = function (filesContainer, fileType, afterLoadCallback) {
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "billboardRequests/getItemFiles",
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

		var afterLoad = function () {
			var itemContent = $.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .item-content');

			var formLogger = new $.SalesPortal.FormLogger();
			formLogger.init({
				logObject: {name: shortcutData.options.headerTitle},
				formContent: itemContent
			});

			that.isSubmitted = itemContent.find('.submit-data .submitted-by').length > 0;
			var actiomMenuSubmitButton = $('#shortcut-action-container').find('.billboard-requests-item-submit');
			var itemListSumbitButton = $.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .service-panel .item-list-buttons .item-list-submit');
			var bottomBarSubmitButton = $.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .content-panel .content-buttons .item-submit');
			if (that.isSubmitted)
			{
				if (!bottomBarSubmitButton.hasClass('disabled'))
					bottomBarSubmitButton.addClass('disabled');
				itemListSumbitButton.hide();
				actiomMenuSubmitButton.hide();
			}
			else
			{
				bottomBarSubmitButton.removeClass('disabled');
				itemListSumbitButton.show();
				actiomMenuSubmitButton.show();
			}


			$('#billboard-requests-item-assigned-to').selectpicker();

			var statusSelector = $('#billboard-requests-item-status');
			statusSelector.selectpicker();
			statusSelector.on('change', function () {
				var status = statusSelector.selectpicker('val');
				var dateCompleteContainer = itemContent.find('.date-complete-container');
				if (status == "complete")
					dateCompleteContainer.show();
				else
					dateCompleteContainer.hide();
			});

			var dateCompletePickerContainer = itemContent.find('.billboard-requests-item-date-complete-container');
			var dateCompletePicker = $('#billboard-requests-item-date-complete');
			var dateCompleteValue = dateCompletePicker.val();
			if (dateCompleteValue == '')
				dateCompleteValue = moment().endOf('day');
			dateCompletePickerContainer.daterangepicker(
				{
					format: dateFormat,
					singleDatePicker: true,
					startDate: dateCompleteValue,
					endDate: dateCompleteValue
				});

			var dateNeededPickerContainer = itemContent.find('.billboard-requests-item-date-needed-container');
			var dateNeededPicker = $('#billboard-requests-item-date-needed');
			var dateNeededValue = dateNeededPicker.val();
			if (dateNeededValue == '')
				dateNeededValue = moment().endOf('day');
			dateNeededPickerContainer.daterangepicker(
				{
					format: dateFormat,
					singleDatePicker: true,
					startDate: dateNeededValue,
					endDate: dateNeededValue
				});

			itemContent.find('.billboard-requests-item-length-container .dropdown-menu a').click(function () {
				$('#billboard-requests-item-length').val($(this).data('value'));
			});

			itemContent.find('.billboard-requests-item-property-container .dropdown-menu a').click(function () {
				$('#billboard-requests-item-property').val($(this).data('value'));
			});

			$.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .content-buttons').show();

			Dropzone.autoDiscover = false;
			var allowEditAttachments = itemContent.find(">div.editable").length > 0 || shortcutData.options.isAdminRole;
			if (allowEditAttachments)
			{
				var attachmentsContainer = $("#billboard-requests-item-attachments");
				var attachmentsDataContainer = $("#billboard-requests-item-attachments-data");
				let dropZoneObjectAttachments = undefined;
				let abortUploadingAttachments = false;
				attachmentsDataContainer.dropzone({
					url: window.BaseUrl + "billboardRequests/uploadFile?itemId=" + that.itemId + "&fileType=attachment",
					dictDefaultMessage: "",
					previewTemplate: "<div></div>",
					uploadMultiple: true,
					parallelUploads: 1,
					clickable: shortcutData.options.uploadOnClick == true,
					init: function () {
						dropZoneObjectAttachments = this;
					},
					sendingmultiple: function (files, xhr, formData) {
						if (abortUploadingAttachments)
						{
							$.each(files, function (index, value) {
								dropZoneObjectAttachments.removeFile(value);
							});
							abortUploadingAttachments = false;
						}
						else
						{
							attachmentsContainer.find('.progress-bar').css({
								width: 0
							});
							attachmentsContainer.find('.progress').show();
						}
					},
					accept: function (file, done) {
						let existingFiles = attachmentsDataContainer.find('.file-item');
						$.each(existingFiles, function (index, value) {
							let existingFileNode = $(value);
							if (existingFileNode.find('.file-name').html() == file.name)
							{
								abortUploadingAttachments = true;
								let modalDialog = new $.SalesPortal.ModalDialog({
									title: 'Upload file?',
									description: 'This file already exists on the server',
									buttons: [
										{
											tag: 'ok',
											title: 'Replace',
											width: 160,
											clickHandler: function () {
												abortUploadingAttachments = false;
												existingFileNode.remove();
												dropZoneObjectAttachments.addFile(file);
												modalDialog.close();
											}
										},
										{
											tag: 'cancel',
											title: 'Cancel',
											width: 160,
											clickHandler: function () {
												modalDialog.close();
											}
										}
									]
								});
								modalDialog.show();
								return false;
							}
						});
						if (!abortUploadingAttachments)
						{
							if (file.size > parseInt(shortcutData.options.maxFileSize) * 1024 * 1024)
							{
								abortUploadingAttachments = true;
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
								return false;
							}
							else if (shortcutData.options.allowedFileTypes.length > 0)
							{
								let acceptedFile = false;
								$.each(shortcutData.options.allowedFileTypes, function (index, value) {
									acceptedFile = acceptedFile || file.name.includes("." + value);
								});
								if (!acceptedFile)
								{
									abortUploadingAttachments = true;
									if (shortcutData.options.fileTypeDiscardMessage !== '')
									{
										let modalDialog = new $.SalesPortal.ModalDialog({
											title: 'File type type is not authorized',
											description: shortcutData.options.fileTypeDiscardMessage,
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
									return false;
								}
							}
							done();
						}
					},
					complete: function () {
						attachmentsContainer.find('.progress').hide();
					},
					uploadprogress: function (file, progress) {
						attachmentsContainer.find('.progress-bar').css({
							width: progress + "%"
						});
						if (progress > 70)
							attachmentsContainer.find('.progress-text').css({
								color: '#ffffff'
							});
						else
							attachmentsContainer.find('.progress-text').css({
								color: '#000000'
							});
						attachmentsContainer.find('.file-name').text(file.name);
						attachmentsContainer.find('.progress-percent').text(Math.round(progress));
					},
					queuecomplete: function () {
						loadFiles(attachmentsDataContainer, 'attachment', afterAttachmentsLoad);
					}
				});
			}
			afterAttachmentsLoad();

			var allowEditDeliverables = shortcutData.options.isAdminRole;
			if (allowEditDeliverables)
			{
				var deliverablesContainer = $("#billboard-requests-item-deliverables");
				var deliverablesDataContainer = $("#billboard-requests-item-deliverables-data");
				let dropZoneObjectDeliverables = undefined;
				let abortUploadingDeliverables = false;
				deliverablesDataContainer.dropzone({
					url: window.BaseUrl + "billboardRequests/uploadFile?itemId=" + that.itemId + "&fileType=deliverable",
					dictDefaultMessage: "",
					previewTemplate: "<div></div>",
					uploadMultiple: true,
					parallelUploads: 1,
					clickable: shortcutData.options.uploadOnClick == true,
					init: function () {
						dropZoneObjectDeliverables = this;
					},
					sendingmultiple: function (files, xhr, formData) {
						if (abortUploadingDeliverables)
						{
							$.each(files, function (index, value) {
								dropZoneObjectDeliverables.removeFile(value);
							});
							abortUploadingDeliverables = false;
						}
						else
						{
							deliverablesContainer.find('.progress-bar').css({
								width: 0
							});
							deliverablesContainer.find('.progress').show();
						}
					},
					accept: function (file, done) {
						let existingFiles = deliverablesDataContainer.find('.file-item');
						$.each(existingFiles, function (index, value) {
							let existingFileNode = $(value);
							if (existingFileNode.find('.file-name').html() == file.name)
							{
								abortUploadingDeliverables = true;
								let modalDialog = new $.SalesPortal.ModalDialog({
									title: 'Upload file?',
									description: 'This file already exists on the server',
									buttons: [
										{
											tag: 'ok',
											title: 'Replace',
											width: 160,
											clickHandler: function () {
												abortUploadingDeliverables = false;
												existingFileNode.remove();
												dropZoneObjectDeliverables.addFile(file);
												modalDialog.close();
											}
										},
										{
											tag: 'cancel',
											title: 'Cancel',
											width: 160,
											clickHandler: function () {
												modalDialog.close();
											}
										}
									]
								});
								modalDialog.show();
								return false;
							}
						});
						if (!abortUploadingDeliverables)
						{
							if (file.size > parseInt(shortcutData.options.maxFileSize) * 1024 * 1024)
							{
								abortUploadingDeliverables = true;
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
								return false;
							}
							else if (shortcutData.options.allowedFileTypes.length > 0)
							{
								let acceptedFile = false;
								$.each(shortcutData.options.allowedFileTypes, function (index, value) {
									acceptedFile = acceptedFile || file.name.includes("." + value);
								});
								if (!acceptedFile)
								{
									abortUploadingDeliverables = true;
									if (shortcutData.options.fileTypeDiscardMessage !== '')
									{
										let modalDialog = new $.SalesPortal.ModalDialog({
											title: 'File type type is not authorized',
											description: shortcutData.options.fileTypeDiscardMessage,
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
									return false;
								}
							}
							done();
						}
					},
					complete: function () {
						deliverablesContainer.find('.progress').hide();
					},
					uploadprogress: function (file, progress) {
						deliverablesContainer.find('.progress-bar').css({
							width: progress + "%"
						});
						if (progress > 70)
							deliverablesContainer.find('.progress-text').css({
								color: '#ffffff'
							});
						else
							deliverablesContainer.find('.progress-text').css({
								color: '#000000'
							});
						deliverablesContainer.find('.file-name').text(file.name);
						deliverablesContainer.find('.progress-percent').text(Math.round(progress));
					},
					queuecomplete: function () {
						loadFiles(deliverablesDataContainer, "deliverable", afterDeliverablesLoad);
					}
				});
			}
			afterDeliverablesLoad();

			that.updateContentSize();
		};

		var afterAttachmentsLoad = function () {
			var itemContent = $.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .item-content');
			var attachmentsDataContainer = $("#billboard-requests-item-attachments-data");

			var attachmentsCount = attachmentsDataContainer.find('.file-item').length;
			if (attachmentsCount > 0)
				itemContent.find('.billboard-requests-item-content-additional-sections .nav-tabs .attachments-count').html(' (' + attachmentsCount + ')');
			else
				itemContent.find('.billboard-requests-item-content-additional-sections .nav-tabs .attachments-count').html('');

			attachmentsDataContainer.find('.file-item .file-name').off('click').on('click', function () {
				var fileId = $(this).closest('.file-item').find('.service-data .file-id').text();
				var form = document.getElementById('form-download-billboard-request-file');
				if (form === null)
				{
					form = document.createElement("form");
					form.setAttribute("id", "form-download-billboard-request-file");
					form.setAttribute("method", "post");
					form.setAttribute("action", window.BaseUrl + 'billboardRequests/downloadFile');
					form._submit_function_ = form.submit;

					var hiddenField = document.createElement("input");
					hiddenField.setAttribute("id", "input-billboard-request-file-id");
					hiddenField.setAttribute("type", "hidden");
					hiddenField.setAttribute("name", 'fileId');
					form.appendChild(hiddenField);
					document.body.appendChild(form);
				}
				document.getElementById('input-billboard-request-file-id').setAttribute("value", fileId);
				form._submit_function_();
			});

			attachmentsDataContainer.find('.draggable').off('dragstart').on('dragstart', function (e) {
				var urlHeader = $(this).data("url-header");
				var url = $(this).data('url');
				if (url !== '')
					e.originalEvent.dataTransfer.setData(urlHeader, url);
			});

			var allowEdit = itemContent.find(">div.editable").length > 0 || shortcutData.options.isAdminRole;
			if (allowEdit)
			{
				attachmentsDataContainer.find('.file-item .file-delete').off('click').on('click', function () {
					var fileId = $(this).closest('.file-item').find('.service-data .file-id').text();
					$.ajax({
						type: "POST",
						url: window.BaseUrl + "billboardRequests/deleteFile",
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

		var afterDeliverablesLoad = function () {
			var itemContent = $.SalesPortal.Content.getContentObject().find('.billboard-requests-main-page .item-content');
			var deliverablesDataContainer = $("#billboard-requests-item-deliverables-data");

			var deliverablesCount = deliverablesDataContainer.find('.file-item').length;
			if (deliverablesCount > 0)
				itemContent.find('.billboard-requests-item-content-additional-sections .nav-tabs .deliverables-count').html(' (' + deliverablesCount + ')');
			else
				itemContent.find('.billboard-requests-item-content-additional-sections .nav-tabs .deliverables-count').html('');

			deliverablesDataContainer.find('.file-item .file-name').off('click').on('click', function () {
				var fileId = $(this).closest('.file-item').find('.service-data .file-id').text();
				var form = document.getElementById('form-download-billboard-request-file');
				if (form === null)
				{
					form = document.createElement("form");
					form.setAttribute("id", "form-download-billboard-request-file");
					form.setAttribute("method", "post");
					form.setAttribute("action", window.BaseUrl + 'billboardRequests/downloadFile');
					form._submit_function_ = form.submit;

					var hiddenField = document.createElement("input");
					hiddenField.setAttribute("id", "input-billboard-request-file-id");
					hiddenField.setAttribute("type", "hidden");
					hiddenField.setAttribute("name", 'fileId');
					form.appendChild(hiddenField);
					document.body.appendChild(form);
				}
				document.getElementById('input-billboard-request-file-id').setAttribute("value", fileId);
				form._submit_function_();
			});

			deliverablesDataContainer.find('.draggable').off('dragstart').on('dragstart', function (e) {
				var urlHeader = $(this).data("url-header");
				var url = $(this).data('url');
				if (url !== '')
					e.originalEvent.dataTransfer.setData(urlHeader, url);
			});

			if (shortcutData.options.isAdminRole)
			{
				deliverablesDataContainer.find('.file-item .file-delete').off('click').on('click', function () {
					var fileId = $(this).closest('.file-item').find('.service-data .file-id').text();
					$.ajax({
						type: "POST",
						url: window.BaseUrl + "billboardRequests/deleteFile",
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
							loadFiles(deliverablesDataContainer, "deliverable", afterDeliverablesLoad);
						},
						error: function () {
						},
						async: true,
						dataType: 'html'
					});
				});
			}
			else
				deliverablesDataContainer.find('.file-item .file-delete').hide();
		};

		load();
	};
})(jQuery);
