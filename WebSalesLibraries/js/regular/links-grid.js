(function ($)
{
	$.linkGrid = [];
	$.linkGrid.refreshData = null;
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
				expires:(60 * 60 * 24 * 7)
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

		var searchGridBody = $("#links-grid-body");
		var clickableLinks = searchGridBody.find("td.click-no-mobile");
		clickableLinks.off('click').on('click', function ()
		{
			previewLink.call($(this));
		});
		clickableLinks.draggable({
				delay:100,
				revert:"invalid",
				helper:function (event)
				{
					var linkId = $(this).parent().find('.link-id-column').html();
					return  $('<i id="' + linkId + '" class="icon-file"></i>');
				},
				appendTo: "body",
				cursorAt:{ left:-10, top:0 }
			}
		);

		searchGridBody.find("td.click-mobile").off('touchstart').off('touchmove').off('touchend').on('touchstart',function ()
		{
			isScrolling = false;
		}).on('touchmove',function ()
			{
				isScrolling = true;
			}).on('touchend', function (e)
			{
				if (!isScrolling)
					previewLink.call($(this));
				e.stopPropagation();
				e.preventDefault();
				return false;
			});

		searchGridBody.find("td.details-button").off('click');
		searchGridBody.find("td.details-button.click-no-mobile").on('click', function ()
		{
			viewLinkDetails.call($(this));
		});
		searchGridBody.find("td.details-button").off('touchstart').off('touchmove').off('touchend');
		searchGridBody.find("td.details-button.click-mobile").on('touchstart',function ()
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
			expires:(60 * 60 * 24 * 7)
		});
		$.cookie("sortDirection", sortDirection, {
			expires:(60 * 60 * 24 * 7)
		});

		var scrollPosition = $('#links-grid-body-container').scrollTop();
		$.cookie("linksGridScrollPosition", scrollPosition, {
			expires:(60 * 60 * 24 * 7)
		});
	};

	var previewLink = function ()
	{
		var linkId = $(this).parent().find('.link-id-column').html();
		$.openViewDialogFromGrid(linkId);
	};

	var viewFileCard = function ()
	{
		var fileCardContainer = $(this).parent().find('td.hidden-content');
		$.openFileCard.call(fileCardContainer);
	};

	var viewAttachment = function ()
	{
		var viewDialogContainer = $(this).parent().find('td.hidden-content');
		$.openViewDialogEmbedded.call(viewDialogContainer);
	};

	var viewLinkDetails = function ()
	{
		if ($(this).hasClass('collapsed'))
		{
			var currentCell = $(this);
			var currentRow = $(this).parent();
			var linkId = currentRow.find('.link-id-column').html();
			$.ajax({
				type:"POST",
				url:"wallbin/getLinkDetails",
				data:{
					linkId:linkId
				},
				beforeSend:function ()
				{
					$.showOverlayLight();
				},
				complete:function ()
				{
					$.hideOverlayLight();
				},
				success:function (msg)
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
				async:true,
				dataType:'html'
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