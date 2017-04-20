<?
	use application\models\shortcuts\models\landing_page\regular_markup\common\ShortcutBlock;

    /** @var $contentBlock ShortcutBlock */
?>
<<?echo $contentBlock->buttonize?'button':'a';?> class="<? if ($contentBlock->buttonize): ?>btn btn-default<? else: ?>landing-url<? endif; ?> shortcuts-link<?if(!isset($contentBlock->shortcut)):?> disabled<?endif;?>" href="<? echo isset($contentBlock->shortcut) ? $contentBlock->shortcut->getSourceLink() : '#'; ?>" target="_blank"
        style="<? echo $this->renderPartial('landingPageMarkup/style/styleTextAppearance', array('textAppearance' => $contentBlock->getTextAppearance()), true); ?>
        <? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $contentBlock->padding), true); ?>
        <? echo $this->renderPartial('landingPageMarkup/style/styleMargin', array('margin' => $contentBlock->margin), true); ?>
        <? echo $this->renderPartial('landingPageMarkup/style/styleBorder', array('border' => $contentBlock->border), true); ?>"
	<? if (!empty($contentBlock->hoverText)): ?> title="<? echo $contentBlock->hoverText; ?>"<? endif; ?>>
	<? echo $this->renderPartial('landingPageMarkup/common/blockContainer', array('contentBlocks' => $contentBlock->items), true); ?>
    <div class="service-data">
	    <? echo isset($contentBlock->shortcut) ? $contentBlock->shortcut->getMenuItemData() : '<div class="same-page"></div><div class="has-custom-handler"></div>'; ?>
    </div>
</<?echo $contentBlock->buttonize?'button':'a';?>>