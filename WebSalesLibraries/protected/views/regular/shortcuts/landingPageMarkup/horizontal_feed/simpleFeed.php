<?
	use application\models\feeds\common\FeedControlSettings;
	use application\models\feeds\horizontal\FeedSettings;
	use application\models\feeds\horizontal\SimpleFeedSettings;
	use application\models\shortcuts\models\landing_page\regular_markup\horizontal_feed\SimpleFeedBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\horizontal_feed\SimpleFeedItem;
	use application\models\shortcuts\models\landing_page\regular_markup\horizontal_feed\SimpleFeedShortcutItem;
	use application\models\shortcuts\models\landing_page\regular_markup\horizontal_feed\SimpleFeedUrlItem;

	/** @var $contentBlock SimpleFeedBlock */

	$feedId = $contentBlock->id;

	/** @var SimpleFeedSettings $viewSettings */
	$viewSettings = $contentBlock->viewSettings;
	/** @var SimpleFeedItem[] $feedItems */
	$feedItems = $contentBlock->items;

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

    #horizontal-feed-carousel-<? echo $feedId; ?> {
        padding: <?echo $viewSettings->style->feedPadding->top;?>px <?echo $viewSettings->style->feedPadding->right;?>px <?echo $viewSettings->style->feedPadding->bottom;?>px <?echo $viewSettings->style->feedPadding->left;?>px !important;
    }

    #horizontal-feed-carousel-<? echo $feedId; ?> .portfolio_utube_item{
        padding: <?echo $viewSettings->style->itemPadding->top;?>px <?echo $viewSettings->style->itemPadding->right;?>px <?echo $viewSettings->style->itemPadding->bottom;?>px <?echo $viewSettings->style->itemPadding->left;?>px !important;
    }

    #horizontal-feed-carousel-<? echo $feedId; ?> .portfolio_utube_carousel_control_left,
    #horizontal-feed-carousel-<? echo $feedId; ?> .portfolio_utube_carousel_control_right {
        top: <?echo $viewSettings->style->controlButtonTop;?>% !important;
        width: <?echo $viewSettings->style->controlButtonWidth;?>px !important;
        height: <?echo $viewSettings->style->controlButtonHeight;?>px !important;
    }

    #horizontal-feed-carousel-<? echo $feedId; ?> .portfolio_utube_carousel_control_icons {
        line-height: <?echo $viewSettings->style->controlButtonHeight;?>px !important;
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
</style>
<div id="horizontal-feed-<? echo $feedId; ?>" class="row horizontal-feed">
	<div class="service-data">
		<div class="encoded-object">
			<div class="view-settings"><? echo CJSON::encode($viewSettings); ?></div>
		</div>
	</div>
	<div id="horizontal-feed-carousel-<? echo $feedId; ?>"
	     class="col-xs-12 carousel slide portfolio_utube_carousel_wrapper<? echo $viewSettings->linksScrollMode !== FeedSettings::LinksScrollModeSlide ? (' ' . $carouseOneMoveClass) : ''; ?><? if ($viewSettings->slideShow == true): ?> carousel-slide-show<? endif; ?>"
	     <? if ($viewSettings->slideShow === true): ?>data-interval="<? echo $viewSettings->slideShowInterval; ?>"
	     <? else: ?>data-interval="false"<? endif; ?>>
		<div class="carousel-inner" role="listbox">
			<? $linksCount = count($feedItems); ?>
			<? for ($i = 0; $i < $linksCount; $i += $linksPerSlide): ?>
				<div class="item<? if ($i === 0): ?> active<? endif; ?>">
					<? if ($linksPerSlide > 1): ?>
					<div class="row">
						<? endif; ?>
						<? for ($j = $i; $j < ($i + $linksPerSlide) && $j < $linksCount; $j++): ?>
							<div class="portfolio_utube_item <? echo $itemColumnClass; ?>">
								<? if ($feedItems[$i]->type === 'url'): ?>
									<?
										/** @var SimpleFeedUrlItem $feedItem */
										$feedItem = $feedItems[$i];
									?>
                                <a href="<? echo $feedItem->url; ?>"
								   <? if ($feedItem->isMailTo!==false): ?>target="_self"<?else:?>target="_blank"<? endif; ?> <? if (!empty($feedItem->hoverText)): ?> title="<? echo $feedItem->hoverText; ?>"<? endif; ?>>
								<? elseif ($feedItems[$i]->type === 'shortcut'): ?>
										<?
											/** @var SimpleFeedShortcutItem $feedItem */
											$feedItem = $feedItems[$i];
										?>
									<a href="<? echo isset($feedItem->shortcut) ? $feedItem->shortcut->getSourceLink() : '#'; ?>"
									   class="shortcuts-link<? if (!isset($feedItem->shortcut)): ?> disabled<? endif; ?>" <? if (!empty($feedItem->hoverText)): ?> title="<? echo $feedItem->hoverText; ?>"<? endif; ?>>
										<div class="service-data">
											<? echo isset($feedItem->shortcut) ? $feedItem->shortcut->getMenuItemData() : '<div class="same-page"></div><div class="has-custom-handler"></div>'; ?>
										</div>
								<? endif; ?>
									<div class="portfolio_utube_item_image">
										<img src="<? echo $feedItems[$j]->imagePath; ?>"/>
									</div>
									<div class="portfolio_utube_item_caption">
										<? if (!empty($feedItems[$j]->title)): ?>
											<?
											$itemTitleId = sprintf("simple-horizontal-feed-item-title-%s", $feedItems[$j]->id);
											echo $this->renderPartial('landingPageMarkup/style/styleTextAppearance',
												array(
													'textAppearance' => $feedItems[$j]->titleTextAppearance,
													'blockId' => $itemTitleId
												)
												, true);
											?>
											<div id="<? echo $itemTitleId; ?>" class="title">
												<? echo $feedItems[$j]->title; ?>
											</div>
										<? endif; ?>
										<? if (!empty($feedItems[$j]->description)): ?>
										<?
										$itemDescriptionId = sprintf("simple-horizontal-feed-item-description-%s", $feedItems[$j]->id);
										echo $this->renderPartial('landingPageMarkup/style/styleTextAppearance',
											array(
												'textAppearance' => $feedItems[$j]->descriptionTextAppearance,
												'blockId' => $itemDescriptionId
											)
											, true);
										?>
											<ul>
												<li id="<? echo $itemDescriptionId; ?>">
													<? echo $feedItems[$j]->description; ?>
												</li>
											</ul>
										<? endif; ?>
									</div>
								</a>
							</div>
						<? endfor; ?>
						<? if ($linksPerSlide > 1): ?>
					</div>
				<? endif; ?>
				</div>
			<? endfor; ?>
		</div>
		<?
			/** @var FeedControlSettings $control */
			$control = $viewSettings->controlSettings->{FeedControlSettings::ControlTagScrollButton};
		?>
		<? if ($control->enabled && count($feedItems) > 1): ?>
			<div class="carousel-controls-container <? if ($control->hideCondition->large): ?> hidden-lg<? endif; ?>
                    <? if ($control->hideCondition->medium): ?> hidden-md<? endif; ?>
                    <? if ($control->hideCondition->small): ?> hidden-sm<? endif; ?>
                    <? if ($control->hideCondition->extraSmall): ?> hidden-xs<? endif; ?>">
				<a class="left carousel-control portfolio_utube_carousel_control_left" href="#adv_portfolio_6_columns_utube_carousel" role="button" data-slide="prev">
					<span class="fa fa-angle-left portfolio_utube_carousel_control_icons" aria-hidden="true"></span>
					<span class="sr-only">Previous</span>
				</a>
				<a class="right carousel-control portfolio_utube_carousel_control_right" href="#adv_portfolio_6_columns_utube_carousel" role="button" data-slide="next">
					<span class="fa fa-angle-right portfolio_utube_carousel_control_icons" aria-hidden="true"></span>
					<span class="sr-only">Next</span>
				</a>
			</div>
		<? endif; ?>
	</div>
</div>