<?
	namespace application\models\wallbin\models\web\style;

	/**
	 * FolderStyle
	 */
	class FolderStyle
	{
		public $showRegularHeader;

		public $showCustomTitle;
		public $showWidget;
		public $hideTopFoldersCustomTitle;
		public $textAlign;
		public $textColor;
		public $backColor;
		/** @var  \Font */
		public $font;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return FolderStyle
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$folderStyle = self::createDefault();

			$queryResult = $xpath->query('.//ShowTitle', $contextNode);
			$folderStyle->showCustomTitle = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $folderStyle->showCustomTitle;
			$folderStyle->showRegularHeader &= !$folderStyle->showCustomTitle;

			$queryResult = $xpath->query('.//ShowWidget', $contextNode);
			$folderStyle->showWidget = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $folderStyle->showWidget;

			$queryResult = $xpath->query('.//HideWindow1Title', $contextNode);
			$folderStyle->hideTopFoldersCustomTitle = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $folderStyle->hideTopFoldersCustomTitle;

			$queryResult = $xpath->query('.//TextAlignment', $contextNode);
			$folderStyle->textAlign = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $folderStyle->textAlign;

			$queryResult = $xpath->query('.//TextColor', $contextNode);
			$folderStyle->textColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $folderStyle->textColor;

			$queryResult = $xpath->query('.//BackColor', $contextNode);
			$folderStyle->backColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $folderStyle->backColor;

			$queryResult = $xpath->query('.//Font', $contextNode);
			if ($queryResult->length > 0)
				$folderStyle->font = \Font::fromXml($xpath, $queryResult->item(0));

			return $folderStyle;
		}

		/**
		 * @return FolderStyle
		 */
		public static function createDefault()
		{
			$folderStyle = new FolderStyle();
			$folderStyle->showRegularHeader = true;
			$folderStyle->showCustomTitle = false;
			$folderStyle->showWidget = false;
			$folderStyle->hideTopFoldersCustomTitle = false;
			$folderStyle->textAlign = 'left';
			$folderStyle->textColor = null;
			$folderStyle->backColor = null;
			$folderStyle->font = \Font::createDefault();
			return $folderStyle;
		}
	}