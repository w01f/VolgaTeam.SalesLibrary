<?
	namespace application\models\wallbin\models\web;

	/**
	 * Class AutoWidget
	 */
	class AutoWidget
	{
		public $libraryId;
		public $extension;
		public $widget;

		/**
		 * @param $autoWidgetRecord
		 */
		public function load($autoWidgetRecord)
		{
			$this->libraryId = $autoWidgetRecord->id_library;
			$this->extension = $autoWidgetRecord->extension;
			$this->widget = $autoWidgetRecord->widget;
		}
	}
