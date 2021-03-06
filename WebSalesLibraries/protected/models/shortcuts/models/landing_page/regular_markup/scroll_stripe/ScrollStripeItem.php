<?
	namespace application\models\shortcuts\models\landing_page\regular_markup\scroll_stripe;

	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;

	/**
	 * Class ScrollStripeItem
	 */
	class ScrollStripeItem extends ContentBlock
	{
		const IconSizeLarge = 'lg';
		const IconSize2x = '2x';
		const IconSize3x = '3x';
		const IconSize4x = '4x';

		public $text;

		public $icon;
		public $iconColor;
		public $iconHoverColor;
		public $backgroundColor;
		public $backgroundHoverColor;
		public $iconSize;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'scroll-stripe-item';

			$this->iconColor = '000000';
			$this->iconHoverColor = '000000';
			$this->iconSize = null;
			$this->backgroundColor = 'ffffff';
			$this->backgroundHoverColor = 'ffffff';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./Text', $contextNode);
			$this->text = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./Icon', $contextNode);
			$this->icon = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./IconColor', $contextNode);
			$this->iconColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->iconColor;

			$queryResult = $xpath->query('./IconHoverColor', $contextNode);
			$this->iconHoverColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->iconHoverColor;

			$queryResult = $xpath->query('./BackgroundColor', $contextNode);
			$this->backgroundColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->backgroundColor;

			$queryResult = $xpath->query('./BackgroundHoverColor', $contextNode);
			$this->backgroundHoverColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->backgroundHoverColor;

			$queryResult = $xpath->query('./IconSize', $contextNode);
			$iconSize = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $this->iconSize;
			if (in_array($iconSize, array(self::IconSizeLarge, self::IconSize2x, self::IconSize3x, self::IconSize4x)))
				$this->iconSize = $iconSize;
		}
	}