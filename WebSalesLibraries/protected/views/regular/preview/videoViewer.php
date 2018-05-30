<?
	/**
	 * @var $data VideoPreviewData
	 * */

	$imageUrlPrefix = Yii::app()->getBaseUrl(true);
	$isEOBrowser = Yii::app()->browser->getBrowser() == Browser::BROWSER_EO;
	$fullScreenSizeMode = Yii::app()->browser->isMobile() ? 'mobile' : 'regular';
	$linkBundleInfo = $data->getLinkBundleInfo();
?>
<div class="link-viewer<? echo $isEOBrowser ? ' eo' : ''; ?><? if ($data->config->enableLogging): ?> logger-form<? endif; ?>"
     data-log-group="Link" data-log-action="Preview Activity">
	<?
		$headerGapSize = 6;
		if (!Yii::app()->params['one_drive_links']['enabled'] || empty($data->oneDriveUrl))
			$headerGapSize += 4;
	?>
    <div class="row row-buttons tab-above-header" id="tab-above-header-preview">
        <div class="col col-xs-<? echo $headerGapSize; ?>"></div>
		<? if (Yii::app()->params['one_drive_links']['enabled'] && !empty($data->oneDriveUrl)): ?>
            <div class="col col-xs-4 text-center">
                <div class="image-button" title="one drive link">
                    <span class="text-item dropdown">
                         <a href="#" data-toggle="dropdown" class="dropdown-toggle">
                            <img src="<? echo sprintf('%s/images/preview/gallery/button-open-one-drive.png', $imageUrlPrefix); ?>">
                         </a>
                        <ul class="dropdown-menu" style="left: 0;">
                            <li>
                                <a href="<? echo $data->oneDriveUrl; ?>"
                                   class="log-action log-action one-drive-link-open"
                                   data-log-action="Open OneDrive Link" target="_blank" title="open link">Open Link</a>
                            </li>
                            <li>
                                <a href="<? echo $data->oneDriveUrl; ?>" class="log-action one-drive-link-copy" data-log-action="Copy OneDrive Link"
                                   title="copy link">Copy Link URL</a>
                            </li>
                            <li>
                                <a href="#" class="log-action one-drive-link-email"
                                   data-log-action="Email OneDrive Link" title="email link">Email Link</a>
                            </li>
                        </ul>
                    </span>
                </div>
            </div>
		<? endif; ?>
        <div class="col col-xs-1 text-center">
            <div class="image-button log-action open-video-modal" data-log-action="Preview Modal" title="Zoom">
                <img src="<? echo sprintf('%s/images/preview/gallery/button-video-modal.png', $imageUrlPrefix); ?>">
            </div>
        </div>
        <div class="col col-xs-1 text-center">
            <div class="image-button log-action open-video-fullscreen-<? echo $fullScreenSizeMode; ?>"
                 data-log-action="Preview Fullscreen" title="view fullscreen">
                <img
                        src="<? echo sprintf('%s/images/preview/gallery/button-video-fullscreen.png', $imageUrlPrefix); ?>">
            </div>
        </div>
    </div>
	<? if ($data->config->allowSave): ?>
        <div class="row tab-above-header" id="tab-above-header-save">
            <div class="col col-xs-12 text-left">
                <div class="text-label">
                    <img class="text-item file-logo" src="<? echo $data->fileLogo; ?>" style="height: 48px;">
                    <span class="text-item file-name"><? echo $data->fileName; ?></span>
                </div>
            </div>
        </div>
	<? endif; ?>
	<? if ($data->config->allowEmail): ?>
        <div class="row tab-above-header" id="tab-above-header-email">
            <span class="header-text">Send this link to your client…</span>
        </div>
	<? endif; ?>
    <ul class="nav nav-tabs" role="tablist" id="link-viewer-body-tabs">
        <li>
            <a class="log-action" href="#link-viewer-tab-preview" role="tab" data-toggle="tab">Preview</a>
        </li>
		<? if ($data->config->allowSave): ?>
            <li>
                <a class="log-action" href="#link-viewer-tab-save" role="tab" data-toggle="tab">File</a>
            </li>
		<? endif; ?>
		<? if ($data->config->allowEmail): ?>
            <li>
                <a class="log-action" href="#link-viewer-tab-email" role="tab" data-toggle="tab" data-log-action="Add to QS">Email</a>
            </li>
		<? endif; ?>
    </ul>
    <div class="tab-content">
        <div role="tabpanel" class="tab-pane" id="link-viewer-tab-preview">
            <div class="row preview-gallery">
                <div class="col col-xs-12 text-center">
                    <video id="video-player"
                           class="<? if ($isEOBrowser): ?> eo<? endif; ?> log-action"
                           controls poster="<? echo $data->thumbImageSrc; ?>"
                           height="305" width="750">
                        <source src="<? echo $data->mp4Src->href; ?>" type="video/mp4">
                    </video>
                </div>
            </div>
            <div class="row row-buttons gallery-control-buttons">
                <div class="col col-xs-8 text-left">
					<? if ($data->config->enableRating): ?>
                        <div class="user-link-rate-container">
                            <img class="total-rate" src="" style="height:16px"/>
                            <input name="user-link-rate" class="user-link-rate rating">
                        </div>
					<? endif; ?>
                </div>
				<?
					$footerGapSize = 0;
					if (!isset($data->quickLinkUrl))
						$footerGapSize++;
					if (!$data->config->allowDownload)
						$footerGapSize++;
					if (!$data->config->allowAddToFavorites)
						$footerGapSize++;
					if (!$data->config->allowAddToQuickSite)
						$footerGapSize++;
				?>
                <div class="col col-xs-<? echo $footerGapSize; ?>"></div>
				<? if (isset($data->quickLinkUrl)): ?>
                    <div class="col col-xs-1 text-center">
                        <div class="image-button log-action open-quick-link" data-log-action="Open Quick Link"
                             title="<? echo $data->quickLinkTitle; ?>">
							<span class="text-item">
								<img src="<? echo $data->quickLinkLogo; ?>">
							</span>
                        </div>
                    </div>
				<? endif; ?>
				<? if ($data->config->allowDownload): ?>
                    <div class="col col-xs-1 text-center">
						<? if ($data->config->allowDownload): ?>
                            <div class="image-button" title="download">
                                <span class="text-item dropup">
                                     <a href="#" data-toggle="dropdown" class="dropdown-toggle">
                                        <img src="<? echo sprintf('%s/images/preview/gallery/button-download.png', $imageUrlPrefix); ?>">
                                     </a>
                                    <ul class="dropdown-menu">
                                        <li>
                                            <a href="#" class="log-action download-mp4-file"
                                               data-log-action="Download File">
                                                Download .mp4 <span
                                                        class="file-size">(<? echo $data->mp4Src->size; ?></span>)
                                            </a>
                                        </li>
	                                    <? if ($data->downloadSource): ?>
                                            <li role="separator" class="divider"></li>
                                            <li>
                                                <a href="#" class="log-action download-original-file"
                                                   data-log-action="Download File">
                                                    Download .<? echo $data->fileExtension; ?> <span
                                                            class="file-size">(<? echo $data->fileSize; ?></span>)
                                                </a>
                                            </li>
	                                    <? endif; ?>
	                                    <? if (isset($linkBundleInfo) && count($linkBundleInfo->downloadInfo) > 1): ?>
                                            <li role="separator" class="divider"></li>
                                            <li>
                                                <a href="#" class="log-action download-link-bundle"
                                                   data-log-action="Download Link Bundle">
                                                    Download all <? echo count($linkBundleInfo->downloadInfo); ?> files (<? echo FileInfo::formatFileSize(FileDownloadInfo::getTotalSize($linkBundleInfo->downloadInfo)); ?>
                                                    )
                                                </a>
                                            </li>
	                                    <? endif; ?>
                                    </ul>
                                </span>
                            </div>
						<? endif; ?>
                    </div>
				<? endif; ?>
				<? if ($data->config->allowAddToFavorites): ?>
                    <div class="col col-xs-1 text-center">
                        <div class="image-button log-action add-favorites" data-log-action="Add to Favorites"
                             title="save favorite">
							<span class="text-item">
								<img
                                        src="<? echo sprintf('%s/images/preview/gallery/button-favorites.png', $imageUrlPrefix); ?>">
							</span>
                        </div>
                    </div>
				<? endif; ?>
				<? if ($data->config->allowAddToQuickSite): ?>
                    <div class="col col-xs-1 text-center">
                        <div class="image-button log-action add-quicksite" data-log-action="Add to QS"
                             title="add to quickSITE">
							<span class="text-item">
								<img
                                        src="<? echo sprintf('%s/images/preview/gallery/button-quicksite.png', $imageUrlPrefix); ?>">
							</span>
                        </div>
                    </div>
				<? endif; ?>
            </div>
        </div>
		<? if ($data->config->allowSave): ?>
            <div role="tabpanel" class="tab-pane" id="link-viewer-tab-save">
				<? echo $this->renderPartial('save', array('data' => $data), true); ?>
            </div>
		<? endif; ?>
		<? if ($data->config->allowEmail): ?>
            <div role="tabpanel" class="tab-pane link-viewer-tab-email" id="link-viewer-tab-email">
				<? echo $this->renderPartial('email', array('data' => $data), true); ?>
            </div>
		<? endif; ?>
    </div>
</div>

