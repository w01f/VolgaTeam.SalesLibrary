<?

	/**
	 * Class EmptyShortcut
	 */
	class EmptyShortcut extends BaseShortcut
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
		public function getSourceLink()
		{
			return '#';
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return '';
		}
	}