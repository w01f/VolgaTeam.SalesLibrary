<?

	/**
	 * Class ShortcutServiceProfileRecord
	 * @property string id
	 * @property string name
	 * @property string service_type
	 * @property string config
	 */
	class ShortcutServiceProfileRecord extends CActiveRecord
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
			return '{{shortcut_service_profile}}';
		}

		/**
		 * @return BaseServiceProfile
		 */
		public function getModel()
		{
			$profileModel = null;

			switch ($this->service_type)
			{
				case BaseServiceProfile::ServiceTypeDataQueryCache:
					$profileModel = new ShortcutDataQueryCacheServiceProfile();
					break;
				default:
					throw new Exception('Undefined srvice type');
			}
			$profileModel->id = $this->id;
			$profileModel->name = $this->name;
			$profileModel->type = $this->service_type;
			$profileModel->loadConfig($this->config);

			return $profileModel;
		}

		/**
		 * @param $serviceType string
		 * @return BaseServiceProfile[]
		 * @throws Exception
		 */
		public static function getProfiles($serviceType)
		{
			$profileRecords = self::model()->findAll(
				array(
					'condition' => "service_type='" . $serviceType . "'",
					'order' => 't.name'));
			$profileModels = array();
			foreach ($profileRecords as $profileRecord)
			{
				/** @var  $profileRecord ShortcutServiceProfileRecord */
				$profileModels[] = $profileRecord->getModel();
			}
			return $profileModels;
		}

		/**
		 * @param $profileModel BaseServiceProfile
		 * @param $serviceType string
		 */
		public static function saveProfile($profileModel, $serviceType)
		{
			/** @var  $profileRecord ShortcutServiceProfileRecord */
			$profileRecord = self::model()->findByPk($profileModel->id);
			if (!isset($profileRecord))
			{
				$profileRecord = new self();
				$profileRecord->id = !empty($profileModel->id) ? $profileModel->id : uniqid();
			}
			$profileRecord->name = $profileModel->name;
			$profileRecord->service_type = !empty($profileModel->type) ? $profileModel->type : $serviceType;
			$profileRecord->config = CJSON::encode($profileModel->getEncodedConfig());
			$profileRecord->save();
		}

		/**
		 * @param $profileModel BaseServiceProfile
		 */
		public static function deleteProfile($profileModel)
		{
			self::model()->deleteByPk($profileModel->id);
		}
	}