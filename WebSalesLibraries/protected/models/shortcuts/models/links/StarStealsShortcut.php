<?

	/**
	 * Class StarStealsShortcut
	 */
	class StarStealsShortcut extends PageContentShortcut
	{
		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'StarsSteals';
		}

		/**
		 * @return array
		 */
		public function getPageData()
		{
			$data = parent::getPageData();

			$userId = UserIdentity::getCurrentUserId();
			$items = StarStealsItemRecord::model()->getListItems($userId);
			$data['selectedItemId'] = count($items) > 0 ? $items[0]->id : null;
			$data['serviceData'] = $this->getMenuItemData();
			return $data;
		}
	}