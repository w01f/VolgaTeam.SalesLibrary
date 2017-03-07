<?

	/**
	 * Class BundleShortcut
	 */
	abstract class BundleShortcut extends ContainerShortcut
	{
		public $viewName;
		public $allowSwitchView;

		/**
		 * @return array
		 */
		public function getPageData()
		{
			$data = parent::getPageData();
			$data['displayParameters'] = $this->getDisplayParameters();
			$data['savedBundleModeTagName'] = $this->linkRecord->getUniqueId();
			$data['serviceData'] = $this->getMenuItemData();
			return $data;
		}

		public abstract function getDisplayParameters();
	}