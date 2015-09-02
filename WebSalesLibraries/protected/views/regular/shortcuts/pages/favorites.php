<?
	/**
	 * @var $rootFolder FavoritesFolder
	 * */
?>
<table id="favorites-container">
	<tr>
		<td id="favorites-panel-folders">
			<div>
				<ul class="favorites-folders-list nav nav-list">
					<li>
						<? if (isset($selectedFolderId)): ?>
							<a class="droppable" href="#">
								<span class="glyphicon glyphicon-folder-close"></span><? echo $rootFolder->name; ?>
							</a>
						<? else: ?>
							<a class="droppable opened" href="#">
								<span class="glyphicon glyphicon-folder-open"></span><? echo $rootFolder->name; ?>
							</a>
						<?endif; ?>
						<div class="service-data"></div>
						<? if (isset($rootFolder->childFolders)) echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.favorites') . '/favoritesFolders.php', array('folders' => $rootFolder->childFolders, 'selectedFolderId' => isset($selectedFolderId) ? $selectedFolderId : null), true); ?>
					</li>
				</ul>
			</div>
		</td>
		<td id="favorites-panel-links" class="data-table-content-container">
		</td>
	</tr>
</table>