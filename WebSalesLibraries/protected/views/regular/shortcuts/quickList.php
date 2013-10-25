<h2 class="shortcut-title"><? echo $quickListShortcut->title; ?></h2>
<div id="page-links-container" class="folder-links-container">
	<ul class="nav nav-tabs nav-stacked">
		<?php foreach ($quickListShortcut->fileLinks as $link): ?>
			<li>
				<a href="<? echo $link->link; ?>" class="quick-list-file" style="text-decoration: underline;" onfocus="this.blur();this.hideFocus=true;"><? echo $link->name; ?></a>
			</li>
		<?php endforeach; ?>
	</ul>
</div>