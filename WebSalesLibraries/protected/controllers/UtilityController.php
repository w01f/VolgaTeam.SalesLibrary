<?php
	class UtilityController extends CController
	{
		public function actions()
		{
			return array(
				'quote' => array(
					'class' => 'CWebServiceAction',
					'classMap' => array(),
				),
			);
		}

		protected function authenticateBySession($sessionKey)
		{
			$data = Yii::app()->cacheDB->get($sessionKey);
			if ($data !== false)
				return true;
			else
				return false;
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
		 * @return string Command result
		 * @soap
		 */
		public function updateWallbin($sessionKey)
		{
			$result = 'Error';
			if ($this->authenticateBySession($sessionKey))
			{
				ob_start();

				$action = Yii::createComponent('application.components.actions.WallbinUpdateAction', $this, 'updateWallbin');
				$action->run();

				$result = ob_get_contents();
				ob_end_clean();
			}
			return $result;
		}

		/**
		 * @param string Session Key
		 * @return string Command result
		 * @soap
		 */
		public function updateShortcuts($sessionKey)
		{
			$result = 'Error';
			if ($this->authenticateBySession($sessionKey))
			{
				ob_start();

				$action = Yii::createComponent('application.components.actions.ShortcutsUpdateAction', $this, 'updateHelp');
				$action->run();

				$result = ob_get_contents();
				ob_end_clean();
			}
			return $result;
		}

		/**
		 * @param string Session Key
		 * @return string Command result
		 * @soap
		 */
		public function cleanExpiredEmails($sessionKey)
		{
			$result = 'Error';
			if ($this->authenticateBySession($sessionKey))
			{
				ob_start();

				$action = Yii::createComponent('application.components.actions.CleanExpiredEmailsAction', $this, 'cleanExpiredEmails');
				$action->run();

				$result = ob_get_contents();
				ob_end_clean();
			}
			return $result;
		}

		/**
		 * @param string Session Key
		 * @return string Command result
		 * @soap
		 */
		public function notifyDeadLinks($sessionKey)
		{
			$result = 'Error';
			if ($this->authenticateBySession($sessionKey))
			{
				ob_start();

				$action = Yii::createComponent('application.components.actions.NotifyDeadLinksAction', $this, 'notifyDeadLinks');
				$action->run();

				$result = ob_get_contents();
				ob_end_clean();
			}
			return $result;
		}

		/**
		 * @param string Session Key
		 * @return string Command result
		 * @soap
		 */
		public function updateQuizzes($sessionKey)
		{
			$result = 'Error';
			if ($this->authenticateBySession($sessionKey))
			{
				ob_start();

				$action = Yii::createComponent('application.components.actions.QuizzesUpdateAction', $this, 'updateQuizzes');
				$action->run();

				$result = ob_get_contents();
				ob_end_clean();
			}
			return $result;
		}
	}