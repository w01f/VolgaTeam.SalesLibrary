<?

	use application\models\data_query\data_table\DataTableColumnSettings;

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
				'columnSettings' => DataTableColumnSettings::createEmpty(),
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

		/**
		 * @return string
		 */
		public function getTitleForActivityTracker()
		{
			if ($this->isPhone)
				return 'Mobile Search Shortcut ' . parent::getTitleForActivityTracker();
			return parent::getTitleForActivityTracker();
		}
	}