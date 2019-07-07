<?
	use application\models\data_query\link_feed\LinkFeedItem;
	use application\models\data_query\link_feed\LinkFeedQuerySettings;
	use application\models\data_query\link_feed\SpecificLinkFeedQuerySettings;
	use application\models\feeds\common\FeedControlSettings;
	use application\models\feeds\common\FeedControlTag;
	use application\models\feeds\common\FeedDetailsControlSettings;
	use application\models\feeds\vertical\LinkFeedStyle;
	use application\models\feeds\vertical\SpecificLinkFeedSettings;
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\SpecificLinkFeedBlock;

	/** @var $contentBlock SpecificLinkFeedBlock */

	/** @var SpecificLinkFeedQuerySettings $querySettings */
	$querySettings = $contentBlock->querySettings;
	/** @var SpecificLinkFeedSettings $viewSettings */
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
    <?if(!empty($style->buttonBackColor)):?> background-color: <? echo Utils::formatColorToHex($style->buttonBackColor);?> !important;
    <?endif;?><?if(!empty($style->buttonBorderColor)):?> border-color: <? echo Utils::formatColorToHex($style->buttonBorderColor);?> !important;
    <?endif;?>
    }

    #vertical-feed-<? echo $contentBlock->id; ?> .panel-footer .pagination a .glyphicon {
    <?if(!empty($style->buttonIconColor)):?> color: <? echo Utils::formatColorToHex($style->buttonIconColor);?> !important;
    <?endif;?>
    }

    #vertical-feed-<? echo $contentBlock->id; ?> .news-item {
        border-bottom: none !important;
        margin-bottom: <?echo $style->linkSpace?>px !important;
    }

    #vertical-feed-<? echo $contentBlock->id; ?> .news-item .content .image img {
        max-width: <?echo $style->imageWidth;?>px !important;
        width: 100%;
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
		<?
			/** @var FeedControlSettings $control */
			$control = $viewSettings->controlSettings->{FeedControlTag::ControlTagLinkFormatPowerPoint};
		?>
		<? if ($control->enabled): ?>
            <button type="button"
                    class="btn btn-default link-format-toggle<? if (in_array(LinkFeedQuerySettings::LinkFormatPowerPoint, $querySettings->linkFormatsInclude)): ?> active<? endif; ?><? if ($control->hideCondition->large): ?> hidden-lg<? endif; ?>
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
                    class="btn btn-default link-format-toggle<? if (in_array(LinkFeedQuerySettings::LinkFormatVideo, $querySettings->linkFormatsInclude)): ?> active<? endif; ?><? if ($control->hideCondition->large): ?> hidden-lg<? endif; ?>
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
                    class="btn btn-default link-format-toggle<? if (in_array(LinkFeedQuerySettings::LinkFormatDocument, $querySettings->linkFormatsInclude)): ?> active<? endif; ?><? if ($control->hideCondition->large): ?> hidden-lg<? endif; ?>
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
		    /** @var FeedControlSettings $control */
		    $control = $viewSettings->controlSettings->{FeedControlTag::ControlTagLinkFormatHyperlinks};
	    ?>
	    <? if ($control->enabled): ?>
            <button type="button"
                    class="btn btn-default link-format-toggle<? if (in_array(LinkFeedQuerySettings::LinkFormatHyperlink, $querySettings->linkFormatsInclude)): ?> active<? endif; ?><? if ($control->hideCondition->large): ?> hidden-lg<? endif; ?>
                            <? if ($control->hideCondition->medium): ?> hidden-md<? endif; ?>
                            <? if ($control->hideCondition->small): ?> hidden-sm<? endif; ?>
                            <? if ($control->hideCondition->extraSmall): ?> hidden-xs<? endif; ?>">
                <span class="title"><? echo $control->title; ?></span>
                <span class="service-data">
                                <span class="link-format-tag"><? echo LinkFeedQuerySettings::LinkFormatHyperlink; ?></span>
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
                    #vertical-feed-<? echo $contentBlock->id; ?> .feed-details-button .svg path
                    {
                        fill: <? echo Utils::formatColorToHex(!empty($control->iconColor)?$control->iconColor:$viewSettings->controlsStyle->regularTextColor);?> !important;
                    }
                </style>
		    <? endif; ?>
            <button type="button"
                    class="btn btn-default feed-details-button<? if ($control->hideCondition->large): ?> hidden-lg<? endif; ?>
                            <? if ($control->hideCondition->medium): ?> hidden-md<? endif; ?>
                            <? if ($control->hideCondition->small): ?> hidden-sm<? endif; ?>
                            <? if ($control->hideCondition->extraSmall): ?> hidden-xs<? endif; ?>" style="
		    <?if(!empty($control->backColor)):?>background-color: <? echo Utils::formatColorToHex($control->backColor);?> !important;<?endif;?>
		    <?if(!empty($control->borderColor)):?>border-color: <? echo Utils::formatColorToHex($control->borderColor);?> !important;<?endif;?>">
                <img src="<? echo $contentBlock->imagePath . $control->iconFile; ?>" <?if(strpos($control->iconFile, '.svg') !== false):?>class="svg"<?endif;?>>
                <span class="service-data">
                    <? if ($contentBlock->detailsSettings->openSamePage): ?>
                        <span class="same-page">true</span>
                        <span class="default-link-id"><? echo $contentBlock->detailsSettings->detailsLinkId; ?></span>
                    <? else: ?>
                        <span class="default-url"><? echo \PageContentShortcut::createShortcutUrl($contentBlock->detailsSettings->detailsLinkId, false); ?></span>
                    <? endif; ?>
                    <span class="hover-tip-template"><? echo $control->hoverTip; ?></span>
                </span>
            </button>
	    <? endif; ?>
    </div>
    <div class="panel panel-default"
         style="<? if (!empty($style->outsideBorderColor)): ?>border-color: <? echo Utils::formatColorToHex($style->outsideBorderColor); ?>; -webkit-box-shadow: 0 0 0 <? echo Utils::formatColorToHex($style->outsideBorderColor); ?>; box-shadow: 0 0 0 <? echo Utils::formatColorToHex($style->outsideBorderColor); ?>;<? endif; ?>">
        <div class="panel-heading"
             style="<? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $style->headerPadding), true); ?><? if ($viewSettings->hideHeader): ?>display: none;<? endif; ?><? if (!empty($style->headerColor)): ?>background-color: <? echo Utils::formatColorToHex($style->headerColor); ?>;border-color: <? echo Utils::formatColorToHex($style->headerColor); ?>;<? endif; ?>">
			<? if (!empty($viewSettings->icon)): ?>
                <i class="icomoon icomoon-lg <? echo $viewSettings->icon; ?>"
                   style="<? if (!empty($style->headerIconColor)): ?>color: <? echo Utils::formatColorToHex($style->headerIconColor); ?>;<? endif; ?>"></i>
			<? else: ?>
                <span class="glyphicon glyphicon-list-alt"></span>
			<? endif; ?>
            <strong style="<? if (!empty($style->headerTextColor)): ?>color: <? echo Utils::formatColorToHex($style->headerTextColor); ?>;<? endif; ?>"><? echo $viewSettings->title; ?></strong>
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
             style="<? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $style->footerPadding), true); ?><? if ($viewSettings->hideFooter): ?>display: none;<? endif; ?><? if (!empty($style->footerColor)): ?>background-color: <? echo Utils::formatColorToHex($style->footerColor); ?>; border-top-color: <? echo Utils::formatColorToHex($style->footerColor); ?>;<? endif; ?>"></div>
    </div>
</div>
