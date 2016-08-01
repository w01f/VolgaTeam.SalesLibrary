<?php

	/**
	 * Class LinkConfigProfileRecord
	 * @property string id
	 * @property string name
	 * @property int order
	 * @property string config
	 */
	class LinkConfigProfileRecord extends CActiveRecord
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
			return '{{link_config_profile}}';
		}

		/**
		 * @return LinkConfigProfileModel
		 */
		public function getModel()
		{
			$configProfileModel = new LinkConfigProfileModel();

			$configProfileModel->id = $this->id;
			$configProfileModel->name = $this->name;
			$configProfileModel->order = $this->order;

			$configProfileModel->config = new LinkProfileSettings(CJSON::decode($this->config, true));

			return $configProfileModel;
		}

		/**
		 * @return LinkConfigProfileModel[]
		 */
		public static function getProfiles()
		{
			$configProfileRecords = self::model()->findAll();
			$configProfileModels = array();
			foreach ($configProfileRecords as $configProfileRecord)
			{
				/** @var  $configProfileRecord LinkConfigProfileRecord */
				$configProfileModels[] = $configProfileRecord->getModel();
			}
			return $configProfileModels;
		}

		/**
		 * @param $profileModel LinkConfigProfileModel
		 */
		public static function saveProfile($profileModel)
		{
			/** @var  $configProfileRecord LinkConfigProfileRecord */
			$configProfileRecord = self::model()->findByPk($profileModel->id);
			if (!isset($configProfileRecord))
			{
				$configProfileRecord = new LinkConfigProfileRecord();
				$configProfileRecord->id = $profileModel->id;
			}
			$configProfileRecord->name = $profileModel->name;
			$configProfileRecord->order = $profileModel->order;
			$configProfileRecord->config = CJSON::encode($profileModel->config);
			$configProfileRecord->save();
		}

		/**
		 * @param $profileModel LinkConfigProfileModel
		 */
		public static function deleteProfile($profileModel)
		{
			self::model()->deleteByPk($profileModel->id);
		}
	}