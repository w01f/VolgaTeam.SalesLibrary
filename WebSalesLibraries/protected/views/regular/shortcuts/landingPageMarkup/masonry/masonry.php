<?
	use application\models\shortcuts\models\landing_page\regular_markup\masonry\MasonryBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\masonry\MasonryShortcut;
	use application\models\shortcuts\models\landing_page\regular_markup\masonry\MasonryUrl;

	/** @var $contentBlock MasonryBlock */
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
        <div class="default-filter"><? echo isset($contentBlock->defaultFilter) ? sprintf('.%s', $contentBlock->defaultFilter->tags[0]) : '*'; ?></div>
    </div>
	<? if (count($contentBlock->filters) > 1): ?>
        <div id="masonry-filter-<? echo $contentBlock->id; ?>" class="cbp-l-filters-buttonCenter">
			<? foreach ($contentBlock->filters as $filter): ?>
                <div data-filter="<? echo '.' . implode(', .', $filter->tags) ?>"
                     class="cbp-filter-item<? if ($filter->isDefault): ?> cbp-filter-item-active<? endif; ?>">
					<?
						$filterTextId = sprintf("masonry-filter-item-text-%s", $filter->id);
						echo $this->renderPartial('landingPageMarkup/style/styleTextAppearance',
							array(
								'textAppearance' => $filter->textAppearance,
								'blockId' => $filterTextId
							)
							, true);
					?>
                    <span id="<? echo $filterTextId; ?>">
                        <? echo $filter->title; ?>
                    </span>
                    <div class="cbp-filter-counter"></div>
                </div>
			<? endforeach; ?>
        </div>
	<? endif; ?>
    <div id="masonry-grid-<? echo $contentBlock->id; ?>" class="cbp cbp-l-grid-masonry-projects">
		<? foreach ($contentBlock->items as $masonryItem): ?>
		<? if ($masonryItem->type === 'url'): ?>
		<? /** @var MasonryUrl $masonryItem */ ?>
        <a href="<? echo $masonryItem->url; ?>" target="_blank"
           class="cbp-item <? echo implode(' ', $masonryItem->filterTags); ?>">
			<? elseif ($masonryItem->type === 'shortcut'): ?>
		<? /** @var MasonryShortcut $masonryItem */ ?>
            <a href="<? echo isset($masonryItem->shortcut) ? $masonryItem->shortcut->getSourceLink() : '#'; ?>"
               class="cbp-item <? echo implode(' ', $masonryItem->filterTags); ?> shortcuts-link<? if (!isset($masonryItem->shortcut)): ?> disabled<? endif; ?>">
                <div class="service-data">
					<? echo isset($masonryItem->shortcut) ? $masonryItem->shortcut->getMenuItemData() : '<div class="same-page"></div><div class="has-custom-handler"></div>'; ?>
                </div>
				<? endif; ?>
                <div class="cbp-caption">
                    <div class="cbp-caption-defaultWrap">
                        <img src="<? echo $masonryItem->imagePath; ?>"
						     <? if ($masonryItem->imageWidth > 0 && $masonryItem->imageHeight > 0): ?>width="<? echo $masonryItem->imageWidth; ?>"
                             height="<? echo $masonryItem->imageHeight; ?>"<? endif; ?>>
                    </div>
                </div>
				<? if (!empty($masonryItem->title)): ?>
					<?
					$itemTitleId = sprintf("masonry-item-title-%s", $masonryItem->id);
					echo $this->renderPartial('landingPageMarkup/style/styleTextAppearance',
						array(
							'textAppearance' => $masonryItem->titleTextAppearance,
							'blockId' => $itemTitleId
						)
						, true);
					?>
                    <div id="<? echo $itemTitleId; ?>"
                         class="cbp-l-grid-masonry-projects-title"><? echo $masonryItem->title; ?></div>
				<? endif; ?>
				<? if (!empty($masonryItem->description)): ?>
					<?
					$itemDescriptionId = sprintf("masonry-item-description-%s", $masonryItem->id);
					echo $this->renderPartial('landingPageMarkup/style/styleTextAppearance',
						array(
							'textAppearance' => $masonryItem->descriptionTextAppearance,
							'blockId' => $itemDescriptionId
						)
						, true);
					?>
                    <div id="<? echo $itemDescriptionId; ?>"
                         class="cbp-l-grid-masonry-projects-desc"><? echo $masonryItem->description; ?></div>
				<? endif; ?>
            </a>
			<? endforeach; ?>
    </div>
</div>



