(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.LandingPage = $.SalesPortal.LandingPage || {};
	$.SalesPortal.LandingPage.DropFolder = function (parameters) {
		var dropFolderId = parameters.containerId;
		var dropFolderContainer = undefined;
		var dropFolderZone = undefined;
		var dropFolderData = undefined;

		this.init = function () {
			dropFolderContainer = $('#drop-folder-container-' + dropFolderId);
			dropFolderZone = $('#drop-folder-' + dropFolderId);
			dropFolderData = $.parseJSON(dropFolderContainer.find('.drop-folder-data').text());
			initDropFolder();
		};

		var initDropFolder = function () {
			Dropzone.autoDiscover = false;
			dropFolderZone.dropzone({
				url: window.BaseUrl + "dropFolder/uploadFile?folderName=" + dropFolderData.folderName,
				dictDefaultMessage: dropFolderData.defaultMessage,
				previewTemplate: "<div></div>",
				sending: function () {
					dropFolderContainer.find('.progress-bar').css({
						width: 0
					});
					dropFolderContainer.find('.progress').show();
				},
				complete: function () {
					dropFolderContainer.find('.progress').hide();
				},
				success: function () {
					loadFiles();
				},
				uploadprogress: function (e, progress) {
					dropFolderContainer.find('.progress-bar').css({
						width: progress + "%"
					});
				}
			});
			loadFiles();
		};

		var loadFiles = function () {
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "dropFolder/getFiles",
				data: {
					folderName: dropFolderData.folderName,
				},
				beforeSend: function () {
					$.SalesPortal.Overlay.show();
				},
				complete: function () {
					$.SalesPortal.Overlay.hide();
				},
				success: function (content) {
					if (content != '')
					{
						dropFolderZone.html(content);
						initFiles();
					}
					else
					{
						dropFolderZone.html('<div class="dz-default dz-message"><span>' + dropFolderData.defaultMessage + '</span></div>')
						dropFolderZone.removeClass('dz-started');
					}
				},
				error: function () {
					dropFolderZone.html('');
				},
				async: true,
				dataType: 'html'
			});
		};

		var initFiles = function () {
			dropFolderZone.find('.file-item .file-name').off('click').on('click', function () {
				var folderName = $(this).closest('.file-item').find('.service-data .folder-name').text();
				var fileName = $(this).closest('.file-item').find('.service-data .file-name').text();
				var form = document.getElementById('form-download-drop-folder-file');
				if (form === null)
				{
					form = document.createElement("form");
					form.setAttribute("id", "form-download-drop-folder-file");
					form.setAttribute("method", "post");
					form.setAttribute("action", window.BaseUrl + 'dropFolder/downloadFile');
					form._submit_function_ = form.submit;

					var hiddenFolderNameField = document.createElement("input");
					hiddenFolderNameField.setAttribute("id", "input-drop-folder-folder-name");
					hiddenFolderNameField.setAttribute("type", "hidden");
					hiddenFolderNameField.setAttribute("name", 'folderName');
					form.appendChild(hiddenFolderNameField);

					var hiddenFileNameField = document.createElement("input");
					hiddenFileNameField.setAttribute("id", "input-drop-folder-file-name");
					hiddenFileNameField.setAttribute("type", "hidden");
					hiddenFileNameField.setAttribute("name", 'fileName');
					form.appendChild(hiddenFileNameField);
					document.body.appendChild(form);
				}
				document.getElementById('input-drop-folder-folder-name').setAttribute("value", folderName);
				document.getElementById('input-drop-folder-file-name').setAttribute("value", fileName);
				form._submit_function_();
			});

			dropFolderZone.find('.draggable').off('dragstart').on('dragstart', function (e) {
				var urlHeader = $(this).data("url-header");
				var url = $(this).data('url');
				if (url !== '')
					e.originalEvent.dataTransfer.setData(urlHeader, url);
			});

			dropFolderZone.find('.file-item .file-delete').off('click').on('click', function () {
				var folderName = $(this).closest('.file-item').find('.service-data .folder-name').text();
				var fileName = $(this).closest('.file-item').find('.service-data .file-name').text();
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "dropFolder/deleteFile",
					data: {
						folderName: folderName,
						fileName: fileName,
					},
					beforeSend: function () {
						$.SalesPortal.Overlay.show();
					},
					complete: function () {
						$.SalesPortal.Overlay.hide();
					},
					success: function () {
						loadFiles();
					},
					error: function () {
					},
					async: true,
					dataType: 'json'
				});
			});
		};
	};
})(jQuery);