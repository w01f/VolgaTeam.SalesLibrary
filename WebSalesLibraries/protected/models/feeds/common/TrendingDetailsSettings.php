<?

	namespace application\models\feeds\common;


	class TrendingDetailsSettings
	{
		public $openSamePage;

		public $todayDetailsLinkId;
		public $weekDetailsLinkId;
		public $monthDetailsLinkId;
		public $allTimeDetailsLinkId;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return TrendingDetailsSettings
		 * @throws \Exception
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = new TrendingDetailsSettings();

			$queryResult = $xpath->query('./Details/OpenOnSamePage', $contextNode);
			$instance->openSamePage = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

			$queryResult = $xpath->query('./Details/TodayShortcutId', $contextNode);
			$instance->todayDetailsLinkId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./Details/WeekShortcutId', $contextNode);
			$instance->weekDetailsLinkId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./Details/MonthShortcutId', $contextNode);
			$instance->monthDetailsLinkId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./Details/AllTimeShortcutId', $contextNode);
			$instance->allTimeDetailsLinkId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			return $instance;
		}
	}