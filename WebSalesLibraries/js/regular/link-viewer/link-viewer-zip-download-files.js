(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	var ZipDownloadFilesHelper = function ()
	{
		this.processFolderLink = function (linkData)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "preview/prepareDownloadFolderLink",
				data: {
					linkId: linkData.linkId
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show();
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (formContent)
				{
					var filesSelectContent = $(formContent);

					var formLogger = new $.SalesPortal.FormLogger();
					formLogger.init({
						logObject: linkData,
						formContent: filesSelectContent
					});

					initFileSelectorForm(filesSelectContent);
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		this.processLinkBundle = function (linkBundleId)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "preview/prepareDownloadLinkBundle",
				data: {
					linkId: linkBundleId
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show();
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (formContent)
				{
					var filesSelectContent = $(formContent);
					initFileSelectorForm(filesSelectContent);
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		this.processLibraryFolder = function (folderId)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "preview/prepareDownloadLibraryFolder",
				data: {
					folderId: folderId
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show();
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (formContent)
				{
					var filesSelectContent = $(formContent);
					initFileSelectorForm(filesSelectContent);
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		var initFileSelectorForm = function (filesSelectContent)
		{
			var folderName = filesSelectContent.find('.service-data .zip-name').text();

			updateTotalFileInfo(filesSelectContent);

			filesSelectContent.find('.zip-files-item:checked').on('change', function ()
			{
				updateTotalFileInfo(filesSelectContent);
			});
			filesSelectContent.find('#zip-files-select-all').on('click.preview', function ()
			{
				filesSelectContent.find('.zip-files-item').prop('checked', true);
				updateTotalFileInfo(filesSelectContent);
			});
			filesSelectContent.find('#zip-files-clear-all').on('click.preview', function ()
			{
				filesSelectContent.find('.zip-files-item').prop('checked', false);
				updateTotalFileInfo(filesSelectContent);
			});

			filesSelectContent.find('.btn.accept-button').on('click.preview', function ()
			{
				var selectedFiles = [];
				filesSelectContent.find('.zip-files-item:checked').each(function ()
				{
					selectedFiles.push($.parseJSON(atob($(this).val().trim())));
				});

				$.fancybox.close();

				var form = document.getElementById('form-zip-download-files');
				if (form === null)
				{
					form = document.createElement("form");
					form.setAttribute("id", "form-zip-download-files");
					form.setAttribute("method", "post");
					form.setAttribute("action", window.BaseUrl + 'preview/zipAndDownloadFiles');
					form._submit_function_ = form.submit;

					var hiddenField = document.createElement("input");
					hiddenField.setAttribute("id", "input-zip-name");
					hiddenField.setAttribute("type", "hidden");
					hiddenField.setAttribute("name", 'zipName');
					form.appendChild(hiddenField);

					hiddenField = document.createElement("input");
					hiddenField.setAttribute("id", "input-files");
					hiddenField.setAttribute("type", "hidden");
					hiddenField.setAttribute("name", 'files');
					form.appendChild(hiddenField);

					document.body.appendChild(form);
				}

				document.getElementById('input-zip-name').setAttribute("value", folderName);
				document.getElementById('input-files').setAttribute("value", $.toJSON(selectedFiles));

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
		};

		var updateTotalFileInfo = function (filesSelectContent)
		{
			var selectedFiles = filesSelectContent.find('.zip-files-item:checked');

			var totalFiles = selectedFiles.length;
			filesSelectContent.find('.total-files-count').html(totalFiles);

			var totalSize = 0;
			selectedFiles.each(function ()
			{
				var fileInfo = $.parseJSON(atob($(this).val().trim()));
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
	$.SalesPortal.ZipDownloadFilesHelper = new ZipDownloadFilesHelper();
})(jQuery);
