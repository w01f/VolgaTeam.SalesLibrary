<?

	namespace application\models\news_block;

	/**
	 * Class NewsBlockSettings
	 */
	class NewsBlockSettings
	{
		const NewsScrollDirectionUp = 'up';
		const NewsScrollDirectionDown = 'down';

		public $title;
		public $itemsCount;
		public $autoPlay;
		public $direction;
		public $tickerInterval;

		public function __construct()
		{
			$this->title = 'News';
			$this->itemsCount = 3;
			$this->autoPlay = true;
			$this->direction = self::NewsScrollDirectionUp;
			$this->tickerInterval = 4000;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return NewsBlockSettings
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = new self();

			$queryResult = $xpath->query('./Title', $contextNode);
			$instance->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $instance->itemsCount;

			$queryResult = $xpath->query('./VisibleItems', $contextNode);
			$instance->itemsCount = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $instance->itemsCount;

			$queryResult = $xpath->query('./AutoPlay', $contextNode);
			$instance->autoPlay = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $instance->autoPlay;

			$queryResult = $xpath->query('./Direction', $contextNode);
			$direction = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $instance->direction;
			if (in_array($direction, array(self::NewsScrollDirectionUp, self::NewsScrollDirectionDown)))
				$instance->direction = $direction;

			$queryResult = $xpath->query('./TickerInterval', $contextNode);
			$instance->tickerInterval = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $instance->itemsCount;

			return $instance;
		}
	}