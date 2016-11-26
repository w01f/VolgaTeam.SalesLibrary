<?
	use application\models\wallbin\models\web\Category as Category;
	use application\models\wallbin\models\web\SuperFilter as SuperFilter;

	/**
	 * Class FileManagerDataController
	 */
	class FileManagerDataController extends LocalAppDataController
	{
		/** return boolean */
		protected function getIsPublicController()
		{
			return true;
		}

		/**
		 * @return array
		 */
		public function actions()
		{
			return array(
				'quote' => array(
					'class' => 'CWebServiceAction',
					'classMap' => array(
						'GroupModel' => 'GroupModel',
						'SoapCategory' => 'SoapCategory',
						'SoapSuperFilter' => 'SoapSuperFilter',
					),
				),
			);
		}

		/**
		 * @param string $sessionKey
		 * @return GroupModel[]
		 * @soap
		 */
		public function getSecurityGroups($sessionKey)
		{
			$groups = array();
			if ($this->authenticateBySession($sessionKey))
			{
				foreach (GroupRecord::model()->findAll() as $groupRecord)
				{
					/** @var $groupRecord GroupRecord */
					$group = new GroupModel();
					$group->id = $groupRecord->id;
					$group->name = $groupRecord->name;

					$userList = array();
					$assignedUsers = UserGroupRecord::getUserIdsByGroup($groupRecord->id);
					$totalUsers = UserRecord::model()->count('role<>2');
					$group->allUsers = isset($assignedUsers) && $totalUsers == count($assignedUsers);
					foreach ($assignedUsers as $userId)
					{
						/** @var $userRecord UserRecord */
						$userRecord = UserRecord::model()->findByPk($userId);
						if (isset($userRecord))
						{
							$user = new UserModel();
							$user->id = $userRecord->id;
							$user->login = $userRecord->login;
							$user->firstName = $userRecord->first_name;
							$user->lastName = $userRecord->last_name;
							$user->email = $userRecord->email;
							$userList[] = $user;
						}
					}
					$sortHelper = new ObjectSortHelper('firstName', 'asc');
					usort($userList, array($sortHelper, 'sort'));
					$group->users = $userList;

					$group->libraryIds = GroupLibraryRecord::getLibraryIdsByGroup($groupRecord->id);
					$groups[] = $group;
				}
			}

			Yii::app()->cacheDB->flush();
			$sortHelper = new ObjectSortHelper('name', 'asc');
			usort($groups, array($sortHelper, 'sort'));
			return $groups;
		}

		/**
		 * @param string $sessionKey
		 * @return SoapCategory[]
		 * @soap
		 */
		public function getCategories($sessionKey)
		{
			$soapCategories = array();
			if ($this->authenticateBySession($sessionKey))
			{
				$categoryManager = new CategoryManager();
				$categoryManager->loadCategories();
				foreach ($categoryManager->categories as $category)
					$soapCategories[] = SoapCategory::load($category);
			}
			return $soapCategories;
		}

		/**
		 * @param string $sessionKey
		 * @return SoapSuperFilter[]
		 * @soap
		 */
		public function getSuperFilters($sessionKey)
		{
			$soapSuperFilters = array();
			if ($this->authenticateBySession($sessionKey))
			{
				$categoryManager = new CategoryManager();
				$categoryManager->loadCategories();

				foreach ($categoryManager->superFilters as $superFilter)
					$soapSuperFilters[] = SoapSuperFilter::load($superFilter);
			}
			return $soapSuperFilters;
		}
	}