(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.SearchDataTable = function (options)
	{
		var saveState = options != undefined ? options.saveSate : undefined;
		var deleteHandler = options != undefined ? options.deleteHandler : undefined;
		var backHandler = options != undefined ? options.backHandler : undefined;
		var logHandler = options != undefined ? options.logHandler : undefined;

		var dataTable = undefined;

		this.init = function (dataset, viewOptions, sortColumnTag, sortDirection)
		{
			destroy();

			var defaultSortColumnIndex = 1;
			if (viewOptions.showCategory)
				defaultSortColumnIndex++;
			if (viewOptions.showLibraries)
				defaultSortColumnIndex++;

			var sortColumnIndex = defaultSortColumnIndex;
			switch (sortColumnTag)
			{
				case "library":
					sortColumnIndex = 1;
					break;
				case "file_type":
					sortColumnIndex = 2;
					break;
				case "name":
					sortColumnIndex = 3;
					break;
				case "date_modify":
					sortColumnIndex = 6;
					break;
				case "tag":
					sortColumnIndex = 0;
					break;
				case "rate":
					sortColumnIndex = 5;
					break;
				case "views":
					sortColumnIndex = 4;
					break;
			}

			var content = $.SalesPortal.Content.getContentObject();

			var tableContainer = content.find('.data-table-content-container');

			if (!$.SalesPortal.Content.isMobileDevice())
				tableContainer.html('<table id="data-table-content" class="table table-striped table-bordered"></table>');
			else
				tableContainer.html('<table id="data-table-content" class="table table-striped"></table>');

			var table = $("#data-table-content");

			var columnSettings = [];
			if (viewOptions.showCategory)
				columnSettings.push({
					"data": "tag",
					"title": viewOptions.categoryColumnName,
					"width": "15%",
					"render": cellRenderer
				});
			if (viewOptions.showLibraries)
				columnSettings.push({
					"data": "library.name",
					"title": viewOptions.librariesColumnName,
					"class": "centered",
					"width": "10%",
					"render": cellRenderer
				});
			columnSettings.push({
				"data": "file_type",
				"title": "Type",
				"class": "centered",
				"width": "50px",
				"render": cellRenderer
			});
			columnSettings.push({
				"data": "name",
				"title": "Link",
				"render": cellRenderer
			});
			columnSettings.push({
				"data": "views",
				"title": "Views",
				"class": "centered",
				"width": "50px",
				"sType": "numeric",
				"render": {
					_: cellRenderer,
					sort: 'value'
				}
			});
			columnSettings.push({
				"data": "rate",
				"title": "Rating",
				"width": "90px",
				"class": "centered",
				"render": {
					_: function (data)
					{
						var cellContent = '';
						if (data.image != '')
							cellContent = '<img src="' + data.image + '">';
						return '<div class="clickable-area rate-image-container">' + cellContent + '</div>';
					},
					sort: 'value'
				}
			});
			columnSettings.push({
				"data": "date",
				"title": "Date",
				"class": "centered",
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
			if (viewOptions.showDeleteButton)
				columnSettings.push({
					"data": null,
					"title": '',
					"width": "5px",
					"orderable": false,
					"defaultContent": '<img class="link-delete-button" src="' + window.BaseUrl + 'images/search/search-delete.png">'
				});
			columnSettings.push({
				"data": "id",
				"title": "Id",
				"visible": false,
				"searchable": false
			});

			dataTable = table.dataTable({
				"data": dataset != undefined ? dataset : [],
				"columns": columnSettings,
				stateSave: saveState,
				"order": [
					[sortColumnIndex, sortDirection != undefined ? sortDirection : "asc"]
				],
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
				"dom": "<'row'<'col-xs-4'l><'col-xs-4 back-url text-center'><'col-xs-4'f>>" +
				"<'row'<'col-xs-12'tr>>" +
				"<'row'<'col-xs-5'i><'col-xs-7'p>>"
			});
			if (!$.SalesPortal.Content.isMobileDevice())
				$("#data-table-content_length").find('select').selectpicker();

			if (backHandler != undefined)
			{
				var backUrlContent = $("#data-table-content_wrapper").find('.back-url');
				backUrlContent.html(
					'<a href="#">Click <strong><u>HERE</u></strong> for a New Search</a>'
				);
				backUrlContent.find('a').on('click', backHandler);
			}

			if (viewOptions.showDeleteButton && deleteHandler != undefined)
			{
				table.on('click', '.link-delete-button', function (e)
				{
					var linkId = dataTable.api().row($(this).closest("tr")).data().id;
					deleteHandler(linkId);
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
							linkData.rate.value = msg.totalRate;
							linkData.rate.image = msg.totalRateImage;
							linkRow.invalidate().draw();
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
				table.find('.link-file').hammer().on('tap', function ()
				{
					var linkId = dataTable.api().row($(this).closest("tr")).data().id;
					$.SalesPortal.LinkManager.requestViewDialog(linkId, false);
				});

				table.find('.link-url').hammer().on('tap', function ()
				{
					$('body').find('.mtContent').remove();
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

				table.find('.link-file, .link-url').hammer().on('hold', function (event)
				{
					var linkId = dataTable.api().row($(this).closest("tr")).data().id;
					$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, event.gesture.center.pageX, event.gesture.center.pageY);
					event.gesture.stopPropagation();
					event.gesture.preventDefault();
				});
			}
			else
			{
				table.on('click', '.link-file', function ()
				{
					var linkId = dataTable.api().row($(this).closest("tr")).data().id;
					$.SalesPortal.LinkManager.requestViewDialog(linkId, false);
				});

				table.on('click', '.link-url', function ()
				{
					$('body').find('.mtContent').remove();
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

				table.on('contextmenu', '.link-file, .link-url-internal', function (event)
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

				table.on('dragstart', '.link-file', function (e)
				{
					var urlHeader = $(this).find('.link-content').data("url-header");
					var url = $(this).find('.link-content').data("url");
					if (url != '')
						e.originalEvent.dataTransfer.setData(urlHeader, url);
				});
			}

			if (logHandler != undefined)
			{
				table.on('search.dt', logHandler).on('page.dt', logHandler).on('length.dt', logHandler);
			}
		};

		this.updateSize = function ()
		{
			if (dataTable != undefined)
			{
				var height = $.SalesPortal.Content.isMobileDevice() ? getNativeTableSize() : getBootstrapTableSize();
				$('#data-table-content_wrapper').find('.dataTables_scrollBody').css({
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
					if (row.isHyperlink)
					{
						cellContent = '<a class="mtTool link-content" mtcontent="' + row.tooltip + '" href="' + row.url + '" target="_blank">' + displayValue + '</a>';
						objectClass = ' link-url';
						if (row.isExternalHyperlink)
							objectClass += ' link-url-external';
						else
							objectClass += ' link-url-internal';
					}
					else if (row.isDraggable)
					{
						cellContent = '<span class="mtTool link-content" draggable="true" data-url-header="' + row.url_header + '" data-url="' + row.url + '" mtcontent="' + row.tooltip + '">' + displayValue + '</span>';
						objectClass = ' link-file';
					}
					else
						cellContent = '<span class="mtTool link-content" mtcontent="' + row.tooltip + '">' + displayValue + '</span>';
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
			var content = $('#content');

			var topHeight = $('#data-table-content_length').closest('.row').outerHeight(true);
			var bottomHeight = $('#data-table-content_info').closest('.row').outerHeight(true);

			var tableHeaderHeight = $('#data-table-content_wrapper').find('.dataTables_scrollHead').outerHeight(true);

			var containerOuterHeight = content.find('.data-table-content-container').outerHeight(true);
			var containerInnerHeight = content.find('.data-table-content-container').height();

			var conditionDescriptionContent = content.find('.search-app-footer').outerHeight(true);

			return content.height() - (topHeight + bottomHeight + tableHeaderHeight + conditionDescriptionContent + (containerOuterHeight - containerInnerHeight)) - 5;
		};

		var getNativeTableSize = function ()
		{
			var content = $('#content');

			var topHeight = $('#data-table-content_length').outerHeight(true);
			var bottomHeight = $('#data-table-content_info').outerHeight(true);

			var tableHeaderHeight = $('#data-table-content_wrapper').find('.dataTables_scrollHead').outerHeight(true);

			var containerOuterHeight = content.find('.data-table-content-container').outerHeight(true);
			var containerInnerHeight = content.find('.data-table-content-container').height();

			var conditionDescriptionContent = content.find('.search-app-footer').outerHeight(true);

			return content.height() - (topHeight + bottomHeight + tableHeaderHeight + conditionDescriptionContent + (containerOuterHeight - containerInnerHeight)) - 5;
		};
	};
})(jQuery);
