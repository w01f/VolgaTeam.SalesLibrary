<?

	namespace application\models\calendar\models;


	use application\models\shortcuts\models\landing_page\regular_markup\style\TextAppearance;

	class CalendarSettings
	{
		const ViewTypeMonth = 'month';
		const ViewTypeWeek = 'agendaWeek';
		const ViewTypeDay = 'agendaDay';
		const ViewTypeList = 'listWeek';

		public $defaultView;
		public $defaultDate;
		public $allowEdit;
		public $disableWeekend;
		public $minTime;
		public $maxTime;

		/** @var array */
		public $viewToggles;

		/** @var array */
		public $hideLeftNavigationButtonsForViews;

		/** @var TextAppearance */
		public $headerStyle;

		/** @var NavigationButtonStyle */
		public $navigationButtonStyleLeft;

		/** @var NavigationButtonStyle */
		public $navigationButtonStyleRight;

		/** @var EmailSettings */
		public $emailSettings;

		public function __construct()
		{
			$this->defaultView = self::ViewTypeMonth;
			$this->disableWeekend = false;
			$this->minTime = "00:00:00";
			$this->maxTime = "24:00:00";
			$this->viewToggles = array(self::ViewTypeMonth, self::ViewTypeWeek, self::ViewTypeDay, self::ViewTypeList);
			$this->hideLeftNavigationButtonsForViews =array();
			$this->headerStyle = TextAppearance::createEmpty();
			$this->navigationButtonStyleLeft = NavigationButtonStyle::createDefault();
			$this->navigationButtonStyleRight = NavigationButtonStyle::createDefault();
			$this->emailSettings = EmailSettings::createDefault();
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

			$queryResult = $xpath->query('./MinTime', $contextNode);
			$instance->minTime = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $instance->minTime;

			$queryResult = $xpath->query('./MaxTime', $contextNode);
			$instance->maxTime = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $instance->maxTime;

			$queryResult = $xpath->query('./DisableWeekend', $contextNode);
			$instance->disableWeekend = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $instance->disableWeekend;

			$queryResult = $xpath->query('./HeaderStyle', $contextNode);
			if ($queryResult->length > 0)
				$instance->headerStyle = TextAppearance::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./ButtonStyle/LeftSide', $contextNode);
			if ($queryResult->length > 0)
				$instance->navigationButtonStyleLeft = NavigationButtonStyle::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./ButtonStyle/RightSide', $contextNode);
			if ($queryResult->length > 0)
				$instance->navigationButtonStyleRight = NavigationButtonStyle::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./EmailSettings', $contextNode);
			if ($queryResult->length > 0)
				$instance->emailSettings = EmailSettings::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./HideLeftButtonsFor/View', $contextNode);
			foreach ($queryResult as $groupNode)
				switch (trim($groupNode->nodeValue))
				{
					case 'month':
						$instance->hideLeftNavigationButtonsForViews[] = self::ViewTypeMonth;
						break;
					case 'week':
						$instance->hideLeftNavigationButtonsForViews[] = self::ViewTypeWeek;
						break;
					case 'day':
						$instance->hideLeftNavigationButtonsForViews[] = self::ViewTypeDay;
						break;
					case 'list':
						$instance->hideLeftNavigationButtonsForViews[] = self::ViewTypeList;
						break;
				}

			$queryResult = $xpath->query('./ViewToggles/View', $contextNode);
			if ($queryResult->length > 0)
			{
				$instance->viewToggles = array();
				foreach ($queryResult as $groupNode)
					switch (trim($groupNode->nodeValue))
					{
						case 'month':
							$instance->viewToggles[] = self::ViewTypeMonth;
							break;
						case 'week':
							$instance->viewToggles[] = self::ViewTypeWeek;
							break;
						case 'day':
							$instance->viewToggles[] = self::ViewTypeDay;
							break;
						case 'list':
							$instance->viewToggles[] = self::ViewTypeList;
							break;
					}
			}

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