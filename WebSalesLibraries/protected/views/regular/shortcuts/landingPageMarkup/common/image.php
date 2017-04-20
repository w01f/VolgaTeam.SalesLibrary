<?
	use application\models\shortcuts\models\landing_page\regular_markup\common\ImageBlock;

	/** @var $contentBlock ImageBlock */
?>
<div <? if (isset($contentBlock->floatSide)): ?>class="pull-<? echo $contentBlock->floatSide; ?>" <? endif; ?>
     style="<? echo $this->renderPartial('landingPageMarkup/style/styleTextAppearance', array('textAppearance' => $contentBlock->getTextAppearance()), true); ?>
     <? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $contentBlock->padding), true); ?>
     <? echo $this->renderPartial('landingPageMarkup/style/styleMargin', array('margin' => $contentBlock->margin), true); ?>
     <? echo $this->renderPartial('landingPageMarkup/style/styleBorder', array('border' => $contentBlock->border), true); ?>"
	<? if (!empty($contentBlock->hoverText)): ?> title="<? echo $contentBlock->hoverText; ?>"<? endif; ?>>
    <img class="img-responsive" src="<? echo $contentBlock->source; ?>" <? if (!empty($contentBlock->animation)): ?> data-bs-hover-animate="<? echo $contentBlock->animation; ?>"<? endif; ?>>
</div>
