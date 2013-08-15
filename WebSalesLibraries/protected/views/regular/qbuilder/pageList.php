<table class="link-grid" cellspacing="0" cellpadding="0">
	<?php $recordNumber = 1; ?>
	<? foreach ($pages as $page): ?>
		<tr class="page-list-item <?php echo ($recordNumber % 2) ? 'odd' : 'even'; ?> <? if (isset($selectedPage) && $selectedPage->id == $page->id): ?>selected<? endif; ?>">
			<td class="link-id-column"><?php echo $page->id; ?></td>
			<td class="link-name-column"><? echo $page->title;?></td>
			<td class="link-up">
				<img src="<?php echo Yii::app()->baseUrl . '/images/search/search-link-up.png' ?>" alt="Up Page">
			</td>
			<td class="link-down">
				<img src="<?php echo Yii::app()->baseUrl . '/images/search/search-link-down.png' ?>" alt="Down Page">
			</td>
			<td class="link-clone">
				<img src="<?php echo Yii::app()->baseUrl . '/images/search/search-link-clone.png' ?>" alt="Clone Page">
			</td>
			<td class="link-delete">
				<img src="<?php echo Yii::app()->baseUrl . '/images/search/search-delete.png?1' ?>" alt="Delete Page">
			</td>
		</tr>
		<?php $recordNumber++; ?>
	<?php endforeach;?>
</table>
