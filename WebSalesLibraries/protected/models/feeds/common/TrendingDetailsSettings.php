<?

	namespace application\models\feeds\common;


	class TrendingDetailsSettings
	{
		public $todayDetailsUrl;
		public $weekDetailsUrl;
		public $monthDetailsUrl;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return TrendingDetailsSettings
		 * @throws \Exception
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = new TrendingDetailsSettings();

			$queryResult = $xpath->query('./Details/TodayShortcutId', $contextNode);
			$instance->todayDetailsUrl = $queryResult->length > 0 ?
				\PageContentShortcut::createShortcutUrl(trim($queryResult->item(0)->nodeValue), false) :
				'#';

			$queryResult = $xpath->query('./Details/WeekShortcutId', $contextNode);
			$instance->weekDetailsUrl = $queryResult->length > 0 ?
				\PageContentShortcut::createShortcutUrl(trim($queryResult->item(0)->nodeValue), false) :
				'#';

			$queryResult = $xpath->query('./Details/MonthShortcutId', $contextNode);
			$instance->monthDetailsUrl = $queryResult->length > 0 ?
				\PageContentShortcut::createShortcutUrl(trim($queryResult->item(0)->nodeValue), false) :
				'#';

			return $instance;
		}
	}