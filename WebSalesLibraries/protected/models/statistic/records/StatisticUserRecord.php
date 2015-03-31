<?php

	/**
	 * Class StatisticUserRecord
	 * @property string id_activity
	 * @property mixed login
	 * @property mixed first_name
	 * @property mixed last_name
	 * @property mixed email
	 * @property mixed phone
	 * @property mixed ip
	 * @property string device
	 * @property string os
	 * @property string browser
	 */
	class StatisticUserRecord extends CActiveRecord
	{
		/**
		 * @param string $className
		 * @return CActiveRecord
		 */
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		/**
		 * @return string
		 */
		public function tableName()
		{
			return '{{statistic_user}}';
		}

		/**
		 * @param $activityId string
		 */
		public static function WriteUserDetail($activityId)
		{
			$detailRecord = new StatisticUserRecord();
			$detailRecord->id_activity = $activityId;
			/** @var $platform string */
			$platform = Yii::app()->browser->getPlatform();
			/** @var $browser string */
			$browser = Yii::app()->browser->getBrowser();
			switch ($platform)
			{
				case Browser::PLATFORM_IPHONE:
				case Browser::PLATFORM_IPOD:
				case Browser::PLATFORM_IPAD:
					$detailRecord->device = $platform;
					$detailRecord->os = 'iOS';
					break;
				case Browser::PLATFORM_BLACKBERRY:
					$detailRecord->device = $platform;
					$detailRecord->os = $platform;
					break;
				case Browser::PLATFORM_ANDROID:
					$detailRecord->device = $platform;
					$detailRecord->os = $platform;
					break;
				case Browser::PLATFORM_APPLE:
					$detailRecord->device = 'Mac';
					$detailRecord->os = 'iOS';
					break;
				default:
					$detailRecord->device = 'PC';
					$detailRecord->os = $platform;
			}
			$detailRecord->browser = $browser;
			$detailRecord->ip = Yii::app()->request->getUserHostAddress();
			if (isset(Yii::app()->user) && !Yii::app()->user->isGuest)
			{
				$userId = Yii::app()->user->getId();
				if (isset($userId))
				{
					/** @var $userRecord UserRecord */
					$userRecord = UserRecord::model()->findByPk($userId);
					if (isset($userRecord))
					{
						$detailRecord->login = $userRecord->login;
						$detailRecord->first_name = $userRecord->first_name;
						$detailRecord->last_name = $userRecord->last_name;
						$detailRecord->email = $userRecord->email;
						$detailRecord->phone = $userRecord->phone;

						$userGroupIds = UserGroupRecord::getGroupIdsByUser($userRecord->id);
						if (isset($userGroupIds))
							foreach ($userGroupIds as $groupId)
							{
								/** @var $groupRecord GroupRecord */
								$groupRecord = GroupRecord::model()->findByPk($groupId);
								if (isset($groupRecord))
								{
									$group = new StatisticGroupRecord();
									$group->id_activity = $activityId;
									$group->name = $groupRecord->name;
									$group->save();
								}
							}
					}
				}
			}
			$detailRecord->save();
		}
	}
