<?
	use application\models\data_query\link_feed\LinkFeedItem;
	use application\models\data_query\link_feed\LinkFeedQuerySettings;
	use application\models\data_query\link_feed\SpecificLinkFeedQuerySettings;
	use application\models\feeds\common\FeedControlSettings;
	use application\models\feeds\common\FeedControlTag;
	use application\models\feeds\common\FeedDetailsControlSettings;
	use application\models\shortcuts\models\landing_page\regular_markup\masonry\SpecificLinkFeedBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\masonry\SpecificLinkFeedSettings;

	/** @var $contentBlock SpecificLinkFeedBlock */

	/** @var SpecificLinkFeedQuerySettings $querySettings */
	$querySettings = $contentBlock->querySettings;
	/** @var SpecificLinkFeedSettings $viewSettings */
	$viewSettings = $contentBlock->viewSettings;
	/** @var LinkFeedItem[] $feedItems */
	$feedItems = $contentBlock->getFeedItems();

	echo $this->renderPartial('landingPageMarkup/style/feedControlsStyle',
		array(
			'feedId' => 'masonry-container-' . $contentBlock->id,
			'style' => $viewSettings->controlsStyle
		)
		, true);
?>
<style>
    #masonry-container-<? echo $contentBlock->id; ?> .cbp-caption-zoom .cbp-caption:hover .cbp-caption-defaultWrap {
        -webkit-transform: scale(<?echo $viewSettings->captionZoomScale;?>) !important;
        transform: scale(<?echo $viewSettings->captionZoomScale;?>) !important;
    }
</style>
<div id="masonry-container-<? echo $contentBlock->id; ?>" class="col-xs-12 masonry-container">
    <div class="service-data">
        <div class="encoded-object">
            <div class="query-settings"><? echo CJSON::encode($querySettings); ?></div>
            <div class="view-settings"><? echo CJSON::encode($viewSettings); ?></div>
        </div>
    </div>
    <div class="btn-group feed-controls-container masonry-feed-controls-container" role="group">
		<?
			/** @var FeedControlSettings $control */
			$control = $viewSettings->controlSettings->{FeedControlTag::ControlTagLinkFormatPowerPoint};
		?>
		<? if ($control->enabled): ?>
            <button type="button"
                    class="btn btn-default link-format-toggle<? if (in_array(LinkFeedQuerySettings::LinkFormatPowerPoint, $querySettings->linkFormats)): ?> active<? endif; ?><? if ($control->hideCondition->large): ?> hidden-lg<? endif; ?>
                            <? if ($control->hideCondition->medium): ?> hidden-md<? endif; ?>
                            <? if ($control->hideCondition->small): ?> hidden-sm<? endif; ?>
                            <? if ($control->hideCondition->extraSmall): ?> hidden-xs<? endif; ?>">
                <span class="title"><? echo $control->title; ?></span>
                <span class="service-data">
                                <span class="link-format-tag"><? echo LinkFeedQuerySettings::LinkFormatPowerPoint; ?></span>
                            </span>
            </button>
		<? endif; ?>
		<?
			/** @var FeedControlSettings $control */
			$control = $viewSettings->controlSettings->{FeedControlTag::ControlTagLinkFormatVideo};
		?>
		<? if ($control->enabled): ?>
            <button type="button"
                    class="btn btn-default link-format-toggle<? if (in_array(LinkFeedQuerySettings::LinkFormatVideo, $querySettings->linkFormats)): ?> active<? endif; ?><? if ($control->hideCondition->large): ?> hidden-lg<? endif; ?>
                            <? if ($control->hideCondition->medium): ?> hidden-md<? endif; ?>
                            <? if ($control->hideCondition->small): ?> hidden-sm<? endif; ?>
                            <? if ($control->hideCondition->extraSmall): ?> hidden-xs<? endif; ?>">
                <span class="title"><? echo $control->title; ?></span>
                <span class="service-data">
                                <span class="link-format-tag"><? echo LinkFeedQuerySettings::LinkFormatVideo; ?></span>
                            </span>
            </button>
		<? endif; ?>
		<?
			/** @var FeedControlSettings $control */
			$control = $viewSettings->controlSettings->{FeedControlTag::ControlTagLinkFormatDocuments};
		?>
		<? if ($control->enabled): ?>
            <button type="button"
                    class="btn btn-default link-format-toggle<? if (in_array(LinkFeedQuerySettings::LinkFormatDocument, $querySettings->linkFormats)): ?> active<? endif; ?><? if ($control->hideCondition->large): ?> hidden-lg<? endif; ?>
                            <? if ($control->hideCondition->medium): ?> hidden-md<? endif; ?>
                            <? if ($control->hideCondition->small): ?> hidden-sm<? endif; ?>
                            <? if ($control->hideCondition->extraSmall): ?> hidden-xs<? endif; ?>">
                <span class="title"><? echo $control->title; ?></span>
                <span class="service-data">
                                <span class="link-format-tag"><? echo LinkFeedQuerySettings::LinkFormatDocument; ?></span>
                            </span>
            </button>
		<? endif; ?>
	    <?
		    /** @var FeedDetailsControlSettings $control */
		    $control = $viewSettings->controlSettings->{FeedControlTag::ControlTagDetailsButton};
	    ?>
	    <? if ($control->enabled): ?>
		    <? if (!empty($viewSettings->controlsStyle->regularTextColor) || !empty($control->iconColor)): ?>
                <style>
                    #masonry-container-<? echo $contentBlock->id; ?> .feed-details-button .svg path
                    {
                        fill: <? echo Utils::formatColor(!empty($control->iconColor)?$control->iconColor:$viewSettings->controlsStyle->regularTextColor);?> !important;
                    }
                </style>
		    <? endif; ?>
            <button type="button"
                    class="btn btn-default feed-details-button<? if ($control->hideCondition->large): ?> hidden-lg<? endif; ?>
                            <? if ($control->hideCondition->medium): ?> hidden-md<? endif; ?>
                            <? if ($control->hideCondition->small): ?> hidden-sm<? endif; ?>
                            <? if ($control->hideCondition->extraSmall): ?> hidden-xs<? endif; ?>" style="
		    <? if (!empty($control->backColor)): ?>background-color: <? echo Utils::formatColor($control->backColor); ?> !important;<? endif; ?>
		    <? if (!empty($control->borderColor)): ?>border-color: <? echo Utils::formatColor($control->borderColor); ?> !important;<? endif; ?>">
                <img src="<? echo $contentBlock->imagePath . $control->iconFile; ?>"
			         <? if (strpos($control->iconFile, '.svg') !== false): ?>class="svg"<? endif; ?>>
                <span class="service-data">
                    <span class="default-url"><? echo $contentBlock->detailsSettings->detailsUrl; ?></span>
                    <span class="hover-tip-template"><? echo $control->hoverTip; ?></span>
                </span>
            </button>
	    <? endif; ?>
    </div>
    <div id="masonry-grid-<? echo $contentBlock->id; ?>" class="cbp cbp-l-grid-masonry-projects">
	    <? echo $this->renderPartial('landingPageMarkup/masonry/feedItems', array('feedId' => $contentBlock->id, 'viewSettings' => $viewSettings, 'feedItems' => $feedItems), true); ?>
    </div>
</div>



