<ul data-role="listview" data-theme="c" data-divider-theme="d">
	<li data-role="list-divider">
		<h4><?php echo $quickListShortcut->title; ?></h4>
	</li>
	<? foreach ($quickListShortcut->fileLinks as $link): ?>
		<li><a href="<? echo $link->link; ?>" target="_blank"><? echo $link->name; ?></a></li>
	<? endforeach; ?>
</ul>