<?

	use application\models\shortcuts\models\landing_page\regular_markup\masonry\MasonryBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\masonry\MasonryItem;
	use application\models\shortcuts\models\landing_page\regular_markup\masonry\MasonryShortcut;
	use application\models\shortcuts\models\landing_page\regular_markup\masonry\MasonryUrl;

	/** @var $contentBlock MasonryBlock */
?>
<style>
    #masonry-container-<? echo $contentBlock->id; ?> .cbp-caption-zoom .cbp-caption:hover .cbp-caption-defaultWrap {
        -webkit-transform: scale(<?echo $contentBlock->viewSettings->captionZoomScale;?>) !important;
        transform: scale(<?echo $contentBlock->viewSettings->captionZoomScale;?>) !important;
    }

    #masonry-container-<? echo $contentBlock->id; ?> .cbp-l-filters-buttonCenter .cbp-filter-item {
        background-color: <?echo Utils::formatColor($contentBlock->viewSettings->buttonStyle->backColorRegular);?> !important;
        border-color: <?echo $contentBlock->viewSettings->buttonStyle->hasBorder? Utils::formatColor($contentBlock->viewSettings->buttonStyle->borderColorRegular):'transparent';?> !important;
    }

    #masonry-container-<? echo $contentBlock->id; ?> .cbp-l-filters-buttonCenter .cbp-filter-item.cbp-filter-item-active {
        background-color: <?echo Utils::formatColor($contentBlock->viewSettings->buttonStyle->backColorSelected);?> !important;
        border-color: <?echo $contentBlock->viewSettings->buttonStyle->hasBorder? Utils::formatColor($contentBlock->viewSettings->buttonStyle->borderColorSelected):'transparent';?> !important;
    }
</style>
<div id="masonry-container-<? echo $contentBlock->id; ?>" class="col-xs-12 masonry-container">
    <div class="service-data">
        <div class="encoded-object">
            <div class="view-settings"><? echo CJSON::encode($contentBlock->viewSettings); ?></div>
        </div>
    </div>
	<? if (count($contentBlock->viewSettings->filters) > 1): ?>
        <div id="masonry-filter-<? echo $contentBlock->id; ?>" class="cbp-l-filters-buttonCenter">
			<? foreach ($contentBlock->viewSettings->filters as $filter): ?>
				<?
				$filterTextId = sprintf("masonry-filter-item-text-%s", $filter->id);
				echo $this->renderPartial('landingPageMarkup/style/styleTextAppearance',
					array(
						'textAppearance' => $filter->textAppearance,
						'blockId' => $filterTextId,
                        'selectedClass'=>'cbp-filter-item-active'
					)
					, true);
				?>
                <div id="<? echo $filterTextId; ?>" data-filter="<? echo '.' . implode(', .', $filter->tags) ?>"
                     class="cbp-filter-item<? if ($filter->isDefault): ?> cbp-filter-item-active<? endif; ?>">
                    <span>
                        <? echo $filter->title; ?>
                    </span>
                    <div class="cbp-filter-counter"></div>
                </div>
			<? endforeach; ?>
        </div>
	<? endif; ?>
    <div id="masonry-grid-<? echo $contentBlock->id; ?>" class="cbp cbp-l-grid-masonry-projects">
		<? foreach ($contentBlock->items as $masonryItem): ?>
		<?
			/** @var MasonryItem $masonryItem */
			$itemStyle = '';
			if (((isset($masonryItem->titleTextAppearance) && $masonryItem->titleTextAppearance->wrapText) || (isset($masonryItem->descriptionTextAppearance) && $masonryItem->descriptionTextAppearance->wrapText)) && $masonryItem->imageWidth > 0)
				$itemStyle = sprintf('style="width: %spx;"', $masonryItem->imageWidth);
		?>
		<? if ($masonryItem->type === 'url'): ?>
		<? /** @var MasonryUrl $masonryItem */ ?>
        <a href="<? echo $masonryItem->url; ?>" <? if ($masonryItem->isMailTo !== false): ?>target="_self"
		   <? else: ?>target="_blank"<? endif; ?>
           class="cbp-item <? echo implode(' ', $masonryItem->filterTags); ?>" <? echo $itemStyle; ?>>
			<? elseif ($masonryItem->type === 'shortcut'): ?>
		<? /** @var MasonryShortcut $masonryItem */ ?>
            <a href="<? echo isset($masonryItem->shortcut) ? $masonryItem->shortcut->getSourceLink() : '#'; ?>"
               target="<? echo isset($masonryItem->shortcut) && !$masonryItem->shortcut->samePage ? '_blank' : '_self'; ?>"
               class="cbp-item <? echo implode(' ', $masonryItem->filterTags); ?> shortcuts-link<? if (!isset($masonryItem->shortcut)): ?> disabled<? endif; ?>" <? echo $itemStyle; ?>>
                <div class="service-data">
					<? echo isset($masonryItem->shortcut) ? $masonryItem->shortcut->getMenuItemData() : '<div class="same-page"></div><div class="has-custom-handler"></div>'; ?>
                </div>
				<? endif; ?>
                <div class="cbp-caption">
                    <div class="cbp-caption-defaultWrap">
                        <img src="<? echo $masonryItem->imagePath; ?>"
                             style="<? if ($masonryItem->imageWidth > 0): ?>max-width:<? echo $masonryItem->imageWidth; ?>px;<? endif; ?><? if ($masonryItem->imageHeight > 0): ?> max-height:<? echo $masonryItem->imageHeight; ?>px;<? endif; ?>">
                    </div>
                </div>
                <div style="<? echo $this->renderPartial('../shortcuts/landingPageMarkup/style/stylePadding', array('padding' => $contentBlock->viewSettings->textPadding), true); ?>">
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
                </div>
            </a>
			<? endforeach; ?>
    </div>
</div>



