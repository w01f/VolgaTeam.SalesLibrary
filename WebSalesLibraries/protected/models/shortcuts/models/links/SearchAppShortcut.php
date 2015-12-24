<?

	/**
	 * Class SearchAppShortcut
	 */
	class SearchAppShortcut extends PageContentShortcut
	{
		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);
		}

		/**
		 * @return array
		 */
		public function getPageData()
		{
			$data = parent::getPageData();
			$data['serviceData'] = $this->getMenuItemData();
			$data['viewOptions'] = array(
				'showCategory' => Yii::app()->params['search_options']['hide_tag'] != true,
				'categoryColumnName' => Yii::app()->params['tags']['column_name'],
				'showLibraries' => Yii::app()->params['search_options']['hide_libraries'] != true,
				'librariesColumnName' => Yii::app()->params['stations']['column_name'],
				'showDeleteButton' => false
			);
			return $data;
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'SearchApp';
		}
	}