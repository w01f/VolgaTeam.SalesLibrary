<?
	use application\models\shortcuts\models\landing_page\regular_markup\list_block\ListItem;

	/** @var $contentBlock ListItem */
?>
<li class="list-group-item<? if ($contentBlock->isActive): ?> active<? endif; ?>"
    style="<? echo $this->renderPartial('landingPageMarkup/style/styleTextAppearance', array('textAppearance' => $contentBlock->getTextAppearance()), true); ?>
    <? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $contentBlock->padding), true); ?>
    <? echo $this->renderPartial('landingPageMarkup/style/styleMargin', array('margin' => $contentBlock->margin), true); ?>
    <? echo $this->renderPartial('landingPageMarkup/style/styleBorder', array('border' => $contentBlock->border), true); ?>
    <? if (!empty($contentBlock->backColor)): ?> background-color: <? echo '#' . $contentBlock->backColor; ?><? endif; ?>"
	<? if (!empty($contentBlock->hoverText)): ?> title="<? echo $contentBlock->hoverText; ?>"<? endif; ?>>
	<? echo $this->renderPartial('landingPageMarkup/common/blockContainer', array('contentBlocks' => $contentBlock->items), true); ?>
</li>