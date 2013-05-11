(function ($)
{
	$.fn.ribbon = function (id)
	{
		if (!id)
		{
			if (this.attr('id'))
			{
				id = this.attr('id');
			}
		}

		var that = function ()
		{
			return thatRet;
		};

		var thatRet = that;

		if ($.cookie("selectedRibbonTabId") != null)
			that.selectedTabId = $.cookie("selectedRibbonTabId");
		else
			that.selectedTabId = null;

		that.selectedTabIndex = -1;
		$.cookie("selectedRibbonTabIndex", 0, {
			expires: (60 * 60 * 24 * 7)
		});
		if (that.selectedTabId != null)
		{
			var storedTabIdFound = false;
			$('.ribbon-tab').each(function (index)
			{
				if (that.selectedTabId == $(this).attr('id'))
				{
					that.selectedTabIndex = index;
					$.cookie("selectedRibbonTabIndex", index, {
						expires: (60 * 60 * 24 * 7)
					});
					storedTabIdFound = true;
				}
			});
			if (!storedTabIdFound)
			{
				that.selectedTabIndex = -1;
				that.selectedTabId = null;
				$.cookie("selectedRibbonTabId", null);
			}
		}

		var tabNames = [];

		that.goToBackstage = function ()
		{
			ribObj.addClass('backstage');
		};

		that.returnFromBackstage = function ()
		{
			ribObj.removeClass('backstage');
		};
		var ribObj = null;

		that.init = function (id)
		{
			if (!id)
			{
				id = 'ribbon';
			}

			ribObj = $('#' + id);
			ribObj.find('.ribbon-window-title').after('<div id="ribbon-tab-header-strip"></div>');
			var header = ribObj.find('#ribbon-tab-header-strip');

			ribObj.find('.ribbon-tab').each(function (index)
			{
				var id = $(this).attr('id');
				if (id == undefined || id == null)
				{
					$(this).attr('id', 'tab-' + index);
					id = 'tab-' + index;
				}
				tabNames[index] = id;

				var title = $(this).find('.ribbon-title');
				var isBackstage = $(this).hasClass('file');
				header.append('<div id="ribbon-tab-header-' + index + '" class="ribbon-tab-header"></div>');
				var thisTabHeader = header.find('#ribbon-tab-header-' + index);
				thisTabHeader.append(title);
				if (isBackstage)
				{
					thisTabHeader.addClass('file');

					thisTabHeader.click(function ()
					{
						that.switchToTabByIndex(index, id);
						that.goToBackstage();
					});
				}
				else
				{
					if (that.selectedTabIndex == -1)
					{
						that.selectedTabIndex = index;
						that.selectedTabId = id;
						thisTabHeader.addClass('sel');
					}

					thisTabHeader.click(function ()
					{
						that.returnFromBackstage();
						that.switchToTabByIndex(index, id);
					});
				}

				$(this).hide();
			});

			ribObj.find('.ribbon-button').each(function ()
			{
				var title = $(this).find('.button-title');
				title.detach();
				$(this).append(title);

				var el = $(this);

				this.enable = function ()
				{
					el.removeClass('disabled');
				};
				this.disable = function ()
				{
					el.addClass('disabled');
				};
				this.isEnabled = function ()
				{
					return !el.hasClass('disabled');
				};

				if ($(this).find('.ribbon-hot').length == 0)
				{
					$(this).find('.ribbon-normal').addClass('ribbon-hot');
				}
				if ($(this).find('.ribbon-disabled').length == 0)
				{
					$(this).find('.ribbon-normal').addClass('ribbon-disabled');
					$(this).find('.ribbon-normal').addClass('ribbon-implicit-disabled');
				}
			});

			ribObj.find('.ribbon-section').each(function ()
			{
				$(this).after('<div class="ribbon-section-sep"></div>');
			});

			ribObj.find('div').attr('unselectable', 'on');
			ribObj.find('span').attr('unselectable', 'on');
			ribObj.attr('unselectable', 'on');

			that.switchToTabByIndex(that.selectedTabIndex, that.selectedTabId);
		};

		that.switchToTabByIndex = function (index, id)
		{
			var headerStrip = $('#ribbon ').find('#ribbon-tab-header-strip');
			headerStrip.find('.ribbon-tab-header').removeClass('sel');
			headerStrip.find('#ribbon-tab-header-' + index).addClass('sel');

			$('#ribbon').find('.ribbon-tab').hide();
			$('#ribbon #' + tabNames[index]).show();

			var name = headerStrip.find('#ribbon-tab-header-' + index).find('.ribbon-title').html();
			that.switchToPageByIndex(index, id, name);
		};

		that.switchToPageByIndex = function (index, id, name)
		{
			$.cookie("selectedRibbonTabIndex", index, {
				expires: (60 * 60 * 24 * 7)
			});
			$.cookie("selectedRibbonTabId", id, {
				expires: (60 * 60 * 24 * 7)
			});
			$.ajax({
				type: "POST",
				url: "statistic/writeActivity",
				data: {
					type: 'System',
					subType: 'Tab Changed',
					data: $.toJSON({
						Name: name
					})
				},
				async: true,
				dataType: 'html'
			});
			var minibar = $('.jx-bar, .jx-show');
			switch (id)
			{
				case 'home-tab':
					minibar.css({
						'height': '30px'
					});
					$.initWallbinView();
					break;
				case 'search-full-tab':
				case 'search-file-card-tab':
					minibar.css({
						'height': '0px'
					});
					$.initSearchView();
					break;
				case 'calendar-tab':
					minibar.css({
						'height': '0px'
					});
					$.initCalendarView();
					break;
				case 'favorites-tab':
					minibar.css({
						'height': '0px'
					});
					$.initFavoritesView();
					break;
				default:
					minibar.css({
						'height': '0px'
					});
					if (id != null && id.indexOf("help-tab-") >= 0)
						$.initHelpView(id);
					else
						$.initWallbinView();
					break;
			}
		};

		$.fn.enable = function ()
		{
			if (this.hasClass('ribbon-button'))
			{
				if (this[0] && this[0].enable)
				{
					this[0].enable();
				}
			}
			else
			{
				this.find('.ribbon-button').each(function ()
				{
					$(this).enable();
				});
			}
		};

		$.fn.disable = function ()
		{
			if (this.hasClass('ribbon-button'))
			{
				if (this[0] && this[0].disable)
				{
					this[0].disable();
				}
			}
			else
			{
				this.find('.ribbon-button').each(function ()
				{
					$(this).disable();
				});
			}
		};

		$.fn.isEnabled = function ()
		{
			if (this[0] && this[0].isEnabled)
			{
				return this[0].isEnabled();
			}
			else
			{
				return true;
			}
		};

		that.init(id);

		$.fn.ribbon = that;
	};

	$(document).ready(function ()
	{
		$('#ribbon').ribbon();
		$('a#view-dialog-link').fancybox();
		$.cookie("recoverSearchState" + "search-full-tab", false, {
			expires: (60 * 60 * 24 * 7)
		});
		$.cookie("recoverSearchState" + "search-file-card-tab", false, {
			expires: (60 * 60 * 24 * 7)
		});

		$('.logout-button').off('click').on('click', function ()
		{
			$.logout();
		});
		$('.qbuilder-button').off('click').on('click', function ()
		{
			window.open("qbuilder");
		});
	});
})(jQuery);