<?
	namespace application\models\sales_ideas\models;

	class Dictionaries
	{
		/**
		 * @return array
		 */
		public static function getCategoriesList()
		{
			$categoriesListFilePath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'idea1_categories.txt';
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
			$marketListFilePath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'idea1_markets.txt';
			if (file_exists($marketListFilePath))
				return file($marketListFilePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES);
			else
				return array();
		}
	}