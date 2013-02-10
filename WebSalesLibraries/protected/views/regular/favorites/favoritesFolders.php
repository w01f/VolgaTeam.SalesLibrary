<ul class="favorites-folders-list nav nav-list">
	<? foreach ($folders as $folder): ?>
	<li>
		<?if (isset($selectedFolderId) && $selectedFolderId == $folder->id): ?>
			<a class="draggable-folder droppable opened" href="#"><i class="icon-folder-open"></i><? echo $folder->name;?><i class="delete-folder icon-remove"></i></a>
		<? else: ?>
			<a class="draggable-folder droppable" href="#"><i class="icon-folder-close"></i><? echo $folder->name;?><i class="delete-folder icon-remove"></i></a>
		<?endif;?>
		<div class="service-data">
			<? if (isset($folder->id)): ?>
			<div class="folder-id"><? echo $folder->id;?></div>
			<?php endif;?>
		</div>
		<?if (isset($folder->childFolders)) echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.favorites') . '/favoritesFolders.php', array('folders' => $folder->childFolders, 'selectedFolderId' => isset($selectedFolderId) ? $selectedFolderId : null), true);?>
	</li>
	<?php endforeach;?>
</ul>

