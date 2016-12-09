<?
	/**
	 * @var $pages QPageRecord[]
	 * @var $selectedPage QPageRecord
	 */
?>
<table class="grid" cellspacing="0" cellpadding="0">
	<? $recordNumber = 1; ?>
	<? foreach ($pages as $page): ?>
		<tr class="page-list-item <? echo ($recordNumber % 2) ? 'odd' : 'even'; ?> <? if (isset($selectedPage) && $selectedPage->id == $page->id): ?>selected<? endif; ?>">
			<td class="page-id-column"><? echo $page->id; ?></td>
			<td class="page-name-column log-action"><? echo $page->title; ?></td>
			<td class="page-up log-action">
				<img src="<? echo Yii::app()->baseUrl . '/images/qpages/change-order-up.png' ?>" alt="Up Page">
			</td>
			<td class="page-down log-action">
				<img src="<? echo Yii::app()->baseUrl . '/images/qpages/change-order-down.png' ?>" alt="Down Page">
			</td>
			<td class="page-clone log-action">
				<img src="<? echo Yii::app()->baseUrl . '/images/qpages/clone.png' ?>" alt="Clone Page">
			</td>
			<td class="page-delete log-action">
				<img src="<? echo Yii::app()->baseUrl . '/images/grid/item-delete.png?1' ?>" alt="Delete Page">
			</td>
		</tr>
		<? $recordNumber++; ?>
	<? endforeach; ?>
</table>
