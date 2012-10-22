<?php
class ChangePasswordForm extends CFormModel
{
    public $login;
    public $oldPassword;
    public $rememberMe;
    public $newInitialPassword;
    public $newRepeatPassword;
    public function rules()
    {
        return array(
            array('newInitialPassword, newRepeatPassword', 'required'),
            array('newInitialPassword, newRepeatPassword', 'validatePassword'),
        );
    }

    public function attributeLabels()
    {
        return array(
        );
    }

    public function validatePassword()
    {
        $result = true;
        if ($this->newInitialPassword == '' || $this->newRepeatPassword == '')
        {
            $this->addError('newInitialPassword', 'You need to type password and confirm it.');
            $result = false;
        }
        else if ($this->newInitialPassword != $this->newRepeatPassword)
        {
            $this->addError('newRepeatPassword', 'You need to type same password twice.');
            $result = false;
        }
        return $result;
    }

    public function changePassword()
    {
        $identity = new UserIdentity($this->login, $this->oldPassword);
        if ($identity->changePassword($this->newInitialPassword))
        {
            $identity = new UserIdentity($this->login, $this->newInitialPassword);
            if ($identity->authenticate())
            {
                $duration = $this->rememberMe ? 3600 * 24 * 30 : 0; // 30 days
                Yii::app()->user->login($identity, $duration);
                return true;
            }
        }
        $this->addError('newInitialPassword', 'Error while changing password. Please contact to technical support');
        return false;
    }

}