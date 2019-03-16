<?
	/**
	 * @var $shortcut PageContentShortcut
	 * @var $shortcutContent string
	 */
?>
<div id="shortcut-link-page-<? echo $shortcut->id; ?>" class="shortcut-link-page" data-role='page' data-cache="never"
     data-dom-cache="false" data-ajax="false">
    <div class="service-data">
        <div class="activity-data">
			<?
				$actionData = $shortcut->getActivityData();
				echo CJSON::encode(array('type' => 'Shortcut Tile', 'subType' => $actionData['action'], 'data' => $actionData['details']));
			?>
        </div>
    </div>
    <div data-role='header' class="page-header" data-position="fixed">
		<? if ($shortcut->showNavigationPanel): ?>
            <a href="#shortcut-link-page-<? echo $shortcut->id; ?>-popup-panel-left" class="navigation-panel-toggle"
               data-icon="ion-navicon-round" data-iconpos="notext"></a>
		<? endif; ?>
        <h1 class="header-title"><? echo $shortcut->headerTitle != '' ? $shortcut->headerTitle : $shortcut->title; ?></h1>
		<? if ($shortcut->headerSettings->showRightButton): ?>
            <a href="#shortcut-link-page-<? echo $shortcut->id; ?>-popup-panel-right" class="ui-btn-right"
               data-icon="ion-navicon-round" data-iconpos="notext"></a>
		<? endif; ?>
    </div>
    <div data-role='content' class="main-content">
        <table class="content-header"
		       <? if (!empty($shortcut->headerSettings->topLogo)): ?>style="margin-bottom: 30px;" <? endif; ?>>
			<? if ($shortcut->headerSettings->showTitle || $shortcut->headerSettings->showBackButton): ?>
                <tr>
                    <td class="title column-toggle-placeholder">
						<? if ($shortcut->headerSettings->showTitle): ?>
							<? echo $shortcut->title; ?>
						<? endif; ?>
                    </td>
					<? if ($shortcut->headerSettings->showBackButton): ?>
                        <td class="back">
							<? if ($shortcut->samePage): ?>
                                <a href="#" data-role="button" data-icon="ion-arrow-left-a" data-mini="true"
                                   data-inline="true" data-transition="slidefade" data-direction="reverse">BACK</a>
							<? endif; ?>
                        </td>
					<? endif; ?>
                </tr>
			<? endif; ?>
			<? if (!empty($shortcut->headerSettings->topLogo)): ?>
                <tr>
                    <td class="shortcut-header-logo" <? if ($shortcut->headerSettings->showTopLogoDivider): ?> style="border-bottom: solid 1px <? echo Utils::formatColor($shortcut->headerSettings->topDividerColor); ?>;" <? endif; ?>>
                        <img src="<? echo $shortcut->headerSettings->topLogo; ?>">
                    </td>
                </tr>
			<? endif; ?>
        </table>
        <div class="content-data">
			<? echo $shortcutContent; ?>
        </div>
    </div>
	<? if ($shortcut->showNavigationPanel): ?>
        <div data-role="panel" data-display="overlay"
             id="shortcut-link-page-<? echo $shortcut->id; ?>-popup-panel-left">
            <ul class="navigation-items-container navigation-items-container-main" data-role="listview">
            </ul>
        </div>
	<? endif; ?>
    <div data-role="panel" data-display="overlay" data-position="right"
         id="shortcut-link-page-<? echo $shortcut->id; ?>-popup-panel-right">
        <ul data-role="listview">
			<? echo $this->renderPartial('../shortcuts/groups/groupList'); ?>
            <li data-icon="false">
                <a class="logout-button" href="#">Log Out</a>
            </li>
            <li data-role="list-divider"><p class="user-info">User: <? echo UserIdentity::getCurrentUserLogin(); ?></p>
            </li>
            <li data-role="list-divider"><p>Copyright 2018 adSALESapps.com</p></li>
        </ul>
    </div>
    <div id="shortcut-link-page-<? echo $shortcut->id; ?>-download-warning-popup" data-role="popup" data-theme="a"
         data-overlay-theme="d" data-dismissible="false">
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