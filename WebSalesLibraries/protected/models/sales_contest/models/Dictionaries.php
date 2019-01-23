<?
	namespace application\models\sales_contest\models;

	class Dictionaries
	{
		/**
		 * @return array
		 */
		public static function getCategoriesList()
		{
			$categoriesListFilePath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'wow_categories.txt';
			if (file_exists($categoriesListFilePath))
				return file($categoriesListFilePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES);
			else
				return array();
		}

		/**
		 * @return array
		 */
		public static function getMarketList()
		{
			$marketListFilePath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'wow_markets.txt';
			if (file_exists($marketListFilePath))
				return file($marketListFilePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES);
			else
				return array();
		}

		/**
		 * @return array
		 */
		public static function getStationList()
		{
			$stationsListFilePath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'wow_stations.txt';
			if (file_exists($stationsListFilePath))
				return file($stationsListFilePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES);
			else
				return array();
		}
	}