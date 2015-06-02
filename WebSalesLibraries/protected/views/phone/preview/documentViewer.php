<?
	/**
	 * @var $data DocumentPreviewData
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
			<div class="ui-grid-b">
				<div class="ui-block-a">
					<a href="#link-viewer-open-menu" data-role="button" data-rel="popup" data-inline="true" data-theme="a">Open</a>
				</div>
				<div class="ui-block-b">
					<a id="link-viwer-open-full-screen" href="#" data-role="button" data-inline="true" data-theme="a" data-transition="slidefade">Full Screen</a>
				</div>
				<div class="ui-block-c">
					<a href="#link-viewer-options-menu" data-role="button" data-rel="popup" data-inline="true" data-theme="a">Options</a>
				</div>
			</div>
		</div>
		<div class="slider images">
			<? foreach ($data->pagesInPng as $pngPage): ?>
				<div><img src="<? echo $pngPage->href; ?>"></div>
			<? endforeach; ?>
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
	<div data-role="popup" id="link-viewer-open-menu" data-theme="a">
		<ul data-role="listview" data-inset="true" style="min-width:250px;" data-corners="false">
			<li data-role="list-divider" data-theme="d">Open this file...</li>
			<li>
				<a href="#link-viewer-gallery" data-transition="slidefade">Thumb Gallery</a>
			</li>
			<li>
				<a class="popup-open-action" href="<? echo $data->url; ?>" target="_blank" data-rel="external"><? echo $data->linkTitle; ?></a>
			</li>
			<li>
				<a class="popup-open-action" href="<? echo $data->documentInPdf->link; ?>" target="_blank" data-rel="external">Adobe PDF</a>
			</li>
		</ul>
	</div>
	<div data-role="popup" id="link-viewer-options-menu" data-theme="a">
		<ul data-role="listview" data-inset="true" style="min-width:250px;" data-corners="false">
			<li data-role="list-divider" data-theme="d">File Options...</li>
			<li><a href="#" data-rel="popup">Email this Link</a></li>
			<li><a href="#" data-rel="popup">Save to Favorites</a></li>
		</ul>
	</div>
	<? echo $this->renderPartial('../auth/logoutDialog', array('idPrefix' => 'link-viewer')); ?>
</div>
<div data-role='page' id="link-viewer-gallery" class="link-viewer-page" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed">
		<a href="#link-viewer-gallery-popup-panel-left" data-icon="ion-navicon-round" data-iconpos="notext"></a>
		<h1 class="header-title"></h1>
		<a href="#link-viewer-gallery-popup-panel-right" data-icon="ion-navicon-round" data-iconpos="notext"></a>
	</div>
	<div data-role='content' class="main-content">
		<table class="content-header">
			<tr>
				<td class="title gray"><? echo $data->fileName; ?></td>
				<td class="back">
					<a href="#link-viewer" data-role="button" data-icon="ion-arrow-left-a" data-mini="true" data-inline="true" data-transition="slidefade" data-direction="reverse">BACK</a>
				</td>
			</tr>
		</table>
		<ul class="gallery-items">
			<? for ($i = 0; $i < count($data->pagesInPng); $i++): ?>
				<li>
					<a href="<? echo $data->pagesInPng[$i]->href; ?>" rel="external"><img src="<? echo $data->thumbnails[$i]->link; ?>" alt="<? echo $data->pagesInPng[$i]->title; ?>"></a>
				</li>
			<? endfor; ?>
		</ul>
	</div>
	<div class="page-footer main-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="a">
		<div class="ui-grid-a">
			<div class="ui-block-a">
				<span class="ui-mini">
					<? if (isset(Yii::app()->user->login)): ?>
						(<? echo Yii::app()->user->login; ?>)
					<? endif; ?>
					<a href="#link-viewer-gallery-logout-dialog-accept" data-rel="popup" data-position-to="window" data-transition="pop">Log Out</a>
				</span>
			</div>
			<div class="ui-block-b link-viewer-info">
				<span class="ui-mini"><strong><? echo count($data->pagesInPng) ?> Slides</strong></span>
			</div>
		</div>
	</div>
	<div data-role="panel" data-display="overlay" id="link-viewer-gallery-popup-panel-left">
		<ul data-role="listview">
			<li data-icon="ion-navicon-round-icon-left">
				<a data-ajax="false" href="<? echo $siteUrl; ?>"><? echo $siteName; ?></a>
			</li>
			<? echo $this->renderPartial('../site/tabPageList', array('tabPages' => $tabPages)); ?>
		</ul>
	</div>
	<div data-role="panel" data-display="overlay" data-position="right" id="link-viewer-gallery-popup-panel-right">
		<ul data-role="listview">
			<? echo $this->renderPartial('../wallbin/libraryList'); ?>
		</ul>
	</div>
	<? echo $this->renderPartial('../auth/logoutDialog', array('idPrefix' => 'link-viewer-gallery')); ?>
</div>