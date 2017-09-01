<?

	/**
	 * Class ShortcutGroup
	 */
	class ShortcutGroup
	{
		public $id;
		public $title;
		public $order;
		public $enabled;

		public $useIcon;
		public $iconClass;
		public $imageContent;

		public $defaultItemAppearance;

		public $groupAppearance;

		public $isAccessGranted;

		/** @var array MenuItem[] */
		public $menuItems;

		/**
		 * @param $groupRecord ShortcutGroupRecord
		 * @param $currentSuperGroupTag string
		 * @param $isPhone boolean
		 */
		public function __construct($groupRecord, $currentSuperGroupTag, $isPhone)
		{
			$groupConfig = new DOMDocument();
			$groupConfig->loadXML($groupRecord->config);
			$xpath = new DomXPath($groupConfig);

			$this->id = $groupRecord->id;
			$this->order = $groupRecord->order;

			$visualSettingsSubSection = $isPhone ? 'Mobile' : 'Regular';
			$queryResult = $xpath->query('//Config/' . $visualSettingsSubSection);
			if ($queryResult->length == 0)
				$visualSettingsSubSection = '';
			else
				$visualSettingsSubSection .= '/';

			$queryResult = $xpath->query('//Config/' . $visualSettingsSubSection . 'Enabled');
			$this->enabled = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;

			$queryResult = $xpath->query('//Config/' . $visualSettingsSubSection . 'Name');
			$this->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';

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
					$imagePath = $groupRecord->source_path . DIRECTORY_SEPARATOR . $iconValue;
					if (file_exists($imagePath))
					{
						$data = file_get_contents($imagePath);
						$this->imageContent = 'data:image/png;base64,' . base64_encode($data);
					}
				}
			}

			$queryResult = $xpath->query('//Config/' . $visualSettingsSubSection . 'GroupAppearance');
			if ($queryResult->length > 0)
				$this->groupAppearance = ShortcutAppearance::fromXml($xpath, $queryResult->item(0));
			else
			{
				$this->groupAppearance = new ShortcutAppearance();
				$this->groupAppearance->textColor = '1E90FF';
				$this->groupAppearance->iconColor = '1E90FF';
			}

			$queryResult = $xpath->query('//Config/' . $visualSettingsSubSection . 'ItemAppearance');
			if ($queryResult->length > 0)
				$this->defaultItemAppearance = ShortcutAppearance::fromXml($xpath, $queryResult->item(0));
			else
			{
				$this->defaultItemAppearance = new ShortcutAppearance();
				$this->defaultItemAppearance->size = 'regular';
				$this->defaultItemAppearance->textAlign = 'center';
				$this->defaultItemAppearance->backColor = Yii::app()->params['menu']['BarColor'];
				$this->defaultItemAppearance->textColor = 'FFFFFF';
				$this->defaultItemAppearance->iconColor = 'FFFFFF';
				$this->defaultItemAppearance->shadowColor = 'FFFFFF';
				$this->defaultItemAppearance->useGradient = false;
			}

			$this->isAccessGranted = UserIdentity::isUserAuthorized();
			$isAdmin = UserIdentity::isUserAdmin();
			if(!$isAdmin)
			{
				$queryResult = $xpath->query('//Config/SuperGroups/Tag');
				if (isset($currentSuperGroupTag) && $queryResult->length > 0)
				{
					$this->isAccessGranted = false;
					foreach ($queryResult as $groupNode)
					{
						$superGroupTag = trim($groupNode->nodeValue);
						if ($superGroupTag == $currentSuperGroupTag)
						{
							$this->isAccessGranted = true;
							break;
						}
					}
				}

				if ($this->isAccessGranted)
				{
					$user = Yii::app()->user;
					$userGroups = UserIdentity::getCurrentUserGroups();

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

					$this->isAccessGranted = true;

					if (isset($user) && count($excludedUsers) > 0)
						$this->isAccessGranted &= !in_array($user->login, $excludedUsers);
					if (isset($user) && count($excludedGroups) > 0)
						$this->isAccessGranted &= !array_intersect($userGroups, $excludedGroups);

					if ($this->isAccessGranted && (count($approvedUsers) > 0 || count($approvedGroups) > 0))
					{
						$this->isAccessGranted = false;
						if (isset($user) && isset($user->login))
						{
							$this->isAccessGranted |= in_array($user->login, $approvedUsers);
							if (count($userGroups) > 0)
								$this->isAccessGranted |= array_intersect($userGroups, $approvedGroups);
						}
					}
				}
			}

			if ($this->isAccessGranted)
			{
				$this->menuItems = array();
				$shortcuts = $groupRecord->getTopLevelLinks($isPhone);
				foreach ($shortcuts as $shortcut)
				{
					if ($shortcut->enabled)
						$this->menuItems[] = new MenuItem($shortcut, $this);
				}
			}
		}

		/** @return string */
		public function getUrl()
		{
			return Yii::app()->createAbsoluteUrl('shortcuts/getGroupContent', array('groupId' => $this->id));
		}

		/**
		 * @return string
		 */
		public function getGroupData()
		{
			$result = '';
			$result .= '<div class="activity-data">' . CJSON::encode(array(
					'title' => $this->title)) . '</div>';
			return $result;
		}
	}