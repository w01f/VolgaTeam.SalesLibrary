<?

	/**
	 * Class AdminController
	 */
	class AdminController extends SoapController
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
						'LibraryViewModel' => 'LibraryViewModel',
						'LibraryPageViewModel' => 'LibraryPageViewModel',
						'UserViewModel' => 'UserViewModel',
						'GroupViewModel' => 'GroupViewModel',
						'SoapLibrary' => 'SoapLibrary',
						'SoapLibraryPage' => 'SoapLibraryPage',
						'UserEditModel' => 'UserEditModel',
						'GroupEditModel' => 'GroupEditModel',
					),
				),
			);
		}

		/**
		 * @param string $sessionKey
		 * @param string $login
		 * @param string $password
		 * @param string $firstName
		 * @param string $lastName
		 * @param string $email
		 * @param string $phone
		 * @param GroupViewModel[] $assignedGroups
		 * @param LibraryPageViewModel[] $assignedPages
		 * @param int $role
		 * @param boolean $sendInfoMessage
		 * @soap
		 */
		public function setUser($sessionKey, $login, $password, $firstName, $lastName, $email, $phone, $assignedGroups, $assignedPages, $role, $sendInfoMessage)
		{
			$newUser = false;
			$resetPassword = false;
			if ($this->authenticateBySession($sessionKey))
			{
				/** @var  $user UserRecord */
				$user = UserRecord::model()->find('LOWER(login)=?', array(strtolower($login)));
				if (!isset($user))
				{
					$user = new UserRecord();
					$user->login = $login;
					$user->date_add = date(Yii::app()->params['mysqlDateTimeFormat']);
					$newUser = TRUE;
				}
				else
					$user->date_modify = date(Yii::app()->params['mysqlDateTimeFormat']);
				$user->first_name = $firstName;
				$user->last_name = $lastName;
				$user->email = $email;
				$user->phone = $phone;
				$user->role = $role;

				if ($password !== '')
				{
					$user->password = md5($password);
					$resetPassword = true;
				}


				$user->save();

				if (isset($assignedGroups) && count($assignedGroups) > 0)
					UserGroupRecord::assignGroupsForUser($login, $assignedGroups);

				if (isset($assignedPages) && count($assignedPages) > 0)
					UserLibraryRecord::assignPagesForUser($login, $assignedPages);

				if ($resetPassword)
					ResetPasswordRecord::resetPasswordForUser(
						$login,
						$password,
						$sendInfoMessage,
						$newUser ? Yii::app()->params['email']['new_user']['subject'] : ('Password Reset for ' . Yii::app()->getBaseUrl(true)),
						$newUser ? 'newUser' : 'existedUser');

				MetaDataRecord::setData('library-security', 'last-update', date(Yii::app()->params['sourceDateFormat']));
			}
		}

		/**
		 * @param string $sessionKey
		 * @param string $login
		 * @soap
		 */
		public function deleteUser($sessionKey, $login)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				UserRecord::deleteUserByLogin($login);
				MetaDataRecord::setData('library-security', 'last-update', date(Yii::app()->params['sourceDateFormat']));
			}
		}

		/**
		 * @param string $sessionKey
		 * @return UserViewModel[]
		 * @soap
		 */
		public function getUsers($sessionKey)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$users = array();

				$dbCommand = \Yii::app()->db->createCommand();
				$dbCommand = $dbCommand->from('tbl_user us');
				$dbCommand = $dbCommand->select(array(
					'user_id' => 'us.id as user_id',
					'user_login' => 'us.login as user_login',
					'user_first_name' => 'us.first_name as user_first_name',
					'user_last_name' => 'us.last_name as user_last_name',
					'user_email' => 'us.email as user_email',
					'user_phone' => 'us.phone as user_phone',
					'user_role' => 'us.role as user_role',
					'user_date_add' => 'us.date_add as user_date_add',
					'user_date_modify' => 'us.date_modify as user_date_modify',
					'assigned_groups' => 'group_concat(distinct gr.name separator \',\') as assigned_groups',
					'assigned_libraries' => 'group_concat(distinct lib.name separator \',\') as assigned_libraries'
				));
				$dbCommand = $dbCommand->leftJoin('tbl_user_group us_gr', 'us_gr.id_user = us.id');
				$dbCommand = $dbCommand->leftJoin('tbl_group gr', 'gr.id = us_gr.id_group');
				$dbCommand = $dbCommand->leftJoin('tbl_user_library us_lib', 'us_lib.id_page = us.id');
				$dbCommand = $dbCommand->leftJoin('tbl_library lib', 'lib.id = us_lib.id_library');

				$dbCommand = $dbCommand->where('us.role<>2');

				$dbCommand = $dbCommand->group(array('us.id',
					'us.login',
					'us.first_name',
					'us.last_name',
					'us.email',
					'us.phone',
					'us.role',
					'us.date_add',
					'us.date_modify'));
				$dbCommand = $dbCommand->order('us.first_name, us.last_name');
				$resultRecords = $dbCommand->queryAll();

				foreach ($resultRecords as $resultRecord)
				{
					$user = new UserViewModel();
					$user->id = $resultRecord['user_id'];
					$user->login = $resultRecord['user_login'];
					$user->firstName = $resultRecord['user_first_name'];
					$user->lastName = $resultRecord['user_last_name'];
					$user->email = $resultRecord['user_email'];
					$user->phone = $resultRecord['user_phone'];
					$user->role = $resultRecord['user_role'];
					$user->dateAdd = $resultRecord['user_date_add'];
					$user->dateModify = $resultRecord['user_date_modify'];

					$user->assignedLibraries = array();
					if (!empty($resultRecord['assigned_libraries']))
						$user->assignedLibraries = explode(',', $resultRecord['assigned_libraries']);

					$user->assignedGroups = array();
					if (!empty($resultRecord['assigned_groups']))
						$user->assignedGroups = explode(',', $resultRecord['assigned_groups']);

					$users[] = $user;
				}
				return $users;
			}
			return null;
		}

		/**
		 * @param string $sessionKey
		 * @param int $userId
		 * @return UserEditModel
		 * @soap
		 */
		public function getUser($sessionKey, $userId)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				/** @var $userRecord UserRecord */
				$userRecord = UserRecord::model()->findByPk($userId);

				$user = new UserEditModel();
				$user->id = $userRecord->id;
				$user->login = $userRecord->login;
				$user->firstName = $userRecord->first_name;
				$user->lastName = $userRecord->last_name;
				$user->email = $userRecord->email;
				$user->phone = $userRecord->phone;
				$user->role = $userRecord->role;
				$user->dateAdd = $userRecord->date_add;
				$user->dateModify = $userRecord->date_modify;

				$assignedLibraryIds = UserLibraryRecord::getLibraryIdsByUser($userRecord->id);
				$assignedPageIds = UserLibraryRecord::getPageIdsByUser($userRecord->id);
				foreach ($assignedLibraryIds as $libraryId)
				{
					/** @var $libraryRecord LibraryRecord */
					$libraryRecord = LibraryRecord::model()->findByPk($libraryId);
					if (isset($libraryRecord))
					{
						$library = new LibraryViewModel();
						$library->id = $libraryRecord->id;
						$library->name = $libraryRecord->name;
						/** @var $pageRecords LibraryPageRecord[] */
						$pageRecords = LibraryPageRecord::model()->findAll("id_library=? and id in ('" . implode("','", $assignedPageIds) . "')", array($libraryRecord->id));
						foreach ($pageRecords as $pageRecord)
						{
							$page = new LibraryPageViewModel();
							$page->id = $pageRecord->id;
							$page->libraryId = $pageRecord->id_library;
							$page->name = $pageRecord->name;
							$page->libraryName = $libraryRecord->name;
							$library->pages[] = $page;
						}
						$user->libraries[] = $library;
					}
				}

				$assignedGroups = UserGroupRecord::getGroupIdsByUser($userRecord->id);
				foreach ($assignedGroups as $groupId)
				{
					/** @var $groupRecord GroupRecord */
					$groupRecord = GroupRecord::model()->findByPk($groupId);
					if (isset($groupRecord))
					{
						$group = new GroupViewModel();
						$group->id = $groupRecord->id;
						$group->name = $groupRecord->name;
						$user->groups[] = $group;
					}
				}

				return $user;
			}
			return null;
		}

		/**
		 * @param string $sessionKey
		 * @return bool
		 * @soap
		 */
		public function isUserPasswordComplex($sessionKey)
		{
			if ($this->authenticateBySession($sessionKey))
				return Yii::app()->params['login']['complex_password'];
			return true;
		}

		/**
		 * @param string $sessionKey
		 * @param string $id
		 * @param string $name
		 * @param UserViewModel[] $assignedUsers
		 * @param LibraryPageViewModel[] $assignedPages
		 * @soap
		 */
		public function setGroup($sessionKey, $id, $name, $assignedUsers, $assignedPages)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$group = GroupRecord::model()->findByPk($id);
				if (!isset($group))
				{
					$group = new GroupRecord();
					$group->id = $id;
				}
				$group->name = $name;
				$group->save();

				UserGroupRecord::assignUsersForGroup($id, $assignedUsers);

				GroupLibraryRecord::assignPagesForGroup($id, $assignedPages);

				MetaDataRecord::setData('library-security', 'last-update', date(Yii::app()->params['sourceDateFormat']));
			}
		}

		/**
		 * @param string $sessionKey
		 * @param string $id
		 * @soap
		 */
		public function deleteGroup($sessionKey, $id)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				UserGroupRecord::clearObjectsByGroup($id);
				GroupLibraryRecord::clearObjectsByGroup($id);
				GroupRecord::model()->deleteByPk($id);

				MetaDataRecord::setData('library-security', 'last-update', date(Yii::app()->params['sourceDateFormat']));
			}
		}

		/**
		 * @param string $sessionKey
		 * @return GroupViewModel[]
		 * @soap
		 */
		public function getGroups($sessionKey)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$groups = array();

				$dbCommand = \Yii::app()->db->createCommand();
				$dbCommand = $dbCommand->from('tbl_group gr');
				$dbCommand = $dbCommand->select(array(
					'group_id' => 'gr.id as group_id',
					'group_name' => 'gr.name as group_name',
					'assigned_users' => 'group_concat(distinct us.login separator \',\') as assigned_users'
				));
				$dbCommand = $dbCommand->leftJoin('tbl_user_group us_gr', 'us_gr.id_group = gr.id');
				$dbCommand = $dbCommand->leftJoin('tbl_user us', 'us.id = us_gr.id_user');
				$dbCommand = $dbCommand->group(array('gr.id','gr.name'));
				$dbCommand = $dbCommand->order('gr.name');
				$resultRecords = $dbCommand->queryAll();

				foreach ($resultRecords as $resultRecord)
				{
					$group = new GroupViewModel();
					$group->id = $resultRecord['group_id'];
					$group->name = $resultRecord['group_name'];

					$group->assignedUsers = array();
					if (!empty($resultRecord['assigned_users']))
						$group->assignedUsers = explode(',', $resultRecord['assigned_users']);

//					$group->assignedLibraries = array();
//					$assignedLibraryIds = GroupLibraryRecord::getLibraryIdsByGroup($groupRecord->id);
//					$totalLibraries = LibraryRecord::model()->count();
//					$group->allLibraries = isset($assignedLibraryIds) && $totalLibraries == count($assignedLibraryIds);
//					foreach ($assignedLibraryIds as $libraryId)
//					{
//						/** @var $libraryRecord LibraryRecord */
//						$libraryRecord = LibraryRecord::model()->findByPk($libraryId);
//						if (isset($libraryRecord))
//							$group->assignedLibraries[] = $libraryRecord->name;
//					}

					$groups[] = $group;
				}
				return $groups;
			}
			return null;
		}

		/**
		 * @param string $sessionKey
		 * @param string $groupId
		 * @return GroupEditModel
		 * @soap
		 */
		public function getGroup($sessionKey, $groupId)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				/** @var $groupRecord GroupRecord */
				$groupRecord = GroupRecord::model()->findByPk($groupId);
				$group = new GroupEditModel();
				$group->id = $groupRecord->id;
				$group->name = $groupRecord->name;
				$group->libraries=array();

				$assignedLibraryIds = GroupLibraryRecord::getLibraryIdsByGroup($groupRecord->id);
				$assignedPageIds = GroupLibraryRecord::getPageIdsByGroup($groupRecord->id);
				foreach ($assignedLibraryIds as $libraryId)
				{
					/** @var $libraryRecord LibraryRecord */
					$libraryRecord = LibraryRecord::model()->findByPk($libraryId);
					if (isset($libraryRecord))
					{
						$library = new LibraryViewModel();
						$library->id = $libraryRecord->id;
						$library->name = $libraryRecord->name;
						$library->pages = array();
						/** @var $pageRecords LibraryPageRecord[] */
						$pageRecords = LibraryPageRecord::model()->findAll("id_library=? and id in ('" . implode("','", $assignedPageIds) . "')", array($libraryRecord->id));
						if (isset($pageRecords))
						{
							foreach ($pageRecords as $pageRecord)
							{
								$page = new LibraryPageViewModel();
								$page->id = $pageRecord->id;
								$page->libraryId = $pageRecord->id_library;
								$page->name = $pageRecord->name;
								$page->libraryName = $libraryRecord->name;
								$library->pages[] = $page;
							}
						}
						$group->libraries[] = $library;
					}
				}

				$assignedUsers = UserGroupRecord::getUserIdsByGroup($groupRecord->id);
				foreach ($assignedUsers as $userId)
				{
					/** @var $userRecord UserRecord */
					$userRecord = UserRecord::model()->findByPk($userId);
					if (isset($userRecord))
					{
						$user = new UserViewModel();
						$user->id = $userRecord->id;
						$user->login = $userRecord->login;
						$user->firstName = $userRecord->first_name;
						$user->lastName = $userRecord->last_name;
						$user->email = $userRecord->email;
						$user->dateAdd = $userRecord->date_add;
						$user->dateModify = $userRecord->date_modify;
						$group->users[] = $user;
					}
				}

				return $group;
			}
			return null;
		}

		/**
		 * @param string $sessionKey
		 * @param string $id
		 * @param UserViewModel[] $assignedUsers
		 * @param GroupViewModel[] $assignedGroups
		 * @soap
		 */
		public function setPage($sessionKey, $id, $assignedUsers, $assignedGroups)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				/** @var $pageRecord LibraryPageRecord */
				$pageRecord = LibraryPageRecord::model()->findByPk($id);
				if (isset($pageRecord))
				{
					$library = new SoapLibrary();
					$library->id = $pageRecord->id_library;
					$page = new SoapLibraryPage();
					$page->id = $pageRecord->id;
					$page->libraryId = $pageRecord->id_library;

					UserLibraryRecord::assignUsersForPage($page, $assignedUsers);
					GroupLibraryRecord::assignGroupsForPage($page, $assignedGroups);

					MetaDataRecord::setData('library-security', 'last-update', date(Yii::app()->params['sourceDateFormat']));
				}
			}
		}

		/**
		 * @param string $sessionKey
		 * @return LibraryViewModel[]
		 * @soap
		 */
		public function getLibraries($sessionKey)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$libraries = array();

				$dbCommand = \Yii::app()->db->createCommand();
				$dbCommand = $dbCommand->from('tbl_library lib');
				$dbCommand = $dbCommand->select(array(
					'library_id' => 'lib.id as library_id',
					'library_page_id' => 'pg.id as library_page_id',
					'library_name' => 'lib.name as library_name',
					'library_page_name' => 'pg.name as library_page_name',
//					'assigned_groups' => 'group_concat(distinct gr.name separator \',\') as assigned_groups',
//					'assigned_users' => 'group_concat(distinct us.login separator \',\') as assigned_users'
				));
				$dbCommand = $dbCommand->join('tbl_page pg', 'pg.id_library = lib.id');
