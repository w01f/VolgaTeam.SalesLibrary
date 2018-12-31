<?

	use application\models\shortcuts\models\landing_page\regular_markup\button_group\ButtonItem;

	/** @var $buttonItem ButtonItem */

	$blockId = sprintf('button-item-%s', $buttonItem->id);
	echo $this->renderPartial('landingPageMarkup/button_group/buttonStyle', array('buttonItem' => $buttonItem, 'blockId' => $blockId), true);
?>
<a id="<? echo $blockId; ?>" class="btn btn-default tooltipster-target" href="<? echo $buttonItem->url; ?>"
   <? if (!empty($buttonItem->hoverTip)): ?>title="<? echo $buttonItem->hoverTip; ?>"<? endif; ?>
   target="<? if (!empty($buttonItem->url)): ?>_blank<? else: ?>_self<? endif; ?>">
    <?if(!empty($buttonItem->image)):?>
        <img src="<? echo $buttonItem->image; ?>" style="height: auto; width: auto; max-width: 100%;"/>
    <?else:?>
        <?if(!empty($buttonItem->icon)):?>
            <span class="button-icon <? echo $buttonItem->icon; ?>" aria-hidden="true"></span>
        <? endif;?>
        <? echo $buttonItem->text; ?>
    <? endif;?>
</a>
