<?

	/**
	 * Class PageContentShortcut
	 */
	abstract class PageContentShortcut extends CustomHandledShortcut
	{
		public $headerIcon;
		public $showMainSiteUrl;

		public $showNavigationPanel;

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

			$samePageTags = $linkConfig->getElementsByTagName("OpenOnSamePage");
			$this->samePage = $samePageTags->length > 0 ? filter_var(trim($samePageTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;

			$showMainSiteUrlTags = $linkConfig->getElementsByTagName("Fullsitelink");
			$this->showMainSiteUrl = $showMainSiteUrlTags->length > 0 ? filter_var(trim($showMainSiteUrlTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

			$queryResult = $xpath->query('//Config/HeaderIcon');
			$this->headerIcon = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';

			$showLeftPanelTags = $linkConfig->getElementsByTagName("ShowLeftPanel");
			$this->showNavigationPanel = $showLeftPanelTags->length > 0 ? filter_var(trim($showLeftPanelTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
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
				return ShortcutsManager::getNavigationPanel();
			return null;
		}
	}