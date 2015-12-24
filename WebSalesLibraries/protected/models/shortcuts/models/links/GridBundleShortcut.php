<?

	/**
	 * Class GridBundleShortcut
	 */
	class GridBundleShortcut extends BundleShortcut
	{
		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);

			$this->type = 'gridbundle';
			$this->viewName = $this->isPhone ? 'bundle' :'gridBundle';

			$this->allowSwitchView = false;
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Gridbundle';
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
	}