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
				'displayTypeSpeed' => 100
			);
		}
	}