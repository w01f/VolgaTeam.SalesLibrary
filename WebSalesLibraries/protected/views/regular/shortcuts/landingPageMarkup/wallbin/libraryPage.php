<?

	use application\models\shortcuts\models\landing_page\regular_markup\wallbin\LibraryPageBlock;

	/**
     * @var $contentBlock LibraryPageBlock
	 * @var $screenSettings array
     */

	$blockId = sprintf('library-page-block-%s', $contentBlock->id);

	$libraryPage = $contentBlock->shortcut->getLibraryPage();
	if ($contentBlock->shortcut->pageViewType == 'accordion')
		$content = $this->renderPartial('../wallbin/accordionView',
			array(
				'libraryPage' => $libraryPage,
				'containerId' => $blockId,
				'style' => $contentBlock->shortcut->style->page,
				'screenSettings' => $screenSettings
			), true);
	else
		$content = $this->renderPartial('../wallbin/columnsView',
			array(
				'libraryPage' => $libraryPage,
				'containerId' => $blockId,
				'style' => $contentBlock->shortcut->style->page,
				'screenSettings' => $screenSettings
			), true);

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
<style>
    <? if (isset($contentBlock->shortcut->style->page->padding) && $contentBlock->shortcut->style->page->padding->isConfigured): ?>
    <? echo '#'.$blockId; ?> .wallbin-container .content-container {

        padding-top: <? echo $contentBlock->shortcut->style->page->padding->top; ?>px !important;
        padding-left: <? echo $contentBlock->shortcut->style->page->padding->left; ?>px !important;
        padding-bottom: <? echo $contentBlock->shortcut->style->page->padding->bottom; ?>px !important;
        padding-right: <? echo $contentBlock->shortcut->style->page->padding->right; ?>px !important;
    }

    <?endif;?>
</style>
<div id="<? echo $blockId; ?>" class="library-page-block">
    <div class="service-data wallbin-settings">
        <div class="encoded-data">
			<? echo base64_encode(CJSON::encode(array(
				'shortcutId' => $contentBlock->shortcut->id,
				'pageViewType' => $contentBlock->shortcut->pageViewType,
				'processResponsiveColumns' => $contentBlock->shortcut->style->page->showResponsiveColumns
			))
			); ?>
        </div>
    </div>
    <div class="wallbin-container"><? echo $content; ?></div>
</div>
