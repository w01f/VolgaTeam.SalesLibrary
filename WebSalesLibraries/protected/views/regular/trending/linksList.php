<?
	/**
	 * @var string $barId
	 * @var \application\models\trending\TrendingSettings $trendingSettings
	 * @var \application\models\trending\TrendingLink[] $links
	 */

	$carouseOneMoveClass = 'one-link-move';
	$itemColumnClass = '';
	switch ($trendingSettings->linksPerSlide)
	{
		case \application\models\trending\TrendingSettings::LinksPerSlide3:
			$itemColumnClass = 'col-xs-12 col-sm-4 col-md-4';
			$carouseOneMoveClass .= ' three_shows_one_move';
			break;
		case \application\models\trending\TrendingSettings::LinksPerSlide4:
			$itemColumnClass = 'col-xs-12 col-sm-6 col-md-3';
			$carouseOneMoveClass .= ' four_shows_one_move';
			break;
		case \application\models\trending\TrendingSettings::LinksPerSlide6:
			$itemColumnClass = 'col-xs-12 col-sm-4 col-md-2';
			$carouseOneMoveClass .= ' six_shows_one_move';
			break;
	}

	$linksPerSlide = $trendingSettings->linksScrollMode === \application\models\trending\TrendingSettings::LinksScrollModeSlide ?
		$trendingSettings->linksPerSlide :
		1;
?>
<div id="trending-carousel-<? echo $barId; ?>"
     class="col-xs-12 carousel slide portfolio_utube_carousel_wrapper<? echo $trendingSettings->linksScrollMode === \application\models\trending\TrendingSettings::LinksScrollModeLink ? (' ' . $carouseOneMoveClass) : ''; ?><? if ($trendingSettings->slideShow == true): ?> carousel-slide-show<? endif; ?>"
     <? if ($trendingSettings->slideShow == true): ?>data-interval="<? echo $trendingSettings->slideShowInterval; ?>"<? endif; ?>>
    <div class="carousel-inner" role="listbox">
		<? $linksCount = count($links); ?>
		<? for ($i = 0; $i < $linksCount; $i += $linksPerSlide): ?>
            <div class="item<? if ($i === 0): ?> active<? endif; ?>">
				<? if ($linksPerSlide > 1): ?>
                <div class="row">
					<? endif; ?>
					<? for ($j = $i; $j < ($i + $linksPerSlide) && $j < $linksCount; $j++): ?>
                        <div class="portfolio_utube_item <? echo $itemColumnClass; ?>">
                            <div class="portfolio_utube_item_image">
                                <img src="<? echo $links[$j]->thumbnail; ?>"/>
                            </div>
                            <div class="portfolio_utube_item_caption">
                                <a href="#"><? echo $links[$j]->linkName; ?></a>
                                <ul>
                                    <li><? echo $links[$j]->libraryName; ?></li>
                                    <li><? echo $links[$j]->viewsCount; ?> views</li>
                                </ul>
                            </div>
                            <div class="service-data">
                                <div class="link-id"><? echo $links[$j]->linkId; ?></div>
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



