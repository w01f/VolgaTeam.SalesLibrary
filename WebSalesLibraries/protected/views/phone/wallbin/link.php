<? /** @var $link LibraryLink */ ?>
<? if (isset($link)): ?>
	<li>
		<a class="<? echo $link->isFolder ? 'folder-content-link' : 'file-link'; ?>" href="#link<? echo $link->id; ?>">
			<h3 class="name"><? if (isset($link->name) && $link->name != '') echo $link->name;
				else if (isset($link->fileName) && $link->fileName != '') echo $link->fileName; ?></h3>
			<? if (isset($link->name) && $link->name != '' && !$link->isFolder): ?>
				<p class="file"><? echo $link->fileName; ?></p>
			<? endif; ?>
		</a>
	</li>
<? endif; ?>