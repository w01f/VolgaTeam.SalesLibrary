<?

	/**
	 * Class PageContentShortcut
	 */
	abstract class PageContentShortcut extends CustomHandledShortcut implements IShortcutActionContainer
	{
		public $headerIcon;
		public $showMainSiteUrl;

		/** @var  ShortcutAction[] */
		public $actions;

		public $showNavigationPanel;
		public $navigationPanelId;

		public $allowPublicAccess;
		public $publicPassword;

		/** @var  HideCondition */
		public $hideIconCondition;
		/** @var  HideCondition */
		public $hideTextCondition;

		public function loadPageConfig()
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);
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

			$queryResult = $xpath->query('//Config/Actions/Action');
			$this->initActions($xpath, $queryResult);
		}

		/**
		 * @param $xpath DOMXPath
		 * @param $actionConfigNodes DOMNodeList
		 */
		protected function initActions($xpath, $actionConfigNodes)
		{
			$actionsByKey = ShortcutAction::getShortcutActions($this);
			$this->customizeActions($actionsByKey, $xpath, $actionConfigNodes);

			$actions = array();
			foreach ($actionsByKey as $action)
			{
				/** @var $action ShortcutAction */
				if ($action->enabled == true)
					$actions[] = $action;
			}
			$sortHelper = new ObjectSortHelper('order', 'asc');
			usort($actions, array($sortHelper, 'sort'));
			$this->actions = $actions;
		}

		/**
		 * @return ShortcutAction[]
		 */
		public function getActions()
		{
			return $this->actions;
		}

		/**
		 * @param $actionsByKey array
		 * @param $xpath DOMXPath
		 * @param $actionConfigNodes DOMNodeList
		 */
		protected function customizeActions($actionsByKey, $xpath, $actionConfigNodes)
		{
			foreach ($actionConfigNodes as $configNode)
			{
				$queryResult = $xpath->query('Tag', $configNode);
				if ($queryResult->length == 0) continue;
				$tag = trim($queryResult->item(0)->nodeValue);
				if (array_key_exists($tag, $actionsByKey))
					ShortcutAction::configureFromXml($actionsByKey[$tag], $xpath, $configNode);
			}
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