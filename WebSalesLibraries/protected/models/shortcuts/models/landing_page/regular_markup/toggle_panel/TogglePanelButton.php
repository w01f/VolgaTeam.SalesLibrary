<?
	namespace application\models\shortcuts\models\landing_page\regular_markup\toggle_panel;

	use application\models\shortcuts\models\landing_page\regular_markup\style\TextAppearance;

	/**
	 * Class TogglePanelButton
	 */
	class TogglePanelButton
	{
		public $id;
		public $tag;
		public $title;
		public $isDefault;
		public $isAccessGranted;

		/** @var  TextAppearance */
		public $textAppearance;

		public function __construct()
		{
			$this->id = uniqid();
			$this->isDefault = false;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			$queryResult = $xpath->query('./Tag', $contextNode);
			$this->tag = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./Title', $contextNode);
			$this->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./IsDefault', $contextNode);
			$this->isDefault = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->isDefault;

			$queryResult = $xpath->query('./TextStyle', $contextNode);
			if ($queryResult->length > 0)
				$this->textAppearance = TextAppearance::fromXml($xpath, $queryResult->item(0));

			$user = \Yii::app()->user;
			$userGroups = \UserIdentity::getCurrentUserGroups();

			$approvedUsers = array();
			$queryResult = $xpath->query('./ApprovedUsers/User', $contextNode);
			foreach ($queryResult as $groupNode)
				$approvedUsers[] = trim($groupNode->nodeValue);

			$excludedUsers = array();
			$queryResult = $xpath->query('./ExcludedUsers/User', $contextNode);
			foreach ($queryResult as $groupNode)
				$excludedUsers[] = trim($groupNode->nodeValue);

			$approvedGroups = array();
			$queryResult = $xpath->query('./ApprovedGroups/Group', $contextNode);
			foreach ($queryResult as $groupNode)
				$approvedGroups[] = trim($groupNode->nodeValue);

			$excludedGroups = array();
			$queryResult = $xpath->query('./ExcludedGroups/Group', $contextNode);
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