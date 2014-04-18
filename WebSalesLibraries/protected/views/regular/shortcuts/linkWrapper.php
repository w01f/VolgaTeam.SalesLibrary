<?php
	$version = '5.0';
	$cs = Yii::app()->clientScript;
	$cs->registerCssFile(Yii::app()->clientScript->getCoreScriptUrl() . '/jui/css/base/jquery-ui.css');
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/video-js/video-js.min.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/bootstrap/css/bootstrap.min.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/view-dialog.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/view-dialog-bar.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/tool-dialog.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/link-rate.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/folder-links.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/banner.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/ribbon.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/shortcuts.css?' . $version);
	$cs->registerCoreScript('jquery.ui');
	$cs->registerCoreScript('cookie');
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/json/jquery.json-2.3.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/lib/jquery.mousewheel-3.0.6.pack.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/video-js/video.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/bootstrap/js/bootstrap.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/overlay.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/link-viewing.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/view-dialog-bar.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/links-grid.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/favorites.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/link-rate.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/login.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/scaling.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/shortcuts/ribbon.js?' . $version, CClientScript::POS_HEAD);
	$userId = Yii::app()->user->getId();
?>
<div id="ribbon">
	<div class="ribbon-window-title"></div>
	<div class="ribbon-tab">
		<span class="ribbon-title"><? echo $objectName; ?></span>
		<div class="ribbon-section">
			<span class="section-title"><? echo $objectName; ?></span>
			<img src="<?php echo isset($objectLogo) && @getimagesize($objectLogo) ? $objectLogo : Yii::app()->getBaseUrl(true) . '/images/rbntab2logo.png' ?>"/>
		</div>
	</div>
</div>
<div id="content" oncontextmenu="return false;">
	<div class="padding"><? echo $content; ?></div>
</div>
<div id="content-overlay"></div>
<!--  View dialog hidden part  -->
<div>
	<a id="view-dialog-link" href="#view-dialog-container">View Options</a>
	<div id="view-dialog-wrapper">
		<div id="view-dialog-container"></div>
	</div>
</div>
<!------------------------->