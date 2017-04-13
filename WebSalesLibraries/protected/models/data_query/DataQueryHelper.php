<?

	/**
	 * Class DataQueryHelper
	 */
	class DataQueryHelper
	{
		/**
		 * @param $querySettings QuerySettings
		 * @return CDbCommand
		 */
		public static function buildQuery($querySettings)
		{
			/** @var CDbCommand $dbCommand */
			$dbCommand = Yii::app()->db->createCommand();

			$dbCommand = $dbCommand->from($querySettings->from);

			$queryFields = array_merge($querySettings->baseQueryFields, $querySettings->customQueryFields);
			$dbCommand = $dbCommand->select(array_values($queryFields));

			foreach ($querySettings->innerJoin as $table => $condition)
				$dbCommand = $dbCommand->join($table, $condition);

			foreach ($querySettings->leftJoin as $table => $condition)
				$dbCommand = $dbCommand->leftJoin($table, $condition);

			$whereConditions = array('AND',
				'link.is_preview_not_ready=0');
			$includeAppLinks = Yii::app()->browser->getBrowser() == Browser::BROWSER_EO;
			if ($includeAppLinks)
				$whereConditions[] = 'link.type<>15';
			$whereConditions = array_merge($whereConditions, $querySettings->whereConditions);
			$dbCommand = $dbCommand->where($whereConditions);

			$dbCommand = $dbCommand->group($querySettings->groupFields);

			if (count($querySettings->sortSettings) > 0)
				$dbCommand = $dbCommand->order(sprintf("%s %s", $querySettings->sortSettings['field'], $querySettings->sortSettings['order']));

			if ($querySettings->limit > 0)
				$dbCommand = $dbCommand->limit($querySettings->limit);
			return $dbCommand;
		}
	}