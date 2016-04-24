<?
	/**
	 * @var $data DocumentPreviewData
	 */

	$imageUrlPrefix = Yii::app()->getBaseUrl(true);
	$pageItemName = '';
	switch ($data->format)
	{
		case 'ppt':
			$pageItemName = 'Slide';
			break;
		case 'doc':
			$pageItemName = 'Page';
			break;
	}

	$headerColumnSizeDivider = 0;
	if ($data->config->allowDownload)
		$headerColumnSizeDivider++;
	if ($data->config->allowDownload && !$data->singlePage)
		$headerColumnSizeDivider++;
	if ($data->config->allowAddToFavorites)
		$headerColumnSizeDivider++;
	if ($data->config->allowAddToQuickSite)
		$headerColumnSizeDivider++;
	if ($headerColumnSizeDivider > 0)
		$headerColumnSize = 12 / $headerColumnSizeDivider;
	else
		$headerColumnSize = 0;
	$enablePreviewHeader = $headerColumnSize > 0;
?>
<div class="link-viewer<? if ($data->config->enableLogging): ?> logger-form<? endif; ?>" data-log-group="Link" data-log-action="Preview Activity">
	<? if ($enablePreviewHeader): ?>
		<div class="row row-buttons tab-above-header" id="tab-above-header-preview">
			<? if ($data->config->allowDownload): ?>
				<div class="col col-xs-<? echo $headerColumnSize; ?> text-center">
					<div class="text-button log-action download-file" data-log-action="Download File">
						<span>Download File</span> <span class="text-muted file-size"></span>
					</div>
				</div>
			<? endif; ?>
			<? if ($data->config->allowDownload && !$data->singlePage): ?>
				<div class="col col-xs-<? echo $headerColumnSize; ?> text-center">
					<div class="text-button log-action download-page" data-log-action="Download File">
						<span>Download <? echo $pageItemName; ?></span> <span class="text-muted page-size"></span>
					</div>
				</div>
			<? endif; ?>
			<? if ($data->config->allowAddToQuickSite): ?>
				<div class="col col-xs-<? echo $headerColumnSize; ?> text-center">
					<div class="text-button log-action add-quicksite" data-log-action="Add to QS">
						<span>Quicksite</span>
					</div>
				</div>
			<? endif; ?>
			<? if ($data->config->allowAddToFavorites): ?>
				<div class="col col-xs-<? echo $headerColumnSize; ?> text-center">
					<div class="text-button log-action add-favorites" data-log-action="Add to Favorites">
						<span>Favorites</span>
					</div>
				</div>
			<? endif; ?>
		</div>
	<? else: ?>
		<div class="row tab-above-header" id="tab-above-header-preview">
			<span class="header-text">Preview this file…</span>
		</div>
	<? endif; ?>
	<? if ($data->config->allowSave): ?>
		<div class="row tab-above-header" id="tab-above-header-save">
			<span class="header-text">Download or Save this file…</span>
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
				<a class="log-action" href="#link-viewer-tab-save" role="tab" data-toggle="tab">Save</a>
			</li>
		<? endif; ?>
		<? if ($data->config->allowEmail): ?>
			<li><a class="log-action" href="#link-viewer-tab-email" role="tab" data-toggle="tab">Email</a></li>
		<? endif; ?>
	</ul>
	<div class="tab-content">
		<div role="tabpanel" class="tab-pane" id="link-viewer-tab-preview">
			<div class="row preview-gallery">
				<div class="col col-xs-1 text-center">
					<? if (!$data->singlePage): ?>
						<div class="image-button nav-image-button log-action move-previous" data-log-action="Preview Page">
							<img src="<? echo sprintf('%s/images/preview/gallery/prev-slide.png', $imageUrlPrefix); ?>">
						</div>
					<? endif; ?>
				</div>
				<div class="col col-xs-10 text-center page-image-container">
					<img class="page-image log-action" style="display: none;" src="//:0">
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
				<div class="col col-xs-5 text-left">
					<? if ($data->config->enableRating): ?>
						<div id="user-link-rate-container">
							<img class="total-rate" src="" style="height:16px"/>
							<label for="user-link-rate" class="ui-hide-label"></label><input id="user-link-rate" class="rating">
						</div>
					<? endif; ?>
				</div>
				<div class="col col-xs-2 text-center">
					<? if (!$data->singlePage): ?>
						<label class="ui-hide-label" for="image-viewer-slide-selector"></label>
						<select class="selectpicker dropup bootstrapped log-action" id="image-viewer-slide-selector" data-log-action="Preview Page"></select>
					<? endif; ?>
				</div>
				<? if ($data->config->allowPdf): ?>
					<div class="col col-xs-1 col-xs-offset-2 text-center">
						<div class="text-button log-action open-pdf" data-log-action="Open PDF">
							<span>PDF</span>
						</div>
					</div>
				<? endif; ?>
				<div class="col col-xs-1<? if (!$data->config->allowPdf): ?> col-xs-offset-3<? endif; ?> text-center">
					<div class="text-button log-action open-gallery-modal" data-log-action="Preview Modal">
						<span>75%</span>
					</div>
				</div>
				<div class="col col-xs-1 text-center">
					<div class="text-button log-action open-gallery-fullscreen" data-log-action="Preview Fullscreen">
						<span>100%</span>
					</div>
				</div>
			</div>
		</div>
		<? if ($data->config->allowSave): ?>
			<div role="tabpanel" class="tab-pane" id="link-viewer-tab-save">
				<? echo $this->renderPartial('save', array('data' => $data), true); ?>
			</div>
		<? endif; ?>
		<? if ($data->config->allowEmail): ?>
			<div role="tabpanel" class="tab-pane" id="link-viewer-tab-email">
				<? echo $this->renderPartial('email', array('data' => $data), true); ?>
			</div>
		<? endif; ?>
	</div>
</div>