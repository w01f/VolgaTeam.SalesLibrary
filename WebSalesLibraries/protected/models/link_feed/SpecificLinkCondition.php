<?
	namespace application\models\link_feed;

	/**
	 * Class SpecificLinkCondition
	 */
	class SpecificLinkCondition
	{
		public $libraryName;
		public $pageName;
		public $folderName;
		public $linkName;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return SpecificLinkCondition
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = new self();

			/** @var $queryResult \DOMNodeList */
			$queryResult = $xpath->query('./LibraryName', $contextNode);
			$instance->libraryName = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./PageName', $contextNode);
			$instance->pageName = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./WindowName', $contextNode);
			$instance->folderName = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./LinkName', $contextNode);
			$instance->linkName = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			return $instance;
		}
	}