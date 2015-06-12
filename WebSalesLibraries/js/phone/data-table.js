(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.SearchDataTable = function (searchResults, sortColumnTag, sortDirection, parentPageData)
	{
		var dataset = searchResults;
		var pageContainer = $('#search-results');
		var tableContainer = undefined;
		var contentHeaderContainer = pageContainer.find('.content-header');

		var populateTable = function (dataArray, columnTag, direction)
		{
			var tableBody = tableContainer.find('tbody');
			tableBody.empty();

			dataArray = sortDataset(
				dataArray,
				columnTag,
				direction
			);

			$.each(dataArray, function (i, record)
			{
				var tr = $('<tr>');
				$('<th>').html(record.library.name).appendTo(tr);
				$('<td class="link-name">').html(record.name.value).appendTo(tr);
				$('<td>').html(record.file_type).appendTo(tr);
				$('<td>').html(record.date.display).appendTo(tr);
				$('<td class="link-id">').html(record.id).appendTo(tr);
				tableBody.append(tr);
			});

			tableBody.find('tr').off('click').on('click', function (e)
			{
				$.SalesPortal.LinkManager.requestViewDialog($(this).find('.link-id').text(), parentPageData, false);
			})
		};

		var sortDataset = function (originalDataset, columnTag, direction)
		{
			var sorter = getSorter(columnTag, direction);
			originalDataset.sort(sorter);
			return originalDataset;
		};

		var getSorter = function (columnTag, direction)
		{
			var directionIndex = direction == 'asc' ? 1 : -1;
			switch (columnTag)
			{
				case "library":
					return function (x, y)
					{
						var xValue = x.library.name;
						var yValue = y.library.name;
						return ((xValue < yValue) ? (-1 * directionIndex) : ((xValue > yValue) ? directionIndex : 0));
					};
				case "name" :
					return function (x, y)
					{
						var xValue = x.name.value;
						var yValue = y.name.value;
						return ((xValue < yValue) ? (-1 * directionIndex) : ((xValue > yValue) ? directionIndex : 0));
					};
				case "file_type":
					return function (x, y)
					{
						var xValue = x.file_type;
						var yValue = y.file_type;
						return ((xValue < yValue) ? (-1 * directionIndex) : ((xValue > yValue) ? directionIndex : 0));
					};
				case "date_modify":
					return function (x, y)
					{
						var xValue = x.date.value;
						var yValue = y.date.value;
						return ((xValue < yValue) ? (-1 * directionIndex) : ((xValue > yValue) ? directionIndex : 0));
					};
				default :
					return function (x, y)
					{
						var xValue = x.date.value;
						var yValue = y.date.value;
						return ((xValue < yValue) ? (-1 * directionIndex) : ((xValue > yValue) ? directionIndex : 0));
					};
			}
		};

		var initTable = function ()
		{
			pageContainer.find('.content-data').empty().html('<table data-role="table" data-mode="columntoggle" class="ui-responsive">' +
				'<thead>' +
				'<tr>' +
				'<th class="station" data-priority="2"><span>Station</span></th>' +
				'<th class="name"><span>Link</span></th>' +
				'<th class="type" data-priority="2"><span>Type</span></th>' +
				'<th class="date"><span>Date</span></th>' +
				'<th class="link-id">Id</th>' +
				'</tr>' +
				'</thead>' +
				'<tbody></tbody>' +
				'</table>');
			tableContainer = pageContainer.find('.content-data');
			initTableHeader();
		};

		var initTableHeader = function ()
		{
			var tableHeader = tableContainer.find('thead');
			var revertSortDirection = function (currentDirection)
			{
				if (currentDirection == 'asc')
					return 'desc';
				else
					return 'asc';
			};
			tableHeader.find('th.name span').off('click').on('click', function ()
			{
				$.mobile.loading('show', {
					textVisible: false,
					html: ""
				});
				var tag = 'name';
				var direction = revertSortDirection($(this).prop('class'));
				populateTable(dataset, tag, direction);
				updateTableHeader(tag, direction);
				tableContainer.find('table').table('refresh');
				$.mobile.loading('hide', {
					textVisible: false,
					html: ""
				});
			});
			tableHeader.find('th.date span').off('click').on('click', function ()
			{
				$.mobile.loading('show', {
					textVisible: false,
					html: ""
				});
				var tag = 'date_modify';
				var direction = revertSortDirection($(this).prop('class'));
				populateTable(dataset, tag, direction);
				updateTableHeader(tag, direction);
				tableContainer.find('table').table('refresh');
				$.mobile.loading('hide', {
					textVisible: false,
					html: ""
				});
			});
		};

		var updateTableHeader = function (columnTag, direction)
		{
			direction = direction != 'asc' ? 'desc' : 'asc';
			var tableHeader = tableContainer.find('thead');
			tableHeader.find('span').removeClass('asc').removeClass('desc');
			switch (columnTag)
			{
				case "library":
					tableHeader.find('th.station span').addClass(direction);
					break;
				case "name":
					tableHeader.find('th.name span').addClass(direction);
					break;
				case "file_type":
					tableHeader.find('th.type span').addClass(direction);
					break;
				case "date_modify":
					tableHeader.find('th.date span').addClass(direction);
					break;
				default :
					tableHeader.find('th.date span').addClass(direction);
					break;
			}
		};

		initTable();

		updateTableHeader(sortColumnTag, sortDirection);

		populateTable(dataset, sortColumnTag, sortDirection);

		tableContainer.trigger('create');
		contentHeaderContainer.find('.column-toggle-placeholder').empty().append(pageContainer.find('.ui-table-columntoggle-btn'));
	};
})(jQuery);
