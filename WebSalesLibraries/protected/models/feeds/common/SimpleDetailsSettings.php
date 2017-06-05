<?

	namespace application\models\feeds\common;


	class SimpleDetailsSettings
	{
		public $detailsUrl;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return SimpleDetailsSettings
		 * @throws \Exception
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = new SimpleDetailsSettings();

			$queryResult = $xpath->query('./Details/ShortcutId', $contextNode);
			$instance->detailsUrl = $queryResult->length > 0 ?
				\PageContentShortcut::createShortcutUrl(trim($queryResult->item(0)->nodeValue), false) :
				'#';

			return $instance;
		}
	}