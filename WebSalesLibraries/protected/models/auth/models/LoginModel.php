<?

	namespace application\models\auth\models;

	/**
	 * Class LoginModel
	 */
	class LoginModel
	{
		private $_identity;

		public $login;
		public $password;
		public $rememberMe;

		public $authenticated;
		public $needToResetPassword;

		/**
		 * @param string $encodedContent
		 * @return LoginModel
		 */
		public static function fromJson($encodedContent)
		{
			$instance = new self();
			\Utils::loadFromJson($instance, $encodedContent);
			return $instance;
		}

		/**
		 * @return array()
		 */
		public function validateCredentials()
		{
			$errors = array();
			$this->_identity = new \UserIdentity($this->login, $this->password);
			if (!$this->_identity->authenticate())
			{
				if ($this->_identity->errorCode === \UserIdentity::ERROR_USERNAME_INVALID)
					$errors[] = 'User with this name was not found.';
				else if ($this->_identity->errorCode === \UserIdentity::ERROR_PASSWORD_INVALID)
					$errors[] = 'Incorrect username or password.';
				else if ($this->_identity->errorCode === \UserIdentity::ERROR_PASSWORD_EXPIRED)
					$errors[] = 'Temporary password is expired';
				$this->authenticated = false;
			}
			else
			{
				$this->authenticated = true;
				$this->needToResetPassword = $this->_identity->errorCode === \UserIdentity::ERROR_PASSWORD_NEED_CHANGE;
			}
			return $errors;
		}

		public function login()
		{
			$duration = $this->rememberMe ? 3600 * 24 * 30 : 0; // 30 days
			\Yii::app()->user->login($this->_identity, $duration);
		}
	}