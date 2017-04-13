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
				'columnSettings' => DataColumnSettings::createEmpty(),
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