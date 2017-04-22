<?
	use application\models\data_query\link_feed\LinkFeedItem;
	use application\models\feeds\common\FeedItemSettings;
	use application\models\shortcuts\models\landing_page\regular_markup\masonry\SearchFeedBlock;

	/** @var $contentBlock SearchFeedBlock */

	/** @var LinkFeedItem[] $feedItems */
	$feedItems = $contentBlock->getFeedItems();
?>
<style>
    #masonry-container-<? echo $contentBlock->id; ?> .cbp-caption-zoom .cbp-caption:hover .cbp-caption-defaultWrap {
        -webkit-transform: scale(<?echo $contentBlock->viewSettings->captionZoomScale;?>) !important;
        transform: scale(<?echo $contentBlock->viewSettings->captionZoomScale;?>) !important;
    }
</style>
<div id="masonry-container-<? echo $contentBlock->id; ?>" class="col-xs-12 masonry-container">
    <div class="service-data">
        <div class="horizontal-gap"><? echo($contentBlock->viewSettings->itemsPadding->left + $contentBlock->viewSettings->itemsPadding->right); ?></div>
        <div class="vertical-gap"><? echo($contentBlock->viewSettings->itemsPadding->top + $contentBlock->viewSettings->itemsPadding->bottom); ?></div>
        <div class="caption"><? echo $contentBlock->viewSettings->enableCaptionZoom ? 'zoom' : ''; ?></div>
        <div class="default-filter">*</div>
    </div>
    <div id="masonry-grid-<? echo $contentBlock->id; ?>" class="cbp cbp-l-grid-masonry-projects">
		<? foreach ($feedItems as $masonryItem): ?>
            <a href="#" class="cbp-item library-link-item">
                <div class="service-data">
                    <div class="link-id"><? echo $masonryItem->linkId; ?></div>
                </div>
                <div class="cbp-caption">
                    <div class="cbp-caption-defaultWrap">
                        <img src="<? echo $masonryItem->thumbnail; ?>"
                             style="<? if ($contentBlock->viewSettings->imageWidth > 0): ?>max-width:<? echo $contentBlock->viewSettings->imageWidth; ?>px;<? endif; ?><? if ($contentBlock->viewSettings->imageHeight > 0): ?> max-height:<? echo $contentBlock->viewSettings->imageHeight; ?>px;<? endif; ?>">
                    </div>
                </div>
                <?
                    $itemSettingsName = $contentBlock->dataItemSettings->{FeedItemSettings::DataItemTagName};
                    $itemSettingsLibrary = $contentBlock->dataItemSettings->{FeedItemSettings::DataItemTagLibrary};
                    $itemSettingsViewsCount = $contentBlock->dataItemSettings->{FeedItemSettings::DataItemTagViewsCount};
                ?>
	            <? if ($itemSettingsName->enabled): ?>
                    <div class="cbp-l-grid-masonry-projects-title" style="
                            text-align: <? echo $itemSettingsName->textAlign; ?>;
		            <? if($itemSettingsName->wrapText): ?>;
                            white-space: normal;
		            <? endif; ?>
		            <? if (!empty($itemSettingsName->color)): ?>
                            color: <? echo '#' . $itemSettingsName->color;?>;
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
		            <? if($itemSettingsLibrary->wrapText): ?>;
                            white-space: normal;
		            <? endif; ?>
		            <? if (!empty($itemSettingsLibrary->color)): ?>
                            color: <? echo '#' . $itemSettingsLibrary->color;?>;
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
		            <? if($itemSettingsViewsCount->wrapText): ?>;
                            white-space: normal;
		            <? endif; ?>
		            <? if (!empty($itemSettingsViewsCount->color)): ?>
                            color: <? echo '#' . $itemSettingsViewsCount->color;?>;
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
    </div>
</div>



