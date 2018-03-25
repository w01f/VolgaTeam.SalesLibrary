/*!
=====================================

OneMenu by SONHLAB.com - version 2.0 
website: http://sonhlab.com
Documentation: http://docs.sonhlab.com/onemenu-responsive-metro-ui-menu/

build - 0046
=====================================
*/

(function ($) {

	var OneMenuObj = function (e, options) {

		//Default settings
		var settings = $.extend({}, $.fn.onemenu.defaults, options);

		var $effect = settings.animEffect;

		var $hiddenzone = settings.hidezone;
		var $overlayId = settings.overlayId;
		var $onOpen = settings.onOpen;
		var $onClose = settings.onClose;

		var $curlastCtrlitem;
		var $nextlastCtrlitem;
		var $maxShowCtrlitems;
		var $totalCtrlitems;
		var $movenext = 0;
		var $curgroupid;
		var $clicked = 0;

		var $winWidth = $(window).width();

		var $viewWidth;
		var $newviewWidth;

		var $nav;
		var $itemholderWidth;
		var $itemsinline;

		var $submenu;

		var $subpadding;
		var $finishEffect;

		var $ctrlitemMargin;

		var $selectedGroupId = undefined;

		if (settings.closemenu == 'hide')
		{
			$ctrlitemMargin = 50;
		}
		else
		{
			$ctrlitemMargin = 100;
		}

		// Set Masonry Layout
		var $msrObj = {
			itemSelector: '.om-showitem',
			columnWidth: 100
		};
		var $masonryLayout = $(e).find('.om-nav').find('.om-itemlist').masonry($msrObj);


		// Start ctrlitemBar function
		function ctrlBar($nav)
		{

			// Control Item Holder Width
			if (settings.closemenu == 'hide')
			{
				$ctrlitemsWidth = $viewWidth - 100;
				$('.om-closenav').css({'display': 'none'});
			}
			else
			{
				$ctrlitemsWidth = $viewWidth - 150;
			}

			// Count Ctrl Items
			$totalCtrlitems = $('#' + $nav).find('.om-ctrlitems').find('.om-ctrlitem').length;

			var itemWidth = 1;
			if ($totalCtrlitems > 0)
				itemWidth = $('#' + $nav).find('.om-ctrlitems').find('.om-ctrlitem').outerWidth(true);

			// Max Ctrl Items in a line
			$maxShowCtrlitems = Math.floor($ctrlitemsWidth / itemWidth);

			$('#' + $nav).find('.om-ctrlitems').find('.om-centerblock').find('.om-ctrlitem').css({'display': 'none'});
			$movenext = 0;

			if ($maxShowCtrlitems >= $totalCtrlitems)
			{
				$('.om-nav').find('.om-movenext').css({'display': 'none'});
				$('#' + $nav).find('.om-ctrlitems').find('.om-centerblock').find('.om-ctrlitem').css({'display': 'inline-block'});


				// Update Control Item Bar Width
				if (settings.closemenu == 'hide')
				{
					$ctrlitemsWidth = $viewWidth;
				}
				else
				{
					$ctrlitemsWidth = $viewWidth - 100;
				}

			}
			else
			{ // Show Next Button

				$('.om-nav').find('.om-movenext').css({'display': 'block'});

				for (var $j = 0; $j < $maxShowCtrlitems; $j++)
				{
					$('#' + $nav).find('.om-ctrlitems').find('.om-centerblock').find('.om-ctrlitem').eq($j).css({'display': 'inline-block'});
				}
				$curlastCtrlitem = $j;

				// Update Control Item Bar Width
				if (settings.closemenu == 'hide')
				{
					$ctrlitemsWidth = $viewWidth - 50;
				}
				else
				{
					$ctrlitemsWidth = $viewWidth - 100;
				}
			}

			$('#' + $nav).find('.om-ctrlitems').css({'width': $ctrlitemsWidth + 'px'});

		}

		// End ctrlitemBar function


		// Start omRender function - Render Nav Panel
		function omRender($nav, $submenu)
		{

			// Reset Masonry
			$masonryLayout.masonry('destroy');

			if (settings.openstyle == 'overlay')
			{
				$('#' + $nav).css({'position': 'absolute'});
			}
			else
			{ // pushdown
				$('#' + $nav).css({'position': 'relative'});
			}


			// 10 = default padding 5px for each side
			$itemsinline = Math.floor(($viewWidth - 10) / 100);

			$itemholderWidth = $itemsinline * 100;

			$subpadding = ($viewWidth - $itemholderWidth) / 2;


			// Set Item Holder Width
			$('#' + $nav).find('.om-itemholder').css({'width': $itemholderWidth + 'px', 'padding': $subpadding + 'px'});

			// Show Control Bar
			ctrlBar($nav);


			// Start - Align Control Items
			if (settings.ctrlalign == 'center')
			{
				$('#' + $nav).find('.om-ctrlitems').css({
					'text-align': 'center',
					'margin': 'auto ' + $ctrlitemMargin + 'px'
				});
			}
			else if (settings.ctrlalign == 'left')
			{
				$('#' + $nav).find('.om-ctrlitems').css({'text-align': 'left', 'margin-left': $ctrlitemMargin + 'px'});
			}
			else
			{ // right
				$('#' + $nav).find('.om-ctrlitems').css({'text-align': 'right', 'margin-left': $ctrlitemMargin + 'px'});
			}
			// End - Align Control Items


			// Start - Check Sub Menu state
			if ($submenu == 'show')
			{ // Show Sub Menu

				var $dfgroupid = undefined;
				if($selectedGroupId != undefined)
				{
					// Set active ctrl item
					$('#' + $nav).find('.om-ctrlitems').find('.om-ctrlitem[data-groupid="' + $selectedGroupId + '"]').addClass('om-activectrlitem');

					// Get Ctrlitem Default Group ID
					$dfgroupid = $selectedGroupId;
				}
				else
				{
					// Set active ctrl item
					$('#' + $nav).find('.om-ctrlitems').find('.om-ctrlitem').eq(0).addClass('om-activectrlitem');

					// Get Ctrlitem Default Group ID
					$dfgroupid = $('#' + $nav).find('.om-ctrlitems').find('.om-ctrlitem').eq(0).attr('data-groupid');
				}

				// Show items in Default Group
				$('#' + $nav).find('.om-itemlist').find('div[data-group="' + $dfgroupid + '"]').toggleClass('om-item om-showitem');

				// Show active item group in submenu
				$('#' + $nav).find('.om-itemlist').find('.om-showitem').css({'display': 'block'});

				if ($effect == 'fade')
				{

					$('#' + $nav).find('.om-itemholder').fadeIn(400, function () {

						$newviewWidth = $('#' + $nav).parent().width();

						if ($viewWidth != $newviewWidth)
						{

							omResize($newviewWidth);

						}
						else
						{

							// Show active item group in submenu
							$('#' + $nav).find('.om-itemlist').find('.om-showitem').css({'display': 'block'});

							// Display Masonry Layout
							$masonryLayout.masonry($msrObj);

						}

					});

				}
				else if ($effect == 'slide')
				{

					$('#' + $nav).find('.om-itemholder').slideDown(400, function () {

						$newviewWidth = $('#' + $nav).parent().width();

						if ($viewWidth != $newviewWidth)
						{

							omResize($newviewWidth);

						}
						else
						{

							// Show active item group in submenu
							$('#' + $nav).find('.om-itemlist').find('.om-showitem').css({'display': 'block'});

							// Display Masonry Layout
							$masonryLayout.masonry($msrObj);

						}

					});

				}
				else
				{ // No effect

					$('#' + $nav).find('.om-itemholder').css({'display': 'block'});

					$newviewWidth = $('#' + $nav).parent().width();

					if ($viewWidth != $newviewWidth)
					{

						omResize($newviewWidth);

					}
					else
					{

						// Show active item group in submenu
						$('#' + $nav).find('.om-itemlist').find('.om-showitem').css({'display': 'block'});

						// Display Masonry Layout
						$masonryLayout.masonry($msrObj);

					}

				}
				// End - Check Sub Menu state

			}
			else
			{ // Hide Sub Menu

				$('.om-itemholder').css({'display': 'none'});

				// Display Masonry Layout
				$masonryLayout.masonry($msrObj);

			}

		}

		// End omRender function


		// Start Resize Function
		function omResize($newviewWidth)
		{

			// Set new OneMenu Container Width
			$viewWidth = $newviewWidth;

			$itemsinline = Math.floor(($viewWidth - 10) / 100);

			$itemholderWidth = $itemsinline * 100;

			$subpadding = ($viewWidth - $itemholderWidth) / 2;


			// Set new Item Holder Width
			$('#' + $nav).find('.om-itemholder').css({'width': $itemholderWidth + 'px', 'padding': $subpadding + 'px'});

			// Prepare active item group in submenu
			$('#' + $nav).find('.om-itemlist').find('.om-showitem').css({'display': 'block', 'opacity': 0});


			// Display Masonry Layout
			$masonryLayout.masonry($msrObj);

			// Show Control Bar
			ctrlBar($nav);

			$('#' + $nav).find('.om-itemlist').find('.om-showitem').css({'display': 'block', 'opacity': 1});

		}

		// End Resize Function


		// Start - Click MoveNext Button
		function movenextCtrlBt($nav)
		{

			$movenext = $movenext + 1;

			$nextlastCtrlitem = $maxShowCtrlitems + ($maxShowCtrlitems * $movenext);

			// Reset Control Items
			$('#' + $nav).find('.om-ctrlitems').find('.om-centerblock').find('.om-ctrlitem').css({'display': 'none'});

			// Return first control item
			if ($curlastCtrlitem >= $totalCtrlitems)
			{
				$movenext = 0;
				$curlastCtrlitem = 0;
				$nextlastCtrlitem = $maxShowCtrlitems;
			}

			for ($curlastCtrlitem; $curlastCtrlitem < $nextlastCtrlitem; $curlastCtrlitem++)
			{
				$('#' + $nav).find('.om-ctrlitems').find('.om-centerblock').find('.om-ctrlitem').eq($curlastCtrlitem).css({'display': 'block'});
			}

			return false;
		}

		// End - Click MoveNext Button


		// Start Autoshow
		if (settings.autoshow)
		{

			// Get default OneMenu ID
			$nav = settings.autoshow;

			// Check autoshow ID
			if ($(e).find('#' + $nav).length > 0)
			{

				if ($clicked == 0)
				{

					$clicked = 1;

					// Get OneMenu Container Width
					$viewWidth = $('#' + $nav).parent().width();

					// Show Nav Panel
					if ($effect == 'fade')
					{
						$('#' + $nav).fadeIn(400, function () {
						});
					}
					else
					{
						$('#' + $nav).css({'display': 'block'});
					}

					$submenu = settings.submenu;

					// Call omRender function
					omRender($nav, $submenu);

				}

			}
			else
			{
				alert('Your menu ID is not found. Please check your autoshow value!');
			}

		}
		// End Autoshow


		// OneMenu is clicked
		$(e).find('.onemenu').on('click', function () {

			if ($clicked == 0)
			{

				$nav = $(this).attr('data-navid');

				// Get OneMenu Container Width
				$viewWidth = $('#' + $nav).parent().width();

				// Get Submenu setting
				if ($(this).attr('data-submenu'))
				{
					$submenu = $(this).attr('data-submenu');
				}
				else
				{
					$submenu = settings.submenu;
				}

				$clicked = 1;

				// Hide specified zone
				if ($hiddenzone != '')
				{
					$($hiddenzone).addClass('none');
				}

				// Show Nav Panel
				if ($effect == 'fade')
				{
					$('#' + $nav).fadeIn(400, function () {
						omRender($nav, $submenu);
					});
				}
				else if ($effect == 'slide')
				{
					$('#' + $nav).slideDown(400, function () {
						omRender($nav, $submenu);
					});
				}
				else
				{ // No Effect
					$('#' + $nav).css({'display': 'block'});
					omRender($nav, $submenu);
				}

				$onOpen();
			}

		});
		// End OneMenu Click function


		// Start - Move next Control Items
		$('.om-nav').find('.om-ctrlbar').find('.om-movenext').on('click', function () {
			movenextCtrlBt($nav);
		});
		// End - Move next Control Items


		// Click Control Items
		$(e).find('.om-nav').find('.om-ctrlitem').on('click', function () {

			if (!$(this).hasClass('om-activectrlitem'))
			{ // Show SubMenu

				// Current Active Group
				$curgroupid = $('.om-activectrlitem').attr('data-groupid');

				// Reset active item
				$(e).find('.om-nav').find('.om-ctrlitem').removeClass('om-activectrlitem');

				// Destroy Masonry
				$masonryLayout.masonry('destroy');

				if ($curgroupid)
				{
					// Hide items in current group
					$('#' + $nav).find('.om-itemlist').find('.om-showitem').css({'display': 'none'});
					$('#' + $nav).find('.om-itemlist').find('div[data-group="' + $curgroupid + '"]').toggleClass('om-item om-showitem');
				}

				// Set new active group
				var $newgroupid = $(this).attr('data-groupid');
				$selectedGroupId = $newgroupid;

					// Show items in new group
				$('#' + $nav).find('.om-itemlist').find('div[data-group="' + $newgroupid + '"]').toggleClass('om-item om-showitem');

				// Set active item
				$(this).addClass('om-activectrlitem');

				// Get OneMenu Container Width
				$viewWidth = $('#' + $nav).parent().width();

				$finishEffect = $('#' + $nav).find('.om-itemlist').find('.om-showitem').length;

				$('#' + $nav).find('.om-itemlist').find('.om-showitem').fadeIn(400, function () {

					$newviewWidth = $('#' + $nav).parent().width();

					if ($viewWidth != $newviewWidth)
					{

						if (--$finishEffect == 0)
						{
							omResize($newviewWidth);
						}

					}
					else
					{

						// Show active item group in submenu
						$('#' + $nav).find('.om-itemlist').find('.om-showitem').css({'display': 'block'});

						// Display Masonry Layout
						$masonryLayout.masonry($msrObj);
					}

				});

				// Show Sub Menu
				$('#' + $nav).find('.om-itemholder').show();

				// re-Call Masonry function
				$masonryLayout.masonry($msrObj);

			}
			else
			{ // Hide SubMenu

				// Current Active Group
				$curgroupid = $('.om-activectrlitem').attr('data-groupid');

				// Reset active item
				$(e).find('.om-nav').find('.om-ctrlitem').removeClass('om-activectrlitem');

				if ($curgroupid)
				{
					// Hide items in current group
					$('#' + $nav).find('.om-itemlist').find('.om-showitem').css({'display': 'none'});
					$('#' + $nav).find('.om-itemlist').find('div[data-group="' + $curgroupid + '"]').toggleClass('om-item om-showitem');
				}
				// Hide Sub Menu
				$('#' + $nav).find('.om-itemholder').hide();
				$submenu = 'hide';
			}
		});
		// End Click Control Items

		// Close Button is Clicked
		$(e).find('.om-nav').find('.om-closenav').on('click', function () {
			close();
		});

		$('#shortcut-action-menu').find('.main-level .menu').on('click', function () {
			close();
		});

		$($overlayId).on('click', function () {
			close();
		});

		function close()
		{
			$clicked = 0;

			// Show Hidden Zone
			if ($hiddenzone != '')
			{
				$($hiddenzone).removeClass('none');
			}

			// Reset Nav
			$(e).find('.om-nav').find('.om-ctrlitem').removeClass('om-activectrlitem');


			$(e).find('.om-nav').find('.om-itemlist').find('.om-showitem').css({'display': 'none'});
			$(e).find('.om-nav').find('.om-itemlist').find('.om-showitem').toggleClass('om-item om-showitem');

			// Hide Nav Panel
			if ($effect == 'fade')
			{
				$('#' + $nav).fadeOut(400, function () {
				});
			}
			else if ($effect == 'slide')
			{
				$('#' + $nav).slideUp(400, function () {
				});
			}
			else
			{
				$('#' + $nav).css({'display': 'none'});
			}

			if ($onClose !== undefined)
				$onClose();
		}

		// End Close Button


		// Resize Window
		function debouncer(func, timeout)
		{
			var timeoutID, timeout = timeout || 200;
			return function () {
				var scope = this, args = arguments;
				clearTimeout(timeoutID);
				timeoutID = setTimeout(function () {
					func.apply(scope, Array.prototype.slice.call(args));
				}, timeout);
			}
		}

		$(window).resize(debouncer(function () {

			if ($clicked == 1)
			{

				var $nwWidth = $(window).width();

				if ($winWidth != $nwWidth)
				{

					// Get New View Width
					$newviewWidth = $('#' + $nav).parent().width();

					if ($viewWidth != $newviewWidth)
					{
						omResize($newviewWidth);
					}

					// update winWidth
					$winWidth = $nwWidth;
				}
			}

		}));
		// End Resize


	};

	$.fn.onemenu = function (options) {

		return this.each(function (key, value) {

			// Return early if this element already has a plugin instance
			if ($(this).data('onemenu')) return $(this).data('onemenu');

			// Pass options to plugin constructor
			var onemenu = new OneMenuObj(this, options);

			// Store plugin object in this element's data
			$(this).data('onemenu', onemenu);

		});

	};

	//Default settings
	$.fn.onemenu.defaults = {
		openstyle: 'overlay', // overlay || pushdown
		ctrlalign: 'center', // left, center, right
		submenu: 'show', // values: show | hide
		closemenu: 'show', // values: show | hide
		hidezone: '', // the zone will be hidden when nav panel display
		animEffect: 'none', // none | fade
		overlayId: '#content-overlay',
		onOpen: function ()
		{
		},
		onClose: function ()
		{
		}
	};

})(jQuery);