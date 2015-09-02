<?
	/** @var $shortcut QuickListShortcut */
?>
<ul data-role="listview" data-theme="a">
	<? foreach ($shortcut->fileLinks as $link): ?>
		<li><a href="<? echo $link->link; ?>" target="_blank"><? echo $link->name; ?></a></li>
	<? endforeach; ?>
</ul>