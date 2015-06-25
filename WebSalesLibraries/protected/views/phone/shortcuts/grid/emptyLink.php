<?
	/** @var $link BaseShortcut*/
?>
<li class="cbp-item">
	<a class="shortcuts-link empty" href="#">
		<img class="logo" src="<? echo $link->imagePath ?>" alt="" width="100%">
		<div class="service-data">
			<? echo $link->getServiceData(); ?>
		</div>
	</a>
</li>