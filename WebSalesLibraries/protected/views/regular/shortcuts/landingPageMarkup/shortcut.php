<?
	/** @var $contentBlock \application\models\shortcuts\models\landing_page\regular_markup\ShortcutBlock */
?>
<<?echo $contentBlock->buttonize?'button':'a';?> class="<? if ($contentBlock->buttonize): ?>btn btn-default<? else: ?>landing-url<? endif; ?> shortcuts-link<?if(!isset($contentBlock->shortcut)):?> disabled<?endif;?>" href="<? echo isset($contentBlock->shortcut) ? $contentBlock->shortcut->getSourceLink() : '#'; ?>" target="_blank"
        style="<? echo $this->renderPartial('landingPageMarkup/styleTextAppearance', array('textAppearance' => $contentBlock->getTextAppearance()), true); ?>
        <? echo $this->renderPartial('landingPageMarkup/stylePadding', array('padding' => $contentBlock->padding), true); ?>
        <? echo $this->renderPartial('landingPageMarkup/styleMargin', array('margin' => $contentBlock->margin), true); ?>
        <? echo $this->renderPartial('landingPageMarkup/styleBorder', array('border' => $contentBlock->border), true); ?>"
	<? if (!empty($contentBlock->hoverText)): ?> title="<? echo $contentBlock->hoverText; ?>"<? endif; ?>>
	<? echo $this->renderPartial('landingPageMarkup/blockContainer', array('contentBlocks' => $contentBlock->items), true); ?>
    <div class="service-data">
	    <? echo isset($contentBlock->shortcut) ? $contentBlock->shortcut->getMenuItemData() : '<div class="same-page"></div><div class="has-custom-handler"></div>'; ?>
    </div>
</<?echo $contentBlock->buttonize?'button':'a';?>>