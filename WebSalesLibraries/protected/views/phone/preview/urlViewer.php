<?
	/**
	 * @var $data UrlPreviewData
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
				<td class="title gray"><? echo $data->name; ?></td>
				<td class="back">
					<a href="#" data-role="button" data-icon="ion-arrow-left-a" data-mini="true" data-inline="true" data-transition="slidefade" data-direction="reverse">BACK</a>
				</td>
			</tr>
		</table>
		<div class="actions">
			<ul data-role="listview">
				<? foreach ($data->actions as $action): ?>
					<li class="action">
						<span class="action-text"><? echo $action->shortText; ?></span>
						<div class="service-data">
							<div class="tag"><? echo $action->tag; ?></div>
						</div>
					</li>
				<? endforeach; ?>
			</ul>
		</div>
	</div>
	<div class="page-footer main-footer" data-role='footer'  data-position="fixed" data-theme="a">
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
				<? if (Yii::app()->params['jqm_home_page_enabled'] == true): ?>
					<li data-icon="false">
						<a data-ajax="false" href="<? echo $siteUrl; ?>"><? echo $siteName; ?></a>
					</li>
				<? endif; ?>
				<? echo $this->renderPartial('../site/tabPageList', array('tabPages' => $tabPages, 'librariesPopupId' => 'link-viewer-popup-panel-right')); ?>
				<li data-icon="false">
					<a class="logout-button" href="#">Log Out</a>
				</li>
				<li data-role="list-divider"><p>Copyright 2015 adSALESapps.com</p></li>
			</ul>
		</div>
		<div data-role="panel" data-display="overlay" data-position="right" id="link-viewer-popup-panel-right">
			<ul data-role="listview">
				<? echo $this->renderPartial('../wallbin/libraryList'); ?>
			</ul>
		</div>
	<? endif; ?>
</div>