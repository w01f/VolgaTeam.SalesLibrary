(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.LinkGrid = function ()
	{
		var that = this;

		var scrollPosition = 0;
		var gridContent = undefined;
		var showDelete = undefined;

		that.sortColumn = undefined;
		that.sortDirection = undefined;
		that.datasetKey = undefined;
		that.hasData = false;

		that.refreshData = undefined;

		var sortByColumn = function (columnName)
		{
			var sortDirection = 'asc';
			if (typeof that.sortColumn !== 'undefined' && typeof that.sortDirection !== 'undefined')
			{
				if (that.sortColumn == columnName)
				{
					if (that.sortDirection == 'asc')
						sortDirection = 'desc';
				}
			}
			that.sortColumn = columnName;
			that.sortDirection = sortDirection;
			scrollPosition = gridContent.find('.links-grid-body-container').scrollTop();
		};

		var updateSortingColumns = function ()
		{
			gridContent.find('.links-grid-header').find('td span').removeClass('asc').removeClass('desc');

			var selector = null;
			if (typeof that.sortColumn !== 'undefined')
			{
				switch (that.sortColumn)
				{
					case "library":
						selector = '.links-grid-header td.library-column span';
						break;
					case "file_type":
						selector = '.links-grid-header td.link-type-column span';
						break;
					case "name":
						selector = '.links-grid-header td.link-name-column span';
						break;
					case "date_modify":
						selector = '.links-grid-header td.link-date-column span';
						break;
					case "tag":
						selector = '.links-grid-header td.link-tag-column span';
						break;
					case "rate":
						selector = '.links-grid-header td.link-rate-column span';
						break;
				}
			}
			if (selector != null && typeof that.sortDirection !== 'undefined')
				gridContent.find(selector).addClass(that.sortDirection);
		};

		var previewLink = function ()
		{
			var linkId = $(this).parent().find('.link-id-column').html();
			$.SalesPortal.LinkManager.requestViewDialog(linkId, false);
		};

		var viewFileCard = function ()
		{
			var linkId = $(this).parent().find('.link-id-column').html();
			$.SalesPortal.LinkManager.openFileCard(linkId);
		};

		var viewAttachment = function ()
		{
			var linkId = $(this).parent().find('.link-id-column').html();
			$.SalesPortal.LinkManager.requestViewDialog(linkId, true);
		};

		var viewLinkDetails = function ()
		{
			if ($(this).hasClass('collapsed'))
			{
				var currentCell = $(this);
				var currentRow = $(this).parent();
				var linkId = currentRow.find('.link-id-column').html();
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "preview/getLinkDetails",
					data: {
						linkId: linkId
					},
					beforeSend: function ()
					{
						$.SalesPortal.Overlay.show(false);
					},
					complete: function ()
					{
						$.SalesPortal.Overlay.hide();
					},
					success: function (msg)
					{
						if (msg != '')
						{
							$(msg).after(currentRow);

							var searchGridBody = gridContent.find(".links-grid-body");
							searchGridBody.find("tr.link-details-container tr.file-card td").off('click');
							searchGridBody.find("tr.link-details-container tr.file-card>td.click-no-mobile").on('click', function (e)
							{
								e.stopPropagation();
								viewFileCard.call($(this));
							});

							searchGridBody.find("tr.link-details-container tr.attachment td").off('click');
							searchGridBody.find("tr.link-details-container tr.attachment>td.click-no-mobile").on('click', function (e)
							{
								e.stopPropagation();
								viewAttachment.call($(this));
							});

							currentCell.removeClass('collapsed');
							currentCell.addClass('expanded');
						}
					},
					async: true,
					dataType: 'html'
				});
			}
			else if ($(this).hasClass('expanded'))
			{
				$(this).parent().next('.link-details-container').remove();
				$(this).removeClass('expanded');
				$(this).addClass('collapsed');
			}
		};

		this.init = function (options)
		{
			gridContent = options.content;
			that.refreshData = options.refreshCallback;
			that.sortColumn = options.sortColumn !== undefined && options.sortColumn !== null ? options.sortColumn : that.sortColumn;
			that.sortDirection = options.sortDirection !== undefined && options.sortDirection !== null ? options.sortDirection : that.sortDirection;
			showDelete = options.showDelete;

			var datasetKeyNodes = gridContent.find('.dataset-key');
			that.datasetKey = datasetKeyNodes.length > 0 ? datasetKeyNodes.html() : null;

			if (gridContent.find('.links-grid-body').find('tr').length > 0)
			{
				that.hasData = true;
				updateSortingColumns();
				gridContent.find('.links-grid-body-container').scrollTop(scrollPosition);
			}
			else
				that.hasData = false;
			var searchGridHeader = gridContent.find(".links-grid-header");
			searchGridHeader.find("td.library-column").off('click').on('click', function ()
			{
				sortByColumn('library');
				that.refreshData();
			});

			searchGridHeader.find("td.link-type-column").off('click').on('click', function ()
			{
				sortByColumn('file_type');
				that.refreshData();
			});

			searchGridHeader.find("td.link-name-column").off('click').on('click', function ()
			{
				sortByColumn('name');
				that.refreshData();
			});

			searchGridHeader.find("td.link-date-column").off('click').on('click', function ()
			{
				sortByColumn('date_modify');
				that.refreshData();
			});

			searchGridHeader.find("td.link-tag-column").off('click').on('click', function ()
			{
				sortByColumn('tag');
				that.refreshData();
			});

			searchGridHeader.find("td.link-rate-column").off('click').on('click', function ()
			{
				sortByColumn('rate');
				that.refreshData();
			});

			var linkGridBody = gridContent.find(".links-grid-body");
			var regularClickableLinks = linkGridBody.find("td.click-no-mobile");
			if (regularClickableLinks.length > 0)
			{
				regularClickableLinks.off('click').on('click', function ()
				{
					previewLink.call($(this));
				});
				regularClickableLinks.draggable({
						delay: 100,
						revert: "invalid",
						helper: function ()
						{
							var linkId = $(this).parent().find('.link-id-column').html();
							return  $('<span id="' + linkId + '" class="glyphicon glyphicon-file"></span>');
						},
						appendTo: "body",
						cursorAt: { left: -10, top: 0 }
					}
				);
			}

			var mobileClickableLinks = linkGridBody.find("td.click-mobile");
			if (mobileClickableLinks.length > 0)
			{
				mobileClickableLinks.off('click').on('click', previewLink);
			}

			linkGridBody.find("td.details-button").off('click');
			linkGridBody.find("td.details-button.click-no-mobile").on('click', function ()
			{
				viewLinkDetails.call($(this));
			});
			var isScrolling = false;
			linkGridBody.find("td.details-button").off('touchstart').off('touchmove').off('touchend');
			linkGridBody.find("td.details-button.click-mobile").on('touchstart',function ()
			{
				isScrolling = false;
			}).on('touchmove',function ()
				{
					isScrolling = true;
				}).on('touchend', function ()
				{
					if (!isScrolling)
						viewLinkDetails.call($(this));
				});

			if (!showDelete)
				linkGridBody.find('.delete-link').hide();
		}
	};
})(jQuery);