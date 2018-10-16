(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsStarSteals = function ()
	{
		var shortcutData = undefined;
		var servicePanel = undefined;

		var itemList = new ItemList();

		this.init = function (data)
		{
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

			servicePanel = $.SalesPortal.Content.getContentObject().find('.star-steals-main-page .service-panel');

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

			$(window).off('resize.starsteals').on('resize.starsteals', updateContentSize);
			updateContentSize();
		};

		var saveItem = function ()
		{
			if(itemList.selectedItem !== undefined)
			{
				var itemContent = $.SalesPortal.Content.getContentObject().find('.star-steals-main-page .item-content');

				var digitalPlatforms = [];
				$.each(itemContent.find('.star-steals-item-details-platform:checked'), function () {
					var digitalPlatform = $(this).val();
					digitalPlatforms.push(digitalPlatform);
				});

				var teamMates = [];
				$.each(itemContent.find('.star-steals-item-team-item'), function () {
					var teamMateValue = $(this).val();
					teamMates.push(teamMateValue);
				});

				$.ajax({
					type: "POST",
					url: window.BaseUrl + "starSteals/saveItem",
					data: {
						selectedItemId: itemList.selectedItem.itemId,
						title: itemList.selectedItem.itemTitle,
						content: {
							revenue: $('#star-steals-item-revenue').val(),
							client: $('#star-steals-item-client').val(),
							seller: $('#star-steals-item-seller').val(),
							closedDate: $('#star-steals-item-closed-date').val(),
							market: $("#star-steals-item-market").find("option:selected").text(),
							station: $("#star-steals-item-station").find("option:selected").text(),
							saleType: itemContent.find('.star-steals-item-sale-type:checked').val(),
							revenueType: itemContent.find('.star-steals-item-revenue-type:checked').val(),
							teamMates: teamMates,
							details: {
								advertiserCategory: $("#star-steals-item-details-category").find("option:selected").text(),
								digitalRevenue: $('#star-steals-item-details-revenue-digital').val(),
								mediaRevenue: $('#star-steals-item-details-revenue-media').val(),
								digitalPlatforms: digitalPlatforms
							},
							success: {
								why: $('#star-steals-item-success-why').val(),
								what: $('#star-steals-item-success-what').val()
							}
						}
					},
					beforeSend: function () {
						$.SalesPortal.Overlay.show();
					},
					complete: function () {
						$.SalesPortal.Overlay.hide();
					},
					success: function () {
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
					},
					error: function () {
					},
					async: false,
					dataType: 'html'
				});
			}
		};

		var initActionButtons = function ()
		{
			var shortcutActionsContainer = $('#shortcut-action-container');

			if ($.cookie("showServicePanel") === "true")
			{
				shortcutActionsContainer.find('.starssteals-panel-show').hide();
			}
			else
				shortcutActionsContainer.find('.starssteals-panel-hide').hide();
			shortcutActionsContainer.find('.starssteals-panel-show').off('click.action').on('click.action', function ()
			{
				servicePanel.show();
				shortcutActionsContainer.find('.starssteals-panel-hide').show();
				$(this).hide();

				$.cookie("showServicePanel", true, {
					expires: (60 * 60 * 24 * 7)
				});

				updateContentSize();
			});
			shortcutActionsContainer.find('.starssteals-panel-hide').off('click.action').on('click.action', function ()
			{
				servicePanel.hide();
				shortcutActionsContainer.find('.starssteals-panel-show').show();
				$(this).hide();

				$.cookie("showServicePanel", false, {
					expires: (60 * 60 * 24 * 7)
				});

				updateContentSize();
			});

			shortcutActionsContainer.find('.starssteals-item-add').off('click.action').on('click.action', function ()
			{
				itemList.addItem();
			});

			shortcutActionsContainer.find('.starssteals-item-delete').off('click.action').on('click.action', function ()
			{
				itemList.deleteItem();
			});

			shortcutActionsContainer.find('.starssteals-item-save').off('click.action').on('click.action', function ()
			{
				saveItem();
			});
		};

		var updateContentSize = function ()
		{
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

		var trackActivity = function ()
		{
			var activityData = $.parseJSON($('<div>' + shortcutData.options.serviceData + '</div>').find('.activity-data').text());
			$.SalesPortal.ShortcutsManager.trackActivity(
				activityData,
				'StarsSteals Activity',
				'StarsSteals Activity');
		};
	};

	var ItemList = function () {
		var that = this;
		this.selectedItem = undefined;
		this.shortcutData = undefined;

		this.load = function (itemToSelectId)
		{
			var itemList = $.SalesPortal.Content.getContentObject().find('.star-steals-main-page .service-panel .item-list-container');
			if (itemToSelectId === undefined && that.selectedItem !== undefined)
				itemToSelectId = that.selectedItem.pageId;
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "starSteals/getItemList",
				data: {
					selectedItemId: itemToSelectId
				},
				beforeSend: function ()
				{
					itemList.html('');
					if (that.selectedItem !== undefined)
						that.selectedItem.clear();
					that.selectedItem = undefined;
					$.SalesPortal.Overlay.show();
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					itemList.html(msg);
					init();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		this.addItem = function (parameters)
		{
			if (parameters === undefined)
				parameters = {};
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "starSteals/addItemDialog",
				data: {
					isCloning: parameters.templateItemId !== undefined
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show();
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					var addItemContent = $(msg);

					var formLogger = new $.SalesPortal.FormLogger();
					formLogger.init({
						logObject: {name: that.shortcutData.options.headerTitle},
						formContent: addItemContent
					});

					addItemContent.find('.btn.accept-button').on('click.starsteals', function ()
					{
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "starSteals/addItem",
							data: {
								title: $('#add-item-name').val(),
								templateItemId: parameters.templateItemId
							},
							beforeSend: function ()
							{
								$.SalesPortal.Overlay.show();
							},
							complete: function ()
							{
								$.SalesPortal.Overlay.hide();
							},
							success: function (msg)
							{
								that.load(msg);
							},
							error: function ()
							{
							},
							async: true,
							dataType: 'html'
						});
						$.fancybox.close();
					});
					addItemContent.find('.btn.cancel-button').on('click.starsteals', function ()
					{
						$.fancybox.close();
					});

					$.fancybox({
						content: addItemContent,
						title: parameters.templatePageId === undefined ? "Add STAR or STEAL!" : "Clone STAR or STEAL!",
						scrolling: 'no',
						autoSize: true,
						openEffect: 'none',
						closeEffect: 'none'
					});
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		this.deleteItem = function (itemId)
		{
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
							clickHandler: function ()
							{
								modalDialog.close();
								$.ajax({
									type: "POST",
									url: window.BaseUrl + "starSteals/deleteItem",
									data: {
										selectedItemId: selectedItemId
									},
									beforeSend: function ()
									{
										$.SalesPortal.Overlay.show();
									},
									complete: function ()
									{
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
							clickHandler: function ()
							{
								modalDialog.close();
							}
						}
					]
				});
				modalDialog.show();
			}
		};

		this.updateContentSize = function ()
		{
			var itemListContainer = $.SalesPortal.Content.getContentObject().find('.star-steals-main-page .service-panel .item-list-container');
			var itemListButtons = $.SalesPortal.Content.getContentObject().find('.star-steals-main-page .service-panel .item-list-buttons');

			var height = $.SalesPortal.Content.getContentObject().height() - itemListButtons.outerHeight(true) - 5;
			itemListContainer.css({
				'height': height + 'px'
			});
			if (that.selectedItem !== undefined)
				that.selectedItem.updateContentSize();
		};

		var init = function ()
		{
			var itemList = $.SalesPortal.Content.getContentObject().find('.star-steals-main-page .service-panel .item-list-container');
			var itemListButtons = $.SalesPortal.Content.getContentObject().find('.star-steals-main-page .service-panel .item-list-buttons');

			var formLogger = new $.SalesPortal.FormLogger();
			formLogger.init({
				logObject: {name: that.shortcutData.options.headerTitle},
				formContent: itemList
			});

			if (itemList.find('tr.selected').length === 0)
				itemList.find('tr').first().addClass('selected');

			if (itemList.find('tr').length > 0)
				openItem(
					itemList.find('tr.selected').find('.item-id-column').html(),
					itemList.find('tr.selected').find('.item-name-column').html()
				);
			else
			{
				$.SalesPortal.Content.getContentObject().find('.star-steals-main-page .item-content').html('');
			}
			itemList.find('tr').off('click.starsteals').on('click.starsteals', function (event)
			{
				if (!$(this).hasClass('selected'))
				{
					itemList.find('tr').removeClass('selected');
					$(this).addClass('selected');
					var selectedItemId = $(this).find('.item-id-column').html();
					var selectedItemTitle = $(this).find('.item-name-column').html();
					openItem(selectedItemId, selectedItemTitle);
				}
				event.stopPropagation();
			});
			itemList.find('.item-delete').off('click.starsteals').on('click.starsteals', function (event)
			{
				event.stopPropagation();
				that.deleteItem($(this).parent().find('.item-id-column').html());
			});
			itemList.find('.item-clone').off('click.starsteals').on('click.starsteals', function (event)
			{
				event.stopPropagation();
				that.addItem({
						templateItemId: $(this).parent().find('.item-id-column').text()
					}
				);
			});
			itemList.find('.item-up').off('click.starsteals').on('click.starsteals', function (event)
			{
				event.stopPropagation();
				upItem($(this).parent());
			});
			itemList.find('.item-down').off('click.starsteals').on('click.starsteals', function (event)
			{
				event.stopPropagation();
				downItem($(this).parent());
			});
			itemListButtons.find('.item-list-clear').off('click.starsteals').on('click.starsteals', function ()
			{
				deleteItems();
			});
			itemListButtons.find('.item-list-add').off('click.starsteals').on('click.starsteals', function ()
			{
				that.addItem();
			});
		};

		var openItem = function (selectedItemId, selectedItemTitle)
		{
			that.selectedItem = new ItemContent({
				selectedItemId: selectedItemId,
				selectedItemTitle: selectedItemTitle,
				shortcutData: that.shortcutData
			});
		};

		var deleteItems = function ()
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "starSteals/deleteItemsDialog",
				data: {},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show();
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					var deleteItemsContent = $(msg);

					var formLogger = new $.SalesPortal.FormLogger();
					formLogger.init({
						logObject: {name: that.shortcutData.options.headerTitle},
						formContent: deleteItemsContent
					});

					deleteItemsContent.find('#delete-items-select-all').on('click.starsteals', function ()
					{
						deleteItemsContent.find('.delete-items-item').prop('checked', true);
					});
					deleteItemsContent.find('#delete-items-clear-all').on('click.starsteals', function ()
					{
						deleteItemsContent.find('.delete-items-item').prop('checked', false);
					});
					deleteItemsContent.find('.btn.accept-button').on('click.starsteals', function ()
					{
						var selectedItemsIds = [];
						deleteItemsContent.find('.delete-items-item:checked').each(function ()
						{
							selectedItemsIds.push($(this).val());
						});

						$.ajax({
							type: "POST",
							url: window.BaseUrl + "starSteals/deleteItems",
							data: {
								pageIds: $.toJSON(selectedItemsIds)
							},
							beforeSend: function ()
							{
								$.SalesPortal.Overlay.show();
							},
							complete: function ()
							{
								$.SalesPortal.Overlay.hide();
								that.load();
							},
							error: function ()
							{
							},
							async: true,
							dataType: 'html'
						});
						$.fancybox.close();
					});
					deleteItemsContent.find('.btn.cancel-button').on('click.qbuilder', function ()
					{
						$.fancybox.close();
					});
					$.fancybox({
						content: deleteItemsContent,
						title: "Clean up existed items",
						width: 430,
						scrolling: 'no',
						autoSize: false,
						autoHeight: true,
						openEffect: 'none',
						closeEffect: 'none'
					});
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		var upItem = function (row)
		{
			var itemId = row.find('.item-id-column').html();
			var itemListContainer = $.SalesPortal.Content.getContentObject().find('.star-steals-main-page .service-panel .item-list-container');
			var rowIndex = itemListContainer.find('tr.page-list-item').index(row);
			if (rowIndex > 0)
			{
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "starSteals/setPageOrder",
					data: {
						itemId: itemId,
						order: (rowIndex - 1)
					},
					beforeSend: function ()
					{
						$.SalesPortal.Overlay.show();
					},
					complete: function ()
					{
						$.SalesPortal.Overlay.hide();
						that.load();
					},
					async: true,
					dataType: 'html'
				});
			}
		};

		var downItem = function (row)
		{
			var nextRow = row.next();
			if (nextRow.length > 0)
			{
				var itemId = nextRow.find('.item-id-column').html();
				var itemListContainer = $.SalesPortal.Content.getContentObject().find('.star-steals-main-page .service-panel .item-list-container');
				var rowIndex = itemListContainer.find('tr.page-list-item').index(nextRow);
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "starSteals/setPageOrder",
					data: {
						itemId: itemId,
						order: rowIndex > 0 ? (rowIndex - 1) : 0
					},
					beforeSend: function ()
					{
						$.SalesPortal.Overlay.show();
					},
					complete: function ()
					{
						$.SalesPortal.Overlay.hide();
						that.load();
					},
					async: true,
					dataType: 'html'
				});
			}
		};
	};

	var ItemContent = function (parameters)
	{
		var that = this;
		var dateFormat = 'MM/DD/YYYY';

		this.itemId = parameters.selectedItemId;
		this.itemTitle = parameters.selectedItemTitle;
		var shortcutData = parameters.shortcutData;

		this.clear = function ()
		{
			$.SalesPortal.Content.getContentObject().find('.star-steals-main-page .item-content').html('');
		};

		this.updateContentSize = function ()
		{
			var itemContent = $.SalesPortal.Content.getContentObject().find('.star-steals-main-page .item-content > div');
			var height = $.SalesPortal.Content.getContentObject().height();
			itemContent.css({
				'height': height + 'px'
			});
		};

		var load = function ()
		{
			var itemContent = $.SalesPortal.Content.getContentObject().find('.star-steals-main-page .item-content');
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "starSteals/getItemContent",
				data: {
					selectedItemId: that.itemId
				},
				beforeSend: function ()
				{
					that.clear();
					$.SalesPortal.Overlay.show();
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					itemContent.html(msg);
					afterLoad();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		var afterLoad = function ()
		{
			var itemContent = $.SalesPortal.Content.getContentObject().find('.star-steals-main-page .item-content');

			var formLogger = new $.SalesPortal.FormLogger();
			formLogger.init({
				logObject: {name: shortcutData.options.headerTitle},
				formContent: itemContent
			});

			$('#star-steals-item-revenue').off('keypress.starsteals').on('keypress.starsteals',validateNumbers);
			$('#star-steals-item-details-revenue-digital').off('keypress.starsteals').on('keypress.starsteals',validateNumbers);
			$('#star-steals-item-details-revenue-media').off('keypress.starsteals').on('keypress.starsteals',validateNumbers);

			var closedDatePickerContainer = itemContent.find('.star-steals-item-closed-date-container');
			var closedDatePicker = $('#star-steals-item-closed-date');

			var dateValue = closedDatePicker.val();
			if (dateValue == '')
				dateValue = moment().endOf('day');

			closedDatePickerContainer.daterangepicker(
				{
					format: dateFormat,
					singleDatePicker: true,
					startDate: dateValue,
					endDate: dateValue
				});

			that.updateContentSize();
		};

		var validateNumbers =  function validate(evt) {
			var theEvent = evt || window.event;

			if ($.inArray(theEvent.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
				// Allow: Ctrl+A
				(theEvent.keyCode == 65 && (theEvent.ctrlKey === true || theEvent.metaKey === true)) ||
				// Allow: Ctrl+C
				(theEvent.keyCode == 67 && (theEvent.ctrlKey === true || theEvent.metaKey === true)) ||
				// Allow: Ctrl+X
				(theEvent.keyCode == 88 && (theEvent.ctrlKey === true || theEvent.metaKey === true)) ||
				// Allow: home, end, left, right
				(theEvent.keyCode >= 35 && theEvent.keyCode <= 39) ||
				//Allow numbers and numbers + shift key
				((theEvent.shiftKey && (theEvent.keyCode >= 48 && theEvent.keyCode <= 57)) || (theEvent.keyCode >= 96 && theEvent.keyCode <= 105)))
			{
				// let it happen, don't do anything
				return;
			}

			// Handle paste
			if (theEvent.type === 'paste') {
				key = event.clipboardData.getData('text/plain');
			} else {
				// Handle key press
				var key = theEvent.keyCode || theEvent.which;
				key = String.fromCharCode(key);
			}
			var regex = /[0-9]|\./;
			if( !regex.test(key) ) {
				theEvent.returnValue = false;
				if(theEvent.preventDefault) theEvent.preventDefault();
			}
		};

		load();
	};
})(jQuery);
