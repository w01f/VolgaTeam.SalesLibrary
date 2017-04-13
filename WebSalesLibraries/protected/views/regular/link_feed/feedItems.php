<?
	/**
	 * @var string $feedId
	 * @var \application\models\link_feed\LinkFeedSettings $feedSettings
	 * @var \application\models\link_feed\LinkFeedItem[] $feedItems
	 */

	$carouseOneMoveClass = 'one-link-move';
	$itemColumnClass = '';
	switch ($feedSettings->linksPerSlide)
	{
		case \application\models\link_feed\LinkFeedSettings::LinksPerSlide3:
			$itemColumnClass = 'col-xs-12 col-sm-4 col-md-4';
			$carouseOneMoveClass .= ' three_shows_one_move';
			break;
		case \application\models\link_feed\LinkFeedSettings::LinksPerSlide4:
			$itemColumnClass = 'col-xs-12 col-sm-6 col-md-3';
			$carouseOneMoveClass .= ' four_shows_one_move';
			break;
		case \application\models\link_feed\LinkFeedSettings::LinksPerSlide6:
			$itemColumnClass = 'col-xs-12 col-sm-4 col-md-2';
			$carouseOneMoveClass .= ' six_shows_one_move';
			break;
	}

	$linksPerSlide = $feedSettings->linksScrollMode === \application\models\link_feed\LinkFeedSettings::LinksScrollModeSlide ?
		$feedSettings->linksPerSlide :
		1;
?>
<div id="link-feed-carousel-<? echo $feedId; ?>"
     class="col-xs-12 carousel slide portfolio_utube_carousel_wrapper<? echo $feedSettings->linksScrollMode === \application\models\link_feed\LinkFeedSettings::LinksScrollModeLink ? (' ' . $carouseOneMoveClass) : ''; ?><? if ($feedSettings->slideShow == true): ?> carousel-slide-show<? endif; ?>"
     <? if ($feedSettings->slideShow == true): ?>data-interval="<? echo $feedSettings->slideShowInterval; ?>"<? endif; ?>>
    <div class="carousel-inner" role="listbox">
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
									$itemSettingsName = $feedSettings->dataItemSettings->{\application\models\link_feed\FeedItemSettings::DataItemTagName};
									$itemSettingsLibrary = $feedSettings->dataItemSettings->{\application\models\link_feed\FeedItemSettings::DataItemTagLibrary};
									$itemSettingsViewsCount = $feedSettings->dataItemSettings->{\application\models\link_feed\FeedItemSettings::DataItemTagViewsCount};
								?>
								<? if ($itemSettingsName->enabled): ?>
                                    <a href="#" style="
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
										<? echo $feedItems[$j]->linkName; ?>
                                    </a>
								<? endif; ?>
                                <ul>
									<? if ($itemSettingsLibrary->enabled): ?>
                                        <li style="
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
											<? echo $feedItems[$j]->libraryName; ?>
                                        </li>
									<? endif; ?>
									<? if ($itemSettingsViewsCount->enabled && $feedItems[$j]->viewsCount > 0): ?>
                                        <li style="
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



