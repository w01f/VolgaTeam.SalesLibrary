/*!
 =====================================

 OneMenu by SONHLAB.com - version 1.1 - 04162013
 website: http://sonhlab.com
 Documentation: http://docs.sonhlab.com/onemenu-responsive-metro-ui-menu/

 =====================================
 */

(function ($)
{

	var OneMenuObj = function (e, options)
	{

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
		var $clicked;

		var $winWidth;
		var $winHeight;
		var $nav;

		var $selectedGroupId = undefined;

		// Get IE version
		function getInternetExplorerVersion()
		{
			var rv = -1;
			if (navigator.appName == 'Microsoft Internet Explorer')
			{
				var ua = navigator.userAgent;
				var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
				if (re.exec(ua) != null)
					rv = parseFloat(RegExp.$1);
			}
			return rv;
		}

		var $ie_ver = getInternetExplorerVersion();


		// Call Masonry function
		var $msr = 0;

		function callMasonry()
		{
			//  Dynamic Grid use Masonry Plugin
			$('.om-itemlist').masonry({
				itemSelector: '.om-showitem',
				columnWidth: 100
			});
			$msr = 1;
		}

		function open()
		{
			$nav = $(e).attr('data-navid');

			$clicked = 1;

			// Show Nav Panel
			if ($effect == 'fade')
			{
				$('#' + $nav).fadeIn(400, function ()
				{
				});
			}
			else
			{
				$('#' + $nav).css({'display': 'block'});
			}

			$('#' + $nav).addClass('opened');


			// Call omRender function
			omRender($nav);


			// Hide specified zone
			if ($hiddenzone != '')
			{
				$($hiddenzone).addClass('none');
			}


			// Click Control Items
			$('#' + $nav).find('.om-ctrlitem').on('click', function ()
			{
				if (!$(this).hasClass('om-activectrlitem'))
				{

					// Current Active Group
					$curgroupid = $('.om-activectrlitem').attr('data-groupid');

					// Reset active item
					$('.om-ctrlitem').removeClass('om-activectrlitem');

					if ($msr == 1)
					{
						$('.om-itemlist').masonry('destroy');
						$msr = 0;
					}

					// Hide items in current group
					$('#' + $nav).find('.om-itemlist').find('.om-showitem').css({'display': 'none'});
					$('#' + $nav).find('.om-itemlist').find('div[data-group="' + $curgroupid + '"]').toggleClass('om-item om-showitem');

					// Set new active group
					var $newgroupid = $(this).attr('data-groupid');
					$selectedGroupId = $newgroupid;

					// Show items in new group
					$('#' + $nav).find('.om-itemlist').find('div[data-group="' + $newgroupid + '"]').toggleClass('om-item om-showitem');

					// Call Masonry function
					callMasonry();

					// Show items
					if ($effect == 'fade')
					{
						$('#' + $nav).find('.om-itemlist').find('.om-showitem').fadeIn(200);
					}
					else
					{
						$('#' + $nav).find('.om-itemlist').find('.om-showitem').css({'display': 'block'});
					}

					// Set active item
					$(this).addClass('om-activectrlitem');
				}
			});
			// End Click Control Items


			// Close Button is Clicked
			$('.om-closenav').on('click', function ()
			{
				close();
				$onClose();
			});

			$('#shortcut-action-menu').find('.main-level .menu').on('click', function ()
			{
				close();
				$onClose();
			});

			$($overlayId).on('click', function ()
			{
				close();
				$onClose();
			});
			// End Close Button
		}

		function close()
		{
			$clicked = 0;

			// Reset Nav
			$('.om-ctrlitem').removeClass('om-activectrlitem');

			// Show Hidden Zone
			$($hiddenzone).removeClass('none');

			if ($msr == 1)
			{
				$('.om-itemlist').masonry('destroy');
				$msr = 0;
			}

			$('.om-itemlist').find('.om-showitem').css({'display': 'none'});
			$('.om-itemlist').find('.om-showitem').toggleClass('om-item om-showitem');

			// Hide Nav Panel
			if ($effect == 'fade')
			{
				$('#' + $nav).fadeOut(400, function ()
				{
				});
			}
			else
			{
				$('#' + $nav).css({'display': 'none'});
			}
			$('#' + $nav).removeClass('opened');


			// Unbind movenextCtrlBt function
			$('#' + $nav).find('.om-movenext').unbind();
		}

		// Render Nav Panel
		function omRender($nav)
		{

			// Get Screen Size
			$winWidth = $(window).width();
			$winHeight = $(window).height();

			var $itemholderWidth;
			var $itemsinline;
			var $ctrlitemsWidth;

			$itemsinline = Math.floor($winWidth / 100);
			$itemholderWidth = $itemsinline * 100;

			// Screen Width is less than 960px
			if ($winWidth < 960)
			{
				$ctrlitemsWidth = $winWidth - 144;
				$ctrlitemsMargin = 72;
			}
			else
			{ // Screen Width > 960px
				$ctrlitemsWidth = $winWidth - 144;
				$ctrlitemsMargin = 48;
			}

			// Item Holder Size
			$('.om-itemholder').css({'width': $itemholderWidth + 'px'});

			// Ctrl Items Size
			$('.om-ctrlitems').css({'width': $ctrlitemsWidth + 'px', 'margin-left': $ctrlitemsMargin + 'px'});


			/*  NAV */

			// Current Active Group
			$curgroupid = $('.om-activectrlitem').attr('data-groupid');

			// Hide items in current group
			$('#' + $nav).find('.om-itemlist').find('.om-showitem').css({'display': 'none'});
			$('#' + $nav).find('.om-itemlist').find('div[data-group="' + $curgroupid + '"]').toggleClass('om-item om-showitem');

			if ($msr == 1)
			{
				$('.om-itemlist').masonry('destroy');
				$msr = 0;
			}

			// Reset active ctrl item
			$('#' + $nav).find('.om-ctrlitems').find('.om-ctrlitem').removeClass('om-activectrlitem');

			// Get Ctrlitem Default Group ID
			var $dfgroupid = $selectedGroupId != undefined ?
				$selectedGroupId :
				$('#' + $nav).find('.om-ctrlitems').find('.om-ctrlitem').eq(0).attr('data-groupid');

			// Set active ctrl item
			$('#' + $nav).find('.om-ctrlitems').find('div[data-groupid="' + $dfgroupid + '"]').eq(0).addClass('om-activectrlitem');


			// Show items in Default Group
			$('#' + $nav).find('.om-itemlist').find('div[data-group="' + $dfgroupid + '"]').toggleClass('om-item om-showitem');

			if ($effect == 'fade')
			{
				$('#' + $nav).find('.om-itemlist').find('.om-showitem').fadeIn(200);
			}
			else
			{
				$('#' + $nav).find('.om-itemlist').find('.om-showitem').css({'display': 'block'});
			}

			// Count Ctrl Items			
			$totalCtrlitems = $('#' + $nav).find('.om-ctrlitems').find('.om-ctrlitem').length;

			// Max Ctrl Items in a line
			$maxShowCtrlitems = Math.floor($ctrlitemsWidth / 48);


			// Hide Next Button
			if ($maxShowCtrlitems >= $totalCtrlitems)
			{
				$('.om-movenext').addClass('none');
			}
			else
			{ // Show Next Button
				$('.om-movenext').removeClass('none');
			}


			// Ctrl Items length
			$curlastCtrlitem = 0;
			$nextlastCtrlitem = $maxShowCtrlitems;
			var $j = $curlastCtrlitem;

			// Reset Ctrl Bar
			$('#' + $nav).find('.om-ctrlitems').find('.om-centerblock').find('.om-ctrlitem').css({'display': 'none'});
			$movenext = 0;

			for ($j; $j < $maxShowCtrlitems; $j++)
			{
				$('#' + $nav).find('.om-ctrlitems').find('.om-centerblock').find('.om-ctrlitem').eq($j).css({'display': 'block'});
			}
			$curlastCtrlitem = $j;

			/* END NAV */

			// Call Masonry function
			callMasonry();


			// Start to Click MoveNext Button
			//var $movenext = 0;

			function movenextCtrlBt()
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


			// Move next Control Items
			$('#' + $nav).find('.om-movenext').on('click', function ()
			{
				movenextCtrlBt();
			});


		}


		// OneMenu is clicked
		$(e).on('click', function ()
		{
			open();
			$onOpen();
		});
		$(e).find('.main-site-url').on('click', function (e)
		{
			e.stopPropagation();
		});
		// End OneMenu Click function

		// Resize Window
		$(window).on('resize', function ()
		{
			if ($clicked == 1)
			{
				var $nwWidth = $(window).width();
				var $nwHeight = $(window).height();
				if ($winWidth != $nwWidth)
				{
					omRender($nav);
				}
			}
		});
		// End Resize
	};

	$.fn.onemenu = function (options)
	{

		return this.each(function (key, value)
		{

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
		hidezone: '', // the zone will be hidden when nav panel display
		animEffect: 'none', // none, fade
		overlayId: '#content-overlay',
		onOpen: function ()
		{
		},
		onClose: function ()
		{
		}
	};

})(jQuery);