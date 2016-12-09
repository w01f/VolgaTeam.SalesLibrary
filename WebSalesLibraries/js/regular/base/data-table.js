(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.SearchDataTable = function (tableOptions)
	{
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
				showCategory: false,
				showLibraries: false,
				showType: false,
				showDate: false,
				showRate: false,
				showViewsCount: false,
				showDeleteButton: false,
				categoryColumnName: '',
				librariesColumnName: '',
				reorderSourceField: undefined
			};
			data.sortDirection = data.sortDirection != undefined ? data.sortDirection : 'asc';


			var sortColumnIndex = -1;
			if (data.dataOptions.reorderSourceField == undefined)
			{
				var index = 0;
				var sortColumnsIndexes = {};
				if (data.dataOptions.showCategory)
				{
					sortColumnsIndexes['tag'] = index;
					index++;
				}
				if (data.dataOptions.showLibraries)
				{
					sortColumnsIndexes['library'] = index;
					index++;
				}
				if (data.dataOptions.showType)
				{
					sortColumnsIndexes['file_type'] = index;
					index++;
				}
				sortColumnsIndexes['name'] = index;
				index++;
				if (data.dataOptions.showViewsCount)
				{
					sortColumnsIndexes['views'] = index;
					index++;
				}
				if (data.dataOptions.showRate)
				{
					sortColumnsIndexes['rate'] = index;
					index++;
				}
				if (data.dataOptions.showDate)
				{
					sortColumnsIndexes['date_modify'] = index;
				}
				sortColumnIndex = data.sortColumnTag != undefined ? sortColumnsIndexes[data.sortColumnTag] : -1;
			}
			else
				sortColumnIndex = 0;


			var content = $.SalesPortal.Content.getContentObject();

			var tableContainer = content.find(tableContainerSelector);

			if (!$.SalesPortal.Content.isMobileDevice())
				tableContainer.html('<table id="' + tableIdentifier + '" class="table link-data-table table-striped table-bordered"></table>');
			else
				tableContainer.html('<table id="' + tableIdentifier + '" class="table link-data-table-table table-striped"></table>');

			var table = $("#" + tableIdentifier);

			var hasCategories = false;

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

			if (data.dataOptions.showCategory && hasCategories)
				columnSettings.push({
					"data": "tag",
					"orderable": data.dataOptions.reorderSourceField == undefined,
					"title": data.dataOptions.categoryColumnName,
					"class": "allow-reorder",
					"width": "15%",
					"render": cellRenderer
				});
			if (data.dataOptions.showLibraries)
				columnSettings.push({
					"data": "library.name",
					"orderable": data.dataOptions.reorderSourceField == undefined,
					"title": data.dataOptions.librariesColumnName,
					"class": "centered allow-reorder",
					"width": "10%",
					"render": cellRenderer
				});
			if (data.dataOptions.showType)
				columnSettings.push({
					"data": "file_type",
					"orderable": data.dataOptions.reorderSourceField == undefined,
					"title": "Type",
					"class": "centered allow-reorder",
					"width": "50px",
					"render": cellRenderer
				});
			columnSettings.push({
				"data": "name",
				"orderable": data.dataOptions.reorderSourceField == undefined,
				"title": "Link",
				"class": "allow-reorder",
				"render": cellRenderer
			});
			if (data.dataOptions.showViewsCount)
				columnSettings.push({
					"data": "views",
					"orderable": data.dataOptions.reorderSourceField == undefined,
					"title": "Views",
					"class": "centered allow-reorder",
					"width": "50px",
					"sType": "numeric",
					"render": {
						_: cellRenderer,
						sort: 'value'
					}
				});
			if (data.dataOptions.showRate)
				columnSettings.push({
					"data": "rate",
					"orderable": data.dataOptions.reorderSourceField == undefined,
					"title": "Rating",
					"width": "90px",
					"class": "centered allow-reorder rate-image-container",
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
			if (data.dataOptions.showDate)
			{
				columnSettings.push({
					"data": "date",
					"orderable": data.dataOptions.reorderSourceField == undefined,
					"title": "Date",
					"class": "centered allow-reorder",
					"width": "80px",
					"render": {
						_: cellRenderer,
						sort: 'value'
					}
				});
				columnSettings.push({
					"data": "date.value",
					"title": "Date",
					"visible": false,
					"searchable": false,
					"sType": "numeric"
				});
			}
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
					selector: 'td.allow-reorder',
					snapX: 0
				} :
					false,
				"scrollY": $.SalesPortal.Content.isMobileDevice() ? getNativeTableSize() : getBootstrapTableSize(),
				"scrollCollapse": false,
				"aLengthMenu": [
					[15, 25, 50, 100, -1],
					[15, 25, 50, 100, "All"]
				],
				"iDisplayLength": 15,
				"oLanguage": {
					"sEmptyTable": "",
					"sZeroRecords": ""
				},
				"dom": (data.dataset.length > 0 ?
					"<'row'<'col-xs-4'l><'col-xs-4 back-url text-center'><'col-xs-4'f>>" :
					"<'row'<'col-xs-12 back-url text-center'>>") +
				"<'row'<'col-xs-12'tr>>" +
				"<'row'<'col-xs-5'i><'col-xs-7'p>>",
				"fnRowCallback": function (nRow, aData, iDisplayIndex)
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
					'<a href="#">Click <strong style="text-decoration: underline;">HERE</strong> for a New Search</a>'
				);
				backUrlContent.find('a').on('click', backHandler);
			}

			if (data.dataOptions.showDeleteButton)
			{
				table.on('click', '.link-delete-button', function (e)
				{
					var linkInfo = dataTable.api().row($(this).closest("tr")).data();
					deleteHandler(linkInfo);
					e.stopPropagation();
				});
			}

			table.on('click', '.rate-image-container', function (e)
			{
				e.stopPropagation();

				var linkRow = dataTable.api().row($(this).closest("tr"));
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
					var linkId = dataTable.api().row($(this).closest("tr")).data().id;
					$.SalesPortal.LinkManager.requestViewDialog({
						linkId: linkId,
						isQuickSite: false
					});
				});

				table.find('.link-url').hammer().on('tap', function ()
				{
					var linkId = dataTable.api().row($(this).closest("tr")).data().id;
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
					var linkId = dataTable.api().row($(this).closest("tr")).data().id;
					$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, event.gesture.center.pageX, event.gesture.center.pageY);
					event.gesture.stopPropagation();
					event.gesture.preventDefault();
				});
			}
			else
			{
				table.on('click', '.link-file, .link-common', function ()
				{
					var linkId = dataTable.api().row($(this).closest("tr")).data().id;
					$.SalesPortal.LinkManager.requestViewDialog({
						linkId: linkId,
						isQuickSite: false
					});
				});

				table.on('click', '.link-url', function ()
				{
					var url = $(this).find('.link-content').prop('href');
					var linkId = dataTable.api().row($(this).closest("tr")).data().id;
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
					var linkId = dataTable.api().row($(this).closest("tr")).data().id;
					$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, event.clientX, event.clientY);
					return false;
				});

				if (!$.SalesPortal.Content.isEOBrowser())
				{
					table.on('contextmenu', '.link-url-external', function (event)
					{
						var linkId = dataTable.api().row($(this).closest("tr")).data().id;
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
				dataTable.fnSettings().oScroll.sY = height + 'px';
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

		var cellRenderer = function (data, type, row)
		{
			var cellContent = '';
			var objectClass = '';

			if (type == "display")
			{
				var displayValue = typeof data === 'object' && data != null ? data.display : data;
				displayValue = displayValue != null ? displayValue : '';

				if (row != '')
				{
					var dragData = '';
					if (row.isDraggable)
					{
						objectClass += ' is-draggable';
						dragData = 'draggable="true" data-url-header="' + row.url_header + '" data-url="' + row.url + '"';
					}

					if (row.isHyperlink)
					{
						cellContent = '<a class="link-content" title="' + row.tooltip + '" href="' + row.url + '" target="_blank" ' + dragData + '>' + displayValue + '</a>';
						objectClass = ' link-url';
						if (row.isExternalHyperlink)
							objectClass += ' link-url-external';
						else
							objectClass += ' link-url-internal';
					}
					else if (row.isFile)
					{
						cellContent = '<span class="link-content" ' + dragData + ' title="' + row.tooltip + '">' + displayValue + '</span>';
						objectClass += ' link-file';
					}
					else
					{
						cellContent = '<span class="link-content" ' + dragData + ' title="' + row.tooltip + '">' + displayValue + '</span>';
						objectClass += ' link-common';
					}
				}
			}
			else
				cellContent = data;
			return '<span class="clickable-area' + objectClass + '">' + cellContent + '</span>';
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

			var topHeight = content.find("#" + tableIdentifier + "_length").closest('.row').outerHeight(true);
			if (topHeight == undefined)
				topHeight = content.find("#" + tableIdentifier + "_filter").closest('.row').outerHeight(true);
			var bottomHeight = content.find("#" + tableIdentifier + "_info").closest('.row').outerHeight(true);

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

			var topHeight = content.find("#" + tableIdentifier + "_length").outerHeight(true);
			if (topHeight == undefined)
				topHeight = content.find("#" + tableIdentifier + "_filter").outerHeight(true);
			var bottomHeight = content.find("#" + tableIdentifier + "_info").outerHeight(true);

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
