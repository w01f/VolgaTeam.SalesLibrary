<?
	use application\models\shortcuts\models\landing_page\regular_markup\slider\SlideBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\slider\SliderBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\slider\SlideShortcutBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\slider\SlideUrlBlock;

	/** @var $contentBlock SliderBlock */

	$blockId = sprintf('carousel%s', $contentBlock->id);

	/** @var SlideBlock[] $slides */
	$slides = $contentBlock->items;

	echo $this->renderPartial('landingPageMarkup/style/styleBorder',
		array(
			'border' => $contentBlock->border,
			'blockId' => $blockId
		)
		, true);
	echo $this->renderPartial('landingPageMarkup/style/styleBackground',
		array(
			'background' => $contentBlock->background,
			'blockId' => $blockId
		)
		, true);
?>
<div id="<? echo $blockId; ?>" class="carousel landing-carousel slide <? if ($contentBlock->slideShow): ?>carousel-slide-show<? endif; ?>"
     <? if ($contentBlock->slideShow): ?>data-interval="<? echo $contentBlock->slideShowInterval;?>"<? endif; ?>
     style="<? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $contentBlock->padding), true); ?>
     <? echo $this->renderPartial('landingPageMarkup/style/styleMargin', array('margin' => $contentBlock->margin), true); ?>"
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
		    <? /** @var SlideUrlBlock $slideBlock */ ?>
            <a href="<? echo $slideBlock->url; ?>" target="_blank"
                class="item<? if ($i == 0): ?> active<? endif; ?>"<? if (!empty($slideBlock->hoverText)): ?> title="<? echo $slideBlock->hoverText; ?>"<? endif; ?>>
		<? elseif ($slideBlock->type === 'shortcut'): ?>
			<? /** @var SlideShortcutBlock $slideBlock */ ?>
            <a href="<? echo isset($slideBlock->shortcut) ? $slideBlock->shortcut->getSourceLink() : '#'; ?>"
               class="item<? if ($i == 0): ?> active<? endif; ?> shortcuts-link<?if(!isset($slideBlock->shortcut)):?> disabled<?endif;?>"<? if (!empty($slideBlock->hoverText)): ?> title="<? echo $slideBlock->hoverText; ?>"<? endif; ?>>
                <div class="service-data">
	                <? echo isset($slideBlock->shortcut) ? $slideBlock->shortcut->getMenuItemData() : '<div class="same-page"></div><div class="has-custom-handler"></div>'; ?>
                </div>
		<? else: ?>
           <a href="#"
                   class="item<? if ($i == 0): ?> active<? endif; ?>"<? if (!empty($slideBlock->hoverText)): ?> title="<? echo $slideBlock->hoverText; ?>"<? endif; ?>>
					<? endif; ?>
                    <img src="<? echo $slideBlock->imageSettings->source; ?>">
                    <div class="carousel-caption">
						<? echo $this->renderPartial('landingPageMarkup/common/blockContainer', array('contentBlocks' => $slideBlock->items), true); ?>
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
