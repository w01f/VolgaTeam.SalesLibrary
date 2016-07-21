<?php
	namespace application\models\wallbin\models\web;

	/**
	 * Class SuperFilter
	 */
	class SuperFilter
	{
		public $value;
		public $selected;

		public function load($superFilterRecord)
		{
			$this->value = $superFilterRecord->value;
		}
	}
