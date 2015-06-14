<?
	/**
	 * @var $tabPages array
	 */

	$cs = Yii::app()->clientScript;
	$cs->registerCoreScript('jquery');
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/mobile/css/jquery.mobile.ios.theme.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/cubeportfolio/css/cubeportfolio.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/layout.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/home.css?' . Yii::app()->params['version']);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/mobile/js/jquery.mobile.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/cubeportfolio/js/jquery.cubeportfolio.min.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/login.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/home.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);

	$siteUrl = Yii::app()->getBaseUrl(true);
	$siteName = str_replace('http://', '', $siteUrl);
	$staticTabLogoFolderPath = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'jqm-icons';
?>
<div data-role='page' id="home" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="a">
		<a href="#home-popup-panel-left" data-icon="ion-navicon-round" data-iconpos="notext"></a>
		<h1 class="header-title"><? echo Yii::app()->params['jqm_home_page_title']; ?></h1>
		<a href="#home-popup-panel-right" data-icon="ion-navicon-round" data-iconpos="notext"></a>
	</div>
	<div data-role='content' class="main-content">
		<div class="tab-items">
			<div class="cbp-l-grid-masonry">
				<? foreach ($tabPages as $tabName => $tabIndex): ?>
					<? $url = TabPages::getTabUrl($tabName); ?>
					<? if ($tabName == 'home_tab'): ?>
						<? $logoContent = 'data:image/png;base64,' . base64_encode(file_get_contents($staticTabLogoFolderPath . DIRECTORY_SEPARATOR . 'stations.png')); ?>
						<a class="cbp-item tab-item" data-ajax="false" href="<? echo $url; ?>">
							<div class="cbp-caption">
								<img class="logo" src="<? echo $logoContent; ?>">
								<p class="title"><? echo Yii::app()->params['home_tab']['jqm_name'] ?></p>
							</div>
						</a>
					<? elseif ($tabName == 'search_full_tab'): ?>
						<? $logoContent = 'data:image/png;base64,' . base64_encode(file_get_contents($staticTabLogoFolderPath . DIRECTORY_SEPARATOR . 'search.png')); ?>
						<a class="cbp-item tab-item not-working" data-ajax="false" href="<? echo $url; ?>">
							<div class="cbp-caption">
								<img class="logo" src="<? echo $logoContent; ?>">
								<p class="title"><? echo Yii::app()->params['search_full_tab']['jqm_name'] ?></p>
							</div>
						</a>
					<?
					elseif ($tabName == 'favorites_tab'): ?>
						<? $logoContent = 'data:image/png;base64,' . base64_encode(file_get_contents($staticTabLogoFolderPath . DIRECTORY_SEPARATOR . 'favorites.png')); ?>
						<a class="cbp-item tab-item not-working" data-ajax="false" href="<? echo $url; ?>">
							<div class="cbp-caption">
								<img class="logo" src="<? echo $logoContent; ?>">
								<p class="title"><? echo Yii::app()->params['favorites_tab']['jqm_name'] ?></p>
							</div>
						</a>
					<?
					elseif (strpos($tabName, 'shortcuts-tab-') !== false): ?>
						<?
						/** @var $tabShortcutsRecord ShortcutsTabRecord */
						$tabShortcutsRecord = ShortcutsTabRecord::model()->findByPk(str_replace('shortcuts-tab-', '', $tabName));
						?>
						<? if (isset($tabShortcutsRecord)): ?>
							<a class="cbp-item tab-item" data-ajax="false" href="<? echo $url; ?>">
								<div class="cbp-caption">
									<img class="logo" src="<? echo Yii::app()->getBaseUrl(true) . $tabShortcutsRecord->image_path . '/jqmlogo.png?' . $tabShortcutsRecord->id; ?>">
									<p class="title"><? echo $tabShortcutsRecord->getPhoneName(); ?></p>
								</div>
							</a>
						<? endif; ?>
					<? endif; ?>
				<? endforeach; ?>
			</div>
		</div>
	</div>
	<div class="page-footer main-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="a">
			<span class="ui-mini login">
				<? if (isset(Yii::app()->user->login)): ?>
					<? echo Yii::app()->user->login; ?>
				<? endif; ?>
			</span>
	</div>
	<div data-role="panel" data-display="overlay" id="home-popup-panel-left">
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
	<div data-role="panel" data-display="overlay" data-position="right" id="home-popup-panel-right">
		<ul data-role="listview">
			<? echo $this->renderPartial('../wallbin/libraryList'); ?>
		</ul>
	</div>
	<div id="not-working-message" data-role="popup" data-theme="a" data-overlay-theme="d" data-dismissible="false">
		<div data-role="header" data-theme="d">
			<h1>Not Yet</h1>
		</div>
		<div role="main" style="padding:0 20px">
			<p>This Smartphone Feature is still in development…</p>
			<div>
				<a class="accept-button" href="#" data-inline="true" data-role="button" data-rel="back" data-theme="d">OK</a>
			</div>
		</div>
	</div>
</div>

