<?

	/**
	 * Class BaseNavigationItem
	 */
	abstract class BaseNavigationItem
	{
		public $id;
		public $type;

		/** @var  RegularNavigationItemSettings|MobileNavigationItemSettings */
		public $settings;

		public $contentView;

		/**
		 * @param $parent NavigationPanel
		 * @param $xpath DOMXPath
		 * @param $contextNode DOMNode
		 * @param $imagePath string
		 * @param $isPhone boolean
		 */
		public function __construct($parent, $xpath, $contextNode, $imagePath, $isPhone)
		{
			$this->id = uniqid();

			$settingsSubSection = $isPhone ? 'Mobile' : 'Regular';
			$queryResult = $xpath->query('./' . $settingsSubSection, $contextNode);
			if ($queryResult->length == 0)
				$settingsNode = $contextNode;
			else
				$settingsNode = $queryResult->item(0);
			if ($isPhone)
				$this->settings = new MobileNavigationItemSettings($parent, $xpath, $settingsNode, $imagePath);
			else
				$this->settings = new RegularNavigationItemSettings($parent, $xpath, $settingsNode, $imagePath);

			if ($this->settings->enabled && \UserIdentity::isUserAuthorized())
			{
				$user = \Yii::app()->user;
				$userGroups = \UserIdentity::getCurrentUserGroups();

				$approvedUsers = array();
				$queryResult = $xpath->query('./ApprovedUsers/User', $contextNode);
				foreach ($queryResult as $groupNode)
					$approvedUsers[] = trim($groupNode->nodeValue);

				$approvedGroups = array();
				$queryResult = $xpath->query('./ApprovedGroups/Group', $contextNode);
				foreach ($queryResult as $groupNode)
					$approvedGroups[] = trim($groupNode->nodeValue);

				$excludedUsers = array();
				$queryResult = $xpath->query('./ExcludedUsers/User', $contextNode);
				foreach ($queryResult as $groupNode)
					$excludedUsers[] = trim($groupNode->nodeValue);

				$excludedGroups = array();
				$queryResult = $xpath->query('./ExcludedGroups/Group', $contextNode);
				foreach ($queryResult as $groupNode)
					$excludedGroups[] = trim($groupNode->nodeValue);

				$isAccessGranted = true;

				if (isset($user) && count($excludedUsers) > 0)
					$isAccessGranted &= !in_array($user->login, $excludedUsers);
				if (isset($user) && count($excludedGroups) > 0)
					$isAccessGranted &= !array_intersect($userGroups, $excludedGroups);

				if ($isAccessGranted && (count($approvedUsers) > 0 || count($approvedGroups) > 0))
				{
					$isAccessGranted = false;
					if (isset($user) && isset($user->login))
					{
						$isAccessGranted |= in_array($user->login, $approvedUsers);
						if (count($userGroups) > 0)
							$isAccessGranted |= array_intersect($userGroups, $approvedGroups);
					}
				}

				$this->settings->enabled &= $isAccessGranted;
			}
		}

		/** @return string */
		public abstract function getUrl();

		/** @return string */
		public abstract function getTarget();

		/** @return string */
		public abstract function getItemData();

		/**
		 * @param $parent NavigationPanel
		 * @param $xpath DOMXPath
		 * @param $contextNode DOMNode
		 * @param $imagePath string
		 * @param $isPhone boolean
		 * @return BaseNavigationItem
		 */
		public static function fromXml($parent, $xpath, $contextNode, $imagePath, $isPhone)
		{
			$queryResult = $xpath->query('Type', $contextNode);
			$itemType = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';
			switch ($itemType)
			{
				case 'shortcut':
					$item = new ShortcutNavigationItem($parent, $xpath, $contextNode, $imagePath, $isPhone);
					break;
				case 'shortcut-group':
					if ($isPhone)
						$item = new ShortcutGroupNavigationItem($parent, $xpath, $contextNode, $imagePath, $isPhone);
					else
						$item = null;
					break;
				case 'url':
					$item = new UrlNavigationItem($parent, $xpath, $contextNode, $imagePath, $isPhone);
					break;
				default:
					$item = null;
			}
			return isset($item) && $item->settings->enabled ? $item : null;
		}
	}