<?
	/**
	 * @var $library Library
	 * @var $tabPages array
	 */

	$cs = Yii::app()->clientScript;
	$cs->registerCoreScript('jquery');
	$cs->registerCoreScript('cookie');
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/mobile/css/jquery.mobile.ios.theme.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/photoswipe/photoswipe.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/slick-slider/slick.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/video-js/video-js.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/layout.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/link-viewer.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/wallbin.css?' . Yii::app()->params['version']);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/json/jquery.json-2.3.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/mobile/js/jquery.mobile.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/photoswipe/lib/klass.min.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/photoswipe/code.photoswipe.jquery-3.0.5.min.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/slick-slider/slick.min.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/video-js/video.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/common/link-viewer-data.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/login.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/link-viewer.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/link-viewer-file.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/link-viewer-document.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/wallbin.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);

	$siteUrl = Yii::app()->getBaseUrl(true);
	$siteName = str_replace('http://', '', $siteUrl);

	$defaultPage = $library->pages[0];
?>
<div data-role='page' id="library" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="a">
		<a href="#library-popup-panel-left" data-icon="ion-navicon-round" data-iconpos="notext"></a>
		<h1 class="header-title"><? echo $library->name ?></h1>
		<a href="#library-popup-panel-right" data-icon="ion-navicon-round" data-iconpos="notext"></a>
	</div>
	<div data-role='content' class="main-content">
		<table class="content-header">
			<tr>
				<td class="title">
					<a data-rel="popup" data-ajax="false" href="#library-popup-panel-pages"><? echo $defaultPage->name; ?></a>
				</td>
				<td class="logo">
					<a data-rel="popup" data-ajax="false" href="#library-popup-panel-pages"><img src="<? echo Yii::app()->getBaseUrl(true) . '/' . $library->logoLink ?>"></a>
				</td>
			</tr>
		</table>
		<div class="content-data">
			<? echo $this->renderPartial('pageContent', array('page' => $defaultPage)); ?>
		</div>
	</div>
	<div class="page-footer main-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="a">
		<div class="ui-grid-a">
			<div class="ui-block-a">
				<span class="ui-mini login">
					<? if (isset(Yii::app()->user->login)): ?>
						<? echo Yii::app()->user->login; ?>
					<? endif; ?>
				</span>
			</div>
			<div class="ui-block-b entities-count">
				<span class="ui-mini"><? echo LinkRecord::getLinksCountByLibrary($library->id); ?> files</span>
			</div>
		</div>
	</div>
	<div data-role="panel" data-display="overlay" id="library-popup-panel-left">
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
	<div data-role="panel" data-display="overlay" data-position="right" id="library-popup-panel-right">
		<ul data-role="listview">
			<? echo $this->renderPartial('../wallbin/libraryList'); ?>
		</ul>
	</div>
	<div data-role="panel" data-display="overlay" data-position="right" id="library-popup-panel-pages">
		<ul data-role="listview">
			<li data-role="list-divider" class="header">
				<a href="#" data-ajax="false"><img src="<? echo Yii::app()->getBaseUrl(true) . '/' . $library->logoLink ?>">
					<span><? echo $library->name ?></span> </a>
			</li>
			<? foreach ($library->pages as $page): ?>
				<li data-icon="false" class="page-item">
					<a data-ajax="false" href="#"> <span><? echo $page->name; ?></span>
						<div class="service-data">
							<div class="page-id"><? echo $page->id; ?></div>
						</div>
					</a>
				</li>
			<? endforeach; ?>
		</ul>
	</div>
</div>
