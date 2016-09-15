<?
	/**
	 * @var $data YouTubePreviewData
	 */

	$imageUrlPrefix = Yii::app()->getBaseUrl(true);

	$headerColumnSizeDivider = 0;
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
			<span class="header-text">YouTube Link...</span>
		</div>
	<? endif; ?>
	<? if ($data->config->allowSave): ?>
		<div class="row tab-above-header" id="tab-above-header-save">
			<span class="header-text">YouTube Link...</span>
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
				<div class="col col-xs-12 text-center">
					<iframe height="305" width="750" src="https://www.youtube.com/embed/<? echo $data->youTubeId; ?>"
					        frameborder="0" allowfullscreen></iframe>
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
				<?
					$offsetApplied = false;
					$initialButtonsOffset = 4;
					if ($data->totalViews == 0)
						$initialButtonsOffset+=2;
					if (!isset($data->quickLinkUrl))
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
				<div class="col col-xs-1 <? echo !$offsetApplied ? ('col-xs-offset-' . $initialButtonsOffset . ' ') : '';
					$offsetApplied = true; ?>text-center">
					<div class="text-button log-action open-video-modal" data-log-action="Preview Modal">
						<span>75%</span>
					</div>
				</div>
				<div class="col col-xs-1 text-center">
					<div class="text-button log-action open-fullscreen" data-log-action="Preview Fullscreen">
						<span>YouTube</span>
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

