<?

	namespace application\models\data_query\data_table;
	/**
	 * Class DataTableQueryHelper
	 */
	class DataTableQueryHelper
	{
		/**
		 * @param $querySettings DataTableQuerySettings
		 * @return \CDbCommand
		 */
		public static function buildQuery($querySettings)
		{
			/** @var \CDbCommand $dbCommand */
			$dbCommand = \Yii::app()->db->createCommand();

			$dbCommand = $dbCommand->from($querySettings->from);

			$queryFields = array_merge($querySettings->baseQueryFields, $querySettings->customQueryFields);
			$dbCommand = $dbCommand->select(array_values($queryFields));

			foreach ($querySettings->innerJoin as $table => $condition)
				$dbCommand = $dbCommand->join($table, $condition);

			foreach ($querySettings->leftJoin as $table => $condition)
				$dbCommand = $dbCommand->leftJoin($table, $condition);

			$dbCommand = $dbCommand->where($querySettings->whereConditions);

			$dbCommand = $dbCommand->group($querySettings->groupFields);

			if (count($querySettings->sortSettings) > 0)
				$dbCommand = $dbCommand->order(sprintf("%s %s", $querySettings->sortSettings['field'], $querySettings->sortSettings['order']));

			if ($querySettings->limit > 0)
				$dbCommand = $dbCommand->limit($querySettings->limit);
			return $dbCommand;
		}
	}