<ul class="favorites-folders-list nav nav-list">
	<? foreach ($folders as $folder): ?>
	<li>
		<a href="#"><i class="icon-folder-close"></i><? echo $folder->name;?></a>
		<div class="service-data">
			<? if (isset($folder->id)): ?>
			<div class="folder-id"><? echo $folder->id;?></div>
			<?php endif;?>
		</div>
		<?if (isset($folder->childFolders)) echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.favorites') . '/favoritesFolders.php', array('folders' => $folder->childFolders), true);?>
	</li>
	<?php endforeach;?>
</ul>

