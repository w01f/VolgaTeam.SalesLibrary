<?php

	/**
	 * Class UrlShortcut
	 */
	class UrlShortcut extends BaseShortcut
	{
		/**
		 * @param $linkRecord
		 */
		public function __construct($linkRecord)
		{
			parent::__construct($linkRecord);
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);

			$this->viewPath = 'directLink';

			$this->sourceLink = trim($linkConfig->getElementsByTagName("Source")->item(0)->nodeValue);
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
					'action' => 'Shortcut Web Link',
					'title' => sprintf('%s - %s', $this->name, $this->tooltip)
				)) . '</div>';
			return $result;
		}
	}