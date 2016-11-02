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

		public function __construct(array $properties = array())
		{
			foreach ($properties as $key => $value)
			{
				if (is_array($value))
					$this->{$key} = LinkUserSettings::fromJsonArray($value);
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
			$userProfileModel->powerPointSettings = LinkUserSettings::getDefault();
			$userProfileModel->docSettings = LinkUserSettings::getDefault();
			$userProfileModel->xlsSettings = LinkUserSettings::getDefault();
			$userProfileModel->pdfSettings = LinkUserSettings::getDefault();
			$userProfileModel->imageSettings = LinkUserSettings::getDefault();
			return $userProfileModel;
		}
	}