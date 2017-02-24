<?
	/** @var $contentBlock \application\models\shortcuts\models\landing_page\regular_markup\ListItem */
?>
<li class="list-group-item<? if ($contentBlock->isActive): ?> active<? endif; ?>"
    style="<? echo $this->renderPartial('landingPageMarkup/styleTextAppearance', array('textAppearance' => $contentBlock->getTextAppearance()), true); ?>
    <? echo $this->renderPartial('landingPageMarkup/stylePadding', array('padding' => $contentBlock->padding), true); ?>
    <? echo $this->renderPartial('landingPageMarkup/styleMargin', array('margin' => $contentBlock->margin), true); ?>
    <? echo $this->renderPartial('landingPageMarkup/styleBorder', array('border' => $contentBlock->border), true); ?>
    <? if (!empty($contentBlock->backColor)): ?> background-color: <? echo '#' . $contentBlock->backColor; ?><? endif; ?>"
	<? if (!empty($contentBlock->hoverText)): ?> title="<? echo $contentBlock->hoverText; ?>"<? endif; ?>>
	<? echo $this->renderPartial('landingPageMarkup/blockContainer', array('contentBlocks' => $contentBlock->items), true); ?>
</li>