<?php

	/**
	 * Class LinkUserProfileRecord
	 * @property string id
	 * @property mixed id_user
	 * @property string config
	 */
	class LinkUserProfileRecord extends CActiveRecord
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
		 * @return LinkUserProfileRecord
		 */
		public function getModel()
		{
			$configProfileModel = new LinkUserProfileModel(CJSON::decode($this->config, true));
			return $configProfileModel;
		}

		/**
		 * @param $userId int
		 * @return LinkUserProfileModel
		 */
		public static function getProfile($userId)
		{
			/** @var  $userProfileRecord LinkUserProfileRecord */
			$userProfileRecord = self::model()->find('id_user', $userId);
			if(isset($userProfileRecord))
				return $userProfileRecord->getModel();
			return LinkUserProfileModel::getDefault();
		}

		/**
		 * @param $userId int
		 * @param $profileModel LinkUserProfileModel
		 */
		public static function saveProfile($userId, $profileModel)
		{
			/** @var  $userProfileRecord LinkUserProfileRecord */
			$userProfileRecord = self::model()->find('id_user', $userId);
			if (!isset($userProfileRecord))
			{
				$userProfileRecord = new LinkUserProfileRecord();
				$userProfileRecord->id = uniqid();
				$userProfileRecord->id_user = $userId;
			}
			$userProfileRecord->config = CJSON::encode($profileModel);
			$userProfileRecord->save();
		}
	}