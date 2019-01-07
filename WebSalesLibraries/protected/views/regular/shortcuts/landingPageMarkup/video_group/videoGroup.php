<?

	use application\models\shortcuts\models\landing_page\regular_markup\video_group\VideoGroupBlock;

	/** @var $contentBlock VideoGroupBlock */

	$blockId = sprintf('video-group-%s', $contentBlock->id);

	$textAppearance = $contentBlock->textAppearance;

	$columnSizeExtraSmall = 12 / $contentBlock->settings->columnsCount->extraSmall;
	$columnSizeSmall = 12 / $contentBlock->settings->columnsCount->small;
	$columnSizeMedium = 12 / $contentBlock->settings->columnsCount->medium;
	$columnSizeLarge = 12 / $contentBlock->settings->columnsCount->large;
?>
<style>
    <?echo '#'.$blockId;?>
    .video-item {
        margin: 15px;
        cursor: pointer;
    }

    .video-item .suspended {
        cursor: default;
    }

    <?echo '#'.$blockId;?>
    .video-item .placeholder {
        min-height: <? echo $contentBlock->settings->itemHeight->extraSmall; ?>px;
        height: <? echo $contentBlock->settings->itemHeight->extraSmall; ?>px;
    }

    @media (min-width: 768px) {
    <?echo '#'.$blockId;?> .video-item .placeholder {
        min-height: <? echo $contentBlock->settings->itemHeight->small; ?>px;
        height: <? echo $contentBlock->settings->itemHeight->small; ?>px;
    }
    }

    @media (min-width: 992px) {
    <?echo '#'.$blockId;?> .video-item .placeholder {
        min-height: <? echo $contentBlock->settings->itemHeight->medium; ?>px;
        height: <? echo $contentBlock->settings->itemHeight->medium; ?>px;
    }
    }

    @media (min-width: 1200px) {
    <?echo '#'.$blockId;?> .video-item .placeholder {
        min-height: <? echo $contentBlock->settings->itemHeight->large; ?>px;
        height: <? echo $contentBlock->settings->itemHeight->large; ?>px;
    }
    }
</style>
<div id="<? echo $blockId; ?>" class="row landing-page-video-group">
	<? foreach ($contentBlock->items as $videoItem): ?>
        <div class="col text-center col-lg-<? echo $columnSizeLarge; ?> col-md-<? echo $columnSizeMedium; ?> col-sm-<? echo $columnSizeSmall; ?> col-xs-<? echo $columnSizeExtraSmall; ?>">
            <div class="video-item<? if ($videoItem->index > 0): ?> suspended<? endif; ?>" data-index="<? echo $videoItem->index; ?>" data-source="<? echo $videoItem->getVideoUrl(); ?>">
                <img class="placeholder" src="<? echo $videoItem->getVideoPlaceholder(); ?>">
                <div class="service-data video-item-data"></div>
            </div>
        </div>
	<? endforeach; ?>
    <div class="service-data video-group-data">
		<? echo CJSON::encode(array(
			'groupId' => $contentBlock->id,
			'shortcutId' => $contentBlock->parentShortcut->id,
		)) ?>
    </div>
</div>