<?
	/**
	 * @var $links array
	 */
?>
<table class="link-grid">
	<?
		if (Yii::app()->browser->isMobile())
			$clickClass = ' click-mobile draggable-link';
		else
			$clickClass = ' click-no-mobile draggable-link';
		$recordNumber = 1;
	?>
	<? foreach ($links as $link): ?>
		<tr class="page-link <? echo ($recordNumber % 2) ? 'odd' : 'even'; ?>">
			<td class="link-id-column"><? echo $link['id']; ?></td>
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
			</td>
			<td class="link-up">
				<img src="<? echo Yii::app()->baseUrl . '/images/search/search-link-up.png' ?>" alt="Up Link">
			</td>
			<td class="link-down">
				<img src="<? echo Yii::app()->baseUrl . '/images/search/search-link-down.png' ?>" alt="Down Link">
			</td>
			<td class="link-delete">
				<img src="<? echo Yii::app()->baseUrl . '/images/search/search-delete.png?1' ?>" alt="Delete Link">
			</td>
		</tr>
		<? $recordNumber++; ?>
	<? endforeach; ?>
</table>

