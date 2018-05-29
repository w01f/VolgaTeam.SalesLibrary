<?

	use application\models\data_query\link_feed\LinkFeedQuerySettings;

	/**
	 * Interface IDataQueryableBlock
	 */
	interface IDataQueryableBlock
	{
		/**
		 * @return LinkFeedQuerySettings
		 */
		public function getQuerySettings();
	}


