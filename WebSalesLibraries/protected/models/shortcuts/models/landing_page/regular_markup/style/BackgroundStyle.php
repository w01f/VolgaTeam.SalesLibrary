<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\style;

	/**
	 * Class BackgroundStyle
	 */
	class BackgroundStyle
	{
		const GradientTypeNone = 'none';
		const GradientTypeToTop = 'to top';
		const GradientTypeToLeft = 'to left';
		const GradientTypeToRight = 'to right';
		const GradientTypeToBottom = 'to bottom';
		const GradientTypeToTopLeft = 'to top left';
		const GradientTypeToTopRight = 'to top right';
		const GradientTypeToBottomLeft = 'to bottom left';
		const GradientTypeToBottomRight = 'to bottom right';

		public $image;
		public $overlayColor;
		public $overlayOpacity;
		public $color1;
		public $color2;
		public $gradient;

		public function __construct()
		{
			$this->gradient = self::GradientTypeNone;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @param $imagePath string
		 * @return BackgroundStyle
		 */
		public static function fromXml($xpath, $contextNode, $imagePath)
		{
			$backgroundStyle = new BackgroundStyle();

			$queryResult = $xpath->query('./UseImage', $contextNode);
			$fileName = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			if (!empty($fileName))
				$backgroundStyle->image = $imagePath . $fileName;

			$queryResult = $xpath->query('./Overlay/Color', $contextNode);
			$backgroundStyle->overlayColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./Overlay/Opacity', $contextNode);
			$backgroundStyle->overlayOpacity = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue))/100 : 1;

			$queryResult = $xpath->query('./Color', $contextNode);
			$backgroundStyle->color1 = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./Color1', $contextNode);
			$backgroundStyle->color1 = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $backgroundStyle->color1;

			$queryResult = $xpath->query('./Color2', $contextNode);
			$backgroundStyle->color2 = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./Gradient', $contextNode);
			$gradient = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $backgroundStyle->gradient;
			if (in_array($gradient, array(self::GradientTypeNone,
				self::GradientTypeToTop,
				self::GradientTypeToLeft,
				self::GradientTypeToBottom,
				self::GradientTypeToRight,
				self::GradientTypeToTopLeft,
				self::GradientTypeToTopRight,
				self::GradientTypeToBottomLeft,
				self::GradientTypeToBottomRight
			)))
				$backgroundStyle->gradient = $gradient;

			return $backgroundStyle;
		}
	}