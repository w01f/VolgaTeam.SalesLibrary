<?
	/** @var $actionContainer IShortcutActionContainer */

	$actions = $actionContainer->getActions();
	$actionCount = count($actions);
	$columnCount = ceil($actionCount / 5);
	$columnIndex = 0;
	$actionsByColumn = array();
	$prevGroup = '';
	foreach ($actions as $action)
	{
		if ($prevGroup != $action->group || $prevGroup == '')
			$columnIndex++;
		if ($columnIndex > $columnCount)
			$columnIndex = 1;
		$actionsByColumn[$columnIndex][] = $action;
		$prevGroup = $action->group;
	}
?>
<? foreach ($actionsByColumn as $columnItems): ?>
	<div class="column">
		<? foreach ($columnItems as $action): ?>
			<? $this->renderPartial('../menu/actionItem', array('action' => $action)); ?>
		<? endforeach; ?>
	</div>
<? endforeach; ?>


