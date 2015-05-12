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

?>
<div class="link-viewer">
	<div class="row row-buttons tab-above-header active" id="tab-above-header-preview">
		<div class="col col-xs-<? echo $headerColumnSize; ?> text-center">
			<div class="text-button download-file">
				<span>Download</span> <span class="text-muted file-size"></span>
			</div>
		</div>
		<? if ($data->allowAddToQuickSite): ?>
			<div class="col col-xs-<? echo $headerColumnSize; ?> text-center">
				<div class="text-button add-quicksite">
					<span>Quicksite</span>
				</div>
			</div>
		<? endif; ?>
		<? if ($data->allowAddToFavorites): ?>
			<div class="col col-xs-<? echo $headerColumnSize; ?> text-center">
				<div class="text-button add-favorites">
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
			<a href="#link-viewer-tab-preview" role="tab" data-toggle="tab">Preview</a></li>
		<li><a href="#link-viewer-tab-save" role="tab" data-toggle="tab">Save</a></li>
		<? if ($data->allowAddToQuickSite): ?>
			<li><a href="#link-viewer-tab-email" role="tab" data-toggle="tab">Email</a></li>
		<? endif; ?>
	</ul>
	<div class="tab-content">
		<div role="tabpanel" class="tab-pane active" id="link-viewer-tab-preview">
			<div class="row preview-gallery">
				<div class="col col-xs-12 text-center">
					<video id="video-player" class="video-js vjs-default-skin" height="305" width="750"></video>
				</div>
			</div>
			<div class="row row-buttons gallery-control-buttons">
				<div class="col col-xs-5 text-left">
					<div id="user-link-rate-container">
						<img class="total-rate" src="" style="height:16px"/>
						<label for="user-link-rate" class="ui-hide-label"></label><input id="user-link-rate" class="rating">
					</div>
				</div>
				<div class="col col-xs-1 col-xs-offset-5 text-center">
					<div class="text-button open-video-modal">
						<span>75%</span>
					</div>
				</div>
				<div class="col col-xs-1 text-center">
					<div class="text-button open-video-fullscreen">
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

