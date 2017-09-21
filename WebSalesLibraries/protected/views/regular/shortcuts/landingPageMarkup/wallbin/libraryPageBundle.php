<?

	use application\models\shortcuts\models\landing_page\regular_markup\wallbin\LibraryPageBundleBlock;

	/** @var $contentBlock LibraryPageBundleBlock */

	$blockId = sprintf('library-page-bundle-block-%s', $contentBlock->id);

	/** @var LibraryPageBundleItem $selectedPage */
	$selectedPage = null;
	$savedSelectedPageIdTag = sprintf('SelectedLibraryPageId-%s', $contentBlock->shortcut->id);
	if (isset(Yii::app()->request->cookies[$savedSelectedPageIdTag]))
	{
		$selectedPageId = Yii::app()->request->cookies[$savedSelectedPageIdTag]->value;
		foreach ($contentBlock->shortcut->items as $item)
			if ($item->libraryPage->id == $selectedPageId)
			{
				$selectedPage = $item;
				break;
			}
	}
	if (!isset($selectedPage))
		$selectedPage = $contentBlock->shortcut->items[0];

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
    <? if (isset($contentBlock->shortcut->style->header->padding) && $contentBlock->shortcut->style->header->padding->isConfigured): ?>
    <? echo '#'.$blockId; ?> .wallbin-header-container {

        padding-top: <? echo $contentBlock->shortcut->style->header->padding->top; ?>px !important;
        padding-left: <? echo $contentBlock->shortcut->style->header->padding->left; ?>px !important;
        padding-bottom: <? echo $contentBlock->shortcut->style->header->padding->bottom; ?>px !important;
        padding-right: <? echo $contentBlock->shortcut->style->header->padding->right; ?>px !important;
    }

    <?endif;?>

    <? echo '#'.$blockId; ?> .wallbin-header {
        background-color: <? echo Utils::formatColor($contentBlock->shortcut->style->header->backColor); ?> !important;
    }

    <? echo '#'.$blockId; ?> .wallbin-header .wallbin-header-cell {
        border-bottom: 1px <? echo Utils::formatColor($contentBlock->shortcut->style->header->headerBorderColor); ?> solid !important;
    }

    <? echo '#'.$blockId; ?> .wallbin-header .page-selector-container .tab-pages div,
    <? echo '#'.$blockId; ?> .wallbin-header .page-selector-container .tab-pages span,
    <? echo '#'.$blockId; ?> .wallbin-header .page-selector-container .tab-pages li {
        background-color: <? echo Utils::formatColor($contentBlock->shortcut->style->header->tabSelector->regularBackColor)?> !important;
        border-color: <? echo Utils::formatColor($contentBlock->shortcut->style->header->tabSelector->borderColor)?> !important;
    }

    <? echo '#'.$blockId; ?> .wallbin-header .page-selector-container .tab-pages div.scroll_tab_inner span,
    <? echo '#'.$blockId; ?> .wallbin-header .page-selector-container .tab-pages div.scroll_tab_inner li {
        color: <? echo Utils::formatColor($contentBlock->shortcut->style->header->tabSelector->regularTextColor)?> !important;
    }

    <? echo '#'.$blockId; ?> .wallbin-header .page-selector-container .tab-pages .scroll_tab_left_button,
    <? echo '#'.$blockId; ?> .wallbin-header .page-selector-container .tab-pages .scroll_tab_right_button {
        color: <? echo Utils::formatColor($contentBlock->shortcut->style->header->tabSelector->arrowColor)?> !important;
    }

    <? echo '#'.$blockId; ?> .wallbin-header .page-selector-container .tab-pages div.scroll_tab_inner span.scroll_tab_over,
    <? echo '#'.$blockId; ?> .wallbin-header .page-selector-container .tab-pages div.scroll_tab_inner li.scroll_tab_over {
        color: <? echo Utils::formatColor($contentBlock->shortcut->style->header->tabSelector->hoverTextColor)?> !important;
        background-color: <? echo Utils::formatColor($contentBlock->shortcut->style->header->tabSelector->hoverBackColor)?> !important;
    }

    <? echo '#'.$blockId; ?> .wallbin-header .page-selector-container .tab-pages .page-tab-header.selected {
        color: <? echo Utils::formatColor($contentBlock->shortcut->style->header->tabSelector->selectedTextColor)?> !important;
        background-color: <? echo Utils::formatColor($contentBlock->shortcut->style->header->tabSelector->selectedBackColor)?> !important;
    }
