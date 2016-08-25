<?
	/**
	 * @var $previewData PreviewData
	 */
?>
<div class="link-viewer link-viewer-rate tool-dialog">
	<div class="row">
		<div class="col col-xs-12 text-left">
			<h3 class="title">Do you want to RATE this link?</h3>
		</div>
	</div>
	<div class="row">
		<div class="col col-xs-12 text-left">
			<h4 class="file-name">
				<? echo isset($previewData->fileName) ? $previewData->fileName : nl2br($previewData->name); ?>
			</h4>
		</div>
	</div>
	<div class="row">
		<div class="col col-xs-12 text-left">
			<div id="user-link-rate-container">
				<img class="total-rate" src="" style="height:16px"/>
				<label for="user-link-rate" class="ui-hide-label"></label><input id="user-link-rate" class="rating">
			</div>
		</div>
	</div>
</div>