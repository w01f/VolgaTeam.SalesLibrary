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

            UserGroupStorage::assignGroupsForUser($login, $assignedGroups);

            UserLibraryStorage::assignPagesForUser($login, $assignedPages);

            if ($resetPassword)
                ResetPasswordStorage::resetPasswordForUser($login, $password, $newUser);

            Yii::app()->cacheDB->flush();
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
            foreach (UserStorage::model()->findAll() as $userRecord)
            {
                $user = new UserRecord();
                $user->id = $userRecord->id;
                $user->login = $userRecord->login;
                $user->firstName = $userRecord->first_name;
                $user->lastName = $userRecord->last_name;
                $user->email = $userRecord->email;

                $assignedLibraryIds = UserLibraryStorage::getLibraryIdsByUser($userRecord->id);
                $assignedPageIds = UserLibraryStorage::getPageIdsByUser($userRecord->id);
                $libraryRecords = LibraryStorage::model()->findAll();
                if (isset($libraryRecords))
                {
                    foreach ($libraryRecords as $libraryRecord)
                    {
                        $library = new Library();
                        $library->id = $libraryRecord->id;
                        $library->name = $libraryRecord->name;
                        $library->selected = isset($assignedLibraryIds) && in_array($libraryRecord->id, $assignedLibraryIds);
                        $pageRecords = LibraryPageStorage::model()->findAll('id_library=?', array($libraryRecord->id));
                        if (isset($pageRecords))
                        {
                            foreach ($pageRecords as $pageRecord)
                            {
                                $page = new LibraryPage($library);
                                $page->id = $pageRecord->id;
                                $page->libraryId = $pageRecord->id_library;
                                $page->name = $pageRecord->name;
                                $page->libraryName = $libraryRecord->name;
                                $page->selected = isset($assignedPageIds) && in_array($pageRecord->id, $assignedPageIds);
                                $library->pages[] = $page;
                            }
                        }
                        $user->libraries[] = $library;
                    }
                }

                $assignedGroups = UserGroupStorage::getGroupIdsByUser($userRecord->id);
                $groupRecords = GroupStorage::model()->findAll();
                if (isset($groupRecords))
                {
                    foreach ($groupRecords as $groupRecord)
                    {
                        $group = new GroupRecord();
                        $group->id = $groupRecord->id;
                        $group->name = $groupRecord->name;
                        $group->selected = isset($assignedGroups) && in_array($groupRecord->id, $assignedGroups);
                        $user->groups[] = $group;
                    }
                }
                $users[] = $user;
            }
        }
        if (isset($users))
            return $users;
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

            Yii::app()->cacheDB->flush();
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
                $assignedPageIds = GroupLibraryStorage::getPageIdsByGroup($groupRecord->id);
                $libraryRecords = LibraryStorage::model()->findAll();
                if (isset($libraryRecords))
                {
                    foreach ($libraryRecords as $libraryRecord)
                    {
                        $library = new Library();
                        $library->id = $libraryRecord->id;
                        $library->name = $libraryRecord->name;
                        $library->selected = isset($assignedLibraryIds) && in_array($libraryRecord->id, $assignedLibraryIds);
                        $pageRecords = LibraryPageStorage::model()->findAll('id_library=?', array($libraryRecord->id));
                        if (isset($pageRecords))
                        {
                            foreach ($pageRecords as $pageRecord)
                            {
                                $page = new LibraryPage($library);
                                $page->id = $pageRecord->id;
                                $page->libraryId = $pageRecord->id_library;
                                $page->name = $pageRecord->name;
                                $page->libraryName = $libraryRecord->name;
                                $page->selected = isset($assignedPageIds) && in_array($pageRecord->id, $assignedPageIds);
                                $library->pages[] = $page;
                            }
                        }
                        $group->libraries[] = $library;
                    }
                }

                $assignedUsers = UserGroupStorage::getUserIdsByGroup($groupRecord->id);
                $userRecords = UserStorage::model()->findAll();
                if (isset($userRecords))
                {
                    foreach ($userRecords as $userRecord)
                    {
                        $user = new UserRecord();
                        $user->id = $userRecord->id;
                        $user->login = $userRecord->login;
                        $user->firstName = $userRecord->first_name;
                        $user->lastName = $userRecord->last_name;
                        $user->email = $userRecord->email;
                        $user->selected = isset($assignedUsers) && in_array($userRecord->id, $assignedUsers);
                        $group->users[] = $user;
                    }
                }
                $groups[] = $group;
            }
        }
        if (isset($groups))
            return $groups;
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

                Yii::app()->cacheDB->flush();
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
                        $userRecords = UserStorage::model()->findAll();
                        if (isset($userRecords))
                        {
                            foreach ($userRecords as $userRecord)
                            {
                                $user = new UserRecord();
                                $user->id = $userRecord->id;
                                $user->login = $userRecord->login;
                                $user->firstName = $userRecord->first_name;
                                $user->lastName = $userRecord->last_name;
                                $user->email = $userRecord->email;
                                $user->selected = isset($assignedUsers) && in_array($userRecord->id, $assignedUsers);
                                $page->users[] = $user;
                            }
                        }

                        $assignedGroups = GroupLibraryStorage::getGroupIdsByPage($pageRecord->id);
                        $groupRecords = GroupStorage::model()->findAll();
                        if (isset($groupRecords))
                        {
                            foreach ($groupRecords as $groupRecord)
                            {
                                $group = new GroupRecord();
                                $group->id = $groupRecord->id;
                                $group->name = $groupRecord->name;
                                $group->selected = isset($assignedGroups) && in_array($groupRecord->id, $assignedGroups);
                                $page->groups[] = $group;
                            }
                        }
                    }
                }
                $libraries[] = $library;
            }
        }
        if (isset($libraries))
            return $libraries;
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
    }

}

?>