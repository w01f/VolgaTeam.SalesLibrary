<?
	/** @var $contentBlock \application\models\shortcuts\models\landing_page\regular_markup\ListBlock */
?>
<ul class="list-group" style="<? echo $this->renderPartial('landingPageMarkup/styleTextAppearance', array('textAppearance' => $contentBlock->getTextAppearance()), true); ?>
	<? echo $this->renderPartial('landingPageMarkup/stylePadding', array('padding' => $contentBlock->padding), true); ?>
	<? echo $this->renderPartial('landingPageMarkup/styleMargin', array('margin' => $contentBlock->margin), true); ?>
	<? echo $this->renderPartial('landingPageMarkup/styleBorder', array('border' => $contentBlock->border), true); ?>"
	<? if (!empty($contentBlock->hoverText)): ?> title="<? echo $contentBlock->hoverText; ?>"<? endif; ?>>
	<? echo $this->renderPartial('landingPageMarkup/blockContainer', array('contentBlocks' => $contentBlock->items), true); ?>
</ul>