<?php

	/**
	 * Class BaseShortcut
	 */
	abstract class BaseShortcut
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

		/** @var ShortcutAppearance  */
		public $appearance;

		public $isPhone;

		public $relativePath;
		public $relativeLink;

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

			$this->useIcon = true;
			$this->iconClass = 'default-icon';
			$queryResult = $xpath->query('//Config/' . $visualSettingsSubSection . 'Icon');
			if ($queryResult->length == 0)
				$queryResult = $xpath->query('//Config/Regular/Icon');
			if ($queryResult->length == 0)
				$queryResult = $xpath->query('//Config/Icon');
			if ($queryResult->length > 0)
			{
				$iconValue = trim($queryResult->item(0)->nodeValue);
				if (strpos($iconValue, '.png') !== false)
					$this->useIcon = false;
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

			$excludedUsers = array();
			$queryResult = $xpath->query('//Config/ExcludedUsers/User');
			foreach ($queryResult as $groupNode)
				$excludedUsers[] = trim($groupNode->nodeValue);

			$approvedGroups = array();
			$queryResult = $xpath->query('//Config/ApprovedGroups/Group');
			foreach ($queryResult as $groupNode)
				$approvedGroups[] = trim($groupNode->nodeValue);

			$excludedGroups = array();
			$queryResult = $xpath->query('//Config/ExcludedGroups/Group');
			foreach ($queryResult as $groupNode)
				$excludedGroups[] = trim($groupNode->nodeValue);

			if(UserIdentity::isUserAuthorized())
			{
				$user = Yii::app()->user;
				$userGroups = UserIdentity::getCurrentUserGroups();

				if (isset($user) && count($excludedUsers) > 0)
					$this->isAccessGranted &= !in_array($user->login, $excludedUsers);
				if (isset($user) && count($excludedGroups) > 0)
					$this->isAccessGranted &= !array_intersect($userGroups, $excludedGroups);

				if ($this->isAccessGranted && (count($approvedUsers) > 0 || count($approvedGroups) > 0))
				{
					$this->isAccessGranted = false;
					if (isset($user))
					{
						$this->isAccessGranted |= in_array($user->login, $approvedUsers);
						if (count($userGroups) > 0)
							$this->isAccessGranted |= array_intersect($userGroups, $approvedGroups);
					}
				}
			}

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
			$result .= '<div class="activity-data">' . CJSON::encode($this->getActivityData()) . '</div>';
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
		 * @return array
		 */
		public function getActivityData()
		{
			return array(
				'action' => $this->getTypeForActivityTracker(),
				'details' => array(
					'File' => $this->getTitleForActivityTracker()
				),
			);
		}

		/**
		 * @return string
		 */
		public abstract function getTypeForActivityTracker();

		/**
		 * @return string
		 */
		public function getTitleForActivityTracker()
		{
			return isset($this->title) && $this->title != '' ?
				$this->title :
				(isset($this->headerTitle) && $this->headerTitle != '' ? $this->headerTitle : $this->description);
		}
	}