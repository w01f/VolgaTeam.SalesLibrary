<?

	/**
	 * Class ShortcutDataQueryCacheServiceProfile
	 */
	class ShortcutDataQueryCacheServiceProfile extends BaseServiceProfile
	{
		/**
		 * @var string[]
		 * @soap
		 */
		public $shortcutIds;


		public function __construct()
		{
			$this->shortcutIds = array();
		}

		/**
		 * @param $encodedConfig
		 */
		public function loadConfig($encodedConfig)
		{
			if (!empty($encodedConfig))
			{
				$this->shortcutIds = explode(";", str_replace('"','',$encodedConfig));
			}
		}

		/**
		 * @return string
		 */
		public function getEncodedConfig()
		{
			return implode(";", $this->shortcutIds);
		}
	}