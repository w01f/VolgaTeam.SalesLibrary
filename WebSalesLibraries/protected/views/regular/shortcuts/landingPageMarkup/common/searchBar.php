<?
	use application\models\shortcuts\models\landing_page\regular_markup\common\SearchBarBlock;

    /** @var $contentBlock SearchBarBlock */

	$searchBar = $contentBlock->getSearchBar();
	$blockId = sprintf('search-bar-container-%s', $contentBlock->id);

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
<? if ($searchBar->configured): ?>
    <div id="<? echo $blockId; ?>" class="shortcuts-search-bar-container hidden-xs"
         style="<? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $contentBlock->padding), true); ?>
	     <? echo $this->renderPartial('landingPageMarkup/style/styleMargin', array('margin' => $contentBlock->margin), true); ?>"
		<? if (!empty($contentBlock->hoverText)): ?> title="<? echo $contentBlock->hoverText; ?>"<? endif; ?>>
		<? echo $this->renderPartial('searchBar/bar', array('searchBar' => $searchBar), true); ?>
    </div>
<? endif; ?>