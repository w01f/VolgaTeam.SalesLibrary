<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\scroll_stripe;

	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;

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

		public $iconPosition;
		public $stripeSize;

		public $leftButtonColor;
		public $leftButtonDisabledColor;
		public $rightButtonColor;
		public $rightButtonDisabledColor;

		/**
		 * @param $parentShortcut \PageContentShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'scroll-stripe';

			$this->iconPosition = self::IconPositionLeft;
			$this->stripeSize = self::StripeSizeNormal;

			$this->leftButtonColor = '000000';
			$this->leftButtonDisabledColor = '000000';
			$this->rightButtonColor = '000000';
			$this->rightButtonDisabledColor = '000000';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
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

			$queryResult = $xpath->query('./LeftArrowColor/Enabled', $contextNode);
			$this->leftButtonColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->leftButtonColor;

			$queryResult = $xpath->query('./LeftArrowColor/Disabled', $contextNode);
			$this->leftButtonDisabledColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->leftButtonDisabledColor;

			$queryResult = $xpath->query('./RightArrowColor/Enabled', $contextNode);
			$this->rightButtonColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->rightButtonColor;

			$queryResult = $xpath->query('./RightArrowColor/Disabled', $contextNode);
			$this->rightButtonDisabledColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->rightButtonDisabledColor;

			if (!$this->parentShortcut->usePermissions || $this->isAccessGranted)
			{
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
					if (isset($contentBlock) && $contentBlock->isAccessGranted)
						$this->items[] = $contentBlock;
				}
			}
		}
	}