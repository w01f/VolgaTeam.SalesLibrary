// =======================================================================================
// 
// Metro Tooltip v1.1
//
// Author: Klerith
// Page: http://codecanyon.net/user/klerith
// Email: fernando.herrera85@gmail.com BUT, first send me a message through codecanyon page.
//        That's because some people stole the code and ask me support when they are not a customer :(
//
// =======================================================================================


(function ($)
{

	var ToolCounter = 0;
	var ParentCounter = 0;
	var tooltipTimer = undefined;

	$.MetroTooltipInit = function (settings)
	{

		settings = $.extend({
			title: undefined,
			content: undefined,
			image: undefined,
			autohide: true,
			persist: 0,
			helpicon: 1,
			color: "#7514b5",
			animation: "fadeIn",
			position: "right",

		}, settings);


		$(".mtTool").each(function ()
		{
			if ($(this).attr("mtTool") == undefined)
			{
				ParentCounter += 1;
				$(this).attr("mtTool", "mtTool" + ParentCounter);
			}

			if ($(this).attr("mthelpicon") == "0")
			{

			}
			else
			{
				if (settings.helpicon == 1)
					$(this).addClass("mtHelpIcon");
			}
		});

		var showTooltip = function (target)
		{
			var ThisElement = target;
			// Mark the element with an open tooltip

			var mtTitle = ThisElement.attr("mttitle");
			var mtContent = ThisElement.attr("mtcontent");
			if (mtTitle == undefined && mtContent == undefined)
				return false;

			if (ThisElement.attr("toolopen") == undefined)
			{
				ThisElement.attr("toolopen", "1");
				ToolCounter += 1;
				ThisElement.attr("closeToolID", "mtool" + ToolCounter);

			}
			else
			{
				return false;
			}


			var ParentLauncher = ThisElement.attr("mtTool");

			var content = "";


			var ID = "mtool" + ToolCounter;
			var mtImg = ThisElement.attr("mtImg");
			var mtAnimation = ThisElement.attr("mtanimation");

			var mtPosition = ThisElement.attr("mtposition");
			if (mtPosition == undefined)
				mtPosition = settings.position;

			var BackColor = ThisElement.attr("mtcolor");
			if (BackColor == undefined)
				BackColor = settings.color;


			if (mtAnimation == undefined)
				mtAnimation = settings.animation;

			var mtPersist = ThisElement.attr("mtpersist");
			if (mtPersist == undefined)
				mtPersist = settings.persist;

			var HelpIcon = ThisElement.attr("mthelpicon");
			if (HelpIcon == undefined)
				mthelpicon = settings.helpicon;

			content += '<div id="' + ID + '" class="mtContent animated ' + mtAnimation + '" parentlauncher="' + ParentLauncher + '">';

			if (mtTitle != undefined)
				content += '<span class="mtContentTitle">' + mtTitle + '</span><br/>';

			if (mtPersist == 1)
				content += '<span class="mtCloseCross" close="' + ID + '">X</span>';

			if (mtContent != undefined)
			{
				content += '<span class="mtContentBody">';
				content += mtContent;
				content += '</span>';
			}

			if (mtImg != undefined)
				content += '<img class="mtToolImage" src="' + mtImg + '">';

			content += '</div>';


			$("body").append(content);

			$("#" + ID).css("background-color", BackColor);

			if (navigator.userAgent.match(/msie/i))
			{

				var ThisTool = $("#" + ID);
				ThisTool.removeClass("animated " + mtAnimation);
				ThisTool.css("opacity", "0")
				ThisTool.animate({
					opacity: 1
				}, 300);
			}

			//Positioning
			RePosition(ID, mtPosition, ThisElement, mtPersist);

			tooltipTimer = undefined;
		};


		$(document).on("mouseover", ".mtTool", function (event)
		{
			if (tooltipTimer != undefined)
				clearTimeout(tooltipTimer);

			var target = $(this);
			tooltipTimer = setTimeout(function ()
			{
				showTooltip(target)
			}, 800);
		});

		function RePosition(ID, Position, ParentLauncher, mtPersist)
		{
			var Top = ParentLauncher.offset().top;
			var Left = ParentLauncher.offset().left;
			var WindowWidth = $(window).width();

			Position = Position.toLowerCase();

			if (mtPersist == 1)
			{
				$("#" + ID).css({
					width: "+=15"
				});
			}


			switch (Position)
			{

				case"top":

					var ParentMiddle = ParentLauncher.width() / 2;
					var ThisToolWidthMiddle = $("#" + ID).width() / 2;

					var ToolHeigth = $("#" + ID).height();
					$("#" + ID).css({
						top: Top - 30 - ToolHeigth,
						left: Left + ParentMiddle - ThisToolWidthMiddle - 10,
						width: "+=15"
					});

					break;

				case"bottom":
					var ParentMiddle = ParentLauncher.width() / 2;
					var ThisToolWidthMiddle = $("#" + ID).width() / 2;

					$("#" + ID).css({
						top: Top + ParentLauncher.height() + 10,
						left: Left + ParentMiddle - ThisToolWidthMiddle - 10,
						width: "+=15"
					});

					var PixelPos = $("#" + ID).offset().left + 20 + $("#" + ID).width();

					if (PixelPos > WindowWidth)
					{

						var Diff = (PixelPos - WindowWidth) + 15;

						$("#" + ID).css({
							left: "-=" + Diff
						});
					}

					break;

				case"right":
					$("#" + ID).css({
						top: Top,
						left: Left + ParentLauncher.width() + 10,
					});

					var PixelPos = $("#" + ID).offset().left + 20 + $("#" + ID).width();

					if (PixelPos > WindowWidth)
					{
						var Diff = (PixelPos - WindowWidth) + 15;
						$("#" + ID).css({
							top: Top + ParentLauncher.height() + 10,
							left: "-=" + Diff
						});
					}


					break;

				case"left":
					var ToolWidth = $("#" + ID).width();

					$("#" + ID).css({
						top: Top,
						left: Left - ToolWidth - 30,
					});
					break;
			}

			//Left position correction
			var Left = $("#" + ID).offset().left;
			if (Left < 0)
			{
				Left = Left * -1;

				$("#" + ID).css({
					left: "5px",
				})
			}

			if (Position == "left")
			{

				var PixelPos = $("#" + ID).offset().left + $("#" + ID).width();
				if (PixelPos > ParentLauncher.offset().left)
				{
					//We need to set the tooltip bottom
					RePosition(ID, "bottom", ParentLauncher, mtPersist);
				}
			}

			//Check the window width
			var ToolWidth = $("#" + ID).width();
			var WindowWidth = $(window).width();

			if (ToolWidth > WindowWidth)
			{
				$("#" + ID).css({
					width: (WindowWidth - 20) + "px",
				})
			}


		}

		// Close Tool with X
		$(document).on("click", ".mtCloseCross", function ()
		{

			var IDtoClose = $(this).attr("close");
			var ToolTip = $("#" + IDtoClose);
			var Parent = ToolTip.attr("parentlauncher");

			// Release the Tooltip for a new one
			ReleaseToolTip(Parent);
			$("#" + IDtoClose).removeClass().addClass("mtContent").fadeOut(300, function ()
			{
				$(this).remove();
			})
		});

		// Close oun Mouse Leave
		$(document).on("mouseleave", ".mtTool", function ()
		{
			if (tooltipTimer != undefined)
				clearTimeout(tooltipTimer);
			var CloseToolID = $(this).attr("closeToolID");
			var Parent = $(this).attr("mtTool");
			var Persist = $(this).attr("mtpersist");

			if (Persist == 1)
			{
				return false;
			}

			// Release the Tooltip for a new one
			ReleaseToolTip(Parent);
			$("#" + CloseToolID).removeClass().addClass("mtContent").fadeOut(300, function ()
			{
				$(this).remove();
			})
		});

		var Timer;

		$(window).on("resize", function ()
		{

			clearTimeout(Timer);
			Timer = setTimeout(function ()
			{
				$(".mtContent").each(function ()
				{

					var ThisTool = $(this);
					var ParentLauncher = ThisTool.attr("parentlauncher");
					ReleaseToolTip(ParentLauncher);
					ThisTool.removeClass().addClass("mtContent").fadeOut(300, function ()
					{
						$(this).remove();
					});

				});

			}, 100);
		});


		// On Resize Persisting Tooltips
		function OnResizePersist(ID, Position, ParentElement)
		{

			ThisTool = $("#" + ID);
			ThisTool.clearQueue();

			var LeftParentPoint = ParentElement.offset().left;
			var ParentWidth = ParentElement.width();

			var ThisToolLeft = ThisTool.offset().left;
			var ThisToolWidth = ThisTool.width();
			var WindowWidth = $(window).width();

			var Top = ParentElement.offset().top;
			var Left = ParentElement.offset().left;

			switch (Position)
			{
				case "right":

					if ((ThisToolLeft + ThisToolWidth) > WindowWidth)
					{
						var ParentMiddle = ParentElement.width() / 2;
						var ThisToolWidthMiddle = ThisTool.width() / 2;

						$("#" + ID).css({
							top: Top + ParentElement.height() + 10,
							left: Left + ParentMiddle - ThisToolWidthMiddle - 10,
							width: "+=15"
						});
					}
					else
					{
						ThisTool.animate({
							top: ParentElement.offset().top,
							left: ParentElement.offset().left + ParentElement.width() + 10,
						}, 300);
					}


					break;


			}


		}

	}

	function ReleaseToolTip(Attr)
	{

		$(".mtTool").each(function ()
		{

			var ThisElement = $(this);
			var ElementAttr = ThisElement.attr("mtTool");

			if (ElementAttr == Attr)
			{
				ThisElement.removeAttr("toolopen");
				ThisElement.removeAttr("closeToolID");
				return false;
			}
		});

	}

	$.mtReInit = function (settings)
	{

		settings = $.extend({
			helpicon: 1,
		}, settings);

		$(".mtTool").each(function ()
		{
			if ($(this).attr("mtTool") == undefined)
			{
				ParentCounter += 1;
				$(this).attr("mtTool", "mtTool" + ParentCounter);
				if ($(this).attr("mthelpicon") == "0")
				{
				}
				else
				{
					if (settings.helpicon == 1)
						$(this).addClass("mtHelpIcon");
				}
			}
		});
	}

})(jQuery);