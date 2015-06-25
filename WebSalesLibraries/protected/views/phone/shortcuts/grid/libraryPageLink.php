<?
	/** @var $link PageShortcut*/
?>
<li class="cbp-item">
	<a class="shortcuts-link library-page" href="#" data-ajax="false">
		<img class="logo" src="<? echo $link->imagePath ?>" alt="" width="100%">
		<div class="service-data">
			<? echo $link->getServiceData(); ?>
		</div>
	</a>
</li>