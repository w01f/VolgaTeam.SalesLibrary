<?

	namespace application\models\feeds\common;


	class SimpleDetailsSettings
	{
		public $openSamePage;

		public $detailsLinkId;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return SimpleDetailsSettings
		 * @throws \Exception
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = new SimpleDetailsSettings();

			$queryResult = $xpath->query('./Details/OpenOnSamePage', $contextNode);
			$instance->openSamePage = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

			$queryResult = $xpath->query('./Details/ShortcutId', $contextNode);
			$instance->detailsLinkId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			return $instance;
		}
	}