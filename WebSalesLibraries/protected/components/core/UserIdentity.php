<?

	/**
	 * Class UserIdentity
	 */
	class UserIdentity extends CUserIdentity
	{
		const ERROR_PASSWORD_NEED_CHANGE = 200;
		const ERROR_PASSWORD_EXPIRED = 300;

		private $_id;

		public function authenticate()
		{
			/** @var $user UserRecord */
			$user = UserRecord::model()->find('LOWER(login)=:login or LOWER(email)=:login', array('login' => strtolower($this->username)));
			if (!isset($user))
				$this->errorCode = self::ERROR_USERNAME_INVALID;
			else if (!$user->validatePassword($this->password))
				$this->errorCode = self::ERROR_PASSWORD_INVALID;
			else
			{
				$this->_id = $user->id;
				$this->username = $user->login;
				$this->setState('login', $user->login);
				$this->setState('firstName', $user->first_name);
				$this->setState('lastName', $user->last_name);
				$this->setState('email', $user->email);
				$this->setState('role', $user->role);
				$this->setState('groups', UserRecord::getGroupNames($user->id));
				$this->errorCode = self::ERROR_NONE;

				/** @var $resetPassword ResetPasswordRecord */
				$resetPassword = ResetPasswordRecord::model()->findByPk($this->username);
				if (isset($resetPassword))
				{
					$this->errorCode = self::ERROR_PASSWORD_NEED_CHANGE;

					$today = strtotime(date("Y-m-d"));
					$expirationDate = strtotime($resetPassword->initial_date . '+' . Yii::app()->params['login']['tempPasswordExpiredIn'] . ' days');
					if ($expirationDate <= $today)
						$this->errorCode = self::ERROR_PASSWORD_EXPIRED;
				}
			}
			return $this->errorCode == self::ERROR_NONE || $this->errorCode == self::ERROR_PASSWORD_NEED_CHANGE;
		}

		/**
		 * @param $newPassword
		 * @return bool
		 * @throws CDbException
		 */
		public function changePassword($newPassword)
		{
			/** @var $user UserRecord */
			$user = UserRecord::model()->find('LOWER(login)=?', array(strtolower($this->username)));
			if (isset($user))
			{
				$user->password = md5($newPassword);
				$user->save();

				$resetPassword = ResetPasswordRecord::model()->findByPk($this->username);
				if (isset($resetPassword))
					$resetPassword->delete();
				return true;
			}
			return false;
		}

		/**
		 * @return int
		 */
		public function getId()
		{
			return $this->_id;
		}

		/**
		 * @return int
		 */
		public static function getCurrentUserId()
		{
			$userId = -1;
			if(self::isUserAuthorized())
				$userId = Yii::app()->user->getId();
			return isset($userId) ? $userId : -1;
		}

		/**
		 * @return string
		 */
		public static function getCurrentUserLogin()
		{
			if(self::isUserAuthorized())
			{
				if (isset(Yii::app()->user))
					return Yii::app()->user->login;
			}
			return null;
		}

		/**
		 * @return array
		 */
		public static function getCurrentUserGroups()
		{
			if(self::isUserAuthorized())
			{
				$user = Yii::app()->user;
				if (isset($user))
				{
					$groups = $user->getState('groups');
					if (isset($groups))
						return $groups;
				}
			}
			return array();
		}

		/**
		 * @return boolean
		 */
		public static function isUserAuthorized()
		{
			return !(Yii::app() instanceof CConsoleApplication) && isset(Yii::app()->user) && !Yii::app()->user->isGuest;
		}

		/**
		 * @return boolean
		 */
		public static function isUserAdmin()
		{
			if(self::isUserAuthorized())
				return Yii::app()->user->role == 2;
			else
				return false;
		}
	}
