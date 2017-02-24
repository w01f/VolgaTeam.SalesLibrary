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

		/** @var  HideCondition */
		public $hideIconCondition;
		/** @var  HideCondition */
		public $hideTextCondition;

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

			$queryResult = $xpath->query('//Config/Regular/HideHeaderIcon');
			if ($queryResult->length > 0)
				$this->hideIconCondition = HideCondition::fromXml($xpath, $queryResult->item(0));
			else
				$this->hideIconCondition = new HideCondition();

			$queryResult = $xpath->query('//Config/Regular/HideHeaderTitle');
			if ($queryResult->length > 0)
				$this->hideTextCondition = HideCondition::fromXml($xpath, $queryResult->item(0));
			else
				$this->hideTextCondition = new HideCondition();

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
			return self::createShortcutUrl($this->id, $this->samePage);
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
		 * @return string
		 */
		public function getMenuItemData()
		{
			$result = parent::getMenuItemData();
			$result .= '<div class="push-history"></div>';
			return $result;
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

			$data['headerIconHideCondition'] = array(
				'extraSmall' => $this->hideIconCondition->extraSmall,
				'small' => $this->hideIconCondition->small,
				'medium' => $this->hideIconCondition->medium,
				'large' => $this->hideIconCondition->large,
			);

			$data['headerTitleHideCondition'] = array(
				'extraSmall' => $this->hideTextCondition->extraSmall,
				'small' => $this->hideTextCondition->small,
				'medium' => $this->hideTextCondition->medium,
				'large' => $this->hideTextCondition->large,
			);

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

		/**
		 * @var $linkId string
		 * @param $samePage boolean
		 * @return string
		 */
		public static function createShortcutUrl($linkId, $samePage)
		{
			if ($samePage)
				return Yii::app()->createAbsoluteUrl('shortcuts/getSamePage');
			else
				return Yii::app()->createAbsoluteUrl('shortcuts/getSinglePage', array('linkId' => $linkId));
		}
	}