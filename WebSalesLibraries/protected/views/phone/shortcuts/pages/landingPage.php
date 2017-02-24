<? /** @var $shortcut LandingPageShortcut */ ?>
<div class="cbp-l-grid-masonry">
	<? foreach ($shortcut->mobileSettings->items as $item): ?>
        <a class="cbp-item shortcuts-link" href="<? echo $item->getSourceLink(); ?>" data-ajax="false" target="_blank">
            <div class="cbp-caption">
				<? if ($item->useIcon == true): ?>
                    <i class="logo <? echo $item->iconClass; ?>"></i>
				<? elseif ($item->useIcon != true && isset($item->imageUrl)): ?>
                    <img class="logo" src="<? echo $item->imageUrl; ?>" alt=""/>
				<? endif; ?>
                <p class="title"><? echo $item->title; ?></p>
            </div>
            <div class="service-data">
				<? echo $item->getMenuItemData(); ?>
            </div>
        </a>
	<? endforeach; ?>
</div>
