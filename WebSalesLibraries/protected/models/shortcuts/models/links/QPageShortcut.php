<?php

	/**
	 * Class QPageShortcut
	 */
	class QPageShortcut extends UrlShortcut
	{
		public $pageId;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);

			$urlData = parse_url($this->sourceLink);
			$this->pageId = str_replace('id=', '', $urlData['query']);
		}

		/**
		 * @return string
		 */
		public function getMenuItemData()
		{
			$result = parent::getMenuItemData();
			if ($this->isPhone)
			{
				$result .= '<div class="has-custom-handler"></div>';
				$result .= '<div class="same-page"></div>';
			}
			return $result;
		}

		/**
		 * @return array
		 */
		public function getViewParameters()
		{
			$viewParameters = parent::getViewParameters();
			$viewParameters['page'] = QPageRecord::model()->findByPk($this->pageId);
			return $viewParameters;
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Qpage';
		}
	}