<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;

	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\style\TextAppearance;

	/**
	 * Class MasonryItem
	 */
	class MasonryItem extends ContentBlock
	{
		public $imagePath;
		public $imageWidth;
		public $imageHeight;

		public $title;
		public $description;

		/** @var  TextAppearance */
		public $titleTextAppearance;

		/** @var  TextAppearance */
		public $descriptionTextAppearance;

		/** @var  array */
		public $filterTags;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'masonry-item';

			$this->imageWidth = 0;
			$this->imageHeight = 0;
			$this->filterTags = array();
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

			$queryResult = $xpath->query('./ImageWidth', $contextNode);
			$this->imageWidth = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->imageWidth;

			$queryResult = $xpath->query('./ImageHeight', $contextNode);
			$this->imageHeight = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->imageHeight;

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

			$queryResult = $xpath->query('./FilterTag', $contextNode);
			foreach ($queryResult as $node)
				$this->filterTags[] = trim($node->nodeValue);
		}
	}