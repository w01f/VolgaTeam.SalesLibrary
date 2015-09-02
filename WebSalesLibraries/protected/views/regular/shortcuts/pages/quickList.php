<?
	/** @var $shortcut QuickListShortcut */
?>
<div id="page-links-container" class="folder-links-container">
	<ul class="nav nav-pills nav-stacked">
		<? foreach ($shortcut->fileLinks as $link): ?>
			<li>
				<a href="<? echo $link->link; ?>" class="quick-list-file" style="text-decoration: underline;" onfocus="this.blur();this.hideFocus=true;"><? echo $link->name; ?></a>
			</li>
		<? endforeach; ?>
	</ul>
</div>