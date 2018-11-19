<?
	namespace application\models\sales_requests\models;

	class Dictionaries
	{
		public const StatusItemAll = "all";
		public const StatusItemIncompleted = "incomplete";
		public const StatusItemSubmitted = "submitted";
		public const StatusItemInReview = "in review";
		public const StatusItemWorking = "working";
		public const StatusItemComplete = "complete";

		/**
		 * @return array
		 */
		public static function getStatusList()
		{
			return array(
				self::StatusItemIncompleted,
				self::StatusItemSubmitted,
				self::StatusItemInReview,
				self::StatusItemWorking,
				self::StatusItemComplete,
			);
		}

		/**
		 * @return array
		 */
		public static function getAssignedList()
		{
			$assignedToListFilePath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'rrq1_assigned.txt';
			if (file_exists($assignedToListFilePath))
				return file($assignedToListFilePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES);
			else
				return array();
		}

		/**
		 * @return array
		 */
		public static function getCategoriesList()
		{
			$categoriesListFilePath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'rrq1_categories.txt';
			if (file_exists($categoriesListFilePath))
				return file($categoriesListFilePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES);
			else
				return array();
		}

		/**
		 * @return array
		 */
		public static function getDemoList()
		{
			$demosListFilePath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'rrq1_demos.txt';
			if (file_exists($demosListFilePath))
				return file($demosListFilePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES);
			else
				return array();
		}

		/**
		 * @return array
		 */
		public static function getReasonsList()
		{
			$reasonsListFilePath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'rrq1_reasons.txt';
			if (file_exists($reasonsListFilePath))
				return file($reasonsListFilePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES);
			else
				return array();
		}
	}