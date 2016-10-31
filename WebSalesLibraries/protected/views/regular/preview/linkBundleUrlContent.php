<?
	/**
	 * @var $itemData LinkBundlePreviewUrlItem
	 */
?>
<a href="<? echo $itemData->url; ?>" target="_blank">
	<img src="data:image/png;base64,<? echo $itemData->image; ?>">
	<div class="item-title"><? echo $itemData->title; ?></div>
</a>
