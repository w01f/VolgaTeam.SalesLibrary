<?

	use application\models\shortcuts\models\landing_page\regular_markup\button_group\ButtonItem;

	/** @var $buttonItem ButtonItem */

	$blockId = sprintf('button-item-%s', $buttonItem->id);
	echo $this->renderPartial('landingPageMarkup/button_group/buttonStyle', array('buttonItem' => $buttonItem, 'blockId' => $blockId), true);
?>
<a id="<? echo $blockId; ?>" class="btn btn-default" href="<? echo isset($buttonItem->shortcut) ? $buttonItem->shortcut->getSourceLink() : '#'; ?>"
   title="<? echo $buttonItem->text; ?>"
   target="<? if (isset($buttonItem->shortcut)): ?>_blank<? else: ?>_self<? endif; ?>">
	<?if(!empty($buttonItem->icon)):?>
		<span class="<? echo $buttonItem->icon; ?>" aria-hidden="true"></span>
	<? endif;?>
	<? echo $buttonItem->text; ?>
</a>
