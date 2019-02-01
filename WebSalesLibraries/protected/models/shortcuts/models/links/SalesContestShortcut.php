<?

	/**
	 * Class SalesContestShortcut
	 */
	class SalesContestShortcut extends PageContentShortcut
	{
		public $selectedItemId;
		public $isAdminRole;
		public $showAllItemsTab;
		public $showArchiveTab;

		public $fileStorageRootPath;
		public $maxFileSize;
		public $maxFileSizeExcessMessage;

		/** @var  \application\models\sales_contest\models\ArchiveSettings */
		public $archiveSettings;

		/**
		 * @param ShortcutLinkRecord $linkRecord
		 * @param $isPhone boolean
		 * @param $parameters array
		 */
		public function __construct($linkRecord, $isPhone, $parameters = null)
		{
			parent::__construct($linkRecord, $isPhone);

			$this->archiveSettings = new \application\models\sales_contest\models\ArchiveSettings();

			if (isset($parameters) && array_key_exists('itemId', $parameters))
				$this->selectedItemId = $parameters['itemId'];
		}

		public function loadPageConfig()
		{
			parent::loadPageConfig();

			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);
			$xpath = new DomXPath($linkConfig);

			$queryResult = $xpath->query('//Config/DropZone/MaxSize');
			$this->maxFileSize = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 256;

			$queryResult = $xpath->query('//Config/DropZone/MaxSizeMessage');
			$this->maxFileSizeExcessMessage = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('//Config/DropZone/Storage');
			if ($queryResult->length > 0)
				$this->fileStorageRootPath = trim($queryResult->item(0)->nodeValue);

			$queryResult = $xpath->query('//Config/ArchiveAfter');
			if ($queryResult->length > 0)
				$this->archiveSettings = \application\models\sales_contest\models\ArchiveSettings::fromXml($xpath, $queryResult->item(0));

			$this->isAdminRole = false;
			$this->showAllItemsTab = false;
			$this->showArchiveTab = false;

			if (UserIdentity::isUserAuthorized())
			{
				$userLogin = UserIdentity::getCurrentUserLogin();
				$userGroups = UserIdentity::getCurrentUserGroups();

				$adminUsers = array();
				$queryResult = $xpath->query('//Config/AdminEdit/ApprovedUsers/User');
				foreach ($queryResult as $groupNode)
					$adminUsers[] = trim($groupNode->nodeValue);

				$adminGroups = array();
				$queryResult = $xpath->query('//Config/AdminEdit/ApprovedGroups/Group');
				foreach ($queryResult as $groupNode)
					$adminGroups[] = trim($groupNode->nodeValue);

				if (count($adminUsers) > 0 || count($adminGroups) > 0)
				{
					if (!empty($userLogin))
					{
						$this->isAdminRole = $this->isAdminRole || in_array($userLogin, $adminUsers);
						if (count($userGroups) > 0)
							$this->isAdminRole = $this->isAdminRole || array_intersect($userGroups, $adminGroups);
					}
				}

				$allItemsAvailableUsers = array();
				$queryResult = $xpath->query('//Config/AllNominationsTab/ApprovedUsers/User');
				foreach ($queryResult as $groupNode)
					$allItemsAvailableUsers[] = trim($groupNode->nodeValue);

				$allItemsAvailableGroups = array();
				$queryResult = $xpath->query('//Config/AllNominationsTab/ApprovedGroups/Group');
				foreach ($queryResult as $groupNode)
					$allItemsAvailableGroups[] = trim($groupNode->nodeValue);

				if (count($allItemsAvailableUsers) > 0 || count($allItemsAvailableGroups) > 0)
				{
					if (!empty($userLogin))
					{
						$this->showAllItemsTab = $this->showAllItemsTab || in_array($userLogin, $allItemsAvailableUsers);
						if (count($userGroups) > 0)
							$this->showAllItemsTab = $this->showAllItemsTab || array_intersect($userGroups, $allItemsAvailableGroups);
					}
				}

				$archiveItemsAvailableUsers = array();
				$queryResult = $xpath->query('//Config/ArchivesTab/ApprovedUsers/User');
				foreach ($queryResult as $groupNode)
					$archiveItemsAvailableUsers[] = trim($groupNode->nodeValue);

				$archiveItemsAvailableGroups = array();
				$queryResult = $xpath->query('//Config/ArchivesTab/ApprovedGroups/Group');
				foreach ($queryResult as $groupNode)
					$archiveItemsAvailableGroups[] = trim($groupNode->nodeValue);

				if (count($archiveItemsAvailableUsers) > 0 || count($archiveItemsAvailableGroups) > 0)
				{
					if (!empty($userLogin))
					{
						$this->showArchiveTab = $this->showArchiveTab || in_array($userLogin, $archiveItemsAvailableUsers);
						if (count($userGroups) > 0)
							$this->showArchiveTab = $this->showArchiveTab || array_intersect($userGroups, $archiveItemsAvailableGroups);
					}
				}
			}
		}

		/**
		 * @return string[]
		 */
		public function getSubmitEmailRecipients()
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);
			$xpath = new DomXPath($linkConfig);

			$userEmails = array();

			$queryResult = $xpath->query('//Config/EmailOnSubmit/ApprovedGroups/Group');
			foreach ($queryResult as $groupNode)
			{
				$groupName = trim($groupNode->nodeValue);
				/** @var $groupRecord \GroupRecord */
				$groupRecord = GroupRecord::model()->find('name=?', array($groupName));
				if (isset($groupRecord))
				{
					$userIds = UserGroupRecord::getUserIdsByGroup($groupRecord->id);
					foreach ($userIds as $userId)
					{
						/** @var $userRecord \UserRecord */
						$userRecord = UserRecord::model()->findByPk($userId);
						if (isset($userRecord) && !empty($userRecord->email) && !in_array($userRecord->email, $userEmails))
							$userEmails[] = $userRecord->email;
					}
				}
			}

			$queryResult = $xpath->query('//Config/EmailOnSubmit/ApprovedUsers/User');
			foreach ($queryResult as $userNode)
			{
				$userLogin = trim($userNode->nodeValue);
				/** @var $userRecord \UserRecord */
				$userRecord = UserRecord::model()->find('login=?', array($userLogin));
				if (isset($userRecord) && !empty($userRecord->email) && !in_array($userRecord->email, $userEmails))
					$userEmails[] = $userRecord->email;
			}

			return $userEmails;
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Sales Contest';
		}

		/**
		 * @return array
		 */
		public function getPageData()
		{
			$data = parent::getPageData();
			if (!empty($this->selectedItemId))
			{
				$data['selectedItemId'] = $this->selectedItemId;
			}
			else
			{
				$items = SalesContestItemRecord::model()->getListItems(SalesContestItemRecord::ListTypeOwn, $this->id);
				$data['selectedItemId'] = count($items) > 0 ? $items[0]->id : null;
			}
			$data['isAdminRole'] = $this->isAdminRole;
			$data['maxFileSize'] = $this->maxFileSize;
			$data['maxFileSizeExcessMessage'] = $this->maxFileSizeExcessMessage;
			$data['serviceData'] = $this->getMenuItemData();
			return $data;
		}

		/**
		 * @return array
		 */
		public function getShortcutCustomParameters()
		{
			$customParameters = parent::getShortcutCustomParameters();
			$customParameters['itemId'] = $this->selectedItemId;
			return $customParameters;
		}
	}