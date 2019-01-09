<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\video_group;

	class VideoPlaceholder
	{
		public $ready;
		public $notReady;
		public $inProgress;
		public $complete;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @param $relativeRootLink string
		 * @param $defaultUrl string
		 * @return VideoPlaceholder
		 */
		public static function fromXml($xpath, $contextNode, $relativeRootLink, $defaultUrl)
		{
			$instance = self::createDefault($defaultUrl);

			$baseUrl = \Yii::app()->getBaseUrl(true);

			$queryResult = $xpath->query('./Ready', $contextNode);
			$instance->ready = $queryResult->length > 0 ? \Utils::formatUrl($baseUrl . $relativeRootLink . '/images/' . trim($queryResult->item(0)->nodeValue)) : $defaultUrl;

			$queryResult = $xpath->query('./NotYet', $contextNode);
			$instance->notReady = $queryResult->length > 0 ? \Utils::formatUrl($baseUrl . $relativeRootLink . '/images/' . trim($queryResult->item(0)->nodeValue)) : $defaultUrl;

			$queryResult = $xpath->query('./InProgress', $contextNode);
			$instance->inProgress = $queryResult->length > 0 ? \Utils::formatUrl($baseUrl . $relativeRootLink . '/images/' . trim($queryResult->item(0)->nodeValue)) : $defaultUrl;

			$queryResult = $xpath->query('./Complete', $contextNode);
			$instance->complete = $queryResult->length > 0 ? \Utils::formatUrl($baseUrl . $relativeRootLink . '/images/' . trim($queryResult->item(0)->nodeValue)) : $defaultUrl;

			return $instance;
		}

		/**
		 * @param $defaultUrl string
		 * @return VideoPlaceholder
		 */
		public static function createDefault($defaultUrl)
		{
			$instance = new self();
			$instance->ready = $defaultUrl;
			$instance->notReady = $defaultUrl;
			$instance->inProgress = $defaultUrl;
			$instance->complete = $defaultUrl;
			return $instance;
		}
	}