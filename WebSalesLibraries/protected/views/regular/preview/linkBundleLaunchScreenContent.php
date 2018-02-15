<?
	/**
	 * @var $itemData LinkBundlePreviewLaunchScreenItem
	 */

	if (!empty($itemData->logo))
	{
		$offset = 2;
		$mainColumnSize = 10;
	}
	else
	{
		$offset = 0;
		$mainColumnSize = 12;
	}
?>
<a href="#">
    <img src="data:image/png;base64,<? echo $itemData->image; ?>">
    <div class="item-title"><? echo $itemData->title; ?></div>
    <div class="service-data">
		<? echo $itemData->getItemData(); ?>
        <div class="item-info-content">
            <div class="item-info link-viewer launch-screen">
				<? if (!empty($itemData->logo) || !empty($itemData->header)): ?>
                    <div class="row top-row">
						<? if (!empty($itemData->logo)): ?>
                            <div class="col-xs-2">
                                <img src="data:image/png;base64,<? echo $itemData->logo; ?>">
                            </div>
						<? endif; ?>
						<? if (!empty($itemData->header)): ?>
                            <div class="col-xs-<? echo $mainColumnSize; ?>">
                                <div style="color: <? echo $itemData->headerForeColor; ?>;
                                        <? if ($itemData->headerBackColor !== '#FFFFFF'): ?>background-color: <? echo $itemData->headerBackColor; ?>;<? endif; ?>
                                        font-family: <? echo FontReplacementHelper::replaceFont($itemData->headerFont->name); ?>;
                                        font-size: <? echo !is_int($itemData->headerFont->size) ? $itemData->headerFont->size->single : $itemData->headerFont->size; ?>pt;
                                        font-weight: <? echo $itemData->headerFont->isBold ? 'bold' : 'normal'; ?>;
                                        font-style: <? echo $itemData->headerFont->isItalic ? 'italic' : 'normal'; ?>;
                                        text-decoration: <? echo $itemData->headerFont->isUnderlined ? 'underline' : 'none'; ?>;">
									<? echo $itemData->header; ?>
                                </div>
                            </div>
						<? endif; ?>
                    </div>
				<? endif; ?>
				<? if (!empty($itemData->banner)): ?>
                    <div class="row banner">
                        <div class="col-xs-<? echo $mainColumnSize; ?> col-xs-offset-<? echo $offset; ?> text-center">
                            <img src="data:image/png;base64,<? echo $itemData->banner; ?>">
                        </div>
                    </div>
				<? endif; ?>
				<? if (!empty($itemData->footer)): ?>
                    <div class="row bottom-row">
                        <div class="col-xs-<? echo $mainColumnSize; ?> col-xs-offset-<? echo $offset; ?>">
                            <div style="color: <? echo $itemData->footerForeColor; ?>;
                                    <? if ($itemData->footerBackColor !== '#FFFFFF'): ?>background-color: <? echo $itemData->footerBackColor; ?>;<? endif; ?>
                                    font-family: <? echo FontReplacementHelper::replaceFont($itemData->footerFont->name); ?>;
                                    font-size: <? echo !is_int($itemData->footerFont->size) ? $itemData->footerFont->size->single : $itemData->footerFont->size; ?>pt;
                                    font-weight: <? echo $itemData->footerFont->isBold ? 'bold' : 'normal'; ?>;
                                    font-style: <? echo $itemData->footerFont->isItalic ? 'italic' : 'normal'; ?>;
                                    text-decoration: <? echo $itemData->footerFont->isUnderlined ? 'underline' : 'none'; ?>;">
								<? echo $itemData->footer; ?>
                            </div>
                        </div>
                    </div>
				<? endif; ?>
            </div>
        </div>
    </div>
</a>
