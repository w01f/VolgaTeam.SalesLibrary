<?php

	/**
	 * Class UtilityController
	 */
	class UtilityController extends SoapController
	{
		/** return boolean */
		protected function getIsPublicController()
		{
			return true;
		}

		/**
		 * @return array
		 */
		public function actions()
		{
			return array(
				'quote' => array(
					'class' => 'CWebServiceAction',
					'classMap' => array(),
				),
			);
		}

		/**
		 * @param string $sessionKey
		 * @return string
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
		 * @param string $sessionKey
		 * @return string
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
		 * @param string $sessionKey
		 * @return string
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
		 * @param string $sessionKey
		 * @return string
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