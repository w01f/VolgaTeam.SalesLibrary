window.salesDepot = window.salesDepot || { };

(function ($)
{
	$.linkGrid = [];
	$.linkGrid.refreshData = null;
	$.linkGrid.showDelete = false;
	$.linkGrid.init = function ()
	{
		if ($('#links-grid-body').find('tr').length > 0)
		{
			updateSortingColumns();
			if ($.cookie("linksGridScrollPosition") != null)
				$('#links-grid-body-container').scrollTop(parseInt($.cookie("linksGridScrollPosition")));
		}
		else
			$.cookie("linksGridScrollPosition", 0, {
				expires: (60 * 60 * 24 * 7)
			});

		var searchGridHeader = $("#links-grid-header");
		searchGridHeader.find("td.library-column").off('click').on('click', function ()
		{
			sortByColumn('library');
			$.linkGrid.refreshData();
		});

		searchGridHeader.find("td.link-type-column").off('click').on('click', function ()
		{
			sortByColumn('link-type');
			$.linkGrid.refreshData();
		});

		searchGridHeader.find("td.link-name-column").off('click').on('click', function ()
		{
			sortByColumn('link-name');
			$.linkGrid.refreshData();
		});

		searchGridHeader.find("td.link-date-column").off('click').on('click', function ()
		{
			sortByColumn('link-date');
			$.linkGrid.refreshData();
		});

		searchGridHeader.find("td.link-rate-column").off('click').on('click', function ()
		{
			sortByColumn('link-rate');
			$.linkGrid.refreshData();
		});

		searchGridHeader.find("td.link-tag-column").off('click').on('click', function ()
		{
			sortByColumn('link-tag');
			$.linkGrid.refreshData();
		});

		var linkGridBody = $("#links-grid-body");
		var regularClickableLinks = linkGridBody.find("td.click-no-mobile");
		if (regularClickableLinks.length > 0)
		{
			regularClickableLinks.off('click').on('click', function ()
			{
				previewLink.call($(this));
			});
			regularClickableLinks.off('mousedown.context').on('mousedown.context', function (eventDown)
			{
				if (eventDown.which == 3)
				{
					$(this).off('mouseup.context').on('mouseup.context', function (eventUp)
					{
						if (eventUp.which == 3)
						{
							specialPreviewLink.call($(this));
							$(this).off('mouseup.context');
							eventUp.stopPropagation();
							eventUp.preventDefault();
						}
					});
				}
			});
			regularClickableLinks.draggable({
					delay: 100,
					revert: "invalid",
					helper: function (event)
					{
						var linkId = $(this).parent().find('.link-id-column').html();
						return  $('<i id="' + linkId + '" class="icon-file"></i>');
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
			mobileClickableLinks.hammer().on('doubletap', specialPreviewLink);
		}

		linkGridBody.find("td.details-button").off('click');
		linkGridBody.find("td.details-button.click-no-mobile").on('click', function ()
		{
			viewLinkDetails.call($(this));
		});
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

		if (!$.linkGrid.showDelete)
			linkGridBody.find('.delete-link').hide();
	};

	var updateSortingColumns = function ()
	{
		$('#links-grid-header').find('td span').removeClass('asc').removeClass('desc');

		var selector = null;
		if ($.cookie("sortColumn") != null)
		{
			switch ($.cookie("sortColumn"))
			{
				case "library":
					selector = '#links-grid-header td.library-column span';
					break;
				case "link-type":
					selector = '#links-grid-header td.link-type-column span';
					break;
				case "link-name":
					selector = '#links-grid-header td.link-name-column span';
					break;
				case "link-date":
					selector = '#links-grid-header td.link-date-column span';
					break;
				case "link-rate":
					selector = '#links-grid-header td.link-rate-column span';
					break;
				case "link-tag":
					selector = '#links-grid-header td.link-tag-column span';
					break;
			}
		}
		if (selector != null && $.cookie("sortDirection") != null)
			$(selector).addClass($.cookie("sortDirection"));
	};

	var sortByColumn = function (columnName)
	{
		var sortDirection = 'asc';
		if ($.cookie("sortColumn") != null && $.cookie("sortDirection") != null)
		{
			if ($.cookie("sortColumn") == columnName)
			{
				if ($.cookie("sortDirection") == 'asc')
					sortDirection = 'desc';
			}
		}

		$.cookie("sortColumn", columnName, {
			expires: (60 * 60 * 24 * 7)
		});
		$.cookie("sortDirection", sortDirection, {
			expires: (60 * 60 * 24 * 7)
		});

		var scrollPosition = $('#links-grid-body-container').scrollTop();
		$.cookie("linksGridScrollPosition", scrollPosition, {
			expires: (60 * 60 * 24 * 7)
		});
	};

	var previewLink = function ()
	{
		var linkId = $(this).parent().find('.link-id-column').html();
		$.requestViewDialog(linkId, false);
	};

	var specialPreviewLink = function ()
	{
		var linkId = $(this).parent().find('.link-id-column').html();
		$.requestSpecialDialog([linkId], undefined);
	};

	var viewFileCard = function ()
	{
		var linkId = $(this).parent().find('.link-id-column').html();
		$.openFileCard(linkId);
	};

	var viewAttachment = function ()
	{
		var linkId = $(this).parent().find('.link-id-column').html();
		$.requestViewDialog(linkId, true);
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
				url: "preview/getLinkDetails",
				data: {
					linkId: linkId
				},
				beforeSend: function ()
				{
					$.showOverlayLight();
				},
				complete: function ()
				{
					$.hideOverlayLight();
				},
				success: function (msg)
				{
					if (msg != '')
					{
						$(msg).insertAfter(currentRow);

						var searchGridBody = $("#links-grid-body");
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

})(jQuery);