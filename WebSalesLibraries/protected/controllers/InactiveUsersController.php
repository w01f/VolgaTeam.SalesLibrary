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
						'UserModel' => 'UserModel',
					),
				),
			);
		}

		/**
		 * @param string $sessionKey
		 * @param string $dateStart
		 * @param string $dateEnd
		 * @return UserModel[]
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
					$userRecord = new UserModel();
					$userRecord->id = $resultRecord['id'];
					$userRecord->login = $resultRecord['login'];
					$userRecord->firstName = $resultRecord['first_name'];
					$userRecord->lastName = $resultRecord['last_name'];
					$userRecord->groupNames = $resultRecord['groups'];
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
		 * @param string[] $userIds
		 * @param boolean $onlyEmail
		 * @param string $sender
		 * @param string $subject
		 * @param string $body
		 * @soap
		 */
		public function resetUsers($sessionKey, $userIds, $onlyEmail, $sender, $subject, $body)
		{
			if ($this->authenticateBySession($sessionKey) && isset($userIds))
			{
				foreach ($userIds as $userId)
				{
					/** @var $userRecord UserRecord */
					$userRecord = UserRecord::model()->findByPk($userId);
					if (isset($userRecord->email))
					{
						$password = '';
						if (!$onlyEmail)
						{
							$password = UserRecord::generatePassword();
							UserRecord::changePassword($userRecord->login, $password);
							ResetPasswordRecord::resetPasswordForUser($userRecord->login, $password, false, false);
						}

						$message = Yii::app()->email;
						$message->to = $userRecord->email;
						$message->cc = $sender;
						$message->subject = $subject;
						$message->from = $sender;
						$body = str_replace(PHP_EOL, '<br>', $body);
						if ($onlyEmail)
							$message->message = $body;
						else
						{
							$message->viewVars = array('login' => $userRecord->login, 'password' => $password, 'body' => $body);
							$message->view = 'resetUser';
						}
						$message->send();
					}
				}
			}
		}

		/**
		 * @param string $sessionKey
		 * @param string[] $userIds
		 * @param boolean $onlyEmail
		 * @param string $sender
		 * @param string $subject
		 * @param string $body
		 * @soap
		 */
		public function deleteUsers($sessionKey, $userIds, $onlyEmail, $sender, $subject, $body)
		{
			if ($this->authenticateBySession($sessionKey) && isset($userIds))
			{
				foreach ($userIds as $userId)
				{
					$userRecord = UserRecord::model()->findByPk($userId);
					if (isset($userRecord->email))
					{
						$message = Yii::app()->email;
						$message->to = $userRecord->email;
						$message->cc = $sender;
						$message->subject = $subject;
						$message->from = $sender;
						$message->message = $body;
						$message->send();
						if (!$onlyEmail)
							UserRecord::deleteUserByLogin($userRecord->login);
					}
				}
			}
		}
	}
