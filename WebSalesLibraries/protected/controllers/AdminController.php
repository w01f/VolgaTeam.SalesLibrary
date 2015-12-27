<?php

	/**
	 * Class AdminController
	 */
	class AdminController extends SoapController
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
						'UserModel' => 'UserModel',
						'GroupModel' => 'GroupModel',
						'Library' => 'Library',
						'LibraryPage' => 'LibraryPage',
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
		 * @param GroupModel[] $assignedGroups
		 * @param LibraryPage[] $assignedPages
		 * @param int $role
		 * @soap
		 */
		public function setUser($sessionKey, $login, $password, $firstName, $lastName, $email, $phone, $assignedGroups, $assignedPages, $role)
		{
			$newUser = false;
			$resetPassword = false;
			if ($this->authenticateBySession($sessionKey))
			{
				$user = UserRecord::model()->find('LOWER(login)=?', array(strtolower($login)));
				if (!isset($user))
				{
					$user = new UserRecord();
					$user->login = $login;
					$user->date_add = date(Yii::app()->params['mysqlDateFormat'], strtotime(date("Y-m-d H:i:s")));
					$newUser = TRUE;
				}
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
					ResetPasswordRecord::resetPasswordForUser($login, $password, $newUser, true);

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
		 * @return UserModel[]
		 * @soap
		 */
		public function getUsers($sessionKey)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				foreach (UserRecord::model()->findAll('role<>2') as $userRecord)
				{
					$user = new UserModel();
					$user->id = $userRecord->id;
					$user->login = $userRecord->login;
					$user->firstName = $userRecord->first_name;
					$user->lastName = $userRecord->last_name;
					$user->email = $userRecord->email;
					$user->phone = $userRecord->phone;
					$user->role = $userRecord->role;
					$user->dateAdd = $userRecord->date_add;

					$assignedLibraryIds = UserLibraryRecord::getLibraryIdsByUser($userRecord->id);
					$totalLibraries = LibraryRecord::model()->count();
					$user->allLibraries = isset($assignedLibraryIds) && $totalLibraries == count($assignedLibraryIds);
					$assignedPageIds = UserLibraryRecord::getPageIdsByUser($userRecord->id);
					foreach ($assignedLibraryIds as $libraryId)
					{
						/** @var $libraryRecord LibraryRecord */
						$libraryRecord = LibraryRecord::model()->findByPk($libraryId);
						if (isset($libraryRecord))
						{
							$library = new Library();
							$library->id = $libraryRecord->id;
							$library->name = $libraryRecord->name;
							$pageRecords = LibraryPageRecord::model()->findAll("id_library=? and id in ('" . implode("','", $assignedPageIds) . "')", array($libraryRecord->id));
							foreach ($pageRecords as $pageRecord)
							{
								$page = new LibraryPage($library);
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
					$totalGroups = GroupRecord::model()->count();
					$user->allGroups = isset($assignedGroups) && $totalGroups == count($assignedGroups);
					foreach ($assignedGroups as $groupId)
					{
						/** @var $groupRecord GroupRecord */
						$groupRecord = GroupRecord::model()->findByPk($groupId);
						if (isset($groupRecord))
						{
							$group = new GroupModel();
							$group->id = $groupRecord->id;
							$group->name = $groupRecord->name;
							$user->groups[] = $group;
						}
					}
					$users[] = $user;
				}
			}

			Yii::app()->cacheDB->flush();

			if (isset($users))
				return $users;
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
		 * @param UserModel[] $assignedUsers
		 * @param LibraryPage[] $assignedPages
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
		 * @return GroupModel[]
		 * @soap
		 */
		public function getGroups($sessionKey)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				foreach (GroupRecord::model()->findAll() as $groupRecord)
				{
					$group = new GroupModel();
					$group->id = $groupRecord->id;
					$group->name = $groupRecord->name;

					$assignedLibraryIds = GroupLibraryRecord::getLibraryIdsByGroup($groupRecord->id);
					$totalLibraries = LibraryRecord::model()->count();
					$group->allLibraries = isset($assignedLibraryIds) && $totalLibraries == count($assignedLibraryIds);
					$assignedPageIds = GroupLibraryRecord::getPageIdsByGroup($groupRecord->id);
					foreach ($assignedLibraryIds as $libraryId)
					{
						/** @var $libraryRecord LibraryRecord */
						$libraryRecord = LibraryRecord::model()->findByPk($libraryId);
						if (isset($libraryRecord))
						{
							$library = new Library();
							$library->id = $libraryRecord->id;
							$library->name = $libraryRecord->name;
							$pageRecords = LibraryPageRecord::model()->findAll("id_library=? and id in ('" . implode("','", $assignedPageIds) . "')", array($libraryRecord->id));
							if (isset($pageRecords))
							{
								foreach ($pageRecords as $pageRecord)
								{
									$page = new LibraryPage($library);
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
							$user->dateAdd = $userRecord->date_add;
							$group->users[] = $user;
						}
					}
					$groups[] = $group;
				}
			}

			Yii::app()->cacheDB->flush();

			if (isset($groups))
				return $groups;
			return null;
		}

		/**
		 * @param string $sessionKey
		 * @param string $id
		 * @param UserModel[] $assignedUsers
		 * @param GroupModel[] $assignedGroups
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
					$library = new Library();
					$library->id = $pageRecord->id_library;
					$page = new LibraryPage($library);
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
		 * @return Library[]
		 * @soap
		 */
		public function getLibraries($sessionKey)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				foreach (LibraryRecord::model()->findAll() as $libraryRecord)
				{
					$library = new Library();
					$library->id = $libraryRecord->id;
					$library->name = $libraryRecord->name;

					$pageRecords = LibraryPageRecord::model()->findAll('id_library=?', array($libraryRecord->id));
					if (isset($pageRecords))
					{
						foreach ($pageRecords as $pageRecord)
						{
							$page = new LibraryPage($library);
							$page->id = $pageRecord->id;
							$page->libraryId = $pageRecord->id_library;
							$page->name = $pageRecord->name;
							$page->libraryName = $library->name;
							$library->pages[] = $page;

							$assignedUsers = UserLibraryRecord::getUserIdsByPage($pageRecord->id);
							$totalUsers = UserRecord::model()->count('role<>2');
							$page->allUsers = isset($assignedUsers) && $totalUsers == count($assignedUsers);
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
									$user->dateAdd = $userRecord->date_add;
									$page->users[] = $user;
								}
							}

							$assignedGroups = GroupLibraryRecord::getGroupIdsByPage($pageRecord->id);
							$totalGroups = GroupRecord::model()->count();
							$page->allGroups = isset($assignedGroups) && $totalGroups == count($assignedGroups);
							foreach ($assignedGroups as $groupId)
							{
								/** @var $groupRecord GroupRecord */
								$groupRecord = GroupRecord::model()->findByPk($groupId);
								if (isset($groupRecord))
								{
									$group = new GroupModel();
									$group->id = $groupRecord->id;
									$group->name = $groupRecord->name;
									$page->groups[] = $group;
								}
							}
						}
					}
					$libraries[] = $library;
				}
			}

			Yii::app()->cacheDB->flush();

			if (isset($libraries))
				return $libraries;
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
					$groupTemplates[] = $groupTemplateRecord->name;
			if (isset($groupTemplates))
				return $groupTemplates;
			return array();
		}

	}
