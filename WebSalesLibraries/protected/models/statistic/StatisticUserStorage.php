<?php
	class StatisticUserStorage extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		public function tableName()
		{
			return '{{statistic_user}}';
		}

		public static function WriteUserDetail($activityId)
		{
			if (isset(Yii::app()->user))
			{
				$userId = Yii::app()->user->getId();
				if (isset($userId))
				{
					$userRecord = UserStorage::model()->findByPk($userId);
					if (isset($userRecord))
					{
						$detailRecord = new StatisticUserStorage();
						$detailRecord->id_activity = $activityId;
						$detailRecord->login = $userRecord->login;
						$detailRecord->first_name = $userRecord->first_name;
						$detailRecord->last_name = $userRecord->last_name;
						$detailRecord->email = $userRecord->email;

						$userGroupIds = UserGroupStorage::getGroupIdsByUser($userRecord->id);
						if (isset($userGroupIds))
							foreach ($userGroupIds as $groupId)
							{
								$groupRecord = GroupStorage::model()->findByPk($groupId);
								if (isset($groupRecord))
								{
									$group = new StatisticGroupStorage();
									$group->id_activity = $activityId;
									$group->name = $groupRecord->name;
									$group->save();
								}
							}
						$detailRecord->ip = Yii::app()->request->getUserHostAddress();

						$platform = Yii::app()->browser->getPlatform();
						$browser = Yii::app()->browser->getBrowser();
						switch($platform)
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

						$detailRecord->save();
					}
				}
			}
		}
	}
