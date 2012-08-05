<?php
class UserIdentity extends CUserIdentity
{
    private $_id;
    
    public function authenticate()
    {
        $user=User::model()->find('LOWER(login)=?',array(strtolower($this->username)));
        if ($user === null)
            $this->errorCode = self::ERROR_USERNAME_INVALID;
        else if (!$user->validatePassword($this->password))
            $this->errorCode = self::ERROR_PASSWORD_INVALID;
        else
        {
            $this->_id = $user->id;
            $this->username = $user->login;
            $this->errorCode = self::ERROR_NONE;
            $this->setState('role', $user->role);
        }
        return $this->errorCode == self::ERROR_NONE;
    }

    public function getId()
    {
        return $this->_id;
    }
}

?>
