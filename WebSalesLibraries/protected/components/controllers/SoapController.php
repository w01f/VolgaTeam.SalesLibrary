<?

	/**
	 * Class SoapController
	 */
	abstract class SoapController extends IsdController
	{
		/**
		 * @param string $sessionKey
		 * @return bool
		 */
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
		 * @return string
		 * @soap
		 */
		public function getSessionKey($login, $password)
		{
			$identity = new UserIdentity($login, UserRecord::hashPassword($password));
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
	}