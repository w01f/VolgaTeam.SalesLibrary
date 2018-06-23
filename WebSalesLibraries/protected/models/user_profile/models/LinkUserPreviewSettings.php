<?

	/**
	 * Class LinkUserPreviewSettings
	 */
	class LinkUserPreviewSettings
	{
		public $forceWebOpen;
		public $forceEOOpen;
		public $forceOneDriveOpen;
		public $forceOpenGallery;

		/**
		 * @param array $jsonArray
		 * @return  LinkUserPreviewSettings
		 */
		public static function fromJsonArray($jsonArray)
		{
			$linkSettings = self::getDefault();
			foreach ($jsonArray as $key => $value)
			{
				switch ($key)
				{
					case 'forceEOOpen':
					case 'forceOpen':
						$linkSettings->forceEOOpen = $value;
						break;
					case 'forceWebOpen':
						$linkSettings->forceWebOpen = $value;
						break;
					case 'forceOneDriveOpen':
						$linkSettings->forceOneDriveOpen = $value;
						break;
					case 'forceOpenGallery':
						$linkSettings->forceOpenGallery = $value;
						break;
				}
			}
			return $linkSettings;
		}

		/**
		 * @return  LinkUserPreviewSettings
		 */
		public static function getDefault()
		{
			$linkSettings = new LinkUserPreviewSettings();
			$linkSettings->forceWebOpen = false;
			$linkSettings->forceEOOpen = false;
			$linkSettings->forceOneDriveOpen = false;
			$linkSettings->forceOpenGallery = false;
			return $linkSettings;
		}

		/**
		 * @return  boolean
		 */
		public function isDefault()
		{
			return !($this->forceEOOpen || $this->forceWebOpen || $this->forceOneDriveOpen || $this->forceOpenGallery);
		}
	}