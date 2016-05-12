<?
	/**
	 * @var $link LibraryLink
	 * @var $authorized boolean
	 */

	if ($link->isDirectUrl)
		$linkContainerClass = 'url';
	else
		$linkContainerClass = 'clickable';
?>
<li>
	<a href="#" target="_blank" id="link<?php echo $link->id; ?>"
	   class="<? echo $linkContainerClass; ?><? if ($authorized): ?> log-action<? endif; ?>"
	   style="text-decoration: underline;">
		<? echo $link->name; ?>
		<span class="service-data"><? echo $link->getLinkData(); ?></span>
	</a>
</li>
