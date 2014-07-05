<?
	/**
	 * @var $pages QPageRecord[]
	 * @var $selectedPage QPageRecord
	 */
?>
<table class="link-grid" cellspacing="0" cellpadding="0">
	<? $recordNumber = 1; ?>
	<? foreach ($pages as $page): ?>
		<tr class="page-list-item <? echo ($recordNumber % 2) ? 'odd' : 'even'; ?> <? if (isset($selectedPage) && $selectedPage->id == $page->id): ?>selected<? endif; ?>">
			<td class="link-id-column"><? echo $page->id; ?></td>
			<td class="link-name-column"><? echo $page->title; ?></td>
			<td class="link-up">
				<img src="<? echo Yii::app()->baseUrl . '/images/search/search-link-up.png' ?>" alt="Up Page">
			</td>
			<td class="link-down">
				<img src="<? echo Yii::app()->baseUrl . '/images/search/search-link-down.png' ?>" alt="Down Page">
			</td>
			<td class="link-clone">
				<img src="<? echo Yii::app()->baseUrl . '/images/search/search-link-clone.png' ?>" alt="Clone Page">
			</td>
			<td class="link-delete">
				<img src="<? echo Yii::app()->baseUrl . '/images/search/search-delete.png?1' ?>" alt="Delete Page">
			</td>
		</tr>
		<? $recordNumber++; ?>
	<? endforeach; ?>
</table>
