(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.RateManager = function ()
	{
		this.init = function (linkData, controlsContainer, rateData, rateChangeCallback)
		{
			if (rateData !== undefined)
			{
				var totalRateImage = controlsContainer.find("img.total-rate");
				totalRateImage.hide();

				controlsContainer.hide();
				controlsContainer.find('.user-link-rate').off('rating.change').on('rating.change', function (event, value)
				{
					$.ajax({
						type: "POST",
						url: window.BaseUrl + "rate/setRate",
						data: {
							linkId: rateData.linkId,
							value: value
						},
						success: function (msg)
						{
							if (msg.totalRateImage !== '')
							{
								totalRateImage.show();
								totalRateImage.attr('src', msg.totalRateImage);
							}
							else
								totalRateImage.hide();

							if (rateChangeCallback !== undefined)
								rateChangeCallback({
									userRate: value,
									totalRateImage: msg.totalRateImage
								});
						},
						error: function ()
						{
						},
						async: true,
						dataType: 'json'
					});

					$.SalesPortal.LogHelper.write({
						type: 'Link',
						subType: 'Rate',
						linkId: linkData.id,
						data: {
							name: linkData.name,
							file: linkData.file,
							'originalFormat': linkData.format,
							'rate': value
						}
					});
				});

				if (rateData.totalRateImage !== '')
				{
					totalRateImage.show();
					totalRateImage.attr('src', rateData.totalRateImage);
				}
				else
					totalRateImage.hide();

				controlsContainer.show();
				controlsContainer.find('.user-link-rate').rating({
					size: 'xs',
					showClear: false,
					showCaption: false
				}).rating('update', rateData.userRate);
			}
		};
	};
})(jQuery);