<?

	/**
	 * Class LinkUserProfileModel
	 */
	class LinkUserProfileModel
	{
		/** @var  LinkUserSettings */
		public $powerPointSettings;
		/** @var  LinkUserSettings */
		public $docSettings;
		/** @var  LinkUserSettings */
		public $xlsSettings;
		/** @var  LinkUserSettings */
		public $pdfSettings;
		/** @var  LinkUserSettings */
		public $imageSettings;

		public function __construct(Array $properties = array())
		{
			foreach ($properties as $key => $value)
			{
				if (is_array($value))
					$this->{$key} = CJSON::decode(CJSON::encode($value), false);
				else
					$this->{$key} = $value;
			}
		}

		/**
		 * @return LinkUserProfileModel
		 */
		public static function getDefault()
		{
			$userProfileModel = new LinkUserProfileModel();

			$userProfileModel->powerPointSettings = new LinkUserSettings();
			$userProfileModel->powerPointSettings->forceOpen = false;

			$userProfileModel->docSettings = new LinkUserSettings();
			$userProfileModel->docSettings->forceOpen = false;

			$userProfileModel->xlsSettings = new LinkUserSettings();
			$userProfileModel->xlsSettings->forceOpen = false;

			$userProfileModel->pdfSettings = new LinkUserSettings();
			$userProfileModel->pdfSettings->forceOpen = false;

			$userProfileModel->imageSettings = new LinkUserSettings();
			$userProfileModel->imageSettings->forceOpen = false;

			return $userProfileModel;
		}
	}