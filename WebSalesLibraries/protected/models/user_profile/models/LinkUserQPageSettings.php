<?

	/**
	 * Class LinkUserQPageSettings
	 */
	abstract class LinkUserQPageSettings
	{
		public $expiresInDays;
		public $showLinksAsUrl;
		public $disableWidgets;
		public $disableBanners;
		public $requireLogin;
		public $requirePinCode;
		public $autoLaunch;

		public function __construct()
		{
			$this->resetToDefault();
		}

		/**
		 * @param array $jsonArray
		 */
		public function loadFromJsonArray($jsonArray)
		{
			foreach ($jsonArray as $key => $value)
			{
				switch ($key)
				{
					case 'expiresInDays':
						$this->expiresInDays = $value;
						break;
					case 'showLinksAsUrl':
						$this->showLinksAsUrl = $value;
						break;
					case 'disableWidgets':
						$this->disableWidgets = $value;
						break;
					case 'disableBanners':
						$this->disableBanners = $value;
						break;
					case 'requireLogin':
						$this->requireLogin = $value;
						break;
					case 'requirePinCode':
						$this->requirePinCode = $value;
						break;
					case 'autoLaunch':
						$this->autoLaunch = $value;
						break;
				}
			}
		}

		public abstract function resetToDefault();
	}