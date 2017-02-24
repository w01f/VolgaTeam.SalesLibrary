<?
	/**
	 * @var $shortcut PageContentShortcut
	 * @var $shortcutContent string
	 */
?>
<div id="shortcut-link-page-<? echo $shortcut->id; ?>" class="shortcut-link-page" data-role='page' data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed">
		<a href="#shortcut-link-page-<? echo $shortcut->id; ?>-popup-panel-left" class="navigation-panel-toggle" data-icon="ion-navicon-round" data-iconpos="notext"></a>
		<h1 class="header-title"><? echo $shortcut->headerTitle != '' ? $shortcut->headerTitle : $shortcut->title; ?></h1>
		<a href="#shortcut-link-page-<? echo $shortcut->id; ?>-popup-panel-right" class="ui-btn-right" data-icon="ion-navicon-round" data-iconpos="notext"></a>
	</div>
	<div data-role='content' class="main-content">
		<table class="content-header">
			<tr>
				<td class="title column-toggle-placeholder">
					<? echo $shortcut->title; ?>
				</td>
				<td class="back">
					<? if ($shortcut->samePage): ?>
						<a href="#" data-role="button" data-icon="ion-arrow-left-a" data-mini="true" data-inline="true" data-transition="slidefade" data-direction="reverse">BACK</a>
					<? endif; ?>
				</td>
			</tr>
		</table>
		<div class="content-data">
			<? echo $shortcutContent; ?>
		</div>
	</div>
	<div data-role="panel" data-display="overlay" id="shortcut-link-page-<? echo $shortcut->id; ?>-popup-panel-left">
        <ul class="navigation-items-container" data-role="listview">
		</ul>
	</div>
	<div data-role="panel" data-display="overlay" data-position="right" id="shortcut-link-page-<? echo $shortcut->id; ?>-popup-panel-right">
        <ul data-role="listview">
			<? echo $this->renderPartial('../shortcuts/groups/groupList'); ?>
            <li data-icon="false">
                <a class="logout-button" href="#">Log Out</a>
            </li>
            <li data-role="list-divider"><p class="user-info">User: <? echo UserIdentity::getCurrentUserLogin(); ?></p></li>
            <li data-role="list-divider"><p>Copyright 2015 adSALESapps.com</p></li>
        </ul>
	</div>
	<div id="shortcut-link-page-<? echo $shortcut->id; ?>-download-warning-popup" data-role="popup" data-theme="a" data-overlay-theme="d" data-dismissible="false">
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