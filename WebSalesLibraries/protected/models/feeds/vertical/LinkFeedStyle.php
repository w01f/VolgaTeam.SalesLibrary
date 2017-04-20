<?

	namespace application\models\feeds\vertical;

	/**
	 * Class LinkFeedStyle
	 */
	class LinkFeedStyle extends FeedStyle
	{
		const LinkNamePositionTop = 'top';
		const LinkNamePositionBottom = 'bottom';

		public $imageWidth;
		public $imageHeight;
		public $bodyPadding;
		public $linkSpace;
		public $linkNamePosition;
		public $showLinkCounter;

		public function __construct()
		{
			$this->imageWidth = 60;
			$this->imageHeight = 0;
			$this->bodyPadding = new \Padding(15);
			$this->linkSpace = 20;
			$this->linkNamePosition = self::LinkNamePositionBottom;
			$this->showLinkCounter = false;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./ImageWidth', $contextNode);
			$this->imageWidth = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->imageWidth;

			$queryResult = $xpath->query('./ImageHeight', $contextNode);
			$this->imageHeight = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->imageHeight;

			$queryResult = $xpath->query('./FeedBodyPadding', $contextNode);
			if ($queryResult->length > 0)
				$this->bodyPadding = \Padding::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./LinkSpace', $contextNode);
			$this->linkSpace = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->linkSpace;

			$queryResult = $xpath->query('./LinkNamePosition', $contextNode);
			$linksScrollMode = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $this->linkNamePosition;
			if (in_array($linksScrollMode, array(self::LinkNamePositionTop, self::LinkNamePositionBottom)))
				$this->linkNamePosition = $linksScrollMode;

			$queryResult = $xpath->query('./ShowLinkCounter', $contextNode);
			$this->showLinkCounter = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->showLinkCounter;
		}
	}