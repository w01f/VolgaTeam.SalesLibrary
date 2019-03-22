<?
	/**
	 * Class LinkUserEmailSettings
	 */
	class LinkUserEmailSettings extends LinkUserQPageSettings
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
		 * @return  LinkUserEmailSettings
		 */
		public static function fromJsonArray($jsonArray)
		{
			$linkSettings = new self();
			$linkSettings->loadFromJsonArray($jsonArray);
			return $linkSettings;
		}
	}