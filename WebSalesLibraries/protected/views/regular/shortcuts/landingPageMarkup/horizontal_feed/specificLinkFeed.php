<?
	use application\models\data_query\link_feed\LinkFeedItem;
	use application\models\data_query\link_feed\LinkFeedQuerySettings;
	use application\models\data_query\link_feed\SpecificLinkFeedQuerySettings;
	use application\models\feeds\common\FeedControlSettings;
	use application\models\feeds\horizontal\SpecificLinkFeedSettings;
	use application\models\shortcuts\models\landing_page\regular_markup\horizontal_feed\SpecificLinkFeedBlock;

	/** @var $contentBlock SpecificLinkFeedBlock */

	$feedId = $contentBlock->id;

	/** @var SpecificLinkFeedQuerySettings $querySettings */
	$querySettings = $contentBlock->querySettings;
	/** @var SpecificLinkFeedSettings $viewSettings */
	$viewSettings = $contentBlock->viewSettings;
	/** @var LinkFeedItem[] $feedItems */
	$feedItems = $contentBlock->getFeedItems();

	echo $this->renderPartial('landingPageMarkup/style/feedControlsStyle',
		array(
			'feedId' => 'horizontal-feed-' . $feedId,
			'style' => $contentBlock->viewSettings->controlsStyle
		)
		, true);
?>
<div id="horizontal-feed-<? echo $feedId; ?>" class="row horizontal-feed horizontal-link-feed">
	<div class="service-data">
		<div class="encoded-object">
			<div class="query-settings"><? echo CJSON::encode($querySettings); ?></div>
			<div class="view-settings"><? echo CJSON::encode($viewSettings); ?></div>
		</div>
	</div>
	<div class="col-xs-12">
		<div class="row">
            <div class="feed-controls-container col-md-8 col-sm-8 col-xs-8">
				<div class="btn-group" role="group">
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
			</div>
			<?
				/** @var FeedControlSettings $control */
				$control = $viewSettings->controlSettings->{FeedControlSettings::ControlTagScrollButton};
			?>
			<? if ($control->enabled && count($feedItems) > 1): ?>
                <div class="carousel-controls-container col-md-4 col-sm-4 col-xs-4 text-right
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
