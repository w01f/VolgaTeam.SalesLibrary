<?php
class LoginForm extends CFormModel
{
    public $login;
    public $password;
    public $rememberMe;
    private $_identity;
    public function rules()
    {
        return array(
            array('login, password', 'required'),
            array('rememberMe', 'boolean'),
            array('login', 'validateCredentials'),
            array('password', 'validateCredentials'),
        );
    }

    public function attributeLabels()
    {
        return array(
            'rememberMe' => 'Remember me',
        );
    }

    public function validateCredentials()
    {
        $this->_identity = new UserIdentity($this->login, $this->password);
        $this->_identity->authenticate();
        if($this->_identity->errorCode === UserIdentity::ERROR_USERNAME_INVALID)
            $this->addError('login', 'User with this name was not found.');
        else if($this->_identity->errorCode === UserIdentity::ERROR_PASSWORD_INVALID)
            $this->addError('password', 'Incorrect username or password.');
    }

    public function login()
    {
        if ($this->_identity === null)
        {
            $this->_identity = new UserIdentity($this->login, $this->password);
            $this->_identity->authenticate();
        }
        if ($this->_identity->errorCode === UserIdentity::ERROR_NONE)
        {
            $duration = $this->rememberMe ? 3600 * 24 * 30 : 0; // 30 days
            Yii::app()->user->login($this->_identity, $duration);
            return true;
        }
        else
            return false;
    }
}