<?
	/**
	 * @var $idPrefix string
	 */
?>
<div id="<? echo $idPrefix; ?>-logout-dialog-accept" data-role="popup" data-theme="a" data-overlay-theme="d" data-dismissible="false">
	<div data-role="header" data-theme="d">
		<h1>Log Out</h1>
	</div>
	<div role="main" style="padding:0 20px">
		<p>Are you sure you want to log out?</p>
		<div class="ui-grid-a">
			<div class="ui-block-a">
				<a class="logout-button-accept" href="#" data-role="button" data-theme="d">Log Out</a>     
			</div>
			<div class="ui-block-b">
				<a href="#" data-role="button" data-theme="d" data-rel="back">Cancel</a>
			</div>
		</div>
	</div>
</div>