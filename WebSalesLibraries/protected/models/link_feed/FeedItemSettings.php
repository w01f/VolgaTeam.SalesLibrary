<?
	namespace application\models\link_feed;

	/**
	 * Class FeedItemSettings
	 */
	class FeedItemSettings
	{
		const DataItemTagName = 'name';
		const DataItemTagLibrary = 'library';
		const DataItemTagViewsCount = 'views count';

		public static $tags = array(
			self::DataItemTagName,
			self::DataItemTagLibrary,
			self::DataItemTagViewsCount
		);

		public $enabled;
		/** @var  \Font */
		public $font;
		public $color;

		/** @param string $tag */
		public function __construct($tag)
		{
			switch ($tag)
			{
				case self::DataItemTagName:
				case self::DataItemTagLibrary:
				case self::DataItemTagViewsCount:
					$this->enabled = true;
					break;
			}
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			$queryResult = $xpath->query('./Enable', $contextNode);
			$this->enabled = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->enabled;

			$queryResult = $xpath->query('./Color', $contextNode);
			$this->color = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./Font', $contextNode);
			if($queryResult->length > 0)
				$this->font = \Font::fromXml($xpath, $queryResult->item(0));
		}
	}