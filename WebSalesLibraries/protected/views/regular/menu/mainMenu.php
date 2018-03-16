<?
	/**
	 * @var $menuGroups ShortcutGroup[]
	 * @var $iconClass string
	 * @var $headerText string
	 * @var $showMainSiteUrl bool
	 * @var $mainSiteUrl string
	 * @var $mainSiteName string
	 * @var $mainSiteTarget string
	 */

	if (!isset($mainSiteUrl))
		$mainSiteUrl = Yii::app()->getBaseUrl(true);
	if (!isset($mainSiteName))
		$mainSiteName = str_replace('http://', '', str_replace('https://', '', $mainSiteUrl));
	if (isset($headerText))
		$headerText = trim(strip_tags($headerText));
?>
<div id="main-menu">
	<nav id="om-nav" class="om-nav img-darkbrick"
	     data-last-update="<? echo date(Yii::app()->params['sourceDateFormat']); ?>">
		<? $this->renderPartial('../menu/menuPopupContent', array('menuGroups' => $menuGroups)); ?>
	</nav>
	<div data-navid="om-nav" class="onemenu om-ctrlbar menu-back-colored">
		<div class="om-controlitems">
			<div class="om-controlitem menu-icon-holder menu-header row">
				<div class="col col-xs-8">
					<i class="white-icon icon-menu9 open-main-menu-button"></i>
					<i class="header-icon <? echo isset($iconClass) ? $iconClass : ''; ?>"></i>
					<span class="header-text"><? echo !empty($headerText) ? $headerText : ''; ?></span>
				</div>
				<? if (isset($showMainSiteUrl) && $showMainSiteUrl): ?>
					<div class="col col-xs-4 text-right main-site-url">
						<a href="<? echo $mainSiteUrl; ?>"
						   target="<? echo isset($mainSiteTarget) ? $mainSiteTarget : '_blank'; ?>"><? echo $mainSiteName; ?></a>
					</div>
				<? endif; ?>
                <div class="om-movenext om-controlitem" style="display: none;">
                    <img src="/images/menu/right-arrow.png" alt="">
                </div>
			</div>
		</div>
	</div>
</div>
<? $this->renderPartial('../menu/actionMenu'); ?>
