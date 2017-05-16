<?
	use application\models\data_query\link_feed\LinkFeedItem;
	use application\models\feeds\common\FeedItemSettings;
	use application\models\feeds\horizontal\FeedSettings;
	use application\models\feeds\horizontal\LinkFeedSettings;

	/**
	 * @var string $feedId
	 * @var LinkFeedSettings $viewSettings
	 * @var LinkFeedItem[] $feedItems
	 */

	$carouseOneMoveClass = 'one-link-move';
	$itemColumnClass = '';
	switch ($viewSettings->linksPerSlide)
	{
		case FeedSettings::LinksPerSlide1:
			$itemColumnClass = 'col-xs-12 col-sm-12 col-md-12';
			break;
		case FeedSettings::LinksPerSlide2:
			$itemColumnClass = 'col-xs-12 col-sm-6 col-md-6';
			$carouseOneMoveClass .= ' two_shows_one_move';
			break;
		case FeedSettings::LinksPerSlide3:
			$itemColumnClass = 'col-xs-12 col-sm-4 col-md-4';
			$carouseOneMoveClass .= ' three_shows_one_move';
			break;
		case FeedSettings::LinksPerSlide4:
			$itemColumnClass = 'col-xs-12 col-sm-6 col-md-3';
			$carouseOneMoveClass .= ' four_shows_one_move';
			break;
		case FeedSettings::LinksPerSlide6:
			$itemColumnClass = 'col-xs-12 col-sm-4 col-md-2';
			$carouseOneMoveClass .= ' six_shows_one_move';
			break;
	}

	$linksPerSlide = $viewSettings->linksScrollMode === FeedSettings::LinksScrollModeLink ?
		1:
		$viewSettings->linksPerSlide;
?>
<style>
    <?if($viewSettings->maxImageHeight>0):?>
    #horizontal-feed-carousel-<? echo $feedId; ?> .portfolio_utube_item_image > img {
        max-height: <?echo $viewSettings->maxImageHeight;?>px;
    }

    #horizontal-feed-carousel-<? echo $feedId; ?> .carousel-inner > .item {
        -webkit-transition: <? echo $viewSettings->animationSpeed;?>s ease-in-out left;
        -o-transition: <? echo $viewSettings->animationSpeed;?>s ease-in-out left;
        transition: <? echo $viewSettings->animationSpeed;?>s ease-in-out left;
    }

    @media all and (transform-3d), (-webkit-transform-3d) {
        #horizontal-feed-carousel-<? echo $feedId; ?> .carousel-inner > .item {
            -webkit-transition: -webkit-transform <? echo $viewSettings->animationSpeed;?>s ease-in-out;
            -o-transition: -o-transform <? echo $viewSettings->animationSpeed;?>s ease-in-out;
            transition: transform <? echo $viewSettings->animationSpeed;?>s ease-in-out;
        }
    }
    <?endif;?>
