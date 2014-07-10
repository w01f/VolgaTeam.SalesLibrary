<?php
	$cs = Yii::app()->clientScript;
	$cs->registerCssFile(Yii::app()->clientScript->getCoreScriptUrl() . '/jui/css/base/jquery-ui.css');
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/jquery.fancybox.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/video-js/video-js.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/bootstrap/css/bootstrap.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/star-rating/css/star-rating.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/view-dialog.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/view-dialog-bar.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/tool-dialog.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/folder-links.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/banner.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/layout.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/ribbon.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/qbuilder/logo-list.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/qpage/page-content.css?' . Yii::app()->params['version']);
	$cs->registerCoreScript('jquery.ui');
	$cs->registerCoreScript('cookie');
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/json/jquery.json-2.3.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/jquery.fancybox.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/lib/jquery.mousewheel-3.0.6.pack.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/video-js/video.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/bootstrap/js/bootstrap.min.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/gesture-handler/jquery.hammer.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/star-rating/js/star-rating.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/ribbon.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/overlay.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/link-viewing.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/view-dialog-bar.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/links-grid.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/favorites.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/link-rate.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/login.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/qbuilder/page-list.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/qpage/page-links.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/qpage/controller.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$userId = Yii::app()->user->getId();
?>
<script type="text/javascript">
	window.BaseUrl = '<?php echo Yii::app()->getBaseUrl(true); ?>' + '/qpage/';
</script>
<div id="ribbon">
	<div class="ribbon-window-title"></div>
	<div class="ribbon-tab">
		<span class="ribbon-title"><? echo $page->title; ?></span>
		<div class="ribbon-section">
			<span class="section-title"><? echo $page->title; ?></span> <img src="<? echo $page->logo; ?>"/>
		</div>
		<? if ($page->record_activity): ?>
			<div class="ribbon-section">
				<form class="form-horizontal" role="form">
					<div class="form-group">
						<p class="form-control-static text-danger">
							<strong>To view the links on this site, enter your email address:</strong>
						</p>
					</div>
					<div class="form-group">
						<input type="email" id="user-email" class="form-control" placeholder="Email" required <? if (isset(Yii::app()->user->email)): ?>value="<? echo Yii::app()->user->email; ?>" <? endif; ?>>
					</div>
				</form>
			</div>
		<? endif; ?>
		<? if (!$page->restricted && !isset($userId)): ?>
			<div class="ribbon-section">
				<span class="section-title">User Login</span>
				<div class="ribbon-button ribbon-button-large" id="login-button" rel="tooltip" title="To email links, Log into the site">
					<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/login.png' ?>"/>
					<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/login.png' ?>"/>
					<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/login.png' ?>"/>
				</div>
			</div>
		<? endif; ?>
	</div>
</div>
<div id="content" oncontextmenu="return false;">
	<div>
		<div id="page-id" style="display: none;"><? echo $page->id; ?></div>
		<div id="page-title"><? echo $page->subtitle; ?></div>
		<div id="page-header"><? echo nl2br($page->header); ?></div>
		<div id="page-links-container" class="folder-links-container">
			<? if ($page->show_links_as_url): ?>
			<ul class="nav nav-pills nav-stacked"><? endif; ?>
				<? $links = $page->getLibraryLinks() ?>
				<? if (isset($links)): ?>
					<?php foreach ($links as $link): ?>
						<? if ($page->show_links_as_url): ?>
							<? if ($link->name != '' && !$link->isFolder): ?>
								<li>
									<a href="#" id="link<?php echo $link->id; ?>" class="clickable" style="text-decoration: underline;"><? echo $link->name; ?></a>
								</li>
							<? endif; ?>
						<? else: ?>
							<? echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/link.php', array('link' => $link, 'disableBanner' => $page->disable_banners, 'disableWidget' => $page->disable_widgets), true); ?>
						<? endif; ?>
					<?php endforeach; ?>
				<? endif; ?>
				<? if ($page->show_links_as_url): ?></ul><? endif; ?>
		</div>
		<div id="page-footer"><? echo nl2br($page->footer); ?></div>
	</div>
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