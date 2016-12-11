(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	var DownloadFolderHelper = function ()
	{
		var filesSelectContent = undefined;

		this.run = function (linkData)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "preview/prepareDownloadFolder",
				data: {
					linkId: linkData.linkId
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					filesSelectContent = $(msg);

					var folderName = filesSelectContent.find('.service-data .folder-name').text();

					var formLogger = new $.SalesPortal.FormLogger();
					formLogger.init({
						logObject: linkData,
						formContent: filesSelectContent
					});

					updateTotalFileInfo();

					filesSelectContent.find('.zip-folder-item:checked').on('change', function ()
					{
						updateTotalFileInfo();
					});
					filesSelectContent.find('#zip-folder-select-all').on('click.preview', function ()
					{
						filesSelectContent.find('.zip-folder-item').prop('checked', true);
						updateTotalFileInfo();
					});
					filesSelectContent.find('#zip-folder-clear-all').on('click.preview', function ()
					{
						filesSelectContent.find('.zip-folder-item').prop('checked', false);
						updateTotalFileInfo();
					});

					filesSelectContent.find('.btn.accept-button').on('click.preview', function ()
					{
						var selectedFiles = [];
						filesSelectContent.find('.zip-folder-item:checked').each(function ()
						{
							selectedFiles.push($.parseJSON(atob($(this).val())));
						});

						$.fancybox.close();

						var form = document.getElementById('form-download-folder');
						if (form == null)
						{
							form = document.createElement("form");
							form.setAttribute("id", "form-download-folder");
							form.setAttribute("method", "post");
							form.setAttribute("action", 'preview/downloadFolder');
							form._submit_function_ = form.submit;

							var hiddenField = document.createElement("input");
							hiddenField.setAttribute("id", "input-folder-name");
							hiddenField.setAttribute("type", "hidden");
							hiddenField.setAttribute("name", 'folderName');
							form.appendChild(hiddenField);

							hiddenField = document.createElement("input");
							hiddenField.setAttribute("id", "input-folder-files");
							hiddenField.setAttribute("type", "hidden");
							hiddenField.setAttribute("name", 'folderFiles');
							form.appendChild(hiddenField);

							document.body.appendChild(form);
						}

						document.getElementById('input-folder-name').setAttribute("value", folderName);
						document.getElementById('input-folder-files').setAttribute("value", $.toJSON(selectedFiles));

						form._submit_function_();
					});
					filesSelectContent.find('.btn.cancel-button').on('click.preview', function ()
					{
						$.fancybox.close();
					});
					$.fancybox({
						content: filesSelectContent,
						title: "Download Zip",
						width: 430,
						scrolling: 'no',
						autoSize: false,
						autoHeight: true,
						openEffect: 'none',
						closeEffect: 'none'
					});
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		var updateTotalFileInfo = function ()
		{
			var selectedFiles = filesSelectContent.find('.zip-folder-item:checked');

			var totalFiles = selectedFiles.length;
			filesSelectContent.find('.total-files-count').html(totalFiles);

			var totalSize = 0;
			selectedFiles.each(function ()
			{
				var fileInfo = $.parseJSON(atob($(this).val()));
				totalSize += parseInt(fileInfo.size);
			});

			var measure = '';
			if (totalSize < 1073741824)
			{
				if (totalSize < 1048576)
					measure = 'kb';
				else
					measure = 'mb';
			}
			else
				measure = 'gb';
			switch (measure)
			{
				case "kb":
					totalSize = totalSize * 0.0009765625; // bytes to KB
					break;
				case "mb":
					totalSize = Math.round(totalSize * 0.0009765625) * 0.0009765625; // bytes to MB
					break;
				case "gb":
					totalSize = Math.round(Math.round(totalSize * 0.0009765625) * 0.0009765625) * 0.0009765625; // bytes to GB
					break;
			}
			filesSelectContent.find('.total-files-size').html(Math.round(totalSize) + measure);
		};
	};
	$.SalesPortal.DownloadFolderHelper = new DownloadFolderHelper();
})(jQuery);
