(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.RateManager = function ()
	{
		this.init = function (linkId, controlsContainer, rateData)
		{
			if (rateData != undefined)
			{
				var totalRateImage = controlsContainer.find("img.total-rate");
				totalRateImage.hide();

				controlsContainer.hide();
				controlsContainer.find('#user-link-rate').off('rating.change').on('rating.change', function (event, value)
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

				controlsContainer.show();
				controlsContainer.find('#user-link-rate').rating({
					size: 'xs',
					showClear: false,
					showCaption: false
				}).rating('update', rateData.userRate);
			}
		};
	};
})(jQuery);