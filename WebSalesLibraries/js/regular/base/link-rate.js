(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var RateManager = function ()
	{
		this.init = function (linkId, relatedObject, rateData)
		{
			if (rateData != undefined)
			{
				var totalRateImage = relatedObject.find("img.total-rate");
				var userRateContainer = relatedObject.find("#user-link-rate-container");
				totalRateImage.hide();
				userRateContainer.hide();
				userRateContainer.find('#user-link-rate').off('rating.change').on('rating.change', function (event, value)
				{
					$.ajax({
						type: "POST",
						url: window.BaseUrl + "rate/setRate",
						data: {linkId: linkId, value: value},
						success: function (msg)
						{
							if (msg.totalRateImage != '')
							{
								totalRateImage.show();
								totalRateImage.attr('src', msg.totalRateImage);
							}
							else
								totalRateImage.hide();
							userRateContainer.find('#user-link-rate-description').html(msg.userRateDescription);
						},
						error: function ()
						{
						},
						async: true,
						dataType: 'json'
					});
				});
				if (rateData.totalRateImage != '')
				{
					totalRateImage.show();
					totalRateImage.attr('src', rateData.totalRateImage);
				}
				else
					totalRateImage.hide();
				userRateContainer.show();
				userRateContainer.find('#user-link-rate').rating({
					showClear: false,
					showCaption: false
				}).rating('update', rateData.userRate);
				userRateContainer.find('#user-link-rate-description').html(rateData.userRateDescription);
			}
		};
	};
	$.SalesPortal.Rate = new RateManager();
})(jQuery);