<?

	namespace application\models\feeds\vertical;

	/**
	 * Class SimpleFeedSettings
	 */
	class SimpleFeedSettings extends FeedSettings
	{
		public function __construct()
		{
			$this->feedType = self::FeedTypeNews;
			parent::__construct();
		}
	}