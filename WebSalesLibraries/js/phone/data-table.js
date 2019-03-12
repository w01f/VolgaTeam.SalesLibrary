(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.SearchDataTable = function (searchResults, sortColumnTag, sortDirection, parentPageData) {
		var dataset = searchResults;
		var pageContainer = $('.search-results-page.ui-page-active');
		var contentHeaderContainer = pageContainer.find('.content-header');
		var tableContainer = undefined;
		var columnsState = undefined;

		var populateTable = function (dataArray, columnTag, direction) {
			var tableBody = tableContainer.find('tbody');
			tableBody.empty();

			dataArray = sortDataset(
				dataArray,
				columnTag,
				direction
			);

			$.each(dataArray, function (i, record) {
				var tr = $('<tr>');
				$('<td class="link-name">').html('<div class="name">' + record.name + '</div><div class="library">' + record.library.name + '</div>').appendTo(tr);
				$('<td class="type">').html(record.file_type).appendTo(tr);
				$('<td class="date">').html(record.date.display).appendTo(tr);
				$('<td class="views">').html(record.views.display).appendTo(tr);
				$('<td class="link-id">').html(record.id).appendTo(tr);
				tableBody.append(tr);
			});

			tableBody.find('tr').off('click').on('click', function (e) {
				$.SalesPortal.LinkManager.requestViewDialog($(this).find('.link-id').text(), parentPageData, false);
				ga('send', {
					hitType: 'pageview',
					title: $(this).find('.link-id').text(),
					location: window.BaseUrl,
					page: $(this).find('.link-id').text()
				});
				e.preventDefault();
				e.stopPropagation();
			})
		};

		var sortDataset = function (originalDataset, columnTag, direction) {
			var sorter = getSorter(columnTag, direction);
			originalDataset.sort(sorter);
			return originalDataset;
		};

		var getSorter = function (columnTag, direction) {
			var directionIndex = direction === 'asc' ? 1 : -1;
			switch (columnTag)
			{
				case "name" :
					return function (x, y) {
						var xValue = x.name;
						var yValue = y.name;
						return ((xValue < yValue) ? (-1 * directionIndex) : ((xValue > yValue) ? directionIndex : 0));
					};
				case "file_type":
					return function (x, y) {
						var xValue = x.file_type;
						var yValue = y.file_type;
						return ((xValue < yValue) ? (-1 * directionIndex) : ((xValue > yValue) ? directionIndex : 0));
					};
				case "date_modify":
					return function (x, y) {
						var xValue = x.date.value;
						var yValue = y.date.value;
						return ((xValue < yValue) ? (-1 * directionIndex) : ((xValue > yValue) ? directionIndex : 0));
					};
				case "views":
					return function (x, y) {
						var xValue = x.views.value;
						var yValue = y.views.value;
						return ((xValue < yValue) ? (-1 * directionIndex) : ((xValue > yValue) ? directionIndex : 0));
					};

				default :
					return function (x, y) {
						var xValue = x.views.value;
						var yValue = y.views.value;
						return ((xValue < yValue) ? 1 : ((xValue > yValue) ? -1 : 0));
					};
			}
		};

		var initTable = function () {
			pageContainer.find('.content-data').empty().html('<table data-role="table" data-mode="columntoggle" data-column-btn-text="options..." class="ui-responsive">' +
				'<thead>' +
				'<tr>' +
				'<th class="name"><span>Link</span></th>' +
				'<th class="type" data-priority="2"><span>Type</span></th>' +
				'<th class="date" data-priority="2"><span>Date</span></th>' +
				'<th class="views"><span>Views</span></th>' +
				'<th class="link-id">Id</th>' +
				'</tr>' +
				'</thead>' +
				'<tbody></tbody>' +
				'</table>');
			tableContainer = pageContainer.find('.content-data');
			initTableHeader();
		};

		var initTableHeader = function () {
			var tableHeader = tableContainer.find('thead');

			var revertSortDirection = function (currentDirection) {
				if (currentDirection === 'asc')
					return 'desc';
				else
					return 'asc';
			};

			var saveColumnsState = function () {
				columnsState = [];

				var typeColumn = tableHeader.find('th.type');
				if (typeColumn.hasClass('ui-table-cell-visible'))
					columnsState.push({
						columnTag: 'file_type',
						visible: true
					});
				else if (typeColumn.hasClass('ui-table-cell-hidden'))
					columnsState.push({
						columnTag: 'file_type',
						visible: false
					});

				var dateColumn = tableHeader.find('th.date');
				if (dateColumn.hasClass('ui-table-cell-visible'))
					columnsState.push({
						columnTag: 'date_modify',
						visible: true
					});
				else if (dateColumn.hasClass('ui-table-cell-hidden'))
					columnsState.push({
						columnTag: 'date_modify',
						visible: false
					});
			};

			var restoreColumnsState = function () {
				$.each(columnsState, function (i, record) {
					switch (record.columnTag)
					{
						case 'file_type':
							var typeColumn = tableContainer.find('.type');
							if (record.visible)
								typeColumn.addClass('ui-table-cell-visible');
							else
								typeColumn.addClass('ui-table-cell-hidden');
							break;
						case 'date_modify':
							var dateColumn = tableContainer.find('.date');
							if (record.visible)
								dateColumn.addClass('ui-table-cell-visible');
							else
								dateColumn.addClass('ui-table-cell-hidden');
							break;
					}
				});
			};

			tableHeader.find('th.name span').off('click').on('click', function () {
				$.mobile.loading('show', {
					textVisible: false,
					html: ""
				});
				saveColumnsState();
				var tag = 'name';
				var direction = revertSortDirection($(this).prop('class'));
				populateTable(dataset, tag, direction);
				updateTableHeader(tag, direction);
				tableContainer.find('table').table('refresh');
				restoreColumnsState();
				$.mobile.loading('hide', {
					textVisible: false,
					html: ""
				});
			});
			tableHeader.find('th.date span').off('click').on('click', function () {
				$.mobile.loading('show', {
					textVisible: false,
					html: ""
				});
				saveColumnsState();
				var tag = 'date_modify';
				var direction = revertSortDirection($(this).prop('class'));
				populateTable(dataset, tag, direction);
				updateTableHeader(tag, direction);
				tableContainer.find('table').table('refresh');
				restoreColumnsState();
				$.mobile.loading('hide', {
					textVisible: false,
					html: ""
				});
			});
			tableHeader.find('th.views span').off('click').on('click', function () {
				$.mobile.loading('show', {
					textVisible: false,
					html: ""
				});
				saveColumnsState();
				var tag = 'views';
				var direction = revertSortDirection($(this).prop('class'));
				populateTable(dataset, tag, direction);
				updateTableHeader(tag, direction);
				tableContainer.find('table').table('refresh');
				restoreColumnsState();
				$.mobile.loading('hide', {
					textVisible: false,
					html: ""
				});
			});
		};

		var updateTableHeader = function (columnTag, direction) {
			direction = direction !== 'asc' ? 'desc' : 'asc';
			var tableHeader = tableContainer.find('thead');
			tableHeader.find('span').removeClass('asc').removeClass('desc');
			switch (columnTag)
			{
				case "name":
					tableHeader.find('th.name span').addClass(direction);
					break;
				case "file_type":
					tableHeader.find('th.type span').addClass(direction);
					break;
				case "date_modify":
					tableHeader.find('th.date span').addClass(direction);
					break;
				case "views":
					tableHeader.find('th.views span').addClass(direction);
					break;
				default :
					tableHeader.find('th.views span').addClass('desc');
					break;
			}
		};

		initTable();

		updateTableHeader(sortColumnTag, sortDirection);

		populateTable(dataset, sortColumnTag, sortDirection);

		tableContainer.trigger('create');
		contentHeaderContainer.find('.column-toggle-placeholder').empty().append(pageContainer.find('.ui-table-columntoggle-btn'));

		ga('send', 'pageview');
	};
})(jQuery);
