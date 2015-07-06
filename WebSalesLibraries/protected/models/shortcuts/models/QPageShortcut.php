<?php

	/**
	 * Class QPageShortcut
	 */
	class QPageShortcut extends UrlShortcut
	{
		public $pageId;

		/**
		 * @param $linkRecord
		 */
		public function __construct($linkRecord)
		{
			parent::__construct($linkRecord);

			$urlData = parse_url($this->sourceLink);
			$this->pageId = str_replace('id=', '', $urlData['query']);
		}
	}