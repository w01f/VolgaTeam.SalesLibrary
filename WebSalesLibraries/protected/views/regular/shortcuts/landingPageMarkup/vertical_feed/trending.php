<?
	use application\models\data_query\link_feed\LinkFeedItem;
	use application\models\data_query\link_feed\LinkFeedQuerySettings;
	use application\models\data_query\link_feed\TrendingFeedQuerySettings;
	use application\models\feeds\common\FeedControlSettings;
	use application\models\feeds\common\TrendingFeedControlSettings;
	use application\models\feeds\vertical\LinkFeedStyle;
	use application\models\feeds\vertical\TrendingFeedSettings;
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\TrendingBlock;

	/** @var $contentBlock TrendingBlock */

	/** @var TrendingFeedQuerySettings $querySettings */
	$querySettings = $contentBlock->querySettings;
	/** @var TrendingFeedSettings $viewSettings */
	$viewSettings = $contentBlock->viewSettings;
	/** @var LinkFeedItem[] $feedItems */
	$feedItems = $contentBlock->getFeedItems();

	/** @var LinkFeedStyle $style */
	$style = $viewSettings->style;

	echo $this->renderPartial('landingPageMarkup/style/feedControlsStyle',
		array(
			'feedId' => 'vertical-feed-' . $contentBlock->id,
			'style' => $viewSettings->controlsStyle
		)
		, true);
?>
<style>
    #vertical-feed-<? echo $contentBlock->id; ?> .panel
    {
        background: inherit;
    }

    #vertical-feed-<? echo $contentBlock->id; ?> .panel-body {
        padding-top: <?echo $style->bodyPadding->top;?>px !important;
        padding-left: <?echo $style->bodyPadding->left;?>px !important;
        padding-bottom: <?echo $style->bodyPadding->bottom;?>px !important;
        padding-right: <?echo $style->bodyPadding->right;?>px !important;
    }

    #vertical-feed-<? echo $contentBlock->id; ?> .panel-footer .pagination a {
    <?if(!empty($style->buttonBackColor)):?> background-color: <? echo Utils::formatColor($style->buttonBackColor);?> !important;
    <?endif;?><?if(!empty($style->buttonBorderColor)):?> border-color: <? echo Utils::formatColor($style->buttonBorderColor);?> !important;
    <?endif;?>
    }

    #vertical-feed-<? echo $contentBlock->id; ?> .panel-footer .pagination a .glyphicon {
    <?if(!empty($style->buttonIconColor)):?> color: <? echo Utils::formatColor($style->buttonIconColor);?> !important;
    <?endif;?>
    }

    #vertical-feed-<? echo $contentBlock->id; ?> .news-item {
        border-bottom: none !important;
        margin-bottom: <?echo $style->linkSpace?>px !important;
    }

    #vertical-feed-<? echo $contentBlock->id; ?> .news-item .content .image img {
        max-width: <?echo $style->imageWidth;?>px !important;
    }

    <?if($style->imageHeight>0):?>
    #vertical-feed-<? echo $contentBlock->id; ?> .news-item .content .image img {
        max-height: <?echo $style->imageHeight;?>px !important;
    }

    <?endif;?>

    #vertical-feed-<? echo $contentBlock->id; ?> .news-item .content .text {
        max-width: <?echo $style->imageWidth;?>px !important;
    }
</style>
<div id="vertical-feed-<? echo $contentBlock->id; ?>" class="vertical-feed news-block">
    <div class="service-data">
        <div class="encoded-object">
            <div class="query-settings"><? echo CJSON::encode($querySettings); ?></div>
            <div class="view-settings"><? echo CJSON::encode($viewSettings); ?></div>
        </div>
    </div>
    <div class="btn-group feed-controls-container vertical-feed-controls-container" role="group">
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
    <div class="panel panel-default"
         style="<? if (!empty($style->outsideBorderColor)): ?>border-color: <? echo Utils::formatColor($style->outsideBorderColor); ?>; -webkit-box-shadow: 0 0 0 <? echo Utils::formatColor($style->outsideBorderColor); ?>; box-shadow: 0 0 0 <? echo Utils::formatColor($style->outsideBorderColor); ?>;<? endif; ?>">
        <div class="panel-heading"
             style="<? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $style->headerPadding), true); ?><? if ($viewSettings->hideHeader): ?>display: none;<? endif; ?><? if (!empty($style->headerColor)): ?>background-color: <? echo Utils::formatColor($style->headerColor); ?>;border-color: <? echo Utils::formatColor($style->headerColor); ?>;<? endif; ?>">
			<? if (!empty($viewSettings->icon)): ?>
                <i class="icomoon icomoon-lg <? echo $viewSettings->icon; ?>"
                   style="<? if (!empty($style->headerIconColor)): ?>color: <? echo Utils::formatColor($style->headerIconColor); ?>;<? endif; ?>"></i>
			<? else: ?>
                <span class="glyphicon glyphicon-list-alt"></span>
			<? endif; ?>
            <strong style="<? if (!empty($style->headerTextColor)): ?>color: <? echo Utils::formatColor($style->headerTextColor); ?>;<? endif; ?>"><? echo $viewSettings->title; ?></strong>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-xs-12 feed-items-list-container">
					<? echo $this->renderPartial('landingPageMarkup/vertical_feed/feedItems', array('feedId' => $contentBlock->id, 'viewSettings' => $viewSettings, 'feedItems' => $feedItems), true); ?>
                </div>
            </div>
        </div>
        <div class="panel-footer"
             style="<? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $style->footerPadding), true); ?><? if ($viewSettings->hideFooter): ?>display: none;<? endif; ?><? if (!empty($style->footerColor)): ?>background-color: <? echo Utils::formatColor($style->footerColor); ?>; border-top-color: <? echo Utils::formatColor($style->footerColor); ?>;<? endif; ?>"></div>
    </div>
</div>