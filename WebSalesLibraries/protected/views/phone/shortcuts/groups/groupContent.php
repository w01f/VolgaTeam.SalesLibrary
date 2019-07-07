<?
	/**
	 * @var $group ShortcutGroup
	 */

	$this->renderPartial('../site/scripts');

	/** @var NavigationPanelShortcut[] $navigationPanelShortcuts */
	$navigationPanelShortcuts = array();
	foreach ($group->menuItems as $menuItem)
		if ($menuItem->shortcut instanceof NavigationPanelShortcut)
			$navigationPanelShortcuts[] = $menuItem->shortcut;
?>
<script type="text/javascript">
	$(document).ready(function ()
	{
		$.SalesPortal.ShortcutsManager.init();
	});
</script>
<div data-role='page' id="shortcut-group-<? echo $group->id; ?>" class="shortcut-group-page" data-cache="never" data-dom-cache="false" data-ajax="false">
    <div class="service-data">
        <div class="activity-data">
	        <? echo CJSON::encode(array('type' => 'Shortcut', 'subType' => 'Open Mobile Screen Group', 'data' => array('file' => 'Mobile Screen Group '. $group->title))); ?>
        </div>
    </div>
    <div data-role='header' class="page-header" data-position="fixed" data-theme="a">
		<? if ($group->showNavigationPanel): ?>
            <a href="#shortcut-group-<? echo $group->id; ?>-popup-panel-left" class="navigation-panel-toggle" data-icon="ion-navicon-round" data-iconpos="notext"></a>
		<? endif; ?>
		<h1 class="header-title"><? echo $group->title; ?></h1>
		<a href="#shortcut-group-popup-panel-right" class="ui-btn-right" data-icon="ion-navicon-round" data-iconpos="notext"></a>
	</div>
	<div data-role='content' class="main-content">
		<div class="menu-items">
			<div class="cbp-l-grid-masonry">
				<? foreach ($group->menuItems as $menuItem): ?>
					<?
                        /** @var $menuItem MenuItem */
                        $identifier = 'menu-item-' . $menuItem->shortcut->id;
					?>
                    <style>
                        <?echo '#'.$identifier;?> .logo
                        {
                            color: <?echo Utils::formatColorToHex($menuItem->appearance->iconColor);?> !important;
                        }
                        <?echo '#'.$identifier;?> .title
                        {
                            color: <?echo Utils::formatColorToHex($menuItem->appearance->textColor);?> !important;
                        }
                    </style>
                    <a id="<? echo $identifier; ?>" class="cbp-item menu-item" data-ajax="false" href="<? echo $menuItem->shortcut->getSourceLink(); ?>" target="_blank">
                        <div class="cbp-caption">
                            <? if ($menuItem->shortcut->useIcon == true): ?>
                                <i class="logo <? echo $menuItem->shortcut->iconClass; ?>"></i>
                            <? elseif ($menuItem->shortcut->useIcon != true && isset($menuItem->shortcut->imageContent)): ?>
                                <img class="logo" src="<? echo $menuItem->shortcut->imageContent; ?>" alt=""/>
                            <?endif; ?>
                            <p class="title"><? echo $menuItem->shortcut->title; ?></p>
                        </div>
                        <div class="service-data menu-item-data">
                            <? echo $menuItem->shortcut->getMenuItemData(); ?>
                        </div>
                    </a>
				<? endforeach; ?>
			</div>
		</div>
	</div>
	<? if ($group->showNavigationPanel): ?>
		<? $navigationPanel = $group->getNavigationPanel(); ?>
        <div data-role="panel" data-display="overlay" id="shortcut-group-<? echo $group->id; ?>-popup-panel-left">
            <ul class="navigation-items-container navigation-items-container-main" data-role="listview">
				<? echo $this->renderPartial('../shortcuts/navigationPanel/itemsList', array('navigationPanel' => $navigationPanel)); ?>
            </ul>
        </div>
	<? endif; ?>
	<div data-role="panel" data-display="overlay" data-position="right" id="shortcut-group-popup-panel-right">
        <ul data-role="listview">
			<? echo $this->renderPartial('../shortcuts/groups/groupList'); ?>
            <li data-icon="false">
                <a class="logout-button" href="#">Log Out</a>
            </li>
            <li data-role="list-divider"><p class="user-info">User: <? echo UserIdentity::getCurrentUserLogin(); ?></p></li>
            <li data-role="list-divider"><p>Copyright 2018 adSALESapps.com</p></li>
        </ul>
	</div>
	<? foreach ($navigationPanelShortcuts as $navigationPanelShortcut): ?>
		<? $shortcutNavigationPanel = $navigationPanelShortcut->getNavigationPanel();?>
		<? if (isset($shortcutNavigationPanel)): ?>
            <div data-role="panel" data-display="overlay" data-position="<? echo $navigationPanelShortcut->position; ?>" id="<? echo $navigationPanelShortcut->expandPanelId; ?>">
                <ul class="navigation-items-container navigation-items-container-main" data-role="listview">
					<? echo $this->renderPartial('../shortcuts/navigationPanel/itemsList', array('navigationPanel' => $shortcutNavigationPanel)); ?>
                </ul>
            </div>
		<? endif; ?>
	<? endforeach; ?>
	<div id="shortcuts-link-download-warning-popup" data-role="popup" data-theme="a" data-overlay-theme="d" data-dismissible="false">
		<div data-role="header" data-theme="d">
			<h1>Message</h1>
		</div>
		<div role="main" style="padding:0 20px">
			<p>File Downloads are Disabled for Mobile Devices</p>
			<div class="ui-grid-solo" style="padding:0 80px">
				<div class="ui-block-a">
					<a href="#" data-role="button" data-theme="d" data-rel="back">OK</a>
				</div>
			</div>
		</div>
	</div>
</div>

