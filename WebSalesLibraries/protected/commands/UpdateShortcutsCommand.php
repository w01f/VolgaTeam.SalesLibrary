<?php
class UpdateShortcutsCommand extends CConsoleCommand
{
    public function run($args)
    {
		ob_start();

		$action = Yii::createComponent('application.components.actions.ShortcutsUpdateAction', $this, 'updateShortcuts');
		$action->run();

		$result = ob_get_contents();
		ob_end_clean();

		echo $result;
    }
}
