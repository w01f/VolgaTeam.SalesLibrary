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
		 * @return array
		 */
		public function getViewParameters()
		{
			$viewParameters = parent::getViewParameters();

			$userId = Yii::app()->user->getId();
			$pages = QPageRecord::model()->getByOwner($userId);

			$viewParameters['pages'] = $pages;
			$viewParameters['selectedPage'] = count($pages) > 0 ? $pages[0] : null;
			$viewParameters['links'] = UserLinkCartRecord::getLinksByUser($userId);

			return $viewParameters;
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Search App';
		}
	}