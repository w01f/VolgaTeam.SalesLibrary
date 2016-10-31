<?
	/**
	 * @var $itemData LinkBundlePreviewLinkItem
	 */
?>
<a href="#">
	<img src="data:image/png;base64,<? echo $itemData->image; ?>">
	<div class="item-title"><? echo $itemData->title; ?></div>
	<div class="service-data">
		<? echo $itemData->getItemData(); ?>
	</div>
</a>
