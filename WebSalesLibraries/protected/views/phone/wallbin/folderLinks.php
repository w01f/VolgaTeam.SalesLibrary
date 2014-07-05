<? /** @var $folder LibraryFolder */ ?>
<ul data-role="listview" data-theme="c" data-divider-theme="d">
	<li data-role="list-divider">
		<h4><? echo $folder->parent->name . ' - ' . $folder->name; ?></h4>
	</li>
	<? if (isset($folder->files)): ?>
		<? foreach ($folder->files as $link): ?>
			<? if ($link->type != 6 && ((isset($link->name) && $link->name != '') || (isset($link->fileName) && $link->fileName != ''))): ?>
				<? echo $this->renderFile(Yii::getPathOfAlias('application.views.phone.wallbin') . '/link.php', array('link' => $link), true); ?>
			<? endif; ?>
		<? endforeach; ?>
	<? endif; ?>
</ul>