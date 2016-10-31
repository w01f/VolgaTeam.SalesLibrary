<?
	/**
	 * @var $itemData LinkBundlePreviewStrategyItem
	 */
?>
<a href="#">
	<img src="data:image/png;base64,<? echo $itemData->image; ?>">
	<div class="item-title"><? echo $itemData->title; ?></div>
	<div class="service-data">
		<? echo $itemData->getItemData(); ?>
		<div class="item-info-content">
			<div class="item-info link-viewer">
				<? if (!empty($itemData->header)): ?>
					<div class="header"><? echo nl2br($itemData->header); ?></div>
				<? endif; ?>
				<? if (!empty($itemData->body)): ?>
					<div class="body"><? echo nl2br($itemData->body); ?></div>
				<? endif; ?>
			</div>
		</div>
	</div>
</a>

