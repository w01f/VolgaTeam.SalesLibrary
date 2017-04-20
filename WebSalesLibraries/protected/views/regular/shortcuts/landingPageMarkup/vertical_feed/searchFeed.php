<?
	use application\models\data_query\link_feed\LinkFeedItem;
	use application\models\feeds\common\FeedItemSettings;
	use application\models\feeds\vertical\LinkFeedStyle;
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\SearchFeedBlock;

	/** @var $contentBlock SearchFeedBlock */

	/** @var LinkFeedItem[] $feedItems */
	$feedItems = $contentBlock->getFeedItems();

	/** @var LinkFeedStyle $style */
	$style = $contentBlock->viewSettings->style;
?>
<style>
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
			<? echo CJSON::encode($contentBlock->viewSettings); ?>
        </div>
    </div>
    <div class="panel panel-default"
         style="<? if (!empty($style->outsideBorderColor)): ?>border-color: <? echo '#' . $style->outsideBorderColor; ?>;<? endif; ?>">
        <div class="panel-heading"
             style="<? if ($contentBlock->viewSettings->hideHeader): ?>display: none;<? endif; ?><? if (!empty($style->headerColor)): ?>background-color: <? echo '#' . $style->headerColor; ?>;<? endif; ?>">
			<? if (!empty($contentBlock->viewSettings->icon)): ?>
                <i class="icomoon icomoon-lg <? echo $contentBlock->viewSettings->icon; ?>"
                   style="<? if (!empty($style->headerIconColor)): ?>color: <? echo '#' . $style->headerIconColor; ?>;<? endif; ?>"></i>
			<? else: ?>
                <span class="glyphicon glyphicon-list-alt"></span>
			<? endif; ?>
            <strong style="<? if (!empty($style->headerTextColor)): ?>color: <? echo '#' . $style->headerTextColor; ?>;<? endif; ?>"><? echo $contentBlock->viewSettings->title; ?></strong>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-xs-12">
                    <ul class="feed-items-list">
						<? $linkNumber = 1; ?>
						<? foreach ($feedItems as $feedItem): ?>
                            <li class="news-item">
                                <a href="#" class="content library-link-block">
									<? if ($style->showLinkCounter): ?>
                                        <div class="link-number"><? echo $linkNumber; ?>.</div>
									<? endif; ?>
                                    <div class="link-info">
										<?
											$itemSettingsName = $contentBlock->dataItemSettings->{FeedItemSettings::DataItemTagName};
											$itemSettingsLibrary = $contentBlock->dataItemSettings->{FeedItemSettings::DataItemTagLibrary};
											$itemSettingsViewsCount = $contentBlock->dataItemSettings->{FeedItemSettings::DataItemTagViewsCount};
										?>
										<? if ($itemSettingsName->enabled && $style->linkNamePosition === LinkFeedStyle::LinkNamePositionTop): ?>
                                            <div class="text">
                                            <span class="feed-info link-name" style="
                                            <? if (!empty($itemSettingsName->color)): ?>
                                                    color: <? echo '#' . $itemSettingsName->color;?>;
                                    <? endif; ?>
                                            <? if (!empty($itemSettingsName->font)): ?>
                                                    font-family: <? echo FontReplacementHelper::replaceFont($itemSettingsName->font->name); ?> !important;
                                                    font-size: <? echo $itemSettingsName->font->size; ?>pt !important;
                                                    font-weight: <? echo $itemSettingsName->font->isBold ? 'bold' : 'normal'; ?> !important;
                                                    font-style: <? echo $itemSettingsName->font->isItalic ? 'italic' : 'normal'; ?> !important;
                                                    text-decoration: <? echo $itemSettingsName->font->isUnderlined ? 'underline' : 'inherit'; ?> !important;
                                            <? endif; ?>
                                                    ">
                                                <? echo $feedItem->linkName; ?>
                                            </span>
                                            </div>
										<? endif; ?>
										<? if (!empty($feedItem->thumbnail)): ?>
                                            <div class="image library-link-thumbnail">
                                                <img src="<? echo $feedItem->thumbnail; ?>"/>
                                            </div>
										<? endif; ?>
                                        <div class="text">
											<? if ($itemSettingsName->enabled && $style->linkNamePosition === LinkFeedStyle::LinkNamePositionBottom): ?>
                                                <span class="feed-info link-name" style="
												<? if (!empty($itemSettingsName->color)): ?>
                                                        color: <? echo '#' . $itemSettingsName->color;?>;
                                    <? endif; ?>
												<? if (!empty($itemSettingsName->font)): ?>
                                                        font-family: <? echo FontReplacementHelper::replaceFont($itemSettingsName->font->name); ?> !important;
                                                        font-size: <? echo $itemSettingsName->font->size; ?>pt !important;
                                                        font-weight: <? echo $itemSettingsName->font->isBold ? 'bold' : 'normal'; ?> !important;
                                                        font-style: <? echo $itemSettingsName->font->isItalic ? 'italic' : 'normal'; ?> !important;
                                                        text-decoration: <? echo $itemSettingsName->font->isUnderlined ? 'underline' : 'inherit'; ?> !important;
												<? endif; ?>
                                                        ">
                                                <? echo $feedItem->linkName; ?>
                                            </span>
											<? endif; ?>
											<? if ($itemSettingsLibrary->enabled): ?>
                                                <span class="feed-info library-name" style="
												<? if (!empty($itemSettingsLibrary->color)): ?>
                                                        color: <? echo '#' . $itemSettingsLibrary->color;?>;
                                        <? endif; ?>
												<? if (!empty($itemSettingsLibrary->font)): ?>
                                                        font-family: <? echo FontReplacementHelper::replaceFont($itemSettingsLibrary->font->name); ?> !important;
                                                        font-size: <? echo $itemSettingsLibrary->font->size; ?>pt !important;
                                                        font-weight: <? echo $itemSettingsLibrary->font->isBold ? 'bold' : 'normal'; ?> !important;
                                                        font-style: <? echo $itemSettingsLibrary->font->isItalic ? 'italic' : 'normal'; ?> !important;
                                                        text-decoration: <? echo $itemSettingsLibrary->font->isUnderlined ? 'underline' : 'inherit'; ?> !important;
												<? endif; ?>
                                                        ">
                                                <? echo $feedItem->libraryName; ?>
                                            </span>
											<? endif; ?>
											<? if ($itemSettingsViewsCount->enabled && $feedItem->viewsCount > 0): ?>
                                                <span class="feed-info views-count" style="
												<? if (!empty($itemSettingsViewsCount->color)): ?>
                                                        color: <? echo '#' . $itemSettingsViewsCount->color;?>;
                                        <? endif; ?>
												<? if (!empty($itemSettingsViewsCount->font)): ?>
                                                        font-family: <? echo FontReplacementHelper::replaceFont($itemSettingsViewsCount->font->name); ?> !important;
                                                        font-size: <? echo $itemSettingsViewsCount->font->size; ?>pt !important;
                                                        font-weight: <? echo $itemSettingsViewsCount->font->isBold ? 'bold' : 'normal'; ?> !important;
                                                        font-style: <? echo $itemSettingsViewsCount->font->isItalic ? 'italic' : 'normal'; ?> !important;
                                                        text-decoration: <? echo $itemSettingsViewsCount->font->isUnderlined ? 'underline' : 'inherit'; ?> !important;
												<? endif; ?>
                                                        ">
                                                <? echo $feedItem->viewsCount; ?> views
                                            </span>
											<? endif; ?>
                                        </div>
                                    </div>
                                    <div class="service-data">
                                        <div class="link-id"><? echo $feedItem->linkId; ?></div>
                                    </div>
                                </a>
                            </li>
							<? $linkNumber++; ?>
						<? endforeach; ?>
                    </ul>
                </div>
            </div>
        </div>
        <div class="panel-footer"
             style="<? if ($contentBlock->viewSettings->hideFooter): ?>display: none;<? endif; ?><? if (!empty($style->footerColor)): ?>background-color: <? echo '#' . $style->footerColor; ?>;<? endif; ?>"></div>
    </div>
</div>
