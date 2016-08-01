<?

	/**
	 * Class UserPreferencesShortcut
	 */
	class UserPreferencesShortcut extends BaseShortcut
	{
		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'User Preferences';
		}

		/**
		 * @return string
		 */
		public function getSourceLink()
		{
			return '#';
		}
	}