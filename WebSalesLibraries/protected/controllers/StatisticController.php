<?php

	/**
	 * Class StatisticController
	 */
	class StatisticController extends IsdController
	{
		/**
		 * @return array
		 */
		public function actions()
		{
			return array(
				'quote' => array(
					'class' => 'CWebServiceAction',
					'classMap' => array(
						'UserActivity' => 'UserActivity',
						'ActivityDetail' => 'ActivityDetail',
						'MainUserReportModel' => 'MainUserReportModel',
						'MainGroupReportModel' => 'MainGroupReportModel',
						'NavigationUserReportModel' => 'NavigationUserReportModel',
						'NavigationGroupReportModel' => 'NavigationGroupReportModel',
						'QuizPassUserReportModel' => 'QuizPassUserReportModel',
						'QuizPassGroupReportModel' => 'QuizPassGroupReportModel',
						'FileActivityReportModel' => 'FileActivityReportModel',
						'VideoLinkInfo' => 'VideoLinkInfo',
					),
				),
			);
		}

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
		 * @param string $sessionKey
		 * @param string $dateStart
		 * @param string $dateEnd
		 * @return UserActivity[]
		 * @soap
		 */
		public function getActivities($sessionKey, $dateStart, $dateEnd)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$activityRecords = StatisticActivityRecord::model()->findByDateRange($dateStart, $dateEnd);
				foreach ($activityRecords as $activityRecord)
				{
					$activity = new UserActivity();
					$activity->id = $activityRecord->id;
					$activity->date = $activityRecord->date_time;
					$activity->type = $activityRecord->type;
					$activity->subType = $activityRecord->sub_type;
					$activity->login = $activityRecord->userActivity->login;
					$activity->firstName = $activityRecord->userActivity->first_name;
					$activity->lastName = $activityRecord->userActivity->last_name;
					$activity->email = $activityRecord->userActivity->email;
					$activity->phone = $activityRecord->userActivity->phone;
					$activity->ip = $activityRecord->userActivity->ip;
					$activity->os = $activityRecord->userActivity->os;
					$activity->device = $activityRecord->userActivity->device;
					$activity->browser = $activityRecord->userActivity->browser;

					unset($groups);
					foreach ($activityRecord->groupActivities as $groupRecord)
						$groups[] = $groupRecord->name;
					if (isset($groups))
						$activity->groups = implode(", ", array_values($groups));

					foreach ($activityRecord->activityDetails as $detailRecord)
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

		/**
		 * @param string $sessionKey
		 * @param string $dateStart
		 * @param string $dateEnd
		 * @return MainUserReportModel[]
		 * @soap
		 */
		public function getMainUserReport($sessionKey, $dateStart, $dateEnd)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$command = Yii::app()->db->createCommand("call sp_get_main_user_report(:start_date,:end_date)");
				$command->bindValue(":start_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateStart)), PDO::PARAM_STR);
				$command->bindValue(":end_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateEnd)), PDO::PARAM_STR);
				$resultRecords = $command->queryAll();
				foreach ($resultRecords as $resultRecord)
				{
					$reportRecord = new MainUserReportModel();
					$reportRecord->firstName = $resultRecord['first_name'];
					$reportRecord->lastName = $resultRecord['last_name'];
					$reportRecord->group = $resultRecord['group_name'];
					$reportRecord->groupUserCount = $resultRecord['group_user_count'];
					$reportRecord->userTotal = $resultRecord['user_activity_total'];
					$reportRecord->groupTotal = $resultRecord['group_activity_total'];
					$reportRecord->userLogins = $resultRecord['user_activity_login'];
					$reportRecord->groupLogins = $resultRecord['group_activity_login'];
					$reportRecord->userDocs = $resultRecord['user_activity_doc'];
					$reportRecord->groupDocs = $resultRecord['group_activity_doc'];
					$reportRecord->userVideos = $resultRecord['user_activity_video'];
					$reportRecord->groupVideos = $resultRecord['group_activity_video'];
					$reportRecords[] = $reportRecord;
				}
			}
			if (isset($reportRecords))
				return $reportRecords;
			else
				return null;
		}

		/**
		 * @param string $sessionKey
		 * @param string $dateStart
		 * @param string $dateEnd
		 * @return MainGroupReportModel[]
		 * @soap
		 */
		public function getMainGroupReport($sessionKey, $dateStart, $dateEnd)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$command = Yii::app()->db->createCommand("call sp_get_main_group_report(:start_date,:end_date)");
				$command->bindValue(":start_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateStart)), PDO::PARAM_STR);
				$command->bindValue(":end_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateEnd)), PDO::PARAM_STR);
				$resultRecords = $command->queryAll();
				foreach ($resultRecords as $resultRecord)
				{
					$reportRecord = new MainGroupReportModel();
					$reportRecord->name = $resultRecord['name'];
					$reportRecord->totals = $resultRecord['totals'];
					$reportRecord->logins = $resultRecord['logins'];
					$reportRecord->docs = $resultRecord['docs'];
					$reportRecord->videos = $resultRecord['videos'];
					$reportRecords[] = $reportRecord;
				}
			}
			if (isset($reportRecords))
				return $reportRecords;
			else
				return null;
		}

		/**
		 * @param string $sessionKey
		 * @param string $dateStart
		 * @param string $dateEnd
		 * @return NavigationUserReportModel[]
		 * @soap
		 */
		public function getNavigationUserReport($sessionKey, $dateStart, $dateEnd)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$command = Yii::app()->db->createCommand("call sp_get_navigation_user_report(:start_date,:end_date)");
				$command->bindValue(":start_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateStart)), PDO::PARAM_STR);
				$command->bindValue(":end_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateEnd)), PDO::PARAM_STR);
				$resultRecords = $command->queryAll();
				foreach ($resultRecords as $resultRecord)
				{
					$reportRecord = new NavigationUserReportModel();
					$reportRecord->firstName = $resultRecord['first_name'];
					$reportRecord->lastName = $resultRecord['last_name'];
					$reportRecord->group = $resultRecord['group_name'];
					$reportRecord->groupUserCount = $resultRecord['group_user_count'];
					$reportRecord->userTotal = $resultRecord['user_activity_total'];
					$reportRecord->groupTotal = $resultRecord['group_activity_total'];
					$reportRecord->userLibraries = $resultRecord['user_activity_libs'];
					$reportRecord->groupLibraries = $resultRecord['group_activity_libs'];
					$reportRecord->userPages = $resultRecord['user_activity_pages'];
					$reportRecord->groupPages = $resultRecord['group_activity_pages'];
					$reportRecords[] = $reportRecord;
				}
			}
			if (isset($reportRecords))
				return $reportRecords;
			else
				return null;
		}

		/**
		 * @param string $sessionKey
		 * @param string $dateStart
		 * @param string $dateEnd
		 * @return NavigationGroupReportModel[]
		 * @soap
		 */
		public function getNavigationGroupReport($sessionKey, $dateStart, $dateEnd)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$command = Yii::app()->db->createCommand("call sp_get_navigation_group_report(:start_date,:end_date)");
				$command->bindValue(":start_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateStart)), PDO::PARAM_STR);
				$command->bindValue(":end_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateEnd)), PDO::PARAM_STR);
				$resultRecords = $command->queryAll();
				foreach ($resultRecords as $resultRecord)
				{
					$reportRecord = new NavigationGroupReportModel();
					$reportRecord->name = $resultRecord['name'];
					$reportRecord->totals = $resultRecord['totals'];
					$reportRecord->libs = $resultRecord['libs'];
					$reportRecord->pages = $resultRecord['pages'];
					$reportRecords[] = $reportRecord;
				}
			}
			if (isset($reportRecords))
				return $reportRecords;
			else
				return null;
		}

		/**
		 * @param string $sessionKey
		 * @param string $dateStart
		 * @param string $dateEnd
		 * @return AccessReportModel[]
		 * @soap
		 */
		public function getAccessReport($sessionKey, $dateStart, $dateEnd)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$command = Yii::app()->db->createCommand("call sp_get_access_report(:start_date,:end_date)");
				$command->bindValue(":start_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateStart)), PDO::PARAM_STR);
				$command->bindValue(":end_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateEnd)), PDO::PARAM_STR);
				$resultRecords = $command->queryAll();
				foreach ($resultRecords as $resultRecord)
				{
					$reportRecord = new AccessReportModel();
					$reportRecord->name = $resultRecord['name'];
					$reportRecord->userCount = $resultRecord['user_count'];
					$reportRecord->activeCount = $resultRecord['active_count'];
					$reportRecord->activeNames = $resultRecord['active_names'];
					$reportRecord->inactiveCount = $resultRecord['inactive_count'];
					$reportRecord->inactiveNames = $resultRecord['inactive_names'];
					$reportRecords[] = $reportRecord;
				}
			}
			if (isset($reportRecords))
				return $reportRecords;
			else
				return null;
		}

		/**
		 * @param string $sessionKey
		 * @param string $dateStart
		 * @param string $dateEnd
		 * @return QuizPassUserReportModel[]
		 * @soap
		 */
		public function getQuizPassUserReport($sessionKey, $dateStart, $dateEnd)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$command = Yii::app()->db->createCommand("call sp_get_quiz_pass_user_report(:start_date,:end_date)");
				$command->bindValue(":start_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateStart)), PDO::PARAM_STR);
				$command->bindValue(":end_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateEnd)), PDO::PARAM_STR);
				$resultRecords = $command->queryAll();
				foreach ($resultRecords as $resultRecord)
				{
					$reportRecord = new QuizPassUserReportModel();
					$reportRecord->firstName = $resultRecord['first_name'];
					$reportRecord->lastName = $resultRecord['last_name'];
					$reportRecord->group = $resultRecord['group_name'];
					$reportRecord->quizName = $resultRecord['quiz_name'];
					$reportRecord->topLevelName = $resultRecord['top_level_name'];
					$reportRecord->quizPassDate = $resultRecord['quiz_pass_date'];
					$reportRecord->quizTryCount = $resultRecord['quiz_try_count'];
					$reportRecords[] = $reportRecord;
				}
			}
			if (isset($reportRecords))
				return $reportRecords;
			else
				return null;
		}

		/**
		 * @param string $sessionKey
		 * @param string $dateStart
		 * @param string $dateEnd
		 * @return QuizPassGroupReportModel[]
		 * @soap
		 */
		public function getQuizPassGroupReport($sessionKey, $dateStart, $dateEnd)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$command = Yii::app()->db->createCommand("call sp_get_quiz_pass_group_report(:start_date,:end_date)");
				$command->bindValue(":start_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateStart)), PDO::PARAM_STR);
				$command->bindValue(":end_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateEnd)), PDO::PARAM_STR);
				$resultRecords = $command->queryAll();
				foreach ($resultRecords as $resultRecord)
				{
					$reportRecord = new QuizPassGroupReportModel();
					$reportRecord->group = $resultRecord['group_name'];
					$reportRecord->quizName = $resultRecord['quiz_name'];
					$reportRecord->topLevelName = $resultRecord['top_level_name'];
					$reportRecord->userCount = $resultRecord['user_count'];
					$reportRecords[] = $reportRecord;
				}
			}
			if (isset($reportRecords))
				return $reportRecords;
			else
				return null;
		}

		/**
		 * @param string $sessionKey
		 * @return VideoLinkInfo[]
		 * @soap
		 */
		public function getVideoLinkInfo($sessionKey)
		{
			$reportRecords = array();
			if ($this->authenticateBySession($sessionKey))
			{
				$command = Yii::app()->db->createCommand("call sp_get_video_links()");
				$resultRecords = $command->queryAll();
				foreach ($resultRecords as $resultRecord)
				{
					$videoLinkInfo = new VideoLinkInfo();
					$videoLinkInfo->fileName = $resultRecord['file_name'];
					$videoLinkInfo->linkName = $resultRecord['link_name'];
					$videoLinkInfo->categoryGroups = $resultRecord['category_groups'];
					$videoLinkInfo->categoryTags = $resultRecord['category_tags'];
					$videoLinkInfo->keywords = $resultRecord['keywords'];
					$videoLinkInfo->station = $resultRecord['station'];
					$videoLinkInfo->linkDate = $resultRecord['link_date'];
					$videoLinkInfo->fileDate = $resultRecord['file_date'];

					/** @var $extendedProperties LinkSettings */
					$extendedProperties = CJSON::decode($resultRecord['properties'], false);
					$videoLinkInfo->linkNote = $extendedProperties->note;
					$videoLinkInfo->hoverNote = $extendedProperties->hoverNote;

					$rootPath = Yii::app()->basePath . DIRECTORY_SEPARATOR . '..';
					$libraryRelativePath = DIRECTORY_SEPARATOR . Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Libraries' . DIRECTORY_SEPARATOR . $videoLinkInfo->station . DIRECTORY_SEPARATOR . 'Primary Root';
					$libraryFolderPath = realpath($rootPath . $libraryRelativePath);
					if (!file_exists($libraryFolderPath))
						$libraryRelativePath = DIRECTORY_SEPARATOR . Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Libraries' . DIRECTORY_SEPARATOR . $videoLinkInfo->station;
					$videoLinkInfo->mp4Url = $resultRecord['mp4_path'] != '' ? Yii::app()->getBaseUrl(true) . htmlspecialchars(str_replace('&', '%26', str_replace(' ', '%20', str_replace('\\', '/', $libraryRelativePath . $resultRecord['mp4_path'])))) : '';
					$videoLinkInfo->thumbUrl = $resultRecord['thumb_path'] != '' ? Yii::app()->getBaseUrl(true) . htmlspecialchars(str_replace('&', '%26', str_replace(' ', '%20', str_replace('\\', '/', $libraryRelativePath . $resultRecord['thumb_path'])))) : '';
					$reportRecords[] = $videoLinkInfo;
				}
			}
			return $reportRecords;
		}

		/**
		 * @param string $sessionKey
		 * @param string $dateStart
		 * @param string $dateEnd
		 * @return FileActivityReportModel[]
		 * @soap
		 */
		public function getFileActivityReport($sessionKey, $dateStart, $dateEnd)
		{
			$reportRecords = array();
			if ($this->authenticateBySession($sessionKey))
			{
				$command = Yii::app()->db->createCommand("call sp_get_file_activity_report(:start_date,:end_date)");
				$command->bindValue(":start_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateStart)), PDO::PARAM_STR);
				$command->bindValue(":end_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateEnd)), PDO::PARAM_STR);
				$resultRecords = $command->queryAll();
				foreach ($resultRecords as $resultRecord)
				{
					$reportRecord = new FileActivityReportModel();
					$reportRecord->group = $resultRecord['group_name'];
					$reportRecord->fileName = $resultRecord['file_name'];
					$reportRecord->activityCount = $resultRecord['action_count'];
					$reportRecords[] = $reportRecord;
				}
			}
			return $reportRecords;
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

			$authorized = false;
			if (isset(Yii::app()->user))
			{
				$userId = Yii::app()->user->getId();
				$authorized = isset($userId);
			}

			if (isset($type) && isset($subType) && $authorized)
				StatisticActivityRecord::WriteActivity($type, $subType, $data);
			Yii::app()->end();
		}
	}
