<?php
	class AdminController extends CController
	{
		public function actions()
		{
			return array(
				'quote' => array(
					'class' => 'CWebServiceAction',
					'classMap' => array(
						'UserRecord' => 'UserRecord',
						'GroupRecord' => 'GroupRecord',
						'Library' => 'Library',
						'LibraryPage' => 'LibraryPage',
					),
				),
			);
		}

		protected function authenticateBySession($sessionKey)
		{
			$data = Yii::app()->cacheDB->get($sessionKey);
			if ($data !== FALSE)
				return TRUE;
			else
				return FALSE;
		}

		/**
		 * @param string $login
		 * @param string $password
		 * @return string session key
		 * @soap
		 */
		public function getSessionKey($login, $password)
		{
			$identity = new UserIdentity($login, $password);
			$identity->authenticate();
			if ($identity->errorCode === UserIdentity::ERROR_NONE)
			{
				$sessionKey = strval(md5(mt_rand()));
				Yii::app()->cacheDB->set($sessionKey, $login, (60 * 60 * 24 * 7));
				return $sessionKey;
			}
			else
				return '';
		}

		/**
		 * @param string Session Key
		 * @param string Login
		 * @param string Temporary Password
		 * @param string First Name
		 * @param string Last Name
		 * @param string Email
		 * @param GroupRecord[] assigned groups
		 * @param LibraryPage[] assigned pages
		 * @soap
		 */
		public function setUser($sessionKey, $login, $password, $firstName, $lastName, $email, $assignedGroups, $assignedPages)
		{
			$newUser = false;
			$resetPassword = false;
			if ($this->authenticateBySession($sessionKey))
			{
				$user = UserStorage::model()->find('LOWER(login)=?', array(strtolower($login)));
				if (!isset($user))
				{
					$user = new UserStorage();
					$user->login = $login;
					$newUser = TRUE;
				}
				$user->first_name = $firstName;
				$user->last_name = $lastName;
				$user->email = $email;
				if ($password !== '')
				{
					$user->password = md5($password);
					$resetPassword = true;
				}
				$user->save();

				if (isset($assignedGroups) && count($assignedGroups) > 0)
					UserGroupStorage::assignGroupsForUser($login, $assignedGroups);

				if (isset($assignedPages) && count($assignedPages) > 0)
					UserLibraryStorage::assignPagesForUser($login, $assignedPages);

				if ($resetPassword)
					ResetPasswordStorage::resetPasswordForUser($login, $password, $newUser);
			}
		}

		/**
		 * @param string Session Key
		 * @param string Login
		 * @soap
		 */
		public function deleteUser($sessionKey, $login)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$userRecord = UserStorage::model()->find('LOWER(login)=?', array(strtolower($login)));
				if (isset($userRecord))
				{
					UserLibraryStorage::clearObjectsByUser($userRecord->id);
					UserGroupStorage::clearObjectsByUser($userRecord->id);
					UserStorage::model()->deleteByPk($userRecord->id);
					FavoritesLinkStorage::clearByUser($userRecord->id);
					FavoritesFolderStorage::clearByUser($userRecord->id);
					ResetPasswordStorage::model()->deleteAll('LOWER(login)=?', array(strtolower($login)));
				}
			}
		}

		/**
		 * @param string Session Key
		 * @return UserRecord[]
		 * @soap
		 */
		public function getUsers($sessionKey)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				foreach (UserStorage::model()->findAll('role=0') as $userRecord)
				{
					$user = new UserRecord();
					$user->id = $userRecord->id;
					$user->login = $userRecord->login;
					$user->firstName = $userRecord->first_name;
					$user->lastName = $userRecord->last_name;
					$user->email = $userRecord->email;

					$assignedLibraryIds = UserLibraryStorage::getLibraryIdsByUser($userRecord->id);
					$totalLibraries = LibraryStorage::model()->count();
					$user->allLibraries = isset($assignedLibraryIds) && $totalLibraries == count($assignedLibraryIds);
					$assignedPageIds = UserLibraryStorage::getPageIdsByUser($userRecord->id);
					if (isset($assignedLibraryIds) && isset($assignedPageIds))
						foreach ($assignedLibraryIds as $libraryId)
						{
							$libraryRecord = LibraryStorage::model()->findByPk($libraryId);
							if (isset($libraryRecord))
							{
								$library = new Library();
								$library->id = $libraryRecord->id;
								$library->name = $libraryRecord->name;
								$pageRecords = LibraryPageStorage::model()->findAll("id_library=? and id in ('" . implode("','", $assignedPageIds) . "')", array($libraryRecord->id));
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
								$user->libraries[] = $library;
							}
						}

					$assignedGroups = UserGroupStorage::getGroupIdsByUser($userRecord->id);
					$totalGroups = GroupStorage::model()->count();
					$user->allGroups = isset($assignedGroups) && $totalGroups == count($assignedGroups);
					if (isset($assignedGroups))
						foreach ($assignedGroups as $groupId)
						{
							$groupRecord = GroupStorage::model()->findByPk($groupId);
							if (isset($groupRecord))
							{
								$group = new GroupRecord();
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
		 * @param string Session Key
		 * @param string id
		 * @param string name
		 * @param UserRecord[] assigned users
		 * @param LibraryPage[] assigned pages
		 * @soap
		 */
		public function setGroup($sessionKey, $id, $name, $assignedUsers, $assignedPages)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$group = GroupStorage::model()->findByPk($id);
				if (!isset($group))
				{
					$group = new GroupStorage();
					$group->id = $id;
				}
				$group->name = $name;
				$group->save();

				UserGroupStorage::assignUsersForGroup($id, $assignedUsers);

				GroupLibraryStorage::assignPagesForGroup($id, $assignedPages);
			}
		}

		/**
		 * @param string Session Key
		 * @param string id
		 * @soap
		 */
		public function deleteGroup($sessionKey, $id)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				UserGroupStorage::clearObjectsByGroup($id);
				GroupLibraryStorage::clearObjectsByGroup($id);
				GroupStorage::model()->deleteByPk($id);
			}
		}

		/**
		 * @param string Session Key
		 * @return GroupRecord[]
		 * @soap
		 */
		public function getGroups($sessionKey)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				foreach (GroupStorage::model()->findAll() as $groupRecord)
				{
					$group = new GroupRecord();
					$group->id = $groupRecord->id;
					$group->name = $groupRecord->name;

					$assignedLibraryIds = GroupLibraryStorage::getLibraryIdsByGroup($groupRecord->id);
					$totalLibraries = LibraryStorage::model()->count();
					$group->allLibraries = isset($assignedLibraryIds) && $totalLibraries == count($assignedLibraryIds);
					$assignedPageIds = GroupLibraryStorage::getPageIdsByGroup($groupRecord->id);
					if (isset($assignedLibraryIds) && isset($assignedPageIds))
						foreach ($assignedLibraryIds as $libraryId)
						{
							$libraryRecord = LibraryStorage::model()->findByPk($libraryId);
							if (isset($libraryRecord))
							{
								$library = new Library();
								$library->id = $libraryRecord->id;
								$library->name = $libraryRecord->name;
								$pageRecords = LibraryPageStorage::model()->findAll("id_library=? and id in ('" . implode("','", $assignedPageIds) . "')", array($libraryRecord->id));
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

					$assignedUsers = UserGroupStorage::getUserIdsByGroup($groupRecord->id);
					$totalUsers = UserStorage::model()->count('role=0');
					$group->allUsers = isset($assignedUsers) && $totalUsers == count($assignedUsers);
					if (isset($assignedUsers))
						foreach ($assignedUsers as $userId)
						{
							$userRecord = UserStorage::model()->findByPk($userId);
							if (isset($userRecord))
							{
								$user = new UserRecord();
								$user->id = $userRecord->id;
								$user->login = $userRecord->login;
								$user->firstName = $userRecord->first_name;
								$user->lastName = $userRecord->last_name;
								$user->email = $userRecord->email;
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
		 * @param string Session Key
		 * @param string id
		 * @param UserRecord[] assigned users
		 * @param GroupRecord[] assigned groups
		 * @soap
		 */
		public function setPage($sessionKey, $id, $assignedUsers, $assignedGroups)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$pageRecord = LibraryPageStorage::model()->findByPk($id);
				if (isset($pageRecord))
				{
					$library = new Library();
					$library->id = $pageRecord->id_library;
					$page = new LibraryPage($library);
					$page->id = $pageRecord->id;
					$page->libraryId = $pageRecord->id_library;

					UserLibraryStorage::assignUsersForPage($page, $assignedUsers);
					GroupLibraryStorage::assignGroupsForPage($page, $assignedGroups);
				}
			}
		}

		/**
		 * @param string Session Key
		 * @return Library[]
		 * @soap
		 */
		public function getLibraries($sessionKey)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				foreach (LibraryStorage::model()->findAll() as $libraryRecord)
				{
					$library = new Library();
					$library->id = $libraryRecord->id;
					$library->name = $libraryRecord->name;

					$pageRecords = LibraryPageStorage::model()->findAll('id_library=?', array($libraryRecord->id));
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

							$assignedUsers = UserLibraryStorage::getUserIdsByPage($pageRecord->id);
							$totalUsers = UserStorage::model()->count('role=0');
							$page->allUsers = isset($assignedUsers) && $totalUsers == count($assignedUsers);
							if (isset($assignedUsers))
								foreach ($assignedUsers as $userId)
								{
									$userRecord = UserStorage::model()->findByPk($userId);
									if (isset($userRecord))
									{
										$user = new UserRecord();
										$user->id = $userRecord->id;
										$user->login = $userRecord->login;
										$user->firstName = $userRecord->first_name;
										$user->lastName = $userRecord->last_name;
										$user->email = $userRecord->email;
										$page->users[] = $user;
									}
								}

							$assignedGroups = GroupLibraryStorage::getGroupIdsByPage($pageRecord->id);
							$totalGroups = GroupStorage::model()->count();
							$page->allGroups = isset($assignedGroups) && $totalGroups == count($assignedGroups);
							if (isset($assignedGroups))
								foreach ($assignedGroups as $groupId)
								{
									$groupRecord = GroupStorage::model()->findByPk($groupId);
									if (isset($groupRecord))
									{
										$group = new GroupRecord();
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
		 * @param string Session Key
		 * @return string[]
		 * @soap
		 */
		public function getGroupTemplates($sessionKey)
		{
			if ($this->authenticateBySession($sessionKey))
				foreach (GroupTemplateStorage::model()->findAll() as $groupTemplateRecord)
					$groupTemplates[] = $groupTemplateRecord->name;
			if (isset($groupTemplates))
				return $groupTemplates;
			return null;
		}

	}
