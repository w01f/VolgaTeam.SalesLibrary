<?

	use application\models\shortcuts\models\landing_page\regular_markup\button_group\ButtonItem;

	/** @var $buttonItem ButtonItem */

	$blockId = sprintf('button-item-%s', $buttonItem->id);
	echo $this->renderPartial('landingPageMarkup/button_group/buttonStyle', array('buttonItem' => $buttonItem, 'blockId' => $blockId), true);
?>
<a id="<? echo $blockId; ?>" class="btn btn-default shortcuts-link tooltipster-target" href="<? echo isset($buttonItem->shortcut) ? $buttonItem->shortcut->getSourceLink() : '#'; ?>"
   <? if (!empty($buttonItem->hoverTip)): ?>title="<? echo $buttonItem->hoverTip; ?>"<? endif; ?>>
	<?if(!empty($buttonItem->image)):?>
        <img src="<? echo $buttonItem->image; ?>" style="height: auto; width: auto; max-width: 100%;"/>
	<?else:?>
		<?if(!empty($buttonItem->icon)):?>
            <span class="button-icon <? echo $buttonItem->icon; ?>" aria-hidden="true"></span>
		<? endif;?>
		<? echo $buttonItem->text; ?>
	<? endif;?>
	<div class="service-data">
		<? echo isset($buttonItem->shortcut) ? $buttonItem->shortcut->getMenuItemData() : '<div class="same-page"></div><div class="has-custom-handler"></div>'; ?>
	</div>
</a>
