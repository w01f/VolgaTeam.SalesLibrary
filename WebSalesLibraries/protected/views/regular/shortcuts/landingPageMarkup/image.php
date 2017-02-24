<?
	/** @var $contentBlock \application\models\shortcuts\models\landing_page\regular_markup\ImageBlock */
?>
<div <? if (isset($contentBlock->floatSide)): ?>class="pull-<? echo $contentBlock->floatSide; ?>" <? endif; ?>
     style="<? echo $this->renderPartial('landingPageMarkup/styleTextAppearance', array('textAppearance' => $contentBlock->getTextAppearance()), true); ?>
     <? echo $this->renderPartial('landingPageMarkup/stylePadding', array('padding' => $contentBlock->padding), true); ?>
     <? echo $this->renderPartial('landingPageMarkup/styleMargin', array('margin' => $contentBlock->margin), true); ?>
     <? echo $this->renderPartial('landingPageMarkup/styleBorder', array('border' => $contentBlock->border), true); ?>"
	<? if (!empty($contentBlock->hoverText)): ?> title="<? echo $contentBlock->hoverText; ?>"<? endif; ?>>
    <img class="img-responsive" src="<? echo $contentBlock->source; ?>" <? if (!empty($contentBlock->animation)): ?> data-bs-hover-animate="<? echo $contentBlock->animation; ?>"<? endif; ?>>
</div>
