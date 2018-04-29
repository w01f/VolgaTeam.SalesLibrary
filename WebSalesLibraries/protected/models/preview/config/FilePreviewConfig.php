<?

	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class FilePreviewConfig
	 */
	class FilePreviewConfig extends BasePreviewConfig
	{
		public $forceDownload;
		public $forceEOOpen;
		public $forceWebOpen;
		public $forceOpenGallery;

		/**
		 * @param $libraryLink LibraryLink
		 * @param $isQuickSite boolean
		 */
		public function init($libraryLink, $isQuickSite, $openFromBundle)
		{
			parent::init($libraryLink, $isQuickSite, $openFromBundle);
			if (!$openFromBundle)
				$this->loadUserProfile($libraryLink);
		}

		/**
		 * @param $libraryLink LibraryLink
		 */
		protected function loadUserProfile($libraryLink)
		{
			$this->forceDownload = false;
			$this->forceEOOpen = false;
			$this->forceWebOpen = false;
			$this->forceOpenGallery = false;

			if (UserIdentity::isUserAuthorized())
			{
				$userId = UserIdentity::getCurrentUserId();
				/** @var LinkUserProfileModel $userProfile */
				$userProfile = LinkUserProfileRecord::getProfile($userId);

				switch ($libraryLink->originalFormat)
				{
					case 'ppt':
						$this->forceDownload = $userProfile->powerPointSettings->forceEOOpen;
						$this->forceEOOpen = $userProfile->powerPointSettings->forceEOOpen;
						$this->forceWebOpen = $userProfile->powerPointSettings->forceWebOpen;
						$this->forceOpenGallery = $userProfile->powerPointSettings->forceOpenGallery;
						break;
					case 'doc':
						$this->forceDownload = $userProfile->docSettings->forceEOOpen;
						$this->forceEOOpen = $userProfile->docSettings->forceEOOpen;
						$this->forceWebOpen = $userProfile->docSettings->forceWebOpen;
						$this->forceOpenGallery = $userProfile->docSettings->forceOpenGallery;
						break;
					case 'xls':
						$this->forceDownload = $userProfile->xlsSettings->forceEOOpen;
						$this->forceEOOpen = $userProfile->xlsSettings->forceEOOpen;
						$this->forceWebOpen = $userProfile->xlsSettings->forceWebOpen;
						$this->forceOpenGallery = $userProfile->xlsSettings->forceOpenGallery;
						break;
					case 'pdf':
						$this->forceDownload = $userProfile->pdfSettings->forceEOOpen;
						$this->forceEOOpen = $userProfile->pdfSettings->forceEOOpen;
						$this->forceWebOpen = $userProfile->pdfSettings->forceWebOpen;
						$this->forceOpenGallery = $userProfile->pdfSettings->forceOpenGallery;
						break;
					case 'png':
					case 'jpeg':
					case 'gif':
						$this->forceDownload = $userProfile->imageSettings->forceEOOpen;
						$this->forceEOOpen = $userProfile->imageSettings->forceEOOpen;
						$this->forceWebOpen = $userProfile->imageSettings->forceWebOpen;
						$this->forceOpenGallery = $userProfile->imageSettings->forceOpenGallery;
						break;
				}
			}
		}
	}