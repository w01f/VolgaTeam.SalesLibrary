<?
	/**
	 * @var $shortcut FavoritesShortcut
	 * @var $folders FavoritesFolder[]
	 * @var $links array
	 */
?>
<div data-role='page' id="favorites-view" class="shortcut-link-page" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="a">
		<a href="#favorites-view-popup-panel-left" data-icon="ion-navicon-round" data-iconpos="notext"></a>
		<h1 class="header-title">Favorites</h1>
		<a href="#favorites-view-popup-panel-right" data-icon="ion-navicon-round" data-iconpos="notext"></a>
	</div>
	<div data-role='content' class="main-content">
		<table class="content-header">
			<tr>
				<td class="title column-toggle-placeholder">
				</td>
				<td class="back">
					<a href="#shortcut-group" data-role="button" data-icon="ion-arrow-left-a" data-mini="true" data-inline="true" data-transition="slidefade" data-direction="reverse">BACK</a>
				</td>
			</tr>
		</table>
		<div class="content-data">
			<? echo $this->renderPartial('../favorites/foldersAndLinks', array('folders' => $folders, 'links' => $links, 'topLevel' => true)); ?>
		</div>
	</div>
	<div data-role="panel" data-display="overlay" id="favorites-view-popup-panel-left">
		<ul data-role="listview">
			<? echo $this->renderPartial('../shortcuts/groups/groupList'); ?>
			<li data-icon="false">
				<a class="logout-button" href="#">Log Out</a>
			</li>
			<li data-role="list-divider"><p class="user-info">User: <? echo UserIdentity::getCurrentUserLogin(); ?></p></li>
			<li data-role="list-divider"><p>Copyright 2015 adSALESapps.com</p></li>
		</ul>
	</div>
	<div data-role="panel" data-display="overlay" data-position="right" id="favorites-view-popup-panel-right">
		<ul data-role="listview">
			<? echo $this->renderPartial('../wallbin/libraryList'); ?>
		</ul>
	</div>
</div>
