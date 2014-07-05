<? /** @var $page LibraryPage */ ?>
<ul data-role="listview" data-theme="c" data-divider-theme="d">
	<li data-role="list-divider">
		<h4><? echo $page->name; ?></h4>
	</li>
	<? if (isset($page->folders)): ?>
		<? foreach ($page->folders as $folder): ?>
			<li>
				<a class="folder-link" href="#folder<? echo $folder->id; ?>"><? echo $folder->name; ?></a>
			</li>
		<? endforeach; ?>
	<? endif; ?>
</ul>