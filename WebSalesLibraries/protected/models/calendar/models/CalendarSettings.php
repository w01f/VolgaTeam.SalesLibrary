<?

	namespace application\models\calendar\models;


	class CalendarSettings
	{
		const ViewTypeMonth = 'month';
		const ViewTypeWeek = 'agendaWeek';
		const ViewTypeDay = 'agendaDay';
		const ViewTypeList = 'listWeek';

		public $defaultView;
		public $defaultDate;
		public $allowEdit;

		public function __construct()
		{
			$this->defaultView = self::ViewTypeMonth;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return CalendarSettings
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = new self();

			$queryResult = $xpath->query('./DefaultView', $contextNode);
			if ($queryResult->length > 0)
				switch (trim($queryResult->item(0)->nodeValue))
				{
					case 'month':
						$instance->defaultView = self::ViewTypeMonth;
						break;
					case 'week':
						$instance->defaultView = self::ViewTypeWeek;
						break;
					case 'day':
						$instance->defaultView = self::ViewTypeDay;
						break;
					case 'list':
						$instance->defaultView = self::ViewTypeList;
						break;
				}

			$queryResult = $xpath->query('./DefaultDate', $contextNode);
			$instance->defaultDate = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : date("Y-m-d");

			$instance->allowEdit = false;

			$approvedUsers = array();
			$queryResult = $xpath->query('./AllowEdit/Users/User', $contextNode);
			foreach ($queryResult as $groupNode)
				$approvedUsers[] = trim($groupNode->nodeValue);

			$approvedGroups = array();
			$queryResult = $xpath->query('./AllowEdit/Groups/Group', $contextNode);
			foreach ($queryResult as $groupNode)
				$approvedGroups[] = trim($groupNode->nodeValue);

			if (\UserIdentity::isUserAuthorized())
			{
				$userLogin = \UserIdentity::getCurrentUserLogin();
				$userGroups = \UserIdentity::getCurrentUserGroups();

				if (count($approvedUsers) > 0 || count($approvedGroups) > 0)
				{
					if (!empty($userLogin))
					{
						$instance->allowEdit = $instance->allowEdit || in_array($userLogin, $approvedUsers);
						if (!$instance->allowEdit && count($userGroups) > 0)
							$instance->allowEdit = $instance->allowEdit || array_intersect($userGroups, $approvedGroups);
					}
				}
			}

			return $instance;
		}
	}