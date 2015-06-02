<?
	/** @var $link BaseShortcut*/
?>
<div class="ui-block-a shortcuts-link library-page">
	<img src="<? echo $link->imagePath; ?>">
	<div class="service-data">
		<? echo $link->getServiceData(); ?>
	</div>
</div>