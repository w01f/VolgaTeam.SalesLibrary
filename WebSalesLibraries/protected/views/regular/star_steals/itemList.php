<?
	/**
	 * @var $items \application\models\star_steals\models\ItemListModel[]
	 * @var $selectedItemId string
	 */
?>
<table class="grid" cellspacing="0" cellpadding="0">
	<? $itemNumber = 1; ?>
	<? foreach ($items as $item): ?>
		<tr class="item-list-item <? echo ($itemNumber % 2) ? 'odd' : 'even'; ?> <? if ($selectedItemId == $item->id): ?>selected<? endif; ?>">
			<td class="item-id-column"><? echo $item->id; ?></td>
			<td class="item-name-column log-action" style="padding-left: 10px !important;"><? echo $item->title; ?></td>
			<td class="item-up log-action">
				<img src="<? echo Yii::app()->baseUrl . '/images/qpages/change-order-up.png' ?>" alt="Up Page">
			</td>
			<td class="item-down log-action">
				<img src="<? echo Yii::app()->baseUrl . '/images/qpages/change-order-down.png' ?>" alt="Down Page">
			</td>
			<td class="item-clone log-action">
				<img src="<? echo Yii::app()->baseUrl . '/images/qpages/clone.png' ?>" alt="Clone Page">
			</td>
			<td class="item-delete log-action">
				<img src="<? echo Yii::app()->baseUrl . '/images/grid/item-delete.png?1' ?>" alt="Delete Page">
			</td>
		</tr>
		<? $itemNumber++; ?>
	<? endforeach; ?>
</table>
