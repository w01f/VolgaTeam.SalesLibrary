<?

	/**
	 * Class ResetPasswordShortcut
	 */
	class ResetPasswordShortcut extends CustomHandledShortcut
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
			return 'Reset Password';
		}

		/**
		 * @return string
		 */
		public function getSourceLink()
		{
			return '#';
		}
	}