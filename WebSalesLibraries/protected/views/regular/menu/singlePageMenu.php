<?
	/**
	 * @var $iconClass string
	 * @var $headerText string
	 * */
?>

<div id="main-menu">
	<div data-navid="om-nav" class="onemenu om-ctrlbar solid-blue-2">
		<div class="om-controlitems">
			<div class="om-controlitem menu-icon-holder menu-header">
				<i class="header-icon <? echo isset($iconClass) ? $iconClass : ''; ?>"></i>
				<span class="header-text"><? echo isset($headerText) ? $headerText : ''; ?></span>
			</div>
		</div>
	</div>
</div>
<? $this->renderPartial('../menu/actionMenu'); ?>
