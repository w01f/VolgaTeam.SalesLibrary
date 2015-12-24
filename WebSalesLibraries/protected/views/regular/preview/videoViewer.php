<?
	/**
	 * @var $data VideoPreviewData
	 * */

	$imageUrlPrefix = Yii::app()->getBaseUrl(true);

	$headerColumnSizeDivider = 1;
	if ($data->allowAddToFavorites)
		$headerColumnSizeDivider++;
	if ($data->allowAddToQuickSite)
		$headerColumnSizeDivider++;
	$headerColumnSize = 12 / $headerColumnSizeDivider;

	$fullScreenControlMode  = Yii::app()->browser->getBrowser() == Browser::BROWSER_EO ? 'eo' : 'regular';
	$fullScreenSizeMode  = Yii::app()->browser->isMobile() ? 'mobile' : 'regular';
?>
<div class="link-viewer <? echo $fullScreenControlMode; ?><? if ($data->userAuthorized): ?> logger-form<?endif;?>" data-log-group="Link" data-log-action="Preview Activity">
	<div class="row row-buttons tab-above-header active" id="tab-above-header-preview">
		<div class="col col-xs-<? echo $headerColumnSize; ?> text-center">
			<div class="text-button log-action download-file" data-log-action="Download File">
				<span>Download</span> <span class="text-muted file-size"></span>
			</div>
		</div>
		<? if ($data->allowAddToQuickSite): ?>
			<div class="col col-xs-<? echo $headerColumnSize; ?> text-center" data-log-action="Add to QS">
				<div class="text-button log-action add-quicksite">
					<span>Quicksite</span>
				</div>
			</div>
		<? endif; ?>
		<? if ($data->allowAddToFavorites): ?>
			<div class="col col-xs-<? echo $headerColumnSize; ?> text-center" data-log-action="Add to Favorites">
				<div class="text-button log-action add-favorites">
					<span>Favorites</span>
				</div>
			</div>
		<? endif; ?>
	</div>
	<div class="row tab-above-header" id="tab-above-header-save">
		Download or Save this file…
	</div>
	<? if ($data->allowAddToQuickSite): ?>
		<div class="row tab-above-header" id="tab-above-header-email">
			Send this link to your client…
		</div>
	<? endif; ?>
	<ul class="nav nav-tabs" role="tablist" id="link-viewer-body-tabs">
		<li class="active">
			<a class="log-action" href="#link-viewer-tab-preview" role="tab" data-toggle="tab">Preview</a>
		</li>
		<li>
			<a class="log-action" href="#link-viewer-tab-save" role="tab" data-toggle="tab">Save</a>
		</li>
		<? if ($data->allowAddToQuickSite): ?>
			<li>
				<a class="log-action" href="#link-viewer-tab-email" role="tab" data-toggle="tab">Email</a>
			</li>
		<? endif; ?>
	</ul>
	<div class="tab-content">
		<div role="tabpanel" class="tab-pane active" id="link-viewer-tab-preview">
			<div class="row preview-gallery">
				<div class="col col-xs-12 text-center">
					<video id="video-player" class="video-js vjs-default-skin log-action" height="305" width="750"></video>
				</div>
			</div>
			<div class="row row-buttons gallery-control-buttons">
				<div class="col col-xs-5 text-left">
					<? if ($data->userAuthorized): ?>
						<div id="user-link-rate-container">
							<img class="total-rate" src="" style="height:16px"/>
							<label for="user-link-rate" class="ui-hide-label"></label><input id="user-link-rate" class="rating">
						</div>
					<? endif; ?>
				</div>
				<div class="col col-xs-1 col-xs-offset-5 text-center">
					<div class="text-button log-action open-video-modal"  data-log-action="Preview Modal">
						<span>75%</span>
					</div>
				</div>
				<div class="col col-xs-1 text-center">
					<div class="text-button log-action open-video-fullscreen-<? echo $fullScreenSizeMode; ?>" data-log-action="Preview Fullscreen">
						<span>100%</span>
					</div>
				</div>
			</div>
		</div>
		<div role="tabpanel" class="tab-pane" id="link-viewer-tab-save">
			<? echo $this->renderPartial('save', array('data' => $data), true); ?>
		</div>
		<? if ($data->allowAddToQuickSite): ?>
			<div role="tabpanel" class="tab-pane" id="link-viewer-tab-email">
				<? echo $this->renderPartial('email', array('data' => $data), true); ?>
			</div>
		<? endif; ?>
	</div>
</div>

