<?
	/** @var $contentBlock \application\models\shortcuts\models\landing_page\regular_markup\SliderBlock */

	/** @var \application\models\shortcuts\models\landing_page\regular_markup\SlideBlock[] $slides */
	$slides = $contentBlock->items;
?>
<div id="carousel<? echo $contentBlock->id; ?>" class="carousel landing-carousel slide <? if ($contentBlock->slideShow): ?>carousel-slide-show<? endif; ?>"
     <? if ($contentBlock->slideShow): ?>data-interval="<? echo $contentBlock->slideShowInterval;?>"<? endif; ?>
     style="<? echo $this->renderPartial('landingPageMarkup/stylePadding', array('padding' => $contentBlock->padding), true); ?>
     <? echo $this->renderPartial('landingPageMarkup/styleMargin', array('margin' => $contentBlock->margin), true); ?>
     <? echo $this->renderPartial('landingPageMarkup/styleBorder', array('border' => $contentBlock->border), true); ?>"
>
    <ol class="carousel-indicators">
		<? $i = 0; ?>
		<? foreach ($slides as $slideBlock): ?>
            <li data-target="#carousel<? echo $contentBlock->id; ?>" data-slide-to="<? echo $i; ?>"
			    <? if ($i == 0): ?>class="active"<? endif; ?>></li>
			<? $i++; ?>
		<? endforeach; ?>
    </ol>
    <div class="carousel-inner" role="listbox">
		<? $i = 0; ?>
		<? foreach ($slides as $slideBlock): ?>
		<? if ($slideBlock->type === 'url'): ?>
		    <? /** @var \application\models\shortcuts\models\landing_page\regular_markup\SlideUrlBlock $slideBlock */ ?>
            <a href="<? echo $slideBlock->url; ?>" target="_blank"
                class="item<? if ($i == 0): ?> active<? endif; ?>"<? if (!empty($slideBlock->hoverText)): ?> title="<? echo $slideBlock->hoverText; ?>"<? endif; ?>>
		<? elseif ($slideBlock->type === 'shortcut'): ?>
			<? /** @var \application\models\shortcuts\models\landing_page\regular_markup\SlideShortcutBlock $slideBlock */ ?>
            <a href="<? echo $slideBlock->shortcut->getSourceLink(); ?>"
               class="item<? if ($i == 0): ?> active<? endif; ?> shortcuts-link"<? if (!empty($slideBlock->hoverText)): ?> title="<? echo $slideBlock->hoverText; ?>"<? endif; ?>>
                <div class="service-data">
					<? echo $slideBlock->shortcut->getMenuItemData(); ?>
                </div>
		<? else: ?>
           <a href="#"
                   class="item<? if ($i == 0): ?> active<? endif; ?>"<? if (!empty($slideBlock->hoverText)): ?> title="<? echo $slideBlock->hoverText; ?>"<? endif; ?>>
					<? endif; ?>
                    <img src="<? echo $slideBlock->imageSettings->source; ?>">
                    <div class="carousel-caption">
						<? echo $this->renderPartial('landingPageMarkup/blockContainer', array('contentBlocks' => $slideBlock->items), true); ?>
                    </div>
            </a>
		    <? $i++; ?>
		<? endforeach; ?>
    </div>
    <a class="left carousel-control" href="#carousel<? echo $contentBlock->id; ?>" role="button" data-slide="prev">
        <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
        <span class="sr-only">Previous</span>
    </a>
    <a class="right carousel-control" href="#carousel<? echo $contentBlock->id; ?>" role="button" data-slide="next">
        <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
        <span class="sr-only">Next</span>
    </a>
</div>
