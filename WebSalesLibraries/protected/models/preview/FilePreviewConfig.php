<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class FilePreviewConfig
	 */
	class FilePreviewConfig extends BasePreviewConfig
	{
		public $forceDownload;
		public $forceOpen;

		/**
		 * @param $libraryLink LibraryLink
		 * @param $isQuickSite boolean
		 */
		public function init($libraryLink, $isQuickSite)
		{
			parent::init($libraryLink, $isQuickSite);
			$this->loadUserProfile($libraryLink);
		}

		/**
		 * @param $libraryLink LibraryLink
		 */
		protected function loadUserProfile($libraryLink)
		{
			$this->forceDownload = false;
			$this->forceOpen = false;

			if (UserIdentity::isUserAuthorized())
			{
				$userId = UserIdentity::getCurrentUserId();
				/** @var LinkUserProfileModel $userProfile */
				$userProfile = LinkUserProfileRecord::getProfile($userId);

				switch ($libraryLink->originalFormat)
				{
					case 'ppt':
						$this->forceDownload = $userProfile->powerPointSettings->forceOpen;
						$this->forceOpen = $userProfile->powerPointSettings->forceOpen;
						break;
					case 'doc':
						$this->forceDownload = $userProfile->docSettings->forceOpen;
						$this->forceOpen = $userProfile->docSettings->forceOpen;
						break;
					case 'xls':
						$this->forceDownload = $userProfile->xlsSettings->forceOpen;
						$this->forceOpen = $userProfile->xlsSettings->forceOpen;
						break;
					case 'pdf':
						$this->forceDownload = $userProfile->pdfSettings->forceOpen;
						$this->forceOpen = $userProfile->pdfSettings->forceOpen;
						break;
					case 'png':
					case 'jpeg':
						$this->forceDownload = $userProfile->imageSettings->forceOpen;
						$this->forceOpen = $userProfile->imageSettings->forceOpen;
						break;
				}
			}
		}
	}