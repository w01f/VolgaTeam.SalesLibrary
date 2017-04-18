<?
	/** @var $contentBlock \application\models\shortcuts\models\landing_page\regular_markup\MasonryBlock */
?>
<div id="masonry-container-<? echo $contentBlock->id; ?>" class="col-xs-12 masonry-container">
    <div class="service-data">
        <div class="horizontal-gap"><? echo($contentBlock->itemsPadding->left + $contentBlock->itemsPadding->right); ?></div>
        <div class="vertical-gap"><? echo($contentBlock->itemsPadding->top + $contentBlock->itemsPadding->bottom); ?></div>
    </div>
	<? if (count($contentBlock->filters) > 1): ?>
        <div id="masonry-filter-<? echo $contentBlock->id; ?>" class="cbp-l-filters-buttonCenter">
			<? foreach ($contentBlock->filters as $filter): ?>
                <div data-filter="<? echo '.' . implode(', .', $filter->tags) ?>"
                     class="cbp-filter-item">
					<? echo $filter->title; ?>
                    <div class="cbp-filter-counter"></div>
                </div>
			<? endforeach; ?>
        </div>
	<? endif; ?>
    <div id="masonry-grid-<? echo $contentBlock->id; ?>" class="cbp cbp-l-grid-masonry-projects">
		<? foreach ($contentBlock->items as $masonryItem): ?>
			<? if ($masonryItem->type === 'url'): ?>
	            <? /** @var \application\models\shortcuts\models\landing_page\regular_markup\MasonryUrl $masonryItem */ ?>
                <a href="<? echo $masonryItem->url; ?>" target="_blank" class="cbp-item <? echo implode(' ', $masonryItem->filterTags); ?>">
		    <? elseif ($masonryItem->type === 'shortcut'): ?>
		        <? /** @var \application\models\shortcuts\models\landing_page\regular_markup\MasonryShortcut $masonryItem */ ?>
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
                    <div class="cbp-l-grid-masonry-projects-title"><? echo $masonryItem->title; ?></div>
				<? endif; ?>
				<? if (!empty($masonryItem->description)): ?>
                    <div class="cbp-l-grid-masonry-projects-desc"><? echo $masonryItem->description; ?></div>
				<? endif; ?>
            </a>
		<? endforeach; ?>
    </div>
</div>



