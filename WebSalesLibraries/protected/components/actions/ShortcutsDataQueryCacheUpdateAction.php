<?php

	/**
	 * Class ShortcutsDataQueryCacheUpdateAction
	 */
	class ShortcutsDataQueryCacheUpdateAction extends CAction
	{
		public function run()
		{
			echo "Job started...\n";

			ShortcutsManager::prepareDataQueryCache(false);

			echo "Job completed...\n";
		}
	}