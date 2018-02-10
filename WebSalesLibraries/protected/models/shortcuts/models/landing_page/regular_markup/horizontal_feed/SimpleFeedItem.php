<?
	namespace application\models\shortcuts\models\landing_page\regular_markup\horizontal_feed;

	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\style\TextAppearance;

	/**
	 * Class SimpleFeedItem
	 */
	class SimpleFeedItem extends ContentBlock
	{
		public $imagePath;

		public $title;
		public $description;

		/** @var  TextAppearance */
		public $titleTextAppearance;

		/** @var  TextAppearance */
		public $descriptionTextAppearance;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'simple-horizontal-feed-item';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./Image', $contextNode);
			$fileName = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			$this->imagePath = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . $this->parentShortcut->relativeLink . '/images/' . $fileName);

			$queryResult = $xpath->query('./Title', $contextNode);
			$this->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./Description', $contextNode);
			$this->description = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./TitleStyle', $contextNode);
			if ($queryResult->length > 0)
				$this->titleTextAppearance = TextAppearance::fromXml($xpath, $queryResult->item(0));
			else
				$this->titleTextAppearance = TextAppearance::createEmpty();

			$queryResult = $xpath->query('./DescriptionStyle', $contextNode);
			if ($queryResult->length > 0)
				$this->descriptionTextAppearance = TextAppearance::fromXml($xpath, $queryResult->item(0));
			else
				$this->descriptionTextAppearance = TextAppearance::createEmpty();
		}
	}