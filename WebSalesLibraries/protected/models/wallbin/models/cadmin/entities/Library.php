<?
	namespace application\models\wallbin\models\cadmin\entities;
	use application\models\wallbin\models\cadmin\settings\AutoWidget;
	use application\models\wallbin\models\cadmin\settings\LibrarySettings;

	/**
	 * Class Library
	 */
	class Library extends VersionedObject
	{
		public $name;
		/** @var  LibrarySettings */
		public $settings;

		/**
		 * @param string $libraryId
		 * @return Library
		 */
		public static function get($libraryId)
		{
			/** @var  $libraryRecord \LibraryRecord */
			$libraryRecord = \LibraryRecord::model()->findByPk($libraryId);
			if (!isset($libraryRecord))
				return null;
			$info = new Library();
			$info->id = $libraryRecord->id;
			$info->name = $libraryRecord->name;
			$info->lastModified = $libraryRecord->last_update;
			if (isset($libraryRecord->settings))
				$info->settings = \CJSON::decode($libraryRecord->settings, false);
			else
			{
				$settings = new LibrarySettings();
				$settings->autoWidgets = array();
				/** @var \AutoWidgetRecord[] $autoWidgetsRecords */
				$autoWidgetsRecords = \AutoWidgetRecord::model()->findAll('id_library=?', array($info->id));
				foreach ($autoWidgetsRecords as $autoWidgetsRecord)
				{
					$autoWidget = new AutoWidget();
					$autoWidget->extension = $autoWidgetsRecord->extension;
					$autoWidget->originalImage = $autoWidgetsRecord->widget;
					$autoWidget->inverted = false;
					$settings->autoWidgets[] = $autoWidget;
				}
				$info->settings = $settings;
			}
			return $info;
		}
	}