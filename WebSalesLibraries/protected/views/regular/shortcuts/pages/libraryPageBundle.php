<?
	/**
	 * @var $shortcut LibraryPageBundleShortcut
	 */

	/** @var LibraryPageBundleItem $selectedPage */
	$selectedPage = null;
	$savedSelectedPageIdTag = sprintf('SelectedLibraryPageId-%s', $shortcut->id);
	if (isset(Yii::app()->request->cookies[$savedSelectedPageIdTag]))
	{
		$selectedPageId = Yii::app()->request->cookies[$savedSelectedPageIdTag]->value;
		foreach ($shortcut->items as $item)
			if ($item->libraryPage->id == $selectedPageId)
			{
				$selectedPage = $item;
				break;
			}
	}
	if (!isset($selectedPage))
		$selectedPage = $shortcut->items[0];
?>
<style>
    <? if (isset($shortcut->style->header->padding) && $shortcut->style->header->padding->isConfigured): ?>
    #content .wallbin-header-container {

        padding-top: <? echo $shortcut->style->header->padding->top; ?>px !important;
        padding-left: <? echo $shortcut->style->header->padding->left; ?>px !important;
        padding-bottom: <? echo $shortcut->style->header->padding->bottom; ?>px !important;
        padding-right: <? echo $shortcut->style->header->padding->right; ?>px !important;
    }

    <?endif;?>

    #content .wallbin-header {
        background-color: <? echo Utils::formatColor($shortcut->style->header->backColor); ?> !important;
    }

    #content .wallbin-header .wallbin-header-cell {
        border-bottom: 1px <? echo Utils::formatColor($shortcut->style->header->headerBorderColor); ?> solid !important;
    }

    <? if ($shortcut->searchBar->configured): ?>
    #content .wallbin-header .shortcuts-search-bar-container {
        padding: 0 0 25px;
        width: 100%;
        vertical-align: top;
    }

    <? endif; ?>

    #content .wallbin-header .page-selector-container .tab-pages div,
    #content .wallbin-header .page-selector-container .tab-pages span,
    #content .wallbin-header .page-selector-container .tab-pages li {
        background-color: <? echo Utils::formatColor($shortcut->style->header->tabSelector->regularBackColor)?> !important;
        border-color: <? echo Utils::formatColor($shortcut->style->header->tabSelector->borderColor)?> !important;
    }

    #content .wallbin-header .page-selector-container .tab-pages div.scroll_tab_inner span,
    #content .wallbin-header .page-selector-container .tab-pages div.scroll_tab_inner li {
        color: <? echo Utils::formatColor($shortcut->style->header->tabSelector->regularTextColor)?> !important;
    }

    #content .wallbin-header .page-selector-container .tab-pages .scroll_tab_left_button,
    #content .wallbin-header .page-selector-container .tab-pages .scroll_tab_right_button {
        color: <? echo Utils::formatColor($shortcut->style->header->tabSelector->arrowColor)?> !important;
    }

    #content .wallbin-header .page-selector-container .tab-pages div.scroll_tab_inner span.scroll_tab_over,
    #content .wallbin-header .page-selector-container .tab-pages div.scroll_tab_inner li.scroll_tab_over {
        color: <? echo Utils::formatColor($shortcut->style->header->tabSelector->hoverTextColor)?> !important;
        background-color: <? echo Utils::formatColor($shortcut->style->header->tabSelector->hoverBackColor)?> !important;
    }

    #content .wallbin-header .page-selector-container .tab-pages .page-tab-header.selected {
        color: <? echo Utils::formatColor($shortcut->style->header->tabSelector->selectedTextColor)?> !important;
        background-color: <? echo Utils::formatColor($shortcut->style->header->tabSelector->selectedBackColor)?> !important;
    }
</style>
<div class="wallbin-header-container">
    <table class="wallbin-header">
		<? if ($shortcut->searchBar->configured): ?>
            <tr>
                <td class="shortcuts-search-bar-container">
					<? echo $this->renderPartial('../shortcuts/searchBar/bar', array('searchBar' => $shortcut->searchBar), true); ?>
                </td>
            </tr>
		<? endif; ?>
        <tr>
            <td class="wallbin-header-cell page-selector-container">
				<? if ($shortcut->pageSelectorMode == 'tabs'): ?>
                    <div class="tab-pages scroll_tabs_theme_light">
						<? foreach ($shortcut->items as $item): ?>
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
						<? foreach ($shortcut->items as $item): ?>
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
		if ($shortcut->pageViewType == 'accordion')
		{
			$this->renderPartial('../wallbin/accordionView',
                array(
                    'libraryPage' => $selectedPage->libraryPage,
	                'containerId' => 'content',
	                'style' => $selectedPage->shortcut->style->page
                )
            );
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
					'containerId' => 'content',
					'style' => $selectedPage->shortcut->style->page
				));
		}
	?>
</div>