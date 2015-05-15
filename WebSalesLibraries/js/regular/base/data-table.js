(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.SearchDataTable = function (saveState)
	{
		var dataTable = undefined;

		this.init = function (searchResults, sortColumnTag, sortDirection)
		{
			destroy();

			var sortColumnIndex = 3;
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
					sortColumnIndex = 5;
					break;
				case "tag":
					sortColumnIndex = 0;
					break;
				case "rate":
					sortColumnIndex = 4;
					break;
			}

			var content = $('#content');

			var tableContainer = content.find('.search-results-container');
			tableContainer.html('<table id="search-results" class="table table-striped table-bordered"></table>');
			var table = $("#search-results");

			dataTable = table
				.dataTable({
					"data": searchResults != undefined ? searchResults.dataset : [],
					"columns": [
						{
							"data": "tag",
							"title": "Category",
							"width": "15%"
						},
						{
							"data": "library.name",
							"title": "Station",
							"class": "centered",
							"width": "10%"
						},
						{
							"data": "file_type",
							"title": "Type",
							"class": "centered",
							"width": "50px"
						},
						{
							"data": "name",
							"title": "Link",
							"render": function (data)
							{
								if (data != '')
									return '<span class="mtTool" mtcontent="' + data.tooltip + '">' + data.value + '</span>';
								return '';
							}
						},
						{
							"data": "rate.image",
							"title": "Rating",
							"width": "90px",
							"class": "centered",
							"render": function (data)
							{
								if (data != '')
									return '<img src="' + data + '" style="height:16px">';
								return '';
							}
						},
						{
							"data": "date.display",
							"title": "Date",
							"class": "centered",
							"width": "80px"
						},
						{
							"data": "id",
							"title": "Id",
							"visible": false,
							"searchable": false
						}
					],
					stateSave: saveState,
					"order": [
						[ sortColumnIndex, sortDirection != undefined ? sortDirection : "asc" ]
					],
					"scrollY": getTableSize(),
					"scrollCollapse": false,
					"aLengthMenu": [
						[15, 25, 50, 100 , -1],
						[15, 25, 50, 100, "All"]
					],
					"iDisplayLength": 15,
					"oLanguage": {
						"sEmptyTable": "",
						"sZeroRecords": ""
					}
				});
			$("#search-results_length").find('select').selectpicker();
			table.on('click', 'tr', function ()
			{
				var linkId = dataTable.api().row(this).data().id;
				$.SalesPortal.LinkManager.requestViewDialog(linkId, false);
			});
			$.mtReInit();
		};

		this.updateSize = function ()
		{
			if (dataTable != undefined)
			{
				var height = getTableSize();
				$('#search-results_wrapper').find('.dataTables_scrollBody').css({
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

		var destroy = function ()
		{
			if (dataTable != undefined)
				dataTable.fnDestroy();
		};

		var getTableSize = function ()
		{
			var content = $('#content');

			var topHeight = $('#search-results_length').closest('.row').outerHeight(true);
			var bottomHeight = $('#search-results_info').closest('.row').outerHeight(true);

			var tableHeaderHeight = $('#search-results_wrapper').find('.dataTables_scrollHead').outerHeight(true);

			var containerOuterHeight = content.find('.search-results-container').outerHeight(true);
			var containerInnerHeight = content.find('.search-results-container').height();

			var aboveContent = content.find('.search-results-above').outerHeight(true);

			return content.height() - (topHeight + bottomHeight + tableHeaderHeight + aboveContent + (containerOuterHeight - containerInnerHeight)) - 5;
		};
	};
})(jQuery);
