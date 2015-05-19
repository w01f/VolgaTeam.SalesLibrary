<?php

	/**
	 * Class QuickListShortcut
	 */
	class QuickListShortcut extends BaseShortcut
	{
		public $ribbonLogoPath;
		public $fileLinks;

		/**
		 * @param $linkRecord
		 */
		public function __construct($linkRecord)
		{
			parent::__construct($linkRecord);
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);

			$this->viewPath = 'directLink';

			$samePageTags = $linkConfig->getElementsByTagName("OpenOnSamePage");
			$this->samePage = $samePageTags->length > 0 ? filter_var(trim($samePageTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
			$baseUrl = Yii::app()->getBaseUrl(true);
			$this->imagePath = $baseUrl . $linkRecord->image_path . '?' . $linkRecord->id_page . $linkRecord->id;
			$this->ribbonLogoPath = $baseUrl . $linkRecord->source_path . '/rbnlogo.png' . '?' . $linkRecord->id_page . $linkRecord->id;
			$this->sourceLink = Yii::app()->createAbsoluteUrl('shortcuts/getQuickList', array('linkId' => $linkRecord->id, 'samePage' => $this->samePage));
		}

		/**
		 *
		 */
		public function loadQuickLinks()
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);
			$baseUrl = Yii::app()->getBaseUrl(true);
			$quickListPath = $baseUrl . $this->linkRecord->source_path . '/quicklist/';
			$fileNodes = $linkConfig->getElementsByTagName("File");
			foreach ($fileNodes as $fileNode)
			{
				$fileName = trim($fileNode->nodeValue);
				$fileLink = new QuickListFile();
				$fileLink->name = $fileName;
				$fileLink->link = str_replace('&', '%26', str_replace(' ', '%20', $quickListPath . $fileName));
				$this->fileLinks[] = $fileLink;
			}
		}

		/**
		 * @return string
		 */
		public function getServiceData()
		{
			$result = '';
			if (isset($this->ribbonLogoPath) && @getimagesize($this->ribbonLogoPath))
				$result .= '<div class="ribbon-logo-path">' . $this->ribbonLogoPath . '</div>';
			$result .= '<div class="link-id">' . $this->id . '</div>';
			$result .= '<div class="link-type">' . $this->type . '</div>';
			$result .= '<div class="link-name">' . $this->name . ' - ' . $this->tooltip . '</div>';
			$result .= '<div class="url">' . $this->sourceLink . '</div>';
			$result .= '<div class="activity-data">' . CJSON::encode(array(
					'action' => 'Shortcut Quick List Link',
					'title' => sprintf('%s - %s', $this->name, $this->tooltip)
				)) . '</div>';
			return $result;
		}
	}