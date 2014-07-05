<?
	/** @var $quickListShortcut QuickListShortcut */
?>
<h2 class="shortcut-title"><? echo $quickListShortcut->title; ?></h2>
<div id="page-links-container" class="folder-links-container">
	<ul class="nav nav-pills nav-stacked">
		<? foreach ($quickListShortcut->fileLinks as $link): ?>
			<li>
				<a href="<? echo $link->link; ?>" class="quick-list-file" style="text-decoration: underline;" onfocus="this.blur();this.hideFocus=true;"><? echo $link->name; ?></a>
			</li>
		<? endforeach; ?>
	</ul>
</div>