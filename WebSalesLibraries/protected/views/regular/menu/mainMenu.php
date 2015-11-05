<?
	/**
	 * @var $menuGroups ShortcutGroup[]
	 */
?>
<div id="main-menu">
	<nav id="om-nav" class="om-nav img-darkbrick" data-last-update="<? echo date(Yii::app()->params['sourceDateFormat']);?>">
		<? $this->renderPartial('../menu/menuPopupContent', array('menuGroups' => $menuGroups)); ?>
	</nav>
	<div data-navid="om-nav" class="onemenu om-ctrlbar solid-blue-2">
		<div class="om-controlitems">
			<div class="om-controlitem menu-icon-holder menu-header">
				<i class="white-icon icon-menu9"></i>
				<i class="header-icon"></i>
				<span class="header-text"></span>
			</div>
		</div>
	</div>
</div>
<? $this->renderPartial('../menu/actionMenu'); ?>
