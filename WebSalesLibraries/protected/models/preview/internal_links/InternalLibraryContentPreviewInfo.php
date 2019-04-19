<?

	/**
	 * Class InternalLibraryContentPreviewInfo
	 */
	abstract class InternalLibraryContentPreviewInfo implements IShortcutActionContainer
	{
		public $internalLinkId;
		public $internalLinkType;

		public $styleSettingsEncoded;

		public $showHeaderText;

		/** @var  RegularPageHeaderSettings */
		public $headerSettings;

		/** @var  ShortcutAction[] */
		public $actions;
		public $actionsContent;

		public $navigationPanel;

		/** @var SearchBar */
		public $searchBar;

		/**
		 * @param $linkSettings InternalWallbinLinkSettings|InternalLibraryPageLinkSettings|InternalLibraryFolderLinkSettings
		 * @param $isPhone boolean
		 */
		protected function __construct($linkSettings, $isPhone)
		{
			$this->internalLinkType = $linkSettings->internalLinkType;
			$this->styleSettingsEncoded = !empty($linkSettings->styleSettingsEncoded) ? base64_decode($linkSettings->styleSettingsEncoded) : null;
			$this->showHeaderText = $linkSettings->showHeaderText;
			$this->searchBar = SearchBar::createEmpty();
			$this->headerSettings = RegularPageHeaderSettings::createEmpty();

			if (!empty($this->styleSettingsEncoded))
			{
				$styleConfig = new DOMDocument();
				$styleConfig->loadXML($this->styleSettingsEncoded);
				$xpath = new DomXPath($styleConfig);

				$queryResult = $xpath->query('//Config/Regular/HeaderSettings');
				if ($queryResult->length > 0)
					$this->headerSettings = RegularPageHeaderSettings::fromXml($xpath, $queryResult->item(0));

				$queryResult = $xpath->query('//Config/ShowLeftPanel');
				$showNavigationPanel = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
				if ($showNavigationPanel && !UserIdentity::isUserAuthorized())
				{
					$queryResult = $xpath->query('//Config/LeftPanelPublicUser');
					$showNavigationPanel = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
				}
				$queryResult = $xpath->query('//Config/LeftPanelID');
				$navigationPanelId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
				if ($showNavigationPanel)
				{
					$navigationPanelData = ShortcutsManager::getNavigationPanel($navigationPanelId, $isPhone);
					if (isset($navigationPanelData->items))
					{
						$viewPath = \Yii::getPathOfAlias('application.views.regular.shortcuts.navigationPanel') . '/itemsList.php';
						$this->navigationPanel = array(
							'content' => \Yii::app()->controller->renderFile($viewPath, array('navigationPanel' => $navigationPanelData), true),
							'options' => array(
								'expanded' => $navigationPanelData->isExpanded,
								'hideCondition' => array(
									'extraSmall' => $navigationPanelData->hideCondition->extraSmall,
									'small' => $navigationPanelData->hideCondition->small,
									'medium' => $navigationPanelData->hideCondition->medium,
									'large' => $navigationPanelData->hideCondition->large,
								)
							)
						);
					}
				}

				if ($isPhone != true)
					$this->searchBar = SearchBar::fromInternalLink($this);
			}
		}

		/**
		 * @param $xpath DOMXPath
		 * @param $actionConfigNodes DOMNodeList
		 */
		protected function initActions($xpath, $actionConfigNodes)
		{
			$actionsByKey = ShortcutAction::getInternalLinkActions($this);
			$this->customizeActions($actionsByKey, $xpath, $actionConfigNodes);
			foreach ($actionsByKey as $action)
			{
				/** @var $action ShortcutAction */
				if ($action->enabled == true)
					$actions[] = $action;
			}
			$sortHelper = new ObjectSortHelper('order', 'asc');
			usort($actions, array($sortHelper, 'sort'));
			$this->actions = $actions;
			$viewPath = \Yii::getPathOfAlias('application.views.regular.menu') . '/actionItems.php';
			$this->actionsContent = \Yii::app()->controller->renderFile($viewPath, array('actionContainer' => $this), true);
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
					$action->backColor = $this->headerSettings->barBackColor;
					$action->textColor = $this->headerSettings->shortcutGroupsColor;
					ShortcutAction::configureFromXml($action, $xpath, $configNode);
				}
			}
		}

		/** @return string */
		public function getShortcutActionsTag()
		{
			switch ($this->internalLinkType)
			{
				case 1:
					return 'library';
				case 2:
					return 'page';
				case 3:
					return 'window';
				default:
					return null;
			}
		}

		/**
		 * @return ShortcutAction[]
		 */
		public function getActions()
		{
			return $this->actions;
		}
	}