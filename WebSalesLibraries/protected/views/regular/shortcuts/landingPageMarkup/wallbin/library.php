<?

	use application\models\shortcuts\models\landing_page\regular_markup\wallbin\LibraryBlock;

	/** @var $contentBlock LibraryBlock */

	$blockId = sprintf('library-block-%s', $contentBlock->id);

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
<div id="<? echo $blockId; ?>" class="library-block">
    <div class="service-data wallbin-settings">
        <div class="encoded-data">
			<? echo base64_encode(CJSON::encode(array(
				'shortcutId' => $contentBlock->shortcut->id,
				'wallbinId' => $contentBlock->shortcut->library->id,
				'wallbinName' => $contentBlock->shortcut->title,
				'pageViewType' => $contentBlock->shortcut->pageViewType,
				'pageSelectorMode' => $contentBlock->shortcut->pageSelectorMode
			))
			); ?>
        </div>
    </div>
	<?
		$this->renderPartial('../wallbin/library', array(
			'library' => $contentBlock->shortcut->library,
			'pageSelectorMode' => $contentBlock->shortcut->pageSelectorMode,
			'pageViewType' => $contentBlock->shortcut->pageViewType,
			'isEmbedded' => true,
			'containerId' => $blockId,
			'styleContainer' => $contentBlock->shortcut,
			'searchBar' => $contentBlock->shortcut->getSearchBar()
		));
	?>
</div>
