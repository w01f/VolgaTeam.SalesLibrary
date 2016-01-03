<?php
	/** @var $page QPageRecord */

	$this->renderPartial('../site/scripts');

	$cs = Yii::app()->clientScript;
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/qpage/page-content.css?' . Yii::app()->params['version']);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/qpage/qpage-controller.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$authorized = UserIdentity::isUserAuthorized();
?>
<script type="text/javascript">
	window.BaseUrl = '<?php echo Yii::app()->getBaseUrl(true); ?>' + '/qpage/';
</script>
<? $this->renderPartial('../menu/singlePageMenu', array('headerText' => $page->title)); ?>
<div id="content" oncontextmenu="return false;">
	<div id="page-id" style="display: none;"><? echo $page->id; ?></div>
	<? if (isset($page->logo) || $page->record_activity): ?>
		<div class="qpage-header">
			<div>
				<? if (isset($page->logo)): ?>
					<img src="<? echo $page->logo; ?>">
				<? endif; ?>
			</div>
			<div class="email logger-form">
				<? if ($page->record_activity): ?>
					<div class="row">
						<div class="col-xs-8">
							<p class="email-description">
								<strong>To view the links on this site, enter your email address:</strong></p>
						</div>
						<div class="col-xs-4">
							<input type="email" id="user-email" class="form-control" placeholder="Email" required <? if (isset(Yii::app()->user->email)): ?>value="<? echo Yii::app()->user->email; ?>" <? endif; ?>>
						</div>
					</div>
				<? endif; ?>
			</div>
		</div>
	<? endif; ?>
	<div class="padding">
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
									<a href="#" id="link<?php echo $link->id; ?>" class="clickable<? if ($authorized): ?> log-action<?endif;?>" style="text-decoration: underline;"><? echo $link->name; ?></a>
								</li>
							<? endif; ?>
						<? else: ?>
							<? echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/link.php',
								array(
									'link' => $link,
									'disableBanner' => $page->disable_banners,
									'disableWidget' => $page->disable_widgets,
									'authorized' => $authorized
								), true); ?>
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