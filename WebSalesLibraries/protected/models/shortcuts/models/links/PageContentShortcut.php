<?

	/**
	 * Class PageContentShortcut
	 */
	abstract class PageContentShortcut extends CustomHandledShortcut
	{
		public $headerIcon;
		public $showMainSiteUrl;

		public $showNavigationPanel;
		public $navigationPanelId;

		public $allowPublicAccess;
		public $publicPassword;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);

			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$xpath = new DomXPath($linkConfig);

			$queryResult = $xpath->query('//Config/OpenOnSamePage');
			$this->samePage = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;

			$queryResult = $xpath->query('//Config/Fullsitelink');
			$this->showMainSiteUrl = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

			$queryResult = $xpath->query('//Config/HeaderIcon');
			$this->headerIcon = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';

			$queryResult = $xpath->query('//Config/ShowLeftPanel');
			$this->showNavigationPanel = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

			$queryResult = $xpath->query('//Config/LeftPanelID');
			$this->navigationPanelId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('//Config/AllowPublicAccess');
			$this->allowPublicAccess = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

			$queryResult = $xpath->query('//Config/PublicPassword');
			$this->publicPassword = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
		}

		/**
		 * @return string
		 */
		public function getSourceLink()
		{
			if ($this->samePage)
				return Yii::app()->createAbsoluteUrl('shortcuts/getSamePage');
			else
				return Yii::app()->createAbsoluteUrl('shortcuts/getSinglePage', array('linkId' => $this->id));
		}

		/**
		 * @return string
		 */
		public function getServiceDataUrl()
		{
			if ($this->samePage)
				return parent::getServiceDataUrl();
			else
				return Yii::app()->createAbsoluteUrl('shortcuts/getSamePage');
		}

		/**
		 * @return array
		 */
		public function getPageData()
		{
			$data = parent::getPageData();
			if ($this->isPhone)
				$data['headerTitle'] = $this->headerTitle != '' ?
					$this->headerTitle :
					($this->title != '' ? $this->title : $this->description);
			else
				$data['headerTitle'] = $this->description;
			$data['headerIcon'] = $this->headerIcon;
			$data['linkId'] = $this->id;
			return $data;
		}

		/** @return NavigationPanel */
		public function getNavigationPanel()
		{
			if ($this->showNavigationPanel)
				return ShortcutsManager::getNavigationPanel($this->navigationPanelId);
			return null;
		}
	}