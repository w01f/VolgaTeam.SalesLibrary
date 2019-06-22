<?

	namespace application\models\marketing_contest\models;

	class Dictionaries
	{
		/**
		 * @return array
		 */
		public static function getMarketList()
		{
			$marketListFilePath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'mktng1_markets.txt';
			if (file_exists($marketListFilePath))
				return file($marketListFilePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES);
			else
				return array();
		}

		/**
		 * @return array
		 */
		public static function getStrategiesList()
		{
			$categoriesListFilePath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'mktng1_strategies.txt';
			if (file_exists($categoriesListFilePath))
				return file($categoriesListFilePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES);
			else
				return array();
		}

	}