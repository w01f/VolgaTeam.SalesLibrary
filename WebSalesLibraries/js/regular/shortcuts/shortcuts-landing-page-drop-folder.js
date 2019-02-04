(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.LandingPage = $.SalesPortal.LandingPage || {};
	$.SalesPortal.LandingPage.DropFolder = function (parameters) {
		let dropFolderId = parameters.containerId;
		let dropFolderContainer = undefined;
		let dropFolderZone = undefined;
		let dropZoneObject = undefined;
		let dropFolderData = undefined;

		this.init = function () {
			dropFolderContainer = $('#drop-folder-container-' + dropFolderId);
			dropFolderZone = $('#drop-folder-' + dropFolderId);
			dropFolderData = $.parseJSON(dropFolderContainer.find('.drop-folder-data').text());
			initDropFolder();
		};

		let initDropFolder = function () {
			Dropzone.autoDiscover = false;
			let abortUploading = false;
			dropFolderZone.dropzone(
				{
					url: window.BaseUrl + "dropFolder/uploadFile?folderName=" + dropFolderData.folderName,
					dictDefaultMessage: dropFolderData.defaultMessage,
					previewTemplate: "<div></div>",
					uploadMultiple: true,
					parallelUploads: 1,
					clickable: dropFolderData.uploadOnClick == true,
					init: function () {
						dropZoneObject = this;
					},
					sendingmultiple: function (files, xhr, formData) {
						if (abortUploading)
						{
							$.each(files, function (index, value) {
								dropZoneObject.removeFile(value);
							});
							abortUploading = false;
						}
						else
						{
							dropFolderContainer.find('.progress-bar').css({
								width: 0
							});
							dropFolderContainer.find('.progress').show();
						}
					},
					accept: function (file, done) {
						if (file.size > parseInt(dropFolderData.maxFileSize) * 1024 * 1024)
						{
							abortUploading = true;
							let modalDialog = new $.SalesPortal.ModalDialog({
								title: 'File Too BIG?',
								description: dropFolderData.maxFileSizeExcessMessage,
								buttons: [
									{
										tag: 'ok',
										title: 'Close',
										width: 160,
										clickHandler: function () {
											modalDialog.close();
										}
									}
								]
							});
							modalDialog.show();
							return false;
						}
						else if (dropFolderData.allowedFileTypes.length > 0)
						{
							let acceptedFile = false;
							$.each(dropFolderData.allowedFileTypes, function (index, value) {
								acceptedFile = acceptedFile || file.name.includes("." + value);
							});
							if (!acceptedFile)
							{
								abortUploading = true;
								if (dropFolderData.fileTypeDiscardMessage !== '')
								{
									let modalDialog = new $.SalesPortal.ModalDialog({
										title: 'File type type is not authorized',
										description: dropFolderData.fileTypeDiscardMessage,
										buttons: [
											{
												tag: 'ok',
												title: 'Close',
												width: 160,
												clickHandler: function () {
													modalDialog.close();
												}
											}
										]
									});
									modalDialog.show();
								}
								return false;
							}
						}
						done();
					},
					complete: function () {
						dropFolderContainer.find('.progress').hide();
					},
					uploadprogress: function (file, progress) {
						dropFolderContainer.find('.progress-bar').css({
							width: progress + "%"
						});
						if (progress > 70)
							dropFolderContainer.find('.progress-text').css({
								color: '#ffffff'
							});
						else
							dropFolderContainer.find('.progress-text').css({
								color: '#000000'
							});
						dropFolderContainer.find('.file-name').text(file.name);
						dropFolderContainer.find('.progress-percent').text(Math.round(progress));
					},
					queuecomplete: function () {
						loadFiles();
					},
				});
			loadFiles();
		};

		let loadFiles = function () {
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
						dropFolderZone.html('<div class="dz-default dz-message"><span>' + dropFolderData.defaultMessage + '</span></div>');
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

		let initFiles = function () {
			dropFolderZone.find('.file-item .file-name').off('click').on('click', function () {
				let folderName = $(this).closest('.file-item').find('.service-data .folder-name').text();
				let fileName = $(this).closest('.file-item').find('.service-data .file-name').text();
				let form = document.getElementById('form-download-drop-folder-file');
				if (form === null)
				{
					form = document.createElement("form");
					form.setAttribute("id", "form-download-drop-folder-file");
					form.setAttribute("method", "post");
					form.setAttribute("action", window.BaseUrl + 'dropFolder/downloadFile');
					form._submit_function_ = form.submit;

					let hiddenFolderNameField = document.createElement("input");
					hiddenFolderNameField.setAttribute("id", "input-drop-folder-folder-name");
					hiddenFolderNameField.setAttribute("type", "hidden");
					hiddenFolderNameField.setAttribute("name", 'folderName');
					form.appendChild(hiddenFolderNameField);

					let hiddenFileNameField = document.createElement("input");
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
				let urlHeader = $(this).data("url-header");
				let url = $(this).data('url');
				if (url !== '')
					e.originalEvent.dataTransfer.setData(urlHeader, url);
			});

			dropFolderZone.find('.file-item .file-delete').off('click').on('click', function () {
				let folderName = $(this).closest('.file-item').find('.service-data .folder-name').text();
				let fileName = $(this).closest('.file-item').find('.service-data .file-name').text();
				let modalDialog = new $.SalesPortal.ModalDialog({
					title: 'Delete this File?',
					description: "Are you sure you want to DELETE this file from the site?",
					buttons: [
						{
							tag: 'ok',
							title: 'Yes',
							width: 160,
							clickHandler: function () {
								modalDialog.close();

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
							}
						},
						{
							tag: 'cancel',
							title: 'Cancel',
							width: 160,
							clickHandler: function () {
								modalDialog.close();
							}
						}
					]
				});
				modalDialog.show();
			});
		};
	};
})(jQuery);