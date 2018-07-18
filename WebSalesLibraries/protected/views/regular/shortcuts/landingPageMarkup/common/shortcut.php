<?
	use application\models\shortcuts\models\landing_page\regular_markup\common\ShortcutBlock;

    /**
     * @var $contentBlock ShortcutBlock
     * @var $screenSettings array
     */

	$blockId = sprintf('shortcut-%s', $contentBlock->id);

	echo $this->renderPartial('landingPageMarkup/style/styleBorder',
		array(
			'border' => $contentBlock->border,
			'blockId' => $blockId
		)
		, true);
	echo $this->renderPartial('landingPageMarkup/style/styleTextAppearance',
		array(
			'textAppearance' => $contentBlock->getTextAppearance(),
			'blockId' => $blockId
		)
		, true);
	echo $this->renderPartial('landingPageMarkup/style/styleBackground',
		array(
			'background' => $contentBlock->background,
			'blockId' => $blockId
		)
		, true);
?>
<<?echo $contentBlock->buttonize?'button':'a';?> id="<? echo $blockId; ?>" class="<? if ($contentBlock->buttonize): ?>btn btn-default<? else: ?>landing-url<? endif; ?> shortcuts-link<?if(!isset($contentBlock->shortcut)):?> disabled<?endif;?><?if(isset($contentBlock->shortcut) && $contentBlock->shortcut->type === 'libraryfile'):?> shortcut-library-link<?endif;?><?if(isset($contentBlock->shortcut) && $contentBlock->shortcut->type === 'libraryfile' && $contentBlock->shortcut->isDraggable):?> draggable<?endif;?>" href="<? echo isset($contentBlock->shortcut) ? $contentBlock->shortcut->getSourceLink() : '#'; ?>" target="_blank"
        style="<? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $contentBlock->padding), true); ?>
        <? echo $this->renderPartial('landingPageMarkup/style/styleMargin', array('margin' => $contentBlock->margin), true); ?>"
	<? if (!empty($contentBlock->hoverText)): ?> title="<? echo $contentBlock->hoverText; ?>"<? endif; ?> <? if (isset($contentBlock->shortcut) && $contentBlock->shortcut->type === 'libraryfile' && $contentBlock->shortcut->isDraggable): ?>draggable="true"
    data-url-header="<? echo $contentBlock->shortcut->dragHeader; ?>"
    data-url="<? echo $contentBlock->shortcut->url; ?>"<? endif; ?>>
    <? echo $this->renderPartial('landingPageMarkup/common/blockContainer', array('contentBlocks' => $contentBlock->items, 'screenSettings' => $screenSettings), true); ?>
    <div class="service-data">
	    <? echo isset($contentBlock->shortcut) ? $contentBlock->shortcut->getMenuItemData() : '<div class="same-page"></div><div class="has-custom-handler"></div>'; ?>
    </div>
</<?echo $contentBlock->buttonize?'button':'a';?>>