<table class="link-grid">
	<?php $recordNumber = 1; ?>
	<?php foreach ($links as $link): ?>
		<tr class="<?php echo ($recordNumber % 2) ? 'odd' : 'even'; ?>">
			<td class="link-id-column"><?php echo $link['id']; ?></td>
			<td class="library-column"><?php echo $link['library']; ?></td>
			<td class="link-type-column"><?php echo CHtml::tag('img', array('src' => 'data:image/png;base64,' . $link['file_type'], 'alt' => '')); ?></td>
			<td class="link-name-column">
				<table class="link-container">
					<tr>
						<td class="link-name"><?php echo $link['name']; ?></td>
					</tr>
					<tr>
						<td class="file-name"><?php echo $link['file_name']; ?></td>
					</tr>
				</table>
			</td>
			<td class="link-delete">
				<img src="<?php echo Yii::app()->baseUrl . '/images/search/search-delete.png' ?>" alt="Delete Link">
			</td>
		</tr>
		<?php $recordNumber++; ?>
	<?php endforeach; ?>
</table>

