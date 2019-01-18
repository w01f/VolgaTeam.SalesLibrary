<?

	namespace application\models\calendar\models;


	class EmailSettings
	{
		public $enabled;
		public $from;
		public $to;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return EmailSettings
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = self::createDefault();

			$queryResult = $xpath->query('./EnableEmail', $contextNode);
			$instance->enabled = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $instance->enabled;

			$queryResult = $xpath->query('./SendFrom', $contextNode);
			$instance->from = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $instance->from;

			$queryResult = $xpath->query('./SendTo/Email', $contextNode);
			foreach ($queryResult as $groupNode)
				$instance->to[] = trim($groupNode->nodeValue);

			return $instance;
		}

		/**
		 * @return EmailSettings
		 */
		public static function createDefault()
		{
			$instance = new self();
			$instance->enabled = false;
			$instance->to = array();
			return $instance;
		}
	}