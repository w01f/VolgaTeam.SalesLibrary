<?
	/**
	 * @var $data ImagePreviewData
	 */

	$siteUrl = Yii::app()->getBaseUrl(true);
	$siteName = str_replace('http://', '', $siteUrl);

	$authorized = false;
	$userId = Yii::app()->user->getId();
	if (isset($userId))
		$authorized = true;

	if ($authorized)
		$tabPages = TabPages::getList();
	else
		$tabPages = array();
?>
<div data-role='page' id="link-viewer" class="link-viewer-page" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed">
		<? if ($authorized): ?>
			<a href="#link-viewer-popup-panel-left" data-icon="ion-navicon-round" data-iconpos="notext"></a>
		<? endif; ?>
		<h1 class="header-title"></h1>
		<? if ($authorized): ?>
			<a href="#link-viewer-popup-panel-right" data-icon="ion-navicon-round" data-iconpos="notext"></a>
		<? endif; ?>
	</div>
	<div data-role='content' class="main-content">
		<table class="content-header">
			<tr>
				<td class="title gray"><? echo $data->fileName; ?></td>
				<td class="back">
					<a href="#" data-role="button" data-icon="ion-arrow-left-a" data-mini="true" data-inline="true" data-transition="slidefade" data-direction="reverse">BACK</a>
				</td>
			</tr>
		</table>
		<div class="actions">
			<div class="ui-grid-a">
				<div class="ui-block-a">
					<a href="<? echo $data->url; ?>" target="_blank" data-role="button" data-inline="true" data-theme="a">Open</a>
				</div>
				<div class="ui-block-b">
					<? if ($authorized): ?>
						<a href="#link-viewer-options-menu" data-role="button" data-rel="popup" data-inline="true" data-theme="a">Options</a>
					<? endif; ?>
				</div>
			</div>
		</div>
		<div class="thumbnail">
			<a href="<? echo $data->url; ?>" target="_blank"><img src="<? echo $data->url; ?>"></a>
		</div>
	</div>
	<div class="page-footer main-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="a">
		<div class="ui-grid-a">
			<div class="ui-block-a">
				<? if ($authorized): ?>
					<span class="ui-mini login">
						<? if (isset(Yii::app()->user->login)): ?>
							<? echo Yii::app()->user->login; ?>
						<? endif; ?>
					</span>
				<? endif; ?>
			</div>
			<div class="ui-block-b link-viewer-info">
				<span class="ui-mini"><strong><? echo $data->linkTitle; ?></strong></span>
			</div>
		</div>
	</div>
	<? if ($authorized): ?>
		<div data-role="panel" data-display="overlay" id="link-viewer-popup-panel-left">
			<ul data-role="listview">
				<li data-icon="false">
					<a data-ajax="false" href="<? echo $siteUrl; ?>"><? echo $siteName; ?></a>
				</li>
				<? echo $this->renderPartial('../site/tabPageList', array('tabPages' => $tabPages)); ?>
				<li data-icon="false">
					<a class="logout-button" href="#">Log Out</a>
				</li>
			</ul>
		</div>
		<div data-role="panel" data-display="overlay" data-position="right" id="link-viewer-popup-panel-right">
			<ul data-role="listview">
				<? echo $this->renderPartial('../wallbin/libraryList'); ?>
			</ul>
		</div>
		<div data-role="popup" id="link-viewer-options-menu" data-theme="a">
			<ul data-role="listview" data-inset="true" style="min-width:250px;" data-corners="false">
				<li data-role="list-divider" data-theme="d">File Options...</li>
				<li><a href="#" data-rel="popup">Email this Link</a></li>
				<li><a href="#" data-rel="popup">Save to Favorites</a></li>
			</ul>
		</div>
	<? endif; ?>
</div>