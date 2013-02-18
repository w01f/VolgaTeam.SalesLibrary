<table id="links-grid-header">
	<tr>
		<td class="link-id-column"><span>Id</span></td>
		<td class="details-button"><span></span></td>
		<td class="library-column"><span><?php echo Yii::app()->params['stations']['column_name']; ?></span></td>
		<td class="link-type-column"><span>Type</span></td>
		<td class="link-name-column"><span>Link</span></td>
		<td class="link-date-column"><span>Date</span></td>
	</tr>
</table>
<div id="links-grid-body-container">
	<table id="links-grid-body">
		<?php if (isset($links)): ?>
		<?php
		if (Yii::app()->browser->isMobile())
			$clickClass = ' click-mobile draggable-link';
		else
			$clickClass = ' click-no-mobile draggable-link';
		$recordNumber = 1;
		?>
		<?php foreach ($links as $link): ?>
			<tr class="<?php echo ($recordNumber % 2) ? 'odd' : 'even'; ?>">
				<td class="link-id-column"><?php echo $link['id']; ?></td>

				<?php
				$detailsButtonClass = 'details-button';
				if (isset($link['hasDetails']) && $link['hasDetails'])
					$detailsButtonClass .= $clickClass . ' collapsed';
				?>
				<td class="<?php echo $detailsButtonClass; ?>"></td>

				<td class="library-column"><?php echo $link['library']; ?></td>

				<td class="link-type-column<?php echo $clickClass; ?>"><?php echo CHtml::tag('img', array('src' => $link['file_type'], 'alt' => '')); ?></td>

				<td class="link-name-column<?php echo $clickClass; ?>">
					<table class="link-container<?php echo $clickClass; ?>">
						<tr>
							<td class="link-name"><?php echo $link['name']; ?></td>
						</tr>
						<tr>
							<td class="file-name"><?php echo $link['file_name']; ?></td>
						</tr>
					</table>
					<img class="delete-link" src="<?php echo Yii::app()->baseUrl . '/images/search/search-delete.png' ?>" alt="Delete Link">
				</td>
				<td class="link-date-column">
					<table class="link-container">
						<tr>
							<td class="link-name"><?php echo date(Yii::app()->params['outputDateFormat'], strtotime($link['date_modify'])); ?></td>
						</tr>
						<tr>
							<td class="file-name"><?php echo date(Yii::app()->params['outputTimeFormat'], strtotime($link['date_modify'])); ?></td>
						</tr>
					</table>
				</td>
			</tr>
			<?php $recordNumber++; ?>
			<?php endforeach; ?>
		<?php endif; ?>
	</table>
</div>