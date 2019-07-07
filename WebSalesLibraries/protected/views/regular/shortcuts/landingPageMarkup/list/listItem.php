<?
	use application\models\shortcuts\models\landing_page\regular_markup\list_block\ListItem;

	/**
     * @var $contentBlock ListItem
	 * @var $screenSettings array
     */

	$blockId = sprintf('list-item-%s', $contentBlock->id);

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
<li id="<? echo $blockId; ?>" class="list-group-item<? if ($contentBlock->isActive): ?> active<? endif; ?>"
    style="<? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $contentBlock->padding), true); ?>
    <? echo $this->renderPartial('landingPageMarkup/style/styleMargin', array('margin' => $contentBlock->margin), true); ?>
    <? if (!empty($contentBlock->backColor)): ?> background-color: <? echo Utils::formatColorToHex($contentBlock->backColor); ?><? endif; ?>"
	<? if (!empty($contentBlock->hoverText)): ?> title="<? echo $contentBlock->hoverText; ?>"<? endif; ?>>
	<? echo $this->renderPartial('landingPageMarkup/common/blockContainer', array('contentBlocks' => $contentBlock->items, 'screenSettings' => $screenSettings), true); ?>
</li>