<?
	use application\models\data_query\link_feed\LinkFeedItem;
	use application\models\data_query\link_feed\LinkFeedQuerySettings;
	use application\models\data_query\link_feed\TrendingFeedQuerySettings;
	use application\models\feeds\common\FeedControlSettings;
	use application\models\feeds\common\TrendingFeedControlSettings;
	use application\models\feeds\horizontal\TrendingFeedSettings;
	use application\models\shortcuts\models\landing_page\regular_markup\horizontal_feed\TrendingBlock;

	/** @var $contentBlock TrendingBlock */

	$feedId = $contentBlock->id;

	/** @var TrendingFeedQuerySettings $querySettings */
	$querySettings = $contentBlock->querySettings;
	/** @var TrendingFeedSettings $viewSettings */
	$viewSettings = $contentBlock->viewSettings;
	/** @var LinkFeedItem[] $feedItems */
	$feedItems = $contentBlock->getFeedItems();
?>

<style>
	<?if (!empty($viewSettings->controlActiveColor)):?>
    #horizontal-feed-<? echo $feedId; ?> .horizontal-feed-controls-container .btn-group .btn.btn-default.active,
    #horizontal-feed-<? echo $feedId; ?> .horizontal-feed-controls-container .btn-group .btn.btn-default.active:focus,
    #horizontal-feed-<? echo $feedId; ?> .horizontal-feed-controls-container .btn-group .btn.btn-default.active:hover,
    #horizontal-feed-<? echo $feedId; ?> .horizontal-feed-controls-container .btn-group .btn.btn-default.active:focus:hover,
    #horizontal-feed-<? echo $feedId; ?> .carousel-controls-container .btn.btn-default,
    #horizontal-feed-<? echo $feedId; ?> .carousel-controls-container .btn.btn-default:hover,
    #horizontal-feed-<? echo $feedId; ?> .carousel-controls-container .btn.btn-default:focus,
    #horizontal-feed-<? echo $feedId; ?> .carousel-controls-container .btn.btn-default:focus:hover {
		background-color: <? echo '#'.$viewSettings->controlActiveColor;?> !important;
		-webkit-box-shadow: none !important;
		box-shadow: none !important;
	}

	<?endif;?>
</style>
<div id="horizontal-feed-<? echo $feedId; ?>" class="row horizontal-feed">
	<div class="service-data">
		<div class="encoded-object">
			<div class="query-settings"><? echo CJSON::encode($querySettings); ?></div>
			<div class="view-settings"><? echo CJSON::encode($viewSettings); ?></div>
		</div>
	</div>
	<div class="col-xs-12">
		<div class="row">
			<div class="horizontal-feed-controls-container col-md-8 hidden-xs hidden-sm">
				<div class="btn-group" role="group">
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
						<div class="btn-group date-range-toggle-group" role="group">
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
						        class="btn btn-default link-format-toggle<? if (in_array(LinkFeedQuerySettings::LinkFormatPowerPoint, $querySettings->linkFormats)): ?> active<? endif; ?>">
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
						        class="btn btn-default link-format-toggle<? if (in_array(LinkFeedQuerySettings::LinkFormatVideo, $querySettings->linkFormats)): ?> active<? endif; ?>">
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
						        class="btn btn-default link-format-toggle<? if (in_array(LinkFeedQuerySettings::LinkFormatDocument, $querySettings->linkFormats)): ?> active<? endif; ?>">
							<span class="title"><? echo $control->title; ?></span>
							<span class="service-data">
                                <span class="link-format-tag"><? echo LinkFeedQuerySettings::LinkFormatDocument; ?></span>
                            </span>
						</button>
					<? endif; ?>
				</div>
			</div>
			<?
				/** @var FeedControlSettings $control */
				$control = $viewSettings->controlSettings->{FeedControlSettings::ControlTagScrollButton};
			?>
			<? if ($control->enabled && count($feedItems) > 1): ?>
				<div class="carousel-controls-container col-md-4 col-sm-12 col-xs-12 text-right
                    <? if ($control->hideCondition->large): ?> hidden-lg<? endif; ?>
                    <? if ($control->hideCondition->medium): ?> hidden-md<? endif; ?>
                    <? if ($control->hideCondition->small): ?> hidden-sm<? endif; ?>
                    <? if ($control->hideCondition->extraSmall): ?> hidden-xs<? endif; ?>">
					<button class="btn btn-default portfolio_utube_carousel_control_left"
					        role="button" data-slide="prev">
						<span class="fa fa-angle-left portfolio_utube_carousel_control_icons" aria-hidden="true"></span>
						<span class="sr-only">Previous</span>
					</button>
					<button class="btn btn-default portfolio_utube_carousel_control_right"
					        role="button" data-slide="next">
                        <span class="fa fa-angle-right portfolio_utube_carousel_control_icons"
                              aria-hidden="true"></span>
						<span class="sr-only">Next</span>
					</button>
				</div>
			<? endif; ?>
		</div>
	</div>
	<div class="row carousel-container">
		<? echo $this->renderPartial('landingPageMarkup/horizontal_feed/feedItems', array('feedId' => $feedId, 'viewSettings' => $viewSettings, 'feedItems' => $feedItems), true); ?>
	</div>
</div>