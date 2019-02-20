<?php

	/**
	 * Class InactiveUsersController
	 */
	class InactiveUsersController extends SoapController
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
					'classMap' => array(
						'UserViewModel' => 'UserViewModel',
					),
				),
			);
		}

		/**
		 * @param string $sessionKey
		 * @param string $dateStart
		 * @param string $dateEnd
		 * @return UserViewModel[]
		 * @soap
		 */
		public function getInactiveUsers($sessionKey, $dateStart, $dateEnd)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$command = Yii::app()->db->createCommand("call sp_get_inactive_users(:start_date,:end_date)");
				$command->bindValue(":start_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateStart)), PDO::PARAM_STR);
				$command->bindValue(":end_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateEnd)), PDO::PARAM_STR);
				$resultRecords = $command->queryAll();
				foreach ($resultRecords as $resultRecord)
				{
					$userRecord = new UserViewModel();
					$userRecord->id = $resultRecord['id'];
					$userRecord->login = $resultRecord['login'];
					$userRecord->firstName = $resultRecord['first_name'];
					$userRecord->lastName = $resultRecord['last_name'];
					$userRecord->email = $resultRecord['email'];
					if (isset($resultRecord['groups']))
						$userRecord->assignedGroups = explode(',', $resultRecord['groups']);
					$userRecord->dateLastActivity = $resultRecord['last_activity'];
					$userRecords[] = $userRecord;
				}
			}
			if (isset($userRecords))
				return $userRecords;
			else
				return null;
		}

		/**
		 * @param string $sessionKey
		 * @param string $login
		 * @param string $password
		 * @soap
		 */
		public function resetUser($sessionKey, $login, $password)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				UserRecord::changePasswordByLogin($login, $password);
				ResetPasswordRecord::resetPasswordForUser(
					$login,
					$password,
					false,
					null,
					null);
			}
		}

		/**
		 * @param string $sessionKey
		 * @param string $login
		 * @soap
		 */
		public function deleteUser($sessionKey, $login)
		{
			if ($this->authenticateBySession($sessionKey))
				UserRecord::deleteUserByLogin($login);
		}
	}
