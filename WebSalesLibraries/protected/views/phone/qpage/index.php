<?
	/** @var $page QPageRecord */

	$cs = Yii::app()->clientScript;
	$cs->registerCoreScript('jquery');
	$cs->registerCoreScript('cookie');
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/mobile/css/jquery.mobile.ios.theme.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/photoswipe/photoswipe.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/slick-slider/slick.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/video-js/video-js.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/layout.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/link-viewer.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/qpage.css?' . Yii::app()->params['version']);
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
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/qpage.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);

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
<script type="text/javascript">
	window.BaseUrl = '<?php echo Yii::app()->getBaseUrl(true); ?>' + '/qpage/';
</script>
<div data-role='page' id="quicksite" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="a">
		<? if ($authorized): ?>
			<a href="#quicksite-popup-panel-left" data-icon="ion-navicon-round" data-iconpos="notext"></a>
		<? endif; ?>
		<h1 class="header-title">Quicksite</h1>
		<? if ($authorized): ?>
			<a href="#quicksite-popup-panel-right" data-icon="ion-navicon-round" data-iconpos="notext"></a>
		<? endif; ?>
	</div>
	<div data-role='content' class="main-content">
		<table class="content-header qpage-header">
			<tr>
				<td class="logo">
					<img src="<? echo $page->logo; ?>">
				</td>
				<td class="title gray">
					<? echo $page->title; ?>
				</td>
			</tr>
		</table>
		<div class="content-data qpage-data">
			<h3>
				<?
					$string = $page->subtitle;
					$string = preg_replace('/[\n\r ]*<style[^>]*>(([^<]|[<[^\/]|<\/[^s]|<\/s[^t])*)<\/style>[\n\r ]*/i', '', $string);
					echo $string;
				?>
			</h3>
			<p><? echo $page->header; ?></p>
			<? if ($page->record_activity): ?>
				<label for="user-email">To view the links on this site, enter your email address:</label>
				<input type="email" id="user-email" name="user-email" placeholder="Email Address" required data-mini="true" <? if (isset(Yii::app()->user->email)): ?>value="<? echo Yii::app()->user->email; ?>" <? endif; ?>>
			<? endif; ?>
			<div class="qpage-files">
				<h3>Files:</h3>
				<? $links = $page->getLibraryLinks(); ?>
				<? foreach ($links as $link): ?>
					<? if (!in_array($link->type, array(5, 6))): ?>
						<div><a class="file-link" href="#"><span><? echo $link->name; ?></span>
								<div class="service-data">
									<div class="link-id"><? echo $link->id; ?></div>
								</div>
							</a></div>
					<? endif; ?>
				<? endforeach; ?>
			</div>
			<p><? echo strip_tags($page->footer); ?></p>
		</div>
	</div>
	<? if ($authorized): ?>
		<div class="page-footer main-footer" data-role='footer'  data-position="fixed" data-theme="a">
			<div class="ui-grid-a">
				<div class="ui-block-a">
				<span class="ui-mini login">
					<? if (isset(Yii::app()->user->login)): ?>
						<? echo Yii::app()->user->login; ?>
					<? endif; ?>
				</span>
				</div>
				<div class="ui-block-b">
				</div>
			</div>
		</div>
	<? endif; ?>
	<div id="email-warning-dialog" data-role="popup" data-theme="a" data-overlay-theme="d" data-dismissible="false">
		<div data-role="header" data-theme="d">
			<h1>Email</h1>
		</div>
		<div role="main" style="padding:0 40px">
			<p>Enter your email address to view this link...</p>
			<a href="#" data-role="button" data-theme="d" data-rel="back">OK</a>     
		</div>
	</div>
	<? if ($authorized): ?>
		<div data-role="panel" data-display="overlay" id="quicksite-popup-panel-left">
			<ul data-role="listview">
				<? if (Yii::app()->params['jqm_home_page_enabled'] == true): ?>
					<li data-icon="false">
						<a data-ajax="false" href="<? echo $siteUrl; ?>"><? echo $siteName; ?></a>
					</li>
				<? endif; ?>
				<? echo $this->renderPartial('../site/tabPageList', array('tabPages' => $tabPages, 'librariesPopupId' => 'quicksite-popup-panel-right')); ?>
				<li data-icon="false">
					<a class="logout-button" href="#">Log Out</a>
				</li>
				<li data-role="list-divider"><p>Copyright 2015 adSALESapps.com</p></li>
			</ul>
		</div>
		<div data-role="panel" data-display="overlay" data-position="right" id="quicksite-popup-panel-right">
			<ul data-role="listview">
				<? echo $this->renderPartial('../wallbin/libraryList'); ?>
			</ul>
		</div>
	<? endif; ?>
</div>
