<?
	/**
	 * Class LinkUserQuickSiteSettings
	 */
	class LinkUserQuickSiteSettings extends LinkUserQPageSettings
	{
		public function resetToDefault()
		{
			$this->expiresInDays = 7;
			$this->showLinksAsUrl = true;
			$this->disableWidgets = false;
			$this->disableBanners = false;
			$this->requireLogin = true;
			$this->requirePinCode = false;
			$this->autoLaunch = false;
		}

		/**
		 * @param array $jsonArray
		 * @return  LinkUserQuickSiteSettings
		 */
		public static function fromJsonArray($jsonArray)
		{
			$linkSettings = new self();
			$linkSettings->loadFromJsonArray($jsonArray);
			return $linkSettings;
		}
	}