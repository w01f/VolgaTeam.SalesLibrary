<?php

	/**
	 * Class UserProfileRecord
	 * @property string id
	 * @property mixed id_user
	 * @property string config
	 */
	class UserProfileRecord extends CActiveRecord
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
			return '{{link_user_profile}}';
		}

		/**
		 * @return UserProfileModel
		 */
		public function getModel()
		{
			$configProfileModel = new UserProfileModel(CJSON::decode($this->config, true));
			return $configProfileModel;
		}

		/**
		 * @param $userId int
		 * @return UserProfileModel
		 */
		public static function getProfile($userId)
		{
			/** @var  $userProfileRecord UserProfileRecord */
			$userProfileRecord = self::model()->find('id_user=?', array($userId));
			if (isset($userProfileRecord))
				return $userProfileRecord->getModel();
			return UserProfileModel::getDefault();
		}

		/**
		 * @param $userId int
		 * @param $profileModel UserProfileModel
		 */
		public static function saveProfile($userId, $profileModel)
		{
			/** @var  $userProfileRecord UserProfileRecord */
			$userProfileRecord = self::model()->find('id_user=?', array($userId));
			if (!isset($userProfileRecord))
			{
				$userProfileRecord = new UserProfileRecord();
				$userProfileRecord->id = uniqid();
				$userProfileRecord->id_user = $userId;
			}
			$userProfileRecord->config = CJSON::encode($profileModel);
			$userProfileRecord->save();
		}
	}