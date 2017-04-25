<?
	use application\models\data_query\link_feed\LinkFeedItem;
	use application\models\data_query\link_feed\LinkFeedQuerySettings;
	use application\models\data_query\link_feed\SearchFeedQuerySettings;
	use application\models\feeds\common\FeedControlSettings;
	use application\models\feeds\vertical\LinkFeedStyle;
	use application\models\feeds\vertical\SearchFeedSettings;
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\SearchFeedBlock;

	/** @var $contentBlock SearchFeedBlock */

	/** @var SearchFeedQuerySettings $querySettings */
	$querySettings = $contentBlock->querySettings;
	/** @var SearchFeedSettings $viewSettings */
	$viewSettings = $contentBlock->viewSettings;
	/** @var LinkFeedItem[] $feedItems */
	$feedItems = $contentBlock->getFeedItems();

	/** @var LinkFeedStyle $style */
	$style = $viewSettings->style;
?>
<style>
    <?if (!empty($viewSettings->controlActiveColor)):?>
    #vertical-feed-<? echo $contentBlock->id; ?> .vertical-feed-controls-container.btn-group .btn.btn-default.active,
    #vertical-feed-<? echo $contentBlock->id; ?> .vertical-feed-controls-container.btn-group .btn.btn-default.active:focus,
    #vertical-feed-<? echo $contentBlock->id; ?> .vertical-feed-controls-container.btn-group .btn.btn-default.active:hover,
    #vertical-feed-<? echo $contentBlock->id; ?> .vertical-feed-controls-container.btn-group .btn.btn-default.active:focus:hover {
        background-color: <? echo '#'.$viewSettings->controlActiveColor;?> !important;
        -webkit-box-shadow: none !important;
        box-shadow: none !important;
    }

    <?endif;?>

    #vertical-feed-<? echo $contentBlock->id; ?> .panel-body {
        padding-top: <?echo $style->bodyPadding->top;?>px !important;
        padding-left: <?echo $style->bodyPadding->left;?>px !important;
        padding-bottom: <?echo $style->bodyPadding->bottom;?>px !important;
        padding-right: <?echo $style->bodyPadding->right;?>px !important;
    }

    #vertical-feed-<? echo $contentBlock->id; ?> .panel-footer .pagination a {
    <?if(!empty($style->buttonBackColor)):?> background-color: <? echo '#'.$style->buttonBackColor;?> !important;
    <?endif;?><?if(!empty($style->buttonBorderColor)):?> border-color: <? echo '#'.$style->buttonBorderColor;?> !important;
    <?endif;?>
    }

    #vertical-feed-<? echo $contentBlock->id; ?> .panel-footer .pagination a .glyphicon {
    <?if(!empty($style->buttonIconColor)):?> color: <? echo '#'.$style->buttonIconColor;?> !important;
    <?endif;?>
    }

    #vertical-feed-<? echo $contentBlock->id; ?> .news-item {
        border-bottom: none !important;
        margin-bottom: <?echo $style->linkSpace?>px !important;
    }

    #vertical-feed-<? echo $contentBlock->id; ?> .news-item .content .image {
        min-width: <?echo $style->imageWidth;?>px !important;
    }

    #vertical-feed-<? echo $contentBlock->id; ?> .news-item .content .image img {
        max-width: <?echo $style->imageWidth;?>px !important;
    }

    <?if($style->imageHeight>0):?>
    #vertical-feed-<? echo $contentBlock->id; ?> .news-item .content .image img {
        max-height: <?echo $style->imageHeight;?>px !important;
    }

    <?endif;?>
</style>
<div id="vertical-feed-<? echo $contentBlock->id; ?>" class="vertical-feed news-block">
    <div class="service-data">
        <div class="encoded-object">
            <div class="query-settings"><? echo CJSON::encode($querySettings); ?></div>
            <div class="view-settings"><? echo CJSON::encode($viewSettings); ?></div>
        </div>
    </div>
    <div class="btn-group vertical-feed-controls-container hidden-xs hidden-sm" role="group">
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
    <div class="panel panel-default"
         style="<? if (!empty($style->outsideBorderColor)): ?>border-color: <? echo '#' . $style->outsideBorderColor; ?>;<? endif; ?>">
        <div class="panel-heading"
             style="<? if ($viewSettings->hideHeader): ?>display: none;<? endif; ?><? if (!empty($style->headerColor)): ?>background-color: <? echo '#' . $style->headerColor; ?>;<? endif; ?>">
			<? if (!empty($viewSettings->icon)): ?>
                <i class="icomoon icomoon-lg <? echo $viewSettings->icon; ?>"
                   style="<? if (!empty($style->headerIconColor)): ?>color: <? echo '#' . $style->headerIconColor; ?>;<? endif; ?>"></i>
			<? else: ?>
                <span class="glyphicon glyphicon-list-alt"></span>
			<? endif; ?>
            <strong style="<? if (!empty($style->headerTextColor)): ?>color: <? echo '#' . $style->headerTextColor; ?>;<? endif; ?>"><? echo $viewSettings->title; ?></strong>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-xs-12">
                    <div class="col-xs-12 feed-items-list-container">
						<? echo $this->renderPartial('landingPageMarkup/vertical_feed/feedItems', array('feedId' => $contentBlock->id, 'viewSettings' => $viewSettings, 'feedItems' => $feedItems), true); ?>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel-footer"
             style="<? if ($viewSettings->hideFooter): ?>display: none;<? endif; ?><? if (!empty($style->footerColor)): ?>background-color: <? echo '#' . $style->footerColor; ?>;<? endif; ?>"></div>
    </div>
</div>
