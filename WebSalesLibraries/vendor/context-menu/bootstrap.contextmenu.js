(function ($)
{
	var _bfd = {};
	var _cm = {
		init: function (target, elements)
		{
			var element_id;
			element_id = _cm.guid();
			var context_menu = $('<div class="dropdown clearfix bootstrap-contextmenu"></div>');
			context_menu
				.css({
					display: 'none',
					position: 'absolute'
				})
				.attr({
					id: element_id
				})
				.appendTo('body');

			$(target).data('e_id', element_id);
			mc = $(target).data('mc');

			if (mc == undefined)
			{
				mc = 3;
			}
			$(target).data('e', $.extend(true, {}, elements));
			$(target).css('-moz-user-select', 'none');
			$(target).css('-khtml-user-select', 'none');
			$(target).css('user-select', 'none');

			_cm.buildElements(target, context_menu, elements, true);
			_cm.create(target, mc);

		},
		buildElements: function (target, parent, elements, is_parent)
		{
			var ul = $('<ul aria-labelledby="dropdownMenu" role="menu" class="dropdown-menu"></ul>');
			is_parent = (is_parent != undefined) ? is_parent : true
			if (is_parent == true)
			{
				ul.show();
			}
			for (var i in elements)
			{
				var li = $('<li></li>');
				li.attr({
					id: i
				});
				switch (typeof elements[i])
				{
					case 'object':
						if (elements[i].text != undefined)
						{
							if (elements[i].text == '---')
							{
								li.addClass('divider');
							}
							else
							{
								var _a = $('<a href="#" tabindex="-1"></a>');
								if (typeof elements[i].icon == 'string')
								{
									li
										.append(_a
											.append(' <i class="' + elements[i].icon + '"></i> ')
											.append(elements[i].text));
								}
								else
								{
									_a = $('<a href="#" tabindex="-1">' + elements[i].text + '</a>');
									li.append(_a);

								}
								if (typeof elements[i].click == 'function')
								{
									var eventName = "";
									if (!( /Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent) ))
										eventName = "mousedown";
									else
										eventName = "touchstart";
									_a.on(eventName, {
										key: i,
										target: target,
										callback: elements[i].click
									}, function (e)
									{
										e.stopImmediatePropagation();
										if (!$(this).parent().hasClass('disabled'))
										{
											window.setTimeout(function ()
											{
												$('.bootstrap-contextmenu').hide();
												e.data.callback(e.data.target, $(this).parent());
											}, 200);
										}
										return false;
									});
								}
								if (elements[i].disabled != undefined && elements[i].disabled == true)
								{
									li.addClass('disabled');
								}
								if (typeof elements[i].children == 'object')
								{
									if (!li.hasClass('disabled'))
									{
										li.addClass('dropdown-submenu');
										_cm.buildElements(target, li, elements[i].children, false);
									}
								}
								if (li.hasClass('disabled'))
								{
									li.children('a').children('i').hide();
								}
							}
						}
						break;
					case 'string':
						if (elements[i] == '---')
						{
							li.addClass('divider');
						}
						else
						{
							li.append($('<a href="#" tabindex="-1">' + elements[i] + '</a>'));
						}
						break;

				}
				ul.append(li);
			}
			parent.append(ul);
		},
		create: function (target, mc)
		{
			if (mc == undefined) mc = 3;
			if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent))
			{
				mc = 1;
			}
			var element_id = $(target).data('e_id');
			$("#" + element_id).bind("contextmenu", function (e)
			{
				e.preventDefault();
			});
			$(target).on('mousedown', function (e)
			{
				if (!( /Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent) ))
				{
					$("#" + element_id).hide();
				}
			});
			$(target).bind("contextmenu", function (e)
			{
				e.preventDefault();
			});
			$(document).on('mousedown', function (e)
			{
				if (!( /Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent) ))
				{
					$("#" + element_id).hide();
				}
			});

			if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent))
			{
				$("#" + element_id).on('mousedown touchstart', function (e)
				{
					$("#" + element_id).hide();
				});
				$(document).on('mousedown touchstart', function (e)
				{
					$('.bootstrap-contextmenu').hide();
				});
			}

			$(target)
				.off('mousedown.context touchstart.context')
				.on('mousedown.context touchstart.context',function (e)
				{
					e.preventDefault();
					$('.bootstrap-contextmenu').hide();
					var thisItem = $(this);
					$.contextTargetItem = $(target);
					var showContext = function (event)
					{
						if (_bfd[element_id] != undefined)
						{
							var fnc = _bfd[element_id];
							fnc($.contextTargetItem);
						}
						var d = {}, x, y;
						var pageX = event.type == 'mouseup' ? event.pageX : event.originalEvent.touches[0].pageX;
						var clientX = event.type == 'mouseup' ? event.clientX : event.originalEvent.touches[0].clientX;
						var pageY = event.type == 'mouseup' ? event.pageY : event.originalEvent.touches[0].pageY;
						var clientY = event.type == 'mouseup' ? event.clientY : event.originalEvent.touches[0].clientY;
						if (self.innerHeight)
						{
							d.pageYOffset = self.pageYOffset;
							d.pageXOffset = self.pageXOffset;
							d.innerHeight = self.innerHeight;
							d.innerWidth = self.innerWidth;
						}
						else if (document.documentElement &&
							document.documentElement.clientHeight)
						{
							d.pageYOffset = document.documentElement.scrollTop;
							d.pageXOffset = document.documentElement.scrollLeft;
							d.innerHeight = document.documentElement.clientHeight;
							d.innerWidth = document.documentElement.clientWidth;
						}
						else if (document.body)
						{
							d.pageYOffset = document.body.scrollTop;
							d.pageXOffset = document.body.scrollLeft;
							d.innerHeight = document.body.clientHeight;
							d.innerWidth = document.body.clientWidth;
						}
						(pageX) ? x = pageX : x = clientX;
						(pageY) ? y = pageY : y = clientY;
						var height = $("#x-context-" + $.contextTargetItem.attr('id')).height();
						if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent))
						{
							if ($("#" + element_id).css('display') == 'block')
							{
								$('.bootstrap-contextmenu').hide();
							}
							else
							{
								if (y + height > $(document).height())
								{
									$("#" + element_id).css({
										top: y - height,
										left: x
									}).fadeIn(20);
								}
								else
								{
									$("#" + element_id).css({
										top: y,
										left: x
									}).fadeIn(20);
								}
							}
						}
						else
						{
							console.log("Show 4...");
							if (y + height > $(document).height())
							{
								$("#" + element_id).css({
									top: y - height,
									left: x
								}).fadeIn(20);
							}
							else
							{
								$("#" + element_id).css({
									top: y,
									left: x
								}).fadeIn(20);
							}
						}
					};
					// right-click
					if (mc == 3)
					{
						thisItem.off('mouseup.context');
						if (e.which == mc)
						{
							thisItem.on('mouseup.context', function (clickEvent)
							{
								showContext(clickEvent);
							});
						}
					}
					// left-click ot touch
					else if (mc == 1)
					{
						var eventToTouchend = e;
						e.stopPropagation();
						console.log("Set Timer");
						$.contextTimer = window.setTimeout(function ()
						{
							thisItem.off('touchend').on('touchend.context', function (eventUp)
							{
								eventUp.stopPropagation();
							});
							showContext(eventToTouchend);
						}, 500);
						thisItem
							.off('touchmove.context').on('touchmove.context', function ()
							{
								$(this).off('touchend');
								window.clearTimeout($.contextTimer);
							})
							.off('touchend.context').on("touchend.context", function ()
							{
								console.log("Reset Timer");
								window.clearTimeout($.contextTimer);
							});
					}
				}).off('touchmove.context').on('touchmove.context', function ()
				{
					$(this).off('touchend');
					window.clearTimeout($.contextTimer);
				});
		},
		guid: function ()
		{
			var S4 = function ()
			{
				return Math.floor(Math.random() * 0x10000 /* 65536 */
				).toString(16);
			};
			return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
		}

	}

	$.fn.contextMenu = function (elements, ids)
	{
		ids = (ids != undefined) ? ids : '';
		if (typeof elements == 'string' && ids != '')
		{
			var element_id = $(this).data('e_id');
			var _elements = $(this).data('e');
			if (typeof element_id == 'string' && element_id != '')
			{
				if (typeof ids != 'function')
				{
					var ary = [];
					if (ids.indexOf('>') > 0)
					{
						var tmp = ids.split('>');
						for (var i in tmp)
						{
							ary[ary.length] = $.trim(tmp[i]);
						}
					}
					else
					{
						ary[ary.length] = $.trim(ids);
					}
				}
				switch (elements)
				{
					case 'disable':
						$('#' + element_id).remove();
						var tmpe = _elements;
						var l = ary.length - 1;
						for (var i in ary)
						{
							if (tmpe[ary[i]] != undefined)
							{
								if (i == l)
								{
									tmpe[ary[i]].disabled = true;
								}
								else
								{
									if (tmpe[ary[i]].children != undefined)
									{
										tmpe = tmpe[ary[i]].children;
									}
									else
									{
										break;
									}
								}
							}
							else
							{
								break;
							}
						}
						_cm.init(this, _elements);
						break;
					case 'enable':
						$('#' + element_id).remove();
						var tmpe = _elements;
						var l = ary.length - 1;
						for (var i in ary)
						{
							if (tmpe[ary[i]] != undefined)
							{
								if (i == l)
								{
									tmpe[ary[i]].disabled = false;
								}
								else
								{
									if (tmpe[ary[i]].children != undefined)
									{
										tmpe = tmpe[ary[i]].children;
									}
									else
									{
										break;
									}
								}
							}
							else
							{
								break;
							}
						}
						_cm.init(this, _elements);
						break;
					case 'beforeDisplay':
						if (typeof ids == 'function')
						{
							_bfd[element_id] = ids;
						}
						break;
				}
			}
		}
		else if (typeof elements == 'object')
		{
			return $(this).each(function ()
			{
				_cm.init(this, elements);
			});
		}
	};
})(jQuery);