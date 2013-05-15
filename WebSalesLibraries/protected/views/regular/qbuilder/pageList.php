<ul class="nav nav-list page-ul">
	<? foreach ($pages as $page): ?>
		<li id="page<? echo $page->id; ?>">
			<?if (isset($selectedPage) && $selectedPage->id == $page->id): ?>
				<a class="selected" href="#"><i class="icon-folder-open"></i><span><? echo $page->title;?></span></a>
			<? else: ?>
				<a href="#"><i class="icon-folder-close"></i><span><? echo $page->title;?></span></a>
			<?endif;?>
		</li>
	<?php endforeach;?>
</ul>
