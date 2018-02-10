<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\menu_stripe;

	use application\models\shortcuts\models\landing_page\regular_markup\style\TextAppearance;

	/**
	 * Class MenuStripeItem
	 */
	abstract class MenuStripeItem
	{
		/** @var IParentMenu  */
		public $parentMenu;

		public $id;
		public $type;
		public $title;
		public $enable;
		public $isAccessGranted;

		/** @var  TextAppearance */
		public $textAppearance;

		/** @var  \HideCondition */
		public $hideCondition;

		/**
		 * @param $parentMenu IParentMenu
		 */
		protected function __construct($parentMenu)
		{
			$this->id = uniqid();
			$this->parentMenu = $parentMenu;
			$this->enable = true;
			$this->isAccessGranted = true;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			$queryResult = $xpath->query('./Text', $contextNode);
			$this->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./Enabled', $contextNode);
			$this->enable = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->enable;

			$queryResult = $xpath->query('./TextStyle', $contextNode);
			if ($queryResult->length > 0)
				$this->textAppearance = TextAppearance::fromXml($xpath, $queryResult->item(0));
			else
				$this->textAppearance = $this->parentMenu->getTextAppearance();

			$queryResult = $xpath->query('./Hide', $contextNode);
			if ($queryResult->length > 0)
				$this->hideCondition = \HideCondition::fromXml($xpath, $queryResult->item(0));
			else
				$this->hideCondition = new \HideCondition();

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