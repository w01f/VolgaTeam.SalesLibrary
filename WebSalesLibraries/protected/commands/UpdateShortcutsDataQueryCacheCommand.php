<?php

	/**
	 * Class UpdateShortcutsDataQueryCacheCommand
	 */
	class UpdateShortcutsDataQueryCacheCommand extends CConsoleCommand
	{
		/**
		 * @param array $args
		 * @return int|void
		 */
		public function run($args)
		{
			ob_start();

			$action = Yii::createComponent('application.components.actions.ShortcutsDataQueryCacheUpdateAction', $this, 'updateShortcutsDataQueryCache');
			$action->run();

			$result = ob_get_contents();
			ob_end_clean();

			echo $result;
		}
	}
