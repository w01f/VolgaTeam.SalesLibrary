<?
	use application\models\data_query\link_feed\LinkFeedItem;
	use application\models\data_query\link_feed\LinkFeedQuerySettings;
	use application\models\data_query\link_feed\TrendingFeedQuerySettings;
	use application\models\feeds\common\FeedControlSettings;
	use application\models\feeds\common\TrendingFeedControlSettings;
	use application\models\shortcuts\models\landing_page\regular_markup\masonry\TrendingBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\masonry\TrendingFeedSettings;

	/** @var $contentBlock TrendingBlock */

	/** @var TrendingFeedQuerySettings $querySettings */
	$querySettings = $contentBlock->querySettings;
	/** @var TrendingFeedSettings $viewSettings */
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
		<? if ($viewSettings->controlSettings->{TrendingFeedControlSettings::ControlTagDateToday}->enabled ||
			$viewSettings->controlSettings->{TrendingFeedControlSettings::ControlTagDateWeek}->enabled ||
			$viewSettings->controlSettings->{TrendingFeedControlSettings::ControlTagDateMonth}->enabled
		): ?>
			<?
			$activeDateRangeTitle = 'Date Range';
			switch ($querySettings->dateRangeType)
			{
				case TrendingFeedQuerySettings::DataRangeTypeToday:
					$activeDateRangeTitle = $viewSettings->controlSettings->{TrendingFeedControlSettings::ControlTagDateToday}->title;
					break;
				case TrendingFeedQuerySettings::DataRangeTypeWeek:
					$activeDateRangeTitle = $viewSettings->controlSettings->{TrendingFeedControlSettings::ControlTagDateWeek}->title;
					break;
				case TrendingFeedQuerySettings::DataRangeTypeMonth:
					$activeDateRangeTitle = $viewSettings->controlSettings->{TrendingFeedControlSettings::ControlTagDateMonth}->title;
					break;
			}
			?>
			<?
			/** @var TrendingFeedControlSettings $todayControl */
			$todayControl = $viewSettings->controlSettings->{TrendingFeedControlSettings::ControlTagDateToday};
			/** @var TrendingFeedControlSettings $weekControl */
			$weekControl = $viewSettings->controlSettings->{TrendingFeedControlSettings::ControlTagDateWeek};
			/** @var TrendingFeedControlSettings $monthControl */
			$monthControl = $viewSettings->controlSettings->{TrendingFeedControlSettings::ControlTagDateMonth};

			$dateHideLg = $todayControl->hideCondition->large || $weekControl->hideCondition->large || $monthControl->hideCondition->large;
			$dateHideMd = $todayControl->hideCondition->medium || $weekControl->hideCondition->medium || $monthControl->hideCondition->medium;
			$dateHideSm = $todayControl->hideCondition->small || $weekControl->hideCondition->small || $monthControl->hideCondition->small;
			$dateHideXs = $todayControl->hideCondition->extraSmall || $weekControl->hideCondition->extraSmall || $monthControl->hideCondition->extraSmall;
			?>
            <div class="btn-group date-range-toggle-group<? if ($dateHideLg): ?> hidden-lg<? endif; ?>
                            <? if ($dateHideMd): ?> hidden-md<? endif; ?>
                            <? if ($dateHideSm): ?> hidden-sm<? endif; ?>
                            <? if ($dateHideXs): ?> hidden-xs<? endif; ?>" role="group">
                <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown"
                        aria-haspopup="true" aria-expanded="false">
                    <span class="title"><? echo $activeDateRangeTitle; ?></span>
                    <span class="caret"></span>
                </button>
                <ul class="dropdown-menu">
					<?
						/** @var TrendingFeedControlSettings $control */
						$control = $viewSettings->controlSettings->{TrendingFeedControlSettings::ControlTagDateToday};
					?>
					<? if ($control->enabled): ?>
                        <li class="date-range-toggle">
                            <a href="#"><? echo $control->title; ?></a>
                            <span class="service-data">
                                            <span class="date-range-tag"><? echo TrendingFeedQuerySettings::DataRangeTypeToday; ?></span>
                                        </span>
                        </li>
					<? endif; ?>
					<?
						/** @var TrendingFeedControlSettings $control */
						$control = $viewSettings->controlSettings->{TrendingFeedControlSettings::ControlTagDateWeek};
					?>
					<? if ($control->enabled): ?>
                        <li class="date-range-toggle">
                            <a href="#"><? echo $control->title; ?></a>
                            <span class="service-data">
                                            <span class="date-range-tag"><? echo TrendingFeedQuerySettings::DataRangeTypeWeek; ?></span>
                                        </span>
                        </li>
					<? endif; ?>
					<?
						/** @var TrendingFeedControlSettings $control */
						$control = $viewSettings->controlSettings->{TrendingFeedControlSettings::ControlTagDateMonth};
					?>
					<? if ($control->enabled): ?>
                        <li class="date-range-toggle">
                            <a href="#"><? echo $control->title; ?></a>
                            <span class="service-data">
                                            <span class="date-range-tag"><? echo TrendingFeedQuerySettings::DataRangeTypeMonth; ?></span>
                                        </span>
                        </li>
					<? endif; ?>
                </ul>
            </div>
		<? endif; ?>
		<?
			/** @var FeedControlSettings $control */
			$control = $viewSettings->controlSettings->{FeedControlSettings::ControlTagLinkFormatPowerPoint};
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
			$control = $viewSettings->controlSettings->{FeedControlSettings::ControlTagLinkFormatVideo};
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
			$control = $viewSettings->controlSettings->{FeedControlSettings::ControlTagLinkFormatDocuments};
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
    </div>
    <div id="masonry-grid-<? echo $contentBlock->id; ?>" class="cbp cbp-l-grid-masonry-projects">
		<? echo $this->renderPartial('landingPageMarkup/masonry/feedItems', array('feedId' => $contentBlock->id, 'viewSettings' => $viewSettings, 'feedItems' => $feedItems), true); ?>
    </div>
</div>



