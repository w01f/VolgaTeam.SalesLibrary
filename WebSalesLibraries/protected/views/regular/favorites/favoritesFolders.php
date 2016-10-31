<?
	/**
	 * @var $folders FavoritesFolder
	 * @var $selectedFolderId string
	 */
?>
<ul class="favorites-folders-list nav nav-list">
	<? foreach ($folders as $folder): ?>
	<li>
		<?if (isset($selectedFolderId) && $selectedFolderId == $folder->id): ?>
			<a class="draggable-folder droppable opened" href="#"><span class="glyphicon glyphicon-folder-open"></span><? echo $folder->name;?><span class="delete-folder glyphicon glyphicon-remove"></span></a>
		<? else: ?>
			<a class="draggable-folder droppable" href="#"><span class="glyphicon glyphicon-folder-close"></span><? echo $folder->name;?><span class="delete-folder glyphicon glyphicon-remove"></span></a>
		<?endif;?>
		<div class="service-data">
			<? if (isset($folder->id)): ?>
			<div class="folder-id"><? echo $folder->id;?></div>
			<div class="folder-name"><? echo $folder->name;?></div>
			<?php endif;?>
		</div>
		<?
			if(isset($folder->childFolders))
				echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.favorites') . '/favoritesFolders.php',
					array(
						'folders' => $folder->childFolders,
						'selectedFolderId' => isset($selectedFolderId) ? $selectedFolderId : null),
					true);
		?>
	</li>
	<?php endforeach;?>
</ul>

