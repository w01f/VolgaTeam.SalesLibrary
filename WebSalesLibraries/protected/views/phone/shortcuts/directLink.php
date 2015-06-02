<?
	/** @var $link BaseShortcut*/
?>
<a class="ui-block-a shortcuts-link direct<? echo isset($link->samePage) && $link->samePage ? ' embedded' : ''; ?> <? echo $link->type; ?>" href="<? echo $link->sourceLink ?>" target="_blank">
	<img src="<? echo $link->imagePath; ?>">
	<div class="service-data">
		<? echo $link->getServiceData(); ?>
	</div>
</a>