<?
	/**
	 * Class ShortcutAppearance
	 */
	class ShortcutAppearance
	{
		public $size;
		public $textSize;
		public $iconSize;
		public $textAlign;

		public $backColor;
		public $textColor;
		public $iconColor;
		public $shadowColor;
		public $useGradient;

		/**
		 * @param $xpath DOMXPath
		 * @param $contextNode DOMNode
		 * @return ShortcutAppearance
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = new self();

			/** @var $queryResult DOMNodeList */
			$queryResult = $xpath->query('./Size', $contextNode);
			$instance->size = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'regular';

			$queryResult = $xpath->query('./TextSize', $contextNode);
			$instance->textSize = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./IconSize', $contextNode);
			$instance->iconSize = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./TextAlign', $contextNode);
			$instance->textAlign = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : 'center';

			$queryResult = $xpath->query('./BackColor', $contextNode);
			$instance->backColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : Yii::app()->params['menu']['BarColor'];

			$queryResult = $xpath->query('./TextColor', $contextNode);
			$instance->textColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'FFFFFF';

			$queryResult = $xpath->query('./IconColor', $contextNode);
			$instance->iconColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'FFFFFF';

			$queryResult = $xpath->query('./ShadowColor', $contextNode);
			$instance->shadowColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'FFFFFF';

			$queryResult = $xpath->query('./UseGradient', $contextNode);
			$instance->useGradient = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : null;

			return $instance;
		}
	}