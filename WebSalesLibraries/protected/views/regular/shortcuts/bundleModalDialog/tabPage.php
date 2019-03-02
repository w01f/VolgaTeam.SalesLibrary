<?

	use application\models\shortcuts\models\bundle_modal_dialog\TabItemContainer;

	/** @var $tabPage TabItemContainer */

	$tabPageRowId = sprintf('%s', $tabPage->id);

	$itemsCount = count($tabPage->items);

	$columnClass = 'col-xs-12';
	switch ($tabPage->columnsCount)
	{
		case 1;
			$columnClass = 'col-xs-12';
			break;
		case 2;
			$columnClass = 'col-xs-6';
			break;
		case 3;
			$columnClass = 'col-xs-4';
			break;
		case 4;
			$columnClass = 'col-xs-3';
			break;
		case 6;
			$columnClass = 'col-xs-2';
			break;
		case 12;
			$columnClass = 'col-xs-1';
			break;
	}
?>
<style>
    <? echo '#'.$tabPageRowId;?> .item-image {
        <?if($tabPage->itemWidth>0):?>
            max-width: <?echo $tabPage->itemWidth;?>px;
        <?elseif($tabPage->itemHeight>0):?>
            max-height: <?echo $tabPage->itemHeight;?>px;
        <?endif;?>
    }
</style>
<? for ($i = 0;
        $i < $itemsCount;
        $i += $tabPage->columnsCount): ?>
<div class="row">
    <? for ($j = 0;
            $j < $tabPage->columnsCount;
            $j++): ?>
        <?
        $itemIndex = $i + $j;
        if ($itemIndex >= $itemsCount)
            break;
        $item = $tabPage->items[$itemIndex];
        ?>
        <div class="text-center col <? echo $columnClass; ?>">
            <? echo $this->renderPartial('bundleModalDialog/' . $item->contentView, array('item' => $item), true); ?>
        </div>
    <? endfor; ?>
</div>
<? endfor; ?>


