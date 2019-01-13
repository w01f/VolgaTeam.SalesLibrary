(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.LandingPage = $.SalesPortal.LandingPage || {};
	$.SalesPortal.LandingPage.Calendar = function (parameters) {
		var calendarId = parameters.containerId;
		var parentShortcutId = parameters.parentShortcutId;
		var calendarContainer = undefined;
		var calendarSettings = undefined;

		this.init = function () {
			calendarContainer = $('#calendar-' + calendarId);
			calendarSettings = $.parseJSON(calendarContainer.find('.calendar-settings').text());
			initCalendar();
		};

		var initCalendar = function () {
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "calendar/getEvents",
				data: {
					calendarId: calendarId,
					shortcutId: parentShortcutId
				},
				beforeSend: function () {
					$.SalesPortal.Overlay.show();
				},
				success: function (events) {
					$.SalesPortal.Overlay.hide();

					calendarContainer.fullCalendar({
						header: {
							left: 'prev,next today',
							center: 'title',
							right: 'month,agendaWeek,agendaDay,listWeek'
						},
						timezone: 'local',
						firstDay: 1,
						defaultView: calendarSettings.defaultView,
						themeSystem: 'bootstrap3',
						defaultDate: moment(calendarSettings.defaultDate),
						navLinks: true,
						displayEventTime: true,
						editable: calendarSettings.allowEdit,
						eventLimit: true, // allow "more" link when too many events
						dayClick: addEvent,
						eventClick: editEvent,
						eventRender: function (event, element) {
							if (calendarSettings.allowEdit)
							{
								element.bind('contextmenu', function (jsEvent) {
									showEventContextMenu(event, jsEvent);
									return false;
								});
							}
						},
						dayRender: function (date, element) {
							if (calendarSettings.allowEdit)
							{
								element.bind('contextmenu', function (jsEvent) {
									showDayContextMenu(date, jsEvent);
									return false;
								});
							}
						},
						events: events
					});
				},
				error: function () {
					$.SalesPortal.Overlay.hide();
				},
				async: true,
				dataType: 'json'
			});
		};

		var addEvent = function (date, jsEvent, view) {
			if (!calendarSettings.allowEdit)
				return;

			$.ajax({
				type: "POST",
				url: window.BaseUrl + "calendar/getEventEditDialog",
				data: {},
				beforeSend: function () {
					$.SalesPortal.Overlay.show();
				},
				success: function (content) {
					$.SalesPortal.Overlay.hide();
					$.fancybox({
						content: content,
						title: "Add Event",
						width: 500,
						autoSize: false,
						autoHeight: true,
						openEffect: 'none',
						closeEffect: 'none',
						afterShow: function () {
							var innerContent = $('.fancybox-inner');

							var dateDisplayFormat = 'MM/DD/YYYY h:mm a';
							var separator = ' - ';
							var dateRangeSelector = innerContent.find('#calendar-modal-date-range-container');
							var dateRangeInput = dateRangeSelector.find('input');
							dateRangeSelector.daterangepicker(
								{
									format: dateDisplayFormat,
									separator: separator,
									singleDatePicker: false,
									timePicker: true,
									startDate: date,
									endDate: date
								}
							);
							dateRangeInput.val(moment(date).format(dateDisplayFormat) + separator + moment(date).format(dateDisplayFormat));

							innerContent.find('.accept-button').off('click').on('click', function () {
								var dateRangeParts = dateRangeInput.val().split(separator);
								var dateStart = new Date(dateRangeParts[0]);
								var dateEnd = new Date(dateRangeParts[1]);
								$.ajax({
									type: "POST",
									url: window.BaseUrl + "calendar/addEvent",
									data: {
										calendarId: calendarId,
										shortcutId: parentShortcutId,
										eventData: {
											start: dateStart,
											end: dateEnd,
											title: innerContent.find('#calendar-modal-details').val()
										}
									},
									beforeSend: function () {
										$.SalesPortal.Overlay.show();
									},
									success: function (eventData) {
										$.SalesPortal.Overlay.hide();
										$.fancybox.close();
										calendarContainer.fullCalendar('renderEvent', eventData);
									},
									error: function () {
										$.SalesPortal.Overlay.hide();
									},
									async: true,
									dataType: 'json'
								});

							});
							innerContent.find('.cancel-button').off('click').on('click', function () {
								$.fancybox.close();
							});
						}
					});
				},
				error: function () {
					$.SalesPortal.Overlay.hide();
				},
				async: true,
				dataType: 'html'
			});
		};

		var editEvent = function (calEvent, jsEvent, view) {
			if (!calendarSettings.allowEdit)
				return;

			$.ajax({
				type: "POST",
				url: window.BaseUrl + "calendar/getEventEditDialog",
				data: {},
				beforeSend: function () {
					$.SalesPortal.Overlay.show();
				},
				success: function (content) {
					$.SalesPortal.Overlay.hide();
					$.fancybox({
						content: content,
						title: "Edit Event",
						width: 500,
						autoSize: false,
						autoHeight: true,
						openEffect: 'none',
						closeEffect: 'none',
						afterShow: function () {
							var innerContent = $('.fancybox-inner');

							var dateDisplayFormat = 'MM/DD/YYYY h:mm a';
							var separator = ' - ';
							var dateRangeSelector = innerContent.find('#calendar-modal-date-range-container');
							var dateRangeInput = dateRangeSelector.find('input');
							dateRangeSelector.daterangepicker(
								{
									format: dateDisplayFormat,
									separator: separator,
									singleDatePicker: false,
									timePicker: true,
									startDate: calEvent.start,
									endDate: calEvent.end != null ? calEvent.end : calEvent.start
								}
							);
							dateRangeInput.val(moment(calEvent.start).format(dateDisplayFormat) + separator + moment(calEvent.end != null ? calEvent.end : calEvent.start).format(dateDisplayFormat));

							var detailsEditor = innerContent.find('#calendar-modal-details');
							detailsEditor.val(calEvent.title);

							innerContent.find('.accept-button').off('click').on('click', function () {
								var dateRangeParts = dateRangeInput.val().split(separator);
								var dateStart = new Date(dateRangeParts[0]);
								var dateEnd = new Date(dateRangeParts[1]);

								calEvent.start = dateStart;
								calEvent.end = dateEnd;
								calEvent.title = detailsEditor.val();

								$.ajax({
									type: "POST",
									url: window.BaseUrl + "calendar/updateEvent",
									data: {
										eventId: calEvent.id,
										eventData: {
											start: calEvent.start,
											end: calEvent.end,
											title: calEvent.title
										}
									},
									beforeSend: function () {
										$.SalesPortal.Overlay.show();
									},
									success: function () {
										$.SalesPortal.Overlay.hide();
										$.fancybox.close();
										calendarContainer.fullCalendar('updateEvent', calEvent);
									},
									error: function () {
										$.SalesPortal.Overlay.hide();
									},
									async: true,
									dataType: 'json'
								});

								return false;
							});
							innerContent.find('.cancel-button').off('click').on('click', function () {
								$.fancybox.close();
							});
						}
					});
				},
				error: function () {
					$.SalesPortal.Overlay.hide();
				},
				async: true,
				dataType: 'html'
			});
		};

		var deleteEvent = function (event) {
			if (!calendarSettings.allowEdit)
				return;

			var modalDialog = new $.SalesPortal.ModalDialog({
				title: 'Delete meeting',
				description: 'Are you SURE you want to delete selected meeting?',
				buttons: [
					{
						tag: 'yes',
						title: 'Yes',
						clickHandler: function () {
							modalDialog.close();
							$.ajax({
								type: "POST",
								url: window.BaseUrl + "calendar/deleteEvent",
								data: {
									eventId: event.id
								},
								beforeSend: function () {
									$.SalesPortal.Overlay.show();
								},
								complete: function () {
									$.SalesPortal.Overlay.hide();
								},
								success: function () {
									$.SalesPortal.Overlay.hide();
									$.fancybox.close();
									calendarContainer.fullCalendar('removeEvents', event.id);
								},
								async: true,
								dataType: 'json'
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
		};

		var showEventContextMenu = function (event, jsEvent) {
			$.SalesPortal.LinkManager.cleanupContextMenu();

			var menu = $('<ul class="dropdown-menu context-menu-content" role="menu">' +
				'<li><a tabindex="-1" href="#" class="menu-item" data-action-tag="edit">Edit meeting...</a></li>' +
				'<li><a tabindex="-1" href="#" class="menu-item" data-action-tag="delete">Delete meeting...</a></li>' +
				'</ul>');
			$('body').append(menu);

			menu
				.show()
				.css({
					position: "absolute",
					left: $.SalesPortal.LinkManager.getMenuPosition(menu, jsEvent.clientX, 'width', 'scrollLeft'),
					top: $.SalesPortal.LinkManager.getMenuPosition(menu, jsEvent.clientY, 'height', 'scrollTop')
				})
				.off('click')
				.on('click', 'a.menu-item', function () {
					menu.hide();
					var tag = $(this).data('action-tag');
					switch (tag)
					{
						case 'edit':
							editEvent(event, jsEvent);
							break;
						case 'delete':
							deleteEvent(event);
							break;
					}
				});
		};

		var showDayContextMenu = function (date, jsEvent) {
			$.SalesPortal.LinkManager.cleanupContextMenu();

			var menu = $('<ul class="dropdown-menu context-menu-content" role="menu">' +
				'<li><a tabindex="-1" href="#" class="menu-item" data-action-tag="add">Add meeting...</a></li>' +
				'</ul>');
			$('body').append(menu);

			menu
				.show()
				.css({
					position: "absolute",
					left: $.SalesPortal.LinkManager.getMenuPosition(menu, jsEvent.clientX, 'width', 'scrollLeft'),
					top: $.SalesPortal.LinkManager.getMenuPosition(menu, jsEvent.clientY, 'height', 'scrollTop')
				})
				.off('click')
				.on('click', 'a.menu-item', function () {
					menu.hide();
					var tag = $(this).data('action-tag');
					switch (tag)
					{
						case 'add':
							addEvent(date, jsEvent);
							break;
					}
				});
		};
	};
})(jQuery);