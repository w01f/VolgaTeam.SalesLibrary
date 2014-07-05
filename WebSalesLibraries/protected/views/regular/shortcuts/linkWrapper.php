<?
	/**
	 * @var $objectName string
	 * @var $objectLogo string
	 * @var $content string
	 */
	$cs = Yii::app()->clientScript;
	$cs->registerCssFile($cs->getCoreScriptUrl() . '/jui/css/base/jquery-ui.min.css');
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/video-js/video-js.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/bootstrap/css/bootstrap.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/view-dialog.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/view-dialog-bar.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/tool-dialog.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/folder-links.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/banner.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/layout.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/ribbon.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/shortcuts.css?' . Yii::app()->params['version']);
	$cs->registerCoreScript('jquery.ui');
	$cs->registerCoreScript('cookie');
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/json/jquery.json-2.3.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/lib/jquery.mousewheel-3.0.6.pack.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/video-js/video.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/bootstrap/js/bootstrap.min.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/gesture-handler/jquery.hammer.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/ribbon.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/overlay.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/link-viewing.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/view-dialog-bar.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/links-grid.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/favorites.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/link-rate.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/login.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/shortcuts/controller.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
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