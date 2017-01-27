<?
	/**
	 * @var $itemData LinkBundlePreviewInfoItem
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
                    <div class="body" style="color: <? echo $itemData->foreColor; ?>;
					        <? if ($itemData->backColor !== '#FFFFFF'): ?>background-color: <? echo $itemData->backColor; ?>;<? endif; ?>
                            font-family: <? echo $itemData->font->name; ?>;
                            font-size: <? echo $itemData->font->size; ?>pt;
                            font-weight: <? echo $itemData->font->isBold ? 'bold' : 'normal'; ?>;
                            font-style: <? echo $itemData->font->isItalic ? 'italic' : 'normal'; ?>;
                            text-decoration: <? echo $itemData->font->isUnderlined ? 'underline' : 'none'; ?>;">
						<? echo nl2br($itemData->body); ?></div>
				<? endif; ?>
            </div>
        </div>
    </div>
</a>
