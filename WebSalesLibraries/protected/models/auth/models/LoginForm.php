<?php

	/**
	 * Class LoginForm
	 */
	class LoginForm extends CFormModel
	{
		private $_identity;
		private $_authenticated;
		public $login;
		public $password;
		public $rememberMe;
		public $needToResetPassword;

		/**
		 * @return array
		 */
		public function rules()
		{
			return array(
				array('rememberMe', 'boolean'),
				array('login', 'validateCredentials'),
				array('password', 'validateCredentials'),
			);
		}

		/**
		 * @return array
		 */
		public function attributeLabels()
		{
			return array(
				'login' => 'User Name:',
				'password' => 'Password:',
				'rememberMe' => 'Keep me logged in',
			);
		}

		/**
		 * @return bool
		 */
		public function validateCredentials()
		{
			$this->_identity = new UserIdentity($this->login, UserRecord::hashPassword($this->password));
			if (!$this->_identity->authenticate())
			{
				if ($this->_identity->errorCode === UserIdentity::ERROR_USERNAME_INVALID)
					$this->addError('login', 'User with this name was not found.');
				else if ($this->_identity->errorCode === UserIdentity::ERROR_PASSWORD_INVALID)
					$this->addError('password', 'Incorrect username or password.');
				else if ($this->_identity->errorCode === UserIdentity::ERROR_PASSWORD_EXPIRED)
					$this->addError('password', 'Temporary password is expired');
				$this->_authenticated = false;
			}
			else
			{
				$this->_authenticated = true;
				$this->needToResetPassword = $this->_identity->errorCode === UserIdentity::ERROR_PASSWORD_NEED_CHANGE;
			}
			return $this->_authenticated == true;
		}

		/**
		 * @return bool
		 */
		public function login()
		{
			if (isset($this->_identity) && $this->_authenticated)
			{
				$duration = $this->rememberMe ? 3600 * 24 * 30 : 0; // 30 days
				Yii::app()->user->login($this->_identity, $duration);
				return true;
			}
			return true;
		}

	}