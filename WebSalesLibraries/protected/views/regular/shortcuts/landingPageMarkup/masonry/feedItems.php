<?
	use application\models\data_query\link_feed\LinkFeedItem;
	use application\models\feeds\common\FeedItemSettings;
	use application\models\shortcuts\models\landing_page\regular_markup\masonry\TrendingFeedSettings;

	/**
	 * @var string $feedId
	 * @var TrendingFeedSettings $viewSettings
	 * @var LinkFeedItem[] $feedItems
	 */
?>
<? foreach ($feedItems as $masonryItem): ?>
    <a href="#" class="cbp-item library-link-item" style="<? if ($viewSettings->imageWidth > 0): ?>max-width:<? echo $viewSettings->imageWidth; ?>px;<? endif; ?>">
        <div class="service-data">
            <div class="link-id"><? echo $masonryItem->linkId; ?></div>
        </div>
        <div class="cbp-caption">
            <div class="cbp-caption-defaultWrap">
                <img src="<? echo $masonryItem->thumbnail; ?>"
                     style="<? if ($viewSettings->imageWidth > 0): ?>max-width:<? echo $viewSettings->imageWidth; ?>px;<? endif; ?><? if ($viewSettings->imageHeight > 0): ?> max-height:<? echo $viewSettings->imageHeight; ?>px;<? endif; ?>">
            </div>
        </div>
		<?
			$itemSettingsName = $viewSettings->dataItemSettings->{FeedItemSettings::DataItemTagName};
			$itemSettingsLibrary = $viewSettings->dataItemSettings->{FeedItemSettings::DataItemTagLibrary};
			$itemSettingsViewsCount = $viewSettings->dataItemSettings->{FeedItemSettings::DataItemTagViewsCount};
		?>
		<? if ($itemSettingsName->enabled): ?>
            <div class="cbp-l-grid-masonry-projects-title" style="
                    text-align: <? echo $itemSettingsName->textAlign; ?>;
			<? if ($itemSettingsName->wrapText): ?>;
                    white-space: normal;
			<? endif; ?>
			<? if (!empty($itemSettingsName->color)): ?>
                    color: <? echo Utils::formatColor($itemSettingsName->color);?>;
             <? endif; ?>
			<? if (!empty($itemSettingsName->font)): ?>
                    font-family: <? echo FontReplacementHelper::replaceFont($itemSettingsName->font->name); ?> !important;
                    font-size: <? echo $itemSettingsName->font->size; ?>pt !important;
                    font-weight: <? echo $itemSettingsName->font->isBold ? 'bold' : 'normal'; ?> !important;
                    font-style: <? echo $itemSettingsName->font->isItalic ? 'italic' : 'normal'; ?> !important;
                    text-decoration: <? echo $itemSettingsName->font->isUnderlined ? 'underline' : 'inherit'; ?> !important;
			<? endif; ?>
                    ">
				<? echo $masonryItem->linkName; ?>
            </div>
		<? endif; ?>
		<? if ($itemSettingsLibrary->enabled): ?>
            <div class="cbp-l-grid-masonry-projects-desc" style="
                    text-align: <? echo $itemSettingsLibrary->textAlign; ?>;
			<? if ($itemSettingsLibrary->wrapText): ?>;
                    white-space: normal;
			<? endif; ?>
			<? if (!empty($itemSettingsLibrary->color)): ?>
                    color: <? echo Utils::formatColor($itemSettingsLibrary->color);?>;
            <? endif; ?>
			<? if (!empty($itemSettingsLibrary->font)): ?>
                    font-family: <? echo FontReplacementHelper::replaceFont($itemSettingsLibrary->font->name); ?> !important;
                    font-size: <? echo $itemSettingsLibrary->font->size; ?>pt !important;
                    font-weight: <? echo $itemSettingsLibrary->font->isBold ? 'bold' : 'normal'; ?> !important;
                    font-style: <? echo $itemSettingsLibrary->font->isItalic ? 'italic' : 'normal'; ?> !important;
                    text-decoration: <? echo $itemSettingsLibrary->font->isUnderlined ? 'underline' : 'inherit'; ?> !important;
			<? endif; ?>
                    ">
				<? echo $masonryItem->libraryName; ?>
            </div>
		<? endif; ?>
		<? if ($itemSettingsViewsCount->enabled && $masonryItem->viewsCount > 0): ?>
            <div class="cbp-l-grid-masonry-projects-desc" style="
                    text-align: <? echo $itemSettingsViewsCount->textAlign; ?>;
			<? if ($itemSettingsViewsCount->wrapText): ?>;
                    white-space: normal;
			<? endif; ?>
			<? if (!empty($itemSettingsViewsCount->color)): ?>
                    color: <? echo Utils::formatColor($itemSettingsViewsCount->color);?>;
            <? endif; ?>
			<? if (!empty($itemSettingsViewsCount->font)): ?>
                    font-family: <? echo FontReplacementHelper::replaceFont($itemSettingsViewsCount->font->name); ?> !important;
                    font-size: <? echo $itemSettingsViewsCount->font->size; ?>pt !important;
                    font-weight: <? echo $itemSettingsViewsCount->font->isBold ? 'bold' : 'normal'; ?> !important;
                    font-style: <? echo $itemSettingsViewsCount->font->isItalic ? 'italic' : 'normal'; ?> !important;
                    text-decoration: <? echo $itemSettingsViewsCount->font->isUnderlined ? 'underline' : 'inherit'; ?> !important;
			<? endif; ?>
                    ">
				<? echo $masonryItem->viewsCount; ?> views
            </div>
		<? endif; ?>
    </a>
<? endforeach; ?>