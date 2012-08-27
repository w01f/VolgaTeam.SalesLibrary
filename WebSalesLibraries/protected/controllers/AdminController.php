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
     * @param string Password
     * @param string First Name
     * @param string Last Name 
     * @param string Email
     * @soap
     */
    public function setUser($sessionKey, $login, $password, $firstName, $lastName, $email)
    {
        $newUser = FALSE;
        if ($this->authenticateBySession($sessionKey))
        {
            $user = UserStorage::model()->find('LOWER(login)=?', array(strtolower($login)));
            if ($user === null)
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
            }
            $user->save();
            if ($newUser)
            {
                $message = Yii::app()->email;
                $message->to = $email;
                $message->subject = Yii::app()->params['email']['new_user']['subject'];
                $message->from = Yii::app()->params['email']['from'];
                $message->view = 'newUser';
                $message->viewVars = array('fullName' => ($firstName . ' ' . $lastName), 'login' => $login, 'password' => $password, 'site'=>Yii::app()->name);
                $message->send();
            }
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
                $users[] = $user;
            }
            return $users;
        }
    }

}

?>