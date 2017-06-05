<?

	namespace application\models\feeds\common;


	class FeedDetailsControlSettings extends FeedControlSettings
	{
		public $iconFile;
		public $iconColor;
		public $backColor;
		public $borderColor;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./IconFile', $contextNode);
			$this->iconFile	= $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->iconFile;

			$queryResult = $xpath->query('./IconColor', $contextNode);
			$this->iconColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->iconColor;

			$queryResult = $xpath->query('./BackColor', $contextNode);
			$this->backColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->backColor;

			$queryResult = $xpath->query('./BorderColor', $contextNode);
			$this->borderColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->borderColor;
		}
	}