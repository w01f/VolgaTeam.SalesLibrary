<?
	use application\models\shortcuts\models\landing_page\regular_markup\common\ImageBlock;

	/** @var $contentBlock ImageBlock */

	$blockId = sprintf('image-%s', $contentBlock->id);

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
?>
<div id="<? echo $blockId; ?>" class="<? if (isset($contentBlock->floatSide)): ?>pull-<? echo $contentBlock->floatSide; ?><? endif; ?>
        <? if ($contentBlock->hideCondition->large): ?> hidden-lg<? endif; ?>
        <? if ($contentBlock->hideCondition->medium): ?> hidden-md<? endif; ?>
        <? if ($contentBlock->hideCondition->small): ?> hidden-sm<? endif; ?>
        <? if ($contentBlock->hideCondition->extraSmall): ?> hidden-xs<? endif; ?>"
     style="<? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $contentBlock->padding), true); ?>
     <? echo $this->renderPartial('landingPageMarkup/style/styleMargin', array('margin' => $contentBlock->margin), true); ?>"
	<? if (!empty($contentBlock->hoverText)): ?> title="<? echo $contentBlock->hoverText; ?>"<? endif; ?>>
    <img class="img-responsive" src="<? echo $contentBlock->source; ?>" <? if (!empty($contentBlock->animation)): ?> data-bs-hover-animate="<? echo $contentBlock->animation; ?>"<? endif; ?>>
</div>
