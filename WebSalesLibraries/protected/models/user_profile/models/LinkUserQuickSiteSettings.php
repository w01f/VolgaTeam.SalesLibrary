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
			$this->requireLogin = false;
			$this->requirePinCode = false;
			$this->autoLaunch = true;
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