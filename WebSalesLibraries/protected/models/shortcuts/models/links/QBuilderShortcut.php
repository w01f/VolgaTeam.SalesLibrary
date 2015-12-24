<?

	/**
	 * Class QBuilderShortcut
	 */
	class QBuilderShortcut extends PageContentShortcut
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
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Qbuilder';
		}

		/**
		 * @return array
		 */
		public function getPageData()
		{
			$data = parent::getPageData();

			$userId = Yii::app()->user->getId();
			$pages = QPageRecord::model()->getByOwner($userId);
			$data['selectedPageId'] = count($pages) > 0 ? $pages[0]->id : null;
			$data['serviceData'] = $this->getMenuItemData();
			return $data;
		}
	}