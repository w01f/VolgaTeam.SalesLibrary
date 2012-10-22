<?php
class UserStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{user}}';
    }

    public function validatePassword($password)
    {
        return self::hashPassword($password) === $this->password;
    }

    public static function hashPassword($password)
    {
        return md5($password);
    }

    public static function changePassword($login, $password)
    {
        $user = UserStorage::model()->find('LOWER(login)=?', array(strtolower($login)));
        if (isset($user))
        {
            $user->password = self::hashPassword($password);
            $user->save();
        }
    }

    public static function generatePassword()
    {
        $alphabet = "abcdefghijklmnopqrstuwxyz0123456789";
        for ($i = 0; $i < 5; $i++)
        {
            $n = rand(0, strlen($alphabet) - 1);
            $pass[$i] = $alphabet[$n];
        }
        return implode($pass);
    }

    public static function validateUserByEmail($login, $email)
    {
        $result = '';
        $user = UserStorage::model()->find('LOWER(login)=?', array(strtolower($login)));
        if (isset($user))
        {
            if (strtolower($user->email) == strtolower($email))
                $result = '';
            else
                $result = 'Email address is not correct';
        }
        else
            $result = 'User with name "' . $login . '" is not registered';

        return $result;
    }

}