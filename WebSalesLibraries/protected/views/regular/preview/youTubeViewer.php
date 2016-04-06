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
<div class="link-viewer<? if ($data->config->enableLogging): ?> logger-form<? endif; ?>" data-log-group="Link" data-log-action="Preview Activity">
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
				<div class="col col-xs-12 text-center">
					<iframe height="305" width="750" src="https://www.youtube.com/embed/<? echo $data->youTubeId;?>" frameborder="0" allowfullscreen></iframe>
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
				<div class="col col-xs-1 col-xs-offset-5 text-center">
					<div class="text-button log-action open-modal" data-log-action="Preview Modal">
						<span>75%</span>
					</div>
				</div>
				<div class="col col-xs-1 text-center">
					<div class="text-button log-action open-fullscreen" data-log-action="Preview Fullscreen">
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

