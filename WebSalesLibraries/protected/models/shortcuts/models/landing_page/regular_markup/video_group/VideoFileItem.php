<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\video_group;


	class VideoFileItem extends VideoGroupItem
	{
		public $fileUrl;
		public $placeholderUrl;

		public function getVideoUrl()
		{
			return $this->fileUrl;
		}

		public function getVideoPlaceholder()
		{
			return $this->placeholderUrl;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./File', $contextNode);
			$videoFileName = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./Placeholder', $contextNode);
			$imageFileName = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			if (empty($videoFileName) || empty($imageFileName))
				return;

			$baseUrl = \Yii::app()->getBaseUrl(true);
			$this->fileUrl = \Utils::formatUrl($baseUrl . $this->parentGroup->parentShortcut->relativeLink . '/video/' . $videoFileName);
			$this->placeholderUrl = \Utils::formatUrl($baseUrl . $this->parentGroup->parentShortcut->relativeLink . '/images/' . $imageFileName);

			$this->isConfigured = true;
		}
	}