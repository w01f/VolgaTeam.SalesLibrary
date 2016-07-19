<?php

	/**
	 * Class SuperFilter
	 */
	class SuperFilter
	{
		/**
		 * @var string
		 * @soap
		 */
		public $value;
		public $selected;

		public function load($superFilterRecord)
		{
			$this->value = $superFilterRecord->value;
		}
	}
