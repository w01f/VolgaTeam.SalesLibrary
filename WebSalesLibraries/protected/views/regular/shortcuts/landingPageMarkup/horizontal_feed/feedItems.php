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
	if($viewSettings->linksScrollMode === FeedSettings::LinksScrollModeLink)
	{
		$carouseOneMoveClass .= ' carousel-link';
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
	}
	else if($viewSettings->linksScrollMode === FeedSettings::LinksScrollModeFade)
	{
		$carouseOneMoveClass .= ' carousel-fade';
		switch ($viewSettings->linksPerSlide)
		{
			case FeedSettings::LinksPerSlide1:
				$itemColumnClass = 'col-xs-12 col-sm-12 col-md-12';
				break;
			case FeedSettings::LinksPerSlide2:
				$itemColumnClass = 'col-xs-12 col-sm-6 col-md-6 col-lg-6';
				$carouseOneMoveClass .= ' two_shows_one_move';
				break;
			case FeedSettings::LinksPerSlide3:
				$itemColumnClass = 'col-xs-12 col-sm-6 col-md-4 col-lg-4';
				$carouseOneMoveClass .= ' three_shows_one_move';
				break;
			case FeedSettings::LinksPerSlide4:
				$itemColumnClass = 'col-xs-12 col-sm-6 col-md-4 col-lg-3';
				$carouseOneMoveClass .= ' four_shows_one_move';
				break;
			case FeedSettings::LinksPerSlide6:
				$itemColumnClass = 'col-xs-12 col-sm-4 col-md-3 col-lg-2';
				$carouseOneMoveClass .= ' six_shows_one_move';
				break;
		}
	}

	$linksPerSlide = $viewSettings->linksScrollMode !== FeedSettings::LinksScrollModeSlide ?
		1:
		$viewSettings->linksPerSlide;
?>
<style>
    <?if($viewSettings->maxImageHeight>0):?>
    #horizontal-feed-carousel-<? echo $feedId; ?> .portfolio_utube_item_image > img {
        max-height: <?echo $viewSettings->maxImageHeight;?>px;
    }
    <?endif;?>
</style>
<div id="horizontal-feed-carousel-<? echo $feedId; ?>"
     class="col-xs-12 carousel slide portfolio_utube_carousel_wrapper<? echo $viewSettings->linksScrollMode !== FeedSettings::LinksScrollModeSlide ? (' ' . $carouseOneMoveClass) : ''; ?><? if ($viewSettings->slideShow === true): ?> carousel-slide-show<? endif; ?>"
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
                        <div class="portfolio_utube_item library-link-item <? echo $itemColumnClass; ?><? if ($feedItems[$j]->isDraggable): ?> draggable<? endif; ?><? if (!$feedItems[$j]->isDirectUrl): ?> previewable<? else:?> direct-url<? endif; ?>"
						     <? if ($feedItems[$j]->isDraggable): ?>draggable="true"
                             data-url-header="<? echo $feedItems[$j]->dragHeader; ?>"
                             data-url="<? echo $feedItems[$j]->url; ?>"<? endif; ?>>
	                        <? if ($feedItems[$j]->isDirectUrl): ?><a href="<? echo $feedItems[$j]->url; ?>" target="_blank"><? endif; ?>
                            <div class="portfolio_utube_item_image">
                                <img src="<? echo $feedItems[$j]->thumbnail; ?>"/>
                            </div>
                            <div class="portfolio_utube_item_caption" style="<? echo $this->renderPartial('../shortcuts/landingPageMarkup/style/stylePadding', array('padding' => $viewSettings->textPadding), true); ?>">
								<?
									$itemSettingsName = $viewSettings->dataItemSettings->{FeedItemSettings::DataItemTagName};
									$itemSettingsLibrary = $viewSettings->dataItemSettings->{FeedItemSettings::DataItemTagLibrary};
									$itemSettingsViewsCount = $viewSettings->dataItemSettings->{FeedItemSettings::DataItemTagViewsCount};
								?>
								<? if ($itemSettingsName->enabled): ?>
                                    <a href="#" style="
									<? if (!empty($itemSettingsName->color)): ?>
                                            color: <? echo Utils::formatColorToHex($itemSettingsName->color);?>;
                                    <? endif; ?>
									<? if (!empty($itemSettingsName->font)): ?>
                                            font-family: <? echo FontReplacementHelper::replaceFont($itemSettingsName->font->name); ?> !important;
                                            font-size: <? echo $itemSettingsName->font->size->single; ?>pt !important;
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
                                                color: <? echo Utils::formatColorToHex($itemSettingsLibrary->color);?>;
                                        <? endif; ?>
										<? if (!empty($itemSettingsLibrary->font)): ?>
                                                font-family: <? echo FontReplacementHelper::replaceFont($itemSettingsLibrary->font->name); ?> !important;
                                                font-size: <? echo $itemSettingsLibrary->font->size->single; ?>pt !important;
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
                                                color: <? echo Utils::formatColorToHex($itemSettingsViewsCount->color);?>;
                                        <? endif; ?>
										<? if (!empty($itemSettingsViewsCount->font)): ?>
                                                font-family: <? echo FontReplacementHelper::replaceFont($itemSettingsViewsCount->font->name); ?> !important;
                                                font-size: <? echo $itemSettingsViewsCount->font->size->single; ?>pt !important;
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
		                    <? if ($feedItems[$j]->isDirectUrl): ?></a><? endif; ?>
                        </div>
					<? endfor; ?>
					<? if ($linksPerSlide > 1): ?>
                </div>
			<? endif; ?>
            </div>
		<? endfor; ?>
    </div>
</div>



