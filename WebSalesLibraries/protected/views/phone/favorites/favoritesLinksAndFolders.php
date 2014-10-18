<?
	/**
	 * @var $parentFolder FavoritesFolder
	 * @var $folders FavoritesFolder[]
	 * @var $links array
	 */
?>
<ul data-role="listview" data-theme="c" data-divider-theme="d">
	<? if (isset($parentFolder)): ?>
		<li data-role="list-divider">
			<h4><? echo $parentFolder->name; ?></h4>
		</li>
	<? endif; ?>
	<? if (isset($folders)): ?>
		<? foreach ($folders as $folder): ?>
			<li>
				<a class="favorite-folder-link" href="#folder<? echo $folder->id; ?>"><? echo $folder->name; ?></a>
				<a class="favorite-folder-link-delete" href="#folder<? echo $folder->id; ?>" data-icon="delete" data-shadow="false" data-theme="c"></a>
			</li>
		<? endforeach; ?>
	<? endif; ?>
	<? if (isset($links)): ?>
		<? foreach ($links as $link): ?>
			<li>
				<a class="favorite-file-link" href="#link<? echo $link['id']; ?>">
					<h3 class="name"><? if (isset($link['name']) && $link['name'] != '') echo $link['name'];
						else if (isset($link['file_name']) && $link['file_name'] != '') echo $link['file_name']; ?></h3>
					<p class="file">
						<?
							if (isset($link['name']) && $link['name'] != '')
								echo $link['file_name'];
							echo '<br>';
							echo $link['library'] . ' - ' . date(Yii::app()->params['outputDateFormat'], strtotime($link['date_modify']));
						?>
					</p>
				</a>
				<a class="favorite-file-link-delete" href="#link<? echo $link['id']; ?>" data-icon="delete" data-shadow="false" data-theme="c"></a>
			</li>
		<? endforeach; ?>
	<? endif; ?>
</ul>