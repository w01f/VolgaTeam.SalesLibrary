<?

	use application\models\shortcuts\models\bundle_modal_dialog\LeftPanelContainer;

	/** @var $leftPanel LeftPanelContainer */
?>
<style>
    #shortcuts-bundle-modal .left-panel .item-image {
        <?if($leftPanel->itemWidth>0):?>
            max-width: <?echo $leftPanel->itemWidth;?>px;
        <?elseif($leftPanel->itemHeight>0):?>
            max-height: <?echo $leftPanel->itemHeight;?>px;
        <?endif;?>
    }
</style>
<? foreach ($leftPanel->items as $item): ?>
    <div class="row">
        <div class="text-center col col-xs-12">
			<? echo $this->renderPartial('bundleModalDialog/' . $item->contentView, array('item' => $item), true); ?>
        </div>
    </div>
<? endforeach; ?>


