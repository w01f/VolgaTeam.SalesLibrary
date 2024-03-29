<?
	/** @var $action ShortcutAction */
	$identifier = 'shortcut-action-' . $action->id;
?>
<style>
	<?echo '#'.$identifier;?> {background-color: <?echo Utils::formatColorToHex($action->backColor);?> !important;}
	<?echo '#'.$identifier;?>:hover {background-color: <?echo Utils::formatColorToHex($action->backColor);?> !important;}
	<?echo '#'.$identifier;?> .icon	{color: <?echo Utils::formatColorToHex($action->iconColor);?> !important;}
	<?echo '#'.$identifier;?> .text {color: <?echo Utils::formatColorToHex($action->textColor);?> !important;}
</style>
<a id='<? echo $identifier; ?>' class="menu metro-green-1 shortcut-action <? echo $action->group.' '.$action->tag; ?>" href="#">
	<div class="icon">
		<i class="<? echo $action->iconClass; ?>"></i>
	</div>
	<div class="text"><? echo $action->title; ?></div>
	<div class="service-data">
		<? echo $action->getActionData(); ?>
	</div>
</a>