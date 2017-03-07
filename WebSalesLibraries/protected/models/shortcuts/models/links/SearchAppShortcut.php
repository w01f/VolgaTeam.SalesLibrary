<?

	/**
	 * Class SearchAppShortcut
	 */
	class SearchAppShortcut extends PageContentShortcut
	{
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
				'showType' => true,
				'showDate' => true,
				'showRate' => true,
				'showViewsCount' => true,
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