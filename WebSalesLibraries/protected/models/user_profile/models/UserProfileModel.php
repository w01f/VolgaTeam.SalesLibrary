<?

	/**
	 * Class UserProfileModel
	 */
	class UserProfileModel
	{
		/** @var  LinkUserPreviewSettings */
		public $powerPointSettings;
		/** @var  LinkUserPreviewSettings */
		public $docSettings;
		/** @var  LinkUserPreviewSettings */
		public $xlsSettings;
		/** @var  LinkUserPreviewSettings */
		public $pdfSettings;
		/** @var  LinkUserPreviewSettings */
		public $imageSettings;
		/** @var  LinkUserQPageSettings */
		public $defaultQPageSettings;
		/** @var  LinkUserQPageSettings */
		public $defaultEmailSettings;

		public function __construct(array $properties = array())
		{
			$this->powerPointSettings = LinkUserPreviewSettings::getDefault();
			$this->docSettings = LinkUserPreviewSettings::getDefault();
			$this->xlsSettings = LinkUserPreviewSettings::getDefault();
			$this->pdfSettings = LinkUserPreviewSettings::getDefault();
			$this->imageSettings = LinkUserPreviewSettings::getDefault();
			$this->defaultQPageSettings = new LinkUserQuickSiteSettings();
			$this->defaultEmailSettings = new LinkUserEmailSettings();

			foreach ($properties as $key => $value)
			{
				if (is_array($value))
				{
					switch ($key)
					{
						case 'defaultEmailSettings':
							$this->{$key} = LinkUserEmailSettings::fromJsonArray($value);
							break;
						case 'defaultQPageSettings':
							$this->{$key} = LinkUserQuickSiteSettings::fromJsonArray($value);
							break;
						default:
							$this->{$key} = LinkUserPreviewSettings::fromJsonArray($value);
							break;
					}
				}
				else
					$this->{$key} = $value;
			}
		}

		/**
		 * @return UserProfileModel
		 */
		public static function getDefault()
		{
			$userProfileModel = new UserProfileModel();
			$userProfileModel->powerPointSettings = LinkUserPreviewSettings::getDefault();
			$userProfileModel->docSettings = LinkUserPreviewSettings::getDefault();
			$userProfileModel->xlsSettings = LinkUserPreviewSettings::getDefault();
			$userProfileModel->pdfSettings = LinkUserPreviewSettings::getDefault();
			$userProfileModel->imageSettings = LinkUserPreviewSettings::getDefault();
			$userProfileModel->defaultQPageSettings = new LinkUserQuickSiteSettings();
			$userProfileModel->defaultEmailSettings = new LinkUserEmailSettings();
			return $userProfileModel;
		}
	}