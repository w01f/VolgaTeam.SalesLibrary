<?

	namespace application\models\auth\models;


	/**
	 * Class ChangePasswordModel
	 */
	class ChangePasswordModel
	{
		public $login;
		public $oldPassword;
		public $rememberMe;
		public $newInitialPassword;
		public $newRepeatPassword;

		/**
		 * @param string $encodedContent
		 * @return ChangePasswordModel
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
		public function validatePassword()
		{
			$errors = array();
			if ($this->newInitialPassword == '' || $this->newRepeatPassword == '')
			{
				$errors[] = 'You need to type password and confirm it.';
			}
			else if ($this->newInitialPassword != $this->newRepeatPassword)
			{
				$errors[] = 'You need to type same password twice.';
			}
			else if (\Yii::app()->params['login']['complex_password'])
			{
				$conditionPass = 0;
				$pwd = $this->newInitialPassword;
				if (strlen($pwd) >= 8)
					$conditionPass++;
				if (preg_match("#[0-9]+#", $pwd))
					$conditionPass++;
				if (preg_match("#[a-z]+#", $pwd))
					$conditionPass++;
				if (preg_match("#[A-Z]+#", $pwd))
					$conditionPass++;
				if (preg_match("#\W+#", $pwd))
					$conditionPass++;
				if ($conditionPass <= 3)
				{
					$errors[] = 'Password must be min 8 characters and contain at least 3 of these 4 conditions below:' . '<br>' .
						'Password must include at least one character in lower case' . '<br>' .
						'Password must include at least one number' . '<br>' .
						'Password must include at least one CAPS' . '<br>' .
						'Password must include at least one symbol';
				}
			}
			return $errors;
		}

		public function changePassword()
		{
			$identity = new \UserIdentity($this->login, \UserRecord::hashPassword($this->oldPassword));
			$identity->changePassword($this->newInitialPassword);
			$identity = new \UserIdentity($this->login, \UserRecord::hashPassword($this->newInitialPassword));
			$identity->authenticate();
			$duration = $this->rememberMe ? 3600 * 24 * 30 : 0; // 30 days
			\Yii::app()->user->login($identity, $duration);
		}
	}