<?
	/**
	 * @var $iconClass string
	 * @var $headerText string
	 * @var $showMainSiteUrl bool
	 * */
	$mainSiteUrl = Yii::app()->getBaseUrl(true);
	$headerText = trim(strip_tags($headerText));
?>

<div id="main-menu">
	<div data-navid="om-nav" class="onemenu om-ctrlbar menu-back-colored">
		<div class="om-controlitems">
			<div class="om-controlitem menu-icon-holder menu-header row">
				<div class="col col-xs-8">
					<i class="header-icon <? echo isset($iconClass) ? $iconClass : ''; ?>"></i>
					<span class="header-text"><? echo !empty($headerText) ? $headerText : ''; ?></span>
				</div>
				<? if (isset($showMainSiteUrl) && $showMainSiteUrl): ?>
					<div class="col col-xs-4 text-right main-site-url">
						<a href="<? echo $mainSiteUrl; ?>"
						   target="_blank"><? echo str_replace('http://', '', str_replace('https://', '', $mainSiteUrl)); ?></a>
					</div>
				<? endif; ?>
			</div>
		</div>
	</div>
</div>
<? $this->renderPartial('../menu/actionMenu'); ?>
