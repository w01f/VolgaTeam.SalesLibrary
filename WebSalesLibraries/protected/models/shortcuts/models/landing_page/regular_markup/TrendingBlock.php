<?

	namespace application\models\shortcuts\models\landing_page\regular_markup;

	use application\models\trending\TrendingBarManager;
	use application\models\trending\TrendingLink;
	use application\models\trending\TrendingSettings;

	/**
	 * Class TrendingBlock
	 */
	class TrendingBlock extends BlockContainer
	{
		public $id;

		/** @var  TrendingSettings */
		public $settings;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'trending';
			$this->id = uniqid();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		protected function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);
			$this->settings = TrendingSettings::fromXml($xpath, $contextNode);
		}

		/** @return TrendingLink[] */
		public function getTrendingLinks()
		{
			return TrendingBarManager::queryTrendingLinks($this->settings);
		}
	}