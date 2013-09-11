<table class="link-grid">
	<?php
		if (Yii::app()->browser->isMobile())
			$clickClass = ' click-mobile draggable-link';
		else
			$clickClass = ' click-no-mobile draggable-link';
		$recordNumber = 1;
	?>
	<?php foreach ($links as $link): ?>
		<tr class="page-link <?php echo ($recordNumber % 2) ? 'odd' : 'even'; ?>">
			<td class="link-id-column"><?php echo $link['id']; ?></td>
			<td class="library-column"><?php echo $link['library']; ?></td>
			<td class="link-type-column<?php echo $clickClass; ?>"><?php echo CHtml::tag('img', array('src' => 'data:image/png;base64,' . $link['file_type'], 'alt' => '')); ?></td>
			<td class="link-name-column<?php echo $clickClass; ?>">
				<table class="link-container<?php echo $clickClass; ?>">
					<tr>
						<td class="link-name"><?php echo $link['name']; ?></td>
					</tr>
					<tr>
						<td class="file-name"><?php echo $link['file_name']; ?></td>
					</tr>
				</table>
			</td>
			<td class="link-up">
				<img src="<?php echo Yii::app()->baseUrl . '/images/search/search-link-up.png' ?>" alt="Up Link">
			</td>
			<td class="link-down">
				<img src="<?php echo Yii::app()->baseUrl . '/images/search/search-link-down.png' ?>" alt="Down Link">
			</td>
			<td class="link-delete">
				<img src="<?php echo Yii::app()->baseUrl . '/images/search/search-delete.png?1' ?>" alt="Delete Link">
			</td>
		</tr>
		<?php $recordNumber++; ?>
	<?php endforeach; ?>
</table>

