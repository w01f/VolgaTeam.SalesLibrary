<? if (isset($datasetKey)): ?>
	<div class="dataset-key"><? echo $datasetKey; ?></div>
<? endif; ?>
<table class="links-grid-header">
	<tr>
		<td class="link-id-column"><span>Id</span></td>
		<td class="link-tag-column"><span><? echo Yii::app()->params['tags']['column_name']; ?></span></td>
		<td class="library-column"><span><? echo Yii::app()->params['stations']['column_name']; ?></span></td>
		<td class="link-type-column"><span>Type</span></td>
		<td class="link-name-column"><span>Link</span></td>
		<td class="link-rate-column"><span>Rating</span></td>
		<td class="link-date-column"><span>Date</span></td>
	</tr>
</table>
<div class="links-grid-body-container">
	<table class="links-grid-body">
		<? if (isset($links)): ?>
			<?
			if (Yii::app()->browser->isMobile())
				$clickClass = ' click-mobile draggable-link';
			else
				$clickClass = ' click-no-mobile draggable-link';
			$recordNumber = 1;
			?>
			<? foreach ($links as $link): ?>
				<tr class="<? echo ($recordNumber % 2) ? 'odd' : 'even'; ?>">
					<td class="link-id-column"><? echo $link['id']; ?></td>

					<td class="link-tag-column"><? echo $link['tag']; ?></td>

					<td class="library-column"><? echo $link['library']; ?></td>

					<td class="link-type-column<? echo $clickClass; ?>"><? echo CHtml::tag('img', array('src' => 'data:image/png;base64,' . $link['file_type'], 'alt' => '')); ?></td>

					<td class="link-name-column<? echo $clickClass; ?>">
						<table class="link-container<? echo $clickClass; ?>">
							<tr>
								<td class="link-name"><? echo $link['name']; ?></td>
							</tr>
							<tr>
								<td class="file-name"><? echo $link['file_name']; ?></td>
							</tr>
						</table>
						<img class="delete-link" src="<? echo Yii::app()->baseUrl . '/images/search/search-delete.png' ?>" alt="Delete Link">
					</td>
					<td class="link-rate-column"><? echo $link['rate_image'] != '' ? CHtml::image($link['rate_image']) : ''; ?></td>
					<td class="link-date-column">
						<table class="link-container">
							<tr>
								<td class="link-name"><? echo date(Yii::app()->params['outputDateFormat'], strtotime($link['date_modify'])); ?></td>
							</tr>
							<tr>
								<td class="file-name"><? echo date(Yii::app()->params['outputTimeFormat'], strtotime($link['date_modify'])); ?></td>
							</tr>
						</table>
					</td>
				</tr>
				<? $recordNumber++; ?>
			<? endforeach; ?>
		<? endif; ?>
	</table>
</div>