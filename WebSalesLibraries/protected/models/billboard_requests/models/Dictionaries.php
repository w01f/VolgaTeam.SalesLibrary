<?

	namespace application\models\billboard_requests\models;

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
			$assignedToListFilePath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'bbrd1_assigned.txt';
			if (file_exists($assignedToListFilePath))
				return file($assignedToListFilePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES);
			else
				return array();
		}

		/**
		 * @return array
		 */
		public static function getLengthsList()
		{
			$categoriesListFilePath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'bbrd1_length.txt';
			if (file_exists($categoriesListFilePath))
				return file($categoriesListFilePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES);
			else
				return array();
		}

		/**
		 * @return array
		 */
		public static function getPropertyList()
		{
			$demosListFilePath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'bbrd1_property.txt';
			if (file_exists($demosListFilePath))
				return file($demosListFilePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES);
			else
				return array();
		}
	}