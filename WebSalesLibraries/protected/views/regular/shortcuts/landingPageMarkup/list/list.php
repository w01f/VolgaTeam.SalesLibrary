<?
	use application\models\shortcuts\models\landing_page\regular_markup\list_block\ListBlock;

	/**
     * @var $contentBlock ListBlock
	 * @var $screenSettings array
     */

	$blockId = sprintf('list-%s', $contentBlock->id);

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
<style>
    <?if(!empty($contentBlock->borderColor)):?>
    <? echo '#'.$blockId; ?>
    .list-group-item {
        border-color: <? echo Utils::formatColor($contentBlock->borderColor); ?> !important;
    }

    <?endif;?>
</style>
<ul id="<? echo $blockId; ?>" class="list-group"
    style="<? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $contentBlock->padding), true); ?>
    <? echo $this->renderPartial('landingPageMarkup/style/styleMargin', array('margin' => $contentBlock->margin), true); ?>"
	<? if (!empty($contentBlock->hoverText)): ?> title="<? echo $contentBlock->hoverText; ?>"<? endif; ?>>
	<? echo $this->renderPartial('landingPageMarkup/common/blockContainer', array('contentBlocks' => $contentBlock->items, 'screenSettings' => $screenSettings), true); ?>
</ul>