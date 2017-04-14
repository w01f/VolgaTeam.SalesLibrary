<?

	/**
	 * @var string $feedId
	 * @var \application\models\link_feed\SearchFeedSettings $feedSettings
	 * @var \application\models\link_feed\LinkFeedItem[] $feedItems
	 */
?>
<style>
    <?if (!empty($feedSettings->controlActiveColor)):?>
    .link-feed-controls-container .btn-group .btn.btn-default.active,
    .link-feed-controls-container .btn-group .btn.btn-default.active:focus,
    .link-feed-controls-container .btn-group .btn.btn-default.active:hover,
    .link-feed-controls-container .btn-group .btn.btn-default.active:focus:hover,
    .carousel-controls-container .btn.btn-default,
    .carousel-controls-container .btn.btn-default:hover,
    .carousel-controls-container .btn.btn-default:focus,
    .carousel-controls-container .btn.btn-default:focus:hover {
        background-color: <? echo '#'.$feedSettings->controlActiveColor;?> !important;
        -webkit-box-shadow: none !important;
        box-shadow: none !important;
    }

    <?endif;?>
</style>
<div id="link-feed-<? echo $feedId; ?>" class="row link-feed">
    <div class="service-data">
        <div class="encoded-object">
			<? echo CJSON::encode($feedSettings); ?>
        </div>
    </div>
    <div class="col-xs-12">
        <div class="row">
            <div class="link-feed-controls-container col-md-10 hidden-xs hidden-sm">
                <div class="btn-group" role="group">
					<?
						/** @var \application\models\link_feed\FeedControlSettings $control */
						$control = $feedSettings->controlSettings->{\application\models\link_feed\FeedControlSettings::ControlTagLinkFormatPowerPoint};
					?>
					<? if ($control->enabled): ?>
                        <button type="button"
                                class="btn btn-default link-format-toggle<? if (in_array(\application\models\link_feed\LinkFeedSettings::LinkFormatPowerPoint, $feedSettings->linkFormats)): ?> active<? endif; ?>">
                            <span class="title"><? echo $control->title; ?></span>
                            <span class="service-data">
                                <span class="link-format-tag"><? echo \application\models\link_feed\LinkFeedSettings::LinkFormatPowerPoint; ?></span>
                            </span>
                        </button>
					<? endif; ?>
					<?
						/** @var \application\models\link_feed\FeedControlSettings $control */
						$control = $feedSettings->controlSettings->{\application\models\link_feed\FeedControlSettings::ControlTagLinkFormatVideo};
					?>
					<? if ($control->enabled): ?>
                        <button type="button"
                                class="btn btn-default link-format-toggle<? if (in_array(\application\models\link_feed\LinkFeedSettings::LinkFormatVideo, $feedSettings->linkFormats)): ?> active<? endif; ?>">
                            <span class="title"><? echo $control->title; ?></span>
                            <span class="service-data">
                                <span class="link-format-tag"><? echo \application\models\link_feed\LinkFeedSettings::LinkFormatVideo; ?></span>
                            </span>
                        </button>
					<? endif; ?>
					<?
						/** @var \application\models\link_feed\FeedControlSettings $control */
						$control = $feedSettings->controlSettings->{\application\models\link_feed\FeedControlSettings::ControlTagLinkFormatDocuments};
					?>
					<? if ($control->enabled): ?>
                        <button type="button"
                                class="btn btn-default link-format-toggle<? if (in_array(\application\models\link_feed\LinkFeedSettings::LinkFormatDocument, $feedSettings->linkFormats)): ?> active<? endif; ?>">
                            <span class="title"><? echo $control->title; ?></span>
                            <span class="service-data">
                                <span class="link-format-tag"><? echo \application\models\link_feed\LinkFeedSettings::LinkFormatDocument; ?></span>
                            </span>
                        </button>
					<? endif; ?>
                </div>
            </div>
			<? if (count($feedItems) > 1): ?>
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
			<? endif; ?>
        </div>
    </div>
    <div class="row carousel-container">
		<? echo $this->renderPartial('../link_feed/feedItems', array('feedId' => $feedId, 'feedSettings' => $feedSettings, 'feedItems' => $feedItems), true); ?>
    </div>
</div>
