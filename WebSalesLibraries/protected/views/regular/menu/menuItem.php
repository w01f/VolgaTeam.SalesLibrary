<?
	/** @var $menuItem MenuItem */

	$identifier = 'item-' . $menuItem->shortcut->id;
	if (isset($menuItem->appearance->size) && $menuItem->appearance->size != 'regular')
		$itemClass = 'tile-bt-' . $menuItem->appearance->size;
	else
		$itemClass = 'tile-bt';
	$itemClass .= ' om-closenav';
	if ($menuItem->shortcut->title == '')
		$itemClass .= ' only-icon';
	else if ($menuItem->shortcut->useIcon != true && !isset($menuItem->shortcut->imageContent))
		$itemClass .= ' only-text';
?>
<style>
	<?echo '#'.$identifier;?>
	{
		background-color:<?echo '#'.$menuItem->appearance->backColor;?> !important;
	}
	<?echo '#'.$identifier;?> i
	{
		color: <?echo '#'.$menuItem->appearance->iconColor;?> !important;
		<?if(isset($menuItem->appearance->iconSize)):?>
			font-size: <?echo $menuItem->appearance->iconSize.'px';?> !important;
		<?endif;?>
	}
	<?echo '#'.$identifier;?> span
	{
		color: <?echo '#'.$menuItem->appearance->textColor;?> !important;
		text-align: <?echo $menuItem->appearance->textAlign;?> !important;
		<?if(isset($menuItem->appearance->textSize)):?>
			font-size: <?echo $menuItem->appearance->textSize.'px';?> !important;
		<?endif;?>
	}
	<?echo '#'.$identifier;?>:hover
	{
		box-shadow: 0 0 6px 3px <?echo '#'.$menuItem->appearance->shadowColor;?> !important;
		-webkit-box-shadow: 0 0 6px 3px <?echo '#'.$menuItem->appearance->shadowColor;?> !important;
		-moz-box-shadow: 0 0 6px 3px <?echo '#'.$menuItem->appearance->shadowColor;?> !important;
		-o-box-shadow: 0 0 6px 3px <?echo '#'.$menuItem->appearance->shadowColor;?> !important;
		-ms-box-shadow: 0 0 6px 3px <?echo '#'.$menuItem->appearance->shadowColor;?> !important;
	}
</style>
<div id="<? echo $identifier; ?>" class="<? echo $itemClass ?> om-item" data-group="group-<? echo $menuItem->shortcut->groupId; ?>">
	<a href="<? echo $menuItem->shortcut->getSourceLink(); ?>" class="shortcuts-link<? if ($menuItem->appearance->useGradient == true): ?> gradient<? endif; ?>" target="_blank">
		<? if ($menuItem->shortcut->useIcon == true): ?>
			<i class="<? echo $menuItem->shortcut->iconClass; ?>"></i>
		<? elseif ($menuItem->shortcut->useIcon != true && isset($menuItem->shortcut->imageContent)): ?>
			<img src="<? echo $menuItem->shortcut->imageContent; ?>" alt=""/>
		<?endif; ?>
		<span><? echo $menuItem->shortcut->title; ?></span>
		<div class="service-data">
			<? echo $menuItem->shortcut->getMenuItemData(); ?>
		</div>
	</a>
</div>