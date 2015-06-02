<?
	/**
	 * @var $data FilePreviewData
	 */
	$tabPages = TabPages::getList();
	$siteUrl = Yii::app()->getBaseUrl(true);
	$siteName = str_replace('http://', '', $siteUrl);
?>
<div data-role='page' id="link-viewer" class="link-viewer-page" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed">
		<a href="#link-viewer-popup-panel-left" data-icon="ion-navicon-round" data-iconpos="notext"></a>
		<h1 class="header-title"></h1>
		<a href="#link-viewer-popup-panel-right" data-icon="ion-navicon-round" data-iconpos="notext"></a>
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
	<div class="page-footer main-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="a">
		<div class="ui-grid-a">
			<div class="ui-block-a">
				<span class="ui-mini">
					<? if (isset(Yii::app()->user->login)): ?>
						(<? echo Yii::app()->user->login; ?>)
					<? endif; ?>
					<a href="#link-viewer-logout-dialog-accept" data-rel="popup" data-position-to="window" data-transition="pop">Log Out</a>
				</span>
			</div>
			<div class="ui-block-b link-viewer-info">
				<span class="ui-mini"><strong><? echo $data->linkTitle; ?></strong></span>
			</div>
		</div>
	</div>
	<div data-role="panel" data-display="overlay" id="link-viewer-popup-panel-left">
		<ul data-role="listview">
			<li data-icon="ion-navicon-round-icon-left">
				<a data-ajax="false" href="<? echo $siteUrl; ?>"><? echo $siteName; ?></a>
			</li>
			<? echo $this->renderPartial('../site/tabPageList', array('tabPages' => $tabPages)); ?>
		</ul>
	</div>
	<div data-role="panel" data-display="overlay" data-position="right" id="link-viewer-popup-panel-right">
		<ul data-role="listview">
			<? echo $this->renderPartial('../wallbin/libraryList'); ?>
		</ul>
	</div>
	<? echo $this->renderPartial('../auth/logoutDialog', array('idPrefix' => 'link-viewer')); ?>
</div>