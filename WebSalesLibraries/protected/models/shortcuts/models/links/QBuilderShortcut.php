<?

	/**
	 * Class QBuilderShortcut
	 */
	class QBuilderShortcut extends PageContentShortcut
	{
		public $showLinkCart;

		/**
		 * @param ShortcutLinkRecord $linkRecord
		 * @param $isPhone boolean
		 * @param $parameters array
		 */
		public function __construct($linkRecord, $isPhone, $parameters = null)
		{
			parent::__construct($linkRecord, $isPhone);
			if (isset($parameters) && array_key_exists('showLinkCart', $parameters))
				$this->showLinkCart = filter_var($parameters['showLinkCart'], FILTER_VALIDATE_BOOLEAN);
			else
				$this->showLinkCart = false;
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

			$userId = UserIdentity::getCurrentUserId();
			$pages = QPageRecord::model()->getByOwner($userId);
			$data['selectedPageId'] = count($pages) > 0 ? $pages[0]->id : null;
			$data['serviceData'] = $this->getMenuItemData();
			return $data;
		}
	}