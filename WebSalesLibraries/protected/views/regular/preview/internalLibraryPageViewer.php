<?
	/**
	 * @var $data InternalLinkPreviewData
	 * @var $screenSettings array
	 */

	/** @var InternalLibraryPagePreviewInfo $previewInfo */
	$previewInfo = $data->previewInfo;
	$libraryPage = $previewInfo->getLibraryPage();

	if ($previewInfo->pageViewType == 'accordion')
		$content = $this->renderPartial(
			'../wallbin/accordionView',
			array(
				'libraryPage' => $libraryPage,
				'containerId' => 'content',
				'style' => $previewInfo->style->page,
				'screenSettings' => $screenSettings
			), true);
	else
		$content = $this->renderPartial(
			'../wallbin/columnsView',
			array(
				'libraryPage' => $libraryPage,
				'containerId' => 'content',
				'style' => $previewInfo->style->page,
				'screenSettings' => $screenSettings
			), true);
?>
<style>
    <? if (isset($previewInfo->style->header->padding) && $previewInfo->style->header->padding->isConfigured): ?>
    #content .wallbin-header-container {

        padding-top: <? echo $previewInfo->style->header->padding->top; ?>px !important;
        padding-left: <? echo $previewInfo->style->header->padding->left; ?>px !important;
        padding-bottom: <? echo $previewInfo->style->header->padding->bottom; ?>px !important;
        padding-right: <? echo $previewInfo->style->header->padding->right; ?>px !important;
    }

    <?endif;?>

    #content .wallbin-header {
        background-color: <? echo Utils::formatColor($previewInfo->style->header->backColor); ?> !important;
    }

    #content .wallbin-header .wallbin-header-cell {
        border-bottom: 1px <? echo Utils::formatColor($previewInfo->style->header->headerBorderColor); ?> solid !important;
    }

    #content .wallbin-header .single-page-header .header-text {
        color: <? echo Utils::formatColor($previewInfo->style->header->textColor); ?> !important;
    }

    <? if ($previewInfo->searchBar->configured): ?>
    #content .wallbin-header .shortcuts-search-bar-container {
        padding-right: 0;
        padding-top: 25px;
        padding-bottom: 0;
        vertical-align: top;
    }

    <? if ($previewInfo->style->header->showLogo && $previewInfo->style->header->showText): ?>
    #content .wallbin-header .shortcuts-search-bar-container {
        padding-bottom: 25px;
        width: 100%;
    }

    <? else: ?>
    #content .wallbin-header .shortcuts-search-bar-container {
        width: 70%;
    }

    #content .wallbin-header .wallbin-logo-wrapper {
        width: 30%;
        vertical-align: top;
    }

    #content .wallbin-header .single-page-header.single-page-header-no-logo {
        width: 30%;
        padding-top: 30px;
        vertical-align: top;
    }

    #content .wallbin-header .single-page-header.single-page-header-no-logo h4 {
        margin: 0;
    }

    <? endif; ?>
    <? endif; ?>
</style>
<div class="wallbin-header-container">
    <table class="wallbin-header">
		<? if ($previewInfo->style->header->showLogo && $previewInfo->style->header->showText): ?>
            <tr>
                <td class="wallbin-logo-wrapper<? if (!$previewInfo->searchBar->configured): ?> wallbin-header-cell<? endif; ?>">
                    <img class="wallbin-logo" src="<? echo $libraryPage->logoContent; ?>">
                </td>
                <td class="single-page-header<? if (!$previewInfo->searchBar->configured): ?> wallbin-header-cell<? endif; ?>">
                    <h3 class="header-text"><? echo $libraryPage->name; ?></h3>
                </td>
            </tr>
			<? if ($previewInfo->searchBar->configured): ?>
                <tr>
                    <td class="wallbin-header-cell shortcuts-search-bar-container" colspan="2">
						<? echo $this->renderPartial('../shortcuts/searchBar/bar', array('searchBar' => $previewInfo->searchBar), true); ?>
                    </td>
                </tr>
			<? endif; ?>
		<? else: ?>
            <tr>
				<? if ($previewInfo->style->header->showLogo): ?>
                    <td class="wallbin-header-cell wallbin-logo-wrapper">
                        <img class="wallbin-logo" src="<? echo $libraryPage->logoContent; ?>">
                    </td>
				<? endif; ?>
				<? if ($previewInfo->style->header->showText): ?>
                    <td class="wallbin-header-cell single-page-header single-page-header-no-logo">
                        <h4 class="header-text"><? echo $libraryPage->name; ?></h4>
                    </td>
				<? endif; ?>
				<? if ($previewInfo->searchBar->configured): ?>
                    <td class="wallbin-header-cell shortcuts-search-bar-container">
						<? echo $this->renderPartial('../shortcuts/searchBar/bar', array('searchBar' => $previewInfo->searchBar), true); ?>
                    </td>
				<? endif; ?>
            </tr>
		<? endif; ?>
    </table>
</div>
<div class="wallbin-container">
    <div class="service-data">
        <div class="encoded-data selected-page-data">
			<? echo CJSON::encode(array(
					'libraryId' => $libraryPage->libraryId,
					'pageId' => $libraryPage->id,
					'styleContainerType' => $previewInfo->getStyleContainerType(),
					'styleContainerId' => $previewInfo->getStyleContainerId(),
					'pageName' => $libraryPage->name,
					'logoContent' => $previewInfo->style->header->showLogo ? $libraryPage->logoContent : ''
				)
			); ?>
        </div>
    </div>
    <? echo $content; ?>
</div>