//				$dbCommand = $dbCommand->leftJoin('tbl_group_library gr_lib', 'gr_lib.id_page = pg.id');
//				$dbCommand = $dbCommand->leftJoin('tbl_group gr', 'gr.id = gr_lib.id_group');
//				$dbCommand = $dbCommand->leftJoin('tbl_user_library us_lib', 'us_lib.id_page = pg.id');
//				$dbCommand = $dbCommand->leftJoin('tbl_user us', 'us.id = us_lib.id_user');
//				$dbCommand = $dbCommand->group(array(
//					'lib.id',
//					'pg.id',
//					'lib.name',
//					'pg.name',
//				));
				$dbCommand = $dbCommand->order('lib.name,pg.name');
				$resultRecords = $dbCommand->queryAll();

				foreach ($resultRecords as $resultRecord)
				{
					if (!isset($library) || $library->name != $resultRecord['library_name'])
					{
						$library = new LibraryViewModel();
						$library->id = $resultRecord['library_id'];
						$library->name = $resultRecord['library_name'];
						$libraries[] = $library;
					}

					$page = new LibraryPageViewModel();
					$page->id = $resultRecord['library_page_id'];
					$page->libraryId = $resultRecord['library_id'];
					$page->name = $resultRecord['library_page_name'];
					$page->libraryName = $resultRecord['library_name'];

					$page->assignedGroups = array();
//					if (!empty($resultRecord['assigned_groups']))
//						$page->assignedGroups = explode(',', $resultRecord['assigned_groups']);
					$page->assignedUsers = array();
//					if (!empty($resultRecord['assigned_users']))
//						$page->assignedUsers = explode(',', $resultRecord['assigned_users']);

					$library->pages[] = $page;
				}
				return $libraries;
			}
			return null;
		}

		/**
		 * @param string $sessionKey
		 * @param string $pageId
		 * @return SoapLibraryPage
		 * @soap
		 */
		public function getLibraryPage($sessionKey, $pageId)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				/** @var $pageRecord LibraryPageRecord */
				$pageRecord = LibraryPageRecord::model()->findByPk($pageId);
				/** @var $libraryRecord LibraryRecord */
				$libraryRecord = LibraryRecord::model()->findByPk($pageRecord->id_library);
				$page = new SoapLibraryPage();
				$page->id = $pageRecord->id;
				$page->libraryId = $pageRecord->id_library;
				$page->name = $pageRecord->name;
				$page->libraryName = $libraryRecord->name;

				$assignedUsers = UserLibraryRecord::getUserIdsByPage($pageRecord->id);
				foreach ($assignedUsers as $userId)
				{
					/** @var $userRecord UserRecord */
					$userRecord = UserRecord::model()->findByPk($userId);
					if (isset($userRecord))
					{
						$user = new UserViewModel();
						$user->id = $userRecord->id;
						$user->login = $userRecord->login;
						$user->firstName = $userRecord->first_name;
						$user->lastName = $userRecord->last_name;
						$user->email = $userRecord->email;
						$user->dateAdd = $userRecord->date_add;
						$user->dateModify = $userRecord->date_modify;
						$page->users[] = $user;
					}
				}

				$assignedGroups = GroupLibraryRecord::getGroupIdsByPage($pageRecord->id);
				foreach ($assignedGroups as $groupId)
				{
					/** @var $groupRecord GroupRecord */
					$groupRecord = GroupRecord::model()->findByPk($groupId);
					if (isset($groupRecord))
					{
						$group = new GroupViewModel();
						$group->id = $groupRecord->id;
						$group->name = $groupRecord->name;
						$page->groups[] = $group;
					}
				}
				return $page;
			}
			return null;
		}

		/**
		 * @param string $sessionKey
		 * @return string[]
		 * @soap
		 */
		public function getGroupTemplates($sessionKey)
		{
			if ($this->authenticateBySession($sessionKey))
				foreach (GroupTemplateRecord::model()->findAll() as $groupTemplateRecord)
				{
					/** @var $groupTemplateRecord GroupTemplateRecord */
					$groupTemplates[] = $groupTemplateRecord->name;
				}
			if (isset($groupTemplates))
				return $groupTemplates;
			return array();
		}

	}
