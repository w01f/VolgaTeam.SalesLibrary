<?
	/**
	 * @var $library Library
	 * @var $defaultPage LibraryPage
	 * @var $tabPageExisted boolean
	 * @var $tabPages array
	 */

	$cs = Yii::app()->clientScript;
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/mobile/css/jquery.mobile.ios.theme.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/photoswipe/photoswipe.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/slick-slider/slick.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/video-js/video-js.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/layout.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/link-viewer.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/email.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/favorites.css?' . Yii::app()->params['version']);
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
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/email.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/favorites.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/wallbin.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);

	$siteUrl = Yii::app()->getBaseUrl(true);
	$siteName = str_replace('http://', '', $siteUrl);

	$libraryManager = new LibraryManager();
	$allLibraries = $libraryManager->getLibraries();
?>
<div data-role='page' id="library" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="a">
		<a href="#library-popup-panel-left" data-icon="ion-navicon-round" data-iconpos="notext"></a>
		<h1 class="header-title"><? echo $library->alias ?></h1>
		<a href="#library-popup-panel-right" data-icon="ion-navicon-round" data-iconpos="notext"></a>
	</div>
	<div data-role='content' class="main-content">
		<a class="content-header vertical" data-rel="popup" data-ajax="false" href="#library-popup-panel-pages">
			<div class="logo"><img src="<? echo Yii::app()->getBaseUrl(true) . '/' . $defaultPage->logoLink ?>"></div>
			<div class="title"><? echo $defaultPage->name; ?></div>
		</a>
		<div class="content-data">
			<? echo $this->renderPartial('pageContent', array('page' => $defaultPage)); ?>
		</div>
	</div>
	<div class="page-footer main-footer" data-role='footer' data-position="fixed" data-theme="a">
		<ul data-role="listview">
			<? if (count($allLibraries) > 1): ?>
				<li class="library-action delete-library-page<? if ($tabPageExisted == true): ?> active-action<? endif; ?>">
					<h4>
						<a data-rel="popup" data-position-to="window" href="#library-popup-delete-tab-page">Remove this Library from the HOME SCREEN</a>
					</h4>
				</li>
				<li class="library-action add-library-page<? if ($tabPageExisted != true): ?> active-action<? endif; ?>">
					<h4>
						<a data-rel="popup" data-position-to="window" href="#library-popup-add-tab-page">Add this Library to the HOME SCREEN</a>
					</h4>
				</li>
			<? endif; ?>
			<li class="footer-info">
				<div class="ui-grid-a">
					<div class="ui-block-a">
					</div>
					<div class="ui-block-b entities-count">
						<span class="ui-mini"><? echo LinkRecord::getLinksCountByLibrary($library->id); ?> files</span>
					</div>
				</div>
			</li>
		</ul>
	</div>
	<div data-role="panel" data-display="overlay" id="library-popup-panel-left">
		<ul data-role="listview">
			<? if (Yii::app()->params['jqm_home_page_enabled'] == true): ?>
				<li data-icon="false">
					<a data-ajax="false" href="<? echo $siteUrl; ?>"><? echo $siteName; ?></a>
				</li>
			<? endif; ?>
			<? echo $this->renderPartial('../site/tabPageList', array('tabPages' => $tabPages, 'librariesPopupId' => 'library-popup-panel-right')); ?>
			<li data-icon="false">
				<a class="logout-button" href="#">Log Out</a>
			</li>
			<li data-role="list-divider"><p class="user-info">User: <? echo Yii::app()->user->login; ?></p></li>
			<li data-role="list-divider"><p>Copyright 2015 adSALESapps.com</p></li>
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
				<a href="#" data-ajax="false"><span><? echo $library->alias ?></span></a>
			</li>
			<? foreach ($library->pages as $page): ?>
				<li data-icon="false" class="page-item">
					<a data-ajax="false" href="#"> <span><? echo $page->name; ?></span>
						<div class="service-data">
							<div class="page-id"><? echo $page->id; ?></div>
							<div class="page-logo"><? echo Yii::app()->getBaseUrl(true) . '/' . $page->logoLink ?></div>
						</div>
					</a>
				</li>
			<? endforeach; ?>
			<li data-role="list-divider"><p>Copyright 2015 adSALESapps.com</p></li>
		</ul>
	</div>
	<div id="library-popup-add-tab-page" data-role="popup" data-theme="a" data-overlay-theme="d" data-dismissible="false">
		<div data-role="header" data-theme="d">
			<h1><? echo $library->alias ?></h1>
		</div>
		<div role="main" style="padding:0 20px">
			<p class="warning-text">Add the <? echo $library->alias ?> Library to your HOME SCREEN?</p>
			<div class="ui-grid-a">
				<div class="ui-block-a">
					<a class="accept-action" href="#" data-role="button" data-theme="d">Add</a>     
				</div>
				<div class="ui-block-b">
					<a href="#" data-role="button" data-theme="d" data-rel="back">Cancel</a>
				</div>
			</div>
		</div>
	</div>
	<div id="library-popup-delete-tab-page" data-role="popup" data-theme="a" data-overlay-theme="d" data-dismissible="false">
		<div data-role="header" data-theme="d">
			<h1><? echo $library->alias ?></h1>
		</div>
		<div role="main" style="padding:0 20px">
			<p class="warning-text">REMOVE the <? echo $library->alias ?> Library from your HOME SCREEN?</p>
			<div class="ui-grid-a">
				<div class="ui-block-a">
					<a class="accept-action" href="#" data-role="button" data-theme="d">Remove</a>     
				</div>
				<div class="ui-block-b">
					<a href="#" data-role="button" data-theme="d" data-rel="back">Cancel</a>
				</div>
			</div>
		</div>
	</div>
	<div class="service-data">
		<div class="library-id"><? echo $library->id; ?></div>
	</div>
</div>
