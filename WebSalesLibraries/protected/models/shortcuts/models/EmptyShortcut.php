<?php

	/**
	 * Class EmptyShortcut
	 */
	class EmptyShortcut extends BaseShortcut
	{
		/**
		 * @param $linkRecord
		 */
		public function __construct($linkRecord)
		{
			parent::__construct($linkRecord);
			$this->viewPath = 'emptyLink';
		}

		/**
		 * @return string
		 */
		public function getServiceData()
		{
			return '';
		}
	}