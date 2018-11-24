<?

	use application\models\shortcuts\models\landing_page\regular_markup\button_group\ButtonItem;

	/** @var $buttonItem ButtonItem */

	$blockId = sprintf('button-item-%s', $buttonItem->id);
	echo $this->renderPartial('landingPageMarkup/button_group/buttonStyle', array('buttonItem' => $buttonItem, 'blockId' => $blockId), true);
?>
<a id="<? echo $blockId; ?>" class="btn btn-default shortcuts-link" href="<? echo isset($buttonItem->shortcut) ? $buttonItem->shortcut->getSourceLink() : '#'; ?>"
   title="<? echo $buttonItem->text; ?>">
	<?if(!empty($buttonItem->icon)):?>
		<span class="<? echo $buttonItem->icon; ?>" aria-hidden="true"></span>
	<? endif;?>
	<? echo $buttonItem->text; ?>
	<div class="service-data">
		<? echo isset($buttonItem->shortcut) ? $buttonItem->shortcut->getMenuItemData() : '<div class="same-page"></div><div class="has-custom-handler"></div>'; ?>
	</div>
</a>
