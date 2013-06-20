<?php
$version = '6.0';
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
if ($page->show_ticker && isset($tickerRecords))
{
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/modern-ticker/css/modern-ticker.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/modern-ticker/themes/theme' . Yii::app()->params['ticker']['theme'] . '/theme.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/ticker.css?' . $version);
}
$cs->registerCoreScript('jquery.ui');
$cs->registerCoreScript('cookie');
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/json/jquery.json-2.3.min.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/lib/jquery.mousewheel-3.0.6.pack.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/video-js/video.min.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/bootstrap/js/bootstrap.js?' . $version, CClientScript::POS_HEAD);
if ($page->show_ticker && isset($tickerRecords))
{
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/modern-ticker/js/jquery.modern-ticker.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/ticker.js?' . $version, CClientScript::POS_HEAD);
}
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/overlay.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/link-viewing.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/view-dialog-bar.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/favorites.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/link-rate.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/qpage/scaling.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/qpage/page-links.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/qpage/ribbon.js?' . $version, CClientScript::POS_HEAD);
?>
<div id="ribbon">
	<div class="ribbon-window-title"></div>
	<div class="ribbon-tab">
		<span class="ribbon-title"><? echo $page->title; ?></span>
		<div class="ribbon-section">
			<span class="section-title"><? echo $page->title; ?></span> <img src="<? echo $page->logo; ?>"/>
		</div>
		<?if ($page->show_site_link): ?>
			<div class="ribbon-section">
				<span class="section-title">Go to Main Site</span>
				<a href="<? echo Yii::app()->createAbsoluteUrl(''); ?>" target="_blank"><img src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/site-link.png' ?>"/></a>
			</div>
		<? endif;?>
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
	</div>
</div>
<div id="content">
	<div>
		<div id="page-id" style="display: none;"><?echo $page->id;?></div>
		<div id="page-title"><?echo $page->subtitle;?></div>
		<div id="page-header"><?echo nl2br($page->header);?></div>
		<div id="page-links-container" class="folder-links-container">
			<? $links = $page->getLibraryLinks()?>
			<?if (isset($links)): ?>
				<?php foreach ($links as $link): ?>
					<? echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/link.php', array('link' => $link, 'disableBanner' => $page->disable_banners, 'disableWidget' => $page->disable_widgets), true); ?>
				<?php endforeach; ?>
			<? endif;?>
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
<!---------Ticker--------->
<?php if (Yii::app()->params['ticker']['visible'] && isset($tickerRecords)): ?>
	<div class="modern-ticker mt-round <?php echo Yii::app()->params['ticker']['effect']; ?>">
		<?php if ((Yii::app()->params['ticker']['show_label'] || Yii::app()->params['ticker']['show_logo']) && isset($tickerRecords)): ?>
			<div class="mt-label">
				<?php if (Yii::app()->params['ticker']['show_logo']): ?>
					<img src="<?php echo Yii::app()->baseUrl . '/images/tickerlogo.png?' . $version; ?>">
				<?php endif; ?>
				<?php if (Yii::app()->params['ticker']['show_label']): ?>
					<span>NEWS:</span>
				<?php endif; ?>
			</div>
		<?php endif; ?>
		<div class="mt-news">
			<ul>
				<?foreach ($tickerRecords as $tickerRecord): ?>
					<?php echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.ticker') . '/tickerLink.php', array('tickerLink' => $tickerRecord), true); ?>
				<? endforeach;?>
			</ul>
		</div>
		<?php if (Yii::app()->params['ticker']['show_control']): ?>
			<div class="mt-controls">
				<div class="mt-prev"></div>
				<div class="mt-play"></div>
				<div class="mt-next"></div>
			</div>
		<?php endif; ?>
	</div>
<?php endif; ?>
<!------------------------->