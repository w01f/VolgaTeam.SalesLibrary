<?
	/**
	 * @var $data PdfPreviewData
	 * */

	$imageUrlPrefix = Yii::app()->getBaseUrl(true);
?>
<div class="link-viewer<? if ($data->config->enableLogging): ?> logger-form<? endif; ?>" data-log-group="Link"
     data-log-action="Preview Activity">
	<?
		$headerGapSize = 10;
		if (!$data->config->allowPdf)
			$headerGapSize++;
	?>
	<div class="row row-buttons tab-above-header" id="tab-above-header-preview">
		<div class="col col-xs-<? echo $headerGapSize; ?>">
		</div>
		<? if ($data->config->allowPdf): ?>
			<div class="col col-xs-1 text-center">
				<div class="image-button log-action open-pdf" data-log-action="Open PDF" title="view pdf">
					<img
						src="<? echo sprintf('%s/images/preview/gallery/button-open-pdf.png', $imageUrlPrefix); ?>">
				</div>
			</div>
		<? endif; ?>
		<div class="col col-xs-1 text-center">
			<div class="image-button log-action open-gallery-modal" data-log-action="Preview Modal" title="Zoom">
				<img src="<? echo sprintf('%s/images/preview/gallery/button-open-modal.png', $imageUrlPrefix); ?>">
			</div>
		</div>
	</div>
	<? if ($data->config->allowSave): ?>
		<div class="row tab-above-header" id="tab-above-header-save">
			<div class="col col-xs-12 text-left">
				<div class="text-label">
					<img class="text-item file-logo" src="<? echo $data->fileLogo;?>" style="height: 48px;">
					<span class="text-item file-name"><? echo $data->fileName;?></span>
				</div>
			</div>
		</div>
	<? endif; ?>
	<? if ($data->config->allowEmail): ?>
		<div class="row tab-above-header" id="tab-above-header-email-public">
			<span class="header-text">Send this link to your client…</span>
		</div>
		<div class="row tab-above-header" id="tab-above-header-email-protected">
			<span class="header-text">Email a secure link (not for clients)</span>
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
				<a class="log-action" href="#link-viewer-tab-email-public" role="tab" data-toggle="tab"
				   data-log-action="Add to QS">Public EMAIL</a>
			</li>
			<li>
				<a class="log-action" href="#link-viewer-tab-email-protected" role="tab" data-toggle="tab"
				   data-log-action="Add to QS">Secure EMAIL</a>
			</li>
		<? endif; ?>
	</ul>
	<div class="tab-content">
		<div role="tabpanel" class="tab-pane" id="link-viewer-tab-preview">
			<div class="row preview-gallery">
				<div class="col col-xs-1 text-center">
					<? if (!$data->singlePage): ?>
						<div class="image-button nav-image-button log-action move-previous"
						     data-log-action="Preview Page">
							<img src="<? echo sprintf('%s/images/preview/gallery/prev-slide.png', $imageUrlPrefix); ?>">
						</div>
					<? endif; ?>
				</div>
				<div class="col col-xs-10 text-center preview-image-container">
					<img class="page-preview-image log-action" style="display: none;" src="//:0">
				</div>
				<div class="col col-xs-1 text-center">
					<? if (!$data->singlePage): ?>
						<div class="image-button nav-image-button log-action move-next" data-log-action="Preview Page">
							<img src="<? echo sprintf('%s/images/preview/gallery/next-slide.png', $imageUrlPrefix); ?>">
						</div>
					<? endif; ?>
				</div>
			</div>
			<div class="row row-buttons gallery-control-buttons">
				<div class="col col-xs-4 text-left">
					<? if ($data->config->enableRating): ?>
						<div id="user-link-rate-container">
							<img class="total-rate" src="" style="height:16px"/>
							<label for="user-link-rate" class="ui-hide-label"></label><input id="user-link-rate"
							                                                                 class="rating">
						</div>
					<? endif; ?>
				</div>
				<div class="col col-xs-4 text-center">
					<? if (!$data->singlePage): ?>
						<div class="dropdown-button">
							<div class="dropdown-container">
								<label class="ui-hide-label" for="image-viewer-slide-selector"></label>
								<select class="selectpicker dropup bootstrapped log-action"
								        id="image-viewer-slide-selector"
								        data-log-action="Preview Page"></select>
							</div>
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
                        <div class="image-button" title="download">
                            <span class="text-item dropup">
                                 <a href="#" data-toggle="dropdown" class="dropdown-toggle">
                                    <img src="<? echo sprintf('%s/images/preview/gallery/button-download.png', $imageUrlPrefix); ?>">
                                 </a>
                                <ul class="dropdown-menu">
                                    <li>
                                        <a href="#" class="log-action download-file"
                                           data-log-action="Download File">
                                            Download file <span class="file-size"></span>
                                        </a>
                                    </li>
                                </ul>
                            </span>
                        </div>
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
			<div role="tabpanel" class="tab-pane link-viewer-tab-email" id="link-viewer-tab-email-public">
				<? echo $this->renderPartial('email', array('data' => $data, 'isProtected' => false), true); ?>
			</div>
			<div role="tabpanel" class="tab-pane link-viewer-tab-email" id="link-viewer-tab-email-protected">
				<? echo $this->renderPartial('email', array('data' => $data, 'isProtected' => true), true); ?>
			</div>
		<? endif; ?>
	</div>
</div>

