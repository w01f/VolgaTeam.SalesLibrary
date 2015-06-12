<?
	/** @var $quickListShortcut QuickListShortcut */
?>
<ul data-role="listview" data-theme="a">
	<? foreach ($quickListShortcut->fileLinks as $link): ?>
		<li><a href="<? echo $link->link; ?>" target="_blank"><? echo $link->name; ?></a></li>
	<? endforeach; ?>
</ul>