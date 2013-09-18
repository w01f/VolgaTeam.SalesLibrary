<ul data-role="listview" data-theme="c" data-divider-theme="d">
	<?php if (isset($parentFolder)): ?>
		<li data-role="list-divider">
			<h4><?php echo $parentFolder->name; ?></h4>
		</li>
	<?php endif; ?>
	<?php if (isset($folders)): ?>
		<?php foreach ($folders as $folder): ?>
			<li>
				<a class="favorite-folder-link" href="#folder<?php echo $folder->id; ?>"><?php echo $folder->name; ?></a>
				<a class="favorite-folder-link-delete" href="#folder<?php echo $folder->id; ?>" data-icon="delete" data-shadow="false" data-theme="c"></a>
			</li>
		<?php endforeach; ?>
	<?php endif; ?>
	<?php if (isset($links)): ?>
		<?php foreach ($links as $link): ?>
			<li>
				<a class="favorite-file-link" href="#link<?php echo $link['id']; ?>">
					<?php if ($link['hasDetails']): ?>
						<img class="ui-li-has-thumb favorite-link-detail" src="<? echo Yii::app()->baseUrl . '/images/search/expand-phone.png'; ?>"/>
					<? endif; ?>
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
				<a class="favorite-file-link-delete" href="#link<?php echo $link['id']; ?>" data-icon="delete" data-shadow="false" data-theme="c"></a>
			</li>
		<?php endforeach; ?>
	<?php endif; ?>
</ul>