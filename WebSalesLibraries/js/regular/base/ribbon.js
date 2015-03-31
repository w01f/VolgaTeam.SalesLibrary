(function ($)
{
	$.fn.ribbon = function (options)
	{
		var that = function ()
		{
			return thatRet;
		};
		var thatRet = that;

		var id = undefined;
		var onTabChange = undefined;
		if (options != undefined)
		{
			if (options.id != undefined)
			{
				if (this.attr('id'))
				{
					id = this.attr('id');
				}
			}
			onTabChange = options.onTabChange;
		}

		if ($.cookie("selectedRibbonTabId") != null)
			that.selectedPageId = $.cookie("selectedRibbonTabId");
		else
			that.selectedPageId = null;

		that.selectedTabIndex = -1;
		$.cookie("selectedRibbonTabIndex", 0, {
			expires: (60 * 60 * 24 * 7)
		});
		if (that.selectedPageId != null)
		{
			var storedTabIdFound = false;
			$('.ribbon-tab').each(function (index)
			{
				if (that.selectedPageId == $(this).attr('id'))
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
				that.selectedPageId = null;
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
				var tabLogos = $(this).find('.ribbon-tab-logo');
				var isBackstage = $(this).hasClass('file');
				header.append('<div id="ribbon-tab-header-' + index + '" class="ribbon-tab-header"></div>');
				var thisTabHeader = header.find('#ribbon-tab-header-' + index);
				thisTabHeader.append(title);
				var tabClickHandler = undefined;
				if (isBackstage)
				{
					thisTabHeader.addClass('file');
					tabClickHandler = function ()
					{
						that.switchToTabByIndex(index, id);
						that.goToBackstage();
					};
				}
				else
				{
					if (that.selectedTabIndex == -1)
					{
						that.selectedTabIndex = index;
						that.selectedPageId = id;
						thisTabHeader.addClass('sel');
					}
					tabClickHandler = function ()
					{
						that.returnFromBackstage();
						that.switchToTabByIndex(index, id);
					};
				}
				thisTabHeader.click(tabClickHandler);
				tabLogos.click(tabClickHandler);
				$(this).hide();
			});

			ribObj.find('.ribbon-button').each(function (index, value)
			{
				var title = $(this).find('.button-title');
				title.detach();
				$(this).append(title);

				var el = $(this);

				value.enable = function ()
				{
					el.removeClass('disabled');
				};
				value.disable = function ()
				{
					el.addClass('disabled');
				};
				value.isEnabled = function ()
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

			that.switchToTabByIndex(that.selectedTabIndex, that.selectedPageId);
		};

		that.switchToTabByIndex = function (index, id)
		{
			var headerStrip = $('#ribbon ').find('#ribbon-tab-header-strip');
			headerStrip.find('.ribbon-tab-header').removeClass('sel');
			headerStrip.find('#ribbon-tab-header-' + index).addClass('sel');

			$('#ribbon').find('.ribbon-tab').hide();
			$('#ribbon #' + tabNames[index]).show();

			var name = headerStrip.find('#ribbon-tab-header-' + index).find('.ribbon-title').html();
			if (onTabChange != undefined)
				onTabChange(index, id, name);
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
})(jQuery);