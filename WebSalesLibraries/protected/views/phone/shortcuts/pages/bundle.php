<? /** @var $shortcut ContainerShortcut */ ?>
<div class="cbp-l-grid-masonry">
	<? foreach ($shortcut->links as $link): ?>
		<a class="cbp-item shortcuts-link" href="<? echo $link->getSourceLink(); ?>" data-ajax="false" target="_blank">
			<div class="cbp-caption">
				<? if ($link->useIcon == true): ?>
					<i class="logo <? echo $link->iconClass; ?>"></i>
				<? elseif ($link->useIcon != true && isset($link->imageContent)): ?>
					<img class="logo" src="<? echo $link->imageContent; ?>" alt=""/>
				<? endif; ?>
				<p class="title"><? echo $link->headerTitle; ?></p>
			</div>
			<div class="service-data">
				<? echo $link->getMenuItemData(); ?>
			</div>
		</a>
	<? endforeach; ?>
</div>
