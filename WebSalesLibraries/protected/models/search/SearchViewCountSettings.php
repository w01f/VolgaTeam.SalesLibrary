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
						$instance->startDate = $today;
						break;
					case "yesterday":
						$instance->startDate = date(Yii::app()->params['mysqlDateFormat'], strtotime($today . ' - 1 days'));
						break;
					case "current week":
						$instance->startDate = date(Yii::app()->params['mysqlDateFormat'], strtotime("last Monday"));
						break;
					case "last week":
						$instance->startDate = date(Yii::app()->params['mysqlDateFormat'], strtotime("Monday last week"));
						break;
					case "current month":
						$instance->startDate = date(Yii::app()->params['mysqlDateFormat'], strtotime(date('Y-m-1')));
						break;
					default:
						if (strstr($startDateText, ' days ago'))
						{
							$daysCount = intval(str_replace(' days ago', '', $startDateText));
							if ($daysCount > 0)
								$instance->startDate = date(Yii::app()->params['mysqlDateFormat'], strtotime($today . ' - ' . $daysCount . ' days'));
							else
								$instance->startDate = $today;
						}
						else if (!empty($startDateText))
							$instance->startDate = date(Yii::app()->params['mysqlDateFormat'], strtotime($startDateText));
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
					case "current week":
					case "current month":
						$instance->endDate = date(Yii::app()->params['mysqlDateFormat'], strtotime($today . ' + 1 days'));
						break;
					case "yesterday":
						$instance->endDate = $today;
						break;
					case "last week":
						$instance->endDate = date(Yii::app()->params['mysqlDateFormat'], strtotime("last Monday"));
						break;
					default:
						if (strstr($endDateText, ' days ago'))
						{
							$daysCount = intval(str_replace(' days ago', '', $endDateText));
							if ($daysCount > 0)
								$instance->endDate = date(Yii::app()->params['mysqlDateFormat'], strtotime($today . ' - ' . $daysCount . ' days'));
							else
								$instance->endDate = $today;
						}
						else if (!empty($endDateText))
							$instance->endDate = date(Yii::app()->params['mysqlDateFormat'], strtotime($endDateText));
				}
			}

			return $instance;
		}
	}