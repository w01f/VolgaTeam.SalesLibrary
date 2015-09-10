<?

	/**
	 * Class FileManagerDataController
	 */
	class FileManagerDataController extends LocalAppDataController
	{
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
						'Category' => 'Category',
						'SuperFilter' => 'SuperFilter',
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
					$sortHelper = new ObjectSortHelper('firstName','asc');
					usort($userList, array($sortHelper, 'sort'));
					$group->users = $userList;

					$group->libraryIds = GroupLibraryRecord::getLibraryIdsByGroup($groupRecord->id);
					$groups[] = $group;
				}
			}

			Yii::app()->cacheDB->flush();
			$sortHelper = new ObjectSortHelper('name','asc');
			usort($groups, array($sortHelper, 'sort'));
			return $groups;
		}

		/**
		 * @param string $sessionKey
		 * @return Category[]
		 * @soap
		 */
		public function getCategories($sessionKey)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$categoryManager = new CategoryManager();
				$categoryManager->loadCategories();
				return $categoryManager->categories;
			}
			return null;
		}

		/**
		 * @param string $sessionKey
		 * @return SuperFilter[]
		 * @soap
		 */
		public function getSuperFilters($sessionKey)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$categoryManager = new CategoryManager();
				$categoryManager->loadCategories();
				return $categoryManager->superFilters;
			}
			return null;
		}
	}