<?
	/**
	 * @var $itemData LinkBundlePreviewRevenueItem
	 */
?>
<a href="#">
	<img src="data:image/png;base64,<? echo $itemData->image; ?>">
	<div class="item-title"><? echo $itemData->title; ?></div>
	<div class="service-data">
		<? echo $itemData->getItemData(); ?>
		<div class="item-info-content">
			<div class="item-info link-viewer">
				<div class="header"><? echo nl2br($itemData->header); ?></div>
				<div class="body">
					<? if (count($itemData->revenueItems) > 0): ?>
						<div class="table">
							<? foreach ($itemData->revenueItems as $revenueItem): ?>
								<div class="row">
									<div class="text-left"><? echo $revenueItem['title']; ?></div>
									<div class="text-left"
									     style="width: 15%"><? echo $revenueItem['value']; ?></div>
								</div>
							<? endforeach; ?>
						</div>
					<? endif; ?>
					<? if (!empty($itemData->additionalInfo)): ?>
						<div class="footer">
							<div class="title">Additional Info:</div>
							<? echo nl2br($itemData->additionalInfo); ?>
						</div>
					<? endif; ?>
				</div>
			</div>
		</div>
	</div>
</a>
