<?
	/** @var NavigationPanel $navigationPanel */
?>
<? foreach ($navigationPanel->items as $navigationItem): ?>
	<?
	$identifier = 'navigation-item-' . $navigationItem->id;
	/** @var MobileNavigationItemSettings $settings */
	$settings = $navigationItem->settings;
	?>
    <style>
        <?echo '#'.$identifier;?> .title {
            color: <?echo Utils::formatColor($settings->textColor);?> !important;
        }
    </style>
    <li data-icon="false">
        <a id="<? echo $identifier; ?>"
           class="navigation-item<? if ($navigationItem->type === 'shortcut'): ?> shortcuts-link<? endif; ?>"
           data-ajax="false" href="<? echo $navigationItem->getUrl(); ?>" target="_blank">
            <div>
                <img class="logo" src="<? echo $settings->icon; ?>" alt=""/>
                <span class="title"><? echo $settings->title; ?></span>
            </div>
            <div class="service-data">
				<? echo $navigationItem->getItemData(); ?>
            </div>
        </a>
    </li>
<? endforeach; ?>
