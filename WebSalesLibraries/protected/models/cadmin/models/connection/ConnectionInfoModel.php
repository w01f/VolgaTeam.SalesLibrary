<?

	/**
	 * Class ConnectionInfoModel
	 */
	class ConnectionInfoModel
	{
		const ConnectionStateActive = 1;
		const ConnectionStateBusy = 2;

		public $state;
		public $user;
		public $connectionTime;
		public $libraryId;
		public $libraryName;
	}