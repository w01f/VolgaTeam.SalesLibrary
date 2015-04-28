<?

	/**
	 * Class GridPage
	 */
	class GridPage extends PageModel
	{
		/**
		 * @param $pageRecord ShortcutsPageRecord
		 */
		public function __construct($pageRecord)
		{
			parent::__construct($pageRecord);
			$this->type = 'grid';
			$this->viewPath = 'grid';
			$this->allowSwitchView = false;
		}

		/**
		 * @return array
		 */
		public function getDisplayParameters()
		{
			return array(
				'defaultFilter' => '*',
				'animationType' => 'slideDelay',
				'gapHorizontal' => 20,
				'gapVertical' => 20,
				'gridAdjustment' => 'responsive',
				'caption' => 'overlayBottomAlong',
				'displayType' => 'bottomToTop',
				'displayTypeSpeed' => 100,
				'showPageModeToggle' => $this->allowSwitchView,
			);
		}

		/**
		 * @param $pageRecord ShortcutsPageRecord
		 * @return ShortcutsLinkRecord[]
		 */
		public function getPageLinkRecords($pageRecord)
		{
			return $pageRecord->getLinks();
		}
	}