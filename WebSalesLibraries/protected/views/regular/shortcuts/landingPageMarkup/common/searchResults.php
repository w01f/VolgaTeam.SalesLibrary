<?

	use application\models\shortcuts\models\landing_page\regular_markup\common\SearchResultsBlock;

	/** @var $contentBlock SearchResultsBlock */

	$blockId = sprintf('search-results-block-%s', $contentBlock->id);

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
<div id="<? echo $blockId; ?>" class="search-results-block"
     style="<? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $contentBlock->padding), true); ?>
     <? echo $this->renderPartial('landingPageMarkup/style/styleMargin', array('margin' => $contentBlock->margin), true); ?>">
    <div class="service-data">
        <div class="shortcut-id"><? echo $contentBlock->shortcut->id; ?></div>
		<? if (isset($contentBlock->fixedHeight)): ?>
            <div class="fixed-height"><? echo $contentBlock->fixedHeight; ?></div>
		<? endif; ?>
		<? $this->renderPartial('searchConditions', array('searchContainer' => $contentBlock->shortcut)); ?>
    </div>
</div>
