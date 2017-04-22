<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;

	use application\models\feeds\common\FeedItemSettings;

	/**
	 * Class MasonryFeedItemSettings
	 */
	class MasonryFeedItemSettings extends FeedItemSettings
	{
		const TextAlignLeft = 'left';
		const TextAlignCenter = 'center';
		const TextAlignRight = 'right';

		public $wrapText;
		public $textAlign;


		/** @param string $tag */
		public function __construct($tag)
		{
			parent::__construct($tag);
			$this->wrapText = false;
			$this->textAlign = self::TextAlignCenter;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./TextWrap', $contextNode);
			$this->wrapText = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->wrapText;

			$queryResult = $xpath->query('./TextAlign', $contextNode);
			$textAlign = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $this->textAlign;
			if (in_array($textAlign, array(self::TextAlignLeft, self::TextAlignCenter, self::TextAlignRight)))
				$this->textAlign = $textAlign;
		}
	}