<?
	/**
	 * @var $data PdfPreviewData
	 * */

	$imageUrlPrefix = Yii::app()->getBaseUrl(true);

	$headerColumnSizeDivider = 0;
	if ($data->config->allowDownload)
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
<div class="link-viewer<? if ($data->config->enableLogging): ?> logger-form<? endif; ?>" data-log-group="Link"
     data-log-action="Preview Activity">
	<? if ($enablePreviewHeader): ?>
		<div class="row row-buttons tab-above-header" id="tab-above-header-preview">
			<? if ($data->config->allowDownload): ?>
				<div class="col col-xs-<? echo $headerColumnSize; ?> text-center" data-log-action="Download File">
					<div class="text-button log-action download-file">
						<span>Download</span> <span class="text-muted file-size"></span>
					</div>
				</div>
			<? endif; ?>
			<? if ($data->config->allowAddToQuickSite): ?>
				<div class="col col-xs-<? echo $headerColumnSize; ?> text-center" data-log-action="Add to QS">
					<div class="text-button log-action add-quicksite">
						<span>Quicksite</span>
					</div>
				</div>
			<? endif; ?>
			<? if ($data->config->allowAddToFavorites): ?>
				<div class="col col-xs-<? echo $headerColumnSize; ?> text-center" data-log-action="Add to Favorites">
					<div class="text-button log-action add-favorites">
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
				<a class="log-action" href="#link-viewer-tab-save" role="tab" data-toggle="tab">Save</a>
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
				<div class="col col-xs-3 text-left">
					<? if ($data->config->enableRating): ?>
						<div id="user-link-rate-container">
							<img class="total-rate" src="" style="height:16px"/>
							<label for="user-link-rate" class="ui-hide-label"></label><input id="user-link-rate"
							                                                                 class="rating">
						</div>
					<? endif; ?>
				</div>
				<? if ($data->totalViews > 0): ?>
					<div class="col col-xs-2 <? echo !$data->config->enableRating ? 'col-xs-offset-2 ' : ''; ?>text-center">
						<div class="text-label">Views: <? echo $data->totalViews; ?></div>
					</div>
				<? endif; ?>
				<div class="col col-xs-2  <? echo $data->totalViews == 0 ? 'col-xs-offset-2 ' : ''; ?>text-center">
					<? if (!$data->singlePage): ?>
						<label class="ui-hide-label" for="image-viewer-slide-selector"></label>
						<select class="selectpicker dropup bootstrapped log-action" id="image-viewer-slide-selector"
						        data-log-action="Preview Page"></select>
					<? endif; ?>
				</div>
				<?
					$offsetApplied = false;
					$initialButtonsOffset = 1;
					if (!isset($data->quickLinkUrl))
						$initialButtonsOffset++;
					if (!$data->config->allowPdf)
						$initialButtonsOffset++;
				?>
				<? if (isset($data->quickLinkUrl)): ?>
					<div
						class="col col-xs-1 <? echo !$offsetApplied ? ('col-xs-offset-' . $initialButtonsOffset . ' ') : '';
							$offsetApplied = true; ?>text-center">
						<div class="text-button log-action open-quick-link" data-log-action="Open Quick Link">
							<span><? echo $data->quickLinkTitle; ?></span>
						</div>
					</div>
				<? endif; ?>
				<? if ($data->config->allowPdf): ?>
					<div
						class="col col-xs-1 <? echo !$offsetApplied ? ('col-xs-offset-' . $initialButtonsOffset . ' ') : '';
							$offsetApplied = true; ?>text-center">
						<div class="text-button log-action open-pdf" data-log-action="Open PDF">
							<span>PDF</span>
						</div>
					</div>
				<? endif; ?>
				<div
					class="col col-xs-1 <? echo !$offsetApplied ? ('col-xs-offset-' . $initialButtonsOffset . ' ') : '';
						$offsetApplied = true; ?>text-center">
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
			<div role="tabpanel" class="tab-pane link-viewer-tab-email" id="link-viewer-tab-email-public">
				<? echo $this->renderPartial('email', array('data' => $data, 'isProtected' => false), true); ?>
			</div>
			<div role="tabpanel" class="tab-pane link-viewer-tab-email" id="link-viewer-tab-email-protected">
				<? echo $this->renderPartial('email', array('data' => $data, 'isProtected' => true), true); ?>
			</div>
		<? endif; ?>
	</div>
</div>

