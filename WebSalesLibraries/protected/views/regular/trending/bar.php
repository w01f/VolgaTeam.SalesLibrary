<?
	use application\models\trending\TrendingControlSettings;

	/**
	 * @var string $barId
	 * @var \application\models\trending\TrendingSettings $trendingSettings
	 * @var \application\models\trending\TrendingLink[] $trendingLinks
	 */
?>
<style>
    <?if (!empty($trendingSettings->controlActiveColor)):?>
    .trending-controls-container .btn-group .btn.btn-default.active,
    .trending-controls-container .btn-group .btn.btn-default.active:focus,
    .trending-controls-container .btn-group .btn.btn-default.active:hover,
    .trending-controls-container .btn-group .btn.btn-default.active:focus:hover,
    .carousel-controls-container .btn.btn-default,
    .carousel-controls-container .btn.btn-default:hover,
    .carousel-controls-container .btn.btn-default:focus,
    .carousel-controls-container .btn.btn-default:focus:hover
    {
        background-color: <? echo '#'.$trendingSettings->controlActiveColor;?> !important;
        -webkit-box-shadow: none !important;
        box-shadow: none !important;
    }

    <?endif;?>
</style>
<div id="trending-bar-<? echo $barId; ?>" class="row trending-bar">
    <div class="service-data">
        <div class="encoded-object">
			<? echo CJSON::encode($trendingSettings); ?>
        </div>
    </div>
    <div class="col-xs-12">
        <div class="row">
            <div class="trending-controls-container col-md-10 hidden-xs hidden-sm">
                <div class="btn-group" role="group">
					<? if ($trendingSettings->controlSettings[TrendingControlSettings::ControlTagDateToday]->enabled ||
						$trendingSettings->controlSettings[TrendingControlSettings::ControlTagDateWeek]->enabled ||
						$trendingSettings->controlSettings[TrendingControlSettings::ControlTagDateMonth]->enabled
					): ?>
						<?
						$activeDateRangeTitle = 'Date Range';
						switch ($trendingSettings->dateRangeType)
						{
							case \application\models\trending\TrendingSettings::DataRangeTypeToday:
								$activeDateRangeTitle = $trendingSettings->controlSettings[TrendingControlSettings::ControlTagDateToday]->title;
								break;
							case \application\models\trending\TrendingSettings::DataRangeTypeWeek:
								$activeDateRangeTitle = $trendingSettings->controlSettings[TrendingControlSettings::ControlTagDateWeek]->title;
								break;
							case \application\models\trending\TrendingSettings::DataRangeTypeMonth:
								$activeDateRangeTitle = $trendingSettings->controlSettings[TrendingControlSettings::ControlTagDateMonth]->title;
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
									/** @var TrendingControlSettings $control */
									$control = $trendingSettings->controlSettings[TrendingControlSettings::ControlTagDateToday];
								?>
								<? if ($control->enabled): ?>
                                    <li class="date-range-toggle">
                                        <a href="#"><? echo $control->title; ?></a>
                                        <span class="service-data">
                                            <span class="date-range-tag"><? echo \application\models\trending\TrendingSettings::DataRangeTypeToday; ?></span>
                                        </span>
                                    </li>
								<? endif; ?>
								<?
									/** @var TrendingControlSettings $control */
									$control = $trendingSettings->controlSettings[TrendingControlSettings::ControlTagDateWeek];
								?>
								<? if ($control->enabled): ?>
                                    <li class="date-range-toggle">
                                        <a href="#"><? echo $control->title; ?></a>
                                        <span class="service-data">
                                            <span class="date-range-tag"><? echo \application\models\trending\TrendingSettings::DataRangeTypeWeek; ?></span>
                                        </span>
                                    </li>
								<? endif; ?>
								<?
									/** @var TrendingControlSettings $control */
									$control = $trendingSettings->controlSettings[TrendingControlSettings::ControlTagDateMonth];
								?>
								<? if ($control->enabled): ?>
                                    <li class="date-range-toggle">
                                        <a href="#"><? echo $control->title; ?></a>
                                        <span class="service-data">
                                            <span class="date-range-tag"><? echo \application\models\trending\TrendingSettings::DataRangeTypeMonth; ?></span>
                                        </span>
                                    </li>
								<? endif; ?>
                            </ul>
                        </div>
					<? endif; ?>
					<?
						/** @var TrendingControlSettings $control */
						$control = $trendingSettings->controlSettings[TrendingControlSettings::ControlTagLinkFormatPowerPoint];
					?>
					<? if ($control->enabled): ?>
                        <button type="button"
                                class="btn btn-default link-format-toggle<? if (in_array(\application\models\trending\TrendingSettings::LinkFormatPowerPoint, $trendingSettings->linkFormats)): ?> active<? endif; ?>">
                            <span class="title"><? echo $control->title; ?></span>
                            <span class="service-data">
                                <span class="link-format-tag"><? echo \application\models\trending\TrendingSettings::LinkFormatPowerPoint; ?></span>
                            </span>
                        </button>
					<? endif; ?>
					<?
						/** @var TrendingControlSettings $control */
						$control = $trendingSettings->controlSettings[TrendingControlSettings::ControlTagLinkFormatVideo];
					?>
					<? if ($control->enabled): ?>
                        <button type="button"
                                class="btn btn-default link-format-toggle<? if (in_array(\application\models\trending\TrendingSettings::LinkFormatVideo, $trendingSettings->linkFormats)): ?> active<? endif; ?>">
                            <span class="title"><? echo $control->title; ?></span>
                            <span class="service-data">
                                <span class="link-format-tag"><? echo \application\models\trending\TrendingSettings::LinkFormatVideo; ?></span>
                            </span>
                        </button>
					<? endif; ?>
					<?
						/** @var TrendingControlSettings $control */
						$control = $trendingSettings->controlSettings[TrendingControlSettings::ControlTagLinkFormatDocuments];
					?>
					<? if ($control->enabled): ?>
                        <button type="button"
                                class="btn btn-default link-format-toggle<? if (in_array(\application\models\trending\TrendingSettings::LinkFormatDocument, $trendingSettings->linkFormats)): ?> active<? endif; ?>">
                            <span class="title"><? echo $control->title; ?></span>
                            <span class="service-data">
                                <span class="link-format-tag"><? echo \application\models\trending\TrendingSettings::LinkFormatDocument; ?></span>
                            </span>
                        </button>
					<? endif; ?>
                </div>
            </div>
            <div class="carousel-controls-container col-md-2 col-sm-12 col-xs-12 text-right">
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
        </div>
    </div>
    <div class="row carousel-container">
		<? echo $this->renderPartial('../trending/linksList', array('barId' => $barId, 'trendingSettings' => $trendingSettings, 'links' => $trendingLinks), true); ?>
    </div>
</div>
