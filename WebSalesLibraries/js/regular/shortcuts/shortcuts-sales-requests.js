(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsSalesRequests = function () {
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

			servicePanel = $.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .service-panel');

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

			$.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .service-panel').resizable({
				resizeHeight: false,
				resize: function (event, ui) {
					updateContentSize();
				}
			});

			$(window).off('resize.sales-requests').on('resize.sales-requests', updateContentSize);
			updateContentSize();
		};

		var saveItem = function (onSuccessHandler) {
			if (itemList.selectedItem !== undefined)
			{
				var itemContent = $.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .item-content');
				var allowSave = itemContent.find(">div.editable").length > 0 || shortcutData.options.isAdminRole;
				if (allowSave)
				{
					var requestReasons = [];
					$.each(itemContent.find('.sales-requests-item-request-reason:checked'), function () {
						var requestReason = $(this).val();
						requestReasons.push(requestReason);
					});

					var status = $('#sales-requests-item-status').val();

					$.ajax({
						type: "POST",
						url: window.BaseUrl + "salesRequests/saveItem",
						data: {
							selectedItemId: itemList.selectedItem.itemId,
							title: itemList.selectedItem.itemTitle,
							status: status,
							assignedTo: $('#sales-requests-item-assigned-to').val(),
							dateNeeded: $('#sales-requests-item-date-needed').val(),
							dateCompleted: status == "complete" ?
								$('#sales-requests-item-date-complete').val() :
								undefined,
							content: {
								dateSubmit: itemContent.find('.submit-data .submitted-date').text(),
								submittedByUserId: itemContent.find('.submit-data .submitted-by').text(),
								advertiser: $('#sales-requests-item-advertiser').val(),
								agency: $('#sales-requests-item-agency').val(),
								category: $('#sales-requests-item-category').val(),
								meetingWith: $('#sales-requests-item-meeting-with').val(),
								demos: $('#sales-requests-item-demos').val(),
								requestReason: requestReasons,
								details: $('#sales-requests-item-details-data').val()
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
								$('body').append('<div id="page-content-save-confirm" title="adSALESapps.com">Data SAVED!</div>');
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
			var itemContent = $.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .item-content');
			var allowSave = itemContent.find(">div.editable").length > 0 || shortcutData.options.isAdminRole;
			if (allowSave)
			{
				$('#sales-requests-item-status').val('submitted');
				saveItem(function () {
					itemList.loadCurrentItem();
				});
			}
		};

		var initActionButtons = function () {
			var shortcutActionsContainer = $('#shortcut-action-container');

			if ($.cookie("showServicePanel") === "true")
			{
				shortcutActionsContainer.find('.sales-requests-panel-show').hide();
			}
			else
				shortcutActionsContainer.find('.sales-requests-panel-hide').hide();
			shortcutActionsContainer.find('.sales-requests-panel-show').off('click.action').on('click.action', function () {
				servicePanel.show();
				shortcutActionsContainer.find('.sales-requests-panel-hide').show();
				$(this).hide();

				$.cookie("showServicePanel", true, {
					expires: (60 * 60 * 24 * 7)
				});

				updateContentSize();
			});
			shortcutActionsContainer.find('.sales-requests-panel-hide').off('click.action').on('click.action', function () {
				servicePanel.hide();
				shortcutActionsContainer.find('.sales-requests-panel-show').show();
				$(this).hide();

				$.cookie("showServicePanel", false, {
					expires: (60 * 60 * 24 * 7)
				});

				updateContentSize();
			});
			shortcutActionsContainer.find('.sales-requests-item-add').off('click.action').on('click.action', function () {
				itemList.addItem();
			});
			shortcutActionsContainer.find('.sales-requests-item-delete').off('click.action').on('click.action', function () {
				itemList.deleteItem();
			});
			shortcutActionsContainer.find('.sales-requests-item-save').off('click.action').on('click.action', function () {
				saveItem();
			});
			shortcutActionsContainer.find('.sales-requests-item-submit').off('click.action').on('click.action', function () {
				submitItem();
			});

			var itemListButtons = $.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .service-panel .item-list-buttons');
			itemListButtons.find('.item-list-add').off('click.sales-requests').on('click.sales-requests', function () {
				itemList.addItem();
			});
			itemListButtons.find('.item-list-save').off('click.sales-requests').on('click.sales-requests', function () {
				saveItem()
			});
			itemListButtons.find('.item-list-submit').off('click.sales-requests').on('click.sales-requests', function () {
				submitItem();
			});

			var contentButtons = $.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .content-panel .content-buttons');
			contentButtons.find('.item-save').off('click.sales-requests').on('click.sales-requests', function () {
				saveItem()
			});
			contentButtons.find('.item-submit').off('click.sales-requests').on('click.sales-requests', function () {
				submitItem();
			});

			$.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .service-panel .item-list-tabs .nav-tabs a[data-toggle="tab"]').on('shown.bs.tab', function () {
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
				'Sales Request Activity',
				'Sales Request Activity');
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

			if($('#sales-requests-item-list-all').hasClass('active'))
				itemListConditions.listType = 'all-items';
			else if($('#sales-requests-item-list-archive').hasClass('active'))
				itemListConditions.listType = 'archive-items';
			else
				itemListConditions.listType = 'own-items';

			itemListConditions.statusFilter = $('#sales-requests-filter-status').find("option:selected").val();

			var itemList = $.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .service-panel .item-list-container');
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "salesRequests/getItemList",
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
				url: window.BaseUrl + "salesRequests/addItemDialog",
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

					addItemContent.find('.btn.accept-button').on('click.sales-requests', function () {
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "salesRequests/addItem",
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
					addItemContent.find('.btn.cancel-button').on('click.sales-requests', function () {
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
									url: window.BaseUrl + "salesRequests/deleteItem",
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

		this.loadCurrentItem = function () {
			var itemList = $.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .service-panel .item-list-container');
			var selectedRow = itemList.find('tr.selected');
			var selectedItemId = selectedRow.find('.item-id-column').html();
			var selectedItemTitle = selectedRow.find('.item-name-column .title').html();
			openItemInternal(selectedItemId, selectedItemTitle);
		};

		this.updateContentSize = function () {
			var itemListContainer = $.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .service-panel .item-list-container');
			var itemListButtons = $.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .service-panel .item-list-buttons');
			var itemListTabs = $.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .service-panel .item-list-tabs');

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
			var itemListContainer = $.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .service-panel .item-list-container');

			var formLogger = new $.SalesPortal.FormLogger();
			formLogger.init({
				logObject: {name: that.shortcutData.options.headerTitle},
				formContent: itemListContainer
			});

			var tableIdentifier = 'sales-requests-item-list';

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
				"width": "40%",
				"render": cellRenderer
			});
			columnSettings.push({
				"data": "dateNeeded",
				"title": "Date Needed",
				"class": "",
				"width": "20%",
				"render": {
					_: cellRenderer,
					sort: 'value'
				}
			});
			columnSettings.push({
				"data": "assignedTo",
				"title": "Assigned",
				"class": "",
				"width": "20%",
				"render": cellRenderer
			});
			columnSettings.push({
				"data": "status",
				"title": "Status",
				"class": "",
				"width": "20%",
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
					[[0, 'asc']],
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

			var statusFilterMarkup = "<label>Status:<select id=\"sales-requests-filter-status\" class=\"form-control\">";
			statusFilterMarkup += data.settings.statusFilterMarkup;
			statusFilterMarkup += "</select></label>";
			itemListContainer.find('.status-filter-container').html(statusFilterMarkup);
			var statusFilter = $('#sales-requests-filter-status');
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
				}
			});

			table.on('contextmenu', '.item-data-row', function (event) {
				var row = $(this);
				var rowObject = itemListTable.api().row(row);

				$.SalesPortal.LinkManager.cleanupContextMenu();

				if(rowObject.data().allowEdit || that.shortcutData.options.isAdminRole)
				{
					var menu = $('<ul class="dropdown-menu context-menu-content logger-form" role="menu" data-log-group="Sales Request" data-log-action="Sales Request Activity">' +
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
				$.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .content-buttons').hide();
				$.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .item-content').html('');
			}
		};

		var openItemInternal = function (selectedItemId, selectedItemTitle) {
			that.selectedItem = new ItemContent({
				selectedItemId: selectedItemId,
				selectedItemTitle: selectedItemTitle,
				shortcutData: that.shortcutData
			});
		};

		var getDataRowElement = function (cellItem) {
			var tableRow = cellItem.closest("tr");
			if (tableRow.hasClass('child'))
				return tableRow.prev('tr.parent');
			return tableRow;
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
		var shortcutData = parameters.shortcutData;

		this.clear = function () {
			$.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .content-buttons').hide();
			$.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .item-content').html('');
		};

		this.updateContentSize = function () {
			var itemContent = $.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .item-content > div');
			var contentButtons = $.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .content-buttons');
			var height = $.SalesPortal.Content.getContentObject().height() - contentButtons.outerHeight(true);
			itemContent.css({
				'height': height + 'px'
			});
		};

		var load = function () {
			var itemContent = $.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .item-content');
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "salesRequests/getItemContent",
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
				url: window.BaseUrl + "salesRequests/getItemFiles",
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
			var itemContent = $.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .item-content');

			var formLogger = new $.SalesPortal.FormLogger();
			formLogger.init({
				logObject: {name: shortcutData.options.headerTitle},
				formContent: itemContent
			});

			$('#sales-requests-item-assigned-to').selectpicker();

			var statusSelector = $('#sales-requests-item-status');
			statusSelector.selectpicker();
			statusSelector.on('change', function () {
				var status = statusSelector.selectpicker('val');
				var dateCompleteContainer = itemContent.find('.date-complete-container');
				if (status == "complete")
					dateCompleteContainer.show();
				else
					dateCompleteContainer.hide();
			});

			var dateCompletePickerContainer = itemContent.find('.sales-requests-item-date-complete-container');
			var dateCompletePicker = $('#sales-requests-item-date-complete');
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

			var dateNeededPickerContainer = itemContent.find('.sales-requests-item-date-needed-container');
			var dateNeededPicker = $('#sales-requests-item-date-needed');
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

			itemContent.find('.sales-requests-item-category-container .dropdown-menu a').click(function () {
				$('#sales-requests-item-category').val($(this).data('value'));
			});

			itemContent.find('.sales-requests-item-demos-container .dropdown-menu a').click(function () {
				$('#sales-requests-item-demos').val($(this).data('value'));
			});

			$.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .content-buttons').show();

			Dropzone.autoDiscover = false;
			var allowEditAttachments = itemContent.find(">div.editable").length > 0 || shortcutData.options.isAdminRole;
			if (allowEditAttachments)
			{
				var attachmentsContainer = $("#sales-requests-item-attachments");
				var attachmentsDataContainer = $("#sales-requests-item-attachments-data");
				attachmentsDataContainer.dropzone({
					url: window.BaseUrl + "salesRequests/uploadFile?itemId=" + that.itemId + "&fileType=attachment",
					dictDefaultMessage: "",
					previewTemplate: "<div></div>",
					sending: function () {
						attachmentsContainer.find('.progress-bar').css({
							width: 0
						});
						attachmentsContainer.find('.progress').show();
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

			var allowEditDeliverables = shortcutData.options.isAdminRole;
			if (allowEditDeliverables)
			{
				var deliverablesContainer = $("#sales-requests-item-deliverables");
				var deliverablesDataContainer = $("#sales-requests-item-deliverables-data");
				deliverablesDataContainer.dropzone({
					url: window.BaseUrl + "salesRequests/uploadFile?itemId=" + that.itemId + "&fileType=deliverable",
					dictDefaultMessage: "",
					previewTemplate: "<div></div>",
					sending: function () {
						deliverablesContainer.find('.progress-bar').css({
							width: 0
						});
						deliverablesContainer.find('.progress').show();
					},
					complete: function () {
						deliverablesContainer.find('.progress').hide();
					},
					success: function () {
						loadFiles(deliverablesDataContainer, "deliverable", afterDeliverablesLoad);
					},
					uploadprogress: function (e, progress) {
						deliverablesContainer.find('.progress-bar').css({
							width: progress + "%"
						});
					}
				});
			}
			afterDeliverablesLoad();

			that.updateContentSize();
		};

		var afterAttachmentsLoad = function () {
			var itemContent = $.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .item-content');
			var attachmentsDataContainer = $("#sales-requests-item-attachments-data");

			var attachmentsCount =attachmentsDataContainer.find('.file-item').length;
			if(attachmentsCount>0)
				itemContent.find('.sales-requests-item-content-additional-sections .nav-tabs .attachments-count').html(' ('+attachmentsCount+')');
			else
				itemContent.find('.sales-requests-item-content-additional-sections .nav-tabs .attachments-count').html('');

			attachmentsDataContainer.find('.file-item .file-name').off('click').on('click', function () {
				var fileId = $(this).closest('.file-item').find('.service-data .file-id').text();
				var form = document.getElementById('form-download-sales-request-file');
				if (form === null)
				{
					form = document.createElement("form");
					form.setAttribute("id", "form-download-sales-request-file");
					form.setAttribute("method", "post");
					form.setAttribute("action", window.BaseUrl + 'salesRequests/downloadFile');
					form._submit_function_ = form.submit;

					var hiddenField = document.createElement("input");
					hiddenField.setAttribute("id", "input-sales-request-file-id");
					hiddenField.setAttribute("type", "hidden");
					hiddenField.setAttribute("name", 'fileId');
					form.appendChild(hiddenField);
					document.body.appendChild(form);
				}
				document.getElementById('input-sales-request-file-id').setAttribute("value", fileId);
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
						url: window.BaseUrl + "salesRequests/deleteFile",
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
			var itemContent = $.SalesPortal.Content.getContentObject().find('.sales-requests-main-page .item-content');
			var deliverablesDataContainer = $("#sales-requests-item-deliverables-data");

			var deliverablesCount = deliverablesDataContainer.find('.file-item').length;
			if (deliverablesCount > 0)
				itemContent.find('.sales-requests-item-content-additional-sections .nav-tabs .deliverables-count').html(' (' + deliverablesCount + ')');
			else
				itemContent.find('.sales-requests-item-content-additional-sections .nav-tabs .deliverables-count').html('');

			deliverablesDataContainer.find('.file-item .file-name').off('click').on('click', function () {
				var fileId = $(this).closest('.file-item').find('.service-data .file-id').text();
				var form = document.getElementById('form-download-sales-request-file');
				if (form === null)
				{
					form = document.createElement("form");
					form.setAttribute("id", "form-download-sales-request-file");
					form.setAttribute("method", "post");
					form.setAttribute("action", window.BaseUrl + 'salesRequests/downloadFile');
					form._submit_function_ = form.submit;

					var hiddenField = document.createElement("input");
					hiddenField.setAttribute("id", "input-sales-request-file-id");
					hiddenField.setAttribute("type", "hidden");
					hiddenField.setAttribute("name", 'fileId');
					form.appendChild(hiddenField);
					document.body.appendChild(form);
				}
				document.getElementById('input-sales-request-file-id').setAttribute("value", fileId);
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
						url: window.BaseUrl + "salesRequests/deleteFile",
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
