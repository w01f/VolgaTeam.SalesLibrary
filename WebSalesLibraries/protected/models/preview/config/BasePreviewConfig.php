<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class BasePreviewConfig
	 */
	class BasePreviewConfig
	{
		public $userAuthorized;

		public $allowPreview;
		public $allowDownload;
		public $allowSave;
		public $allowEmail;
		public $allowPdf;
		public $allowAddToFavorites;
		public $allowAddToQuickSite;

		public $enableLogging;
		public $enableRating;

		public $isEOBrowser;

		/**
		 * @param $libraryLink LibraryLink
		 * @param $isQuickSite boolean
		 */
		public function init($libraryLink, $isQuickSite)
		{
			$this->isEOBrowser = Yii::app()->browser->getBrowser() == Browser::BROWSER_EO;
			$this->initDefaults($isQuickSite);
			$this->loadConfigProfiles($libraryLink);
		}

		/**
		 * @param $isQuickSite boolean
		 */
		private function initDefaults($isQuickSite)
		{
			$this->userAuthorized = UserIdentity::isUserAuthorized();

			$this->allowPreview = true;
			$this->allowSave = true;
			$this->allowEmail = !$isQuickSite && $this->userAuthorized;

			$this->allowDownload = true;
			$this->allowPdf = true;
			$this->allowAddToFavorites = $this->userAuthorized;
			$this->allowAddToQuickSite = !$isQuickSite && $this->userAuthorized;

			$this->enableLogging = $this->userAuthorized;
			$this->enableRating = $this->userAuthorized;
		}

		/**
		 * @param $libraryLink LibraryLink
		 */
		private function loadConfigProfiles($libraryLink)
		{
			$profiles = LinkConfigProfileRecord::getProfiles();
			foreach ($profiles as $profile)
			{
				if ($profile->config->isAffectToLibraryLink($libraryLink))
				{
					$this->allowPreview &= !$profile->config->disablePreview;
					$this->allowSave &= !$profile->config->disableSave;
					$this->allowEmail &= !$profile->config->disableEmail;

					$this->allowDownload &= !$profile->config->disableDownload;
					$this->allowPdf &= !$profile->config->disablePdf;
					$this->allowAddToFavorites &= !$profile->config->disableFavorites;
					$this->allowAddToQuickSite &= !$profile->config->disableQuickSite;
				}
			}
		}
	}