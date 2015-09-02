<?
	/**
	 * @var $parentId string
	 */
?>
<div id="search-results" class="search-results-page shortcut-link-page" data-role='page' data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed">
		<a href="#search-results-popup-panel-left" data-icon="ion-navicon-round" data-iconpos="notext"></a>
		<h1 class="header-title">Search Results</h1>
		<a href="#search-results-popup-panel-right" data-icon="ion-navicon-round" data-iconpos="notext"></a>
	</div>
	<div data-role='content' class="main-content">
		<table class="content-header">
			<tr>
				<td class="title column-toggle-placeholder">
				</td>
				<td class="back">
					<a href="#<? echo $parentId; ?>" data-role="button" data-icon="ion-arrow-left-a" data-mini="true" data-inline="true" data-transition="slidefade" data-direction="reverse">BACK</a>
				</td>
			</tr>
		</table>
		<div class="content-data">
		</div>
	</div>
	<div class="page-footer main-footer" data-role='footer'  data-position="fixed" data-theme="a">
		<div class="ui-grid-a">
			<div class="ui-block-a">
			</div>
			<div class="ui-block-b entities-count">
				<span class="ui-mini"></span>
			</div>
		</div>
	</div>
	<div data-role="panel" data-display="overlay" id="search-results-popup-panel-left">
		<ul data-role="listview">
			<? echo $this->renderPartial('../shortcuts/groups/groupList'); ?>
			<li data-icon="false">
				<a class="logout-button" href="#">Log Out</a>
			</li>
			<li data-role="list-divider"><p class="user-info">User: <? echo Yii::app()->user->login; ?></p></li>
			<li data-role="list-divider"><p>Copyright 2015 adSALESapps.com</p></li>
		</ul>
	</div>
	<div data-role="panel" data-display="overlay" data-position="right" id="search-results-popup-panel-right">
		<ul data-role="listview">
			<? echo $this->renderPartial('../wallbin/libraryList'); ?>
		</ul>
	</div>
</div>