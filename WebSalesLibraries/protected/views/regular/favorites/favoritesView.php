<table id="favorites-container">
	<tr>
		<td id="favorites-panel-folders">
			<div>
				<ul class="favorites-folders-list nav nav-list">
					<li>
						<a href="#" class="opened"><i class="icon-folder-open"></i><? echo $rootFolder->name;?></a>
						<div class="service-data"></div>
						<? if (isset($rootFolder->childFolders)) echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.favorites') . '/favoritesFolders.php', array('folders' => $rootFolder->childFolders), true);?>
					</li>
				</ul>
			</div>
		</td>
		<td id="favorites-panel-links">
			<div></div>
		</td>
	</tr>
</table>