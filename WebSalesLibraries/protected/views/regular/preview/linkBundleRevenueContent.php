<?
	/**
	 * @var $itemData LinkBundlePreviewRevenueItem
	 */
?>
<style>
    .item-info .footer .text {

    }
</style>
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
                            <div style="color: <? echo $itemData->foreColor; ?>;
                                    <? if ($itemData->backColor !== '#FFFFFF'): ?>background-color: <? echo $itemData->backColor; ?>;<? endif; ?>
                                    font-family: <? echo FontReplacementHelper::replaceFont($itemData->font->name); ?>;
                                    font-size: <? echo !is_int($itemData->font->size) ? $itemData->font->size->single : $itemData->font->size; ?>pt;
                                    font-weight: <? echo $itemData->font->isBold ? 'bold' : 'normal'; ?>;
                                    font-style: <? echo $itemData->font->isItalic ? 'italic' : 'normal'; ?>;
                                    text-decoration: <? echo $itemData->font->isUnderlined ? 'underline' : 'none'; ?>;">
								<? echo nl2br($itemData->additionalInfo); ?></div>
                        </div>
					<? endif; ?>
                </div>
            </div>
        </div>
    </div>
</a>
