<?
	/**
	 * @var $itemData LinkBundlePreviewCoverItem
	 */
?>
<a href="#">
    <img src="data:image/png;base64,<? echo $itemData->image; ?>">
    <div class="item-title"><? echo $itemData->title; ?></div>
    <div class="service-data">
		<? echo $itemData->getItemData(); ?>
        <div class="item-info-content">
            <div class="item-info link-viewer">
                <div class="row banner">
                    <div class="col-xs-12 text-center" style="padding: 30px">
                        <img src="data:image/png;base64,<? echo $itemData->logo; ?>" style="max-height: 440px">
                    </div>
                </div>
            </div>
        </div>
    </div>
</a>
