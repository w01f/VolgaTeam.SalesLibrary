<?
	use application\models\shortcuts\models\landing_page\regular_markup\common\Row;

	/**
     * @var $contentBlock Row
	 * @var $screenSettings array
     */

	$blockId = sprintf('row-%s', $contentBlock->id);

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
<div id="<? echo $blockId; ?>" class="row<? if ($contentBlock->centerContent): ?> center-content<? endif; ?>"
     style="<? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $contentBlock->padding), true); ?>
     <? echo $this->renderPartial('landingPageMarkup/style/styleMargin', array('margin' => $contentBlock->margin), true); ?>"
	<? if (!empty($contentBlock->hoverText)): ?> title="<? echo $contentBlock->hoverText; ?>"<? endif; ?>>
	<? echo $this->renderPartial('landingPageMarkup/common/blockContainer', array('contentBlocks' => $contentBlock->items, 'screenSettings' => $screenSettings), true); ?>
</div>