</style>
<div id="<? echo $blockId; ?>" class="library-page-bundle-block">
    <div class="service-data wallbin-settings">
        <div class="encoded-data">
	        <? echo base64_encode(CJSON::encode(array(
		        'shortcutId' => $contentBlock->shortcut->id,
		        'wallbinId' => $contentBlock->shortcut->id,
		        'wallbinName' => $contentBlock->shortcut->title,
		        'pageViewType' => $contentBlock->shortcut->pageViewType,
		        'pageSelectorMode' => $contentBlock->shortcut->pageSelectorMode
	        ))
	        ); ?>
        </div>
    </div>
    <div class="wallbin-header-container">
        <table class="wallbin-header">
            <tr>
                <td class="wallbin-header-cell page-selector-container">
					<? if ($contentBlock->shortcut->pageSelectorMode == 'tabs'): ?>
                        <div class="tab-pages scroll_tabs_theme_light">
							<? foreach ($contentBlock->shortcut->items as $item): ?>
                                <span class="page-tab-header<? echo $selectedPage->libraryPage->id == $item->libraryPage->id ? ' selected' : ''; ?>">
								<? echo $item->name; ?>
                                    <div class="service-data">
                                        <div class="encoded-data">
                                            <? echo CJSON::encode(array(
                                                    'libraryId' => $item->libraryPage->libraryId,
                                                    'pageId' => $item->libraryPage->id,
		                                            'styleContainerType' => $item->shortcut->getStyleContainerType(),
                                                    'styleContainerId' => $item->shortcut->getStyleContainerId(),
                                                    'pageName' => $item->name
                                                )
                                            ); ?>
                                        </div>
								    </div>
				                </span>
							<? endforeach; ?>
                        </div>
					<? else: ?>
                        <select class="selectpicker bootstrapped">
							<? foreach ($contentBlock->shortcut->items as $item): ?>
                                <option value='<? echo base64_encode(CJSON::encode(array(
										'libraryId' => $item->libraryPage->libraryId,
										'pageId' => $item->libraryPage->id,
		                                'styleContainerType' => $item->shortcut->getStyleContainerType(),
										'styleContainerId' => $item->shortcut->getStyleContainerId(),
										'pageName' => $item->name
									)
								)); ?>' <? echo $selectedPage->libraryPage->id == $item->libraryPage->id ? 'selected' : ''; ?>><? echo $item->name; ?></option>
							<? endforeach; ?>
                        </select>
					<? endif; ?>
                </td>
            </tr>
        </table>
    </div>
    <div class="wallbin-container">
		<?
			if ($contentBlock->shortcut->pageViewType == 'accordion')
			{
				$this->renderPartial('../wallbin/accordionView', array('libraryPage' => $selectedPage->libraryPage));
			}
			else if ($this->isIOSDevice)
			{
				$selectedPage->libraryPage->loadFolders(true);
				$this->renderPartial('../wallbin/columnsView',
					array(
						'libraryPage' => $selectedPage->libraryPage,
						'containerId' => 'content',
						'style' => \application\models\wallbin\models\web\style\WallbinStyle::createDefault()
					));
			}
			else
			{
				$selectedPage->libraryPage->loadFolders(true);
				$this->renderPartial('../wallbin/columnsView',
					array(
						'libraryPage' => $selectedPage->libraryPage,
						'containerId' => $blockId,
						'style' => $selectedPage->shortcut->style->page
					));
			}
		?>
    </div>
</div>
