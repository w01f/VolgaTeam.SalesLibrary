<ul data-role="listview" data-theme="c" data-divider-theme="d">
	<?php if (isset($parentFolder)): ?>
	<li data-role="list-divider" >
		<h4><?php echo $parentFolder->name; ?></h4>
	</li>
	<?php endif; ?>
	<?php if (isset($folders)): ?>
	<?php foreach ($folders as $folder): ?>
		<li>
			<a class="favorite-folder-link" href="#folder<?php echo $folder->id; ?>"><?php echo $folder->name;?></a>
			<a class="favorite-folder-link-delete" href="#folder<?php echo $folder->id; ?>" data-icon="delete" data-shadow="false" data-theme="c"></a>
		</li>
		<?php endforeach; ?>
	<?php endif; ?>
	<?php if (isset($links)): ?>
	<?php foreach ($links as $link): ?>
		<li>
			<a class="favorite-file-link" href="#link<?php echo $link['id']; ?>">
				<table class="link-container">
					<tr>
						<?php if ($link['hasDetails']): ?>
						<td rowspan="3" class="link-details-container">
							<a class="favorite-file-link-detail" href="#link<?php echo $link['id']; ?>"> <img src="<?php echo Yii::app()->baseUrl . '/images/search/expand.png'; ?>"/> </a>
						</td>
						<?php endif; ?>
						<td>
                            <span class="name">
                                <?php
								if (isset($link['name']) && $link['name'] != '')
									echo $link['name'];
								else if (isset($link['file_name']) && $link['file_name'] != '')
									echo $link['file_name'];
								?>
                            </span>
						</td>
					</tr>
					<?php if (isset($link['name']) && $link['name'] != ''): ?>
					<tr>
						<td>
							<span class="file"><?php echo $link['file_name']; ?></span>
						</td>
					</tr>
					<?php endif; ?>
					<tr>
						<td>
							<span class="file"><?php echo $link['library'] . ' - ' . date(Yii::app()->params['outputDateFormat'], strtotime($link['date_modify'])); ?></span>
						</td>
					</tr>
				</table>
			</a>
			<a class="favorite-file-link-delete" href="#link<?php echo $link['id']; ?>" data-icon="delete" data-shadow="false" data-theme="c"></a>
		</li>
		<?php endforeach; ?>
	<?php endif; ?>
</ul>