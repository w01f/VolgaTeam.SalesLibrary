<?
	use application\models\shortcuts\models\landing_page\regular_markup\common\TextBlock;

	/** @var $contentBlock TextBlock */
?>
<div style="<? echo $this->renderPartial('landingPageMarkup/style/styleTextAppearance', array('textAppearance' => $contentBlock->getTextAppearance()), true); ?>
<? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $contentBlock->padding), true); ?>
<? echo $this->renderPartial('landingPageMarkup/style/styleMargin', array('margin' => $contentBlock->margin), true); ?>
<? echo $this->renderPartial('landingPageMarkup/style/styleBorder', array('border' => $contentBlock->border), true); ?>"
	<? if (!empty($contentBlock->hoverText)): ?> title="<? echo $contentBlock->hoverText; ?>"<? endif; ?>>
	<? echo '<' . $contentBlock->htmlTag . (isset($contentBlock->htmlClass) ? (' class="' . $contentBlock->htmlClass . '"') : '') . '>' ?>
	<? echo $contentBlock->text; ?>
	<? echo '</' . $contentBlock->htmlTag . '>' ?>
</div>
