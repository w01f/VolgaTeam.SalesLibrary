<?php

	/**
	 * Class BaseShortcut
	 */
	abstract class BaseShortcut implements IShortcutActionContainer
	{
		public $id;
		public $groupId;
		public $bundleId;
		public $type;
		public $title;
		public $headerTitle;
		public $description;
		public $order;
		public $enabled;

		public $carouselGroup;

		public $useIcon;
		public $iconClass;
		public $imageContent;

		public $isAccessGranted;

		public $appearance;

		public $isPhone;

		public $relativePath;
		public $relativeLink;

		public $actions;

		/** @var $linkRecord ShortcutLinkRecord */
		public $linkRecord;

		/**
		 * @param $linkRecord ShortcutLinkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			$this->linkRecord = $linkRecord;
			$this->id = $linkRecord->id;
			$this->groupId = $linkRecord->id_group;
			$this->bundleId = $linkRecord->id_parent;

			$this->relativePath = str_replace(Yii::app()->params['appRoot'], '', $linkRecord->source_path);
			$this->relativeLink = str_replace('\\', '/', $this->relativePath);

			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$xpath = new DomXPath($linkConfig);

			$queryResult = $xpath->query('//Config/Type');
			$this->type = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';

			$this->order = $linkRecord->order;


			$visualSettingsSubSection = $isPhone ? 'Mobile' : 'Regular';
			$queryResult = $xpath->query('//Config/' . $visualSettingsSubSection);
			if ($queryResult->length == 0)
				$visualSettingsSubSection = '';
			else
				$visualSettingsSubSection .= '/';

			$queryResult = $xpath->query('//Config/' . $visualSettingsSubSection . 'Enabled');
			$this->enabled = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;

			$queryResult = $xpath->query('//Config/' . $visualSettingsSubSection . 'Title');
			$this->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';

			$queryResult = $xpath->query('//Config/' . $visualSettingsSubSection . 'HeaderTitle');
			$this->headerTitle = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('//Config/' . $visualSettingsSubSection . 'Description');
			$this->description = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';

			$this->useIcon = false;
			$queryResult = $xpath->query('//Config/' . $visualSettingsSubSection . 'Icon');
			if ($queryResult->length > 0)
			{
				$iconValue = trim($queryResult->item(0)->nodeValue);
				$this->useIcon = strpos($iconValue, '.png') === false;
				if ($this->useIcon)
				{
					$this->iconClass = $iconValue;
				}
				else
				{
					$imagePath = $linkRecord->source_path . DIRECTORY_SEPARATOR . $iconValue;
					if (file_exists($imagePath))
					{
						$data = file_get_contents($imagePath);
						$this->imageContent = 'data:image/png;base64,' . base64_encode($data);
					}
				}
			}
			$queryResult = $xpath->query('//Config/' . $visualSettingsSubSection . 'ItemAppearance');
			if ($queryResult->length > 0)
				$this->appearance = ShortcutAppearance::fromXml($xpath, $queryResult->item(0));
			else
				$this->appearance = new ShortcutAppearance();


			$queryResult = $xpath->query('//Config/CarouselGroup');
			$this->carouselGroup = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';

			$this->isAccessGranted = true;
			$approvedUsers = array();
			$queryResult = $xpath->query('//Config/ApprovedUsers/User');
			foreach ($queryResult as $groupNode)
				$approvedUsers[] = trim($groupNode->nodeValue);
			$approvedGroups = array();
			$queryResult = $xpath->query('//Config/ApprovedGroups/Group');
			foreach ($queryResult as $groupNode)
				$approvedGroups[] = trim($groupNode->nodeValue);
			if (count($approvedUsers) > 0 || count($approvedGroups) > 0)
			{
				$this->isAccessGranted = false;
				$user = Yii::app()->user;
				if (isset($user))
				{
					$this->isAccessGranted |= in_array($user->login, $approvedUsers);

					$userGroups = $user->getState('groups');
					if (count($userGroups) > 0)
						$this->isAccessGranted |= array_intersect($userGroups, $approvedGroups);
				}
			}

			$queryResult = $xpath->query('//Config/Actions/Action');
			$this->initActions($xpath, $queryResult);

			$this->isPhone = $isPhone;
		}

		/**
		 * @return string
		 */
		public function getMenuItemData()
		{
			$result = '';
			$result .= '<div class="link-id">' . $this->id . '</div>';
			$result .= '<div class="link-type">' . $this->type . '</div>';
			$result .= '<div class="link-name">' . $this->title . ' - ' . $this->description . '</div>';
			$result .= '<div class="url">' . $this->getServiceDataUrl() . '</div>';
			$result .= '<div class="activity-data">' . CJSON::encode(array(
					'action' => $this->getTypeForActivityTracker(),
					'title' => sprintf('%s - %s', $this->title, $this->description)
				)) . '</div>';
			return $result;
		}

		/**
		 * @return array
		 */
		public function getPageData()
		{
			$data = array();
			$data['linkId'] = $this->id;
			$data['shortcutType'] = $this->type;
			return $data;
		}

		/**
		 * @return array
		 */
		public function getViewParameters()
		{
			$viewParameters = array('shortcut' => $this);
			return $viewParameters;
		}

		/**
		 * @return string
		 */
		public abstract function getSourceLink();

		/**
		 * @return string
		 */
		public function getServiceDataUrl()
		{
			return $this->getSourceLink();
		}

		/**
		 * @return string
		 */
		public abstract function getTypeForActivityTracker();

		/**
		 * @param $xpath DOMXPath
		 * @param $actionConfigNodes DOMNode[]
		 */
		protected function initActions($xpath, $actionConfigNodes)
		{
			$commonActions = ShortcutAction::getCommonActions();
			$customActions = ShortcutAction::getCustomActions($this->type);
			$actionsByKey = array_merge($commonActions, $customActions);

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
		 * @param $actionConfigNodes DOMNode[]
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
	}