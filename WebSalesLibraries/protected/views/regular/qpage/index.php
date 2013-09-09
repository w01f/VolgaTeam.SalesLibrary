<?php
$version = '11.0';
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
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/qpage/page-content.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/qpage/ribbon.css?' . $version);
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
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/favorites.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/link-rate.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/login.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/qpage/scaling.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/qpage/page-links.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/qpage/ribbon.js?' . $version, CClientScript::POS_HEAD);
$userId = Yii::app()->user->getId();
?>
<div id="ribbon">
	<div class="ribbon-window-title"></div>
	<div class="ribbon-tab">
		<span class="ribbon-title"><? echo $page->title; ?></span>
		<div class="ribbon-section">
			<span class="section-title"><? echo $page->title; ?></span> <img src="<? echo $page->logo; ?>"/>
		</div>
		<?if ($page->record_activity): ?>
			<div class="ribbon-section">
				<div class="control-group">
					<h4><p class="text-error">To view the links on this site, enter your email address:</p></h4>
					<div class="controls">
						<input type="email" id="user-email" class="input-block-level" placeholder="Email" required <? if (isset(Yii::app()->user->email)): ?>value="<? echo Yii::app()->user->email; ?>" <? endif;?>>
					</div>
				</div>
			</div>
		<? endif;?>
		<? if (!$page->restricted && !isset($userId)): ?>
			<div class="ribbon-section">
				<span class="section-title">User Login</span>
				<div class="ribbon-button ribbon-button-large" id="login-button" rel="tooltip" title="To email links, Log into the site">
					<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/login.png' ?>"/>
					<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/login.png' ?>"/>
					<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/login.png' ?>"/>
				</div>
			</div>
		<? endif; ?>
	</div>
</div>
<div id="content" oncontextmenu="return false;">
	<div>
		<div id="page-id" style="display: none;"><?echo $page->id;?></div>
		<div id="page-title"><?echo $page->subtitle;?></div>
		<div id="page-header"><?echo nl2br($page->header);?></div>
		<div id="page-links-container" class="folder-links-container">
			<? if ($page->show_links_as_url): ?>
			<ul class="nav nav-tabs nav-stacked"><?endif;?>
				<? $links = $page->getLibraryLinks()?>
				<?if (isset($links)): ?>
					<?php foreach ($links as $link): ?>
						<? if ($page->show_links_as_url): ?>
							<? if ($link->name != '' && !$link->isFolder): ?>
								<li>
									<a href="#" id="link<?php echo $link->id; ?>" class="clickable" style="text-decoration: underline;"><?echo $link->name;?></a>
								</li>
							<? endif; ?>
						<? else: ?>
							<? echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/link.php', array('link' => $link, 'disableBanner' => $page->disable_banners, 'disableWidget' => $page->disable_widgets), true); ?>
						<? endif; ?>
					<?php endforeach; ?>
				<? endif;?>
				<? if ($page->show_links_as_url): ?></ul><?endif;?>
		</div>
		<div id="page-footer"><?echo nl2br($page->footer);?></div>
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