<?

	use application\models\shortcuts\models\landing_page\regular_markup\common\FixedPanelSettings;

	/**
	 * Class PageContentShortcut
	 */
	abstract class PageContentShortcut extends CustomHandledShortcut implements IShortcutActionContainer
	{
		/** @var  ShortcutAction[] */
		public $actions;

		public $showNavigationPanel;
		public $navigationPanelId;

		public $allowPublicAccess;
		public $publicPassword;

		/** @var  RegularPageHeaderSettings | MobilePageHeaderSettings */
		public $headerSettings;

		/** @var  FixedPanelSettings */
		public $fixedPanelSettings;

		public $autoLoadShortcutId;
		public $autoLoadLinkId;
		public $autoLoadLinkShowDelayHours;

		public $usePermissions;

		public function initRegularModel()
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);
			$xpath = new DomXPath($linkConfig);

			if ($this->isPhone)
			{
				$queryResult = $xpath->query('//Config/Mobile/HeaderSettings');
				if ($queryResult->length > 0)
					$this->headerSettings = MobilePageHeaderSettings::fromXml($xpath, $queryResult->item(0), $this);
				else
					$this->headerSettings = MobilePageHeaderSettings::createEmpty();
			}
			else
			{
				$queryResult = $xpath->query('//Config/Regular/HeaderSettings');
				if ($queryResult->length > 0)
					$this->headerSettings = RegularPageHeaderSettings::fromXml($xpath, $queryResult->item(0));
				else
					$this->headerSettings = RegularPageHeaderSettings::createEmpty();
			}

			parent::initRegularModel();

			if ($this->isPhone)
			{
				$this->headerSettings->title = $this->headerTitle != '' ?
					$this->headerTitle :
					($this->title != '' ? $this->title : $this->description);
			}
			else
				$this->headerSettings->title = $this->description;

			$queryResult = $xpath->query('//Config/OpenOnSamePage');
			$this->samePage = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
		}

		public function loadPageConfig()
		{
			$this->usePermissions = true;

			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);
			$xpath = new DomXPath($linkConfig);

			$queryResult = $xpath->query('//Config/ShowLeftPanel');
			$this->showNavigationPanel = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
			if ($this->showNavigationPanel && !UserIdentity::isUserAuthorized())
			{
				$queryResult = $xpath->query('//Config/LeftPanelPublicUser');
				$this->showNavigationPanel = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
			}

			$queryResult = $xpath->query('//Config/LeftPanelID');
			$this->navigationPanelId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('//Config/AllowPublicAccess');
			$this->allowPublicAccess = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

			$queryResult = $xpath->query('//Config/PublicPassword');
			$this->publicPassword = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('//Config/AutoLoadShortcutId');
			$this->autoLoadShortcutId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('//Config/AutoLoadLink');
			if ($queryResult->length > 0)
				$this->configureAutoLoadLink($xpath, $queryResult->item(0));

			if (!$this->isPhone)
			{
				$queryResult = $xpath->query('//Config/FixedPanel');
				if ($queryResult->length > 0)
					$this->fixedPanelSettings = FixedPanelSettings::fromXml($this, $xpath, $queryResult->item(0));
			}

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
				{
					/** @var ShortcutAction $action */
					$action = $actionsByKey[$tag];
					ShortcutAction::configureFromXml($action, $xpath, $configNode);
				}
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

			if (!empty($this->autoLoadShortcutId))
				$data['autoLoadShortcutId'] = $this->autoLoadShortcutId;
			else if (!empty($this->autoLoadLinkId))
				$data['autoLoadLinkId'] = $this->autoLoadLinkId;

			$data['headerOptions'] = $this->headerSettings;

			if (!$this->isPhone)
			{
				$data['headerIcon'] = $this->headerSettings->icon;
				$data['headerIconHideCondition'] = array(
					'extraSmall' => $this->headerSettings->hideIconCondition->extraSmall,
					'small' => $this->headerSettings->hideIconCondition->small,
					'medium' => $this->headerSettings->hideIconCondition->medium,
					'large' => $this->headerSettings->hideIconCondition->large,
				);

				$data['headerTitleHideCondition'] = array(
					'extraSmall' => $this->headerSettings->hideTextCondition->extraSmall,
					'small' => $this->headerSettings->hideTextCondition->small,
					'medium' => $this->headerSettings->hideTextCondition->medium,
					'large' => $this->headerSettings->hideTextCondition->large,
				);
			}

			return $data;
		}

		/** @return NavigationPanel */
		public function getNavigationPanel()
		{
			if ($this->showNavigationPanel)
				return ShortcutsManager::getNavigationPanel($this->navigationPanelId, $this->isPhone);
			return null;
		}

		/**
		 * @param $linkId string
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

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		private function configureAutoLoadLink($xpath, $contextNode)
		{
			$queryResult = $xpath->query('./Enabled', $contextNode);
			$enabled = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
			$cookieTag = sprintf("autoload-link-hours-separation-%s", $this->id);
			if ($enabled)
			{
				$queryResult = $xpath->query('./HourSeparation', $contextNode);
				$hoursSeparation = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 0;
				$cookieValue = null;
				if ($hoursSeparation > 0)
				{
					$cookieValue = isset(Yii::app()->request->cookies[$cookieTag]) ? Yii::app()->request->cookies[$cookieTag]->value : null;
				}
				if (!isset($cookieValue))
				{
					$queryResult = $xpath->query('./LibraryName', $contextNode);
					$libraryName = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
					$queryResult = $xpath->query('./PageName', $contextNode);
					$pageName = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
					$queryResult = $xpath->query('./WindowName', $contextNode);
					$windowName = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
					$queryResult = $xpath->query('./LinkName', $contextNode);
					$linkName = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
					if (!empty($libraryName) && !empty($pageName) && !empty($windowName) && !empty($linkName))
					{
						$linkRecord = LinkRecord::getLinkByName($libraryName, $pageName, $windowName, $linkName);
						if (isset($linkRecord))
						{
							$this->autoLoadLinkId = $linkRecord->id;
							if ($hoursSeparation > 0)
							{
								$cookie = new CHttpCookie($cookieTag, $hoursSeparation);
								$cookie->expire = time() + (60 * 60 * $hoursSeparation);
								Yii::app()->request->cookies[$cookieTag] = $cookie;
							}
						}
					}
				}
			}
			else
				unset(Yii::app()->request->cookies[$cookieTag]);
		}
	}