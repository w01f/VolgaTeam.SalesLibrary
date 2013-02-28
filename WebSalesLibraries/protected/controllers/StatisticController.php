<?php
	class StatisticController extends IsdController
	{
		public function actions()
		{
			return array(
				'quote' => array(
					'class' => 'CWebServiceAction',
					'classMap' => array(
						'UserActivity' => 'UserActivity',
						'ActivityDetail' => 'ActivityDetail',
					),
				),
			);
		}

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
		 * @param string Date Start
		 * @param string Date End
		 * @return UserActivity[]
		 * @soap
		 */
		public function getActivities($sessionKey, $dateStart, $dateEnd)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$activityRecords = StatisticActivityStorage::model()->findAll("date_time>=? and date_time<=?", array(date(Yii::app()->params['mysqlDateFormat'], strtotime($dateStart)), date(Yii::app()->params['mysqlDateFormat'], strtotime($dateEnd))));
				foreach ($activityRecords as $activityRecord)
				{
					$activity = new UserActivity();
					$activity->id = $activityRecord->id;
					$activity->date = $activityRecord->date_time;
					$activity->type = $activityRecord->type;
					$activity->subType = $activityRecord->sub_type;

					$userRecord = StatisticUserStorage::model()->find('id_activity=?', array($activityRecord->id));
					if (isset($userRecord))
					{
						$activity->login = $userRecord->login;
						$activity->firstName = $userRecord->first_name;
						$activity->lastName = $userRecord->last_name;
						$activity->email = $userRecord->email;
						$activity->ip = $userRecord->ip;
						$activity->os = $userRecord->os;
						$activity->device = $userRecord->device;
						$activity->browser = $userRecord->browser;
					}

					$groupRecords = StatisticGroupStorage::model()->findAll('id_activity=?', array($activityRecord->id));
					foreach ($groupRecords as $groupRecord)
						$groups[] = $groupRecord->name;
					if (isset($groups))
						$activity->groups = implode(",", $groups);


					$detailRecords = StatisticDetailStorage::model()->findAll('id_activity=?', array($activityRecord->id));
					foreach ($detailRecords as $detailRecord)
					{
						$detail = new ActivityDetail();
						$detail->tag = $detailRecord->tag;
						$detail->value = $detailRecord->data;
						$activity->details[] = $detail;
					}
					$activities[] = $activity;
				}
			}
			if (isset($activities))
				return $activities;
			else
				return null;
		}

		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'statistic');
		}

		public function actionWriteActivity()
		{
			$type = Yii::app()->request->getPost('type');
			$subType = Yii::app()->request->getPost('subType');
			$data = Yii::app()->request->getPost('data');
			if (isset($data))
				$data = CJSON::decode($data);

			if (isset($type) && isset($subType))
				StatisticActivityStorage::WriteActivity($type, $subType, $data);
			Yii::app()->end();
		}
	}
