<?php

	/**
	 * Class StatisticController
	 */
	class StatisticController extends SoapController
	{
		/** return array */
		protected function getPublicActionIds()
		{
			return array(
				'quote',
			);
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
						'UserActivity' => 'UserActivity',
						'ActivityDetail' => 'ActivityDetail',
						'MainUserReportModel' => 'MainUserReportModel',
						'MainGroupReportModel' => 'MainGroupReportModel',
						'QuizPassUserReportModel' => 'QuizPassUserReportModel',
						'QuizPassGroupReportModel' => 'QuizPassGroupReportModel',
						'FileActivityReportModel' => 'FileActivityReportModel',
						'VideoLinkInfo' => 'VideoLinkInfo',
						'LibraryFilesModel' => 'LibraryFilesModel',
					),
				),
			);
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
					$activity->login = isset($activityRecord->userActivity->login) && $activityRecord->userActivity->login != '' ?
						$activityRecord->userActivity->login :
						'public user';
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

					if (isset($activityRecord->activityData) && isset($activityRecord->activityData->data))
					{
						$dataArray = CJSON::decode($activityRecord->activityData->data, true);
						foreach ($dataArray as $key => $value)
						{
							$detail = new ActivityDetail();
							$detail->tag = ucfirst(preg_replace('/\B([A-Z])/', ' $1', $key));
							$detail->value = $value;
							$activity->details[] = $detail;
						}
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
					$videoLinkInfo->station = $resultRecord['station_name'];
					$videoLinkInfo->linkDate = $resultRecord['link_date'];
					$videoLinkInfo->fileDate = $resultRecord['file_date'];

					/** @var $extendedProperties BaseLinkSettings */
					$extendedProperties = BaseLinkSettings::createByContent($resultRecord['properties']);
					$videoLinkInfo->linkNote = $extendedProperties->note;
					$videoLinkInfo->hoverNote = $extendedProperties->hoverNote;

					$libraryRelativePath = DIRECTORY_SEPARATOR . Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . $resultRecord['station_path'];
					$videoLinkInfo->mp4Url = $resultRecord['mp4_path'] != '' ? Yii::app()->getBaseUrl(true) . Utils::formatUrl($libraryRelativePath . $resultRecord['mp4_path']) : '';
					$videoLinkInfo->thumbUrl = $resultRecord['thumb_path'] != '' ? Yii::app()->getBaseUrl(true) . Utils::formatUrl($libraryRelativePath . $resultRecord['thumb_path']) : '';
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
					$reportRecord->fileType = $resultRecord['file_type'];
					$reportRecord->fileName = base64_encode(str_replace('\/', '/', $resultRecord['file_name']));
					$reportRecord->fileLink = base64_encode(str_replace('\/', '/', $resultRecord['file_link']));
					$reportRecord->fileDetail = base64_encode(str_replace('\/', '/', $resultRecord['file_detail']));
					$reportRecord->fileExtension = $resultRecord['file_extension'];
					$reportRecord->library = isset($resultRecord['lib_name']) && $resultRecord['lib_name'] != '' ? $resultRecord['lib_name'] : 'URL';
					$reportRecord->activityCount = $resultRecord['action_count'];
					$reportRecords[] = $reportRecord;
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
		public function getFileActivityReportLegacy($sessionKey, $dateStart, $dateEnd)
		{
			$reportRecords = array();
			if ($this->authenticateBySession($sessionKey))
			{
				$command = Yii::app()->db->createCommand("call sp_get_file_activity_report_legacy(:start_date,:end_date)");
				$command->bindValue(":start_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateStart)), PDO::PARAM_STR);
				$command->bindValue(":end_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateEnd)), PDO::PARAM_STR);
				$resultRecords = $command->queryAll();
				foreach ($resultRecords as $resultRecord)
				{
					if (!empty($resultRecord['file_name']) && $resultRecord['file_name'] != '-')
					{
						$reportRecord = new FileActivityReportModel();
						$reportRecord->group = $resultRecord['group_name'];
						$reportRecord->fileType = $resultRecord['file_type'];
						$reportRecord->fileName = base64_encode(str_replace('\/', '/', $resultRecord['file_name']));
						$reportRecord->fileLink = base64_encode(str_replace('\/', '/', $resultRecord['file_name']));
						$reportRecord->library = isset($resultRecord['lib_name']) && $resultRecord['lib_name'] != '' ? $resultRecord['lib_name'] : 'URL';
						$reportRecord->activityCount = $resultRecord['action_count'];
						$reportRecords[] = $reportRecord;
					}
				}
			}
			return $reportRecords;
		}

		/**
		 * @param string $sessionKey
		 * @return LibraryFilesModel[]
		 * @soap
		 */
		public function getLibraryFiles($sessionKey)
		{
			$reportRecords = array();
			if ($this->authenticateBySession($sessionKey))
			{
				$command = Yii::app()->db->createCommand("call sp_get_library_files_report()");
				$resultRecords = $command->queryAll();
				foreach ($resultRecords as $resultRecord)
				{
					$libraryFilesModel = new LibraryFilesModel();
					$libraryFilesModel->library = $resultRecord['library'];
					$libraryFilesModel->libraryDate = $resultRecord['library_date'];
					$libraryFilesModel->linkName = $resultRecord['link_name'];
					$libraryFilesModel->fileName = $resultRecord['file_name'];
					$libraryFilesModel->fileType = $resultRecord['file_type'];
					$libraryFilesModel->fileFormat = $resultRecord['file_format'];
					$libraryFilesModel->fileDate = $resultRecord['file_date'];
					$reportRecords[] = $libraryFilesModel;
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
			$linkId = Yii::app()->request->getPost('linkId');
			$authorized = UserIdentity::isUserAuthorized();
			if (isset($type) && isset($subType) && $authorized)
			{
				if (isset($linkId))
					StatisticActivityRecord::writeLinkActivity($linkId, $type, $subType, $data);
				else
					StatisticActivityRecord::writeCommonActivity($type, $subType, $data);
			}
			Yii::app()->end();
		}
	}