<?php
	use application\models\wallbin\models\web\SuperFilter as SuperFilter;

	/**
	 * Class SoapSuperFilter
	 */
	class SoapSuperFilter
	{
		/**
		 * @var string
		 * @soap
		 */
		public $value;

		/**
		 * @param SuperFilter $superFilter
		 * @return SoapSuperFilter
		 */
		public static function load($superFilter)
		{
			$soapSuperFilter = new SoapSuperFilter();
			$soapSuperFilter->value = $superFilter->value;
			return $soapSuperFilter;
		}
	}
