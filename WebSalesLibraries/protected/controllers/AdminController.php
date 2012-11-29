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
     * @param LibraryPage[] assigned pages
     * @soap
     */
    public function setUser($sessionKey, $login, $password, $firstName, $lastName, $email, $assignedPages)
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
            UserStorage::model()->deleteAll('LOWER(login)=?', array(strtolower($login)));
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
                $user->login = $userRecord->login;
                $user->firstName = $userRecord->first_name;
                $user->lastName = $userRecord->last_name;
                $user->email = $userRecord->email;

                $assignedLibraryIds = UserLibraryStorage::getLibraryIdsByUser($userRecord->id);
                $assignedPageIds = UserLibraryStorage::getPageIdsByUser($userRecord->id);
                if (isset($assignedLibraryIds) && isset($assignedPageIds))
                {
                    $libraryRecords = LibraryStorage::model()->findAll();
                    if (isset($libraryRecords))
                    {
                        foreach ($libraryRecords as $libraryRecord)
                        {
                            $library = new Library();
                            $library->id = $libraryRecord->id;
                            $library->name = $libraryRecord->name;
                            $library->selected = in_array($libraryRecord->id, $assignedLibraryIds);
                            $pageRecords = LibraryPageStorage::model()->findAll('id_library=?', array($libraryRecord->id));
                            if (isset($pageRecords))
                            {
                                foreach ($pageRecords as $pageRecord)
                                {
                                    $page = new LibraryPage($library);
                                    $page->id = $pageRecord->id;
                                    $page->libraryId = $pageRecord->id_library;
                                    $page->name = $pageRecord->name;
                                    $page->selected = in_array($pageRecord->id, $assignedPageIds);
                                    $library->pages[] = $page;
                                }
                            }
                            $user->libraries[] = $library;
                        }
                    }
                }
                $users[] = $user;
            }
        }
        return $users;
    }

}

?>