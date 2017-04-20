<?

	namespace application\models\feeds\vertical;

	/**
	 * Class LinkFeedSettings
	 */
	class LinkFeedSettings extends FeedSettings
	{
		public function __construct()
		{
			parent::__construct();
			$this->style = new LinkFeedStyle();
		}
	}