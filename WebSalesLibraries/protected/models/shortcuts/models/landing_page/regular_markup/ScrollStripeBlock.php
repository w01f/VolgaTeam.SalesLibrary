<?

	namespace application\models\shortcuts\models\landing_page\regular_markup;
	/**
	 * Class ScrollStripeBlock
	 */
	class ScrollStripeBlock extends BlockContainer
	{
		const IconPositionTop = 'top';
		const IconPositionLeft = 'left';
		const StripeSizeNormal = 'normal';
		const StripeSizeMedium = 'medium';
		const StripeSizeLarge = 'large';

		public $id;
		public $iconPosition;
		public $stripeSize;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->id = uniqid();
			$this->type = 'scroll-stripe';

			$this->iconPosition = self::IconPositionLeft;
			$this->stripeSize = self::StripeSizeNormal;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		protected function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./IconPosition', $contextNode);
			$iconPosition = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $this->iconPosition;
			if (in_array($iconPosition, array(self::IconPositionTop, self::IconPositionLeft)))
				$this->iconPosition = $iconPosition;

			$queryResult = $xpath->query('./TabSize', $contextNode);
			$stripeSize = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $this->stripeSize;
			if (in_array($stripeSize, array(self::StripeSizeNormal, self::StripeSizeMedium, self::StripeSizeLarge)))
				$this->stripeSize = $stripeSize;

			$queryResult = $xpath->query('./ContentBlock', $contextNode);
			foreach ($queryResult as $node)
			{
				$typeAttribute = $node->attributes->getNamedItem("Type");
				$type = isset($typeAttribute) ? strtolower(trim($typeAttribute->nodeValue)) : null;
				$contentBlock = null;
				switch ($type)
				{
					case "url":
						$contentBlock = new ScrollStripeUrl($this->parentShortcut, $this);
						$contentBlock->configureFromXml($xpath, $node);
						break;
					case "shortcut":
						$contentBlock = new ScrollStripeShortcut($this->parentShortcut, $this);
						$contentBlock->configureFromXml($xpath, $node);
						break;
				}
				if (isset($contentBlock))
					$this->items[] = $contentBlock;
			}
		}
	}