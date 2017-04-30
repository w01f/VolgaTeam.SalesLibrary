<?
	use application\models\shortcuts\models\landing_page\regular_markup\common\TextBlock;

	/** @var $contentBlock TextBlock */

	$blockId = sprintf('text-%s', $contentBlock->id);

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
<div id="<? echo $blockId; ?>" class="<? if ($contentBlock->hideCondition->large): ?> hidden-lg<? endif; ?>
        <? if ($contentBlock->hideCondition->medium): ?> hidden-md<? endif; ?>
        <? if ($contentBlock->hideCondition->small): ?> hidden-sm<? endif; ?>
        <? if ($contentBlock->hideCondition->extraSmall): ?> hidden-xs<? endif; ?>"
     style="<? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $contentBlock->padding), true); ?>
        <? echo $this->renderPartial('landingPageMarkup/style/styleMargin', array('margin' => $contentBlock->margin), true); ?>"
	<? if (!empty($contentBlock->hoverText)): ?> title="<? echo $contentBlock->hoverText; ?>"<? endif; ?>>
	<? echo '<' . $contentBlock->htmlTag . (isset($contentBlock->htmlClass) ? (' class="' . $contentBlock->htmlClass . '"') : '') . '>' ?>
	<? echo $contentBlock->text; ?>
	<? echo '</' . $contentBlock->htmlTag . '>' ?>
</div>
