(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.SearchDataTable = function (tableOptions) {
		const ColumnTagCategory = 'tag';
		const ColumnTagLibrary = 'library';
		const ColumnTagType = 'type';
		const ColumnTagName = 'link';
		const ColumnTagThumbnail = 'thumbnail';
		const ColumnTagViews = 'views';
		const ColumnTagRate = 'rate';
		const ColumnTagDate = 'date';

		var tableIdentifier = tableOptions !== undefined && tableOptions.tableIdentifier !== undefined ?
			tableOptions.tableIdentifier :
			'data-table-content';
		var tableContainerSelector = tableOptions !== undefined && tableOptions.tableContainerSelector !== undefined ?
			tableOptions.tableContainerSelector :
			undefined;
		var parentContainerSelector = tableOptions !== undefined && tableOptions.parentContainerSelector !== undefined ?
			tableOptions.parentContainerSelector :
			undefined;
		var saveState = tableOptions !== undefined && tableOptions.saveSate !== undefined ?
			tableOptions.saveSate :
			false;
		var usePaginate = tableOptions !== undefined && tableOptions.paginate !== undefined ?
			tableOptions.paginate :
			true;
		var useSubSearch = tableOptions !== undefined && tableOptions.subSearch !== undefined ?
			tableOptions.subSearch :
			true;
		var useExcelExport = tableOptions !== undefined && tableOptions.excelExport !== undefined ?
			tableOptions.excelExport :
			true;
		var defaultLength = tableOptions !== undefined && tableOptions.defaultLength !== undefined ?
			tableOptions.defaultLength :
			15;
		var sizeSettings = tableOptions !== undefined && tableOptions.sizeSettings !== undefined ?
			tableOptions.sizeSettings :
			{
				useFixedSize: true,
				size: 'screen'
			};
		var deleteHandler = tableOptions !== undefined && tableOptions.deleteHandler !== undefined ?
			tableOptions.deleteHandler :
			function () {
			};
		var backHandler = tableOptions !== undefined && tableOptions.backHandler !== undefined ?
			tableOptions.backHandler :
			undefined;
		var logHandler = tableOptions !== undefined && tableOptions.logHandler !== undefined ?
			tableOptions.logHandler :
			function () {
			};

		var columnVisibilitySettings = [];
		var dataTable = undefined;

		var updateResponsiveColumnsTimer = null;

		this.init = function (data) {
			destroy();

			if (data === undefined)
				data = {
					dataset: undefined,
					dataViewOptions: undefined,
					sortColumnTag: undefined,
					sortDirection: undefined
				};
			data.dataset = data.dataset !== undefined ? data.dataset : [];
			data.dataViewOptions = data.dataViewOptions !== undefined ? data.dataViewOptions : {
				columnSettings: [],
				showDeleteButton: false,
				reorderSourceField: undefined
			};
			data.sortColumnTag = data.sortColumnTag !== undefined ? data.sortColumnTag : ColumnTagName;
			data.sortDirection = data.sortDirection !== undefined ? data.sortDirection : 'asc';


			var content = $.SalesPortal.Content.getContentObject();
			var tableContainer = content.find(tableContainerSelector);
			if (!$.SalesPortal.Content.isMobileDevice())
				tableContainer.html('<table id="' + tableIdentifier + '" class="table link-data-table table-striped table-bordered"></table>');
			else
				tableContainer.html('<table id="' + tableIdentifier + '" class="table link-data-table-table table-striped"></table>');

			var table = $("#" + tableIdentifier);

			var hasCategories = false;
			if (data.dataViewOptions.columnSettings[ColumnTagCategory].enable)
				$.each(data.dataset, function (index, value) {
					if (value.tag !== null && value.tag !== '')
					{
						hasCategories = true;
						return false;
					}
				});

			var columnSettings = [];
			var exportColumnsIndexes = [];
			columnVisibilitySettings = [];

			if (data.dataViewOptions.reorderSourceField !== undefined)
			{
				columnSettings.push({
					"data": 'extended_data.' + data.dataViewOptions.reorderSourceField,
					"visible": false,
					"searchable": false,
					"orderable": true
				});
				columnVisibilitySettings.push({
					large: false,
					medium: false,
					small: false,
					extraSmall: false
				});
			}

			var sortColumnIndex = 0;
			var columnIndex = 0;
			var columnVisibility = null;
			$.each(data.dataViewOptions.columnSettings, function (key, value) {
				if (data.dataViewOptions.reorderSourceField === undefined && key === data.sortColumnTag)
					sortColumnIndex = columnIndex;
				if (value.enable)
				{
					exportColumnsIndexes[key] = (columnIndex + (data.dataViewOptions.reorderSourceField === undefined ? 0 : 1));
				}
				switch (key)
				{
					case ColumnTagCategory:
						if (value.enable && hasCategories)
						{
							columnVisibility = {
								large: !value.visibilitySettings.hideForLargeScreen,
								medium: !value.visibilitySettings.hideForMediumScreen,
								small: !value.visibilitySettings.hideForSmallScreen,
								extraSmall: !value.visibilitySettings.hideForExtraSmallScreen
							};
							columnSettings.push({
								"data": "tag",
								"orderable": data.dataViewOptions.reorderSourceField === undefined,
								"title": value.title,
								"class": "tag-text-container allow-export allow-reorder" + (value.fullWidth ? ' none' : ' all'),
								"width": value.width > 0 ? (value.width + "px") : "15%",
								"visible": getIsColumnVisibile(columnVisibility),
								"render": cellRenderer,
							});
							columnVisibilitySettings.push(columnVisibility);
							columnIndex++;
						}
						break;
					case ColumnTagLibrary:
						if (value.enable)
						{
							columnVisibility = {
								large: !value.visibilitySettings.hideForLargeScreen,
								medium: !value.visibilitySettings.hideForMediumScreen,
								small: !value.visibilitySettings.hideForSmallScreen,
								extraSmall: !value.visibilitySettings.hideForExtraSmallScreen
							};
							columnSettings.push({
								"data": "library.name",
								"orderable": data.dataViewOptions.reorderSourceField === undefined,
								"title": value.title,
								"class": "centered allow-export allow-reorder" + (value.fullWidth ? ' none' : ' all'),
								"width": value.width > 0 ? (value.width + "px") : "10%",
								"visible": getIsColumnVisibile(columnVisibility),
								"render": cellRenderer
							});
							columnVisibilitySettings.push(columnVisibility);
							columnIndex++;
						}
						break;
					case ColumnTagType:
						if (value.enable)
						{
							columnVisibility = {
								large: !value.visibilitySettings.hideForLargeScreen,
								medium: !value.visibilitySettings.hideForMediumScreen,
								small: !value.visibilitySettings.hideForSmallScreen,
								extraSmall: !value.visibilitySettings.hideForExtraSmallScreen
							};
							columnSettings.push({
								"data": "file_type",
								"orderable": data.dataViewOptions.reorderSourceField === undefined,
								"title": value.title,
								"class": "centered allow-export allow-reorder" + (value.fullWidth ? ' none' : ' all'),
								"width": value.width > 0 ? (value.width + "px") : "70px",
								"visible": getIsColumnVisibile(columnVisibility),
								"render": cellRenderer
							});
							columnVisibilitySettings.push(columnVisibility);
							columnIndex++;
						}
						break;
					case ColumnTagName:
						if (value.enable)
						{
							columnVisibility = {
								large: !value.visibilitySettings.hideForLargeScreen,
								medium: !value.visibilitySettings.hideForMediumScreen,
								small: !value.visibilitySettings.hideForSmallScreen,
								extraSmall: !value.visibilitySettings.hideForExtraSmallScreen
							};
							columnSettings.push({
								"data": "name",
								"orderable": data.dataViewOptions.reorderSourceField === undefined,
								"title": value.title,
								"class": "link-name-text-container allow-export" + (value.fullWidth ? ' none' : ' all'),
								"width": value.width > 0 ? (value.width + "px") : null,
								"visible": getIsColumnVisibile(columnVisibility),
								"render": cellRenderer,
								"sType": "natural"
							});
							columnVisibilitySettings.push(columnVisibility);
							columnIndex++;
						}
						break;
					case ColumnTagThumbnail:
						if (value.enable)
						{
							columnVisibility = {
								large: !value.visibilitySettings.hideForLargeScreen,
								medium: !value.visibilitySettings.hideForMediumScreen,
								small: !value.visibilitySettings.hideForSmallScreen,
								extraSmall: !value.visibilitySettings.hideForExtraSmallScreen
							};
							columnSettings.push({
								"data": "thumbnail",
								"orderable": data.dataViewOptions.reorderSourceField === undefined,
								"title": value.title,
								"width": value.width > 0 ? (value.width + "px") : "90px",
								"visible": getIsColumnVisibile(columnVisibility),
								"class": "centered" + (value.fullWidth ? ' none' : ' all'),
								"render": cellRenderer
							});
							columnVisibilitySettings.push(columnVisibility);
							columnIndex++;
						}
						break;
					case ColumnTagViews:
						if (value.enable)
						{
							columnVisibility = {
								large: !value.visibilitySettings.hideForLargeScreen,
								medium: !value.visibilitySettings.hideForMediumScreen,
								small: !value.visibilitySettings.hideForSmallScreen,
								extraSmall: !value.visibilitySettings.hideForExtraSmallScreen
							};
							columnSettings.push({
								"data": "views",
								"orderable": data.dataViewOptions.reorderSourceField === undefined,
								"title": value.title,
								"class": "centered allow-reorder allow-export" + (value.fullWidth ? ' none' : ' all'),
								"width": value.width > 0 ? (value.width + "px") : "50px",
								"visible": getIsColumnVisibile(columnVisibility),
								"sType": "numeric",
								"render": {
									_: cellRenderer,
									sort: 'value'
								}
							});
							columnVisibilitySettings.push(columnVisibility);
							columnIndex++;
						}
						break;
					case ColumnTagRate:
						if (value.enable)
						{
							columnVisibility = {
								large: !value.visibilitySettings.hideForLargeScreen,
								medium: !value.visibilitySettings.hideForMediumScreen,
								small: !value.visibilitySettings.hideForSmallScreen,
								extraSmall: !value.visibilitySettings.hideForExtraSmallScreen
							};
							columnSettings.push({
								"data": "rate",
								"orderable": data.dataViewOptions.reorderSourceField === undefined,
								"title": value.title,
								"width": value.width > 0 ? (value.width + "px") : "90px",
								"visible": getIsColumnVisibile(columnVisibility),
								"class": "centered rate-image-container" + (value.fullWidth ? ' none' : ' all'),
								"render": {
									_: function (columnData) {
										var cellContent = '';
										if (columnData.image !== '')
											cellContent = '<img src="' + columnData.image + '">';
										return cellContent;
									},
									sort: 'value'
								}
							});
							columnVisibilitySettings.push(columnVisibility);
							columnIndex++;
						}
						break;
					case ColumnTagDate:
						if (value.enable)
						{
							columnVisibility = {
								large: !value.visibilitySettings.hideForLargeScreen,
								medium: !value.visibilitySettings.hideForMediumScreen,
								small: !value.visibilitySettings.hideForSmallScreen,
								extraSmall: !value.visibilitySettings.hideForExtraSmallScreen
							};
							columnSettings.push({
								"data": "date",
								"orderable": data.dataViewOptions.reorderSourceField === undefined,
								"title": value.title,
								"class": "centered allow-reorder allow-export" + (value.fullWidth ? ' none' : ' all'),
								"visible": getIsColumnVisibile(columnVisibility),
								"width": value.width > 0 ? (value.width + "px") : "80px",
								"render": {
									_: cellRenderer,
									sort: 'value'
								}
							});
							columnVisibilitySettings.push(columnVisibility);

							columnSettings.push({
								"data": "date.value",
								"title": value.title,
								"visible": false,
								"searchable": false,
								"sType": "numeric"
							});
							columnVisibilitySettings.push({
								large: false,
								medium: false,
								small: false,
								extraSmall: false
							});

							columnIndex++;
						}
						break;
				}
			});

			if (data.dataViewOptions.showDeleteButton)
			{
				columnSettings.push({
					"data": null,
					"title": '',
					"width": "5px",
					"orderable": false,
					"defaultContent": '<img class="link-delete-button" src="' + window.BaseUrl + 'images/grid/item-delete.png">'
				});
				columnVisibilitySettings.push({
					large: true,
					medium: true,
					small: true,
					extraSmall: true
				});
			}

			columnSettings.push({
				"data": "id",
				"title": "Id",
				"visible": false,
				"searchable": false
			});
			columnVisibilitySettings.push({
				large: false,
				medium: false,
				small: false,
				extraSmall: false
			});

			columnSettings.push({
				"data": "extended_data",
				"title": "extended_data",
				"visible": false,
				"searchable": false
			});
			columnVisibilitySettings.push({
				large: false,
				medium: false,
				small: false,
				extraSmall: false
			});

			columnSettings.push({
				"data": "previewUrl",
				"title": "Preview",
				"visible": false,
				"searchable": false,
				"class": "allow-export"

			});
			columnVisibilitySettings.push({
				large: false,
				medium: false,
				small: false,
				extraSmall: false
			});

			columnSettings.push({
				"data": "oneDriveUrl",
				"title": "OneDrive",
				"visible": false,
				"searchable": false,
				"class": "allow-export"
			});
			columnVisibilitySettings.push({
				large: false,
				medium: false,
				small: false,
				extraSmall: false
			});

			$.extend($.fn.dataTableExt.oStdClasses, {
				"sFilterInput": "form-control",
				"sLengthSelect": "form-control"
			});

			jQuery.fn.dataTableExt.oSort['natural-asc'] = function (a, b) {
				return naturalSort(a, b);
			};

			jQuery.fn.dataTableExt.oSort['natural-desc'] = function (a, b) {
				return naturalSort(a, b) * -1;
			};

			dataTable = table.dataTable({
				"data": data.dataset,
				columns: columnSettings,
				stateSave: saveState,
				paging: usePaginate,
				searching: useSubSearch,
				order: sortColumnIndex >= 0 ? [[sortColumnIndex, data.sortDirection]] : [],
				rowReorder: data.dataViewOptions.reorderSourceField !== undefined ?
					{
						update: true,
						dataSrc: 'extended_data.' + data.dataViewOptions.reorderSourceField,
						selector: '.allow-reorder',
						snapX: 0
					} :
					false,
				responsive: {
					details: {
						display: $.fn.dataTable.Responsive.display.childRowImmediate,
						type: ''
					}
				},
				buttons: [{
					extend: 'excelHtml5',
					text: '<img style="width: 32px; height: 32px" src="' + window.BaseUrl + 'images/grid/thumbnail-placeholder/xls.png">',
					exportOptions: {
						columns: '.allow-export'
					},
					customize: function (xlsx) {
						var sheet = xlsx.xl.worksheets['sheet1.xml'];
						var col = $('col', sheet);
						$(col[exportColumnsIndexes[ColumnTagCategory]]).attr('width', 50);
						$(col[exportColumnsIndexes[ColumnTagName]]).attr('width', 50);
					}
				}],
				"scrollY": getTableSize(),
				"scrollCollapse": false,
				"aLengthMenu": data.dataset.length < 500 ?
					[
						[5, 15, 25, 50, 100, -1],
						[5, 15, 25, 50, 100, "All"]
					] :
					[
						[5, 15, 25, 50, 100, 200],
						[5, 15, 25, 50, 100, 200]
					],
				"iDisplayLength": data.dataset.length >= defaultLength ? defaultLength : -1,
				"language": {
					"emptyTable": "",
					"zeroRecords": "",
					"search": "_INPUT_",
					"searchPlaceholder": "filter...",
					"info": "Showing _START_ to _END_ of _TOTAL_ links",
					"lengthMenu": '_MENU_ <span style="color: #c0c0c0;">(of <span class="length-filter-total-items">0</span> links)</span>'
				},
				"dom": (data.dataset.length > 0 ? (
					"<'row table-header-row hidden-xs'<'col-xs-12 back-url text-left'>>" +
					"<'row table-header-row hidden-xs'<'col-lg-3 col-md-3 col-sm-3'f><'col-lg-3 col-md-3 col-sm-3 text-center'l><'col-lg-6 col-md-6 col-sm-6'p>>" +
					(backHandler ? ("<'row table-header-row hidden-lg hidden-md hidden-sm'<'col-xs-6 back-url text-right'>>") : "") +
					"<'row table-header-row hidden-lg hidden-md hidden-sm'<'col-xs-4'f><'col-xs-8 text-left'l><'col-xs-12'p>>") :
					"<'row'<'col-xs-12 back-url text-center'>>") +
					"<'row table-content-row'<'col-xs-12'tr>>" +
					"<'row table-footer-row'<'col-xs-6'i>" + (useExcelExport ? "<'col-xs-1 excel-export-action left'B>" : "") + ">",
				"fnRowCallback": function (nRow) {
					$(nRow).addClass(tableIdentifier + '-row');
					return nRow;
				}
			});

			var tableWrapper = $("#" + tableIdentifier + "_wrapper");

			tableWrapper.find('.length-filter-total-items').html(data.dataset.length);

			table.find('img').load(function () {
				dataTable.api().columns.adjust().draw();
			});

			if (!$.SalesPortal.Content.isMobileDevice())
				$("#" + tableIdentifier + "_length").find('select').selectpicker();

			if (useExcelExport)
			{
				var excelExportActionContent = tableWrapper.find('.excel-export-action');
				excelExportActionContent.hide();
			}

			if (backHandler !== undefined)
			{
				var backUrlContent = tableWrapper.find('.back-url');
				backUrlContent.html(
					'<a href="#" style="text-decoration: underline">New Search</a>'
				);
				backUrlContent.find('a').on('click', backHandler);
			}

			if (data.dataViewOptions.showDeleteButton)
			{
				table.on('click', '.link-delete-button', function (e) {
					var linkInfo = dataTable.api().row(getDataRowElement($(this))).data();
					deleteHandler(linkInfo);
					e.stopPropagation();
				});
			}

			table.on('click', '.rate-image-container', function (e) {
				e.stopPropagation();

				var linkRow = dataTable.api().row(getDataRowElement($(this)));
				var linkData = linkRow.data();
				var linkId = linkData.id;

				$.SalesPortal.LinkManager.requestRateDialog(linkId, function () {
					$.ajax({
						type: "POST",
						url: window.BaseUrl + "rate/getRate",
						data: {
							linkId: linkData.id
						},
						success: function (msg) {
							var scrollBody = $(".dataTables_scrollBody");
							var scrollPos = scrollBody.scrollTop();
							linkData.rate.value = msg.totalRate;
							linkData.rate.image = msg.totalRateImage;
							linkRow.invalidate().draw();
							scrollBody.scrollTop(scrollPos);
						},
						error: function () {
						},
						async: true,
						dataType: 'json'
					});
				});
			});

			if ($.SalesPortal.Content.isMobileDevice())
			{
				table.find('.link-file, .link-common').hammer().on('tap', function () {
					var linkId = dataTable.api().row(getDataRowElement($(this))).data().id;
					$.SalesPortal.LinkManager.requestViewDialog({
						linkId: linkId,
						isQuickSite: false
					});
				});

				table.find('.link-url').hammer().on('tap', function () {
					var linkId = dataTable.api().row(getDataRowElement($(this))).data().id;
					var url = $(this).find('.link-content').prop('href');
					$.SalesPortal.LogHelper.write({
						type: 'Link',
						subType: 'Open',
						linkId: linkId,
						data: {
							file: url
						}
					});
				});

				table.find('.link-file, .link-url, .link-common').hammer().on('hold', function (event) {
					var linkId = dataTable.api().row(getDataRowElement($(this))).data().id;
					$.SalesPortal.LinkManager.requestLinkContextMenu(
						linkId,
						false,
						false,
						event.gesture.center.pageX,
						event.gesture.center.pageY,
						extendLinkMenu);
					event.gesture.stopPropagation();
					event.gesture.preventDefault();
				});
			}
			else
			{
				table.on('click', '.link-file, .link-common', function () {
					var linkId = dataTable.api().row(getDataRowElement($(this))).data().id;
					$.SalesPortal.LinkManager.requestViewDialog({
						linkId: linkId,
						isQuickSite: false
					});
				});

				table.on('click', '.link-url', function () {
					var url = $(this).find('.link-content').prop('href');
					var linkId = dataTable.api().row(getDataRowElement($(this))).data().id;
					$.SalesPortal.LogHelper.write({
						type: 'Link',
						subType: 'Open',
						linkId: linkId,
						data: {
							file: url
						}
					});
				});

				table.on('contextmenu', '.link-file, .link-url-internal, .link-common', function (event) {
					var linkId = dataTable.api().row(getDataRowElement($(this))).data().id;
					$.SalesPortal.LinkManager.requestLinkContextMenu(
						linkId,
						false,
						false,
						event.clientX,
						event.clientY,
						extendLinkMenu);
					return false;
				});

				if (!$.SalesPortal.Content.isEOBrowser())
				{
					table.on('contextmenu', '.link-url-external', function (event) {
						var linkId = dataTable.api().row(getDataRowElement($(this))).data().id;
						$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, false, event.clientX, event.clientY, extendLinkMenu);
						return false;
					});
				}

				table.on('dragstart', '.is-draggable', function (e) {
					var urlHeader = $(this).find('.link-content').data("url-header");
					var url = $(this).find('.link-content').data('url');
					if (url !== '')
						e.originalEvent.dataTransfer.setData(urlHeader, url);
				});
			}

			table.on('search.dt', logHandler).on('page.dt', logHandler).on('length.dt', logHandler);

			tableWrapper.find('.tooltipster-target').tooltipster({contentAsHTML: true});
		};

		this.updateSize = function () {
			if (dataTable !== undefined)
			{
				if (sizeSettings.useFixedSize)
				{
					var content = $.SalesPortal.Content.getContentObject();
					if (parentContainerSelector !== undefined)
						content = $.SalesPortal.Content.getContentObject().find(parentContainerSelector);

					var height = getTableSize();
					content.find("#" + tableIdentifier + "_wrapper").find('.dataTables_scrollBody').css({
						'height': height + 'px'
					});
					var oSettings = dataTable.fnSettings();
					if (oSettings)
						oSettings.oScroll.sY = height + 'px';
				}
				dataTable.api().columns.adjust().draw();

				updateResponsiveColumnsTimer = setTimeout(function () {
					$.SalesPortal.ScreenManager.processScreenSizeChange(updateResponsiveColumns);
				}, 200);
			}
		};

		this.clear = function () {
			if (dataTable !== undefined)
				dataTable.api().state.clear();
		};

		this.getTable = function () {
			return dataTable.api();
		};

		var extendLinkMenu = function (menu) {
			if (useExcelExport)
			{
				menu.append($('<li role="separator" class="divider"></li><li><a class="export-excel-action" href="#">Save this list to Excel</a></li>'));
				menu.find('.export-excel-action').on('click', function () {
					var excelExportActionContent = $("#" + tableIdentifier + "_wrapper").find('.excel-export-action');
					var actionButton = excelExportActionContent.find('.buttons-excel');
					actionButton.click();
				});
			}
		};

		var getDataRowElement = function (cellItem) {
			var tableRow = cellItem.closest("tr");
			if (tableRow.hasClass('child'))
				return tableRow.prev('tr.parent');
			return tableRow;
		};

		var updateResponsiveColumns = function () {
			updateResponsiveColumnsTimer = null;

			$.SalesPortal.Overlay.show();

			$.each(columnVisibilitySettings, function (index, value) {
				var visibilitySettings = getIsColumnVisibile(value);
				var column = dataTable.api().columns(index);
				column.visible(visibilitySettings, false);
			});

			dataTable.api().columns.adjust().draw();

			$.SalesPortal.Overlay.hide();

			updateResponsiveColumnsTimer = null;
		};

		var getIsColumnVisibile = function (visibilitySettings) {
			var screenSettings = $.SalesPortal.ScreenManager.getScreenSettings();
			switch (screenSettings.screenSizeType)
			{
				case "large":
					return visibilitySettings.large;
				case "medium":
					return visibilitySettings.medium;
				case "small":
					return visibilitySettings.small;
				case "extrasmall":
					return visibilitySettings.extraSmall;
			}
			return true;
		};

		var cellRenderer = function (data, type, row) {
			if (type === "display")
			{
				var content = '';
				var objectClass = '';
				var tooltipsterClass = '';

				if (row && row !== '')
				{
					var displayValue = data && typeof data === 'object' ? data.display : data;
					displayValue = displayValue !== null ? displayValue : '';

					var dragData = '';
					if (row.isDraggable)
					{
						objectClass += ' is-draggable';
						dragData = 'draggable="true" data-url-header="' + row.url_header + '" data-url="' + row.url + '"';
					}

					if (row.isHyperlink)
					{
						content = '<a class="link-content tooltipster-target" title="' + row.tooltip + '" href="' + row.url + '" target="_blank" ' + dragData + '>' + displayValue + '</a>';
						objectClass = ' link-url';
						if (row.isExternalHyperlink)
							objectClass += ' link-url-external';
						else
							objectClass += ' link-url-internal';
					}
					else if (row.isFile)
					{
						content = '<span class="link-content tooltipster-target" ' + dragData + ' title="' + row.tooltip + '">' + displayValue + '</span>';
						objectClass += ' link-file';
					}
					else
					{
						content = '<span class="link-content tooltipster-target" ' + dragData + ' title="' + row.tooltip + '">' + displayValue + '</span>';
						objectClass += ' link-common';
					}
				}
				return '<span class="clickable-area' + objectClass + '">' + content + '</span>';
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

		var destroy = function () {
			if (dataTable !== undefined)
				dataTable.fnDestroy();
		};

		var getTableSize = function () {
			if (sizeSettings.useFixedSize)
			{
				if (sizeSettings.size === 'screen')
					return $.SalesPortal.Content.isMobileDevice() ? getNativeTableSize() : getBootstrapTableSize();
				else
					return sizeSettings.size;
			}
			else
				return undefined;
		};

		var getBootstrapTableSize = function () {
			var content = $.SalesPortal.Content.getContentObject();
			if (parentContainerSelector !== undefined)
				content = $.SalesPortal.Content.getContentObject().find(parentContainerSelector);

			var topHeight = content.find("#" + tableIdentifier + "_wrapper .table-header-row").outerHeight(true);
			var bottomHeight = content.find("#" + tableIdentifier + "_wrapper .table-footer-row").outerHeight(true);

			var tableHeaderHeight = 0;
			$.each(content.find("#" + tableIdentifier + "_wrapper").find('.table-header-row'), function (index, value) {
				tableHeaderHeight += $(value).outerHeight(true);
			});

			var containerOuterHeight = content.outerHeight(true);
			var containerInnerHeight = content.height();
			if (tableContainerSelector !== parentContainerSelector)
			{
				containerOuterHeight = content.find(tableContainerSelector).outerHeight(true);
				containerInnerHeight = content.find(tableContainerSelector).height();
			}

			var conditionDescriptionContent = content.find('.search-app-footer').outerHeight(true);

			return content.outerHeight(true) - (topHeight + bottomHeight + tableHeaderHeight + conditionDescriptionContent + (containerOuterHeight - containerInnerHeight));
		};

		var getNativeTableSize = function () {
			var content = $.SalesPortal.Content.getContentObject();
			if (parentContainerSelector !== undefined)
				content = $.SalesPortal.Content.getContentObject().find(parentContainerSelector);

			var topHeight = content.find("#" + tableIdentifier + "_wrapper .table-header-row").outerHeight(true);
			var bottomHeight = content.find("#" + tableIdentifier + "_wrapper .table-footer-row").outerHeight(true);

			var tableHeaderHeight = 0;
			$.each(content.find("#" + tableIdentifier + "_wrapper").find('.table-header-row'), function (index, value) {
				tableHeaderHeight += $(value).outerHeight(true);
			});

			var containerOuterHeight = content.outerHeight(true);
			var containerInnerHeight = content.height();
			if (tableContainerSelector !== parentContainerSelector)
			{
				containerOuterHeight = content.find(tableContainerSelector).outerHeight(true);
				containerInnerHeight = content.find(tableContainerSelector).height();
			}

			var conditionDescriptionContent = content.find('.search-app-footer').outerHeight(true);

			return content.height() - (topHeight + bottomHeight + tableHeaderHeight + conditionDescriptionContent + (containerOuterHeight - containerInnerHeight)) - 5;
		};
	};
})(jQuery);
