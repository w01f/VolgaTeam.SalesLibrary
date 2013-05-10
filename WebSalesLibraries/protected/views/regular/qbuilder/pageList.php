<ul class="nav nav-list page-ul">
	<? foreach ($pages as $page): ?>
		<li id="page<? echo $page->id; ?>">
			<?if (isset($selectedPage) && $selectedPage->id == $page->id): ?>
				<a class="selected" href="#"><i class="icon-folder-open"></i><? echo $page->title;?></a>
			<? else: ?>
				<a href="#"><i class="icon-folder-close"></i><? echo $page->title;?></a>
			<?endif;?>
		</li>
	<?php endforeach;?>
</ul>
