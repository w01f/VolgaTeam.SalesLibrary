(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.SearchDataTable = function (tableOptions)
	{
		const ColumnTagCategory = 'tag';
		const ColumnTagLibrary = 'library';
		const ColumnTagType = 'type';
		const ColumnTagName = 'link';
		const ColumnTagThumbnail = 'thumbnail';
		const ColumnTagViews = 'views';
		const ColumnTagRate = 'rate';
		const ColumnTagDate = 'date';

		var tableIdentifier = tableOptions != undefined && tableOptions.tableIdentifier != undefined ?
			tableOptions.tableIdentifier :
			'data-table-content';
		var tableContainerSelector = tableOptions != undefined && tableOptions.tableContainerSelector != undefined ?
			tableOptions.tableContainerSelector :
			undefined;
		var parentContainerSelector = tableOptions != undefined && tableOptions.parentContainerSelector != undefined ?
			tableOptions.parentContainerSelector :
			undefined;
		var saveState = tableOptions != undefined && tableOptions.saveSate != undefined ?
			tableOptions.saveSate :
			false;
		var usePaginate = tableOptions != undefined && tableOptions.paginate != undefined ?
			tableOptions.paginate :
			true;
		var useSubSearch = tableOptions != undefined && tableOptions.subSearch != undefined ?
			tableOptions.subSearch :
			true;
		var deleteHandler = tableOptions != undefined && tableOptions.deleteHandler != undefined ?
			tableOptions.deleteHandler :
			function ()
			{
			};
		var backHandler = tableOptions != undefined && tableOptions.backHandler != undefined ?
			tableOptions.backHandler :
			undefined;
		var logHandler = tableOptions != undefined && tableOptions.logHandler != undefined ?
			tableOptions.logHandler :
			function ()
			{
			};

		var dataTable = undefined;

		this.init = function (data)
		{
			destroy();

			if (data == undefined)
				data = {
					dataset: undefined,
					dataOptions: undefined,
					sortColumnTag: undefined,
					sortDirection: undefined
				};
			data.dataset = data.dataset != undefined ? data.dataset : [];
			data.dataOptions = data.dataOptions != undefined ? data.dataOptions : {
					columnSettings: undefined,
					showDeleteButton: false,
					reorderSourceField: undefined
				};
			data.sortColumnTag = data.sortColumnTag != undefined ? data.sortColumnTag : ColumnTagName;
			data.sortDirection = data.sortDirection != undefined ? data.sortDirection : 'asc';


			var content = $.SalesPortal.Content.getContentObject();
			var tableContainer = content.find(tableContainerSelector);
			if (!$.SalesPortal.Content.isMobileDevice())
				tableContainer.html('<table id="' + tableIdentifier + '" class="table link-data-table table-striped table-bordered"></table>');
			else
				tableContainer.html('<table id="' + tableIdentifier + '" class="table link-data-table-table table-striped"></table>');

			var table = $("#" + tableIdentifier);

			var hasCategories = false;
			if (data.dataOptions.columnSettings[ColumnTagCategory].enable)
				$.each(data.dataset, function (index, value)
				{
					if (value.tag != null && value.tag != '')
					{
						hasCategories = true;
						return false;
					}
				});

			var columnSettings = [];

			if (data.dataOptions.reorderSourceField != undefined)
				columnSettings.push({
					"data": 'extended_data.' + data.dataOptions.reorderSourceField,
					"visible": false,
					"searchable": false,
					"orderable": true
				});

			var sortColumnIndex = 0;
			var columnIndex = 0;
			$.each(data.dataOptions.columnSettings, function (key, value)
			{
				if (data.dataOptions.reorderSourceField == undefined && key == data.sortColumnTag)
					sortColumnIndex = columnIndex;
				switch (key)
				{
					case ColumnTagCategory:
						if (value.enable && hasCategories)
						{
							columnSettings.push({
								"data": "tag",
								"orderable": data.dataOptions.reorderSourceField == undefined,
								"title": value.title,
								"class": "tag-text-container allow-reorder" + (value.fullWidth ? ' none' : ' all'),
								"width": value.width > 0 ? (value.width + "px") : "15%",
								"render": cellRenderer
							});
							columnIndex++;
						}
						break;
					case ColumnTagLibrary:
						if (value.enable)
						{
							columnSettings.push({
								"data": "library.name",
								"orderable": data.dataOptions.reorderSourceField == undefined,
								"title": value.title,
								"class": "centered allow-reorder" + (value.fullWidth ? ' none' : ' all'),
								"width": value.width > 0 ? (value.width + "px") : "10%",
								"render": cellRenderer
							});
							columnIndex++;
						}
						break;
					case ColumnTagType:
						if (value.enable)
						{
							columnSettings.push({
								"data": "file_type",
								"orderable": data.dataOptions.reorderSourceField == undefined,
								"title": value.title,
								"class": "centered allow-reorder" + (value.fullWidth ? ' none' : ' all'),
								"width": value.width > 0 ? (value.width + "px") : "70px",
								"render": cellRenderer
							});
							columnIndex++;
						}
						break;
					case ColumnTagName:
						if (value.enable)
						{
							columnSettings.push({
								"data": "name",
								"orderable": data.dataOptions.reorderSourceField == undefined,
								"title": value.title,
								"class": "link-name-text-container" + (value.fullWidth ? ' none' : ' all'),
								"width": value.width > 0 ? (value.width + "px") : null,
								"render": cellRenderer
							});
							columnIndex++;
						}
						break;
					case ColumnTagThumbnail:
						if (value.enable)
						{
							columnSettings.push({
								"data": "thumbnail",
								"orderable": data.dataOptions.reorderSourceField == undefined,
								"title": value.title,
								"width": value.width > 0 ? (value.width + "px") : "90px",
								"class": "centered" + (value.fullWidth ? ' none' : ' all'),
								"render": cellRenderer
							});
							columnIndex++;
						}
						break;
					case ColumnTagViews:
						if (value.enable)
						{
							columnSettings.push({
								"data": "views",
								"orderable": data.dataOptions.reorderSourceField == undefined,
								"title": value.title,
								"class": "centered allow-reorder" + (value.fullWidth ? ' none' : ' all'),
								"width": value.width > 0 ? (value.width + "px") : "50px",
								"sType": "numeric",
								"render": {
									_: cellRenderer,
									sort: 'value'
								}
							});
							columnIndex++;
						}
						break;
					case ColumnTagRate:
						if (value.enable)
						{
							columnSettings.push({
								"data": "rate",
								"orderable": data.dataOptions.reorderSourceField == undefined,
								"title": value.title,
								"width": value.width > 0 ? (value.width + "px") : "90px",
								"class": "centered rate-image-container" + (value.fullWidth ? ' none' : ' all'),
								"render": {
									_: function (columnData)
									{
										var cellContent = '';
										if (columnData.image != '')
											cellContent = '<img src="' + columnData.image + '">';
										return cellContent;
									},
									sort: 'value'
								}
							});
							columnIndex++;
						}
						break;
					case ColumnTagDate:
						if (value.enable)
						{
							columnSettings.push({
								"data": "date",
								"orderable": data.dataOptions.reorderSourceField == undefined,
								"title": value.title,
								"class": "centered allow-reorder" + (value.fullWidth ? ' none' : ' all'),
								"width": value.width > 0 ? (value.width + "px") : "80px",
								"render": {
									_: cellRenderer,
									sort: 'value'
								}
							});
							columnSettings.push({
								"data": "date.value",
								"title": value.title,
								"visible": false,
								"searchable": false,
								"sType": "numeric"
							});

							columnIndex++;
						}
						break;
				}
			});

			if (data.dataOptions.showDeleteButton)
				columnSettings.push({
					"data": null,
					"title": '',
					"width": "5px",
					"orderable": false,
					"defaultContent": '<img class="link-delete-button" src="' + window.BaseUrl + 'images/grid/item-delete.png">'
				});

			columnSettings.push({
				"data": "id",
				"title": "Id",
				"visible": false,
				"searchable": false
			});

			columnSettings.push({
				"data": "extended_data",
				"title": "extended_data",
				"visible": false,
				"searchable": false
			});

			$.extend($.fn.dataTableExt.oStdClasses, {
				"sFilterInput": "form-control",
				"sLengthSelect": "form-control"
			});

			var tableHeaderItemClass = backHandler ? 'col-sm-3' : 'col-sm-4';

			dataTable = table.dataTable({
				"data": data.dataset,
				columns: columnSettings,
				stateSave: saveState,
				paging: usePaginate,
				searching: useSubSearch,
				order: sortColumnIndex >= 0 ? [[sortColumnIndex, data.sortDirection]] : [],
				rowReorder: data.dataOptions.reorderSourceField != undefined ?
					{
						update: true,
						dataSrc: 'extended_data.' + data.dataOptions.reorderSourceField,
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
				"scrollY": $.SalesPortal.Content.isMobileDevice() ? getNativeTableSize() : getBootstrapTableSize(),
				"scrollCollapse": false,
				"aLengthMenu": data.dataset.length < 500 ?
					[
						[15, 25, 50, 100, -1],
						[15, 25, 50, 100, "All"]
					] :
					[
						[15, 25, 50, 100, 200],
						[15, 25, 50, 100, 200]
					],
				"iDisplayLength": 15,
				"oLanguage": {
					"sEmptyTable": "",
					"sZeroRecords": "",
					"sSearch": "Filter:"
				},
				"dom": (data.dataset.length > 0 ?
					"<'row table-header-row'<'" + tableHeaderItemClass + " col-xs-12'l><'" + tableHeaderItemClass + " col-xs-12'f>" +
					(backHandler ? "<'" + tableHeaderItemClass + " col-xs-12 back-url text-center'>" : "") +
					"<'" + tableHeaderItemClass + " col-xs-12'p>>" :
					"<'row'<'col-xs-12 back-url text-center'>>") +
				"<'row table-content-row'<'col-xs-12'tr>>" +
				"<'row table-footer-row'<'col-xs-6'i>>",
				"fnRowCallback": function (nRow)
				{
					$(nRow).addClass(tableIdentifier + '-row');
					return nRow;
				}
			});
			if (!$.SalesPortal.Content.isMobileDevice())
				$("#" + tableIdentifier + "_length").find('select').selectpicker();

			if (backHandler != undefined)
			{
				var backUrlContent = $("#" + tableIdentifier + "_wrapper").find('.back-url');
				backUrlContent.html(
					'<a href="#" style="text-decoration: underline">New Search</a>'
				);
				backUrlContent.find('a').on('click', backHandler);
			}

			if (data.dataOptions.showDeleteButton)
			{
				table.on('click', '.link-delete-button', function (e)
				{
					var linkInfo = dataTable.api().row(getDataRowElement($(this))).data();
					deleteHandler(linkInfo);
					e.stopPropagation();
				});
			}

			table.on('click', '.rate-image-container', function (e)
			{
				e.stopPropagation();

				var linkRow = dataTable.api().row(getDataRowElement($(this)));
				var linkData = linkRow.data();
				var linkId = linkData.id;

				$.SalesPortal.LinkManager.requestRateDialog(linkId, function ()
				{
					$.ajax({
						type: "POST",
						url: window.BaseUrl + "rate/getRate",
						data: {
							linkId: linkData.id
						},
						success: function (msg)
						{
							var scrollBody = $(".dataTables_scrollBody");
							var scrollPos = scrollBody.scrollTop();
							linkData.rate.value = msg.totalRate;
							linkData.rate.image = msg.totalRateImage;
							linkRow.invalidate().draw();
							scrollBody.scrollTop(scrollPos);
						},
						error: function ()
						{
						},
						async: true,
						dataType: 'json'
					});
				});
			});

			if ($.SalesPortal.Content.isMobileDevice())
			{
				table.find('.link-file, .link-common').hammer().on('tap', function ()
				{
					var linkId = dataTable.api().row(getDataRowElement($(this))).data().id;
					$.SalesPortal.LinkManager.requestViewDialog({
						linkId: linkId,
						isQuickSite: false
					});
				});

				table.find('.link-url').hammer().on('tap', function ()
				{
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

				table.find('.link-file, .link-url, .link-common').hammer().on('hold', function (event)
				{
					var linkId = dataTable.api().row(getDataRowElement($(this))).data().id;
					$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, event.gesture.center.pageX, event.gesture.center.pageY);
					event.gesture.stopPropagation();
					event.gesture.preventDefault();
				});
			}
			else
			{
				table.on('click', '.link-file, .link-common', function ()
				{
					var linkId = dataTable.api().row(getDataRowElement($(this))).data().id;
					$.SalesPortal.LinkManager.requestViewDialog({
						linkId: linkId,
						isQuickSite: false
					});
				});

				table.on('click', '.link-url', function ()
				{
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

				table.on('contextmenu', '.link-file, .link-url-internal, .link-common', function (event)
				{
					var linkId = dataTable.api().row(getDataRowElement($(this))).data().id;
					$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, event.clientX, event.clientY);
					return false;
				});

				if (!$.SalesPortal.Content.isEOBrowser())
				{
					table.on('contextmenu', '.link-url-external', function (event)
					{
						var linkId = dataTable.api().row(getDataRowElement($(this))).data().id;
						$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, event.clientX, event.clientY);
						return false;
					});
				}

				table.on('dragstart', '.is-draggable', function (e)
				{
					var urlHeader = $(this).find('.link-content').data("url-header");
					var url = $(this).find('.link-content').data('url');
					if (url != '')
						e.originalEvent.dataTransfer.setData(urlHeader, url);
				});
			}

			table.on('search.dt', logHandler).on('page.dt', logHandler).on('length.dt', logHandler);
		};

		this.updateSize = function ()
		{
			if (dataTable != undefined)
			{
				var content = $.SalesPortal.Content.getContentObject();
				if (parentContainerSelector != undefined)
					content = $.SalesPortal.Content.getContentObject().find(parentContainerSelector);

				var height = $.SalesPortal.Content.isMobileDevice() ? getNativeTableSize() : getBootstrapTableSize();
				content.find("#" + tableIdentifier + "_wrapper").find('.dataTables_scrollBody').css({
					'height': height + 'px'
				});
				var oSettings = dataTable.fnSettings();
				if (oSettings)
					oSettings.oScroll.sY = height + 'px';
				dataTable.api().columns.adjust().draw();
			}
		};

		this.clear = function ()
		{
			if (dataTable != undefined)
				dataTable.api().state.clear();
		};

		this.getTable = function ()
		{
			return dataTable.api();
		};

		var getDataRowElement = function (cellItem)
		{
			var tableRow = cellItem.closest("tr");
			if (tableRow.hasClass('child'))
				return tableRow.prev('tr.parent');
			return tableRow;
		};

		var cellRenderer = function (data, type, row, meta)
		{
			if (type == "display")
			{
				var content = '';
				var objectClass = '';

				if (row && row != '')
				{
					var displayValue = data && typeof data === 'object' ? data.display : data;
					displayValue = displayValue != null ? displayValue : '';

					var dragData = '';
					if (row.isDraggable)
					{
						objectClass += ' is-draggable';
						dragData = 'draggable="true" data-url-header="' + row.url_header + '" data-url="' + row.url + '"';
					}

					if (row.isHyperlink)
					{
						content = '<a class="link-content" title="' + row.tooltip + '" href="' + row.url + '" target="_blank" ' + dragData + '>' + displayValue + '</a>';
						objectClass = ' link-url';
						if (row.isExternalHyperlink)
							objectClass += ' link-url-external';
						else
							objectClass += ' link-url-internal';
					}
					else if (row.isFile)
					{
						content = '<span class="link-content" ' + dragData + ' title="' + row.tooltip + '">' + displayValue + '</span>';
						objectClass += ' link-file';
					}
					else
					{
						content = '<span class="link-content" ' + dragData + ' title="' + row.tooltip + '">' + displayValue + '</span>';
						objectClass += ' link-common';
					}
				}
				return '<span class="clickable-area' + objectClass + '">' + content + '</span>';
			}
			else
				return data;
		};

		var destroy = function ()
		{
			if (dataTable != undefined)
				dataTable.fnDestroy();
		};

		var getBootstrapTableSize = function ()
		{
			var content = $.SalesPortal.Content.getContentObject();
			if (parentContainerSelector != undefined)
				content = $.SalesPortal.Content.getContentObject().find(parentContainerSelector);

			var topHeight = content.find("#" + tableIdentifier + "_wrapper .table-header-row").outerHeight(true);
			var bottomHeight = content.find("#" + tableIdentifier + "_wrapper .table-footer-row").outerHeight(true);

			var tableHeaderHeight = content.find("#" + tableIdentifier + "_wrapper").find('.dataTables_scrollHead').outerHeight(true);

			var containerOuterHeight = content.outerHeight(true);
			var containerInnerHeight = content.height();
			if (tableContainerSelector != parentContainerSelector)
			{
				containerOuterHeight = content.find(tableContainerSelector).outerHeight(true);
				containerInnerHeight = content.find(tableContainerSelector).height();
			}

			var conditionDescriptionContent = content.find('.search-app-footer').outerHeight(true);

			return content.outerHeight(true) - (topHeight + bottomHeight + tableHeaderHeight + conditionDescriptionContent + (containerOuterHeight - containerInnerHeight));
		};

		var getNativeTableSize = function ()
		{
			var content = $.SalesPortal.Content.getContentObject();
			if (parentContainerSelector != undefined)
				content = $.SalesPortal.Content.getContentObject().find(parentContainerSelector);

			var topHeight = content.find("#" + tableIdentifier + "_wrapper .table-header-row").outerHeight(true);
			var bottomHeight = content.find("#" + tableIdentifier + "_wrapper .table-footer-row").outerHeight(true);

			var tableHeaderHeight = content.find("#" + tableIdentifier + "_wrapper").find('.dataTables_scrollHead').outerHeight(true);

			var containerOuterHeight = content.outerHeight(true);
			var containerInnerHeight = content.height();
			if (tableContainerSelector != parentContainerSelector)
			{
				containerOuterHeight = content.find(tableContainerSelector).outerHeight(true);
				containerInnerHeight = content.find(tableContainerSelector).height();
			}

			var conditionDescriptionContent = content.find('.search-app-footer').outerHeight(true);

			return content.height() - (topHeight + bottomHeight + tableHeaderHeight + conditionDescriptionContent + (containerOuterHeight - containerInnerHeight)) - 5;
		};
	};
})(jQuery);
