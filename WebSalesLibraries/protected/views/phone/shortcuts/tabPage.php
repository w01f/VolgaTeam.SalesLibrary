<?
	/**
	 * @var $tabPages array
	 * @var $tabRecord ShortcutsTabRecord
	 */

	$cs = Yii::app()->clientScript;
	$cs->registerCoreScript('jquery');
	$cs->registerCoreScript('cookie');
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/mobile/css/jquery.mobile.ios.theme.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/photoswipe/photoswipe.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/slick-slider/slick.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/layout.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/shortcuts.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/search-result.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/link-viewer.css?' . Yii::app()->params['version']);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/json/jquery.json-2.3.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/mobile/js/jquery.mobile.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/photoswipe/lib/klass.min.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/photoswipe/code.photoswipe.jquery-3.0.5.min.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/slick-slider/slick.min.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/common/search-processor.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/common/link-viewer-data.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/login.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/shortcuts.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/shortcuts-search.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/data-table.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/link-viewer.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/link-viewer-file.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/link-viewer-document.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);


	$siteUrl = Yii::app()->getBaseUrl(true);
	$siteName = str_replace('http://', '', $siteUrl);

	/** @var $pageShortcuts ShortcutsPageRecord[] */
	$pageShortcuts = ShortcutsPageRecord::model()->findAll(array('order' => '`order`', 'condition' => 'id_tab=:id_tab', 'params' => array(':id_tab' => $tabRecord->id)));
	$pageCount = count($pageShortcuts);
	$currentPage = $pageCount > 0 ? $pageShortcuts[0]->getModel('grid') : null;
?>
	<div data-role='page' id="shortcuts" data-cache="never" data-dom-cache="false" data-ajax="false">
		<div data-role='header' class="page-header" data-position="fixed" data-theme="a">
			<a href="#shortcuts-popup-panel-left" data-icon="ion-navicon-round" data-iconpos="notext"></a>
			<h1 class="header-title"><? echo $tabRecord->name ?></h1>
			<a href="#shortcuts-popup-panel-right" data-icon="ion-navicon-round" data-iconpos="notext"></a>
		</div>
		<div data-role='content' class="main-content">
			<? if ($pageCount > 1): ?>
				<ul data-role="listview">
					<li data-role="fieldcontain">
						<label for="page-selector"></label>
						<select id="page-selector" data-native-menu="true" data-corners="false" data-mini="true">
							<? foreach ($pageShortcuts as $pageShortcut): ?>
								<? if ($pageShortcut->isEnabled(Yii::app()->user->login)): ?>
									<option value="<? echo $pageShortcut->id; ?>"><? echo $pageShortcut->name; ?></option>
								<? endif; ?>
							<? endforeach; ?>
						</select>
					</li>
				</ul>
			<? endif; ?>
			<div class="ui-grid-solo" id="shortcuts-links">
				<?
					if (isset($currentPage))
						echo $this->renderPartial('page', array('page' => $currentPage), true);
				?>
			</div>
		</div>
		<div class="page-footer main-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="a">
		<span class="ui-mini">
			<? if (isset(Yii::app()->user->login)): ?>
				(<? echo Yii::app()->user->login; ?>)
			<? endif; ?>
			<a href="#shortcuts-logout-dialog-accept" data-rel="popup" data-position-to="window" data-transition="pop">Log Out</a>
		</span>
		</div>
		<div data-role="panel" data-display="overlay" id="shortcuts-popup-panel-left">
			<ul data-role="listview">
				<li data-icon="ion-navicon-round-icon-left">
					<a data-ajax="false" href="<? echo $siteUrl; ?>"><? echo $siteName; ?></a>
				</li>
				<? echo $this->renderPartial('../site/tabPageList', array('tabPages' => $tabPages)); ?>
			</ul>
		</div>
		<div data-role="panel" data-display="overlay" data-position="right" id="shortcuts-popup-panel-right">
			<ul data-role="listview">
				<? echo $this->renderPartial('../wallbin/libraryList'); ?>
			</ul>
		</div>
		<? echo $this->renderPartial('../auth/logoutDialog', array('idPrefix' => 'shortcuts')); ?>
	</div>
<? echo $this->renderPartial('../search/searchResultPage', array(
	'tabPages' => $tabPages,
	'parentId' => 'shortcuts'
)); ?>