</style>
<div id="horizontal-feed-carousel-<? echo $feedId; ?>"
     class="col-xs-12 carousel slide portfolio_utube_carousel_wrapper<? echo $viewSettings->linksScrollMode === FeedSettings::LinksScrollModeLink ? (' ' . $carouseOneMoveClass) : ''; ?><? if ($viewSettings->slideShow === true): ?> carousel-slide-show<? endif; ?><? if ($viewSettings->linksScrollMode === FeedSettings::LinksScrollModeFade): ?> carousel-fade<? endif; ?>"
     <? if ($viewSettings->slideShow === true): ?>data-interval="<? echo $viewSettings->slideShowInterval; ?>"
     <? else: ?>data-interval="false"<? endif; ?>>
    <div class="carousel-inner carousel-links" role="listbox">
		<? $linksCount = count($feedItems); ?>
		<? for ($i = 0; $i < $linksCount; $i += $linksPerSlide): ?>
            <div class="item<? if ($i === 0): ?> active<? endif; ?>">
				<? if ($linksPerSlide > 1): ?>
                <div class="row">
					<? endif; ?>
					<? for ($j = $i; $j < ($i + $linksPerSlide) && $j < $linksCount; $j++): ?>
                        <div class="portfolio_utube_item <? echo $itemColumnClass; ?>">
                            <div class="portfolio_utube_item_image">
                                <img src="<? echo $feedItems[$j]->thumbnail; ?>"/>
                            </div>
                            <div class="portfolio_utube_item_caption">
								<?
									$itemSettingsName = $viewSettings->dataItemSettings->{FeedItemSettings::DataItemTagName};
									$itemSettingsLibrary = $viewSettings->dataItemSettings->{FeedItemSettings::DataItemTagLibrary};
									$itemSettingsViewsCount = $viewSettings->dataItemSettings->{FeedItemSettings::DataItemTagViewsCount};
								?>
								<? if ($itemSettingsName->enabled): ?>
                                    <a href="#" style="
									<? if (!empty($itemSettingsName->color)): ?>
                                            color: <? echo Utils::formatColor($itemSettingsName->color);?>;
                                    <? endif; ?>
									<? if (!empty($itemSettingsName->font)): ?>
                                            font-family: <? echo FontReplacementHelper::replaceFont($itemSettingsName->font->name); ?> !important;
                                            font-size: <? echo $itemSettingsName->font->size; ?>pt !important;
                                            font-weight: <? echo $itemSettingsName->font->isBold ? 'bold' : 'normal'; ?> !important;
                                            font-style: <? echo $itemSettingsName->font->isItalic ? 'italic' : 'normal'; ?> !important;
                                            text-decoration: <? echo $itemSettingsName->font->isUnderlined ? 'underline' : 'inherit'; ?> !important;
									<? endif; ?>
                                            ">
										<? echo $feedItems[$j]->linkName; ?>
                                    </a>
								<? endif; ?>
                                <ul>
									<? if ($itemSettingsLibrary->enabled): ?>
                                        <li style="
										<? if (!empty($itemSettingsLibrary->color)): ?>
                                                color: <? echo Utils::formatColor($itemSettingsLibrary->color);?>;
                                        <? endif; ?>
										<? if (!empty($itemSettingsLibrary->font)): ?>
                                                font-family: <? echo FontReplacementHelper::replaceFont($itemSettingsLibrary->font->name); ?> !important;
                                                font-size: <? echo $itemSettingsLibrary->font->size; ?>pt !important;
                                                font-weight: <? echo $itemSettingsLibrary->font->isBold ? 'bold' : 'normal'; ?> !important;
                                                font-style: <? echo $itemSettingsLibrary->font->isItalic ? 'italic' : 'normal'; ?> !important;
                                                text-decoration: <? echo $itemSettingsLibrary->font->isUnderlined ? 'underline' : 'inherit'; ?> !important;
										<? endif; ?>
                                                ">
											<? echo $feedItems[$j]->libraryName; ?>
                                        </li>
									<? endif; ?>
									<? if ($itemSettingsViewsCount->enabled && $feedItems[$j]->viewsCount > 0): ?>
                                        <li style="
										<? if (!empty($itemSettingsViewsCount->color)): ?>
                                                color: <? echo Utils::formatColor($itemSettingsViewsCount->color);?>;
                                        <? endif; ?>
										<? if (!empty($itemSettingsViewsCount->font)): ?>
                                                font-family: <? echo FontReplacementHelper::replaceFont($itemSettingsViewsCount->font->name); ?> !important;
                                                font-size: <? echo $itemSettingsViewsCount->font->size; ?>pt !important;
                                                font-weight: <? echo $itemSettingsViewsCount->font->isBold ? 'bold' : 'normal'; ?> !important;
                                                font-style: <? echo $itemSettingsViewsCount->font->isItalic ? 'italic' : 'normal'; ?> !important;
                                                text-decoration: <? echo $itemSettingsViewsCount->font->isUnderlined ? 'underline' : 'inherit'; ?> !important;
										<? endif; ?>
                                                ">
											<? echo $feedItems[$j]->viewsCount; ?> views
                                        </li>
									<? endif; ?>
                                </ul>
                            </div>
                            <div class="service-data">
                                <div class="link-id"><? echo $feedItems[$j]->linkId; ?></div>
                            </div>
                        </div>
					<? endfor; ?>
					<? if ($linksPerSlide > 1): ?>
                </div>
			<? endif; ?>
            </div>
		<? endfor; ?>
    </div>
</div>


