<?
	/** @var $link BaseShortcut */
?>
<li class="cbp-item">
	<a class="shortcuts-link direct<? echo isset($link->samePage) && $link->samePage ? ' embedded' : ''; ?> <? echo $link->type; ?>" href="<? echo $link->sourceLink ?>" data-ajax="false" target="_blank">
		<img class="logo" src="<? echo $link->imagePath ?>" alt="" width="100%">
		<div class="service-data">
			<? echo $link->getServiceData(); ?>
		</div>
	</a>
</li>