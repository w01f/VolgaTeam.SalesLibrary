<?

	/**
	 * Class SearchLinkViewsSettings
	 */
	class SearchViewCountSettings
	{
		public $useDateRange;
		public $startDate;
		public $endDate;

		public function __construct()
		{
			$this->useDateRange = false;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return SearchViewCountSettings
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = new self();

			$today = date(Yii::app()->params['mysqlDateFormat']);
			$queryResult = $xpath->query('./StartDate', $contextNode);
			$startDateText = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			if (isset($startDateText))
			{
				switch ($startDateText)
				{
					case "today":
						$instance->startDate = date(Yii::app()->params['mysqlDateFormat'], strtotime($today . ' + 1 days'));
						break;
					case "yesterday":
						$instance->startDate = date(Yii::app()->params['mysqlDateFormat'], strtotime($today . ' - 1 days'));
						break;
					default:
						if (strstr($startDateText, ' days ago'))
						{
							$startDateText = str_replace(' days ago', '', $startDateText);
							$instance->startDate = date(Yii::app()->params['mysqlDateFormat'], strtotime($today . ' - ' . $startDateText . ' days'));
						}
						else
							$instance->startDate = $startDateText;
				}
				$instance->useDateRange = true;
			}
			$queryResult = $xpath->query('./EndDate', $contextNode);
			$endDateText = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : ($instance->useDateRange ? 'today' : null);
			if (isset($endDateText))
			{
				switch ($endDateText)
				{
					case "today":
						$instance->endDate = date(Yii::app()->params['mysqlDateFormat'], strtotime($today . ' + 1 days'));
						break;
					case "yesterday":
						$instance->endDate = date(Yii::app()->params['mysqlDateFormat'], strtotime($today . ' - 1 days'));
						break;
					default:
						if (strstr($endDateText, ' days ago'))
						{
							$endDateText = str_replace(' days ago', '', $endDateText);
							$instance->endDate = date(Yii::app()->params['mysqlDateFormat'], strtotime($today . ' - ' . $endDateText . ' days'));
						}
						else
							$instance->endDate = $endDateText;
				}
			}

			return $instance;
		}
	}